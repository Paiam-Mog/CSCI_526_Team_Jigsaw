using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    [SerializeField]
    private ColorState color;

    [SerializeField]
    private float maxDist = 100f;

    [SerializeField]
    private Transform firePos;

    [SerializeField] // Debug
    private bool collideWithMirror = false;
    private bool collideWithTarget = false;

    private LineRenderer laser;
    private GameObject collidedMirror;
    private GameObject collidedTarget;
    private void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;

        if (color == ColorState.Red)
        {
            laser.startColor = Color.red;
            laser.endColor = Color.red;
        }
        else if (color == ColorState.Blue)
        {
            laser.startColor = Color.blue;
            laser.endColor = Color.blue;
        }
        else if (color == ColorState.Green)
        {
            laser.startColor = Color.green;
            laser.endColor = Color.green;
        }
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {

        SB_Analytics sb_Analytics = new SB_Analytics();

        sb_Analytics.levelCompleteTime(1,1);

        if (Physics2D.Raycast(firePos.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePos.position, transform.right);
            DrawRay(firePos.position, _hit.point);
            if (_hit.transform.tag == "Mirror")
            {
                if (!collideWithMirror)
                    collideWithMirror = true;

                collidedMirror = _hit.collider.gameObject;
                Vector2 reflectedDir = Vector2.Reflect(transform.right, _hit.normal);
                collidedMirror.GetComponent<Mirror>().ReflectedLaser(_hit.point, reflectedDir, color);
            }  
            else if(_hit.transform.tag == "Target")
            {
                if (!collideWithTarget)
                    collideWithTarget = true;

                //Debug.Log("Target Detected");
                collidedTarget = _hit.collider.gameObject;
                collidedTarget.GetComponent<Target>().DetectTarget(_hit.point,color);


            }
            else 
            {
                if (collideWithMirror)
                {
                    collidedMirror.GetComponent<Mirror>().LaserCollisionExit();
                    collideWithMirror = false;
                }
            }
        }
        else
        {
            DrawRay(firePos.position, firePos.right * maxDist);
        }

        sb_Analytics.levelCompleteTime(2,20);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPos);   
    }
}
