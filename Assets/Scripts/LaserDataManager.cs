using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserDataManager : MonoBehaviour
{
    [SerializeField]
    private ColorState initColor;

    [SerializeField]
    private float maxDist = 100f;

    [SerializeField]
    private Transform firepos;

    private readonly string MirrorTag = "Mirror";

    private readonly string TargetTag = "Target";

    private readonly int maxCount = 10;

    
    public class LaserData
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        public ColorState laserColor;
    }

    public class ReflectionPoint
    {
        public Vector2 position;
        public LaserData entryLaser;
        public LaserData exitLaser;
        public GameObject mirror;
    }

    private Vector2 dir; // Could be replaced by using the initialLaserPoint’s forward 
    private Vector2 startPos;
    public UnityAction onLaserDataGenerated;    // need to include UnityEngine.Events library
    private GameObject collidedMirror;
    private GameObject collidedTarget;

    [SerializeField]
    private List<LaserData> laserDatas = new List<LaserData>();

    [SerializeField]
    private List<ReflectionPoint> reflectionPoints = new List<ReflectionPoint>();

    private GameObject[] mirrors;
    private Dictionary<GameObject, ColorState> mirrorsDict;

    private ColorTable colorTable;

    // Start is called before the first frame update
    void Start()
    {
        mirrorsDict = new Dictionary<GameObject, ColorState>();
        colorTable = new ColorTable();

        mirrors = GameObject.FindGameObjectsWithTag(MirrorTag);
        foreach(var mirror in mirrors)
        {
            mirrorsDict.Add(mirror, colorTable.GetColorState(mirror.GetComponent<SpriteRenderer>().color));
        }

        startPos = firepos.position;
        dir = firepos.right;
        laserDatas = new List<LaserData>();
        reflectionPoints = new List<ReflectionPoint>();
        GenerateLaserData(startPos, dir, null, 1, initColor);
    }

    // Update is called once per frame
    void Update()
    {
        startPos = firepos.position;
        dir = firepos.right;
        laserDatas = new List<LaserData>();
        reflectionPoints = new List<ReflectionPoint>();
        GenerateLaserData(startPos, dir, null, 1, initColor);
    }

    public void GenerateLaserData(Vector2 _startPos, Vector2 _dir, ReflectionPoint prev_reflection, int count, ColorState color)
    {
        RaycastHit2D _hit;

        _hit = Physics2D.Raycast(_startPos+ _dir * 1.1f, _dir);
        if (_hit.collider != null)
        {
            LaserData laser = new LaserData();
            laser.startPoint = _startPos;
            laser.endPoint = _hit.point;
            laser.laserColor = color;

            if(prev_reflection != null)
            {
                prev_reflection.exitLaser = laser;
                reflectionPoints.Add(prev_reflection);
            }

            laserDatas.Add(laser);

            //float distance = Vector2.Distance(_startPos, _hit.point);
            //Debug.DrawRay(_startPos, _dir * distance, GetColor(color));

            if (count <= maxCount)
            {
                count++;
                if (_hit.transform.tag == MirrorTag)
                {
                    Vector2 reflectedDir = Vector2.Reflect(_dir, _hit.normal);

                    collidedMirror = _hit.collider.gameObject;
                    ColorState mirrorColor = colorTable.GetColorState(collidedMirror.GetComponent<SpriteRenderer>().color);

                    ReflectionPoint reflectionPoint = new ReflectionPoint();
                    reflectionPoint.position = _hit.point;
                    reflectionPoint.entryLaser = laser;
                    reflectionPoint.mirror = collidedMirror;

                    GenerateLaserData(_hit.point, reflectedDir, reflectionPoint, count, colorTable.ChangeColor(color, mirrorColor));
                }
                else if (_hit.transform.tag == TargetTag)
                {
                    collidedTarget = _hit.collider.gameObject;
                    collidedTarget.GetComponent<Target>().DetectTarget(_hit.point, color);
                }
                //else
                //{
                //    Debug.Log("No Surface");
                //}
            }
        }
        else
        {
            Debug.Log("collider null");
        }
   
    }

    public List<LaserData> GetLaserDatas()
    {
        return laserDatas;
    }

    public List<ReflectionPoint> GetReflectionPoints()
    {
        return reflectionPoints;
    }
}
