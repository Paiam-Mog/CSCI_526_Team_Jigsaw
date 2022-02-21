using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private ColorState color;
    [SerializeField]
    private ColorState newLaserColor;

    private LineRenderer laser;

    private GameObject collidedMirror;
    private GameObject collidedTarget;

    private bool collideWithMirror = false;
    private bool collideWithTarget = false;

    private void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        InitLaserColor();
    }

    public void ReflectedLaser(Vector2 startPos, Vector2 direction, ColorState inputColor)
    {
        laser.enabled = true;

        if (Physics2D.Raycast(startPos, direction))
        {

            RaycastHit2D _hit = Physics2D.Raycast(startPos + direction * 1.1f, direction);
            Debug.Log("Hit Mirror" + _hit.point);
            DrawRay(startPos, _hit.point);
            if (_hit.transform.tag == "Mirror")
            {
                if (!collideWithMirror)
                    collideWithMirror = true;

                collidedMirror = _hit.collider.gameObject;
                Vector2 reflectedDir = Vector2.Reflect(direction, _hit.normal);
                collidedMirror.GetComponent<Mirror>().ReflectedLaser(_hit.point, reflectedDir, newLaserColor);

                
            }
            else if (_hit.transform.tag == "Target")
            {
                if (!collideWithTarget)
                    collideWithTarget = true;

                //Debug.Log("Target Detected");
                collidedTarget = _hit.collider.gameObject;
                collidedTarget.GetComponent<Target>().DetectTarget(_hit.point, newLaserColor);


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
            DrawRay(startPos, direction * 100);
        }
        ChangeColor(inputColor);
    }

    public void LaserCollisionExit()
    {
        newLaserColor = color;
        InitLaserColor();
        laser.enabled = false;
    }


    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPos);
    }

    void ChangeColor(ColorState inputColor)
    {
        if(color != inputColor)
        {
            int newColorCode = (int)color + (int)inputColor + 1;
            if(newColorCode == 4)
            {
                laser.startColor = Color.magenta;
                laser.endColor = Color.magenta;
                newLaserColor = ColorState.Magenta;
            }
            else if(newColorCode == 5)
            {
                laser.startColor = Color.yellow;
                laser.endColor = Color.yellow;
                newLaserColor = ColorState.Yellow;
            }
            else if (newColorCode == 6)
            {
                laser.startColor = Color.cyan;
                laser.endColor = Color.cyan;
                newLaserColor = ColorState.Cyan;
            }
            else
            {
                laser.startColor = Color.white;
                laser.endColor = Color.white;
                newLaserColor = ColorState.White;
            }
        }
    }

    void InitLaserColor()
    {
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
}
