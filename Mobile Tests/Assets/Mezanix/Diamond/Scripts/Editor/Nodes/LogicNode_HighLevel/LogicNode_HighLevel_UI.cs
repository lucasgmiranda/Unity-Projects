using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// LogicNode_HighLevel_UI
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{	
	public partial class LogicNode : ScriptableObject 
	{
		#region Button_StringEnum
		Button buttonComp = null;
		const string buttonComp_toCode = "\t\tButton buttonComp = null;\n";
		bool GetbuttonComp ()
		{
			if (buttonComp == null)
			{
				if (gameObjectValues [0] == null)
					return false;

				buttonComp = gameObjectValues [0].GetComponent <Button> ();

				if (buttonComp == null)
					return false;

				return true;
			}

			return true;
		}
		const string getbuttonComp_toCode = "\t\tbool GetbuttonComp ()\n\t\t{\n\t\t\tif (buttonComp == null)\n\t\t\t{\n\t\t\t\tif (gameObjectValues [0] == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\tbuttonComp = gameObjectValues [0].GetComponent <Button> ();\n\n\t\t\t\tif (buttonComp == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn true;\n\t\t}\n";


		void OnClick_Head ()
		{
			OnClick_Input ();
			if (logic.playing) OnClick ();
			OnClick_Output ();
		}
		void OnClick_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			buttonComp = gameObjectValues [0].GetComponent <Button> ();
			if (buttonComp == null)
			{
				DrawInNodeInfo ("Add UI Button component to Gameobject");
				return;
			}

			DrawLogicNodeLabel ("Pulses number", 0, 2);
			DrawFloatInputField (0, 1, 2);
			floatValues [0] = Mathf.Max (0f, floatValues [0]);
		}
		void OnClick ()
		{
			if ( ! GetbuttonComp ())
				return;

			buttonComp.onClick.AddListener (TaskOnClick);

			if (boolValue)
			{
				downTimeCounterInt--;

				if (downTimeCounterInt < 0)
				{
					downTimeCounterInt = Mathf.CeilToInt (floatValues [0]);

					boolValue = false;
				}
			}
		}
		const string OnClick_toCode = "\t\tvoid OnClick ()\n\t\t{\n\t\t\tif ( ! GetbuttonComp ())\n\t\t\t\treturn;\n\n\t\t\tbuttonComp.onClick.AddListener (TaskOnClick);\n\n\t\t\tif (boolValue)\n\t\t\t{\n\t\t\t\tdownTimeCounterInt--;\n\n\t\t\t\tif (downTimeCounterInt < 0)\n\t\t\t\t{\n\t\t\t\t\tdownTimeCounterInt = Mathf.CeilToInt (floatValues [0]);\n\n\t\t\t\t\tboolValue = false;\n\t\t\t\t}\n\t\t\t}\n\t\t}\n";
		void OnClick_Output ()
		{
			
			DrawBoolResultField ();

			string [] documentationMessage = 
				new string[]
			{				
				"  \tCLICK ON BUTTON",
				"  \t-----------------------------------------------",
				"",
				"  Gameobject = the gameobject holding the UI Button",
				"  component.",
				"  You can create such object by cicking right click ",
				"  in the hierarchy: UI -> Button",
				"",
				"  Pulses number = after clicking once on the button,",
				"  \thow many true pulses you want ",
				"  \tto set in the output ",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html",
				"");
		}
		string [] OnClick_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				buttonComp_toCode +
				ExprWs.Gv.boolValue + ExprWs.Gv.floatValues + ExprWs.Gv.downTimeCounter;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.FloattValues (this) + ExprWs.ConstructorExpr.downTimeCounter;

			r [2] = "\t\t\tOnClick ();\n";

			r [3] = OnClick_toCode + ExprWs.UMDecl.taskOnClick + getbuttonComp_toCode;
			r [4] = "";

			return r;
		}


		/// <summary>
		/// Adapts the on text.
		/// </summary>
		void AdaptOnButton ()
		{
			switch (Button)
			{
			case "On Click":
				OnClick_Head ();
				break;
			}
		}

		string [] Buttons = new string[1];
		public string Button = "On Click";


		void Init_Buttons ()
		{
			Buttons = new string[]
			{
				"On Click",
			};
		}
		void SetThisTo_Button (object s)
		{
			Button = s.ToString ();
		}
		void Draw_Button_ListMenu ()
		{
			Init_Buttons ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, Button))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < Buttons.Length; i++)
				{
					if (string.IsNullOrEmpty (Buttons [i]))
						continue;

					menu.AddItem (new GUIContent (Buttons [i]), false, 
						SetThisTo_Button, Buttons [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					Button);

			AdaptOnButton ();
		}
		#endregion ButtonStringEnum


		#region Text_StringEnum
		Text textComp = null;
		const string textComp_toCode = "\t\tText textComp = null;\n";
		bool GettextComp ()
		{
			if (textComp == null)
			{
				if (gameObjectValues [0] == null)
					return false;

				textComp = gameObjectValues [0].GetComponent <Text> ();

				if (textComp == null)
					return false;

				return true;
			}

			return true;
		}
		const string gettextComp_toCode = "\t\tbool GettextComp ()\n\t\t{\n\t\t\tif (textComp == null)\n\t\t\t{\n\t\t\t\tif (gameObjectValues [0] == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\ttextComp = gameObjectValues [0].GetComponent <Text> ();\n\n\t\t\t\tif (textComp == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn true;\n\t\t}\n";


		void GetText_Head ()
		{
			GetText_Input ();
			if (logic.playing) GetText ();
			GetText_Output ();
		}
		void GetText_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			textComp = gameObjectValues [0].GetComponent <Text> ();
			if (textComp == null)
			{
				DrawInNodeInfo ("Add UI Text component to Gameobject");
				return;
			}

			DrawDoItButton ();
		}
		void GetText ()
		{
			if ( ! doIT)
				return;

			if ( ! GettextComp ())
				return;

			stringValue = textComp.text;
		}
		const string GetText_toCode = "\t\tvoid GetText ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t\treturn;\n\n\t\t\tif ( ! GettextComp ())\n\t\t\t\treturn;\n\n\t\t\tstringValue = textComp.text;\n\t\t}\n";
		void GetText_Output ()
		{
			DrawStringResultField (true);

			string [] documentationMessage = 
				new string[]
			{				
				"  GET TEXT FROM UI TEXT GAMEOBJECT",
				"",
				"  Gameobject = the gameobject holding the UI Text",
				"  component.",
				"  You can create such object by cicking right click ",
				"  in the hierarchy: UI -> Text",
				"",
				"  Output : the text shown in the screen by",
				"  the UI text gameobject.",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"https://docs.unity3d.com/ScriptReference/UI.Text-text.html",
				"");
		}
		string [] GetText_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				textComp_toCode + ExprWs.Gv.stringValue;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,});

			r [2] = "\t\t\tGetText ();\n";

			r [3] = GetText_toCode + gettextComp_toCode;
			r [4] = "";

			return r;
		}



		void ShowText_Head ()
		{
			ShowText_Input ();
			if (logic.playing) ShowText ();
			ShowText_Output ();
		}
		void ShowText_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			textComp = gameObjectValues [0].GetComponent <Text> ();
			if (textComp == null)
			{
				DrawInNodeInfo ("Add UI Text component to Gameobject");
				return;
			}


			DrawLogicNodeLabel ("Text", 0, 2);
			DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);

			DrawDoItButton ();
		}
		void ShowText ()
		{
			if ( ! doIT)
				return;

			if ( ! GettextComp ())
				return;

			textComp.text = stringValues [0];

			gameObjectValue = gameObjectValues [0];
		}
		const string ShowText_toCode = "\t\tvoid ShowText ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t\treturn;\n\n\t\t\tif ( ! GettextComp ())\n\t\t\t\treturn;\n\n\t\t\ttextComp.text = stringValues [0];\n\n\t\t\tgameObjectValue = gameObjectValues [0];\n\t\t}\n";
		void ShowText_Output ()
		{			
			DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

			string [] documentationMessage = 
				new string[]
			{				
				"  \tSHOW TEXT IN THE SCREEN",
				"  \t-----------------------------------------------",
				"",
				"  Gameobject = the gameobject holding the UI Text",
				"  component.",
				"  You can create such object by cicking right click ",
				"  in the hierarchy: UI -> Text",
				"",
				"  Text = the text to show to the screen.",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"https://docs.unity3d.com/ScriptReference/UI.Text-text.html",
				"");
		}
		string [] ShowText_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				textComp_toCode + ExprWs.Gv.stringValues;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.StringValues (this);

			r [2] = "\t\t\tShowText ();\n";

			r [3] = ShowText_toCode + gettextComp_toCode;
			r [4] = "";

			return r;
		}


		/// <summary>
		/// Adapts the on text.
		/// </summary>
		void AdaptOnText ()
		{
			switch (Text)
			{
			case "Show":
				ShowText_Head ();
				break;

			case "Get":
				GetText_Head ();
				break;
			}
		}

		string [] Texts = new string[1];
		public string Text = "Show";


		void Init_Texts ()
		{
			Texts = new string[]
			{
				"Show",

				"Get",
			};
		}
		void SetThisTo_Text (object s)
		{
			Text = s.ToString ();
		}
		void Draw_Text_ListMenu ()
		{
			Init_Texts ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, Text))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < Texts.Length; i++)
				{
					if (string.IsNullOrEmpty (Texts [i]))
						continue;

					menu.AddItem (new GUIContent (Texts [i]), false, 
						SetThisTo_Text, Texts [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					Text);

			AdaptOnText ();
		}
		#endregion TextStringEnum


		#region UI_StringEnum
		void AdaptOnUI ()
		{
			switch (UI)
			{
			case "Text":
				Draw_Text_ListMenu ();
				break;

			case "Button":
				Draw_Button_ListMenu ();
				break;
			}
		}

		string [] UIs = new string[1];
		public string UI = "Text";


		void Init_UIs ()
		{
			UIs = new string[]
			{
				"Text",

				"Button",
			};
		}
		void SetThisTo_UI (object s)
		{
			UI = s.ToString ();
		}
		void Draw_UI_ListMenu ()
		{
			Init_UIs ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, UI))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < UIs.Length; i++)
				{
					if (string.IsNullOrEmpty (UIs [i]))
						continue;

					menu.AddItem (new GUIContent (UIs [i]), false, 
						SetThisTo_UI, UIs [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					UI);

			AdaptOnUI ();
		}
		#endregion UIStringEnum

	}
}
