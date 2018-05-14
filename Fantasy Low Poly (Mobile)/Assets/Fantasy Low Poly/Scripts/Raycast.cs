using System.Collections;
using UnityEngine;

public class Raycast : MonoBehaviour {

	public float force = 10f;
	Ray ray;
	RaycastHit hit;

	void Update () {

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {

			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			Debug.DrawRay (ray.origin, ray.direction * 20f, Color.red);

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				hit.rigidbody.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
			}
		}
	}
}