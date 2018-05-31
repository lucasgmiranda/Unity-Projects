using UnityEngine;
using UnityEngine.UI;

public class CameraNavigation : MonoBehaviour {

	private Transform myCamera;
	private Transform cameraRig02, cameraRig01;

	private Vector3 _LocalRotation;
	
	public float OrbitSensitivity = 4f;
	public float OrbitDampening = 5f;
	public float ZoomSensitivity = 10f;
	public float ZoomDampening = 10f;
	public float StartZoom = 10f;
	public float MinZoom = 5f;
	public float MaxZoom = 25f;

	void Start () 
	{
		myCamera = transform;
		cameraRig02 = transform.parent;
		cameraRig01 = cameraRig02.transform.parent;
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
		deltaMagnitudeDiff *= (this.StartZoom * 0.3f);

		this.StartZoom += deltaMagnitudeDiff * Time.deltaTime * 0.01f;

		this.StartZoom = Mathf.Clamp(this.StartZoom, MinZoom, MaxZoom);
	}

	void cameraRigTransform()
	{
		//Actual Camera Rig Transformations
		Quaternion QTcameraRig02 = Quaternion.Euler(_LocalRotation.y, 0, 0);
		Quaternion QTcameraRig01 = Quaternion.Euler(0, _LocalRotation.x, 0);
		cameraRig02.localRotation = Quaternion.Lerp(cameraRig02.localRotation, QTcameraRig02, Time.deltaTime * OrbitDampening);
		cameraRig01.localRotation = Quaternion.Lerp(cameraRig01.localRotation, QTcameraRig01, Time.deltaTime * OrbitDampening);

		if ( myCamera.localPosition.z != StartZoom * -1f )
		{
			myCamera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(myCamera.localPosition.z, this.StartZoom * -1f, Time.deltaTime * ZoomDampening));
		}
	}
}
