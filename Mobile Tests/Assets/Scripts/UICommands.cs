using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICommands : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	private bool DrawFingerAlreadyDown;
    private int drawFinger;

	public bool pressed { get; private set; }
	public int DrawFinger { get; private set; }

	public void OnPointerDown (PointerEventData eventData)
	{
		if (DrawFingerAlreadyDown) return;
		drawFinger = eventData.pointerId;
		DrawFingerAlreadyDown = true;

        SetDrawFinger();
        pressed = true;

	}

	public void OnDrag (PointerEventData eventData)
	{
		if (DrawFingerAlreadyDown == false)
			return;
		if ( drawFinger != eventData.pointerId )
			return;
	}
		
	public void OnPointerUp (PointerEventData eventData)
	{
		if (drawFinger != eventData.pointerId)
			return;

		DrawFingerAlreadyDown = false;

        pressed = false;
    }

    public void SetDrawFinger()
    {
        if (drawFinger == 1)
            DrawFinger = 0;
        else if (drawFinger == 0)
            DrawFinger = 1;
    }
}


