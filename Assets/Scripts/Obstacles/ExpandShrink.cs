using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandShrink : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public Transform objectToOrbit;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(objectToOrbit.position, Vector3.up, 10 * Time.deltaTime);

    }
}
