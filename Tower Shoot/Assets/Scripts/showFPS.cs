using UnityEngine;
using UnityEngine.UI;

public class showFPS : MonoBehaviour
{
	[HideInInspector]
	public float deltaTime, fps;

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fps = 1.0f / deltaTime;
		GetComponent<Text>().text = Mathf.Ceil(fps).ToString();		
	} 
}
