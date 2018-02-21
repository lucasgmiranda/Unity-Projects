using UnityEngine;

public class CellData : MonoBehaviour {

	public bool objPlaced;
	public Vector2 coord;

	public void Initialize(Vector2 vec, bool oP)
	{
		coord = vec;
		objPlaced = oP;
	}
}
