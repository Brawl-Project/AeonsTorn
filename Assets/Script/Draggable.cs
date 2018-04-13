using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform cardDeck = null;

	public void OnBeginDrag(PointerEventData pointerEventData) {
		cardDeck = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);
		Debug.Log (this.transform.parent.name);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData pointerEventData) {
		Debug.Log ("On drag" + this.transform.parent.name);
		this.transform.position = pointerEventData.position;
	}

	public void OnEndDrag(PointerEventData pointerEventData) {
		this.transform.SetParent (cardDeck);
		Debug.Log ("end drag " + this.transform.parent.name);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
