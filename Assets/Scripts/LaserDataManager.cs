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
    private Transform firePosition;

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

    private Vector2 initialLaserPoint;
    private Vector2 initialLaserDirection; // Could be replaced by using the initialLaserPoint’s forward 
    public UnityAction onLaserDataGenerated;    // need to include UnityEngine.Events library
    private GameObject collidedMirror;
    private GameObject collidedTarget;
    private bool flagMirrorReflection;
    private List<LaserData> laserDatas = new List<LaserData>();
    private List<ReflectionPoint> reflectionPoints = new List<ReflectionPoint>();


    // Start is called before the first frame update
    void Start()
    {
        flagMirrorReflection = false;
        //LaserData currentLaser = new LaserData();
        //currentLaser.startPoint = firePosition.position;
        //initialLaserDirection = transform.right;
        //GenerateLaserData(currentLaser);
    }

    // Update is called once per frame
    void Update()
    {
        //LaserData currentLaser = new LaserData();
        //currentLaser.startPoint = firePosition.position;
        flagMirrorReflection = false;
        GenerateLaserData();
    }

    public void GenerateLaserData()
    {
        //Debug.Log("Transform Laser: " + firePosition.position);

        RaycastHit2D _hit;
        if (flagMirrorReflection)
        {
            _hit = Physics2D.Raycast(initialLaserPoint + initialLaserDirection * 1.1f, initialLaserDirection);
        }
        else
        {
            _hit = Physics2D.Raycast(firePosition.position, transform.right);
            //initialLaserPosition = firePosition.position
        }

        Debug.Log(flagMirrorReflection);

        //Debug.Log("Initial Point " + initialLaserPoint + " DIRECTION " + initialLaserDirection);

        //laser.laserColor = color;
        //laser.endPoint = _hit.point;

        Debug.Log("LaserData Hit" + _hit.point);
        Debug.Log(_hit.transform.tag);

        if (_hit.transform.tag == "Mirror")
        {
            Debug.Log("Mirror");

            collidedMirror = _hit.collider.gameObject;
            Vector2 reflectedDir;
            if (flagMirrorReflection)
            {
                reflectedDir = Vector2.Reflect(initialLaserDirection, _hit.normal);
            }
            else
            {
                reflectedDir = Vector2.Reflect(transform.right, _hit.normal);
                flagMirrorReflection = true;
            }

            //collidedMirror.GetComponent<Mirror>().ReflectedLaser(_hit.point, reflectedDir, color);

            initialLaserPoint = _hit.point;
            initialLaserDirection = reflectedDir;

            Debug.Log("Direction" + initialLaserDirection);


            ////float distance = Vector3.Distance(initialLaserPoint.position, _hit.point);

            //LaserData reflectedLaser = new LaserData();
            //reflectedLaser.startPoint = _hit.point;

            //Debug.Log("Reflected Laser" + reflectedLaser.startPoint);

            //ReflectionPoint reflectionPoint = new ReflectionPoint();

            //reflectionPoint.position = _hit.point;
            //reflectionPoint.entryLaser = laser;
            //reflectionPoint.exitLaser = reflectedLaser;

            //laserDatas.Add(laser);
            //reflectionPoints.Add(reflectionPoint);

            //initialLaserPoint = _hit.point;
            //initialLaserDirection = reflectedDir;

            //foreach (var x in laserDatas)
            //{
            //    Debug.Log(x.ToString());
            //}

            //foreach (var x in reflectionPoints)
            //{
            //    Debug.Log(x.ToString());
            //}

            //GenerateLaserData(reflectedLaser);

        }
        else if (_hit.transform.tag == "Target")
        {
            Debug.Log("Target");
            //collidedTarget = _hit.collider.gameObject;
            //collidedTarget.GetComponent<Target>().DetectTarget(_hit.point, color);
        }
        // else
        // {
        // //    if (collideWithMirror)
        // //    {
        // //        collidedMirror.GetComponent<Mirror>().LaserCollisionExit();
        // //        collideWithMirror = false;
        // //    }
        // }
        //onLaserDataGenerated.Invoke();
    }

    public List<LaserData> GetLaserDatas() {
        return laserDatas;
    }

    public List<ReflectionPoint> GetReflectionPoints() {
        return reflectionPoints;
    }

}
