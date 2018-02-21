using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// LogicNode_003_MonoBehVarAccess
	/// </summary>

	///
	/// 
	public partial class LogicNode : ScriptableObject 
	{
		void MonoVarAccess_Head ()
		{
			MonoVarAccess_Input ();
			if (logic.playing) MonoVarAccess ();
			MonoVarAccess_Output ();
		}

		int selectedMonoFieldIndex = -1;
		int selectedMonoIndex = -1;

		public string selectedMonoFieldName = "";
		public string selectedMonoName = "";

		MonoBehaviour selectedMono;
		System.Reflection.FieldInfo selectedMonoField;


		void SetSelectedMono (MonoBehaviour setMono, int index)
		{
			selectedMono = setMono;

			selectedMonoIndex = index;
		}
		public MonoBehaviour [] FindMonoByName ()
		{
			if (string.IsNullOrEmpty (selectedMonoName))
			{
				SetSelectedMono (null, -1);
				return null;
			}

			MonoBehaviour [] monos = Monos ();
			if (monos == null)
			{
				SetSelectedMono (null, -1);
				return null;
			}

			for (int i = 0; i < monos.Length; i++)
			{
				if (monos [i].name == selectedMonoName)
				{
					SetSelectedMono (monos [i], i);
					return monos;
				}
			}

			return monos;
		}
		MonoBehaviour [] FindMonoByName (bool editor)
		{
			if ( ! editor)
			if (string.IsNullOrEmpty (selectedMonoName))
			{
				SetSelectedMono (null, -1);
				return null;
			}

			MonoBehaviour [] monos = Monos ();
			if (monos == null)
			{
				SetSelectedMono (null, -1);
				return null;
			}

			for (int i = 0; i < monos.Length; i++)
			{
				if (monos [i].name == selectedMonoName)
				{
					SetSelectedMono (monos [i], i);
					return monos;
				}
			}

			return monos;
		}


		void SetSelectedMonoField (System.Reflection.FieldInfo setMonoField, int index)
		{
			selectedMonoField = setMonoField;

			selectedMonoFieldIndex = index;
		}
		public System.Reflection.FieldInfo [] FindMonoFieldByName ()
		{
			if (string.IsNullOrEmpty (selectedMonoFieldName))
			{
				SetSelectedMonoField (null, -1);
				return null;
			}

			System.Reflection.FieldInfo [] monoFields = MonoFields ();
			if (monoFields == null)
			{
				SetSelectedMonoField (null, -1);
				return null;
			}

			for (int i = 0; i < monoFields.Length; i++)
			{
				if (monoFields [i].Name == selectedMonoFieldName)
				{
					SetSelectedMonoField (monoFields [i], i);
					return monoFields;
				}
			}

			return monoFields;
		}
		public System.Reflection.FieldInfo [] FindMonoFieldByName (bool editor)
		{
			if ( ! editor)
			if (string.IsNullOrEmpty (selectedMonoFieldName))
			{
				SetSelectedMonoField (null, -1);
				return null;
			}

			System.Reflection.FieldInfo [] monoFields = MonoFields ();
			if (monoFields == null)
			{
				SetSelectedMonoField (null, -1);
				return null;
			}

			for (int i = 0; i < monoFields.Length; i++)
			{
				if (monoFields [i].Name == selectedMonoFieldName)
				{
					SetSelectedMonoField (monoFields [i], i);
					return monoFields;
				}
			}

			return monoFields;
		}


		void MonoVarAccess_Input ()
		{
			MonoBehaviour [] monos = FindMonoByName (true);
			if (monos == null)
				return;

			string [] monosNames = MonosNames (monos);
			if (monosNames == null)
				return;
			
			MonosNamesEnum (monosNames);
			if (selectedMono == null)
				return;


			System.Reflection.FieldInfo [] monoFields = FindMonoFieldByName (true);
			if (monoFields == null)
				return;

			string [] monoFieldsNames = MonoFieldsNames (monoFields);
			if (monoFieldsNames == null)
				return;

			MonoFieldsNamesEnum (monoFieldsNames);
			if (selectedMonoField == null)
				return;
			

			DrawLogicNodeLabel ("Get/Set", 0, 2);
			getOrSet = (GetOrSet)DrawEnum (getOrSet, FieldDrawType.label, 1, 2);

			MonoVarAccess_AdaptOnGetOrSet_Input ();

			DrawDoItButton ();
		}
		void MonoVarAccess ()
		{
			if ( ! doIT)
				return;

			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					rectValue = (Rect)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, rectValues [0]);
					break;
				}
			}
			else if (typeof (Vector4) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					vector4Value = (Vector4)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, vector4Values [0]);
					break;
				}
			}
			else if (typeof (Matrix4x4) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					SetM44Value ((Matrix4x4)(selectedMonoField.GetValue (selectedMono)));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, m44Value_Input_entier [0]);
					break;
				}
			}
			else if (typeof (Shader) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					shaderValue = (Shader)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, shaderValues [0]);
					break;
				}
			}
			else if (typeof (Texture2D) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					texture2DValue = (Texture2D)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, texture2DValues [0]);
					break;
				}
			}
			else if (typeof (Material) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					materialValue = (Material)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, materialValues [0]);
					break;
				}
			}
			else if (typeof (bool) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					boolValue = (bool)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, boolValues [0]);
					break;
				}
			}
			else if (typeof (Color) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					colorValue = (Color)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, colorValues [0]);
					break;
				}
			}
			else if (typeof (float) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					floatValue = (float)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, floatValues [0]);
					break;
				}
			}
			else if (typeof (GameObject) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					gameObjectValue = (GameObject)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, gameObjectValues [0]);
					break;
				}
			}
			else if (typeof (int) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					intValue = (int)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, intValues [0]);
					break;
				}
			}
			else if (typeof (string) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					stringValue = (string)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, stringValues [0]);
					break;
				}
			}
			else if (typeof (Vector2) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					vector2Value = (Vector2)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, vector2Values [0]);
					break;
				}
			}
			else if (typeof (Vector3) == type)
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					vector3Value = (Vector3)(selectedMonoField.GetValue (selectedMono));
					break;

				case GetOrSet.set:
					selectedMonoField.SetValue (selectedMono, vector3Values [0]);
					break;
				}
			}
			else
			{
			}

		}
		void MonoVarAccess_Output ()
		{
			if (selectedMonoField != null)
			{
				Type type = selectedMonoField.FieldType;

				if (typeof (Rect) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawRectResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Vector4) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawVector4ResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Matrix4x4) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawM44ResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Shader) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawShaderResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Texture2D) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawTexture2DResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Material) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawMaterialResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (bool) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawBoolResultField ();
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Color) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawColorResultField ();
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (float) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawFloatResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (GameObject) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawGameObjectResultField (ObjectResultDrawChoice.itsName);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (int) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawIntResultField (true);
						break;

					case GetOrSet.set:
						
						break;
					}
				}
				else if (typeof (string) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawStringResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Vector2) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawVector2ResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else if (typeof (Vector3) == type)
				{
					switch (getOrSet)
					{
					case GetOrSet.get:
						DrawVector3ResultField (true);
						break;

					case GetOrSet.set:
						break;
					}
				}
				else
				{
				}
			}

			string [] documentationMessage;

			documentationMessage = 
				new string[]
			{
				"",
				"",
				"    Interact with public variables of scripts attached ",
				"    to game objects in your scene (any C# script)",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"", 
				"http://mezanix.com/portfolio/diamond-1-1-7-documentation/");
		}

		string MonoVarAccess_Script (CsLogicNodeWhere csLogicNodeWhere)
		{
			string r = "";

			switch (csLogicNodeWhere)
			{
			case CsLogicNodeWhere.constructor:
				r = MonoVarAccess_Script_c ();
				break;

			case CsLogicNodeWhere.globalVariables:
				r = MonoVarAccess_Script_gv ();
				break;

			case CsLogicNodeWhere.logicNodeUpdate:
				r = MonoVarAccess_Script_lu ();
				break;

			case CsLogicNodeWhere.methods:
				r = MonoVarAccess_Script_m ();
				break;
			}

			return r;
		}
		string MonoVarAccess_Script (string forGet, string forSet)
		{
			string  r = "";

			switch (getOrSet)
			{
			case GetOrSet.get:
				r = forGet;
				break;

			case GetOrSet.set:
				r = forSet;
				break;
			}

			return r;
		}
		string MonoVarAccess_Script_gv ()
		{
			string r = "";

			r += ExprWs.Gv.doIt;
			r += "\t\tMonoBehaviour selectedMono;\n\t\tSystem.Reflection.FieldInfo selectedMonoField;\n\n";

			r += "\t\tpublic string selectedMonoFieldName = \"\";\n\t\tpublic string selectedMonoName = \"\";\n";


			if (selectedMonoField == null)
			{
				FindMonoByName ();
				FindMonoFieldByName ();
			}
			if (selectedMonoField == null)
				return r;
			
			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.rectValue, ExprWs.Gv.rectValues);
			}
			else if (typeof (Vector4) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.vector4Value, ExprWs.Gv.vector4Values);
			}
			else if (typeof (Matrix4x4) == type)
			{
				r += MonoVarAccess_Script (
					ExprWs.Gv.m44Value_entier + ExprWs.Gv.m44ValueAndProperties, 
					ExprWs.Gv.m44Value_Input_entier);
			}
			else if (typeof (Shader) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.shaderValue, ExprWs.Gv.shaderValues);
				r += MonoVarAccess_Script (ExprWs.Gv.identifiedObjects, ExprWs.Gv.identifiedObjects);
			}
			else if (typeof (Texture2D) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.texture2DValue, ExprWs.Gv.texture2DValues);
				r += MonoVarAccess_Script (ExprWs.Gv.identifiedObjects, ExprWs.Gv.identifiedObjects);
			}
			else if (typeof (Material) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.materialValue, ExprWs.Gv.materialValues);
				r += MonoVarAccess_Script (ExprWs.Gv.identifiedObjects, ExprWs.Gv.identifiedObjects);
			}
			else if (typeof (bool) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.boolValue, ExprWs.Gv.boolValues);
			}
			else if (typeof (Color) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.colorValue, ExprWs.Gv.colorValues);
			}
			else if (typeof (float) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.floatValue, ExprWs.Gv.floatValues);
			}
			else if (typeof (GameObject) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.gameObjectValue, ExprWs.Gv.gameObjectValues);
				r += MonoVarAccess_Script (ExprWs.Gv.identifiedObjects, ExprWs.Gv.identifiedObjects);
			}
			else if (typeof (int) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.intValue, ExprWs.Gv.intValues);
			}
			else if (typeof (string) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.stringValue, ExprWs.Gv.stringValues);
			}
			else if (typeof (Vector2) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.vector2Value, ExprWs.Gv.vector2Values);
			}
			else if (typeof (Vector3) == type)
			{
				r += MonoVarAccess_Script (ExprWs.Gv.vector3Value, ExprWs.Gv.vector3Values);
			}
			else
			{
			}

			return r;
		}
		string MonoVarAccess_Script_c ()
		{
			string r = "";

			r += "\t\t\tselectedMono = null;\n\t\t\tselectedMonoField = null;\n\n";

			r += "\t\t\tselectedMonoName = " + "\"" + selectedMonoName + "\";\n";
			r += "\t\t\tFindMonoByName ();\n";

			r += "\t\t\tselectedMonoFieldName = " + "\"" + selectedMonoFieldName + "\";\n";
			r += "\t\t\tFindMonoFieldByName ();\n"; 


			if (selectedMonoField == null)
			{
				FindMonoByName ();
				FindMonoFieldByName ();
			}
			if (selectedMonoField == null)
				return r;

			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.RectValue (this), ExprWs.ConstructorExpr.RectValues (this));
			}
			else if (typeof (Vector4) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.Vector4Value (this), ExprWs.ConstructorExpr.Vector4Values (this));
			}
			else if (typeof (Matrix4x4) == type)
			{
				
			}
			else if (typeof (Shader) == type)
			{
				r += MonoVarAccess_Script (
					ConstructorGetIdentifiedObject (new string [] {Enums.shaderValue_ID,}) , 
					ConstructorGetIdentifiedObject (new string [] {Enums.shaderValues_0_ID,}));
			}
			else if (typeof (Texture2D) == type)
			{
				r += MonoVarAccess_Script (
					ConstructorGetIdentifiedObject (new string [] {Enums.texture2DValue_ID,}) , 
					ConstructorGetIdentifiedObject (new string [] {Enums.texture2DValues_0_ID,}));
			}
			else if (typeof (Material) == type)
			{
				r += MonoVarAccess_Script (
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValue_ID,}) , 
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}));
			}
			else if (typeof (bool) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.BoolValue (this), ExprWs.ConstructorExpr.BoolValues (this));
			}
			else if (typeof (Color) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.ColorValue (this), ExprWs.ConstructorExpr.ColorValues (this));
			}
			else if (typeof (float) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.FloattValue (this), ExprWs.ConstructorExpr.FloattValues (this));
			}
			else if (typeof (GameObject) == type)
			{
				r += MonoVarAccess_Script (
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValue_ID,}) , 
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}));
			}
			else if (typeof (int) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.IntValue (this), ExprWs.ConstructorExpr.IntValues (this));
			}
			else if (typeof (string) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.StringValue (this), ExprWs.ConstructorExpr.StringValues (this));
			}
			else if (typeof (Vector2) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.Vector2Value (this), ExprWs.ConstructorExpr.Vector2Values (this));
			}
			else if (typeof (Vector3) == type)
			{
				r += MonoVarAccess_Script (ExprWs.ConstructorExpr.Vector3Value (this), ExprWs.ConstructorExpr.Vector3Values (this));
			}
			else
			{
			}

			return r;
		}
		string MonoVarAccess_Script_lu ()
		{
			string r = "";

			r += "\t\t\tif ( ! doIT)\n\t\t\t\treturn;\n\n\t\t\tif (selectedMono == null)\n\t\t\t\treturn;\n\n\t\t\tif (selectedMonoField == null)\n\t\t\t\treturn;\n";

			if (selectedMonoField == null)
			{
				FindMonoByName ();
				FindMonoFieldByName ();
			}
			if (selectedMonoField == null)
				return r;
			
			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "rectValue = (Rect)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, rectValues [0]);\n");
			}
			else if (typeof (Vector4) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "vector4Value = (Vector4)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, vector4Values [0]);\n");
			}
			else if (typeof (Matrix4x4) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "SetM44Value ((Matrix4x4)(selectedMonoField.GetValue (selectedMono)));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, m44Value_Input_entier [0]);\n");
			}
			else if (typeof (Shader) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "shaderValue = (Shader)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, shaderValues [0]);\n");
			}
			else if (typeof (Texture2D) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "texture2DValue = (Texture2D)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, texture2DValues [0]);\n");
			}
			else if (typeof (Material) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "materialValue = (Material)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, materialValues [0]);\n");
			}
			else if (typeof (bool) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "boolValue = (bool)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, boolValues [0]);\n");
			}
			else if (typeof (Color) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "colorValue = (Color)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, colorValues [0]);\n");
			}
			else if (typeof (float) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "floatValue = (float)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, floatValues [0]);\n");
			}
			else if (typeof (GameObject) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "gameObjectValue = (GameObject)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, gameObjectValues [0]);\n");
			}
			else if (typeof (int) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "intValue = (int)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (" + "selectedMono, intValues [0]);\n");
			}
			else if (typeof (string) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "stringValue = (string)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (" + "selectedMono, stringValues [0]);\n");
			}
			else if (typeof (Vector2) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "vector2Value = (Vector2)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, vector2Values [0]);\n");
			}
			else if (typeof (Vector3) == type)
			{
				r += MonoVarAccess_Script (
					"\t\t\t" + "vector3Value = (Vector3)(selectedMonoField.GetValue (selectedMono));\n",
					"\t\t\t" + "selectedMonoField.SetValue (selectedMono, vector3Values [0]);\n");
			}
			else
			{
			}

			return r;
		}
		string MonoVarAccess_Script_m ()
		{
			string r = "";

			r += "\t\tbool InRange (int v, int min, int max)\n\t\t{\n\t\t\treturn v >= min && v < max;\n\t\t}\n";

			r += "\t\tvoid SetSelectedMono (MonoBehaviour setMono, int index)\n\t\t{\n\t\t\tselectedMono = setMono;\n\t\t}\n\t\tMonoBehaviour [] FindMonoByName ()\n\t\t{\n\t\t\tif (string.IsNullOrEmpty (selectedMonoName))\n\t\t\t{\n\t\t\t\tSetSelectedMono (null, -1);\n\t\t\t\treturn null;\n\t\t\t}\n\n\t\t\tMonoBehaviour [] monos = Monos ();\n\t\t\tif (monos == null)\n\t\t\t{\n\t\t\t\tSetSelectedMono (null, -1);\n\t\t\t\treturn null;\n\t\t\t}\n\n\t\t\tfor (int i = 0; i < monos.Length; i++)\n\t\t\t{\n\t\t\t\tif (monos [i].name == selectedMonoName)\n\t\t\t\t{\n\t\t\t\t\tSetSelectedMono (monos [i], i);\n\t\t\t\t\treturn monos;\n\t\t\t\t}\n\t\t\t}\n\n\t\t\treturn monos;\n\t\t}\n\n\t\tvoid SetSelectedMonoField (System.Reflection.FieldInfo setMonoField, int index)\n\t\t{\n\t\t\tselectedMonoField = setMonoField;\n\t\t}\n\t\tSystem.Reflection.FieldInfo [] FindMonoFieldByName ()\n\t\t{\n\t\t\tif (string.IsNullOrEmpty (selectedMonoFieldName))\n\t\t\t{\n\t\t\t\tSetSelectedMonoField (null, -1);\n\t\t\t\treturn null;\n\t\t\t}\n\n\t\t\tSystem.Reflection.FieldInfo [] monoFields = MonoFields ();\n\t\t\tif (monoFields == null)\n\t\t\t{\n\t\t\t\tSetSelectedMonoField (null, -1);\n\t\t\t\treturn null;\n\t\t\t}\n\n\t\t\tfor (int i = 0; i < monoFields.Length; i++)\n\t\t\t{\n\t\t\t\tif (monoFields [i].Name == selectedMonoFieldName)\n\t\t\t\t{\n\t\t\t\t\tSetSelectedMonoField (monoFields [i], i);\n\t\t\t\t\treturn monoFields;\n\t\t\t\t}\n\t\t\t}\n\n\t\t\treturn monoFields;\n\t\t}\n\n\n\t\tMonoBehaviour [] Monos ()\n\t\t{\n\t\t\treturn UnityEngine.Object.FindObjectsOfType <MonoBehaviour> ();\n\t\t}\n\n\t\tSystem.Reflection.FieldInfo [] MonoFields ()\n\t\t{\n\t\t\tif (selectedMono == null)\n\t\t\t\treturn null;\n\n\t\t\tType monoType = selectedMono.GetType ();\n\t\t\tif (monoType == null)\n\t\t\t\treturn null;\n\n\t\t\treturn monoType.GetFields ();\n\t\t}\n\t";

			if (selectedMonoField == null)
			{
				FindMonoByName ();
				FindMonoFieldByName ();
			}
			if (selectedMonoField == null)
				return r;

			Type type = selectedMonoField.FieldType;

			if (typeof (Matrix4x4) == type && getOrSet == GetOrSet.get)
			{
				r += ExprWs.UMDecl.SetM44Value;
			}

			return r;
		}


		void MonoVarAccess_AdaptOnGetOrSet_Input ()
		{
			switch (getOrSet)
			{
			case GetOrSet.get:
				MonoVarAccess_Get_Input ();
				break;

			case GetOrSet.set:
				MonoVarAccess_Set_Input ();
				break;
			}
		}
		void MonoVarAccess_Set_Input ()
		{
			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawRectInputField (0, 1, 2);
			}
			else if (typeof (Vector4) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name);
				DrawVector4InputField (0);
			}
			else if (typeof (Matrix4x4) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawM44EntierInputField (0, 1, 2);
			}
			else if (typeof (Shader) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawShaderFieldInput (0, 1, 2);
			}
			else if (typeof (Texture2D) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawTexture2DFieldInput (0, 1, 2);
			}
			else if (typeof (Material) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawMaterialFieldInput (0, 1, 2);
			}
			else if (typeof (bool) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawBoolInputField (0, 1, 2);
			}
			else if (typeof (Color) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawColorInputField (0, 1, 2);
			}
			else if (typeof (float) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawFloatInputField (0, 1, 2);
			}
			else if (typeof (GameObject) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawGameObjectFieldInput (0, 1, 2);
			}
			else if (typeof (int) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawIntInputField (0, 1, 2);
			}
			else if (typeof (string) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
			}
			else if (typeof (Vector2) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawVector2InputField (0, 1, 2);
			}
			else if (typeof (Vector3) == type)
			{
				DrawLogicNodeLabel (selectedMonoField.Name, 0, 2);
				DrawVector3InputField (0, 1, 2);
			}
			else
			{
				DrawInNodeInfo ("Variable type not supported");
			}


			switch (variableType)
			{

			case VariableType.rectsList:
				break;

			case VariableType.vector4List:
				break;

			case VariableType.shadersList:
				break;

			case VariableType.texture2DList:
				break;

			case VariableType.materialsList:
				break;

			case VariableType.vector3List:
				break;

			case VariableType.vector2List:
				break;

			case VariableType.GameObjectList:
				break;

			case VariableType.stringsList:
				break;

			case VariableType.intsList:
				break;

			case VariableType.floatsList:
				break;

			case VariableType.boolsList:
				break;

			case VariableType.colorsList:
				break;
			}	
		}
		void MonoVarAccess_Get_Input ()
		{
			Type type = selectedMonoField.FieldType;

			if (typeof (Rect) == type)
			{

			}
			else if (typeof (Vector4) == type)
			{

			}
			else if (typeof (Matrix4x4) == type)
			{

			}
			else if (typeof (Shader) == type)
			{

			}
			else if (typeof (Texture2D) == type)
			{

			}
			else if (typeof (Material) == type)
			{

			}
			else if (typeof (bool) == type)
			{

			}
			else if (typeof (Color) == type)
			{

			}
			else if (typeof (float) == type)
			{

			}
			else if (typeof (GameObject) == type)
			{

			}
			else if (typeof (int) == type)
			{

			}
			else if (typeof (string) == type)
			{

			}
			else if (typeof (Vector2) == type)
			{

			}
			else if (typeof (Vector3) == type)
			{

			}
			else
			{
				DrawInNodeInfo ("Variable type not supported");
			}
		}


		MonoBehaviour [] Monos ()
		{
			return UnityEngine.Object.FindObjectsOfType <MonoBehaviour> ();
		}

		string [] MonosNames (MonoBehaviour [] monos)
		{
			if (monos == null)
				return null;

			if (monos.Length == 0)
				return null;

			string [] r = new string [monos.Length];

			for (int i = 0; i < monos.Length; i++)
			{
				r [i] = monos [i].name;
			}

			return r;
		}

		void MonosNamesEnum (string [] monosNames)
		{
			if (monosNames == null)
				return;

			if (monosNames.Length == 0)
				return;

			DrawLogicNodeLabel ("Select Script", 0, 2);
			Rect menuButtonRect = GetSuitableRect (FieldDrawType.label, 1, 2);
			string menuButtonName = "Nothing Selected";

			if (Aux.Mathm.InRange (selectedMonoIndex, 0, monosNames.Length))
				menuButtonName = monosNames [selectedMonoIndex];
			else
				menuButtonName = "Nothing Selected";
			
			if (GUI.Button (menuButtonRect, menuButtonName))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < monosNames.Length; i++)
				{
					menu.AddItem (new GUIContent (monosNames [i]), false,
						SetSelectedMonoName, monosNames [i]);
				}

				menu.ShowAsContext ();
			}

			if (menuButtonRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					menuButtonName.ToString ());



		}
		void SetSelectedMonoName (object indexObj)
		{
			selectedMonoName = indexObj.ToString ();
		}

		System.Reflection.FieldInfo [] MonoFields ()
		{
			if (selectedMono == null)
				return null;

			Type monoType = selectedMono.GetType ();
			if (monoType == null)
				return null;

			return monoType.GetFields ();
		}


		string [] MonoFieldsNames (System.Reflection.FieldInfo [] monoFields)
		{
			if (monoFields == null)
				return null;

			if (monoFields.Length == 0)
				return null;

			string [] r = new string [monoFields.Length];

			for (int i = 0; i < monoFields.Length; i++)
			{
				r [i] = monoFields [i].Name;
			}

			return r;
		}

		void MonoFieldsNamesEnum (string [] monoFieldsNames)
		{
			if (monoFieldsNames == null)
				return;

			if (monoFieldsNames.Length == 0)
				return;

			DrawLogicNodeLabel ("Select Variable", 0, 2);
			Rect menuButtonRect = GetSuitableRect (FieldDrawType.label, 1, 2);
			string menuButtonName = "Nothing Selected";

			if (Aux.Mathm.InRange (selectedMonoFieldIndex, 0, monoFieldsNames.Length))
				menuButtonName = monoFieldsNames [selectedMonoFieldIndex];
			else
				menuButtonName = "Nothing Selected";

			if (GUI.Button (menuButtonRect, menuButtonName))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < monoFieldsNames.Length; i++)
				{
					menu.AddItem (new GUIContent (monoFieldsNames [i]), false,
						SetSelectedMonoFieldName, monoFieldsNames [i]);
				}

				menu.ShowAsContext ();
			}

			if (menuButtonRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					menuButtonName.ToString ());
		}
		void SetSelectedMonoFieldName (object indexObj)
		{
			selectedMonoFieldName = indexObj.ToString ();
		}
	}
}
