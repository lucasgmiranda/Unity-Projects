using UnityEngine;

public class CameraOrbit : MonoBehaviour {

	private Transform _XForm_Camera;
	private Transform _XForm_Parent;

	private Vector3 _LocalRotation;
	private float _CameraDistance = 10f;

	public float OrbitSensitivity = 4f;
	public float ScrollSensitivity = 2f;
	public float OrbitDampening = 10f;
	public float ScrollDampening = 6f;

	public bool CameraDisabled = false;

	void Start () 
	{
		this._XForm_Camera = this.transform;
		this._XForm_Parent = this.transform.parent;
	}

	void LateUpdate () 
	{
		if (Input.GetKey (KeyCode.LeftShift))
			CameraDisabled = !CameraDisabled;

		if (!CameraDisabled) 
		{
			if(Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0)
			{
				_LocalRotation.x += Input.GetAxis("Mouse X") * OrbitSensitivity;
				_LocalRotation.y -= Input.GetAxis("Mouse Y") * OrbitSensitivity;
				Debug.Log (Input.GetAxis ("Mouse X") * OrbitSensitivity);
				//Clamp the y Rotation to horizon and not flipping over at the top
				_LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
			}
			if (Input.GetAxis("Mouse ScrollWheel") != 0f)
			{
				float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

				//Makes camera zoom faster the further away it is from the target
				ScrollAmount *= (this._CameraDistance * 0.3f);

				this._CameraDistance += ScrollAmount * -1f;

				this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
			}
		}

		//Actual Camera Rig Transformations
		Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
		this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

		if ( this._XForm_Camera.localPosition.z != this._CameraDistance * -1f )
		{
			this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
		}
	}
}
