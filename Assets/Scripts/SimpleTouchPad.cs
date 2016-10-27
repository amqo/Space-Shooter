using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : 
    MonoBehaviour,
    IPointerDownHandler, 
    IDragHandler, 
    IPointerUpHandler {

	public float smoothing;

    Vector2 origin;
    Vector2 direction;
	Vector2 smoothDirection;
	bool touched;
	int pointerID;

    void Awake()
    {
        direction = Vector2.zero;
		smoothDirection = Vector2.zero;
		touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        // Set the start point
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
    }

    public void OnPointerUp(PointerEventData data)
    {
		// Reset everything
		if (pointerID == data.pointerId) {
			direction = Vector2.zero;
			touched = false;
		}
    }

    public void OnDrag(PointerEventData data)
    {
		// Compare the difference between current point and start point
		if (pointerID == data.pointerId) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		}
    }

    public Vector2 GetDirection()
    {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
    }
}
