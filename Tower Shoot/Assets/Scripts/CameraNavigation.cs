using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraNavigation : MonoBehaviour {

	private Transform myCamera;
	private Transform cameraRig02, cameraRig01;
	private Vector3 _LocalRotation;
	private Compass CP;
	
	[Header("Orbit")]
	public float OrbitSensitivity = 4f;
	public float OrbitDampening = 5f;
	public float StartAngle = 10f;
	public float MinAngle = 0f;
	public float MaxAngle = 90f;
	[Header("Zoom")]
	public float ZoomSensitivity = 10f;
	public float ZoomDampening = 10f;
	public float StartZoom = 10f;
	public float MinZoom = 5f;
	public float MaxZoom = 25f;

	int rotationFinger;

	void Start () 
	{
		CP = GameObject.Find("Compass").GetComponent<Compass>();
		myCamera = transform;
		cameraRig02 = transform.parent;
		cameraRig01 = cameraRig02.transform.parent;
		_LocalRotation.y = StartAngle;
	}

	void FixedUpdate()
	{		
		if(!CP.compassHit)
		{
			if (Input.touchCount == 2)
			{
				SetRotationFinger();
				if(!EventSystem.current.IsPointerOverGameObject(rotationFinger))
					cameraRotation();
			}

			if (Input.touchCount == 1)
			{
				rotationFinger = 0;
				if (!EventSystem.current.IsPointerOverGameObject(rotationFinger))
					cameraRotation();
			}

			cameraRigTransform();
		}
	}

	void cameraRotation()
	{
		_LocalRotation.x += Input.GetTouch(rotationFinger).deltaPosition.x * OrbitSensitivity * Time.deltaTime; 
		_LocalRotation.y -= Input.GetTouch(rotationFinger).deltaPosition.y * OrbitSensitivity * Time.deltaTime;
		//Clamp the y Rotation to horizon and not flipping over at the top
		_LocalRotation.y = Mathf.Clamp(_LocalRotation.y, MinAngle, MaxAngle);
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
	public void SetRotationFinger()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).position.x > Screen.width / 2)
				rotationFinger = i;
		}
	}
}
