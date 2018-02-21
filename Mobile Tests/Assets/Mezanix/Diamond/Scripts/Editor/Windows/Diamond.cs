using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Diamond.
	/// The Diamond window, everything begin here. It is about state machine.
	/// Opened and initialized when the user goes to the Unity top menu:
	/// Tools/Mezanix/Diamond
	/// OnGUI method update at every frame the Graph that updates each state (NodeState class),
	/// the state updates its logics (Logic class), and each logic updates its logic nodes (LogicNode class).
	/// The logic node is the building block of a Diamond graph in which the user define what he wants to do.
	/// </summary>
	public class Diamond : EditorWindow
	{
		
		public Graph graph;


		ViewToolbarTop viewToolbarTop;

		ViewWorkspace viewWorkspace;


		static Diamond diamond;

		public static ProjectVariables projectVariables;

		public static NamesToSave namesToSave;


		//public static bool testing = false;


		[MenuItem ("Tools/Mezanix/Diamond")]
		static void Init ()
		{
			Auxiliaries.CreateAllFolders ();

			ClassesNamesManager.Init ();

			diamond = EditorWindow.GetWindow <Diamond> ();

			diamond.titleContent = new GUIContent ("Diamond");

			diamond.Show ();



			Skins.GetGuiSkin ();


			CreateNamesToSave ();

			CreateProjectVariables ();

			if (diamond.graph == null)
			{
				//graphLoaded = false;


				LoadGraph ();
			}


			diamond.ViewsCreate ();
		}

		void ReInit ()
		{
			Auxiliaries.CreateAllFolders ();

			diamond = EditorWindow.GetWindow <Diamond> ();

			diamond.titleContent = new GUIContent ("Diamond");

			diamond.Show ();

			//ticTacFrames = 0;


			Skins.GetGuiSkin ();

			//chocolate.guiSkin = Skins.guiSkin; 

			CreateNamesToSave ();

			CreateProjectVariables ();

			if (diamond.graph == null)
			{
				//graphLoaded = false;

				if (LoadGraph ())
				{
					//Debug.Log ("Graph loaded at ReInit");
				}
			}

			diamond.ViewsCreate ();
		}

		void WriteSomeScripts (Event e)
		{
			if (e.type == EventType.KeyUp)
			{
				if (e.keyCode == KeyCode.W)
				{
					string [][] varDec = new string[][]
					{
						new string[]
						{
							"LogicNode", "sourceLogicNode"
						}
					};

					string [][] enumNames = new string[][]
					{
						new string[]
						{
							"inputsTypes [inIndex]", "VariableType", 

							"touch"
						},


						new string[]
						{
							"inID", "Enums", 

							//"boolValues_0_ID", 
							//"boolValues_1_ID", 


							//"colorValues_0_ID", 
							//"colorValues_1_ID", 

							//"intValues_0_ID", 
							//"intValues_1_ID", 
							//"intValues_2_ID",

							//"floatValues_0_ID", 
							//"floatValues_1_ID", 
							//"floatValues_2_ID",

							//"stringValues_0_ID",
							//"stringValues_1_ID",


							//"vector2Values_0_ID", 
							//"vector2Values_1_ID",

							//"vector3Values_0_ID", 
							//"vector3Values_1_ID",
							//
							//"rayOrigin_ID", 
							//"rayDirection_ID",
							//
							//"ray2DOrigin_ID", 
							//"ray2DDirection_ID",
							//
							//"stringValues_0_ID", 
							//"stringValues_1_ID",

							//"gameObjectValues_0_ID", 
							//"gameObjectValues_1_ID",

							//"doIt_ID",
							//"materialValues_0_ID",
							//"materialValues_1_ID",
							//
							//"texture2DValues_0_ID",
							//"texture2DValues_1_ID",

							//"shaderValues_0_ID",
							//"shaderValues_1_ID",

							//"vector4Values_0_ID",
							//"vector4Values_1_ID",



							//"meshValues_0_ID",

							//"meshValues_1_ID",



							//"m44Value_Input_0_entier_ID",

							//"m44Value_Input_1_entier_ID",



							//"rectValues_0_ID",

							//"rectValues_1_ID",




							//"gameObjectsListEntire0_ID",

							//"gameObjectsListEntire1_ID",


							//"boolsListEntire0_ID",
							//
							//"boolsListEntire1_ID",


							//"colorsListEntire0_ID",

							//"colorsListEntire1_ID",


							//"floatsListEntire0_ID",

							//"floatsListEntire1_ID",


							//"intsListEntire0_ID",
							//
							//"intsListEntire1_ID",



							//"stringsListEntire0_ID",
							//
							//"stringsListEntire1_ID",


							//"vector2ListEntire0_ID",
							//
							//"vector2ListEntire1_ID",


							//"vector3ListEntire0_ID",
							//
							//"vector3ListEntire1_ID",


							//"materialListEntire0_ID",
							//
							//"materialListEntire1_ID",


							//"texture2DListEntire0_ID",
							//
							//"texture2DListEntire1_ID",


							//"shaderListEntire0_ID",

							//"shaderListEntire1_ID",


							//"vector4ListEntire0_ID",
							//
							//"vector4ListEntire1_ID",


							//"rectListEntire0_ID",
							//
							//"rectListEntire1_ID",
						},

						new string[]
						{
							"sourceNodeOutID", "Enums", 
							//
							//"boolValue_ID",
							//"boolValues_0_ID", 
							//"boolValues_1_ID", 
							//
							//"colorValue_ID", 
							//"colorValues_0_ID", 
							//"colorValues_1_ID", 
							//
							//"intValue_ID", 
							//"intValues_0_ID", 
							//"intValues_1_ID", 
							//"intValues_2_ID",
							//
							//"floatValue_ID", 
							//"floatValues_0_ID", 
							//"floatValues_1_ID", 
							//"floatValues_2_ID",
							//
							//"vector2Value_ID", 
							//"vector2Values_0_ID", 
							//"vector2Values_1_ID",
							//
							//"vector3Value_ID", 
							//"vector3Values_0_ID", 
							//"vector3Values_1_ID",
							//
							//"rayOrigin_ID", 
							//"rayDirection_ID",
							//
							//"ray2DOrigin_ID", 
							//"ray2DDirection_ID",
							//
							//"stringValue_ID", 
							//"stringValues_0_ID", 
							//"stringValues_1_ID",
							//
							//"gameObjectValue_ID",
							//"gameObjectValues_0_ID", 
							//"gameObjectValues_1_ID",
							//
							//"doIt_ID",
							//
							//"gameObjectsList_ID",

							//"boundsCenterValue_ID",
							//
							//"boundsExtentsValue_ID",
							//
							//"boundsMaxValue_ID",
							//
							//"boundsMinValue_ID",
							//
							//"boundsSizeValue_ID",


							//"raycastHitBarycentricCoordinate_ID",
							//
							//"raycastHitNormal_ID",
							//
							//"raycastHitPoint_ID",

							//"raycastHitLightmapCoord_ID",
							//
							//"raycastHitTextureCoord_ID",
							//
							//"raycastHitTextureCoord2_ID",

							//"raycastHitGameObject_ID",

							//"raycastHitTriangleIndex_ID",

							//"raycastHitDistance_ID",

							//"m44Value_0_ID",
							//"m44Value_1_ID",
							//"m44Value_2_ID",
							//"m44Value_3_ID",
							//
							//"m44Value_4_ID",
							//"m44Value_5_ID",
							//"m44Value_6_ID",
							//"m44Value_7_ID",
							//
							//"m44Value_8_ID",
							//"m44Value_9_ID",
							//"m44Value_10_ID",
							//"m44Value_11_ID",
							//
							//"m44Value_12_ID",
							//"m44Value_13_ID",
							//"m44Value_14_ID",
							//"m44Value_15_ID",

							//"m44ValueDeterminant_ID",

							//"m44ValueIsIdentity_ID",
							//
							//"m44ValueInvertible_ID",

							//"materialValue_ID",
							//"materialValues_0_ID",
							//"materialValues_1_ID",

							//"texture2DValue_ID",
							//"texture2DValues_0_ID",
							//"texture2DValues_1_ID",

							//"shaderValue_ID",
							//"shaderValues_0_ID",
							//"shaderValues_1_ID",

							//"vector4Value_ID",
							//"vector4Values_0_ID",
							//"vector4Values_1_ID",



							//"OffMeshLinkData_startPosition_ID",
							//"OffMeshLinkData_endPosition_ID",

							//"OffMeshLinkData_activated_ID",
							//"OffMeshLinkData_valid_ID",


							//"NavMeshHit_distance_ID",
							//
							//"NavMeshHit_hit_ID",
							//
							//"NavMeshHit_mask_ID",
							//
							//"NavMeshHit_normal_ID",

							//"NavMeshHit_position_ID",


							//"meshValue_ID",

							//"meshValues_0_ID",

							//"meshValues_1_ID",


							//"m44Value_entier_ID",

							//"m44Value_Input_0_entier_ID",

							//"m44Value_Input_1_entier_ID",


							//"rectValue_ID",
							//
							//"rectValues_0_ID",
							//
							//"rectValues_1_ID",


							//"gameObjectsListEntire_ID",
							//
							//"gameObjectsListEntire0_ID",
							//
							//"gameObjectsListEntire1_ID",


							//"boolsListEntire_ID",
							//
							//"boolsListEntire0_ID",
							//
							//"boolsListEntire1_ID",


							//"colorsListEntire_ID",
							//
							//"colorsListEntire0_ID",
							//
							//"colorsListEntire1_ID",


							//"floatsListEntire_ID",
							//
							//"floatsListEntire0_ID",
							//
							//"floatsListEntire1_ID",


							//"intsListEntire_ID",
							//
							//"intsListEntire0_ID",
							//
							//"intsListEntire1_ID",


							//"stringsListEntire_ID",
							//
							//"stringsListEntire0_ID",
							//
							//"stringsListEntire1_ID",


							//"vector2ListEntire_ID",
							//
							//"vector2ListEntire0_ID",
							//
							//"vector2ListEntire1_ID",


							//"vector3ListEntire_ID",
							//	   
							//"vector3ListEntire0_ID",
							//	   
							//"vector3ListEntire1_ID",


							//"materialListEntire_ID",
							//
							//"materialListEntire0_ID",
							//
							//"materialListEntire1_ID",


							//"texture2DListEntire_ID",
							//
							//"texture2DListEntire0_ID",
							//
							//"texture2DListEntire1_ID",


							//"shaderListEntire_ID",
							//
							//"shaderListEntire0_ID",
							//
							//"shaderListEntire1_ID",


							//"vector4ListEntire_ID",
							//
							//"vector4ListEntire0_ID",
							//
							//"vector4ListEntire1_ID",


							//"rectListEntire_ID",
							//
							//"rectListEntire0_ID",
							//
							//"rectListEntire1_ID",


							//"hit2D_gameObject_ID",

							//"hit2D_distance_ID",

							//"hit2D_fraction_ID",

							//"hit2D_centroid_ID",
							//
							//"hit2D_normal_ID",
							//
							//"hit2D_point_ID",



							//"touch_altitudeAngle_ID",
							//"touch_azimuthAngle_ID",
							//"touch_deltaTime_ID",
							//"touch_maximumPossiblePressure_ID",
							//"touch_pressure_ID",						
							//"touch_radius_ID",						
							//"touch_radiusVariance_ID",


							//"touch_fingerId_ID",
							//"touch_tapCount_ID",

							//"touch_phase_ID",
							//"touch_type_ID",

							//"touch_position_ID = ",
							//"touch_deltaPosition_ID",
							//"touch_rawPosition_ID",

						}
					};

					CsScriptWriter.WriteNestedStatesFunctions ("LinkInput", enumNames, varDec);

					Auxiliaries.SaveAndRefreshAssetsForced ();
				}
			}
		}



		static bool LoadGraph ()
		{
			if (diamond == null)
				return false;

			//graphLoaded = Auxiliaries.LoadGraph (namesToSave.graphPath);

			//return graphLoaded;

			return  Auxiliaries.LoadGraph (namesToSave.graphPath);
		}


		static float timeOnGuiBegin;


		public static int changedLogicNode = -1;
		public static int changedLogic = -1;
		public static int changedNode = -1;

		public void OnGUI ()
		{			
			timeOnGuiBegin = Time.realtimeSinceStartup;

			if (diamond == null)
				ReInit ();

			Auxiliaries.CreateAllFolders ();

			CreateNamesToSave ();

			CreateProjectVariables ();


			if (graph != null)
			{
				ClassesNamesManager.GraphPathToFile (true, this);
			}


			//if ( ! graphLoaded )
			//{
			//	if (DatesTimesAndFrequences.TicTac (ref Startup.ticTacFrames, 120, false))
			//	{
			//		Debug.Log ("graph loading at OnGUI () start, because graph was not loaded");
			//		if (LoadGraph ())
			//			Debug.Log ("graph loaded at OnGUI () start, because graph was not loaded");
			//	}
			//}
			if ( graph == null)
			{
				if (DatesTimesAndFrequences.TicTac (ref Startup.ticTacFrames, 120, false))
				{
					//Debug.Log ("graph loading at OnGUI () start, because graph was null");
					if (LoadGraph ())
					{
						//Debug.Log ("graph loaded at OnGUI () start, because graph was null");
					}
				}
			}


			Event e = Event.current;

			//WriteSomeScripts (e);

			float viewToolbarHeight = 25f;

			Rect wiewToolbarRect = new Rect (
				Vector2.zero, new Vector2 (position.width, viewToolbarHeight));

			Rect viewWorkspaceRect = new Rect (
				0f, wiewToolbarRect.height,
				position.width, position.height - wiewToolbarRect.height);



			//Debug.Log (changedLogicNode);

			if ( graph != null)
			{
				if (LogicNode.Aux_InRange (changedNode, 0, graph.nodes.Count) )
				{
					if (LogicNode.Aux_InRange (changedLogic, 0, graph.nodes [changedNode].logics.Count) )
					{
						if (LogicNode.Aux_InRange (changedLogicNode, 0, graph.nodes [changedNode].logics [changedLogic].nodes.Count) )
						{//Debug.Log (graph.nodes [changedNode].logics [changedLogic].nodes [changedLogicNode].nodeName);
							Undo.RecordObject (
								graph.nodes [changedNode].logics [changedLogic].nodes [changedLogicNode],
								"Logic Node " + changedLogicNode.ToString () + " change");

							//changedLogicNode = -1;
							//changedLogic = -1;
							//changedNode = -1;
						}
					}
				}
			}

			EditorGUI.BeginChangeCheck ();



			viewWorkspace.ViewUpdate (e, graph, viewWorkspaceRect);



			if (EditorGUI.EndChangeCheck ()) 
			{
				changesOccured = true;



				//if (changedLogicNode > -1 && changedLogic > -1 && changedNode > -1)
				//{
				//	if (graph != null)
				//	{
				//		if (graph.nodes [changedNode] != null)
				//		{
				//			if (graph.nodes [changedNode].logics [changedLogic] != null)
				//			{
				//				if (graph.nodes [changedNode].logics [changedLogic].nodes [changedLogicNode] != 
				//					null)
				//				{
				//					Undo.RecordObject (
				//						graph.nodes [changedNode].logics [changedLogic].nodes [changedLogicNode],
				//						"Logic Node " + changedLogicNode.ToString () + " change");
				//
				//					Debug.Log (graph.nodes [changedNode].logics [changedLogic].nodes [changedLogicNode].nodeName);
				//
				//				}
				//			}
				//		}
				//	}
				//}
			}

			viewToolbarTop.ViewUpdate (e, graph, wiewToolbarRect);


			if (DatesTimesAndFrequences.TicTac (ref graphBackupTimeCount, 180f, false))
			{
				if (graph != null)
				{
					Auxiliaries.BackupGraph (graph, graph.myPath);
				}
			}



			DrawDiamondLogo (viewWorkspaceRect);

			//DrawTestingToggle (viewWorkspaceRect);

			ScriptsCreatedByDiamond.IdentifiedObjectsActions.CreateGameObjectHolder_CreateIdentifiedObjects ();

			ScriptsCreatedByDiamond.MezanixDiamondEvents.CreateGameObjectHolder ();

			ScriptsCreatedByDiamond.MezanixDiamondDataTransfer.CreateGameObjectHolder ();




			Repaint ();



			CloseDiamond ();


			float realTimeDelta = Time.realtimeSinceStartup - timeOnGuiBegin;

			if (realTimeDelta > 0f)
				diamondDeltaTime = realTimeDelta;

			//Debug.Log (diamondDeltaTime);
		}
		//void OnInspectorUpdate() 
		//{
		//	//Repaint();
		//}

		//void RecordObjects ()
		//{
		//	if (graph == null)
		//		return;
		//
		//	for (int i = 0; i < graph.nodes.Count; i++)
		//	{
		//		for (int j = 0; j < graph.nodes [i].logics.Count; j++)
		//		{
		//			for (int k = 0; k < graph.nodes [i].logics [j].nodes.Count; k++)
		//			{
		//				Undo.RecordObject (graph.nodes [i].logics [j].nodes [k], "Diamond Graph change");
		//			}
		//		}
		//	}
		//	//Undo.RecordObject (this, "Diamond Graph change");
		//
		//}

		//int nuCounter = 0;
		//const int nuCounterSize = 20;

		void DrawDiamondLogo (Rect viewWorkspaceRect)
		{
			Vector2 diamondLogoSize = Vector2.one * 50f;

			float gap = 10f;

			GUI.Box (new Rect (new Vector2 (
				viewWorkspaceRect.x + viewWorkspaceRect.width - gap - diamondLogoSize.x,
				viewWorkspaceRect.y + viewWorkspaceRect.height - gap - diamondLogoSize.y), diamondLogoSize), "", GetGuiStyle (Skins.diamondLogo));
		}

		//void DrawTestingToggle (Rect viewWorkspaceRect)
		//{
		//	Vector2 diamondLogoSize = Vector2.one * 50f;
		//
		//	float testingWidth = 50f;
		//
		//	float gap = 10f;
		//
		//	Rect drawingRect = new Rect (new Vector2 (
		//		viewWorkspaceRect.x + viewWorkspaceRect.width - gap - diamondLogoSize.x - testingWidth,
		//		viewWorkspaceRect.y + viewWorkspaceRect.height - gap - diamondLogoSize.y), diamondLogoSize);
		//	
		//	EditorGUI.LabelField (drawingRect, "Testing", GetGuiStyle (Skins.logicNodeLabelLeft));
		//
		//
		//	Rect toggleDrawingRect = new Rect (drawingRect.position + new Vector2 (-20f, 17f), new Vector2 (20f, 20f));
		//
		//	bool oldTesting = testing;
		//
		//	testing = EditorGUI.Toggle (toggleDrawingRect, testing);
		//
		//	if (oldTesting != testing)
		//	{
		//		Skins.GetGuiSkin ();
		//	}
		//}

		GUIStyle GetGuiStyle (string style)
		{
			return Skins.guiSkin.GetStyle (style);
		}

		public static float diamondDeltaTime = 0.02f;

		public static float graphBackupTimeCount = 0f; 

		public static bool changesOccured = false;

		public static bool close = false;

		static void CloseDiamond ()
		{
			if ( ! close)
				return;



			//if (EditorUtility.DisplayDialog ("Diamond is closing", "Want to Quit?", "Yes", "No"))
			//{
			//	close = false;
			//	diamond.Close ();
			//}
			//close = false;



			if (changesOccured)
			{
				int i = EditorUtility.DisplayDialogComplex ("Diamond is closing", 
				"Want to Quit?",
				"Save", "Save and close", "Close without saving");
				
				switch (i)
				{
				case 0:
					Auxiliaries.SaveAndRefreshAssetsForced ();
					close = false;
					break;
				
				case 1:
					Auxiliaries.SaveAndRefreshAssetsForced ();
					close = false;
					diamond.Close ();
					break;
				
				case 2:
					close = false;
					diamond.Close ();
					break;
				}
			}
			else
			{
				close = false;
				diamond.Close ();
			}
		}

	

		void ViewsCreate ()
		{
			viewToolbarTop = new ViewToolbarTop ("");

			viewWorkspace = new ViewWorkspace (Enums.virginWorkSpaceTitle);
		}

		public string GetGraphPath ()
		{
			//return AssetDatabase.GetAssetPath (graph);

			if (graph == null)
			{
				Debug.LogWarning ("Searching a Graph Path for a null graph");

				return "Searching a Graph Path for a null graph";
			}

			return graph.myPath;
		}
	
		static void CreateProjectVariables ()
		{
			string projectVariablesFolderPath = Auxiliaries.CreateProjectVariablesFolder ();
								
			projectVariables = AssetDatabase.LoadAssetAtPath <ProjectVariables> (
				projectVariablesFolderPath + "/" + ProjectVariables.myName + ".asset");

			if (projectVariables == null)
			{
				projectVariables = ScriptableObject.CreateInstance <ProjectVariables> ();

				if (projectVariables != null)
				{
					projectVariables.name = ProjectVariables.myName;

					projectVariables.Init ();

					AssetDatabase.CreateAsset (projectVariables, projectVariablesFolderPath + "/" + 
						ProjectVariables.myName + ".asset");
				}
			}
		}

		static void CreateNamesToSave ()
		{
			string namesToSaveFolderPath = Auxiliaries.CreateNamesToSaveFolder ();

			namesToSave = AssetDatabase.LoadAssetAtPath <NamesToSave> (
				namesToSaveFolderPath + "/" + NamesToSave.myName + ".asset");

			if (namesToSave == null)
			{
				namesToSave = ScriptableObject.CreateInstance <NamesToSave> ();

				if (namesToSave != null)
				{
					namesToSave.name = NamesToSave.myName;

					namesToSave.Init ();

					AssetDatabase.CreateAsset (namesToSave, namesToSaveFolderPath + "/" + 
						NamesToSave.myName + ".asset");

					//Debug.Log ("namesToSaveCreated newly");
				}
			}

			//Debug.Log ("namesToSaveCreated");
		}

	}
}
