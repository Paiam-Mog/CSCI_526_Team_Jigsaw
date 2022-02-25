using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserDataManager : MonoBehaviour
{
    [SerializeField]
    private ColorState color;

    [SerializeField]
    private float maxDist = 100f;

    [SerializeField]
    private Transform firepos;

    public class LaserData
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        // public ColorState laserColor;
        //ReflectionPoint originReflection;
        //ReflectionPoint endingReflection;
    }

    public class ReflectionPoint
    {
        public Vector2 position;
        public LaserData entryLaser;
        public LaserData exitLaser;
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

    private int maxCount = 10;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        startPos = firepos.position;
        dir = firepos.right;
        laserDatas = new List<LaserData>();
        reflectionPoints = new List<ReflectionPoint>();
        GenerateLaserData(startPos, dir, null, 1);
    }

    public void GenerateLaserData(Vector2 _startPos, Vector2 _dir, ReflectionPoint prev_reflection, int count)
    {
       
        RaycastHit2D _hit;

        _hit = Physics2D.Raycast(_startPos+ _dir * 1.1f, _dir);
        if (_hit.collider != null)
        {
            LaserData laser = new LaserData();
            laser.startPoint = _startPos;
            laser.endPoint = _hit.point;

            if(prev_reflection != null){
                prev_reflection.exitLaser = laser;
                reflectionPoints.Add(prev_reflection);
            }

            laserDatas.Add(laser);

            float distance = Vector2.Distance(_startPos, _hit.point);
            Debug.DrawRay(_startPos, _dir * distance, Color.red);

            if (count <= maxCount)
            {
                count++;
                if (_hit.transform.tag == "Mirror")
                {
                    Vector2 reflectedDir = Vector2.Reflect(_dir, _hit.normal);

                    ReflectionPoint reflectionPoint = new ReflectionPoint();
                    reflectionPoint.position = _hit.point;
                    reflectionPoint.entryLaser = laser;

                    GenerateLaserData(_hit.point, reflectedDir, reflectionPoint, count);
                }
                else if (_hit.transform.tag == "Target")
                {
                    collidedTarget = _hit.collider.gameObject;
                    collidedTarget.GetComponent<Target>().DetectTarget(_hit.point, color);
                }
                else
                {
                    Debug.Log("No Surface");
                }


            }
        }
        else
        {
            Debug.Log("collider null");
        }

        // int c = 1;
        // foreach (var x in laserDatas)
        // {
        //  Debug.Log("Reflection " + c + " " + x.startPoint + " " + x.endPoint);
        //  c++;
        // }
 
        //onLaserDataGenerated.Invoke();
   
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
