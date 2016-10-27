using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchButtonArea : 
	MonoBehaviour,
	IPointerDownHandler,
	IPointerUpHandler {
	
	bool touched;
	int pointerID;

	void Awake() {
		touched = false;
	}

	public void OnPointerDown(PointerEventData data) {
		// Set the start point
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
		}
	}

	public void OnPointerUp(PointerEventData data) {
		// Reset everything
		if (pointerID == data.pointerId) {
			touched = false;
		}
	}

	public bool CanFire() {
		return touched;
	}
}
