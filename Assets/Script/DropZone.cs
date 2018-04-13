using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData pointEventData) {

		Draggable d = pointEventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			Destroy (d.gameObject);
		}
	}
}
