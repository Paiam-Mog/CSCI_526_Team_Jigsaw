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

    [SerializeField]
    private List<LaserData> laserDatas = new List<LaserData>();

    [SerializeField]
    private List<ReflectionPoint> reflectionPoints = new List<ReflectionPoint>();

    private int maxCount = 10;

    private LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
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

            if(prev_reflection != null){
                prev_reflection.exitLaser = laser;
                reflectionPoints.Add(prev_reflection);
            }

            laserDatas.Add(laser);

            float distance = Vector2.Distance(_startPos, _hit.point);
            Debug.DrawRay(_startPos, _dir * distance, GetColor(color));
            //DrawRay(laserDatas);

            if (count <= maxCount)
            {
                count++;
                if (_hit.transform.tag == "Mirror")
                {
                    Vector2 reflectedDir = Vector2.Reflect(_dir, _hit.normal);

                    ReflectionPoint reflectionPoint = new ReflectionPoint();
                    reflectionPoint.position = _hit.point;
                    reflectionPoint.entryLaser = laser;

                    collidedMirror = _hit.collider.gameObject;
                    ColorState mirrorColor = GetColorState(collidedMirror.GetComponent<SpriteRenderer>().color);
                    Debug.Log("MirrorColor: "+ mirrorColor + ", LaserColor: " + color);
                    GenerateLaserData(_hit.point, reflectedDir, reflectionPoint, count, ChangeColor(color, mirrorColor));
                }
                else if (_hit.transform.tag == "Target")
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

        //int c = 1;
        //foreach (var x in laserDatas)
        //{
            //Debug.Log("Reflection " + c + " " + x.startPoint + " " + x.endPoint);
            //c++;
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

    ColorState ChangeColor(ColorState inputColor, ColorState mirrorColor)
    {
        if(inputColor != mirrorColor)
        {
            int newColorCode = (int)inputColor + (int)mirrorColor + 1;
            if(newColorCode == 4)
            {
                //laser.startColor = Color.magenta;
                //laser.endColor = Color.magenta;
                return ColorState.Magenta;
            }
            else if(newColorCode == 5)
            {
                //laser.startColor = Color.yellow;
                //laser.endColor = Color.yellow;
                return ColorState.Yellow;
            }
            else if (newColorCode == 6)
            {
                //laser.startColor = Color.cyan;
                //laser.endColor = Color.cyan;
                return ColorState.Cyan;
            }
            else
            {
                //laser.startColor = Color.white;
                //laser.endColor = Color.white;
                return ColorState.White;
            }
        }
        else
        {
            return inputColor;
        }
    }

    void InitLaserColor()
    {
        if (initColor == ColorState.Red)
        {
            laser.startColor = Color.red;
            laser.endColor = Color.red;
        }
        else if (initColor == ColorState.Blue)
        {
            laser.startColor = Color.blue;
            laser.endColor = Color.blue;
        }
        else if (initColor == ColorState.Green)
        {
            laser.startColor = Color.green;
            laser.endColor = Color.green;
        }
    }

    public ColorState GetColorState(Color color)
    {
        if (color == Color.red)
        {
            return ColorState.Red;
        }
        else if (color == Color.blue)
        {
            return ColorState.Blue;
        }
        else if (color == Color.green)
        {
            return ColorState.Green;
        }
        else if (color == Color.yellow)
        { 
            return ColorState.Yellow;
        }
        else if(color == Color.magenta)
        {
            return ColorState.Magenta;
        }
        else if (color == Color.cyan)
        {
            return ColorState.Cyan;
        }
        else
        {
            return ColorState.White;
        }
    }

    public Color GetColor(ColorState color)
    {
       if (color == ColorState.Red)
        {
            return Color.red;
        }
        else if (color == ColorState.Blue)
        {
            return Color.blue;
        }
        else if (color == ColorState.Green)
        {
            return Color.green;
        }
        else if (color == ColorState.Yellow)
        { 
            return Color.yellow;
        }
        else if(color == ColorState.Magenta)
        {
            return Color.magenta;
        }
        else if (color == ColorState.Cyan)
        {
            return Color.cyan;
        }
        else
        {
            return Color.white;
        }
    }

    void DrawRay(List<LaserData> laserDatas)
    {
        foreach (var laserData in laserDatas)
        {
            laser.SetPosition(0, laserData.startPoint);
            laser.SetPosition(1, laserData.endPoint);
            laser.startColor = GetColor(laserData.laserColor);
            laser.endColor = GetColor(laserData.laserColor);
        }
    }
}
