using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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

    private readonly string PrismTag = "Prism";
    private readonly string PrismSide1Tag = "Prism_Side_1";
    private readonly string PrismSide2Tag = "Prism_Side_2";
    private readonly string PrismSide3Tag = "Prism_Side_3";

    [SerializeField]
    private GameObject Prism_Side_1;
    [SerializeField]
    private GameObject Prism_Side_2;
    [SerializeField]
    private GameObject Prism_Side_3;

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
    private GameObject collidedPrism;
    private Vector2 prev= new Vector2(0.0f, 0.0f);
    private bool flag = true;

    float angle1 = -60f;
    float angle2 = 60f;

    // private List<float> angles1 = new List<float> { -60f, 180f };
    // private List<float> angles2 = new List<float> { -60f, 60f };
    // private List<float> angles3 = new List<float> { 180f, 60f };

    private int side = 0;

    [SerializeField]
    private List<LaserData> laserDatas = new List<LaserData>();

    [SerializeField]
    private List<ReflectionPoint> reflectionPoints = new List<ReflectionPoint>();

    private GameObject[] mirrors;
    private GameObject[] prisms;
    private Dictionary<GameObject, ColorState> mirrorsDict;
    private Dictionary<GameObject, ColorState> prismsDict;
    private List<ColorState> colors = new List<ColorState>();

    private ColorTable colorTable;

    private GameObject[] particles;
    [SerializeField] public GameObject sparks;
    private int sparkCount;

    // Start is called before the first frame update
    void Start()
    {

        sparkCount = 0;
        mirrorsDict = new Dictionary<GameObject, ColorState>();
        prismsDict = new Dictionary<GameObject, ColorState>();
        colorTable = new ColorTable();

        mirrors = GameObject.FindGameObjectsWithTag(MirrorTag);
        foreach(var mirror in mirrors)
        {
            mirrorsDict.Add(mirror, colorTable.GetColorState(mirror.GetComponent<SpriteRenderer>().color));
        }

        prisms = GameObject.FindGameObjectsWithTag(PrismTag);
        foreach (var prism in prisms)
        {
            prismsDict.Add(prism, colorTable.GetColorState(prism.GetComponent<SpriteRenderer>().color));
        }

        //startPos = firepos.position;
        //dir = firepos.right;
        //laserDatas = new List<LaserData>();
        //reflectionPoints = new List<ReflectionPoint>();
        //GenerateLaserData(startPos, dir, null, 1, initColor);
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

        _hit = Physics2D.Raycast(_startPos + _dir * 1.1f, _dir);
        if (_hit.collider != null)
        {
            LaserData laser = new LaserData
            {
                startPoint = _startPos,
                endPoint = _hit.point,
                laserColor = color
            };

            if (prev_reflection != null)
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

                    ReflectionPoint reflectionPoint = new ReflectionPoint
                    {
                        position = _hit.point,
                        entryLaser = laser,
                        mirror = collidedMirror
                    };

                    GenerateLaserData(_hit.point, reflectedDir, reflectionPoint, count, colorTable.ChangeColor(color, mirrorColor));
                }
                else if (_hit.transform.tag == TargetTag)
                {
                    collidedTarget = _hit.collider.gameObject;
                    collidedTarget.GetComponent<Target>().DetectTarget(_hit.point, color);
                }
                else if(_hit.transform.tag == PrismSide1Tag){
                    RefractionPrism(_hit, Prism_Side_2, Prism_Side_3, count, laser, color);
                }
                else if(_hit.transform.tag == PrismSide2Tag){
                    RefractionPrism(_hit, Prism_Side_1, Prism_Side_3, count, laser, color);
                }
                else if(_hit.transform.tag == PrismSide3Tag){
                    RefractionPrism(_hit, Prism_Side_1, Prism_Side_2, count, laser, color);
                }
            }
        }
        else
        {
            Debug.Log("collider null");
        }
   
    }

    private void RefractionPrism(RaycastHit2D _hit, GameObject side1, GameObject side2, int count, LaserData laser, ColorState color){
        float angle = 0.0f;
        if (!flag)
        {
            angle = Vector2.SignedAngle(prev, _hit.point);
        }

        angle1 = angle1 - angle;
        angle2 = angle2 - angle;
    
        BoxCollider2D sr1 = side1.GetComponent<BoxCollider2D>();
        BoxCollider2D sr2 = side2.GetComponent<BoxCollider2D>();

        Vector2 _prismLaserDir = sr1.transform.right;

        Vector2 side1_bounds = sr1.bounds.center;
        Vector2 side2_bounds = sr2.bounds.center;

        // Debug.Log("Side 2 position:" + sr1.transform.position);
        // Debug.Log("Side 2 Bounds Centre:" + side2_bounds);

        Vector2 _dir1 = Quaternion.Euler(0, 0, angle2) * _prismLaserDir;
        Vector2 _dir2 = Quaternion.Euler(0, 0, angle1) * _prismLaserDir;

        ReflectionPoint point1 = new ReflectionPoint
        {
            position = side1_bounds,
            entryLaser = laser,
            mirror = side1
        };

        ReflectionPoint point2 = new ReflectionPoint
        {
            position = side2_bounds,
            entryLaser = laser,
            mirror = side2
        };

        colors = colorTable.RefractColor(color);

        GenerateLaserData(side1_bounds, _dir1, point1, count, colors[0]);
        GenerateLaserData(side2_bounds, _dir2, point2, count, colors[1]);

        prev = _hit.point;
        flag = false;

    }

    public List<LaserData> GetLaserDatas()
    {
        return laserDatas;
    }

    public List<ReflectionPoint> GetReflectionPoints()
    {
        return reflectionPoints;
    }

    public void SpawnParticles()
    {
        int maxSparks = reflectionPoints.Count;

        for(int i = 1; i < reflectionPoints.Count; i++)
        {
            GameObject tempSparks = Instantiate(sparks, reflectionPoints[i].position, Quaternion.Euler(dir));
            particles[i] = tempSparks;

            if(i >= maxSparks)
            {
                Destroy(particles[i - reflectionPoints.Count]);
                particles[i] = particles[i - 1];
            }
        }
    }
}
