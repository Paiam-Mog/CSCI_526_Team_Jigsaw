using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour
{
    private readonly float laserWidth = 0.1f;

    public LaserDataManager laserDataManager;

    private ColorTable colorTable;

    private LineRenderer initLaser;

    public List<LaserDataManager.LaserData> laserDatas;

    public List<LaserDataManager.ReflectionPoint> reflectionPoints;

    private GameObject[] mirrors;

    // Use this for initialization
    public void Start()
    {
        initLaser = GetComponent<LineRenderer>();
        colorTable = new ColorTable();

        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
    }

    // Update is called once per frame
    public void Update()
    {
        laserDatas = laserDataManager.GetLaserDatas();
        reflectionPoints = laserDataManager.GetReflectionPoints();

        if (laserDatas.Count > 0)
        {
            DrawRay(initLaser, laserDatas[0]);
        }

        foreach(var mirror in mirrors)
        {
            mirror.GetComponent<LineRenderer>().enabled = false;
        }

        foreach(var reflectionPoint in reflectionPoints)
        {
            LineRenderer mirrorLaser = reflectionPoint.mirror.GetComponent<LineRenderer>();
            mirrorLaser.enabled = true;
            DrawRay(mirrorLaser, reflectionPoint.exitLaser);
        }
    }

    public void DrawRay(LineRenderer laser, LaserDataManager.LaserData laserData)
    {
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;
        laser.SetPosition(0, laserData.startPoint);
        laser.SetPosition(1, laserData.endPoint);
        laser.startColor = colorTable.GetColor(laserData.laserColor);
        laser.endColor = colorTable.GetColor(laserData.laserColor);
    }
}