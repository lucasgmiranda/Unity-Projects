using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigButtonsUI : MonoBehaviour 
{
	public class ConfigButtons
	{
		[HideInInspector]
		public string name;
		[HideInInspector]
		public GameObject obj;
		[HideInInspector]
		public Animator anim;
		[HideInInspector]
		public UIButtonData buttonData;
		[HideInInspector]
		public Text text;

		public ConfigButtons(GameObject obj)
		{
			this.obj = obj;
		}
	}

	[HideInInspector]
	public List<ConfigButtons> configButtons = new List<ConfigButtons>();

	void Awake () 
	{
		foreach (Transform child in transform)
		{
			configButtons.Add(new ConfigButtons(child.gameObject));
		}

		foreach (var button in configButtons)
		{
			button.name = button.obj.name;
			button.anim = button.obj.GetComponent<Animator>();
			button.buttonData = button.obj.GetComponent<UIButtonData>();
			button.text = button.obj.GetComponentInChildren<Text>();
		}
	}
	
	void Update () 
	{
		foreach (var button in configButtons)
		{
			if(button.buttonData._click)
			{
				button.buttonData._click = false;

				if (button.buttonData._switch)
				{
					button.anim.SetTrigger("SliderEnable");
				}
				else
				{
					button.anim.SetTrigger("SliderDisable");
				}
			}
		}
	}

	public ConfigButtons GetButton(string name)
	{
		foreach (var button in configButtons)
		{
			if(button.name.Equals(name))
			{
				return button;
			}
		}

		Debug.LogError("Button not find");
		return null;
	}

	public void DisableAllButtons()
	{
		foreach (var button in configButtons)
		{
			button.buttonData.DisableSwitch();
		}
	}
}
