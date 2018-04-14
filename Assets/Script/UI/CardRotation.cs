using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CardRotation : MonoBehaviour 
{
	[SerializeField] public RectTransform _cardFront;
	[SerializeField] public RectTransform _cardBack;
	[SerializeField] public Transform _cardRectTransform;

	private bool _showingBack = false;

	void Update ()
	{
		var angle = Mathf.Abs(_cardRectTransform.localEulerAngles.y);

		if (!_showingBack && angle > 90f && angle < 270f)
		{
			_showingBack = true;
			_cardFront.gameObject.SetActive(false);
			_cardBack.gameObject.SetActive(true);
		}
		else if (_showingBack && angle < 90f || angle > 270f)
		{
			_showingBack = false;
			_cardFront.gameObject.SetActive(true);
			_cardBack.gameObject.SetActive(false);
		}
	}
}

