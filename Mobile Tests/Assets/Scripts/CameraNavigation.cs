using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraNavigation : MonoBehaviour {

	private Transform _XForm_Camera;
	private Transform _XForm_Parent;

	private Vector3 _LocalRotation;
	private float _CameraDistance = 10f;

	public float OrbitSensitivity = 8f;
	public float OrbitDampening = 5f;
	public float ZoomSensitivity = 10f;
	public float ZoomDampening = 10f;
	public float MaxZoomIn = 5f;
	public float MaxZoomOut = 25f;

	int drawFinger;
	bool canRotate, canZoom;

	void Start () 
	{
		this._XForm_Camera = this.transform;
		this._XForm_Parent = this.transform.parent;
	}

	void LateUpdate () 
	{
		if (Input.touchCount == 2)
		{
			cameraRotation();
		}
		cameraRigTransform();
	}

	public void SetDrawFinger()
	{
		if (Input.touchCount >= 2)
			drawFinger = 0;
		if (Input.touchCount == 1)
			drawFinger = 1;
		Debug.Log(drawFinger);
	}

	public void cameraRotation(bool can)
	{
		if (can)
		{
			_LocalRotation.x += Input.GetTouch(drawFinger).deltaPosition.x * OrbitSensitivity * Time.deltaTime;
			_LocalRotation.y -= Input.GetTouch(drawFinger).deltaPosition.y * OrbitSensitivity * Time.deltaTime;

			//Clamp the y Rotation to horizon and not flipping over at the top
			_LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 5f, 90f);
		}
	}

	void cameraZoomTypeA()
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

		this._CameraDistance = Mathf.Clamp(this._CameraDistance, MaxZoomIn, MaxZoomOut);
	}

	void cameraZoomTypeB()
	{
		float ZoomAmount = -Input.GetTouch (0).deltaPosition.y * ZoomSensitivity;

		//Makes camera zoom faster the further away it is from the target
		ZoomAmount *= (this._CameraDistance * 0.3f);

		this._CameraDistance += ZoomAmount * Time.deltaTime * 0.01f;

		this._CameraDistance = Mathf.Clamp(this._CameraDistance, MaxZoomIn, MaxZoomOut);
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
