using UnityEngine;
using UnityEngine.UI;

public class CameraNavigation : MonoBehaviour {

	private Transform _XForm_Camera;
	private Transform _XForm_Parent;

	private Vector3 _LocalRotation;
	private float _CameraDistance = 10f;

	public float OrbitSensitivity = 4f;
	public float OrbitDampening = 5f;
	public float ZoomSensitivity = 10f;
	public float ZoomDampening = 10f;
	public float MinZoom = 5f;
	public float MaxZoom = 25f;

	void Start () 
	{
		this._XForm_Camera = this.transform;
		this._XForm_Parent = this.transform.parent;
	}

	void LateUpdate () 
	{
		if(Input.touchCount == 1) 
			cameraRotation();
		
		if(Input.touchCount > 1) 
			cameraZoom();

		cameraRigTransform();
	}

	void cameraRotation()
	{
		_LocalRotation.x += Input.GetTouch(0).deltaPosition.x * OrbitSensitivity * Time.deltaTime;
		_LocalRotation.y -= Input.GetTouch(0).deltaPosition.y * OrbitSensitivity * Time.deltaTime;

		//Clamp the y Rotation to horizon and not flipping over at the top
		_LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
	}

	void cameraZoom()
	{
		// Store both touches.
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);

		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

		// Find the difference in the distances between each frame.
		float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * ZoomSensitivity;

		//Makes camera zoom faster the further away it is from the target
		deltaMagnitudeDiff *= (this._CameraDistance * 0.3f);

		this._CameraDistance += deltaMagnitudeDiff * Time.deltaTime * 0.01f;

		this._CameraDistance = Mathf.Clamp(this._CameraDistance, MinZoom, MaxZoom);
	}

	void cameraRigTransform()
	{
		//Actual Camera Rig Transformations
		Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
		this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

		if ( this._XForm_Camera.localPosition.z != this._CameraDistance * -1f )
		{
			this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ZoomDampening));
		}
	}
}
