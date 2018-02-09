using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICommands : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	private bool DrawFingerAlreadyDown;

	public bool CanZoom { get; private set; }
	public bool CanRotate { get; private set; }
	public int DrawFinger { get; private set; }

	public void OnPointerDown (PointerEventData eventData)
	{
		if (DrawFingerAlreadyDown) return;
		DrawFinger = eventData.pointerId;
		DrawFingerAlreadyDown = true;

		if (eventData.selectedObject == GameObject.Find ("Rotate Button")) 
			CanRotate = true;
		else if (eventData.selectedObject == GameObject.Find ("Zoom Button")) 
			CanZoom = true;
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (DrawFingerAlreadyDown == false)
			return;
		if ( DrawFinger != eventData.pointerId )
			return;
	}
		
	public void OnPointerUp (PointerEventData eventData)
	{
		if (DrawFinger != eventData.pointerId)
			return;

		DrawFingerAlreadyDown = false;

		//GameObject.Find ("Main Camera").GetComponent<CameraNavigation> ().canRotate = false;
		//GameObject.Find ("Main Camera").GetComponent<CameraNavigation> ().canZoom = false;
	}
}
