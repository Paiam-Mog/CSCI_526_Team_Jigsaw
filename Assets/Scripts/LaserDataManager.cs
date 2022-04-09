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

    private readonly string PrismTag = "Prism";

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
    private List<float> angles1 = new List<float> { 60f, 180f };
    private List<float> angles2 = new List<float> { 60f, -60f };
    private List<float> angles3 = new List<float> { 180f, -60f };

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

    // Start is called before the first frame update
    void Start()
    {
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
                else if (_hit.transform.tag == PrismTag)
                {
                    float angle = 0.0f;
                    if (!flag)
                    {
                        Debug.Log("hit angle:" + Vector2.Angle(prev, _hit.point));
                        angle = Vector2.SignedAngle(prev, _hit.point);
                    }

                    collidedPrism = _hit.collider.gameObject;
                    PolygonCollider2D sr = collidedPrism.GetComponent<PolygonCollider2D>();

                    Transform prismTransform = collidedPrism.transform;
                    Vector2 _prismLaserDir = prismTransform.right;
                    Vector3 scaleFactor = prismTransform.localScale;
                    
                    for (int i = 0; i < sr.points.Length; i++)
                    {
                        Debug.Log(sr.points[i]);
                    }

                    Vector2 pivot = prismTransform.position;

                    Vector2 prismVertex1 = pivot + sr.points[0] * scaleFactor.x;
                    Vector2 prismVertex2 = pivot + sr.points[1] * scaleFactor.x;
                    Vector2 prismVertex3 = pivot + sr.points[2] * scaleFactor.x;

                    //Vector2 offset = collidedPrism.transform.position - sr.bounds.center;
                    //Debug.Log(offset);

                    Debug.Log(prismVertex1);
                    Debug.Log(prismVertex2);
                    Debug.Log(prismVertex3);

                    float distanceFromVertex1 = Vector2.Distance(prismVertex1, _hit.point);
                    float distanceFromVertex2 = Vector2.Distance(prismVertex2, _hit.point);
                    float distanceFromVertex3 = Vector2.Distance(prismVertex3, _hit.point);

                    Vector2 vertex1 = new Vector2();
                    Vector2 vertex2 = new Vector2();

                    if (distanceFromVertex1 < distanceFromVertex2 && distanceFromVertex3 < distanceFromVertex2)
                    {
                        side = 2;

                        angles2[0] -= angle * scaleFactor.z;
                        angles2[1] -= angle * scaleFactor.z;

                        vertex1 = (prismVertex1 + prismVertex2) / 2;
                        vertex2 = (prismVertex3 + prismVertex2) / 2;
                    }
                    else if (distanceFromVertex2 < distanceFromVertex3 && distanceFromVertex1 < distanceFromVertex3)
                    {
                        side = 3;

                        angles3[0] -= angle * scaleFactor.z;
                        angles3[1] -= angle * scaleFactor.z;

                        vertex1 = (prismVertex1 + prismVertex3) / 2;
                        vertex2 = (prismVertex2 + prismVertex3) / 2;
                    }
                    else if (distanceFromVertex3 < distanceFromVertex1 && distanceFromVertex2 < distanceFromVertex1)
                    {
                        side = 1;

                        angles1[0] -= angle * scaleFactor.z;
                        angles1[1] -= angle * scaleFactor.z;

                        vertex1 = (prismVertex2 + prismVertex1) / 2;
                        vertex2 = (prismVertex3 + prismVertex1) / 2;
                    }
                   
                    Debug.Log(vertex2);
                    Debug.Log(vertex1);

                    Vector2 _dir1 = Quaternion.Euler(0, 0, (side == 1 ? angles1[0] : side == 2 ? angles2[0] : angles3[0])) * _prismLaserDir;
                    Vector2 _dir2 = Quaternion.Euler(0, 0, (side == 1 ? angles1[1] : side == 2 ? angles2[1] : angles3[1])) * _prismLaserDir;

                    ReflectionPoint point1 = new ReflectionPoint
                    {
                        position = vertex1,
                        entryLaser = laser,
                        mirror = collidedPrism
                    };

                    ReflectionPoint point2 = new ReflectionPoint
                    {
                        position = vertex2,
                        entryLaser = laser,
                        mirror = collidedPrism
                    };

                    colors = colorTable.RefractColor(color);
                    GenerateLaserData(vertex1, _dir1, point1, count, colors[0]);
                    GenerateLaserData(vertex2, _dir2, point2, count, colors[1]);

                    prev = _hit.point;
                    flag = false;

                }
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
