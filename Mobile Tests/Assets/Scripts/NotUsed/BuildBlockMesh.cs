using System.Collections;
using System;
using UnityEngine;

public class BuildBlockMesh : MonoBehaviour {

	public GameObject newBlock;

	void Combine(GameObject block)
	{
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Destroy (this.gameObject.GetComponent<MeshCollider>());

		for (int i = 0; i < meshFilters.Length; i++) {
			combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
		}
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true);
        transform.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        transform.GetComponent<MeshFilter>().mesh.RecalculateBounds();

        this.gameObject.AddComponent<MeshCollider>();
        transform.gameObject.SetActive(true);

        Destroy(block);
	}

	void Update () 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast (ray, out hitInfo, 1000.0f)) 
			{
				Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.green, 100f);

				Vector3 blockpos = hitInfo.point + hitInfo.normal / 2.0f;

				blockpos.x = (float)Math.Round(blockpos.x,MidpointRounding.AwayFromZero);
				blockpos.y = (float)Math.Round(blockpos.y,MidpointRounding.AwayFromZero);
				blockpos.z = (float)Math.Round(blockpos.z,MidpointRounding.AwayFromZero);

				//GameObject block = (GameObject)Instantiate (newBlock, blockpos, Quaternion.identity);
				//block.transform.parent = this.transform;
				//Combine (block);
			}
		}
	}
}
