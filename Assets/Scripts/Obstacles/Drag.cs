using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
   [SerializeField]
    public float min=20f;
    [SerializeField]
    public float max=30f;
    // Use this for initialization
    void Start () {
       
        min=transform.position.x;
        max=transform.position.x+3;
   
    }
    
     [SerializeField]
     public float obstacleSpeed = 4.5f;
     
    // Update is called once per frame
    void Update()
    {
    
        transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z);
    }
}
