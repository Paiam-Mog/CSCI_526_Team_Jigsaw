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

    private bool flagMirrorReflection = false;


    public class LaserData
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        public ColorState laserColor;
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
    private List<LaserData> laserDatas = new List<LaserData>();
    private List<ReflectionPoint> reflectionPoints = new List<ReflectionPoint>();
    private int maxCount = 10;



    // Start is called before the first frame update
    void Start()
    {

        //LaserData currentLaser = new LaserData();
        //currentLaser.startPoint = firePosition.position;
        //initialLaserDirection = transform.right;

        // LaserData currentLaser = new LaserData();
        //currentLaser.startPoint = initialLaserPoint.position;
        //  initialLaserDirection = transform.right;
        // GenerateLaserData();

    }

    // Update is called once per frame
    void Update()
    {

        startPos = firepos.position;
        dir = firepos.right;
        GenerateLaserData(startPos, dir, 1);
    }

    public void GenerateLaserData(Vector2 _startPos, Vector2 _dir, int count)
    {
        Debug.Log("step:5" + count);
        Debug.Log("step:6" + _dir);
        RaycastHit2D _hit;

        if (!flagMirrorReflection)
        {
            _hit = Physics2D.Raycast(_startPos, dir);
            float distance = Vector2.Distance(_startPos, _hit.point);
            Debug.DrawRay(_startPos, _dir * distance, Color.red);
            Debug.Log("tag is" + _hit.transform.tag);
            if (count <= maxCount)
            {
                count++;
                if (_hit.transform.tag == "Mirror")
                {
                    Vector2 reflectedDir = Vector2.Reflect(_dir, _hit.normal);
                    GenerateLaserData(_hit.point, reflectedDir, count);
                    Debug.Log("tag " + count);
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

            // _hit = Physics2D.Raycast(initialLaserPoint.position, initialLaserDirection);


            //laser.laserColor = color;
            //laser.endPoint = _hit.point;


        }


        // float distance = Vector3.Distance(initialLaserPoint.position, _hit.point);
        //LaserData reflectedLaser = new LaserData();
        // reflectedLaser.startPoint = _hit.point;

        // Debug.Log("Reflected Laser" + reflectedLaser.startPoint);

        // ReflectionPoint reflectionPoint = new ReflectionPoint();

        // reflectionPoint.position = _hit.point;
        //reflectionPoint.entryLaser = laser;
        //reflectionPoint.exitLaser = reflectedLaser;
        // laserDatas.Add(laser);
        //reflectionPoints.Add(reflectionPoint);



        //foreach (var x in reflectionPoints)
        //{
        //  Debug.Log(x.ToString());
        //}

        //            GenerateLaserData();



        //else
        //{
        //    if (collideWithMirror)
        //    {
        //        collidedMirror.GetComponent<Mirror>().LaserCollisionExit();
        //        collideWithMirror = false;
        //    }
        //}
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
