using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;
    public bool clockwise = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(clockwise)
        {
            transform.Rotate(0f, 0f, -1f * speed);
        }
        else
        {
            transform.Rotate(0f, 0f, 1f * speed);
        }
        
    }
}
