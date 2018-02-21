using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// LogicNode_HighLevel_Inputs
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{	
	public partial class LogicNode : ScriptableObject 
	{
		#region Axis_StringEnum

		void AxisInput_Head ()
		{
			AxisInput_Input ();
			if (logic.playing) AxisInput ();
			AxisInput_Output ();
		}
		void AxisInput_Input ()
		{
			DrawLogicNodeLabel ("Axis Name", 0, 2);
			DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
			CheckAxisNameSetup ();
		}
		void AxisInput ()
		{
			if ( ! axisNameIsSetup)
				return;
			
			floatValue = Input.GetAxis (stringValues [0]);
		}
		const string AxisInput_toCode = "\t\tvoid AxisInput ()\n\t\t{\n\t\t\tfloatValue = Input.GetAxis (stringValues [0]);\n\t\t}\n";
		void AxisInput_Output ()
		{
			DrawFloatResultField (true);
		}
		string [] AxisInput_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.floatValue + ExprWs.Gv.stringValues;

			r [1] = ExprWs.ConstructorExpr.StringValues (this);

			r [2] = "\t\t\tAxisInput ();\n";

			r [3] = AxisInput_toCode;
			r [4] = "";

			return r;
		}

		#endregion AxisStringEnum


		#region Keyboard_StringEnum
		public KeyCode KeyboardKeyCode;
		const string KeyboardKeyCode_toCode = "\t\tKeyCode KeyboardKeyCode;\n";
		string Constructor_KeyboardKeyCode_toCode (LogicNode ln)
		{			
			return "\t\t\tKeyboardKeyCode = KeyCode." + ln.KeyboardKeyCode.ToString () + ";\n";
		}

		public enum KeyboardButtonsState
		{
			down,

			up, 

			hold,
		}
		public KeyboardButtonsState keyboardButtonsState;
		const string keyboardButtonsState_toCode = "\t\tpublic enum KeyboardButtonsState\n\t\t{\n\t\t\tdown,\n\n\t\t\tup, \n\n\t\t\thold,\n\t\t}\n\t\tpublic KeyboardButtonsState keyboardButtonsState;\n";
		string Construcor_keyboardButtonsState_toCode (LogicNode ln)
		{
			return "\t\t\tkeyboardButtonsState = KeyboardButtonsState." + ln.keyboardButtonsState.ToString () + ";\n";
		}

		void KeyboardInput_Head ()
		{
			KeyboardInput_Input ();
			if (logic.playing) KeyboardInput ();
			KeyboardInput_Output ();
		}
		void KeyboardInput_Input ()
		{
			DrawLogicNodeLabel ("Key", 0, 2);
			KeyboardKeyCode = (KeyCode)DrawEnum (
				KeyboardKeyCode, FieldDrawType.label, 1, 2);

			DrawLogicNodeLabel ("State", 0, 2);
			keyboardButtonsState = (KeyboardButtonsState)DrawEnum (
				keyboardButtonsState, FieldDrawType.label, 1, 2);
		}
		void KeyboardInput ()
		{
			switch (keyboardButtonsState)
			{
			case KeyboardButtonsState.down:
				boolValue = Input.GetKeyDown (KeyboardKeyCode);
				break;

			case KeyboardButtonsState.hold:
				boolValue = Input.GetKey (KeyboardKeyCode);
				break;

			case KeyboardButtonsState.up:
				boolValue = Input.GetKeyUp (KeyboardKeyCode);
				break;
			}
		}
		const string KeyboardInput_toCode = "\t\tvoid KeyboardInput ()\n\t\t{\n\t\t\tswitch (keyboardButtonsState)\n\t\t\t{\n\t\t\tcase KeyboardButtonsState.down:\n\t\t\t\tboolValue = Input.GetKeyDown (KeyboardKeyCode);\n\t\t\t\tbreak;\n\n\t\t\tcase KeyboardButtonsState.hold:\n\t\t\t\tboolValue = Input.GetKey (KeyboardKeyCode);\n\t\t\t\tbreak;\n\n\t\t\tcase KeyboardButtonsState.up:\n\t\t\t\tboolValue = Input.GetKeyUp (KeyboardKeyCode);\n\t\t\t\tbreak;\n\t\t\t}\n\t\t}\n";
		void KeyboardInput_Output ()
		{
			DrawBoolResultField ();
		}
		string [] KeyboardInput_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.boolValue + KeyboardKeyCode_toCode + keyboardButtonsState_toCode;

			r [1] = Constructor_KeyboardKeyCode_toCode (this) + Construcor_keyboardButtonsState_toCode (this);

			r [2] = "\t\t\tKeyboardInput ();\n";

			r [3] = KeyboardInput_toCode;
			r [4] = "";

			return r;
		}

		#endregion KeyboardStringEnum


		#region Mouse_StringEnum

		public enum ThreeMouseButtons
		{
			left,

			right,

			middle,

			wheel,
		}
		public ThreeMouseButtons threeMouseButtons;
		const string threeMouseButtons_toCode = "\t\tpublic enum ThreeMouseButtons\n\t\t{\n\t\t\tleft,\n\n\t\t\tright,\n\n\t\t\tmiddle,\n\n\t\t\twheel,\n\t\t}\n\t\tpublic ThreeMouseButtons threeMouseButtons;\n";
		string Construcor_threeMouseButtons_toCode (LogicNode ln)
		{
			return "\t\t\tthreeMouseButtons = ThreeMouseButtons." + ln.threeMouseButtons.ToString () + ";\n";
		}

		public enum MouseButtonsState
		{
			down,

			up, 

			hold, 

			drag,
		}
		public MouseButtonsState mouseButtonsState;
		const string mouseButtonsState_toCode = "\t\tpublic enum MouseButtonsState\n\t\t{\n\t\t\tdown,\n\n\t\t\tup, \n\n\t\t\thold, \n\n\t\t\tdrag,\n\t\t}\n\t\tpublic MouseButtonsState mouseButtonsState;\n";
		string Construcor_mouseButtonsState_toCode (LogicNode ln)
		{
			return "\t\t\tmouseButtonsState = MouseButtonsState." + ln.mouseButtonsState.ToString () + ";\n";
		}


		Vector3 lastFrameMousePosition = new Vector3 (float.MinValue, 0f, 0f);
		const string lastFrameMousePosition_toCode = "\t\tVector3 lastFrameMousePosition = new Vector3 (float.MinValue, 0f, 0f);\n";

		bool MousePositionChanged ()
		{
			float delta = 1f;

			if (lastFrameMousePosition.x > float.MinValue)
			{
				if (Mathf.Abs (Input.mousePosition.x - lastFrameMousePosition.x) > delta)
				{
					return true;
				}

				if (Mathf.Abs (Input.mousePosition.y - lastFrameMousePosition.y) > delta)
				{
					return true;
				}

				return false;
			}

			return false;
		}
		const string MousePositionChanged_toCode = "\t\tbool MousePositionChanged ()\n\t\t{\n\t\t\tfloat delta = 1f;\n\n\t\t\tif (lastFrameMousePosition.x > float.MinValue)\n\t\t\t{\n\t\t\t\tif (Mathf.Abs (Input.mousePosition.x - lastFrameMousePosition.x) > delta)\n\t\t\t\t{\n\t\t\t\t\treturn true;\n\t\t\t\t}\n\n\t\t\t\tif (Mathf.Abs (Input.mousePosition.y - lastFrameMousePosition.y) > delta)\n\t\t\t\t{\n\t\t\t\t\treturn true;\n\t\t\t\t}\n\n\t\t\t\treturn false;\n\t\t\t}\n\n\t\t\treturn false;\n\t\t}\n";

		void MouseInput_Head ()
		{
			MouseInput_Input ();
			if (logic.playing) MouseInput ();
			MouseInput_Output ();
		}
		void MouseInput_Input ()
		{
			DrawLogicNodeLabel ("Button", 0, 2);
			threeMouseButtons = (ThreeMouseButtons)DrawEnum (
				threeMouseButtons, FieldDrawType.label, 1, 2);
			
			if ( threeMouseButtons != ThreeMouseButtons.wheel)
			{
				DrawLogicNodeLabel ("Button State", 0, 2);
				mouseButtonsState = (MouseButtonsState)DrawEnum (
					mouseButtonsState, FieldDrawType.label, 1, 2);
			}
		}
		void MouseInput ()
		{
			int mouseButtonId = 0;

			switch (threeMouseButtons)
			{
			case ThreeMouseButtons.left:
				mouseButtonId = 0;
				break;

			case ThreeMouseButtons.middle:
				mouseButtonId = 2;
				break;

			case ThreeMouseButtons.right:
				mouseButtonId = 1;
				break;

			case ThreeMouseButtons.wheel:
				mouseButtonId = 2;
				break;
			}

			if (threeMouseButtons == ThreeMouseButtons.wheel)
			{
				floatValue = Input.mouseScrollDelta.y;

				if (floatValue == 0f)
					boolValue = false;
				else
					boolValue = true;
			}
			else
			{
				switch (mouseButtonsState)
				{
				case MouseButtonsState.down:
					boolValue = Input.GetMouseButtonDown (mouseButtonId);
					break;

				case MouseButtonsState.drag:
					boolValue = Input.GetMouseButton (mouseButtonId) && MousePositionChanged ();
					break;

				case MouseButtonsState.hold:
					boolValue = Input.GetMouseButton (mouseButtonId);
					break;

				case MouseButtonsState.up:
					boolValue = Input.GetMouseButtonUp (mouseButtonId);
					break;
				}
			}

			lastFrameMousePosition = Input.mousePosition;
		}
		const string MouseInput_toCode = "\t\tvoid MouseInput ()\n\t\t{\n\t\t\tint mouseButtonId = 0;\n\n\t\t\tswitch (threeMouseButtons)\n\t\t\t{\n\t\t\tcase ThreeMouseButtons.left:\n\t\t\t\tmouseButtonId = 0;\n\t\t\t\tbreak;\n\n\t\t\tcase ThreeMouseButtons.middle:\n\t\t\t\tmouseButtonId = 2;\n\t\t\t\tbreak;\n\n\t\t\tcase ThreeMouseButtons.right:\n\t\t\t\tmouseButtonId = 1;\n\t\t\t\tbreak;\n\n\t\t\tcase ThreeMouseButtons.wheel:\n\t\t\t\tmouseButtonId = 2;\n\t\t\t\tbreak;\n\t\t\t}\n\n\t\t\tif (threeMouseButtons == ThreeMouseButtons.wheel)\n\t\t\t{\n\t\t\t\tfloatValue = Input.mouseScrollDelta.y;\n\n\t\t\t\tif (floatValue == 0f)\n\t\t\t\t\tboolValue = false;\n\t\t\t\telse\n\t\t\t\t\tboolValue = true;\n\t\t\t}\n\t\t\telse\n\t\t\t{\n\t\t\t\tswitch (mouseButtonsState)\n\t\t\t\t{\n\t\t\t\tcase MouseButtonsState.down:\n\t\t\t\t\tboolValue = Input.GetMouseButtonDown (mouseButtonId);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseButtonsState.drag:\n\t\t\t\t\tboolValue = Input.GetMouseButton (mouseButtonId) && MousePositionChanged ();\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseButtonsState.hold:\n\t\t\t\t\tboolValue = Input.GetMouseButton (mouseButtonId);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseButtonsState.up:\n\t\t\t\t\tboolValue = Input.GetMouseButtonUp (mouseButtonId);\n\t\t\t\t\tbreak;\n\t\t\t\t}\n\t\t\t}\n\n\t\t\tlastFrameMousePosition = Input.mousePosition;\n\t\t}\n";
		void MouseInput_Output ()
		{
			if (threeMouseButtons == ThreeMouseButtons.wheel)
			{
				DrawLogicNodeLabel ("Scroll value", 0, 2);
				DrawFloatResultField (true, 1, 2);

				DrawLogicNodeLabel ("Is Scrolling?", 0, 2);
				DrawBoolResultField (1, 2);
			}
			else
			{
				DrawBoolResultField ();
			}
		}
		string [] MouseInput_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.boolValue + ExprWs.Gv.floatValue +
				threeMouseButtons_toCode + mouseButtonsState_toCode +
				lastFrameMousePosition_toCode;

			r [1] = Construcor_threeMouseButtons_toCode (this) + Construcor_mouseButtonsState_toCode (this);

			r [2] = "\t\t\tMouseInput ();\n";

			r [3] = MouseInput_toCode + MousePositionChanged_toCode;
			r [4] = "";

			return r;
		}

		#endregion MouseStringEnum



		#region Inputs_StringEnum
		void AdaptOnInputs ()
		{
			switch (Inputs)
			{
			case "Mouse":
				MouseInput_Head ();
				break;

			case "Keyboard":
				KeyboardInput_Head ();
				break;

			case "Axis":
				AxisInput_Head ();
				break;
			}
		}

		string [] Inputss = new string[1];
		public string Inputs = "Mouse";


		void Init_Inputss ()
		{
			Inputss = new string[]
			{
				"Mouse",

				"Keyboard",

				"Axis",
			};
		}
		void SetThisTo_Inputs (object s)
		{
			Inputs = s.ToString ();
		}
		void Draw_Inputs_ListMenu ()
		{
			Init_Inputss ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, Inputs))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < Inputss.Length; i++)
				{
					if (string.IsNullOrEmpty (Inputss [i]))
						continue;

					menu.AddItem (new GUIContent (Inputss [i]), false, 
						SetThisTo_Inputs, Inputss [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					Inputs);

			AdaptOnInputs ();
		}
		#endregion InputsStringEnum

	}
}
