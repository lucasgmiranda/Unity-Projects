using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestruction : MonoBehaviour {

	public GameObject destroyedVersion;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Projectile")
		{
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
