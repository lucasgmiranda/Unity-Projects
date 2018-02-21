using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix
{
	public class Movements : MonoBehaviour 
	{
		Vector3 initPos;

		// Use this for initialization
		void Start () 
		{
			initPos = transform.position;
		}
		
		// Update is called once per frame
		void Update () 
		{
			transform.position = initPos + Vector3.one*Mathf.Sin (7f*Time.time%(2f*Mathf.PI));
		}
	}
}
