using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour {

	Slider slider;
	
	void Start ()
	{
		slider = GetComponent<Slider>();
	}
	
	void Update ()
	{		
		slider.value += Mathf.Pow(2, slider.value) / 100;		
	}

	public void decreaseValue(float dValue)
	{
		slider.value -= dValue;
	}

	public bool canFire(float minValue)
	{
		if (slider.value >= minValue)
			return true;
		else
			return false;
	}
}
