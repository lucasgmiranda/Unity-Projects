using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonData : EventTrigger {

	public enum ButtonType
	{
		Hold,
		Switch,
		Click
	}

	public ButtonType buttonType;

	public Sprite enabledSprite, disabledSprite;

	public bool _switch, _hold, _pointer, _click;

	int cont = 0;

	private void Update()
	{
		if(cont == 1 && _click)
		{
			cont = 0;
			_click = false;
		}
		if (cont == 0 && _click)
			cont++;
	}

	public override void OnPointerDown(PointerEventData data)
	{
		_switch = !_switch;
		_hold = !_hold;
		_click = true;
		
		if (buttonType == ButtonType.Switch)
		{
			if (_switch)
			{
				GetComponent<Image>().overrideSprite = enabledSprite;
			}
			else
			{
				GetComponent<Image>().overrideSprite = disabledSprite;
			}
		}
	}

	public override void OnPointerUp(PointerEventData data)
	{
		_hold = !_hold;
	}

	public override void OnPointerEnter(PointerEventData data)
	{
		_pointer = true;
	}

	public override void OnPointerExit(PointerEventData data)
	{
		_pointer = false;
	}
}
