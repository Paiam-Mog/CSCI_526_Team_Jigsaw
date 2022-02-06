using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject_BoxCollider : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("mouse position:" + Input.mousePosition);
        Debug.Log("mouse position2:" + mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Collider:" + collider);
            if (collider.OverlapPoint(mousePos))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }


        }
        if (dragging)
        {
            this.transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}
