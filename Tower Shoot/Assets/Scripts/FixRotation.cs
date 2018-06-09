using UnityEngine;

public class FixRotation : MonoBehaviour {

			
	void LateUpdate()
	{
		transform.rotation = new Quaternion(0,0,0,0);
	}
}
