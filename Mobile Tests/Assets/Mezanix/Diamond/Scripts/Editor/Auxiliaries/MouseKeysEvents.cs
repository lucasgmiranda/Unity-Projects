using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mezanix.Diamond
{
	public class MouseKeysEvents 
	{
		static Vector3 getDragRectFirstPosition;
		static bool getDragRectGetting = false;
		public static Rect GetDragRect (Event e, out bool getting)
		{
			Rect r = new Rect ();

			if (e.button == 0)
			{
				if (e.type == EventType.MouseDown)
				{
					getDragRectFirstPosition = e.mousePosition;

					getDragRectGetting = true;
				}

				r = RectOperations.TwoPositionsToRect (getDragRectFirstPosition, e.mousePosition);

				if (e.type == EventType.MouseUp)
				{
					getDragRectGetting = false;
				}
			}

			getting = getDragRectGetting;

			return r;
		}


		public static bool ControlCommandAltKey (KeyCode key, Event e)
		{
			bool r = false;

			//if (e.control || e.command)
			//{
				if (e.alt)
				{
					if (e.keyCode == key)
					{
						if (e.type == EventType.KeyUp)
						{
							r = true;
						}
					}
				}
			//}

			return r;
		}


		public static bool KeyIsHeld (KeyCode key, Event e)
		{
			bool r = false;

			if (e.keyCode == key)
			{
				if (e.type == EventType.KeyDown)
				{
					r = true;
				}
			}

			return r;
		}

		public static KeyCode LastKey_KeyIsDown;
		public static bool KeyIsDown (KeyCode key, Event e)
		{
			bool r = false;

			if (e.keyCode == key)
			{
				if (LastKey_KeyIsDown != key)
				{
					r = true;

					LastKey_KeyIsDown = key;
				}
			}

			if (LastKey_KeyIsDown == key)
			{
				if (e.type == EventType.KeyUp)
				{
					LastKey_KeyIsDown = KeyCode.None;
				}
			}

			return r;
		}

		public static bool KeyIsUp (KeyCode key, Event e)
		{
			bool r = false;

			if (e.keyCode == key)
			{
				if (e.type == EventType.KeyUp)
				{
					r = true;
				}
			}

			return r;
		}

		enum DoubleKeyOnceState
		{
			none,

			one,

			doublee,
		}
		static	DoubleKeyOnceState doubleKeyOnceState;
		static void DoubleKeyOnceStateGoTo (DoubleKeyOnceState st)
		{
			switch (doubleKeyOnceState)
			{
			case DoubleKeyOnceState.doublee:
				switch (st)
				{
				case DoubleKeyOnceState.doublee:
					break;

				case DoubleKeyOnceState.none:
					break;

				case DoubleKeyOnceState.one:
					doubleKeyOnceState = st;
					break;
				}
				break;



			case DoubleKeyOnceState.none:
				switch (st)
				{
				case DoubleKeyOnceState.doublee:
					break;

				case DoubleKeyOnceState.none:
					break;

				case DoubleKeyOnceState.one:
					doubleKeyOnceState = st;
					break;
				}
				break;



			case DoubleKeyOnceState.one:
				switch (st)
				{
				case DoubleKeyOnceState.doublee:
					doubleKeyOnceState = st;
					break;

				case DoubleKeyOnceState.none:
					doubleKeyOnceState = st;
					break;

				case DoubleKeyOnceState.one:
					break;
				}
				break;
			}
		}
		static KeyCode doubleKeyOnce_k_0;
		static KeyCode doubleKeyOnce_k_1;
		public static bool DoubleKeyHeld (KeyCode key_0, KeyCode key_1, Event e)
		{
			bool r = false;

			if (e.keyCode == key_0)
			{
				doubleKeyOnce_k_0 = key_0;

				DoubleKeyOnceStateGoTo (DoubleKeyOnceState.one);
			}

			if (e.keyCode == key_1)
			{
				doubleKeyOnce_k_1 = key_1;	

				DoubleKeyOnceStateGoTo (DoubleKeyOnceState.one);
			}

			if (doubleKeyOnce_k_0 == key_0)
			{
				if (doubleKeyOnce_k_1 == key_1)
				{
					DoubleKeyOnceStateGoTo (DoubleKeyOnceState.doublee);
				}
			}



			if (doubleKeyOnce_k_0 != key_0)
			{
				if (doubleKeyOnce_k_1 != key_1)
				{
					DoubleKeyOnceStateGoTo (DoubleKeyOnceState.none);
				}
			}




			if (e.keyCode == key_0)
			{
				if (e.type == EventType.KeyUp)
				{
					doubleKeyOnce_k_0 = KeyCode.None;

					DoubleKeyOnceStateGoTo (DoubleKeyOnceState.one);
				}
			}

			if (e.keyCode == key_1)
			{
				if (e.type == EventType.KeyUp)
				{
					doubleKeyOnce_k_1 = KeyCode.None;

					DoubleKeyOnceStateGoTo (DoubleKeyOnceState.one);
				}
			}



			if (doubleKeyOnceState == DoubleKeyOnceState.doublee)
			{
				r = true;
			}

			return r;
		}

		static bool doubleKeyWasHeld;
		public static bool DoubleKeyOnce (KeyCode key_0, KeyCode key_1, Event e)
		{
			bool r = false;

			if (DoubleKeyHeld (key_0, key_1, e))
			{
				if ( ! doubleKeyWasHeld)
				{
					r = true;

					doubleKeyWasHeld = true;
				}
			}

			if ( ! DoubleKeyHeld (key_0, key_1, e))
			{
				doubleKeyWasHeld = false;
			}

			return r;
		}
	}
}
