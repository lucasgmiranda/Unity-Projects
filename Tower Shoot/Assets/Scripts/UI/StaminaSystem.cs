using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour {

	Slider slider;
	showFPS fPS;
	float x;

	public float timeToFill;

	void Start ()
	{
		fPS = GameObject.Find("FPS Text").GetComponent<showFPS>();
		slider = GetComponent<Slider>();
	}
	
	void Update ()
	{
		if (slider.value != 1f)
		{
			x += 1 / (timeToFill * fPS.fps);
			slider.value = Mathf.Pow(x, 2);
		}	
	}

	public void decreaseValue(float dValue)
	{
		slider.value -= dValue;
		x = 0;
	}

	public bool canFire(float minValue)
	{
		if (slider.value >= minValue)
			return true;
		else
			return false;
	}
}
