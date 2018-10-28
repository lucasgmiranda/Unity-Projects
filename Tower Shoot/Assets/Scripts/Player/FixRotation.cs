using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		transform.LookAt(new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z));
	}
}
