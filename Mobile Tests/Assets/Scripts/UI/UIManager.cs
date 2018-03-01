using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
	public Dictionary<string, UIButtonData> buttonsData;

	void Start()
	{
		GetButtonsDataDictionary();
	}

	void Update()
	{
		
	}
	
	public void GetButtonsDataDictionary()
	{
		buttonsData = new Dictionary<string, UIButtonData>();
		Transform[] allChildren = GameObject.Find("Panel").GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren)
		{
			if (child.GetComponent<UIButtonData>() != null)
			{
				buttonsData.Add(child.name, child.GetComponent<UIButtonData>());
			}
		}
	}

	public bool SomeButtonPressed()
	{
		foreach(KeyValuePair<string,UIButtonData> button in buttonsData)
		{
			if (button.Value._hold) return true;
		}
		return false;
	}

	bool canAnim;
	public void NavButtonsAnim()
	{
		canAnim = !canAnim;

		Animator anim = GameObject.Find("NavButtons").GetComponent<Animator>();

		if (canAnim)
			anim.SetTrigger("Selected");
		else
			anim.SetTrigger("Deselected");
	}
}
