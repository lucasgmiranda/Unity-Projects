using UnityEngine;

namespace Mezanix
{
	public class MezInput
	{
		//4
		public class MezAcceleration
		{
			public static Vector3 acceleration
			{
				get {return Input.acceleration;}
			}

			public static int accelerationEventCount
			{
				get {return Input.accelerationEventCount;}
			}

			public static AccelerationEvent [] accelerationEvents
			{
				get {return Input.accelerationEvents;}
			}

			public static AccelerationEvent GetAccelerationEvent (int index)
			{
				return Input.GetAccelerationEvent (index);
			}
		}

		//5	9
		public class MezKeys
		{
			public static bool anyKey
			{
				get {return Input.anyKey;}
			}

			public static bool anyKeyDown
			{
				get {return Input.anyKeyDown;}
			}


			public static bool GetKey (KeyCode key)
			{
				return Input.GetKey (key);
			}

			public static bool GetKeyDown (KeyCode key)
			{
				return Input.GetKeyDown (key);
			}

			public static bool GetKeyUp (KeyCode key)
			{
				return Input.GetKeyUp (key);
			}
		}

		//6 15
		public class MezMouse
		{
			public static bool GetMouseButton (int button)
			{
				return Input.GetMouseButton (button);
			}

			public static bool GetMouseButtonDown (int button)
			{
				return Input.GetMouseButtonDown (button);
			}

			public static bool GetMouseButtonUp (int button)
			{
				return Input.GetMouseButtonUp (button);
			}

			//cross 0
			public static Vector3 mousePosition
			{
				get
				{
#if CROSS_PLATFORM_INPUT
					return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.mousePosition;
#else
					return Input.mousePosition;
#endif

				}
			}

			public static bool mousePresent
			{
				get {return Input.mousePresent;}
			}

			public static Vector2 mouseScrollDelta
			{
				get {return Input.mouseScrollDelta;}
			}
		}

		//7 22
		public class MezTouch
		{
			public static Touch GetTouch (int index)
			{
				return Input.GetTouch (index);
			}

			public static bool multiTouchEnabled
			{
				get {return Input.multiTouchEnabled;}
			}

			public static bool stylusTouchSupported
			{
				get {return Input.stylusTouchSupported;}
			}

			public static int touchCount
			{
				get {return Input.touchCount;}
			}

			public static Touch [] touches
			{
				get {return Input.touches;}
			}

			public static bool touchPressureSupported
			{
				get {return Input.touchPressureSupported;}
			}

			public static bool touchSupported
			{
				get {return Input.touchSupported;}
			}
		}

		//3 25
		public class MezAxis
		{
			//cross 1
			public static float GetAxis (string axisName)
			{
#if CROSS_PLATFORM_INPUT
				return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis(axisName);
#else
				return Input.GetAxis (axisName);
#endif
			}

			//cross 2
			public static float GetAxisRaw (string axisName)
			{
#if CROSS_PLATFORM_INPUT
				return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxisRaw(axisName);
#else
				return Input.GetAxisRaw (axisName);
#endif
			}

			public static void ResetInputAxes ()
			{
				Input.ResetInputAxes ();
			}
		}

		//4 29
		public class MezButton
		{
			public static bool backButtonLeavesApp
			{
				get {return Input.backButtonLeavesApp;}
			}

			//cross 3
			public static bool GetButton (string buttonName)
			{
#if CROSS_PLATFORM_INPUT
				return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButton(buttonName);
#else
				return Input.GetButton (buttonName);
#endif
			}

			//cross 4
			public static bool GetButtonDown (string buttonName)
			{
#if CROSS_PLATFORM_INPUT
				return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown(buttonName);
#else
				return Input.GetButtonDown (buttonName);
#endif
			}

			//cross 5
			public static bool GetButtonUp (string buttonName)
			{
#if CROSS_PLATFORM_INPUT
				return UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonUp(buttonName);
#else
				return Input.GetButtonUp (buttonName);
#endif
			}
		}

		//2 31
		public class MezJoystick
		{
			public static string [] GetJoystickNames ()
			{
				return Input.GetJoystickNames ();
			}

			//public static bool IsJoystickPreconfigured (string joystickName)
			//{
			//	return Input.IsJoystickPreconfigured (joystickName);
			//}
		}

		//1 32
		public class MezCompass
		{
			public static Compass compass
			{
				get {return Input.compass;}
			}
		}

		//1 33
		public class MezDeviceOrientation
		{
			public static DeviceOrientation deviceOrientation
			{
				get {return Input.deviceOrientation;}
			}
		}

		//1 34
		public class MezGyro
		{
			public static Gyroscope gyro
			{
				get {return Input.gyro;}
			}
		}

		//1 35
		public class MezLocationService
		{
			public static LocationService location
			{
				get {return Input.location;}
			}
		}

		void ttt ()
		{
			//Input.compensateSensors;
			//
			//Input.compositionCursorPos;
			//
			//Input.compositionString;
			//
			//
			//Input.imeCompositionMode;
			//
			//Input.imeIsSelected;
			//
			//Input.inputString;


			//Input.simulateMouseWithTouches;

		}


		///
		public enum InputType
		{
			UnityInputManager,
			customKey,
			twoCustomKeys,
		}
		public enum InputMethod
		{
			GetAxis,
			GetButton,
			GetButtonDown,
			GetButtonUp,
		}
		public enum KeyMethod
		{
			GetKey,
			GetKeyDown,
			GetKeyUp,
		}
		public static bool InputKey(KeyMethod method, KeyCode key)
		{
			bool r = false;
			switch (method)
			{
				case KeyMethod.GetKey:
					r = MezKeys.GetKey(key);
					break;

				case KeyMethod.GetKeyDown:
					r = MezKeys.GetKeyDown(key);
					break;

				case KeyMethod.GetKeyUp:
					r = MezKeys.GetKeyUp(key);
					break;
			}
			return r;
		}
		public static float InputManagerFloat (InputMethod method, string axisName)
		{
			float r = 0f;
			switch(method)
			{
				case InputMethod.GetAxis:
					r = MezAxis.GetAxis(axisName);
					break;
			}
			return r;
		}
		public static bool InputManagerBool(InputMethod method, string buttonName)
		{
			bool r = false;
			switch (method)
			{
				case InputMethod.GetButton:
					r = MezButton.GetButton(buttonName);
					break;

				case InputMethod.GetButtonDown:
					r = MezButton.GetButtonDown(buttonName);
					break;

				case InputMethod.GetButtonUp:
					r = MezButton.GetButtonUp(buttonName);
					break;
			}
			return r;
		}
	}
}