using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
	[HideInInspector]
	public bool placeObj, navigation, move, rotate, zoom;

	public Sprite Nav_S, Nav_D, PlaceObj_S, PlaceObj_D;


	public void PlaceObjSelect()
	{
		placeObj = !placeObj;

		GameObject button = GameObject.Find("PlaceObj");

		if (placeObj)
		{
			button.GetComponent<Image>().sprite = PlaceObj_S;
		}
		else
		{
			button.GetComponent<Image>().sprite = PlaceObj_D;
		}
	}

	public void NavSelect()
	{
		navigation = !navigation;

		Animator navButtons = GameObject.Find("NavButtons").GetComponent<Animator>();
		GameObject button = GameObject.Find("Navigation");

		if (navigation)
		{
			button.GetComponent<Image>().sprite = Nav_S;
			navButtons.SetTrigger("Selected");
		}
		else
		{
			button.GetComponent<Image>().sprite = Nav_D;
			navButtons.SetTrigger("Deselected");
		}
	}

	public void MoveSelect()
	{
		move = !move;
	}

	public void RotateSelect()
	{
		rotate = !rotate;
	}

	public void ZoomSelect()
	{
		zoom = !zoom;
	}
}
