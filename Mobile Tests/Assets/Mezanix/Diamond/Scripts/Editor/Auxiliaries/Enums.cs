
namespace Mezanix.Diamond
{
	public enum Diamond_TP
	{
		package,

		removeExtensions,

		removeGameDesignAndItsNodesExtensions,

		loadAnExample,

		resetGenerationPaths,

		logicUpdate,

		logicTypeTest,

		testing,
	}
	public enum Diamond_TP_test
	{
		_5_6_1f1_fresh,

		last_fresh,

		_5_6_1f1_onTopLastDiamond,

		last_fresh_onTopLastDiamond,
	}

	public class DontForgetToDo
	{
		public const string do_Enum_bla_bla_for = "";

		public const string testsToDo = "";

		public const string particleSystem_0 = "Create psMain Enum_bla_bla";

		public const string rayCast_and_2D = "reset raycastinfo before compute";
	}

	public class UpgradingTo_5_6_1f1
	{
		public string [] Upgrades = new string[]
		{
			"\t\t\tcase ComputeCameraType.SetStereoViewMatrices:\n" +
			"\t\t\t\tcam.SetStereoViewMatrices (m44Value_Input_entier [0], m44Value_Input_entier [1]);\n" +
			"\t\t\t\tcam.SetStereoViewMatrix (eye, matrix);",

			"\t\t\tcase ComputeCameraType.SetStereoProjectionMatrices:\n" +
			"\t\t\t\tcam.SetStereoProjectionMatrices (m44Value_Input_entier [0], m44Value_Input_entier [1]);\n" +
			"\t\t\t\tcam.SetStereoProjectionMatrix (eye, matrix:);",

			"ComputeRendererType.setLineRendererColors",



			"IsTouchingLayers",

			"setOpaqueSortMode",

			"setEventMask",

			"setCullingMask",

			"getOpaqueSortMode",

			"getEventMask",

			"getCullingMask",

			"setLayer",

			"getLayer",

			"IsTouchingAnyColliderInThisLayerMask",
		};
	}

	public class Enums 
	{
		#region right click menu item names

		public static readonly string addState = "Add State";

		public static readonly string addLogicNode = "Add Logic Node";
		public const string add_Terra_LogicNode = "Extensions/" + "Add terra Logic Node";
		public const string add_Wood_LogicNode = "Extensions/Materials/" + "Add wood Logic Node";

		public static readonly string addProjectVariable = "Add Project Variable";
		public static readonly string addProjectVariable_bool = "Bool";
		public static readonly string addProjectVariable_float = "Float";
		public static readonly string addProjectVariable_int = "Int";
		public static readonly string addProjectVariable_string = "String";
		public static readonly string addProjectVariable_vector2 = "Vector2";
		public static readonly string addProjectVariable_vector3 = "Vector3";
		public static readonly string addProjectVariable_vector4 = "Vector4";
		public static readonly string addProjectVariable_gameObject = "Game Object";

		#endregion right click menu item names






		#region GUI Control Names

		public static readonly  string nodeInputField = "nodeInputField";

		#endregion GUI Control Names

			
		//public const string DiamondIdentifiedGameObjects = "DiamondIdentifiedGameObjects";

		public static readonly string scriptsCreatedByDiamond = "ScriptsCreatedByDiamond";

		public static readonly string virginWorkSpaceTitle = "To Create Or Load Graph\nClick The New Or Load Button\n(At The Top Left)";

		//public static string monoBehaviourScriptFirstContent = "using UnityEngine;\nusing System.Collections;\n\npublic class ByChocolateScript : MonoBehaviour {\n\n\t// Use this for initialization\n\tvoid Start () {\n\t\n\t}\n\t\n\t// Update is called once per frame\n\tvoid Update () {\n\t\n\t}\n}";
	

		public static readonly string invalidNameDialog = "Please, enter a valid name:\n" +
		"No empty value.\n" +
		"No numeric character at the begining.\n" +
		"No spetial characters.";




		#region nv IDs
		public const string nv_int_0 = "nv_int_0";
		public const string nv_int_1 = "nv_int_1";
		public const string nv_int_2 = "nv_int_2";
		public const string nv_int_3 = "nv_int_3";
		public const string nv_int_4 = "nv_int_4";
		public const string nv_int_5 = "nv_int_5";
		public const string nv_int_6 = "nv_int_6";
		public const string nv_int_7 = "nv_int_7";
		public const string nv_int_8 = "nv_int_8";
		public const string nv_int_9 = "nv_int_9";

		public const string nv_float_0 = "nv_float_0";
		public const string nv_float_1 = "nv_float_1";
		public const string nv_float_2 = "nv_float_2";
		public const string nv_float_3 = "nv_float_3";
		public const string nv_float_4 = "nv_float_4";
		public const string nv_float_5 = "nv_float_5";
		public const string nv_float_6 = "nv_float_6";
		public const string nv_float_7 = "nv_float_7";
		public const string nv_float_8 = "nv_float_8";
		public const string nv_float_9 = "nv_float_9";

		public const string nv_Vector2_0 = "nv_Vector2_0";
		public const string nv_Vector2_1 = "nv_Vector2_1";
		public const string nv_Vector2_2 = "nv_Vector2_2";
		public const string nv_Vector2_3 = "nv_Vector2_3";
		public const string nv_Vector2_4 = "nv_Vector2_4";
		public const string nv_Vector2_5 = "nv_Vector2_5";
		public const string nv_Vector2_6 = "nv_Vector2_6";
		public const string nv_Vector2_7 = "nv_Vector2_7";
		public const string nv_Vector2_8 = "nv_Vector2_8";
		public const string nv_Vector2_9 = "nv_Vector2_9";

		public const string nv_Vector3_0 = "nv_Vector3_0";
		public const string nv_Vector3_1 = "nv_Vector3_1";
		public const string nv_Vector3_2 = "nv_Vector3_2";
		public const string nv_Vector3_3 = "nv_Vector3_3";
		public const string nv_Vector3_4 = "nv_Vector3_4";
		public const string nv_Vector3_5 = "nv_Vector3_5";
		public const string nv_Vector3_6 = "nv_Vector3_6";
		public const string nv_Vector3_7 = "nv_Vector3_7";
		public const string nv_Vector3_8 = "nv_Vector3_8";
		public const string nv_Vector3_9 = "nv_Vector3_9";

		public const string nv_Vector4_0 = "nv_Vector4_0";
		public const string nv_Vector4_1 = "nv_Vector4_1";
		public const string nv_Vector4_2 = "nv_Vector4_2";
		public const string nv_Vector4_3 = "nv_Vector4_3";
		public const string nv_Vector4_4 = "nv_Vector4_4";
		public const string nv_Vector4_5 = "nv_Vector4_5";
		public const string nv_Vector4_6 = "nv_Vector4_6";
		public const string nv_Vector4_7 = "nv_Vector4_7";
		public const string nv_Vector4_8 = "nv_Vector4_8";
		public const string nv_Vector4_9 = "nv_Vector4_9";

		public const string nv_Rect_0 = "nv_Rect_0";
		public const string nv_Rect_1 = "nv_Rect_1";
		public const string nv_Rect_2 = "nv_Rect_2";
		public const string nv_Rect_3 = "nv_Rect_3";
		public const string nv_Rect_4 = "nv_Rect_4";
		public const string nv_Rect_5 = "nv_Rect_5";
		public const string nv_Rect_6 = "nv_Rect_6";
		public const string nv_Rect_7 = "nv_Rect_7";
		public const string nv_Rect_8 = "nv_Rect_8";
		public const string nv_Rect_9 = "nv_Rect_9";

		public const string nv_Color_0 = "nv_Color_0";
		public const string nv_Color_1 = "nv_Color_1";
		public const string nv_Color_2 = "nv_Color_2";
		public const string nv_Color_3 = "nv_Color_3";
		public const string nv_Color_4 = "nv_Color_4";
		public const string nv_Color_5 = "nv_Color_5";
		public const string nv_Color_6 = "nv_Color_6";
		public const string nv_Color_7 = "nv_Color_7";
		public const string nv_Color_8 = "nv_Color_8";
		public const string nv_Color_9 = "nv_Color_9";

		public const string nv_String_0 = "nv_String_0";
		public const string nv_String_1 = "nv_String_1";
		public const string nv_String_2 = "nv_String_2";
		public const string nv_String_3 = "nv_String_3";
		public const string nv_String_4 = "nv_String_4";
		public const string nv_String_5 = "nv_String_5";
		public const string nv_String_6 = "nv_String_6";
		public const string nv_String_7 = "nv_String_7";
		public const string nv_String_8 = "nv_String_8";
		public const string nv_String_9 = "nv_String_9";

		public const string nv_Bool_0 = "nv_Bool_0";
		public const string nv_Bool_1 = "nv_Bool_1";
		public const string nv_Bool_2 = "nv_Bool_2";
		public const string nv_Bool_3 = "nv_Bool_3";
		public const string nv_Bool_4 = "nv_Bool_4";
		public const string nv_Bool_5 = "nv_Bool_5";
		public const string nv_Bool_6 = "nv_Bool_6";
		public const string nv_Bool_7 = "nv_Bool_7";
		public const string nv_Bool_8 = "nv_Bool_8";
		public const string nv_Bool_9 = "nv_Bool_9";

		public const string nv_Texture2D_0 = "nv_Texture2D_0";
		public const string nv_Texture2D_1 = "nv_Texture2D_1";
		public const string nv_Texture2D_2 = "nv_Texture2D_2";
		public const string nv_Texture2D_3 = "nv_Texture2D_3";
		public const string nv_Texture2D_4 = "nv_Texture2D_4";
		public const string nv_Texture2D_5 = "nv_Texture2D_5";
		public const string nv_Texture2D_6 = "nv_Texture2D_6";
		public const string nv_Texture2D_7 = "nv_Texture2D_7";
		public const string nv_Texture2D_8 = "nv_Texture2D_8";
		public const string nv_Texture2D_9 = "nv_Texture2D_9";

		public const string nv_Material_0 = "nv_Material_0";
		public const string nv_Material_1 = "nv_Material_1";
		public const string nv_Material_2 = "nv_Material_2";
		public const string nv_Material_3 = "nv_Material_3";
		public const string nv_Material_4 = "nv_Material_4";
		public const string nv_Material_5 = "nv_Material_5";
		public const string nv_Material_6 = "nv_Material_6";
		public const string nv_Material_7 = "nv_Material_7";
		public const string nv_Material_8 = "nv_Material_8";
		public const string nv_Material_9 = "nv_Material_9";

		public const string nv_Shader_0 = "nv_Shader_0";
		public const string nv_Shader_1 = "nv_Shader_1";
		public const string nv_Shader_2 = "nv_Shader_2";
		public const string nv_Shader_3 = "nv_Shader_3";
		public const string nv_Shader_4 = "nv_Shader_4";
		public const string nv_Shader_5 = "nv_Shader_5";
		public const string nv_Shader_6 = "nv_Shader_6";
		public const string nv_Shader_7 = "nv_Shader_7";
		public const string nv_Shader_8 = "nv_Shader_8";
		public const string nv_Shader_9 = "nv_Shader_9";
		#endregion nv IDs

		#region input output IDs

		public const string touch_altitudeAngle_ID = "touch_altitudeAngle"; 
								 
		public const string touch_azimuthAngle_ID = "touch_azimuthAngle";
								 
		public const string touch_deltaPosition_ID = "touch_deltaPosition";
								 
		public const string touch_deltaTime_ID = "touch_deltaTime";
								 
								 
								 
		public const string touch_fingerId_ID = "touch_fingerId";
								 
		public const string touch_maximumPossiblePressure_ID = "touch_maximumPossiblePressure";
								 
		public const string touch_phase_ID = "touch_phase";
								 
		public const string touch_position_ID = "touch_position";
								 
		public const string touch_pressure_ID = "touch_pressure";
								 
							
								 
		public const string touch_radius_ID = "touch_radius";
								 
		public const string touch_radiusVariance_ID = "touch_radiusVariance";
								 
		public const string touch_rawPosition_ID = "touch_rawPosition";
								 
		public const string touch_tapCount_ID = "touch_tapCount";
								 
		public const string touch_type_ID = "touch_type";







		public const string audioClip_LocalID = "audioClipValue_Local";


		public const string boolValue_ID = "boolValue"; // 0

		public const string boolValues_0_ID = "boolValues_0"; // 1

		public const string boolValues_1_ID = "boolValues_1"; // 2


		public const string boolsList_ID = "boolsList";

		public const string boolsListEntire_ID = "boolsListEntire";
							
		public const string boolsListEntire0_ID = "boolsListEntire0";
												   
		public const string boolsListEntire1_ID = "boolsListEntire1";








		public const string colorValue_ID = "colorValue"; // 3

		public const string colorValues_0_ID = "colorValues_0"; // 4

		public const string colorValues_1_ID = "colorValues_1"; // 5


		public const string colorsList_ID = "colorsList";

		public const string colorsListEntire_ID = "colorsListEntire";

		public const string colorsListEntire0_ID = "colorsListEntire0";

		public const string colorsListEntire1_ID = "colorsListEntire1";






		public const string intValue_ID = "intValue"; // 6

		public const string intValues_0_ID = "intValues_0"; // 7

		public const string intValues_1_ID = "intValues_1"; // 8

		public const string intValues_2_ID = "intValues_2"; // 8


		public const string intsList_ID = "intsList";
							
		public const string intsListEntire_ID = "intsListEntire";
							
		public const string intsListEntire0_ID = "intsListEntire0";
							
		public const string intsListEntire1_ID = "intsListEntire1";





		public const string floatValue_ID = "floatValue"; // 9 

		public const string floatValues_0_ID = "floatValues_0"; // 10

		public const string floatValues_1_ID = "floatValues_1"; // 11

		public const string floatValues_2_ID = "floatValues_2"; // 11


		public const string floatsList_ID = "floatsList";

		public const string floatsListEntire_ID = "floatsListEntire";

		public const string floatsListEntire0_ID = "floatsListEntire0";

		public const string floatsListEntire1_ID = "floatsListEntire1";




		public const string vector2Value_ID = "vector2Value"; // 12

		public const string vector2Values_0_ID = "vector2Values_0"; //13

		public const string vector2Values_1_ID = "vector2Values_1"; // 14


		public const string vector2List_ID = "vector2List";

		public const string vector2ListEntire_ID = "vector2ListEntire";

		public const string vector2ListEntire0_ID = "vector2ListEntire0";

		public const string vector2ListEntire1_ID = "vector2ListEntire1";






		public const string vector3Value_ID = "vector3Value"; // 15

		public const string vector3Values_0_ID = "vector3Values_0"; // 16

		public const string vector3Values_1_ID = "vector3Values_1"; // 17


		public const string vector3List_ID = "vector3List";
								  
		public const string vector3ListEntire_ID = "vector3ListEntire";
								  
		public const string vector3ListEntire0_ID = "vector3ListEntire0";
								  						 
		public const string vector3ListEntire1_ID = "vector3ListEntire1";



		public const string vector4Value_ID = "vector4Value";

		public const string vector4Values_0_ID = "vector4Values_0"; // 15

		public const string vector4Values_1_ID = "vector4Values_1"; // 16


		public const string vector4List_ID = "vector4List";

		public const string vector4ListEntire_ID = "vector4ListEntire";
								  
		public const string vector4ListEntire0_ID = "vector4ListEntire0";
								  
		public const string vector4ListEntire1_ID = "vector4ListEntire1";


		//public const string vector4List_ID = "vector4List";
		//						  
		//public const string vector4ListEntire_ID = "vector4ListEntire";
		//						  
		//public const string vector4ListEntire0_ID = "vector4ListEntire0";
		//						  
		//public const string vector4ListEntire1_ID = "vector4ListEntire1";




		public const string rayOrigin_ID = "rayOrigin"; // 18

		public const string rayDirection_ID = "rayDirection";




		public const string ray2DOrigin_ID = "ray2DOrigin";

		public const string ray2DDirection_ID = "ray2DDirection";




		public const string stringValue_ID = "stringValue"; // 20

		public const string stringValues_0_ID = "stringValues_0"; // 21 

		public const string stringValues_1_ID = "stringValues_1"; // 22


		public const string stringsList_ID = "stringsList";

		public const string stringsListEntire_ID = "stringsListEntire";

		public const string stringsListEntire0_ID = "stringsListEntire0";

		public const string stringsListEntire1_ID = "stringsListEntire1";





		public const string gameObjectValue_ID = "gameObjectValue"; // 23

		public const string gameObjectValues_0_ID = "gameObjectValues_0"; // 24

		public const string gameObjectValues_1_ID = "gameObjectValues_1"; // 25


		public const string gameObjectsList_ID = "gameObjectsList";

		public const string gameObjectsListEntire_ID = "gameObjectsListEntire";

		public const string gameObjectsListEntire0_ID = "gameObjectsListEntire0";

		public const string gameObjectsListEntire1_ID = "gameObjectsListEntire1";





		public const string doIt_ID = "doIt";









		public const string boundsCenterValue_ID = "boundsCenterValue";

		public const string boundsExtentsValue_ID = "boundsExtentsValue";

		public const string boundsMaxValue_ID = "boundsMaxValue";

		public const string boundsMinValue_ID = "boundsMinValue";

		public const string boundsSizeValue_ID = "boundsSizeValue";



		public const string raycastHitBarycentricCoordinate_ID = "raycastHitBarycentricCoordinate";

		public const string raycastHitTriangleIndex_ID = "raycastHitTriangleIndex";


		public const string raycastHitPoint_ID = "raycastHitPoint";

		public const string raycastHitNormal_ID = "raycastHitNormal";

		public const string raycastHitDistance_ID = "raycastHitDistance";


		public const string raycastHitGameObject_ID = "raycastHitGameObject";


		public const string raycastHitLightmapCoord_ID = "raycastHitLightmapCoord";

		public const string raycastHitTextureCoord_ID = "raycastHittextureCoord";

		public const string raycastHitTextureCoord2_ID = "raycastHittextureCoord2";


		public const string hit2D_gameObject_ID = "hit2D_gameObject";

		public const string hit2D_centroid_ID = "hit2D_centroid";

		public const string hit2D_distance_ID = "hit2D_distance";

		public const string hit2D_fraction_ID = "hit2D_fraction";

		public const string hit2D_normal_ID = "hit2D_normal";

		public const string hit2D_point_ID = "hit2D_point";



		public const string m44Value_entier_ID = "m44Value_entier";

		public const string m44Value_Input_0_entier_ID = "m44Value_Input_0_entier";

		public const string m44Value_Input_1_entier_ID = "m44Value_Input_1_entier";




		public const string m44Value_0_ID = "m44Value_0";

		public const string m44Value_1_ID = "m44Value_1";

		public const string m44Value_2_ID = "m44Value_2";

		public const string m44Value_3_ID = "m44Value_3";


		public const string m44Value_4_ID = "m44Value_4";

		public const string m44Value_5_ID = "m44Value_5";

		public const string m44Value_6_ID = "m44Value_6";

		public const string m44Value_7_ID = "m44Value_7";


		public const string m44Value_8_ID = "m44Value_8";

		public const string m44Value_9_ID = "m44Value_9";

		public const string m44Value_10_ID = "m44Value_10";

		public const string m44Value_11_ID = "m44Value_11";


		public const string m44Value_12_ID = "m44Value_12";

		public const string m44Value_13_ID = "m44Value_13";

		public const string m44Value_14_ID = "m44Value_14";

		public const string m44Value_15_ID = "m44Value_15";


		public const string m44ValueDeterminant_ID = "m44ValueDeterminant";

		public const string m44ValueIsIdentity_ID = "m44ValueIsIdentity";

		public const string m44ValueInvertible_ID = "m44ValueInvertible";






		public const string materialValue_ID = "materialValue";

		public const string materialValues_0_ID = "materialValues_0";

		public const string materialValues_1_ID = "materialValues_1";


		public const string materialList_ID = "materialList";
							
		public const string materialListEntire_ID = "materialListEntire";
							
		public const string materialListEntire0_ID = "materialListEntire0";
							
		public const string materialListEntire1_ID = "materialListEntire1";




		public const string texture2DValue_ID = "texture2DValue";

		public const string texture2DValues_0_ID = "texture2DValues_0";

		public const string texture2DValues_1_ID = "texture2DValues_1";


		public const string texture2DList_ID = "texture2DList";

		public const string texture2DListEntire_ID = "texture2DListEntire";

		public const string texture2DListEntire0_ID = "texture2DListEntire0";

		public const string texture2DListEntire1_ID = "texture2DListEntire1";


		//public const string renderTextureValue_ID = "renderTextureValue";

		//public const string renderTextureValues_0_ID = "renderTextureValues_0";

		//public const string renderTextureValues_1_ID = "renderTextureValues_1";


		public const string shaderValue_ID = "shaderValue";

		public const string shaderValues_0_ID = "shaderValues_0";

		public const string shaderValues_1_ID = "shaderValues_1";


		public const string shaderList_ID = "shaderList";

		public const string shaderListEntire_ID = "shaderListEntire";

		public const string shaderListEntire0_ID = "shaderListEntire0";

		public const string shaderListEntire1_ID = "shaderListEntire1";




		public const string OffMeshLinkData_activated_ID = "OffMeshLinkData_activated";

		public const string OffMeshLinkData_startPosition_ID = "OffMeshLinkData_startPosition";
		public const string OffMeshLinkData_endPosition_ID = "OffMeshLinkData_endPosition";

		public const string OffMeshLinkData_valid_ID = "OffMeshLinkData_valid";





		public const string NavMeshHit_distance_ID = "NavMeshHit_distance";

		public const string NavMeshHit_hit_ID = "NavMeshHit_hit";

		public const string NavMeshHit_mask_ID = "NavMeshHit_mask";

		public const string NavMeshHit_normal_ID = "NavMeshHit_normal";

		public const string NavMeshHit_position_ID = "NavMeshHit_position";


		//public const string meshValue_ID = "meshValue";
		//
		//public const string meshValues_0_ID = "meshValues_0";
		//
		//public const string meshValues_1_ID = "meshValues_1";



		public const string rectValue_ID = "rectValue";

		public const string rectValues_0_ID = "rectValues_0";

		public const string rectValues_1_ID = "rectValues_1";


		public const string rectList_ID = "rectList";
							
		public const string rectListEntire_ID = "rectListEntire";
							
		public const string rectListEntire0_ID = "rectListEntire0";
							
		public const string rectListEntire1_ID = "rectListEntire1";

		#endregion input output IDs
	}


	//public enum Mouse_0_DragState
	//{
	//	none,
	//
	//	draggingNode,
	//
	//	drawingDragRect,
	//}


	public enum SelectionState
	{
		notSelected,

		selected
	}


	public enum AxeName
	{
		horizontal,

		vertical,

		horizontalAndOrVertical,


		fire1,

		fire2,

		fire3,


		jump,

		mouseX,

		mouseY,

		mouseScrollWheel,


		submit,

		cancel
	}

	/// <summary>
	/// Time type.
	/// </summary>
	public enum TimeType
	{
		deltaTime,

		fixedDeltaTime,

		framesSinceLevelLoad,

		timeSinceLevelLoad,

		fps, 

		tictacOnFrames,

		tictacOnTime
	}

	public enum ComputeMouseInputType
	{
		getMousePresent,

		getMousePosition,

		getMouseScrollDelta,
	}

	public enum ComputeAdsType
	{
		getDebugMode,

		setDebugMode,


		//getGameId,


		GetPlacementState,


		Initialize,


		isInitialized,


		IsReady,


		isShowing,

		isSupported,



		Show,

		WhatWasTheShowAdResult,


		//getIsTestMode,

		getVersion,
	}
	public enum ComputeAdsTypeToDo
	{
		SetMetaData,
	}


	public enum LogicType
	{	
		computeOrOperation,

		timeOperation,

		input,

		//unityInputAxes,

		unityInputClassAndCrossPlatform,

		mouseInput,

		Ads,

		UseExtensions,
		//UseHighLevelNodes,
		//GetSetOtherGameObjectsComponentsVariables,

		//test,

		GetSetOtherGameObjectsScriptsVariables,
	}

	//

	public enum ComputeUnityInputClassAndCrossPlatformType
	{
		acceleration,

		accelerationEventCount,

		accelerationEventsAccelerations,



		accelerationEventsDeltaTimes,

		anyKey,

		anyKeyDown,



		getBackButtonLeavesApp,

		setBackButtonLeavesApp,

		compass,



		getCompensateSensors,

		setCompensateSensors,

		getCompositionCursorPos,




		setCompositionCursorPos,

		compositionString,

		deviceOrientation,




		GetAccelerationEventAcceleration,

		GetAccelerationEventDeltaTime,


		GetAxis,

		GetAxisNoCrossPlatform,


		GetAxisRaw,

		GetAxisRawNoCrossPlatform,


		GetButton,

		GetButtonNoCrossPlatform,


		GetButtonDown,

		GetButtonDownNoCrossPlatform,


		GetButtonUp,

		GetButtonUpNoCrossPlatform,


		GetJoystickNames,

		GetKey,



		GetKeyDown,

		GetKeyUp,

		GetMouseButton,



		GetMouseButtonDown,

		GetMouseButtonUp,

		GetTouch,



		gyro,

		getImeCompositionMode,

		setImeCompositionMode,



		imeIsSelected,

		inputString,

		IsJoystickPreconfigured,



		locationServiceIsEnabledByUser,

		locationServiceGetLastData,

		locationServiceGetStatus,



		stopLocationService,

		startLocationService,



		mousePosition,

		mousePositionNoCrossPlatform,



		mousePresent,

		mouseScrollDelta,

		multiTouchEnabled,



		ResetInputAxes,

		getSimulateMouseWithTouches,

		setSimulateMouseWithTouches,



		stylusTouchSupported,

		touchCount,

		touchPressureSupported,



		touchSupported,
	}

	public enum ComputeUnityInputClassAndCrossPlatformTypeToDo
	{
		//touches,
	}


	public enum ConditionType
	{
		valueComparision,

		spacialTrigger
	}


	public enum ComputeMatrix44Type
	{
		getDeterminant,

		add,
	}

	public enum ComputeShaderType
	{		
		get,

		getIsSupported,

		getMaximumLOD,

		setMaximumLOD,

		getRenderQueue,

		forAllShadersGetGlobalMaximumLOD,

		forAllShadersSetGlobalMaximumLOD,

		forAllShadersDisableKeyword,

		forAllShadersEnableKeyword,

		FindAShaderByName,

		forAllShadersSetGlobalColor,

		forAllShadersSetGlobalFloat,

		forAllShadersSetGlobalInt,

		forAllShadersSetGlobalTexture,

		forAllShadersSetGlobalVector4,

		forAllShadersIsKeywordEnabled,

		forAllShadersPropertyToID,

		WarmupAllShaders,

		getName,

		setName,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeTexture2DType
	{
		get,

		getAnisoLevel,

		setAnisoLevel,

		setFilterMode,

		getMipMapBias,

		setMipMapBias,

		getWidth,

		getHeight,

		setWrapMode,

		getMipMapCount,

		GetPixel,

		GetPixelBilinear,

		SetPixel,

		CaptureGameViewScreenshot,
		//ReadPixels,
		Compress,

		AuthorANewUniColorTexture,

		MirrorAndRotate,

		ExtractChannels,

		SetChannels,

		SetChannelsToThisTexture,

		SetChannelAccordingToAnotherChannelValue,

		SetColorAccordingToAChannelValue,

		SetPixelAccordingToAChannelValue,

		ComputeNormalMap,

		SwitchColors,

		writeTextureToFile,

		blendTwoTextures,

		metallicAndSmoothnesToTexture,

		metallicSmoothnessAndOcclusionToTexture,

		getName,

		setName,

		sendMeAsTransferredData,

		listenToTransferredData,

		applySetPixelChanges,
	}

	//public enum ComputeRenderTextureType
	//{
	//	getAnisoLevel,
	//
	//	setAnisoLevel,
	//
	//
	//	setFilterMode,
	//
	//
	//	getHeight,
	//
	//
	//	getMipMapBias,
	//
	//	setMipMapBias,
	//
	//
	//	getWidth,
	//
	//	setWrapMode
	//}




	public enum ComputeMaterialType
	{
		get,

		getMainColor,

		setMainColor,

		setGlobalIlluminationFlags,

		getMainTexture,

		setMainTexture,

		getMainTextureOffset,

		setMainTextureOffset,

		getMainTextureTiling,

		setMainTextureTiling,

		getPassCount,

		getRenderQueue,

		setRenderQueue,

		setShader,

		getShader,
#if !UNITY_2018
		ProceduralMaterialGetAnimationUpdateRate,

		ProceduralMaterialSetAnimationUpdateRate,

		ProceduralMaterialGetProceduralBoolean,

		ProceduralMaterialGetProceduralColor,

		ProceduralMaterialGetProceduralFloat,

		ProceduralMaterialGetProceduralVector,

		ProceduralMaterialGetProceduralIntEnum,

		ProceduralMaterialGetIsProcessing,

		ProceduralMaterialRebuildTextures,

		ProceduralMaterialRebuildTexturesImmediately,

		ProceduralMaterialSetProceduralBoolean,

		ProceduralMaterialSetProceduralBooleanAndRebuild,

		ProceduralMaterialSetProceduralColor,

		ProceduralMaterialSetProceduralColorAndRebuild,

		ProceduralMaterialSetProceduralFloat,

		ProceduralMaterialSetProceduralFloatAndRebuild,

		ProceduralMaterialSetProceduralVector,

		ProceduralMaterialSetProceduralVectorAndRebuild,

		ProceduralMaterialSetProceduralIntEnum,

		ProceduralMaterialSetProceduralIntEnumAndRebuild,
#endif
		getName,

		setName,

		sendMeAsTransferredData,

		listenToTransferredData,

		getSetShaderPropertes,
	}
	enum ComputeMaterialTypeToDo
	{
		ProceduralMaterial_CacheProceduralProperty,

		ProceduralMaterial_cacheSize,

		ProceduralMaterial_ClearCache,

		ProceduralMaterial_FreezeAndReleaseSourceData,

		ProceduralMaterial_GetGeneratedTexture,

		ProceduralMaterial_GetGeneratedTextures,

		ProceduralMaterial_GetProceduralEnum,

		ProceduralMaterial_GetProceduralPropertyDescriptions,

		ProceduralMaterial_GetProceduralTexture,

		ProceduralMaterial_HasProceduralProperty,

		ProceduralMaterial_isCachedDataAvailable,

		ProceduralMaterial_isFrozen,

		ProceduralMaterial_isLoadTimeGenerated,

		ProceduralMaterial_IsProceduralPropertyCached,

		ProceduralMaterial_IsProceduralPropertyVisible,

		ProceduralMaterial_isReadable,

		ProceduralMaterial_loadingBehavior,

		ProceduralMaterial_preset,

		ProceduralMaterial_SetProceduralEnum,

		ProceduralMaterial_SetProceduralTexture,
	}


	public enum ComputeIntType
	{
		get,

		add,

		increment,

		subtract,

		multiply,

		divide,

		toString,

		clamp,

		random,

		Switch,

		isSmallerThan,

		isSmallerOrEqualThan,

		isGreaterThan,

		isGreaterOrEqualThan,

		isEqualTo,

		isNotEqualTo,

		saveItInPlayerPrefs,

		readItFromPlayerPrefs,

		shiftOperatorOfOne,

		or,

		incrementStatic,

		incrementStaticWithInit,

		min,

		max,

		multiplyWithFloatOutFloat,

		multiplyWithFloatOutInt,

		multiplyWithVector2,

		multiplyWithVector3,

		multiplyWithVector4,

		modulo,

		MultiplicativeInverse,

		loop,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeFloatType
	{
		get,

		add,

		subtract,

		multiply,

		divide,

		toString,

		clamp,

		random,

		Switch,

		isSmallerThan,

		isSmallerOrEqualThan,

		isGreaterThan,

		isGreaterOrEqualThan,

		isEqualTo,

		isNotEqualTo,

		saveItInPlayerPrefs,

		readItFromPlayerPrefs,

		repeat,

		sigmoid,

		min,

		max,

		modulo,

		MultiplicativeInverse,

		animationCurve,

		animationCurvePickValue,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeFloatType_partialMath
	{
		exp,

		ln,
	}


	public enum ComputeGameObjectType
	{
        get,

		destroy,

		instantiate,

		instantiateAndChooseUp,

		getActiveInHierarchy,

		getActiveSelf,

		CompareTag,

		getLayer,

		setLayer,

		getName,

		setName,

		activate,

		deactivate,

		getTag,

		setTag,

        isEqualTo,

        isChildOf,

        hasThisTag,

		otherGameObjectFoundAtRadius,

		otherGameObjectFoundOnMyWayAtDistance,

		otherGameObjectFoundAtRadius_2D,

		otherGameObjectFoundOnMyWayAtDistance_2D,

		SpawnMeInAGrid,

		sendMeAsTransferredData,

		listenToTransferredData,
    }
	public enum ComputeGameObjectTypeToDo
	{
		AddComponent,


		Equals,

		scene,


		hadEnteredTriggerWithGameObjectOfTag,

		hadExitTriggerWithGameObjectOfTag,


		hadEnteredTrigger2DWithGameObjectOfTag,

		hadExitTrigger2DWithGameObjectOfTag,



		hadEnteredCollisionWithGameObjectOfTag,

		hadExitCollisionWithGameObjectOfTag,


		hadEnteredCollision2DWithGameObjectOfTag,

		hadExitCollision2DWithGameObjectOfTag,
	}


	public enum ComputeBoolListType
	{
		get,

		mergeWith,

		sendMeAsTransferredData,

		listenToTransferredData,

		inventoryList,
	}

	public enum ComputeColorListType
	{
		get,

		mergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}



	public enum ComputeBoxColliderType 
	{

		SetBoxColliderCenter,

		GetBoxColliderCenter,


		SetBoxColliderSize,

		GetBoxColliderSize,

	}

	public enum ComputeSphereColliderType
	{

		SetSphereColliderCenter,

		GetSphereColliderCenter,


		SetSphereColliderRadius,

		GetSphereColliderRadius,


	}

	public enum ComputeCapsuleColliderType
	{

		SetCapsuleColliderCenter,

		GetCapsuleColliderCenter,


		SetCapsuleColliderDirection,

		GetCapsuleColliderDirection,



		SetCapsuleColliderHeight,

		GetCapsuleColliderHeight,


		SetCapsuleColliderRadius,

		GetCapsuleColliderRadius,

	}

	public enum ComputeMeshColliderType
	{
		SetMeshColliderConvex,

		GetMeshColliderConvex,
	}

	public enum ComputeColliderType
	{

		getBouunds,


		getContactOffset,

		setContactOffset,


		getEnabled,

		setEnabled,


		getIsTrigger,

		setIsTrigger,


		setBounceCombineOnPhysicMaterial,

		setBounceCombineOnSharedPhysicMaterial,


		setBouncinessOnPhysicMaterial,

		getBouncinessFromPhysicMaterial,


		setBouncinessOnSharedPhysicMaterial,




		setDynamicFrictionOnPhysicMaterial,

		getDynamicFrictionFromPhysicMaterial,


		setDynamicFrictionOnSharedPhysicMaterial,



		setFrictionCombineOnPhysicMaterial,

		setFrictionCombineOnSharedPhysicMaterial,



		setStaticFrictionOnPhysicMaterial,

		getStaticFrictionFromPhysicMaterial,


		setStaticFrictionOnSharedPhysicMaterial,

	}


	public enum ComputeColorType
	{
		get,

		Switch,

		randomHsv,

		darken,

		lighten,

		multiply,

		screen,

		colorDodge,

		colorBurn,

		linearDodge,

		linearBurn,

		overlay,

		hardLight,

		softLight,

		vividLight,

		linearLight,

		pinLight,

		hardMix,

		difference,

		exclusion,

		hue,

		saturation,

		color,

		luminosity,

		add,

		subtract,

		animationCurve,

		animationCurvePickValue,

		toString,

		sendMeAsTransferredData,

		listenToTransferredData,
	}



	public enum ComputeRayType
	{
		Raycast,
	}

	public enum ComputeRay2DType
	{
		Raycast,
	}



	public enum ComputeRendererType
	{

		getBounds,


		getEnabled,

		setEnabled,


		getIsPartOfStaticBatch,

		getIsVisible,


		getLightmapIndex,

		setLightmapIndex,

		getLightmapScaleOffset,


		getLocalToWorldMatrix,



		getProbeAnchor,

		setProbeAnchor,


		setMaterial,

		getMaterial,


		setSharedMaterial,

		getSharedMaterial,


		getMaterialsArray,

		getSharedMaterialsArray,
	}

	public enum ComputeSpriteRendererType
	{
		setSpriteRendererColor,

		getSpriteRendererColor,

		getSpriteRendererFlipX,

		setSpriteRendererFlipX,

		getSpriteRendererFlipY,

		setSpriteRendererFlipY,

		setSpriteDrawMode,

		setSpriteTileMode,

		setAdaptiveModeThreshold,

		getAdaptiveModeThreshold,

		setSize,

		getSize,

		tile,
		//getSpriteRendererSprite,

	}
	public enum ComputeRendererTypeToDo
	{
		

		setSpriteRendererSprite,
	}

	public enum ComputeLineRendererType
	{
		setLineRendererColors, //change
	}



	public enum ComputeCanvasRendererType
	{


		setCanvasRendererColor,

		getCanvasRendererColor,
	}



	public enum ComputeRigidBody2DType
	{


		ApplyForce,

		AddForceAtPosition,

		AddRelativeForce,

		AddTorque,


		setAngularDrag,

		getAngularDrag,


		setAngularVelocity,

		getAngularVelocity,


		setCenterOfMass,

		getCenterOfMass,


		//setCollisionDetectionMode,


		getDrag,

		setDrag,


		setFreezeRotation,

		getFreezeRotation,


		GetPoint,


		GetPointVelocity,

		GetRelativePoint,

		GetRelativePointVelocity,


		GetRelativeVector,


		GetVector,


		setGravityScale,
	
		getGravityScale,


		setInertia,

		getInertia,


		IsAwake,


		setIsKinematic,

		getIsKinematic,


		IsSleeping,


		IsTouching,

		IsTouchingLayers,


		setMass,

		getMass,


		MovePosition,

		MoveRotation,


		setPosition,

		getPosition,


		setRotation,

		getRotation,


		setSimulated,

		getSimulated,


		Sleep,


		setUseAutoMass,

		getUseAutoMass,


		setVelocity,

		getVelocity,


		WakeUp,


		getWorldCenterOfMass,



	}

	public enum ComputeRigidBodyType
	{


		ApplyForce,

		AddExplosionForce,

		AddForceAtPosition,

		AddRelativeForce,

		AddRelativeTorque,

		AddTorque,


		MovePosition,

		MoveRotation,


		getPosition,

		setPosition,


		getRotation,

		setRotation,


		getVelocity,

		setVelocity,


		GetPointVelocity,

		GetRelativePointVelocity,


		getUseGravity,

		setUseGravity,


		getMass,

		setMass,

		getInertiaTensor,

		setInertiaTensor,

		ResetInertiaTensor,


		getInertiaTensorRotation,

		setInertiaTensorRotation,



		getIsKinemaic,

		setIsKinemaic,


		getDrag,

		setDrag,


		getAngularDrag,

		setAngularDrag,



		getAngularVelocity,

		setAngularVelocity,


		getCenterOfMass,

		setCenterOfMass,

		ResetCenterOfMass,

		getWorldCenterOfMass,



		getDetectCollisions,

		setDetectCollisions,



		ClosestPointOnBounds,


		IsSleeping,


		SetDensity,


		Sleep,

		SweepTest,

		SweepTestAll,

		WakeUp,


		getMaxAngularVelocity,

		setMaxAngularVelocity,


		getMaxDepenetrationVelocity,

		setMaxDepenetrationVelocity,


		getSleepThreshold,

		setSleepThreshold,


		getSolverIterations, //obsolet after 5.3.1 ????

		setSolverIterations,
	}



	public enum ComputeUiButtonType
	{
		onClick,

		getEnabled,

		setEnabled,

		getIsActiveAndEnabled,
	}
	public enum ComputeUiButtonTypeToDo
	{
	}



	public enum ComputeUiImageType
	{
		getColor,

		setColor,


		getDefaultMaterial,



		getDepth,



		getEnabled,

		setEnabled,


		getFillAmount,

		setFillAmount,


		getFillCenter,

		setFillCenter,


		getFillClockwise,

		setFillClockwise,


		setFillMethod,





		GetPixelAdjustedRect,


		getHasBorder,


		getMainTexture,


		getMaterial,

		setMaterial,


		getMaterialForRendering,



		getPixelsPerUnit,


		getPreserveAspect,

		setPreserveAspect,


		SetNativeSize,


		getSprite,




		getOverrideSprite,



	}
	public enum ComputeUiImageTypeToDo
	{

		getFillOrigin,

		setFillOrigin,


		useGUILayout,

		UnregisterDirtyVerticesCallback,

		UnregisterDirtyMaterialCallback,

		UnregisterDirtyLayoutCallback,

		transform,

		tag,

		StopCoroutine,

		StopAllCoroutines,

		StartCoroutine,

		SetVerticesDirty,

		SetMaterialDirty,

		SetLayoutDirty,

		SetClipRect,

		SetAllDirty,

		SendMessageUpwards,

		SendMessage,

		runInEditMode,

		RegisterDirtyVerticesCallback,

		RegisterDirtyMaterialCallback,

		RegisterDirtyLayoutCallback,

		rectTransform,

		RecalculateMasking,

		RecalculateClipping,

		Rebuild,

		raycastTarget,

		Raycast,

		preferredWidth,

		preferredHeight,

		PixelAdjustPoint,

		OnRebuildRequested,

		onCullStateChanged,

		OnBeforeSerialize,

		OnAfterDeserialize,

		name,

		minWidth,

		minHeight,

		maskable,

		layoutPriority,

		LayoutComplete,

		IsRaycastLocationValid,

		hideFlags,

		GraphicUpdateComplete,

		GetModifiedMaterial,

		flexibleWidth,

		flexibleHeight,

		Equals,

		Cull,

		CrossFadeColor,

		CrossFadeAlpha,

		CompareTag,

		alphaHitTestMinimumThreshold,

		BroadcastMessage,

		CalculateLayoutInputHorizontal,

		CalculateLayoutInputVertical,

		CancelInvoke,

		canvas,

		canvasRenderer,


	}


	public enum ComputeUnityTextType
	{		
		getText,

		setText,

		getColor,

		setColor,




		setAlignment,

		getDepth,

		getEnabled,

		setEnabled,

		getFontSize,



		setFontSize,

		GetPixelAdjustedRect,

		getLineSpacing,

		setLineSpacing,

		getMainTexture,



		getMaterial,

		setMaterial,

		getMaterialForRendering,

		getPixelsPerUnit,
	}
	public enum ComputeUnityTextTypeToDo
	{
		//setSprite,

		//setOverrideSprite,


		rectTransform,

		getAlignment,

		getFont,

		setFont,

		getFontStyle,

		setFontStyle,


		resizeTextForBestFit,

		resizeTextMaxSize,

		resizeTextMinSize,

		SetClipRect,

		SetNativeSize,

		supportRichText,


		raycastTarget,

		Raycast,



		maskable,

		FontTextureChanged,

		Cull,

		verticalOverflow,

		useGUILayout,

		UnregisterDirtyVerticesCallback,

		UnregisterDirtyMaterialCallback,

		UnregisterDirtyLayoutCallback,

		transform,

		ToString,

		tag,

		StopCoroutine,

		StopAllCoroutines,

		StartCoroutine,


		SetMaterialDirty,



		SetLayoutDirty,

		SetAllDirty,

		SendMessageUpwards,

		SendMessage,

		runInEditMode,

		RegisterDirtyVerticesCallback,

		RegisterDirtyMaterialCallback,

		RegisterDirtyLayoutCallback,

		RecalculateMasking,

		RecalculateClipping,

		Rebuild,

		preferredWidth,

		preferredHeight,

		PixelAdjustPoint,

		OnRebuildRequested,

		onCullStateChanged,

		name,

		minWidth,

		minHeight,

		layoutPriority,

		LayoutComplete,

		isActiveAndEnabled,

		IsActive,

		horizontalOverflow,

		hideFlags,

		GraphicUpdateComplete,

		GetGenerationSettings,

		flexibleWidth,

		flexibleHeight,

		alignByGeometry,

		cachedTextGenerator,

		cachedTextGeneratorForLayout,

		CalculateLayoutInputHorizontal,

		CalculateLayoutInputVertical,

		CancelInvoke,

		canvas,

		canvasRenderer,

		CrossFadeAlpha,

		CrossFadeColor,

		defaultMaterial,
	}

	public enum ComputeAudioSourceType
	{
		getIsPlaying,

		getLoop,

		setLoop,

		getMaxDistance,
	
		setMaxDistance,



		getMinDistance,

		setMinDistance,

		getMute,

		setMute,

		getPanStereo,



		setPanStereo,

		pause,

		getPitch,

		setPitch,

		play,




		PlayDelayed,

		playOneShot,

		PlayScheduled,

		getPriority,

		setPriority,



		getSpatialBlend,

		setSpatialBlend,

		getSpatialize,

		setSpatialize,

		stop,



		getTime,

		setTime,

		UnPause,

		getVolume,

		setVolume,
	}

	public enum ComputeTransformType
	{
		getPosition,

		getRotation,

		getLocalScale,

		getLocalPosition,

		getLocalRotation,

		rotate,

		RotateAround,

		setPositionOnTransform,

		setRotationOnTransform,

		setLocalScaleOnTransform,

		setLocalPositionOnTransform,

		setLocalRotationOnTransform,

		translate,

		getForward,

		getRight,

		getUp,

		setForward,

		setRight,

		setUp,

		LookAt,

		makeMeChildOf,

		AmIChildOf,

		getRootParent,

		DetachChildren,

		FindChildOfName,

		FindChildOfIndex,

		getChildCount,

		GetSiblingIndex,

		SetAsFirstSibling,

		SetAsLastSibling,

		SetSiblingIndex,

		getHasChanged,

		setHasChanged,

		getLocalToWorldMatrix,

		getWorldToLocalMatrix,

		getParent,

		InverseTransformDirection,

		InverseTransformPoint,

		InverseTransformVector,

		TransformDirection,

		TransformPoint,

		TransformVector,

		SmoothDamp,

		parallaxScrolling,

		parallaxScrollingEnhanced,

		keplerOrbit,

		animationCurvePosition,

		animationCurveRotation,

		animationCurveLocalScale,
	}


	public enum ComputeBoolType
	{
		get,

		atGameStart,

		loadScene,

		or,

		and,

		goToState,

		toString,

		toFloat,

		itIsNot,
		//setRandomSeed

		quitApplication,

		sendATruePulseWithDelay,

		sendEvent,

		listenToEvent,

		invertAtPulse,

		invertPeriodically,

		sendMeAsTransferredData,

		listenToTransferredData,

		atStateStart,
	}



	public enum ComputeRectType 
	{
		get,

		contains,

		toString,
		//Switch,
		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector4Type
	{
		get,

		Add,

		subtract,

		dot,

		Switch,

		toString,

		vector3ToVector4,

		Vector4ToColor,

		animationCurve,

		animationCurvePickValue,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector3Type
	{
		get,

		Add,

		subtract,

		cross,

		dot,

		multiply,

		toVector2,

		Switch,

		putThisFloatInX,

		putThisFloatInY,

		putThisFloatInZ,

		putThisFloatInXY,

		putThisFloatInXZ,

		putThisFloatInYZ,

		putThisFloatInXYZ,

		getFloatFromX,

		getFloatFromY,

		getFloatFromZ,

		insideUnitSphere,

		onUnitSphere,

		randomRotation,

		randomRotationUniform,

		areEqual,

		areOrthogonal,

		areParallel,

		toString,

		magnitude,

		ClampMagnitude,

		normalize,

		min,

		max,

		animationCurve,

		animationCurvePickValue,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector2Type
	{
		get,

		Add,

		subtract,

		dot,

		multiply,

		Switch,

		putThisFloatInX,

		putThisFloatInY,

		putThisFloatInXY,

		getFloatFromX,

		getFloatFromY,

		insideUnitCircle,

		onUnitCircle,

		areEqual,

		areOrthogonal,

		areParallel,

		toString,

		toRect,

		magnitude,

		ClampMagnitude,

		normalize,

		min,

		max,

		animationCurve,

		animationCurvePickValue,

		sendMeAsTransferredData,

		listenToTransferredData,
	}


	public enum ConditionIntFloatType
	{

	}


	public enum ConditionStringType 
	{
		
	}

	public enum ComputeStringType 
	{
		get,

		printToConsole,

		add,

		Switch,

		isEqual,

		isEqualToTag,

		saveItInPlayerPrefs,

		readItFromPlayerPrefs,

		sendMeAsTransferredData,

		listenToTransferredData,

		stringOperations,
	}




	public enum ComputeParticleSystemType
	{
		getParticleCount,

		getIsAlive,

		getIsPaused,

		getIsPlaying,

		getIsStopped,


		getDuration, // 

		EmitImmediat,

		getGravityModifier, //

		setGravityModifier, //


		getIsLooping,// 

		setIsLooping,

		getMaxParticles, // 

		setMaxParticles,


		Pause,

		Play,

		Stop,


		setPlaybackTime,

		getPlaybackTime,



		getPlaybackSpeed, // 

		setPlaybackSpeed, // 


		getPlayOnAwake, // 

		setPlayOnAwake, // 


		getRandomSeed,

		setRandomSeed,



		setSimulationSpace, //



		getStartColor, // 

		setStartColor,



		setScalingMode, // 




		setStartDelay, // 

		getStartDelay, //


		setStartLifeTime,//

		getStartLifeTime, //


		setStartRotation, //

		getStartRotation, // 


		setStartRotation3D, // 

		getStartRotation3D, //


		setStartSize, // 

		getStartSize, //


		setStartSpeed, //

		getStartSpeed, // 

	}

	public enum ComputeParticleSystemTypeToDo
	{
		EmitImmediat, //new overloads
	}


	public enum ComputeNavMeshAgentType
	{
		getRadius,

		setRadius,

		getHeight,

		setHeight,

		getBaseOffset,

		setBaseOffset,

		getAcceleration,

		setAcceleration,

		ActivateCurrentOffMeshLink,

		getAngularSpeed,

		setAngularSpeed,

		getSpeed,

		setSpeed,

		getCurrentVelocity,

		setCurrentVelocity,

		getAreaMask,

		setAreaMask,

		getAutoBraking,

		setAutoBraking,

		getAutoRepath,

		setAutoRepath,

		getAutoTraverseOffMeshLink,

		setAutoTraverseOffMeshLink,

		getAvoidancePriority,

		setAvoidancePriority,
		//CalculatePath,
		CompleteOffMeshLink,

		getCurrentOffMeshLinkData,

		getNextOffMeshLinkData,

		getDesiredVelocity,

		getDestination,

		setDestination,

		SetDestinationWithReturn,

		FindClosestEdge, // obsolet after 5.3.1 ??? change

		GetAreaCost,

		SetAreaCost,

		getHasPath,

		getIsOnNavMesh,

		getIsOnOffMeshLink,

		getIsPathStale,

		Move,

		getNextPosition,

		setNextPosition,

		setObstacleAvoidanceType,

		getIsPathPending,

		getPathStatus,

		RayCast,

		getRemainingDistance,

		ResetPath,

		Resume, 

		Stop, 

		getStoppingDistance,

		setStoppingDistance,

		SamplePathPositionGetInfoFacingTheAgent,

		getSteeringTarget,

		shouldTheAgentUpdateTheTransformPosition,

		isAgentUpdatingTransformPosition,

		shouldTheAgentUpdateTheTransformRotation,

		isAgentUpdatingTransformRotation,

		Warp,
	}
	public enum ComputeNavMeshAgentTypeToDo
	{


		CalculatePath,

		get_path,

		set_path,

		get_pathEndPosition,

		SetPath,
	}
	public enum ComputeNavMeshAgentTypeToTest
	{


		CompleteOffMeshLink,
	}

	public enum ComputeMeshOnSharedOrNot
	{


		onSharedMesh,

		onInstanceMesh,
	}
	//public enum ComputeMeshType
	//{
	//	getBlendShapeCount,
	//
	//	getBounds,
	//
	//	Clear,
	//
	//	ClearBlendShapes,
	//
	//	GetBlendShapeFrameCount,
	//
	//	MarkDynamic,
	//
	//	Optimize,
	//
	//	RecalculateBounds,
	//
	//	RecalculateNormals,
	//
	//	getSubMeshCount,
	//
	//	setSubMeshCount,
	//
	//	getVertexCount,
	//}
	public enum ComputeMeshTypeToDo
	{
		bindposes,

		boneWeights,

		colors,

		CombineMeshes,

		GetBlendShapeFrameVertices,

		GetBlendShapeFrameWeight,

		GetBlendShapeIndex,

		GetBlendShapeName,

		GetIndices,

		GetTopology,


		GetTriangles,

		GetUVs,

		normals,

		SetColors,

		SetIndices,

		SetNormals,

		SetTangents,

		SetTriangles,

		SetUVs,

		SetVertices,

		tangents,

		triangles,

		UploadMeshData,

		uv,

		uv2,

		uv3,

		uv4,
	}
	public enum ComputeMeshTypeToTest
	{


		GetBlendShapeFrameCount,
	}


	public enum ComputeCameraType
	{


		ScreenPointToRay,

		ScreenToViewportPoint,

		ScreenToWorldPoint,


		ViewportPointToRay,

		ViewportToScreenPoint,

		ViewportToWorldPoint,


		WorldToViewportPoint,

		WorldToScreenPoint,



		getActualRenderingPath,

		getRenderingPath,

		setRenderingPath,


		getAspectRatio,

		setAspectRatio,

		ResetAspect,


		getFarClipPlane,

		setFarClipPlane,


		getNearClipPlane,

		setNearClipPlane,


		getFieldOfView,

		setFieldOfView,

		ResetFieldOfView, // 


		getBackgroundColor,

		setBackgroundColor,


		getPixelHeight,

		getPixelWidth,


		getPixelRect,

		setPixelRect,


		getProjectionMatrix,

		setProjectionMatrix,

		ResetProjectionMatrix,


		CalculateObliqueMatrix,


		SetStereoProjectionMatrices,

		ResetStereoProjectionMatrices,


		SetStereoViewMatrices,

		ResetStereoViewMatrices,


		getStereoEnabled,


		getStereoConvergence,

		setStereoConvergence,


		getIsStereoMirrorMode,

		setIsStereoMirrorMode,


		getStereoSeparation,

		setStereoSeparation,





		getCameraToWorldMatrix,


		getWorldToCameraMatrix,

		ResetWorldToCameraMatrix,


		getCullingMask,

		setCullingMask,


		getTransparencySortMode,

		setTransparencySortMode,


		getUseOcclusionCulling,

		setUseOcclusionCulling,


		getVelocity,


		getDepth,

		setDepth,


		getDepthTextureMode,

		setDepthTextureMode,


		getEnabled,

		setEnabled,


		CopySettingsToTheCamera,


		getEventMask,

		setEventMask,


		getAllowHdr,

		setAllowHdr, 


		getOpaqueSortMode,

		setOpaqueSortMode,


		getOrthographic,

		setOrthographic,


		getOrthographicSize,

		setOrthographicSize,



		getRectNormalizedToTheScreen,

		setRectNormalizedToTheScreen,


		RenderManually,

		SetReplacementShader,

		RenderWithShader,

		ResetReplacementShader,


		getTargetDisplay,

		setTargetDisplay,


		setNonJitteredProjectionMatrix,

		getNonJitteredProjectionMatrix,
	}
	public enum ComputeCameraTypeToDo
	{
		targetTexture,
		
		AddCommandBuffer,

		CameraType,

		GetCommandBuffers,

		layerCullDistances,

		layerCullSpherical,

		RemoveAllCommandBuffers,

		RemoveCommandBuffer,

		RemoveCommandBuffers,

		RenderDontRestore,

		RenderToCubemap,

		SetTargetBuffers,
	}


	public enum ComputeBoxCollider2DType
	{
		getBoxCollider2DSize,

		setBoxCollider2DSize,
	}

	public enum ComputeCircleCollider2DType
	{
		
		getCircleCollider2DRadius,

		setCircleCollider2DRadius,
	}

	public enum ComputeCollider2DType 
	{
		getBounds,


		getDensity,

		setDensity,


		getEnabled,

		setEnabled,


		IsTouchingTheCollider2D,

		IsTouchingAnyColliderInThisLayerMask,


		getIsTrigger,

		setIsTrigger,


		getOffset,

		setOffset,


		OverlapPoint,


		getShapeCount,


		getSharedPhysicMaterialBounciness,

		setSharedPhysicMaterialBounciness,

		getSharedPhysicMaterialFriction,

		setSharedPhysicMaterialFriction,


		getUsedByEffector,

		setUsedByEffector,

	}
	public enum ComputeCollider2DTypeToDo
	{
		getPolygonCollider2DPoints,

		setPolygonCollider2DPoints,	

		getPolygonCollider2DPathCount,

		setPolygonCollider2DPathCount,


		edgeCollider2DAndItsProperties,


		IsTouching, // new overloads

	}

	public enum ComputeFloatsListType
	{
		get,

		MergeWith,

		min,

		max,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}


	public enum ComputeStringsListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}
		

	public enum ComputeIntsListType
	{
		get,

		MergeWith,

		min,

		max,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector2ListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector3ListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeMaterialListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeTexture2DListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeVector4ListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}

	public enum ComputeRectListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}


    public enum ComputeGameObjectListType
    {
		get,

        MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
    }

    public enum ComputeShaderListType
	{
		get,

		MergeWith,

		inventoryList,

		sendMeAsTransferredData,

		listenToTransferredData,
	}



	public enum KeyCodeTriggerWhen
	{
		down,

		hold,

		up
	}

	public enum MouseInputTriggerWhen
	{
		down,

		hold,

		drag,

		up
	}


	public enum ScriptLanguageType
	{
		cSharp,

		javaScript
	}

	public enum GraphType
	{
		Static,

		MonoBehaviour,

		Shader,

		Editor
	}

	public enum CsLogicNodeWhere
	{
		globalVariables,

		constructor,

		logicNodeUpdate,

		methods,
	}



	public enum SpacialTriggerType
	{
		sphere,

		box
	}




	public enum VariableTypeForProject
	{
		Bool,

		Float,

		Int,

		String,

		Vector2,

		Vector3,

		Vector4,


		GameObject,
	}


	public enum ComputeUiToggleType
	{
		isOn,

		getEnabled,

		setEnabled,

		getIsActiveAndEnabled,
	}
	public enum ComputeUiToggleTypeToDo
	{
		graphic,

		GraphicUpdateComplete,

		group,

		LayoutComplete,

		OnPointerClick,

		OnSubmit,

		onValueChanged,

		Rebuild,

		toggleTransition,
	}

	public enum ComputeUiInputFieldType
	{
		setKeyboardType,

		getText,

		setText,

		getEnabled,

		setEnabled,

		getIsActiveAndEnabled,
	}
	public enum ComputeUiInputFieldTypeToDo
	{
		ActivateInputField,

		asteriskChar,

		CalculateLayoutInputHorizontal,

		CalculateLayoutInputVertical,

		caretBlinkRate,

		caretColor,

		caretPosition,

		caretWidth,

		characterLimit,

		characterValidation,

		contentType,

		customCaretColor,

		DeactivateInputField,

		flexibleHeight,

		flexibleWidth,

		ForceLabelUpdate,

		GraphicUpdateComplete,

		inputType,

		isFocused,

		LayoutComplete,

		layoutPriority,

		lineType,

		minHeight,

		minWidth,

		MoveTextEnd,

		MoveTextStart,

		multiLine,

		OnBeginDrag,

		OnDeselect,

		OnDrag,

		OnEndDrag,

		onEndEdit,

		OnPointerClick,

		OnPointerDown,

		OnSelect,

		OnSubmit,

		OnUpdateSelected,

		onValidateInput,

		onValueChanged,

		placeholder,

		preferredHeight,

		preferredWidth,

		ProcessEvent,

		readOnly,

		Rebuild,

		selectionAnchorPosition,

		selectionColor,

		selectionFocusPosition,

		shouldHideMobileInput,

		textComponent,

		wasCanceled,


	}

	public enum ComputeLightType
	{
		getEnabled,

		setEnabled,

		getIsActiveAndEnabled,

		getAlreadyLightmapped,

		setAlreadyLightmapped,

		setAreaSize,

		getAreaSize,

		setBounceIntensity,

		getBounceIntensity,

		setColor,

		getColor,

		setCookieSize,

		getCookieSize,

		setCullingMask,

		getCullingMask,

		getIntensity,

		setIntensity,

		getIsBaked,

		setRange,

		getRange,

		setShadowBias,

		getShadowBias,

		setShadowCustomResolution,

		getShadowCustomResolution,

		setShadowNearPlane,

		getShadowNearPlane,

		setShadowNormalBias,

		getShadowNormalBias,

		setShadowStrength,

		getShadowStrength,

		setSpotAngle,

		getSpotAngle,
	}
	enum ComputeLightTypeToDo
	{
		AddCommandBuffer,

		commandBufferCount,

		cookie,

		flare,

		GetCommandBuffers,

		lightmapBakeType,

		RemoveAllCommandBuffers,

		RemoveCommandBuffer,

		RemoveCommandBuffers,

		renderMode,

		shadowResolution,

		shadows,

		type,

		setColorTemperature,

		getColorTemperature,
	}

	public enum VariableType
	{		
		Bool, //1 ///0

		boolsList, ///1

		Camera, ///2

		Color, //2  ///3

		colorsList,  ///4

		Float, //3 ///5

		floatsList, ///6

		GameObject, ///7

		GameObjectList, ///8

		Int, //4

		intsList,

		Ray,

		Ray2D,

		String, //5

		stringsList,

		Vector2, //6

		vector2List,

		Vector3, //7

		vector3List,

		Vector4, //8

		vector4List,

		rect, //9

		rectsList,

		Matrix4x4,

		Material, //10

		materialsList,

		Texture2D, //11

		texture2DList,

		Shader, //12

		shadersList,

		componentTransform,

		componentRigidBody,

		componentRigidBody2D,

		componentCollider,

		componentBoxCollider,

		componentSphereCollider,

		componentCapsuleCollider,

		componentMeshCollider,

		componentCollider2D,

		componentBoxCollider2D,

		componentCircleCollider2D,

		componentNavMeshAgent,

		componentParticleSystem,

		componentRenderer,

		componentSpriteRenderer,

		componentLineRenderer,

		componentUIUnityText,

		componentUIImage,

		componentUIButton,

		componentAudioSource,

		componentUIInputField,

		componentUIToggle,

		componentLight,
	}
	public enum VariableTypeToDo
	{
		//meshOnGameObject,

		//RenderTexture,
		//
		//Texture3D,
		//
		//SparseTexture,
		//
		//ProceduralTexture,
		//
		//MovieTexture,
		//
		//WebCamTexture,
	}


	public enum LogicNodeExtensions
	{
		terra,

		materials,

		gameDesign,
		///
	}

}
