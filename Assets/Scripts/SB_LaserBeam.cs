using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_LaserBeam
{
    Vector2 pos, dir;

    GameObject laserObj;
    LineRenderer laser;
    List<Vector2> laserIndices = new List<Vector2>();

    public SB_LaserBeam(Vector2 pos, Vector2 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser_Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        CastRay(pos, dir, laser);

    }

    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach(Vector2 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }

    }

    void CheckHit(RaycastHit hitInfo, Vector2 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector2 pos = hitInfo.point;
            Vector2 dir = Vector2.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }

    }
    
}
