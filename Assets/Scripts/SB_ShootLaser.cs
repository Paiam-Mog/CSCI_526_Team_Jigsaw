using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_ShootLaser : MonoBehaviour
{
    public Material material;
    SB_LaserBeam beam;

    void Update()
    {
        Destroy(GameObject.Find("Laser_Beam"));
        beam = new SB_LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
