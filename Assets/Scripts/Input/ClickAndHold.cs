using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickAndHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEvent interactAction;
    public float delayBetweenInteractions = 0f;

    private bool interacting = false;
    private float interactionTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interacting) {
            interactionTimer += Time.deltaTime;

            if (interactionTimer >= delayBetweenInteractions) {
                interactionTimer = 0f;
                interactAction.Invoke();
			}
		}
    }

    public void OnPointerDown(PointerEventData eventData) {
        interacting = true;
	}

    public void OnPointerUp(PointerEventData eventData) {
        interacting = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        interactAction.Invoke();
	}
}
