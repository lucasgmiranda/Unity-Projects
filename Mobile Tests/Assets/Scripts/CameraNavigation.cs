using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraNavigation : MonoBehaviour {

	private Transform _XForm_Camera;
	private Transform _XForm_Parent;

	private Vector3 _LocalRotation;

	public float InitialZoom = 30f;
	public float InitialXAngle = 12f;
	public float InitialYAngle = 0f;
	public float OrbitSensitivity = 8f;
	public float OrbitDampening = 5f;
	public float ZoomSensitivity = 10f;
	public float ZoomDampening = 10f;
	public float MaxZoomIn = 20f;
	public float MaxZoomOut = 50f;

	int drawFinger;

	void Start () 
	{
		this._XForm_Camera = this.transform;
		this._XForm_Parent = this.transform.parent;
		_LocalRotation.x = InitialXAngle;
		_LocalRotation.y = InitialYAngle;
	}

	void LateUpdate () 
	{
		if (Input.touchCount == 2)
		{
			if (UIManager.Inst.buttonsData["Rotate"]._hold)
			{
				cameraRotation();
			}
			if (UIManager.Inst.buttonsData["Zoom"]._hold)
			{
				cameraZoomTypeB();
			}
		}
        cameraRigTransform();
	}

	public void cameraRotation()
	{
		
		_LocalRotation.x -= Input.GetTouch(drawFinger).deltaPosition.y * OrbitSensitivity * Time.deltaTime;
		_LocalRotation.y += Input.GetTouch(drawFinger).deltaPosition.x * OrbitSensitivity * Time.deltaTime;

		//Clamp the y Rotation to horizon and not flipping over at the top
		_LocalRotation.x = Mathf.Clamp(_LocalRotation.x, 5f, 90f);
		
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
		deltaMagnitudeDiff *= (this.InitialZoom * 0.3f);

		this.InitialZoom += deltaMagnitudeDiff * Time.deltaTime * 0.01f;

		this.InitialZoom = Mathf.Clamp(this.InitialZoom, MaxZoomIn, MaxZoomOut);
	}

	void cameraZoomTypeB()
	{
		float ZoomAmount = -Input.GetTouch (drawFinger).deltaPosition.y * ZoomSensitivity;

		//Makes camera zoom faster the further away it is from the target
		ZoomAmount *= (this.InitialZoom * 0.3f);

		this.InitialZoom += ZoomAmount * Time.deltaTime * 0.01f;

		this.InitialZoom = Mathf.Clamp(this.InitialZoom, MaxZoomIn, MaxZoomOut);
	}

	void cameraRigTransform()
	{
		//Actual Camera Rig Transformations
		Quaternion QT = Quaternion.Euler(_LocalRotation.x, _LocalRotation.y, 0);
		this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

		if ( this._XForm_Camera.localPosition.z != this.InitialZoom * -1f )
		{
			this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this.InitialZoom * -1f, Time.deltaTime * ZoomDampening));
		}
	}

	public void SetDrawFinger()
	{
		if (Input.touchCount == 1)
			drawFinger = 1;
		else if (Input.touchCount == 2)
			drawFinger = 0;
	}
}
