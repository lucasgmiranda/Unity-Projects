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
	/// LogicNode_001
	/// The building block of the Diamond Graph, here we have the logic executed.
	/// However, Other classes like Node, NodeState, and Logic, manage
	/// which logic and when this logic will be executed.
	/// 
	/// In the Logic Node things are executed,
	/// 	For workflow speed, the Logic Node is adaptive,
	/// 	no need to search in big lists of nodes, you create your Logic Node and you adapt it to your need.
	/// 	to do that, the Logic Node show you 3 enum buttons: Logic Type, Variable Type, and Compute Type.
	/// 	In Logic Type you choose what you want to do (define an input, compute or operation, time operation).
	/// 	In Variable Type you choose on which variabale you want to apply your Logic Type (bool, float, game object,
	/// 	transform, camera, rigidbody etc..).
	/// 	In Compute Type: according to your earlier choices (Logic Type and Variable Type), the Logic Node adapte the
	/// 	Compute Type enum and offer to you a list of operations corresponding to your choice.
	/// 	To explaine the Logic Node adaptivity, lets see some examples: 
	/// 	Example 1: In the Logic Type, choose "Compute Or Operation". In the Variable Type, choose "Camera".
	/// 	click on the Compute Type enum button and you will see a list of operations related to the camera
	/// 	like "Screen Point To Ray", .., "Get Far Clip Plan", .. "Set Field Of View" etc..
	/// 	Example 2: In the Logic Type, choose "Compute Or Operation". In the Variable Type, choose 
	/// 	"Component Transform".
	/// 	click on the Compute Type enum button and you will see a list of operations related to the transform
	/// 	like "Get Position", .. "Get Child Count", .. "Translate" etc..
	/// 	Example 3: In the Logic Type, choose "Time Operation". Click on the Time Type enum button and you will 
	/// 	see a list of variables or operations related to the time or the frames like "Delta Time", .. "TicTac One Frames", 
	/// 	.. "Tictac On Time", .. "Time Since Level Load" etc.. 
	/// 	Example 4: In the Logic Type, choose "Unity Input Class And Cross Platform". In the Compute Type choose
	/// 	"Get Axis". The Logic Node will invite you to enter your axis name and to choose if you want to use 
	/// 	cross platform inputs or not. If you have spelling errors in the axe name, the Logic Node will tell you
	/// 	that this axis is not defined in the Unity Input Manager. 
	/// 	P.S. To acess the Unity Input Manager, in th Unity editor go to top menu: Edit->Project Settings->Input
	/// 	P.S. Cross platform inputs runs only on generated scripts (no run in editor), in order to use the 
	/// 	cross platform inputs, import the Unity cross platform inputs standard asset.
	/// 	P.S In the Logic Type, instead of "Unity Input Class And Cross Platform", you can choose "Input", it's
	/// 	a simpler case of the first one. In it, you will choose essentially desktop inputs (keyboard and mouse).
	/// 	P.S In the Logic Type, you can use also "Mouse Input", the features of "Mouse Input" exists already in
	/// 	"Unity Input Class And Cross Platform", but you choose it if you want to have the mouse position 
	/// 	after freeing the cursor. Some First Person cameras may lock the cursor at the screen center,
	/// 	so sometimes you need to free it if you want to use the mouse position.
	/// </summary>

	/// Access shders properties
	/// Access other scripts variables
	/// Zooming
	/// 
	public partial class LogicNode : ScriptableObject 
	{
		public float rectWidthFactorDefaultFactor = 1f;

		partial void AdaptOnLogicType ()
		{
			if ( ! string.IsNullOrEmpty (specialNodeType))
			{
				switch (specialNodeType)
				{
				case Enums.add_Terra_LogicNode:
					logicType = LogicType.UseExtensions;
					logicNodeExtensions = LogicNodeExtensions.terra;
					LogicNode_terra ();
					break;

				case Enums.add_Wood_LogicNode:
					logicType = LogicType.UseExtensions;
					logicNodeExtensions = LogicNodeExtensions.materials;
					materialType = MaterialType.wood;
					LogicNode_wood ();
					break;
				}

				return;
			}


			switch (logicType)
			{
			case LogicType.Ads:
				rectWidthFactorDefaultFactor = 1f;

				ApplyComputeAds ();
				break;

			case LogicType.computeOrOperation:
				rectWidthFactorDefaultFactor = 1f;

				DrawVariableTypeEnum ();

				switch (variableType)
				{
				case VariableType.componentLight:
					ApplyComputeLight ();
					break;

				case VariableType.componentUIToggle:
					ApplyComputeUIToggle ();
					break;

				case VariableType.componentUIInputField:
					ApplyComputeUIInputField ();
					break;


				case VariableType.componentCircleCollider2D:
					ApplyComputeCircleCollider2D ();
					break;

				case VariableType.componentBoxCollider2D:
					ApplyComputeBoxCollider2D ();
					break;




				case VariableType.componentMeshCollider:
					ApplyComputeMeshCollider ();
					break;

				case VariableType.componentCapsuleCollider:
					ApplyComputeCapsuleCollider ();
					break;

				case VariableType.componentSphereCollider:
					ApplyComputeSphereCollider ();
					break;

				case VariableType.componentBoxCollider:
					ApplyComputeBoxCollider ();
					break;


				case VariableType.componentAudioSource:
					ApplyComputeAudioSource ();
					break;

				case VariableType.componentUIButton:
					ApplyComputeUiButton ();
					break;

				case VariableType.componentUIImage:
					ApplyComputeUiImage ();
					break;

				case VariableType.componentUIUnityText:
					ApplyComputeUnityText ();
					break;

				case VariableType.rect:
					ApplyComputeRect ();
					break;

				case VariableType.Vector4:
					ApplyComputeVector4 ();
					break;

				case VariableType.Ray2D:
					ApplyComputeRay2D ();
					break;

				case VariableType.Ray:
					ApplyComputeRay ();
					break;

				case VariableType.componentRigidBody2D:
					ApplyComputeRigidBody2D ();
					break;

				case VariableType.rectsList:
					ApplyComputeRectList ();
					break;

				case VariableType.vector4List:
					ApplyComputeVector4List ();
					break;

				case VariableType.shadersList:
					ApplyComputeShaderList ();
					break;

				case VariableType.texture2DList:
					ApplyComputeTexture2DList ();
					break;

				case VariableType.materialsList:
					ApplyComputeMaterialList ();
					break;



				case VariableType.vector3List:
					ApplyComputeVector3List ();
					break;

				case VariableType.vector2List:
					ApplyComputeVector2List ();
					break;

				case VariableType.stringsList:
					ApplyComputeStringList ();
					break;

				case VariableType.intsList:
					ApplyComputeIntList ();
					break;

				case VariableType.floatsList:
					ApplyComputeFloatList ();
					break;

				case VariableType.componentCollider2D:
					ApplyComputeCollider2D ();
					break;

				case VariableType.Matrix4x4:
					ApplyComputeM44 ();
					break;

				case VariableType.Camera:
					ApplyComputeCamera ();
					break;

					//case VariableType.meshOnGameObject:
					//	ApplyComputeMesh ();
					//	break;

				case VariableType.componentNavMeshAgent:
					ApplyComputeNavMeshAgent ();
					break;

				case VariableType.componentParticleSystem:
					ApplyComputeParticleSystem ();
					break;

				case VariableType.Shader:
					ApplyComputeShader ();
					break;

					//case VariableType.RenderTexture:
					//	ApplyComputeRenderTexture ();
					//	break;

				case VariableType.Texture2D:
					ApplyComputeTexture2D ();
					break;

				case VariableType.Material:
					ApplyComputeMaterial ();
					break;

				case VariableType.Bool:
					ApplyComputeBool ();
					break;

				case VariableType.boolsList:
					ApplyComputeBoolList ();
					break;

				case VariableType.Color:
					ApplyComputeColor ();
					break;

				case VariableType.colorsList:
					ApplyComputeColorList ();
					break;

				case VariableType.Float:
					ApplyComputeFloat ();
					break;

				case VariableType.GameObject:
					ApplyComputeGameObject ();
					break;

				case VariableType.GameObjectList:
					ApplyComputeGameObjectList ();
					break;


				case VariableType.componentCollider:
					ApplyComputeCollider ();
					break;



				case VariableType.componentRenderer:
					ApplyComputeRenderer ();
					break;

				case VariableType.componentSpriteRenderer:
					ApplyComputeSpriteRenderer ();
					break;

				case VariableType.componentLineRenderer:
					ApplyComputeLineRenderer ();
					break;



				case VariableType.componentRigidBody:
					ApplyComputeRigidBody ();
					break;

				case VariableType.componentTransform:
					ApplyComputeTransform ();
					break;


				case VariableType.Int:
					ApplyComputeInt ();
					break;

				case VariableType.String:
					ApplyComputeString ();
					break;

				case VariableType.Vector2:
					ApplyComputeVector2 ();
					break;

				case VariableType.Vector3:
					ApplyComputeVector3 ();
					break;
				}				

				break;

			//case LogicType.GetSetOtherGameObjectsComponentsVariables:
			//	rectWidthFactorDefaultFactor = 1f;
			//	OtherGameObjectComponent_GetSetVariables_Head ();
			//	break;

			case LogicType.GetSetOtherGameObjectsScriptsVariables:
				rectWidthFactorDefaultFactor = 1f;
				MonoVarAccess_Head ();
				break;

			case LogicType.input:
				rectWidthFactorDefaultFactor = 1f;

				DrawKeyCodeEnum ();

				DrawKeyAndMouseTriggerWhenEnum ();

				ApplyInput ();

				DrawBoolResultField ();
				break;

			case LogicType.mouseInput:
				rectWidthFactorDefaultFactor = 1f;

				ApplyComputeMouseInput ();
				break;


			//case LogicType.test:
			//	rectWidthFactorDefaultFactor = 1f;
			//	Test_Head ();
			//	break;

			case LogicType.timeOperation:
				rectWidthFactorDefaultFactor = 1f;

				DrawTimeTypeEnum ();

				ApplyTimeOperation ();

				TimeOperation_OutputField ();
				break;

			case LogicType.unityInputClassAndCrossPlatform:
				rectWidthFactorDefaultFactor = 1f;
				ApplyUnityInputClassAndCrossPlatform ();
				break;

			case LogicType.UseExtensions:
				rectWidthFactorDefaultFactor = 1f;
				CallPartialExtentions ();
				break;
			}


			///
			if (logicType == LogicType.Ads)
			{

			}
			else if (logicType == LogicType.mouseInput)
			{

			}
			else if (logicType == LogicType.computeOrOperation)
			{
			}
			else if (logicType == LogicType.input)
			{
				
			}
			else if (logicType == LogicType.unityInputClassAndCrossPlatform)
			{

			}
			else if (logicType == LogicType.timeOperation)
			{

			}
			else if (logicType == LogicType.UseExtensions)
			{

			}
			//else if (logicType == LogicType.UseHighLevelNodes)
			//{
			//	CallPartialHighLevelNodes ();
			//}
			//else if (logicType == LogicType.GetSetOtherGameObjectsComponentsVariables)
			//{
			//
			//}
			//else if (logicType == LogicType.test)
			//{	
			//
			//}
		}

		void CallPartialExtentions ()
		{
			DrawExtensionsEnum ();

			AdaptOnLogicNodeExtensions ();
		}

		public LogicNodeExtensions logicNodeExtensions;
		public int logicNodeExtensions_length;
		public string logicNodeExtensions_last;

		void DrawExtensionsEnum ()
		{
			DrawLogicNodeLabel ("Extensions", 0, 2);
			logicNodeExtensions = (LogicNodeExtensions)DrawEnum (logicNodeExtensions,
				ref logicNodeExtensions_length, ref logicNodeExtensions_last, typeof(LogicNodeExtensions),
				FieldDrawType.label, 1, 2);
		}

		void AdaptOnLogicNodeExtensions ()
		{
			switch (logicNodeExtensions)
			{
			case LogicNodeExtensions.terra:
				LogicNode_terra ();
				GetTerra ();
				break;

			case LogicNodeExtensions.materials:
				LogicNode_materials ();
				break;

			case LogicNodeExtensions.gameDesign:
				LogicNode_gameDesign ();
				break;
			}
		}

		partial void LogicNode_terra ();
		void GetTerra ()
		{
			if ( ! ExtensionsHere.terra)
				DrawExtensionUrlButton ("Terra", ExtensionsHere.terraUrl);
		}

		partial void LogicNode_materials ();

		partial void LogicNode_gameDesign ();



		/// <summary>
		/// Draws the enum.
		/// </summary>
		/// <returns>The enum.</returns>
		/// <param name="selected">Selected.</param>
		/// <param name="selectedLength">Selected length.</param>
		/// <param name="selectedLast">Selected last.</param>
		/// <param name="selectedType">Selected type.</param>
		/// <param name="fieldDrawType">Field draw type.</param>
		/// <param name="column">Column.</param>
		/// <param name="totalColumns">Total columns.</param>
		Enum DrawEnum (Enum selected, ref int selectedLength, ref string selectedLast,
			Type selectedType, FieldDrawType fieldDrawType, int column, int totalColumns)
		{
			Rect suitRect = GetSuitableRect (fieldDrawType, column, totalColumns);

			if (selectedLength != Enum.GetNames (selectedType).Length)
			{
				selectedLength = Enum.GetNames (selectedType).Length;

				if ( ! string.IsNullOrEmpty (selectedLast))
					selected = (Enum)Enum.Parse (selectedType, selectedLast);
			}

			Enum r = EditorGUI.EnumPopup (suitRect, selected);

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					selected.ToString ());



			selectedLast = r.ToString ();
			return r;
		}
	
		Enum DrawEnum (Enum selected, ref int selectedLength, ref string selectedLast,
			Type selectedType, string label, string labelStyle)
		{
			if ( ! string.IsNullOrEmpty (label))
				DrawLabelField (FieldDrawType.label, label, labelStyle);
		
			Rect suitRect = GetSuitableRect (FieldDrawType.variable);

			if (selectedLength != Enum.GetNames (selectedType).Length)
			{
				selectedLength = Enum.GetNames (selectedType).Length;

				if ( ! string.IsNullOrEmpty (selectedLast))
					selected = (Enum)Enum.Parse (selectedType, selectedLast);
			}

			Enum r = EditorGUI.EnumPopup (suitRect, selected);

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					selected.ToString ());



			selectedLast = r.ToString ();
			return r;
		}
	

		#region material_getSetShaderProperties
		public enum GetSetShaderPropertiesEnum
		{
			HasProperty,

			GetColor,

			GetFloat,

			GetInt,

			GetTexture,

			GetTextureOffset,

			GetTextureScale,

			GetVector,

			SetColor,

			SetFloat,

			SetInt,

			SetTexture,

			SetTextureOffset,

			SetTextureScale,

			SetVector,
		}
		public GetSetShaderPropertiesEnum getSetShaderPropertiesEnum;
		public int getSetShaderPropertiesEnum_length = 0;
		public string getSetShaderPropertiesEnum_last;


		void PropertyNameSttring_0_Input ()
		{
			DrawLogicNodeLabel ("Property name", 0, 2);
			DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
		}
		void GetSetShaderProperties_Input ()
		{
			DrawLogicNodeLabel ("Do", 0, 2);
			getSetShaderPropertiesEnum = (GetSetShaderPropertiesEnum)DrawEnum (getSetShaderPropertiesEnum,
				ref getSetShaderPropertiesEnum_length, ref getSetShaderPropertiesEnum_last, 
				typeof (GetSetShaderPropertiesEnum), FieldDrawType.label, 1, 2);

			switch (getSetShaderPropertiesEnum)
			{
			case GetSetShaderPropertiesEnum.GetColor:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetFloat:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetInt:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetTexture:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetTextureOffset:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetTextureScale:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.GetVector:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.HasProperty:
				PropertyNameSttring_0_Input ();
				break;

			case GetSetShaderPropertiesEnum.SetColor:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Color", 0, 2);
				DrawColorInputField (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetFloat:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Float", 0, 2);
				DrawFloatInputField (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetInt:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Int", 0, 2);
				DrawIntInputField (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetTexture:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Texture", 0, 2);
				DrawTexture2DFieldInput (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetTextureOffset:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Texture offset", 0, 2);
				DrawVector2InputField (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetTextureScale:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Texture scale", 0, 2);
				DrawVector2InputField (0, 1, 2);
				break;

			case GetSetShaderPropertiesEnum.SetVector:
				PropertyNameSttring_0_Input ();
				DrawLogicNodeLabel ("Vector4");
				DrawVector4InputField (0);
				break;
			}
		}
		void GetSetShaderProperties ()
		{
			if (materialValues [0] == null)
				return;
			
			switch (getSetShaderPropertiesEnum)
			{
			case GetSetShaderPropertiesEnum.GetColor:
				colorValue = materialValues [0].GetColor (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetFloat:
				floatValue = materialValues [0].GetFloat (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetInt:
				intValue = materialValues [0].GetInt (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetTexture:
				texture2DValue = (Texture2D)materialValues [0].GetTexture (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetTextureOffset:
				vector2Value = materialValues [0].GetTextureOffset (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetTextureScale:
				vector2Value = materialValues [0].GetTextureScale (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.GetVector:
				vector4Value = materialValues [0].GetVector (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.HasProperty:
				boolValue = materialValues [0].HasProperty (stringValues [0]);
				break;

			case GetSetShaderPropertiesEnum.SetColor:
				materialValues [0].SetColor (stringValues [0], colorValues [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetFloat:
				materialValues [0].SetFloat (stringValues [0], floatValues [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetInt:
				materialValues [0].SetInt (stringValues [0], intValues [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetTexture:
				materialValues [0].SetTexture (stringValues [0], texture2DValues [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetTextureOffset:
				materialValues [0].SetTextureOffset (stringValues [0], vector2Values [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetTextureScale:
				materialValues [0].SetTextureScale (stringValues [0], vector2Values [0]);
				materialValue = materialValues [0];
				break;

			case GetSetShaderPropertiesEnum.SetVector:
				materialValues [0].SetVector (stringValues [0], vector4Values [0]);
				materialValue = materialValues [0];
				break;
			}
		}
		void GetSetShaderProperties_Outputs ()
		{
			string[] documentationMessage;

			switch (getSetShaderPropertiesEnum)
			{
			case GetSetShaderPropertiesEnum.GetColor:
				DrawColorResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named color value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetColor.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetFloat:
				DrawFloatResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named float value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetFloat.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetInt:
				DrawIntResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named int value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetInt.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetTexture:
				DrawTexture2DResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetTexture.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetTextureOffset:
				DrawVector2ResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Gets the placement offset of texture",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetTextureOffset.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetTextureScale:
				DrawVector2ResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Gets the placement scale of texture",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetTextureScale.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.GetVector:
				DrawVector4ResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named vector value",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.GetVector.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.HasProperty:
				DrawBoolResultField ();


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Checks if material's shader has a property of a given name.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.HasProperty.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetColor:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets a named color value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetColor.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetFloat:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets a named float value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetFloat.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetInt:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets a named int value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetInt.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetTexture:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets a named texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetTexture.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetTextureOffset:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets the placement offset of texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetTextureOffset.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetTextureScale:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets the placement scale of texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetTextureScale.html", 
					"");
				break;

			case GetSetShaderPropertiesEnum.SetVector:
				DrawMaterialResultField (true);


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Sets a named vector value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material.SetVector.html", 
					"");
				break;
			}
		}
		#endregion material_getSetShaderProperties




		#region OtherGameObjectComponent_GetSetVariables
		void OtherGameObjectComponent_GetSetVariables_Head ()
		{
			OtherGameObjectComponent_GetSetVariables_Inputs ();
			if (logic.playing) OtherGameObjectComponent_GetSetVariables ();
			OtherGameObjectComponent_GetSetVariables_Outputs ();
		}


		public string OtherGameObjectComponent_componentTypeName = "Component";
		public string OtherGameObjectComponent_propertyName = "Property";

		void OtherGameObjectComponent_GetSetVariables_Inputs ()
		{
			//OtherGameObjectComponent_GetSetVariables_InputsGood = false;

			DrawLogicNodeLabel ("Game Object", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill in Game Object field");
				return;
			}

			// . //
			Component [] otherGameObjectComponents = 
				gameObjectValues [0].GetComponents <Component> ();
			if (otherGameObjectComponents == null)
			{
				DrawInNodeInfo ("no components found on this Game Object");
				return;
			}


			string [] componentsTypesNames = GetComponentsTypesNames (ref otherGameObjectComponents);
			if (componentsTypesNames == null)
			{
				DrawInNodeInfo ("no components types found on this Game Object");
				return;
			}


			DrawLogicNodeLabel ("Component", 0, 2);
			DrawStringListMenuToString_OtherGameObjectComponent_componentTypeName (componentsTypesNames, 
				new string[]{"None component is found",}, 1, 2);
			if (string.IsNullOrEmpty (OtherGameObjectComponent_componentTypeName))
			{
				DrawInNodeInfo ("Selected Component Type Name is not recognized");
				return;
			}



			Component selectedComponent = GetComponentOnTypeName (ref otherGameObjectComponents, 
				OtherGameObjectComponent_componentTypeName);
			if (selectedComponent == null)
			if (componentsTypesNames == null)
			{
				DrawInNodeInfo ("no component of type: '" + OtherGameObjectComponent_componentTypeName + "' found");
				return;
			}

			// . //
			SerializedProperty [] serializedProperties = GetComponentProperties (ref selectedComponent);
			if (serializedProperties == null)
			{
				DrawInNodeInfo ("no properties found in the component: '" + OtherGameObjectComponent_componentTypeName);
				return;
			}


			//string [] serializedProperties_displayNames = SerializedPropertyToDisplayName (ref serializedProperties);
			//if (serializedProperties_displayNames == null)
			//{
			//	DrawInNodeInfo ("properties display names not found");
			//	return;
			//}

			string [] serializedProperties_Names = SerializedPropertyToName (ref serializedProperties);
			if (serializedProperties_Names == null)
			{
				DrawInNodeInfo ("properties names not found");
				return;
			}


			DrawLogicNodeLabel ("Property", 0, 2);
			DrawStringListMenuToString_OtherGameObjectComponent_propertyName (
				serializedProperties_Names,
				new string[]{"None property is found",}, 1, 2);
			if (string.IsNullOrEmpty (OtherGameObjectComponent_propertyName))
			{
				DrawInNodeInfo ("Selected Property name is not recognized");
				return;
			}


			 
			selectedProperty = GetSerializedPropertyOnName (ref serializedProperties, 
				OtherGameObjectComponent_propertyName);
			if (selectedProperty == null)
			{
				DrawInNodeInfo ("Selected Property is not recognized");
				return;
			}
			selectedProperty_propertyType = selectedProperty.propertyType.ToString ();

			DrawLogicNodeLabel ("Get/Set?", 0, 2);
			getOrSet = (GetOrSet)DrawEnum (getOrSet,
				ref selectedPropertyGetSet_length, ref selectedPropertyGetSet_last, typeof (GetOrSet),
				FieldDrawType.label, 1, 2);

			switch (getOrSet)
			{
			case GetOrSet.get:
				break;

			case GetOrSet.set:
				string valueToSetExpr = "Value To Set";
				switch (selectedProperty.propertyType)
				{
				case SerializedPropertyType.Boolean:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawBoolInputField (0, 1, 2);
					break;

				case SerializedPropertyType.Color:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawColorInputField (0, 1, 2);
					break;

				case SerializedPropertyType.Float:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawFloatInputField (0, 1, 2);
					break;

				case SerializedPropertyType.Integer:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawIntInputField (0, 1, 2);
					break;

				case SerializedPropertyType.Rect:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawRectInputField (0, 1, 2);
					break;

				case SerializedPropertyType.String:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
					break;

				case SerializedPropertyType.Vector2:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawVector2InputField (0, 1, 2);
					break;

				case SerializedPropertyType.Vector3:
					DrawLogicNodeLabel (valueToSetExpr, 0, 2);
					DrawVector3InputField (0, 1, 2);
					break;

				case SerializedPropertyType.Vector4:
					DrawLogicNodeLabel (valueToSetExpr);
					DrawVector4InputField (0);
					break;
				}
				break;
			}
				
		
			//OtherGameObjectComponent_GetSetVariables_InputsGood = true;
		
			oGoGsComp_DrawDoItOnInputs ();
		}
		//bool OtherGameObjectComponent_GetSetVariables_InputsGood = false;
		void oGoGsComp_DrawDoItOnInputs ()
		{
			if (GUI.Button (GetSuitableRect (FieldDrawType.label), "Prepare Action"))
			{
				switch (getOrSet)
				{
				case GetOrSet.get:
					break;

				case GetOrSet.set:
					oGoGsComp_prepareClicked = oGoGsComp_prepare ();
					break;
				}
			}

			if (oGoGsComp_prepareClicked)
			{
				isCompiling = EditorApplication.isCompiling;
				DrawInNodeInfo ("Preparing ..");

				if ( ! isCompiling)
				{
					oGoGsComp_prepareClicked = false;

					oGoGsComp_prepared = true;
					oGoGsComp_PrepReady = true;
				}
			}

			if ( ! oGoGsComp_prepared)
			{
				DrawInNodeInfo ("Not prepared");

				//Debug.LogWarning ("Action is not prepared");
			}
			else if (oGoGsComp_prepared && oGoGsComp_PrepReady)
			{
				DrawLogicNodeLabel ("Prepared");
			}
			else
			{
				DrawLogicNodeLabel ("Prepared but not ready");
			}

			DrawDoItButton ();
		}

		public bool noAlwaysDoItOption = false;
		void DrawDoItButton (bool noAlwaysDoIT)
		{
			noAlwaysDoItOption = true;
			alwaysDoIt = false;

			Rect suitRect = DrawDoItInputField ();

			if (maximized)
			if (GUI.Button (suitRect, doItbuttonLabel) && eGlobal.button == 0)
			{

				doIT = true;
			}

			int varIndex = IndexOfInID (Enums.doIt_ID);

			if (varIndex > -1 && varIndex < activeInputsFields.Length)
				activeInputsFields [varIndex] = true;
		}

		public SerializedProperty selectedProperty = null;
		public string selectedProperty_propertyType = "";
		public enum GetOrSet
		{
			get,

			set,
		}
		public GetOrSet getOrSet;
		public int selectedPropertyGetSet_length = 0;
		public string selectedPropertyGetSet_last;
		const string tmpCsGetSetOtherGoCompProps = "TmpCsGetSetOtherGoCompProps";

		GameObject OtherGameObjectComponentGetSet_TmpGo = null;
		bool oGoGsComp_prepare ()
		{
			OtherGameObjectComponentGetSet_TmpGo = new GameObject (OtherGameObjectComponentGetSet_TmpGoName);
			if (OtherGameObjectComponentGetSet_TmpGo == null)
				return false;

			return OtherGameObjectComponentGetSetCreateTmpGo ();
		}
		bool oGoGsComp_prepared = false;
		bool oGoGsComp_PrepReady = false;
		bool oGoGsComp_prepareClicked = false;
		static bool isCompiling = false;
		void OtherGameObjectComponent_GetSetVariables ()
		{
			//if ( ! OtherGameObjectComponent_GetSetVariables_InputsGood)
			//	return;

			//Debug.Log ("work");

			if ( ! doIT)
				return;


			
			OtherGameObjectComponentGetSetTmpGoSendMessage ();



			//switch (getOrSet)
			//{
			//case GetOrSet.get:
			//	break;
			//
			//case GetOrSet.set:
			//	GameObject go = new GameObject (OtherGameObjectComponentGetSet_TmpGoName);
			//	if (go == null)
			//		return;
			//
			//	if (OtherGameObjectComponentGetSetCreateTmpGo (ref go))
			//	{
			//		OtherGameObjectComponentGetSetTmpGoSendMessage (ref go);
			//	}
			//
			//	//EditorGUI.BeginChangeCheck ();
			//	//
			//	//switch (selectedProperty.propertyType)
			//	//{
			//	//case SerializedPropertyType.Boolean:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Color:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Float:
			//	//	selectedProperty.floatValue = floatValues [0];
			//	//
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Integer:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Rect:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.String:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Vector2:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Vector3:
			//	//	break;
			//	//
			//	//case SerializedPropertyType.Vector4:
			//	//	break;
			//	//}
			//	//
			//	//if (EditorGUI.EndChangeCheck ())
			//	//{
			//	//	serializedComp.ApplyModifiedProperties ();
			//	//
			//	//	serializedComp.Update ();
			//	//}
			//	break;
			//}

		}

		void OtherGameObjectComponent_GetSetVariables_Outputs ()
		{
			if (selectedProperty == null)
				return;
			
			switch (getOrSet)
			{
			case GetOrSet.get:
				switch (selectedProperty.propertyType)
				{
				case SerializedPropertyType.Boolean:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawBoolResultField (1, 2);
					break;

				case SerializedPropertyType.Color:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawColorResultField (1, 2);
					break;

				case SerializedPropertyType.Float:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawFloatResultField (true, 1, 2);
					break;

				case SerializedPropertyType.Integer:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawIntResultField (true, 1, 2);
					break;

				case SerializedPropertyType.Rect:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawRectResultField (true, 1, 2);
					break;

				case SerializedPropertyType.String:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawStringResultField (true, 1, 2);
					break;

				case SerializedPropertyType.Vector2:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawVector2ResultField (true, 1, 2);
					break;

				case SerializedPropertyType.Vector3:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawVector3ResultField (true, 1, 2);
					break;

				case SerializedPropertyType.Vector4:
					DrawLogicNodeLabel ("Got Value", 0, 2);
					DrawVector4ResultField (true, 1, 2);
					break;
				}
				break;

			case GetOrSet.set:
				break;
			}
		}

		string [] OtherGameObjectComponent_GetCodeFrom ()
		{
			string [] r = new string[5];

			if (string.IsNullOrEmpty (selectedProperty_propertyType))
			{
				return r;
			}

			string gv_getSet = "";
			string cst_getSet = "";
			switch (getOrSet)
			{
			case GetOrSet.get:
				switch (selectedProperty_propertyType)
				{
				case "Boolean":
					gv_getSet = ExprWs.Gv.boolValue;
					break;

				case "Color":
					gv_getSet = ExprWs.Gv.colorValue;
					break;

				case "Float":
					gv_getSet = ExprWs.Gv.floatValue;
					break;

				case "Integer":
					gv_getSet = ExprWs.Gv.intValue;
					break;

				case "Rect":
					gv_getSet = ExprWs.Gv.rectValue;
					break;

				case "String":
					gv_getSet = ExprWs.Gv.stringValue;
					break;

				case "Vector2":
					gv_getSet = ExprWs.Gv.vector2Value;
					break;

				case "Vector3":
					gv_getSet = ExprWs.Gv.vector3Value;
					break;

				case "Vector4":
					gv_getSet = ExprWs.Gv.vector4Value;
					break;
				}
				break;

			case GetOrSet.set:
				switch (selectedProperty_propertyType)
				{
				case "Boolean":
					gv_getSet = ExprWs.Gv.boolValues;
					cst_getSet = ExprWs.ConstructorExpr.BoolValues (this);
					break;

				case "Color":
					gv_getSet = ExprWs.Gv.colorValues;
					cst_getSet = ExprWs.ConstructorExpr.ColorValues (this);
					break;

				case "Float":
					gv_getSet = ExprWs.Gv.floatValues;
					cst_getSet = ExprWs.ConstructorExpr.FloattValues (this);
					break;

				case "Integer":
					gv_getSet = ExprWs.Gv.intValues;
					cst_getSet = ExprWs.ConstructorExpr.IntValues (this);
					break;

				case "Rect":
					gv_getSet = ExprWs.Gv.rectValues;
					cst_getSet = ExprWs.ConstructorExpr.RectValues (this);
					break;

				case "String":
					gv_getSet = ExprWs.Gv.stringValues;
					cst_getSet = ExprWs.ConstructorExpr.StringValues (this);
					break;

				case "Vector2":
					gv_getSet = ExprWs.Gv.vector2Values;
					cst_getSet = ExprWs.ConstructorExpr.Vector2Values (this);
					break;

				case "Vector3":
					gv_getSet = ExprWs.Gv.vector3Values;
					cst_getSet = ExprWs.ConstructorExpr.Vector3Values (this);
					break;

				case "Vector4":
					gv_getSet = ExprWs.Gv.vector4Values;
					cst_getSet = ExprWs.ConstructorExpr.Vector4Values (this);
					break;
				}
				break;
			}

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				gv_getSet;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				cst_getSet;

			r [2] = "\t\t\t" + callMeByMessage +" ();\n";


			r [3] = "\t\tvoid " + callMeByMessage + " ()\n\t\t{\n" +

				"\t\t\tif ( ! doIT)\n\t\t\t{\n\t\t\t\treturn;\n\t\t\t}\n" +

				OtherGameObjectComponentGetSetString () +
				"\t\t}\n";
			
			r [4] = "";


			return r;
		}
		#endregion OtherGameObjectComponent_GetSetVariables
	

		#region Auxiliaries_Components
		public static string [] GetComponentsTypesNames (ref Component [] components)
		{
			if (components == null)
				return null;
			

			string [] r = new string[components.Length];

			for (int i = 0; i < components.Length; i++)
			{
				r [i] = components [i].GetType ().ToString ();
			}

			return r;
		}

		public static Component GetComponentOnTypeName (ref Component [] components, string typeName)
		{
			if (components == null)
				return null;

			Component r = null;

			for (int i = 0; i < components.Length; i++)
			{
				if (components [i].GetType ().ToString () != typeName)
				{
					continue;
				}
				else
				{
					r = components [i];
					break;
				}
			}

			return r;
		}
		SerializedObject serializedComp = null;
		public SerializedProperty [] GetComponentProperties (ref Component component)
		{
			if (component == null)
				return null;


			serializedComp = new SerializedObject (component);
			if (serializedComp == null)
				return null;
			

			SerializedProperty serializedCompPropertyIterator = serializedComp.GetIterator ();
			if (serializedCompPropertyIterator == null)
				return null;


			List <SerializedProperty> r = new  List<SerializedProperty>();

			serializedCompPropertyIterator.Next (true);

			do
			{
				r.Add (serializedCompPropertyIterator.Copy ());
			}
			while (serializedCompPropertyIterator.Next (false));

			if (r.Count == 0)
				return null;

			return r.ToArray ();
		}

		public static SerializedProperty GetSerializedPropertyOnDisplayName (ref SerializedProperty [] props, string displayName)
		{
			if (props == null)
				return null;

			SerializedProperty r = null;

			for (int i = 0; i < props.Length; i++)
			{
				if (props [i].displayName != displayName)
				{
					continue;
				}
				else
				{
					r = props [i];
					break;
				}
			}

			return r;
		}

		public static SerializedProperty GetSerializedPropertyOnName (ref SerializedProperty [] props, string propName)
		{
			if (props == null)
				return null;

			SerializedProperty r = null;

			for (int i = 0; i < props.Length; i++)
			{
				if (props [i].name != propName)
				{
					continue;
				}
				else
				{
					r = props [i];
					break;
				}
			}

			return r;
		}


		public static String [] SerializedPropertyToDisplayName (ref SerializedProperty [] props)
		{
			if (props == null)
				return null;

			string [] r = new string[props.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = props [i].displayName;
			}

			if (r.Length == 0)
				return null;

			return r;
		}

		public static String [] SerializedPropertyToName (ref SerializedProperty [] props)
		{
			if (props == null)
				return null;

			string [] r = new string[props.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = props [i].name;
			}

			if (r.Length == 0)
				return null;

			return r;
		}

		#endregion Auxiliaries_Components
	

		#region Draw_Fields
		void DrawStringListMenuToString_OtherGameObjectComponent_componentTypeName (string [] menuNames, string [] notFoundMessage,
			int column, int totalColumns)
		{
			if (menuNames == null || menuNames.Length == 0)
			{
				for (int i = 0; i < notFoundMessage.Length; i++)
				{
					DrawInNodeInfo (notFoundMessage [i]);
				}

				return;
			}

			Rect suitRect = GetSuitableRect (FieldDrawType.label, column, totalColumns);



			if (GUI.Button (suitRect, OtherGameObjectComponent_componentTypeName))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < menuNames.Length; i++)
				{
					if (string.IsNullOrEmpty (menuNames [i]))
						continue;

					menu.AddItem (new GUIContent (menuNames [i]), false, 
						SetThisToString_OtherGameObjectComponent_componentTypeName, menuNames [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					OtherGameObjectComponent_componentTypeName);
		}
		void SetThisToString_OtherGameObjectComponent_componentTypeName (object s)
		{
			OtherGameObjectComponent_componentTypeName = s.ToString ();
		}


		void DrawStringListMenuToString_OtherGameObjectComponent_propertyName (string [] menuNames, string [] notFoundMessage,
			int column, int totalColumns)
		{
			if (menuNames == null || menuNames.Length == 0)
			{
				for (int i = 0; i < notFoundMessage.Length; i++)
				{
					DrawInNodeInfo (notFoundMessage [i]);
				}

				return;
			}

			Rect suitRect = GetSuitableRect (FieldDrawType.label, column, totalColumns);



			if (GUI.Button (suitRect, OtherGameObjectComponent_propertyName))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < menuNames.Length; i++)
				{
					if (string.IsNullOrEmpty (menuNames [i]))
						continue;

					menu.AddItem (new GUIContent (menuNames [i]), false, 
						SetThisToString_OtherGameObjectComponent_propertyName, menuNames [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					OtherGameObjectComponent_propertyName);
		}
		void SetThisToString_OtherGameObjectComponent_propertyName (object s)
		{
			OtherGameObjectComponent_propertyName = s.ToString ();
		}

		#endregion Draw_Fields
	
	
		#region Files_WIO
		public const string monoBehaviourUsingBloc = "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\n";

		public static string MonoBehaviourHead (string className)
		{
			string r = monoBehaviourUsingBloc;

			r += "namespace ScriptsCreatedByDiamond\n{\n\t[ExecuteInEditMode]\n";

			r += "\tpublic class " + className + " : MonoBehaviour \n\t{\n";

			return r;
		}


		const string OtherGameObjectComponentGetSet_TmpGoName = "Tmp_go";

		bool OtherGameObjectComponentGetSetCreateTmpGo ()  
		{
			if (OtherGameObjectComponentGetSet_TmpGo == null)
				return false;

			//OtherGameObjectComponentGetSet_TmpGo.AddComponent <ScriptsCreatedByDiamond.TmpCsGetSetOtherGoCompProps> ();

			bool fileWritten = false;

			fileWritten = MonoBehaviourFile (TmpCsGetSetOtherGoCompProps);


			return fileWritten;
		}

		const string TmpCsGetSetOtherGoCompProps = "TmpCsGetSetOtherGoCompProps";

		void OtherGameObjectComponentGetSetTmpGoSendMessage ()
		{			
			if (OtherGameObjectComponentGetSet_TmpGo == null)
				return;

			OtherGameObjectComponentGetSet_TmpGo.SendMessage (callMeByMessage);

	
			GameObject.DestroyImmediate (OtherGameObjectComponentGetSet_TmpGo, false);

			//MonoBehaviourFileClearedVersion (TmpCsGetSetOtherGoCompProps);

			//oGoGsComp_prepared = false;
			oGoGsComp_PrepReady = false;
		}


		const string callMeByMessage = "CallMeByMessage";
		public string MonoBehaviourString (string className)
		{
			string r = MonoBehaviourHead (className);

			r += "\t\tvoid " + callMeByMessage + " ()\n\t\t{\n" +
				OtherGameObjectComponentGetSetString_editor () +
				"\t\t}\n";

			r += "\t}\n}\n";

			return r;
		}

		const string clearedVersionOf_TmpCsGetSetOtherGoCompProps = 
			"using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\nnamespace ScriptsCreatedByDiamond\n{\n\t[ExecuteInEditMode]\n\tpublic class TmpCsGetSetOtherGoCompProps : MonoBehaviour \n\t{\n\t\tvoid CallMeByMessage ()\n\t\t{\n\n\t\t}\n\t}\n}\n";


		string OtherGameObjectComponent_GetString (string diamondVarName)
		{
			string comp = "comp";

			return "\t\t\t" + diamondVarName + " = " + comp + "." + OtherGameObjectComponent_propertyName + ";\n";
		}
		string OtherGameObjectComponentGetSetString ()
		{
			string r = "";

			if (gameObjectValues [0] == null)
				return "";

			r += "\t\t\tGameObject go = GameObject.Find (\"" + gameObjectValues [0].name + "\");\n";
			r += "\t\t\tif (go == null)\n\t\t\t\treturn;\n\n";

			string comp = "comp";
			r += "\t\t\t" + OtherGameObjectComponent_componentTypeName + " " + comp + " = go.GetComponent" +
				" <" + OtherGameObjectComponent_componentTypeName + "> ();\n\n";


			switch (getOrSet)
			{
			case GetOrSet.get:
				switch (selectedProperty_propertyType)
				{
				case "Boolean":
					r += OtherGameObjectComponent_GetString ("boolValue");
					break;

				case "Color":
					r += OtherGameObjectComponent_GetString ("colorValue");
					break;

				case "Float":
					r += OtherGameObjectComponent_GetString ("floatValue");
					break;

				case "Integer":
					r += OtherGameObjectComponent_GetString ("intValue");
					break;

				case "Rect":
					r += OtherGameObjectComponent_GetString ("rectValue");
					break;

				case "String":
					r += OtherGameObjectComponent_GetString ("stringValue");
					break;

				case "Vector2":					
					r += OtherGameObjectComponent_GetString ("vector2Value");
					break;

				case "Vector3":
					r += OtherGameObjectComponent_GetString ("vector3Value");
					break;

				case "Vector4":
					r += OtherGameObjectComponent_GetString ("vector4Value");
					break;
				}
				break;

			case GetOrSet.set:
				switch (selectedProperty_propertyType)
				{
				case "Boolean":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "boolValues [0]" + ";\n";
					break;

				case "Color":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "colorValues [0]" + ";\n";
					break;

				case "Float":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "floatValues [0]" + ";\n";
					break;

				case "Integer":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "intValues [0]" + ";\n";
					break;

				case "Rect":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "rectValues [0]" + ";\n";
					break;

				case "String":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "stringValues [0]" + ";\n";
					break;

				case "Vector2":					
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "vector2Values [0]" + ";\n";
					break;

				case "Vector3":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "vector3Values [0]" + ";\n";
					break;

				case "Vector4":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + "vector4Values [0]" + ";\n";
					break;
				}
				break;
			}


			return r;
		}
		string OtherGameObjectComponentGetSetString_editor ()
		{
			string r = "";

			if (gameObjectValues [0] == null)
				return "";

			r += "\t\t\tGameObject go = GameObject.Find (\"" + gameObjectValues [0].name + "\");\n";
			r += "\t\t\tif (go == null)\n\t\t\t\treturn;\n\n";

			string comp = "comp";
			r += "\t\t\t" + OtherGameObjectComponent_componentTypeName + " " + comp + " = go.GetComponent" +
				" <" + OtherGameObjectComponent_componentTypeName + "> ();\n\n";


			switch (getOrSet)
			{
			case GetOrSet.get:
				break;

			case GetOrSet.set:
				switch (selectedProperty_propertyType)
				{
				case "Boolean":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + StringTreatment.FirstToLower (boolValues [0].ToString ()) + ";\n";
					break;
				
				case "Color":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + LogicNode.ColorToScriptWrite (colorValues [0]) + ";\n";
					break;
				
				case "Float":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + floatValues [0].ToString () + "f" + ";\n";				
					break;
				
				case "Integer":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + intValues [0].ToString () + ";\n";
					break;
				
				case "Rect":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + LogicNode.RectToScriptWrite (rectValues [0]) + ";\n";
					break;
				
				case "String":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + stringValues [0] + ";\n";
					break;
				
				case "Vector2":					
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + LogicNode.Vector2ToScriptWrite (vector2Values [0]) + ";\n";
					break;
				
				case "Vector3":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + LogicNode.Vector3ToScriptWrite (vector3Values [0]) + ";\n";
					break;
				
				case "Vector4":
					r += "\t\t\t" + comp + "." + OtherGameObjectComponent_propertyName + 
						" = " + LogicNode.Vector4ToScriptWrite (vector4Values [0]) + ";\n";
					break;
				}
				break;
			}


			return r;
		}

		public bool MonoBehaviourFile (string className)
		{
			try
			{
				string path = Application.dataPath + "/Mezanix/Diamond/Scripts/";

				path += className + ".cs";

				File.WriteAllText (path, MonoBehaviourString (className));
			}
			catch
			{
				return false;
			}

			return Auxiliaries.SaveAndRefreshAssetsForced ();
		}

		public bool MonoBehaviourFileClearedVersion (string className)
		{
			try
			{
				string path = Application.dataPath + "/Mezanix/Diamond/Scripts/";

				path += className + ".cs";

				File.WriteAllText (path, clearedVersionOf_TmpCsGetSetOtherGoCompProps);
			}
			catch
			{
				return false;
			}

			return Auxiliaries.SaveAndRefreshAssetsForced ();
		}
		#endregion Files_WIO
	
	
	


		#region zooming
		void Zooming ()
		{
			if (eGlobal.keyCode == KeyCode.AltGr || eGlobal.keyCode == KeyCode.LeftAlt || 
				eGlobal.keyCode == KeyCode.RightAlt)
			{
				if (eGlobal.type == EventType.KeyUp)
				{
					normalizedZoomMousePosition = 0f;
				}
			}
			if (logic.mouseMovedForZoom)
			{
				normalizedZoomMousePosition = 0f;
			}


			float speed = 0.05f;

			if (eGlobal.alt)
			{
				ZoomingAction (true, speed);
			}

			if (rect.Contains (eGlobal.mousePosition))
			{
				ZoomingAction (false, speed);
			}
		}

		void ZoomingAction (bool isGlobalZooming)
		{
			if (isGlobalZooming)
			{
				//ZoomingActionGlobalZooming ();
				//ZoomingActionGlobalZoomingNodesCenter ();
				//ZoomingActionGlobalZoomingNodesCenterMouse ();
				ZoomingActionGlobalHomogen ();

				return;
			}

			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
					zoom -= zoomIncr;
				else if (eGlobal.delta.y < 0f)
					zoom += zoomIncr;

				zoom = Mathf.Clamp (zoom, zoomMin, 1f);

			}
		}

		void ZoomingAction (bool isGlobalZooming, float speed)
		{
			float moveRectCorrection = 100f;

			if (isGlobalZooming)
			{
				//ZoomingActionGlobalZooming ();
				//ZoomingActionGlobalZoomingNodesCenter ();
				//ZoomingActionGlobalZoomingNodesCenterMouse ();
				//ZoomingActionGlobalHomogen ();

				//moveRectCorrection = 500f;
				//float moveRectRadialCorrection = 500f;
				//ZoomingActionGlobal (speed, moveRectCorrection, moveRectRadialCorrection);


				//moveRectCorrection = -100f;
				float speedFactor = 0.4f;
				ZoomingActionLocal (speed*speedFactor);
				ZoomingGlobalNormalized ();
				return;
			}

			moveRectCorrection = 100f;
			ZoomingActionLocal (speed, moveRectCorrection);
		}

		public void SetNormalizedPosition ()
		{
			if (logic == null)
				return;

			Vector2 normalizedZoomedPosition = Mezanix.Aux.Rectan.NormalizedPosition (
				logic.zoomedEditingLogicRect, rect.position);

			Vector2 AbsoluteOnZoomedPosition = Mezanix.Aux.Rectan.AbsolutePosition (
				logic.editLogicRectGlobal, normalizedZoomedPosition);

			normalizedPosition = Mezanix.Aux.Rectan.NormalizedPosition (
				logic.editLogicRectGlobal, AbsoluteOnZoomedPosition);

			normalizedZoomMousePosition = 0f;
		}



		public Vector2 normalizedPosition;
		float normalizedZoomMousePosition = 0f;
		const float normalizedZoomMousePositionSpeed = 20f;
		void ZoomingGlobalNormalized ()
		{
			//normalizedZoomMousePosition = 0f;

			Rect zomedLogicRect = Mezanix.Aux.Rectan.Zoom (logic.zoomedEditingLogicRect, zoom, true);

			Vector2 baseToMouse = logic.editLogicRectGlobal.center;
			Vector2 toMouse = eGlobal.mousePosition - baseToMouse;
			Vector2 toMouseU = toMouse.normalized;

			//Drawer.DrawStraightBezier (logic.nodesRectsCenter,
			//	logic.nodesRectsCenter + toMouse, Color.green, 3f);
			//Drawer.DrawStraightBezier (baseToMouse, baseToMouse + toMouseU*normalizedZoomMousePosition, Color.red, 3f);

			//zomedLogicRect.position = logic.zoomedEditingLogicRect.position; // - toMouse*5f;

			//Drawer.DrawRectBorders (zomedLogicRect, Color.green, 3f);



			if (eGlobal.type != EventType.ScrollWheel)
			{				
				return;
			}

			if (eGlobal.delta.y == 0f)
			{
				return;
			}
			if (Mezanix.Aux.Mathm.InRange (logic.zoom, Logic.zoomMin + 2f*Logic.zoomSpeed, Logic.zoomMax))
			{
				normalizedZoomMousePosition += normalizedZoomMousePositionSpeed*Mathf.Sign (eGlobal.delta.y);
			}

			Vector2 zp = Mezanix.Aux.Rectan.AbsolutePosition (
				zomedLogicRect,
				normalizedPosition);

			rect.position = zp + toMouseU*normalizedZoomMousePosition;

			//rect.position = zp;
		}

		void ZoomingActionLocal (float speed, float moveRectCorrection)
		{
			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					zoom -= speed;


					if (zoom < zoomMin)
					{
						zoom = zoomMin;
					}
					else
					{
						rect.position += speed*Vector2.one*moveRectCorrection;
					}
				}
				else if (eGlobal.delta.y < 0f)
				{
					zoom += speed;

					if (zoom > 1f)
					{
						zoom = 1f;
					}
					else
					{
						rect.position -= speed*Vector2.one*moveRectCorrection;
					}
				}
			}
			
		}

		void ZoomingActionLocal (float speed)
		{
			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					zoom -= speed;

					if (zoom < zoomMin)
					{
						zoom = zoomMin;
					}
				}
				else if (eGlobal.delta.y < 0f)
				{
					zoom += speed;

					if (zoom > 1f)
					{
						zoom = 1f;
					}
				}
			}
		}

		void ZoomingActionGlobal (float speed, float moveRectCorrection, float moveRectRadialCorrection)
		{
			if (eGlobal.type == EventType.ScrollWheel)
			{
				Vector2 toMouse_U = (eGlobal.mousePosition - logic.nodesRectsCenter).normalized;
			
				Vector2 toCenter_U = (logic.nodesRectsCenter - rect.position).normalized;
			
				if (eGlobal.delta.y > 0f)
				{
					zoom -= speed;
			
			
					if (zoom < zoomMin)
					{
						zoom = zoomMin;
					}
					else
					{
						rect.position += speed*toMouse_U*moveRectCorrection;
			
						rect.position += speed*toCenter_U*moveRectRadialCorrection;
					}
				}
				else if (eGlobal.delta.y < 0f)
				{
					zoom += speed;
			
					if (zoom > 1f)
					{
						zoom = 1f;
					}
					else
					{
						rect.position -= speed*toMouse_U*moveRectCorrection;
			
						rect.position -= speed*toCenter_U*moveRectRadialCorrection;
					}
				}
			}

		}



		void ZoomingActionGlobalZooming ()
		{
			Vector2 toMouse = eGlobal.mousePosition - rect.center;

			float toMouse_D = toMouse.magnitude;

			Vector2 toMouse_U = toMouse.normalized;

			float distIncr = 15f;

			float distIncrDown = distIncr;

			float distIncrUp = distIncr;

			float toMouseFactor = 2.5f;

			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					if (zoom == zoomMin)
						return;

					zoom -= zoomIncr;

					toMouse_D -= distIncrDown * toMouseFactor;
				}
				else if (eGlobal.delta.y < 0f)
				{
					if (zoom == 1f)
						return;

					zoom += zoomIncr;

					toMouse_D += distIncrUp * toMouseFactor;
				}

				zoom = Mathf.Clamp (zoom, zoomMin, 1f);


				//if (zoom == 1f)
				//	return;

				//toMouse_D = Mathf.Clamp (toMouse_D, 50f, 1000f);

				rect = new Rect (eGlobal.mousePosition - toMouse_U*toMouse_D - rect.size*0.5f, rect.size);
			}
		}

		void ZoomingActionGlobalZoomingNodesCenter ()
		{
			Vector2 nodesCenter = logic.nodesRectsCenter;

			Vector2 tocenter = nodesCenter - rect.center;

			float toCenter_D = tocenter.magnitude;

			Vector2 toCenter_U = tocenter.normalized;

			float distIncr = 10f;

			float distIncrDown = distIncr;

			float distIncrUp = distIncr;

			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					if (zoom == zoomMin)
						return;

					zoom -= zoomIncr;

					toCenter_D += distIncrDown;
				}
				else if (eGlobal.delta.y < 0f)
				{
					if (zoom == 1f)
						return;

					zoom += zoomIncr;

					toCenter_D -= distIncrUp;
				}

				zoom = Mathf.Clamp (zoom, zoomMin, 1f);


				//if (zoom == 1f)
				//	return;

				//toMouse_D = Mathf.Clamp (toMouse_D, 50f, 1000f);
				//Debug.Log (toCenter_U*toCenter_D);
				rect = new Rect (nodesCenter - toCenter_U*toCenter_D - rect.size*0.5f, rect.size);
			}
		}

		void ZoomingActionGlobalZoomingNodesCenterMouse ()
		{
			Vector2 nodesCenter = (logic.nodesRectsCenter + 100f*eGlobal.mousePosition);

			Vector2 tocenter = nodesCenter - rect.center;

			float toCenter_D = tocenter.magnitude;

			Vector2 toCenter_U = tocenter.normalized;


			float distIncr = 10f;

			float distIncrDown = distIncr;

			float distIncrUp = distIncr;

			//float toMouseFactor = 0.0001f;

			//Vector2 centerToMouse = eGlobal.mousePosition - nodesCenter;
			//
			//float centerToMouse_D = centerToMouse.magnitude;
			//
			//Vector2 centerToMouse_U = centerToMouse.normalized;


			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					if (zoom == zoomMin)
						return;

					zoom -= zoomIncr;

					toCenter_D += distIncrDown;

					//centerToMouse_D -= distIncrDown * toMouseFactor;
				}
				else if (eGlobal.delta.y < 0f)
				{
					if (zoom == 1f)
						return;

					zoom += zoomIncr;

					toCenter_D -= distIncrUp;

					//centerToMouse_D += distIncrDown * toMouseFactor;
				}

				zoom = Mathf.Clamp (zoom, zoomMin, 1f);


				//if (zoom == 1f)
				//	return;

				//toMouse_D = Mathf.Clamp (toMouse_D, 50f, 1000f);


				//Debug.Log (centerToMouse_U * centerToMouse_D);
				rect = new Rect (nodesCenter - toCenter_U*toCenter_D - rect.size*0.5f, rect.size);
			}
		}
	
		void ZoomingActionGlobalHomogen ()
		{
			Rect big = logic.editLogicRectGlobal;

			Rect inBigRatio = RectOperations.AbsoluteToRatio (big, rect);

			//float incr = 0.01f;

			if (eGlobal.type == EventType.ScrollWheel)
			{
				if (eGlobal.delta.y > 0f)
				{
					//inBigRatio = new Rect (;
				}
				else if (eGlobal.delta.y < 0f)
				{
				}
			}

			rect = RectOperations.RatioToAbsolute (big, inBigRatio);

		}
		#endregion zooming
	
	
		/// <summary>
		/// NodeUpdate.
		/// The logicNode update method.
		/// Executed every OnGUI frame.
		/// </summary>
		/// <param name="e">E.</param>
		/// <param name="editLogicRect">Edit logic rect.</param>
		#region update and draw the logicNode
		public void NodeUpdate (Event e, Rect editLogicRect)
		{
			ScriptsCreatedByDiamond.IdentifiedObjectsActions.CreateGameObjectHolder_CreateIdentifiedObjects ();

			ScriptsCreatedByDiamond.MezanixDiamondEvents.CreateGameObjectHolder ();

			ScriptsCreatedByDiamond.MezanixDiamondDataTransfer.CreateGameObjectHolder ();




			EditorGUI.BeginChangeCheck ();




			string guistyle = Skins.node;

			if (selectionState == SelectionState.selected)
			{
				guistyle = Skins.nodeSelected;

				//if (logic != null)
				//	logic.logicNodeSelected = this;
			}

			//if (zoom >= 1f)
			//	rectPosIni = rect.position;
			//
			//Vector2 rectPos = rectPosIni + new Vector2 ((-(zoom-1f)/(1f-zoomMin))*rect.width*0.5f, 
			//	(-(zoom-1f)/(1f-zoomMin))*rect.height*0.5f);
			//
			//rect = new Rect(rectPos, rect.size);

			//if ( ! Diamond.testing)
			//{
				GUI.Box (RectOperations.Offset (rect, new Vector2 (-4f, 8f)), "", GetGuiStyle (Skins.nodeBG));
				GUI.Box (rect, "", Skins.guiSkin.GetStyle (guistyle));
				Skins.DrawSeparator (new Rect (rect.x, rect.y + rect.height, rect.width,
					Skins.separatorThickness));
			//}
			//else
			//{
			//	DrawRoundedRect ();
			//}


			EventProcess (e, editLogicRect);

			//editLogicRectGlobal = editLogicRect;

			DrawElements ();

			ConnectWithProjectVariables ();


			CheckNamesConflict_Update ();

			//if (callGetWriteScriptName)
			//{				
			//	GetScriptNameBox ();
			//}


			if (EditorGUI.EndChangeCheck ()) 
			{
				Diamond.changedLogicNode = logic.nodes.IndexOf (this);
				Diamond.changedLogic = logic.node.logics.IndexOf (logic);
				Diamond.changedNode = logic.node.graph.nodes.IndexOf (logic.node);
			}
			//else
			//{
			//	Diamond.changedLogicNode = -1;
			//	Diamond.changedLogic = -1;
			//	Diamond.changedNode = -1;
			//}


			RemoveNode (e);
		}
		void DrawRoundedRect ()
		{
			float shift = 0f; //27
			string guistyle = Skins.node;

			if (selectionState == SelectionState.selected)
			{
				guistyle = Skins.nodeSelected;

				//if (logic != null)
				//	logic.logicNodeSelected = this;
			}
			GUI.Box (new Rect (rect.position + new Vector2 (0f, 0.5f*shift), new Vector2 (rect.width, rect.height-shift)),
				"", Skins.guiSkin.GetStyle (guistyle));
		}
		#endregion update and draw the logicNode


		/// <summary>
		/// Draws the elements.
		/// Draw all the logicNode elements, fields, inputs, outputs, buttons
		/// </summary>
		void DrawElements ()
		{	
			tanLength = 30f;

			linkWidth = 4f;

			nearLinkExtremity = 40f;

			rectListValues = new List<Rect>[2]; rectListValues [0] = rectListValues [1] = new List<Rect> ();

			vector4ListValues = new List<Vector4>[2]; vector4ListValues [0] = vector4ListValues [1] = new List<Vector4> ();

			shaderListValues = new List<Shader>[2]; shaderListValues [0] = shaderListValues [1] = new List<Shader> ();

			boolsListValues = new List<bool>[2]; boolsListValues [0] = boolsListValues [1] = new List<bool> ();

			colorsListValues = new List<Color>[2]; colorsListValues [0] = colorsListValues [1] = new List<Color>();

			floatsListValues = new List<float>[2]; floatsListValues [0] = floatsListValues [1] = new List<float> ();

			intsListValues = new List<int>[2]; intsListValues [0] = intsListValues [1] = new List<int> ();

			stringsListValues = new List<string>[2]; stringsListValues [0] = stringsListValues [1] = new List<string> ();

			gameObjectsListValues = new List<GameObject>[2]; gameObjectsListValues [0] = gameObjectsListValues [1] = new List<GameObject>();

			vector2ListValues = new List<Vector2>[2]; vector2ListValues [0] = vector2ListValues [1] = new List<Vector2> ();

			vector3ListValues = new List<Vector3>[2]; vector3ListValues [0] = vector3ListValues [1] = new List<Vector3> ();

			materialsListValues = new List<Material>[2]; materialsListValues [0] = materialsListValues [1] = new List<Material> ();

			texture2DListValues = new List<Texture2D>[2]; texture2DListValues [0] = texture2DListValues [1] = new List<Texture2D> ();


			InitInputsOutputs ();

			InitDataFlowControlEnabled ();


			Nv_InitInputsOutputs ();

			Nv_InitDataFlowControlEnabled ();



			if (alwaysDoIt)
				doIT = true;
			if ( ! alwaysDoIt)
				doIT = false;

			cameraDirectionalLightMessageWritten = false;





			SetFieldsContentsAndAlignmentsRatios ();

			fieldIDGlobal = new float[] {0f, 0f};

			fieldsCountGlobal = new float[] {1f, 1f};

			//inOutAdressSeparator = '.';

			//inAdressSignature = "in";

			//outAdressSignature = "out";

			//linkColor = ColorsArithmetic.RGB_255_To_Normalized (31f, 208f, 235f, 1f);


			//CheckGotoState ();





			DrawNameField ();



			DrawLogicTypeEnum ();

			AdaptOnLogicType ();

			//CallPartialExtentions ();

			ManageAttachedGameObject ();


			if (selectionState == SelectionState.selected)
			{
				DrawMessageBoxDownLeft (
					new string[]{deleteHotkeyMessage,});
			}

			IncrementOutIndexForHotKeyLinking ();
			IncrementInputIndexForHotKeyLinking ();

			IncrementHotKeysInputOptionsMenu ();


			DrawExecutionOrder ();


			Zooming ();


		}




		#region getCodeFrom
		public string GetCodeFrom (CsLogicNodeWhere csLogicNodeWhere)
		{
			string input_globalVariables;
			string input_constructor;
			string input_methodsCall;
			string input_methodsDecl;

			string r = "";
		


			switch (logicType)
			{
			//case LogicType.UseHighLevelNodes:
			//	r = GetCodeFrom_HighLevelNode (GetCodeFrom_HighLevelNode (), csLogicNodeWhere);
			//	break;

			case LogicType.GetSetOtherGameObjectsScriptsVariables:
				r = MonoVarAccess_Script (csLogicNodeWhere);
				break;

			//case LogicType.test:
			//	r = Test_Code (csLogicNodeWhere);
			//	break;

			//case LogicType.GetSetOtherGameObjectsComponentsVariables:
			//	r = GetCodeFrom_ComputeClosed (OtherGameObjectComponent_GetCodeFrom (), csLogicNodeWhere);
			//	break;

			case LogicType.Ads:
				r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
					computeAdsType.ToString (),
					EnumInputComputeOutput_Ads (), 
					ExprWs.UMcall.computeAds);
				break;

			case LogicType.unityInputClassAndCrossPlatform:
				r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
					computeUnityInputClassAndCrossPlatformType.ToString (),
					EnumInputComputeOutput_unityInpuClassAndCrossPlatform (), 
					ExprWs.UMcall.computeUnityInputClassAndCrossPlatform);

				break;

			case LogicType.mouseInput:
				r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
					computeMouseInputType.ToString (), EnumInputComputeOutput_MouseInput (), 
					ExprWs.UMcall.computeMouseInput);

				break;

			case LogicType.computeOrOperation:
				switch (variableType)
				{
				case VariableType.componentUIToggle:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeUiToggleType.ToString (), EnumInputComputeOutput_UiToggle (), 
						ExprWs.UMcall.computeUiToggle);
					break;

				case VariableType.componentUIInputField:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeUiInputFieldType.ToString (), EnumInputComputeOutput_UiInputField (), 
						ExprWs.UMcall.computeUiInputField);
					break;

				case VariableType.componentLight:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeLightType.ToString (), EnumInputComputeOutput_Light (), 
						ExprWs.UMcall.computeLight);
					break;

				case VariableType.componentCircleCollider2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeCircleCollider2DType.ToString (), EnumInputComputeOutput_CircleCollider_2D (), 
						ExprWs.UMcall.computeCollider2D);
					break;

				case VariableType.componentBoxCollider2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeBoxCollider2DType.ToString (), EnumInputComputeOutput_BoxCollider_2D (), 
						ExprWs.UMcall.computeCollider2D);
					break;


				case VariableType.componentMeshCollider:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeMeshColliderType.ToString (), EnumInputComputeOutput_MeshCollider (), 
						ExprWs.UMcall.computeCollider);
					break;

				case VariableType.componentCapsuleCollider:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeCapsuleColliderType.ToString (), EnumInputComputeOutput_CapsuleCollider (), 
						ExprWs.UMcall.computeCollider);
					break;

				case VariableType.componentSphereCollider:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeSphereColliderType.ToString (), EnumInputComputeOutput_SphereCollider (), 
						ExprWs.UMcall.computeCollider);
					break;


				case VariableType.componentBoxCollider:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeBoxColliderType.ToString (), EnumInputComputeOutput_BoxCollider (), 
						ExprWs.UMcall.computeCollider);
					break;



				case VariableType.componentLineRenderer:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeLineRendererType.ToString (), EnumInputComputeOutput_LineRenderer (), 
						ExprWs.UMcall.computeRenderer);
					break;

				case VariableType.componentSpriteRenderer:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeSpriteRendererType.ToString (), EnumInputComputeOutput_SpriteRenderer (), 
						ExprWs.UMcall.computeRenderer);
					break;



				case VariableType.componentAudioSource:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeAudioSourceType.ToString (), EnumInputComputeOutput_AudioSource (), 
						ExprWs.UMcall.computeAudioSource);
					break;

				case VariableType.componentUIButton:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeUiButtonType.ToString (), EnumInputComputeOutput_UiButton (), 
						ExprWs.UMcall.computeUiButton);
					break;

				case VariableType.componentUIImage:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeUiImageType.ToString (), EnumInputComputeOutput_UiImage (), 
						ExprWs.UMcall.computeUiImage);
					break;

				case VariableType.componentUIUnityText:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeUnityTextType.ToString (), EnumInputComputeOutput_UnityText (), 
						ExprWs.UMcall.computeUnityText);
					break;

				case VariableType.Texture2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeTexture2DType.ToString (), EnumInputComputeOutput_Texture2D (), ExprWs.UMcall.computeTexture2D);
					break;

				case VariableType.Shader:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeShaderType.ToString (), EnumInputComputeOutput_Shader (), ExprWs.UMcall.computeShader);
					break;
					//getcodefromdiff
				case VariableType.Bool:
					r = GetCodeFrom_computeOrOperation_Bool_ex ( csLogicNodeWhere);
					break;

				case VariableType.boolsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeBoolListType.ToString (), EnumInputComputeOutput_BoolsList (), ExprWs.UMcall.computeBoolList);
					break;
					//getcodefromdiff
				case VariableType.Camera:
					r = GetCodeFrom_computeOrOperation_Camera ( csLogicNodeWhere);
					break;


				case VariableType.Material:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeMaterialType.ToString (), EnumInputComputeOutput_Material (), ExprWs.UMcall.computeMaterial);
					break;


					//
				case VariableType.GameObjectList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeGameObjectListType.ToString (), EnumInputComputeOutput_GameObjectList (), ExprWs.UMcall.computeGameObjectList);
					break;

				case VariableType.materialsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeMaterialListType.ToString (), EnumInputComputeOutput_MaterialList (), ExprWs.UMcall.computeMaterialList);
					break;

				case VariableType.texture2DList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeTexture2DListType.ToString (), EnumInputComputeOutput_Texture2DList (), ExprWs.UMcall.computeTexture2DList);
					break;

				case VariableType.shadersList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeShaderListType.ToString (), EnumInputComputeOutput_ShaderList (), ExprWs.UMcall.computeShaderList);
					break;

				case VariableType.Color:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeColorType.ToString (), EnumInputComputeOutput_Color (), 
						ExprWs.UMcall.computeColor);
					break;

				case VariableType.colorsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeColorListType.ToString (), EnumInputComputeOutput_ColorList (), 
						ExprWs.UMcall.computeColorList);
					break;


				case VariableType.floatsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeFloatsListType.ToString (), EnumInputComputeOutput_FloatsList (), ExprWs.UMcall.computeFloatList);
					break;

				case VariableType.intsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeIntListType.ToString (), EnumInputComputeOutput_IntsList (), ExprWs.UMcall.computeInttList);
					break;

				case VariableType.vector2List:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector2ListType.ToString (), EnumInputComputeOutput_Vector2List (), ExprWs.UMcall.computeVector2tList);
					break;

				case VariableType.vector3List:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector3ListType.ToString (), EnumInputComputeOutput_Vector3List (), ExprWs.UMcall.computeVector3tList);
					break;

				case VariableType.vector4List:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector4ListType.ToString (), EnumInputComputeOutput_Vector4List (), ExprWs.UMcall.computeVector4tList);
					break;

				case VariableType.rectsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRectListType.ToString (), EnumInputComputeOutput_RectList (), ExprWs.UMcall.computeRectList);
					break;

				case VariableType.stringsList:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeStringsListType.ToString (), EnumInputComputeOutput_StringList (), ExprWs.UMcall.computeStringtList);
					break;

				case VariableType.componentCollider:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeColliderType.ToString (), EnumInputComputeOutput_Collider (), ExprWs.UMcall.computeCollider);
					break;

				case VariableType.componentCollider2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeCollider2DType.ToString (), EnumInputComputeOutput_Collider_2D (), ExprWs.UMcall.computeCollider2D);
					break;

				case VariableType.componentNavMeshAgent:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeNavMeshAgentType.ToString (), EnumInputComputeOutput_NavMeshAgent (), ExprWs.UMcall.computeNavMeshAgent);
					break;

				case VariableType.componentParticleSystem:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeParticleSystemType.ToString (), EnumInputComputeOutput_ParticleSystem (), ExprWs.UMcall.computeParticleSystem);
					break;

				case VariableType.componentRenderer:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRendererType.ToString (), EnumInputComputeOutput_Renderer (), ExprWs.UMcall.computeRenderer);
					break;

				case VariableType.componentRigidBody:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRigidBodyType.ToString (), EnumInputComputeOutput_Rigidbody (), ExprWs.UMcall.computeRigidbody);
					break;

				case VariableType.componentRigidBody2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRigidBody2DType.ToString (), EnumInputComputeOutput_Rigidbody2D (), ExprWs.UMcall.computeRigidbody2D);
					break;

				case VariableType.componentTransform:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeTransformType.ToString (), EnumInputComputeOutput_Transform (), ExprWs.UMcall.computeTransform);
					break;

				case VariableType.Float:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeFloatType.ToString (), EnumInputComputeOutput_Float (), ExprWs.UMcall.computeFloat);
					break;

				case VariableType.GameObject:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeGameObjectType.ToString (), EnumInputComputeOutput_GameObject (), ExprWs.UMcall.computeGameObject);
					break;

				case VariableType.Int:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeIntType.ToString (), EnumInputComputeOutput_Int (), ExprWs.UMcall.computeInt);
					break;

				case VariableType.Ray:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRayType.ToString (), EnumInputComputeOutput_Ray (), ExprWs.UMcall.computeRay);
					break;

				case VariableType.Ray2D:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRay2DType.ToString (), EnumInputComputeOutput_Ray2D (), ExprWs.UMcall.computeRay2D);
					break;


				case VariableType.String:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeStringType.ToString (), EnumInputComputeOutput_String (), ExprWs.UMcall.computeString);
					break;


				case VariableType.Vector2:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector2Type.ToString (), EnumInputComputeOutput_Vector2 (), ExprWs.UMcall.computeVector2);
					break;

				case VariableType.Vector3:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector3Type.ToString (), EnumInputComputeOutput_Vector3 (), ExprWs.UMcall.computeVector3);
					break;

				case VariableType.Vector4:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeVector4Type.ToString (), EnumInputComputeOutput_Vector4 (), ExprWs.UMcall.computeVector4);
					break;

				case VariableType.rect:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeRectType.ToString (), EnumInputComputeOutput_Rect (), ExprWs.UMcall.computeRect);
					break;

				case VariableType.Matrix4x4:
					r = GetCodeFrom_computeOrOperation ( csLogicNodeWhere, 
						computeMatrix44Type.ToString (), EnumInputComputeOutput_Matrix44 (), ExprWs.UMcall.computeMatrix44);
					break;
				}

				break;



			case LogicType.input:

				input_globalVariables = "\t\tpublic KeyCode keyCode;\n\n\t\tpublic enum KeyCodeTriggerWhen\n\t\t{\n\t\t\tdown,\n\n\t\t\thold,\n\n\t\t\tup\n\t\t}\n\n\t\tpublic enum MouseInputTriggerWhen\n\t\t{\n\t\t\tdown,\n\n\t\t\thold,\n\n\t\t\tdrag,\n\n\t\t\tup\n\t\t}\n\n\t\tpublic KeyCodeTriggerWhen keyCodeTriggerWhen;\n\n\t\tpublic MouseInputTriggerWhen mouseInputTriggerWhen;\n\n\t\tpublic bool boolValue;\n\n\t\tVector3 mousePosition;" + "\n\n";

				input_constructor = "\t\t\t" + "keyCode" + " = " 
					+ "KeyCode" + "." + keyCode.ToString () + ";" + "\n\n"
					+ "\t\t\t" 
					+ "keyCodeTriggerWhen" + " = " 
					+ "KeyCodeTriggerWhen" + "." + keyCodeTriggerWhen.ToString () + ";" + "\n\n"
					+ "\t\t\t" 
					+ "mouseInputTriggerWhen" + " = " 
					+ "MouseInputTriggerWhen" + "." + mouseInputTriggerWhen.ToString () + ";" + "\n\n"
					+ "\t\t\tmousePosition = new Vector3 ();" + "\n";

				input_methodsCall = "\t\t\tApplyInput ();" + "\n";

				input_methodsDecl = "\t\tvoid ApplyInput ()\n\t\t{\n\t\t\tboolValue = false;\n\n\t\t\tif (keyCode == KeyCode.Mouse0 ||\n\t\t\t\tkeyCode == KeyCode.Mouse1 ||\n\t\t\t\tkeyCode == KeyCode.Mouse2 ||\n\t\t\t\tkeyCode == KeyCode.Mouse3 ||\n\t\t\t\tkeyCode == KeyCode.Mouse4 ||\n\t\t\t\tkeyCode == KeyCode.Mouse5 ||\n\t\t\t\tkeyCode == KeyCode.Mouse6)\n\t\t\t{\n\t\t\t\tswitch (mouseInputTriggerWhen)\n\t\t\t\t{\n\t\t\t\tcase MouseInputTriggerWhen.down:\n\t\t\t\t\tif (Input.GetKeyDown (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseInputTriggerWhen.drag:\n\t\t\t\t\tif (Input.GetKey (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tif (MouseMoved ())\n\t\t\t\t\t\t{\n\t\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t\t}\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseInputTriggerWhen.hold:\n\t\t\t\t\tif (Input.GetKey (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase MouseInputTriggerWhen.up:\n\t\t\t\t\tif (Input.GetKeyUp (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\t\t\t\t}\n\t\t\t}\n\t\t\telse\n\t\t\t{\n\t\t\t\tswitch (keyCodeTriggerWhen)\n\t\t\t\t{\n\t\t\t\tcase KeyCodeTriggerWhen.down:\n\t\t\t\t\tif (Input.GetKeyDown (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase KeyCodeTriggerWhen.hold:\n\t\t\t\t\tif (Input.GetKey (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase KeyCodeTriggerWhen.up:\n\t\t\t\t\tif (Input.GetKeyUp (keyCode))\n\t\t\t\t\t{\n\t\t\t\t\t\tboolValue = true;\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\t\t\t\t}\n\t\t\t}\n\t\t}\n\n\t\tbool MouseMoved ()\n\t\t{\n\t\t\tbool retVal = false;\n\n\t\t\tif (Input.mousePosition != mousePosition)\n\t\t\t{\n\t\t\t\tretVal = true;\n\n\t\t\t\tmousePosition = Input.mousePosition;\n\t\t\t}\n\n\t\t\treturn retVal;\n\t\t}" + "\n\n";

				switch (csLogicNodeWhere)
				{
				case CsLogicNodeWhere.constructor:
					r = input_constructor;
					break;

				case CsLogicNodeWhere.globalVariables:
					r = input_globalVariables;
					break;

				case CsLogicNodeWhere.logicNodeUpdate:
					r = input_methodsCall;
					break;

				case CsLogicNodeWhere.methods:
					r = input_methodsDecl;
					break;
				}
				break;



			case LogicType.timeOperation:
				r = GetCodeFrom_TimeOperation (csLogicNodeWhere);
				break;
			}

			return r;
		}
		#endregion getCodeFrom
	}
}
