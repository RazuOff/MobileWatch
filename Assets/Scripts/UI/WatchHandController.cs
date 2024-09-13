using System;
using UnityEngine;

public class WatchHandController : MonoBehaviour
{
	[SerializeField] private LayerMask _arrowLayer;
	private Transform _selectedArrow;
	private Vector3 touchPos;
	public event Action AnalogWatchChanged;


	private void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			touchPos = Camera.main.ScreenToWorldPoint(touch.position);
			switch (touch.phase)
			{
				case TouchPhase.Began:
					CheckTouchedArrow(touchPos);
					break;
				case TouchPhase.Moved:
					MoveArrow(touchPos);
					break;
				case TouchPhase.Ended:
					EndTouch();
					break;
			}
		}
	}

	private void CheckTouchedArrow(Vector2 touchPosition)
	{
		RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector3.forward, Mathf.Infinity, _arrowLayer);
		if (hit)
		{
			_selectedArrow = hit.collider.transform;
		}
	}

	private void MoveArrow(Vector3 touch)
	{
		if (_selectedArrow != null)
		{
			Vector2 direction = touch - _selectedArrow.position;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
			_selectedArrow.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			AnalogWatchChanged?.Invoke();
		}
	}

	private void EndTouch()
	{
		_selectedArrow = null;
	}
}