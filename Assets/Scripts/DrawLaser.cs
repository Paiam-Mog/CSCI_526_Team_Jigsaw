﻿using System;
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

    public Dictionary<GameObject, List<LaserDataManager.LaserData>> interactingMirrorLasers;

    public Dictionary<GameObject, List<LaserDataManager.LaserData>> interactingPrismLasers;

    private GameObject[] mirrors;

    private GameObject[] prisms1;
    private GameObject[] prisms2;
    private GameObject[] prisms3;
    // Use this for initialization
    public void Start()
    {
        initLaser = GetComponent<LineRenderer>();
        colorTable = new ColorTable();

        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        prisms1 = GameObject.FindGameObjectsWithTag("Prism_Side_1");
        prisms2 = GameObject.FindGameObjectsWithTag("Prism_Side_2");
        prisms3 = GameObject.FindGameObjectsWithTag("Prism_Side_3");
        
    }

    // Update is called once per frame
    public void Update()
    {
        laserDatas = laserDataManager.GetLaserDatas();
        reflectionPoints = laserDataManager.GetReflectionPoints();
        interactingMirrorLasers = new Dictionary<GameObject, List<LaserDataManager.LaserData>>();

        foreach (var reflectionPoint in reflectionPoints)
        {
            if (!interactingMirrorLasers.ContainsKey(reflectionPoint.mirror))
            {
                interactingMirrorLasers[reflectionPoint.mirror] = new List<LaserDataManager.LaserData>(); ;
            }

            interactingMirrorLasers[reflectionPoint.mirror].Add(reflectionPoint.exitLaser);
        }

        foreach (var mirror in mirrors)
        {
            foreach (Transform child in mirror.transform)
            {
                LineRenderer renderer = child.gameObject.GetComponent<LineRenderer>();
                renderer.enabled = false;
            }
        }

        foreach (var prism in prisms1)
        {
            foreach (Transform child in prism.transform)
            {
                LineRenderer renderer = child.gameObject.GetComponent<LineRenderer>();
                renderer.enabled = false;
            }
        }

        foreach (var prism in prisms2)
        {
            foreach (Transform child in prism.transform)
            {
                LineRenderer renderer = child.gameObject.GetComponent<LineRenderer>();
                renderer.enabled = false;
            }
        }

        foreach (var prism in prisms3)
        {
            foreach (Transform child in prism.transform)
            {
                LineRenderer renderer = child.gameObject.GetComponent<LineRenderer>();
                renderer.enabled = false;
            }
        }

        if (laserDatas.Count > 0)
        {
            DrawRay(initLaser, laserDatas[0]);
        }

        foreach (var pair in interactingMirrorLasers)
        {
            GameObject reflectionMirror = pair.Key;
            List<LaserDataManager.LaserData> mirrorLasers = pair.Value;

            int mirrorLasersCount = mirrorLasers.Count;

            for (int i = 0; i < mirrorLasersCount; i++)
            {
                if (reflectionMirror.transform.childCount < mirrorLasersCount)
                {
                    GameObject laserRenderer = Instantiate(Resources.Load("Renderer")) as GameObject;
                    laserRenderer.transform.parent = reflectionMirror.transform;
                }

                GameObject laserRendererObject = reflectionMirror.transform.GetChild(i).gameObject;
                laserRendererObject.name = $"{reflectionMirror.name} - {i + 1}";
                LineRenderer renderer = laserRendererObject.GetComponent<LineRenderer>();
                renderer.enabled = true;
                DrawRay(renderer, mirrorLasers[i]);
            }
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