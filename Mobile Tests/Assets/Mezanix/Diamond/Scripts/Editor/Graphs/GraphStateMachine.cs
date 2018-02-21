using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Graph state machine.
	///	Inherite from the class Graph. Handle the same 
	/// This class handle basic functionalities of the basic graph but in a specific way for
	/// the state machine. 
	/// Also, this class handle the C# scripts genration by preparing data before calling methods
	/// in the class CsScriptWriter.
	/// </summary>
	[Serializable]
	public class GraphStateMachine : Graph 
	{
		public int currentVersion;

		public int maxVersion;

		public List <string> undoRedo;

		[SerializeField]
		int nodeDataLines;

		[SerializeField]
		public int nameDataID;

		public List <string> undoRedoVersionReaden;


		public List <string> csScriptList = new List<string> ();

		public string scriptsGenerationFolderPath = @"Assets"; 

		public override void Init (string setGraphName, string setGraphNameRacine, bool firstTime, string creationPath, 
			GraphType setGraphType)
		{
			base.Init (setGraphName, setGraphNameRacine, firstTime, creationPath, setGraphType);

			nodeDataLines = 10;

			nameDataID = 5;

			csScriptList = new List<string> ();

			if (firstTime)
			{
				undoRedo = new List<string> ();

				currentVersion = 0;

				maxVersion = 0;

			//	WriteUndoRedoFile (firstTime, creationPath);

				CreateIdleState ();

				ScriptGenerationFolderPathFromNamesToSave ();
			}
		}

		public override void Init (string setGraphName, bool firstTime)
		{
			base.Init (setGraphName, firstTime);

			nodeDataLines = 10;

			nameDataID = 5;

			csScriptList = new List<string> ();

			if (firstTime)
			{
				undoRedo = new List<string> ();

				currentVersion = 0;

				maxVersion = 0;

				//WriteUndoRedoFile (firstTime);

				CreateIdleState ();

				ScriptGenerationFolderPathFromNamesToSave ();
			}
		}

		public override void ClearAndInit (string setGraphName)
		{
			base.ClearAndInit (setGraphName);
		}
	
		public override void ScriptGenerationFolderPathFromNamesToSave ()
		{
			if (Diamond.namesToSave == null)
			{
				scriptsGenerationFolderPath = @"Assets";

				return;
			}

			if ( ! AssetDatabase.IsValidFolder (Diamond.namesToSave.scriptsGenerationFolderPath))
			{
				scriptsGenerationFolderPath = @"Assets";

				return;
			}

			scriptsGenerationFolderPath = Diamond.namesToSave.scriptsGenerationFolderPath;
		}

		public override void ScriptGenerationFolderPathToNamesToSave ()
		{
			if (Diamond.namesToSave == null)
			{
				return;
			}

			if ( ! AssetDatabase.IsValidFolder (scriptsGenerationFolderPath))
			{
				Diamond.namesToSave.scriptsGenerationFolderPath = @"Assets";

				return;
			}

			Diamond.namesToSave.scriptsGenerationFolderPath = scriptsGenerationFolderPath;
		}

		void CreateIdleState ()
		{
			Auxiliaries.CreateNodeState (new Vector2 (100f, 50f), this, true, true);
		}

		void RepairNodesList ()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i] == null)
				{
					nodes.Remove (nodes [i]);
					i--;
				}
			}
		}

		public override void GraphUpdate (Event e, Rect workspaceRect)
		{
			//Undo.RecordObject (this, "Diamond Graph change");

			RepairNodesList ();

			IncrementFrameFromLoad ();

			DrawCurrentTransition (e);

			NodesUpdate (e, workspaceRect);

			EventProcess (e, workspaceRect);

			DrawOptionsButton (e, workspaceRect);

			//DrawOnMacDockMaximizeWindowMessage (workspaceRect);

			DrawScriptsGenerationFolderPath (workspaceRect);

			//Test (e);

			if ( ! editingLogic)
			{
				DrawDragRect (e);
			}

			HotKeys (e, workspaceRect);

			RenameGraph (e, workspaceRect);

			UpdateMyPath ();
		}


		bool renameGraphOpened = false;

		void RenameGraph (Event e, Rect workspaceRect)
		{
			if ( ! renameGraphOpened)
				return;

			Vector2 size = new Vector2 (200f, 90f);

			float gap = 10f;

			Rect panelRect = new Rect (workspaceRect.x + workspaceRect.width - gap - size.x,
				workspaceRect.y + gap, size.x, size.y);

			GUI.Box (panelRect, "", GetGuiStyle (Skins.node));



			Vector2 fieldSize = new Vector2 (0.8f, 0.19f);
			Rect fieldRect = RectOperations.RatioToAbsolute (panelRect, new Rect (
				0.5f*(1f -fieldSize.x), 1f-fieldSize.y-0.7f, fieldSize.x, fieldSize.y));

			graphNameRacine = EditorGUI.TextField (fieldRect, graphNameRacine);



			Vector2 okSize = new Vector2 (0.18f, 0.19f);
			if (GUI.Button (RectOperations.RatioToAbsolute (panelRect, new Rect (
				0.5f*(1f -okSize.x), 1f-okSize.y-0.4f, okSize.x, okSize.y)), "Ok", GetGuiStyle (Skins.button)) ||
				MouseKeysEvents.KeyIsUp (KeyCode.Return, e))
			{
				if (graphNameRacine != graphNameRacineBeforeRenaming)
				{
					if (ClassesNamesManager.CheckNewName (graphNameRacine))
					{
						ClassesNamesManager.AddNewName (graphNameRacine);

						renameGraphOpened = false;
					}
					else
					{
						graphNameRacine = graphNameRacineBeforeRenaming;
					}
				}
				else if (graphNameRacine == graphNameRacineBeforeRenaming)
				{
					renameGraphOpened = false;
				}
			}


			Vector2 cancelSize = new Vector2 (0.35f, 0.19f);
			if (GUI.Button (RectOperations.RatioToAbsolute (panelRect, new Rect (
				0.5f*(1f -cancelSize.x), 1f-cancelSize.y-0.1f, cancelSize.x, cancelSize.y)), 
				"Cancel", GetGuiStyle (Skins.button)) ||
				MouseKeysEvents.KeyIsUp (KeyCode.Escape, e))
			{
				graphNameRacine = graphNameRacineBeforeRenaming;

				renameGraphOpened = false;
			}
		}


		GUIStyle GetGuiStyle (string s)
		{
			return Skins.guiSkin.GetStyle (s);
		}


		protected override void UpdateMyPath ()
		{
			if (renameGraphOpened)
				return;

			base.UpdateMyPath ();

			string myPathTmp = AssetDatabase.GetAssetPath (this);

			if (string.IsNullOrEmpty (myPathTmp))
			{
				Debug.LogWarning ("Trying to Update graph path failed. the path is null or empty.");

				return;
			}
			
			string myContainerFolderPath = myPathTmp.Remove (myPathTmp.Length - graphName.Length - 7);
			//string myContainerFolderPathTmp = 
			//	StringTreatment.SubtractWeakFromEnd (
			//		myPathTmp, StringTreatment.AfterThat (myPathTmp, '/'));
			//
			//if (myContainerFolderPathTmp.Length < 2)
			//	return;
			//
			//string myContainerFolderPath = "";
			//for (int i = 0; i < myContainerFolderPathTmp.Length -1; i++)
			//	myContainerFolderPath += myContainerFolderPathTmp [i];

			if ( ! AssetDatabase.IsValidFolder (myContainerFolderPath))
			{
				Debug.LogWarning ("Trying to Update graph path failed. This is an invalide folder path: ");
				Debug.LogWarning (myContainerFolderPath);

				return;
			}

			myPath = myPathTmp;
			//Debug.Log (myPath);
		}

		void HotKeys (Event e, Rect wsRect)
		{
			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.S, e))
			{
				ChangeNodesSelectionState ();
			}
			else if (MouseKeysEvents.ControlCommandAltKey (KeyCode.Y, e))
			{
				SelectAllNodes ();
			}
			else if (MouseKeysEvents.ControlCommandAltKey (KeyCode.D, e))
			{
				DeselectAllNodes ();
			}
			else if (MouseKeysEvents.ControlCommandAltKey (KeyCode.O, e))
			{
				if (IsAllNodesDeselected ())
				{
					if ( ! editingLogic)
					{
						e.mousePosition = new Vector2 (wsRect.x + wsRect.width, wsRect.y);

						OpenGraphOptionsMenu (e);
					}
				}
			}
			else if (MouseKeysEvents.ControlCommandAltKey (KeyCode.K, e))
			{
				FocusIdle ();
			}
		}

		Rect dragRect = new Rect ();
		void DrawDragRect (Event e)
		{
			bool getting = false;

			dragRect = MouseKeysEvents.GetDragRect (e, out getting);

			if (getting && IsAllNodesDeselected ())
			{
				if (Mathf.Max (dragRect.width, dragRect.height) > 10f)
				{					
					Drawer.DrawRectBorders (dragRect, Color.white, 2.5f);
				}			
			}

			if (e.type == EventType.MouseUp)
			{
				if ( ! editingLogic)
				{
					SelectNodesInside (dragRect);
				}

				dragRect = new Rect ();
			}
		}

		bool IsPointInAnyNodeRect (Vector2 p)
		{
			bool r = true;

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].rect.Contains (p))
				{
					r = false;
				}
			}

			return r;
		}
		void SelectNodesInside (Rect r)
		{
			for (int i = 0; i < nodes.Count; i ++)
			{
				if (r.Contains (nodes [i].rect.center))
				{
					nodes [i].ChangeSelectionState (SelectionState.selected, true);
				}
			}
		}
		bool IsAllNodesDeselected ()
		{
			bool r = true;

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].selectionState == SelectionState.selected)
				{
					r = false;

					break;
				}
			}

			return r;
		}
	
		void SelectAllNodes ()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].selectionState == SelectionState.notSelected)
				{
					nodes [i].ChangeSelectionState (SelectionState.selected);
				}
			}
		}
		void DeselectAllNodes ()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].selectionState == SelectionState.selected)
				{
					nodes [i].ChangeSelectionState (SelectionState.notSelected);
				}
			}
		}
		void ChangeNodesSelectionState ()
		{
			int indexOfSelected = -1;
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].selectionState == SelectionState.selected)
				{
					nodes [i].ChangeSelectionState (SelectionState.notSelected);

					indexOfSelected = i;

					break;
				}
			}

			if (indexOfSelected == -1)
			{
				if (nodes.Count > 0)
				{
					nodes [0].ChangeSelectionState (SelectionState.selected);
				}
			}
			else if (indexOfSelected > -1 && indexOfSelected < nodes.Count)
			{
				if (indexOfSelected == nodes.Count - 1)
				{
					nodes [0].ChangeSelectionState (SelectionState.selected);
				}
				else if (indexOfSelected < nodes.Count - 1)
				{
					nodes [indexOfSelected + 1].ChangeSelectionState (SelectionState.selected);
				}
			}
		}


		void IncrementFrameFromLoad ()
		{
			framesFromLoad ++;

			if (framesFromLoad > 1000)
			{
				framesFromLoad = framesToActiveObjectFields;
			}
		}


		void NodesUpdate (Event e, Rect workspaceRect)
		{
			if (nodes.Count == 0)
				return;

			for (int i = 0; i < nodes.Count; i++)
				nodes [i].DrawTransitions ();

			Node editingLogicNode = null;

			for (int i = 0; i < nodes.Count; i++)
			{				
				if (nodes [i].editingLogic)
				{
					editingLogicNode = nodes [i];

					continue;
				}

				nodes [i].NodeUpdate (e, workspaceRect);
			}

			if (editingLogicNode != null)
				editingLogicNode.NodeUpdate (e, workspaceRect);
		}

		void EventProcess (Event e, Rect workspaceRect)
		{
			if (workspaceRect.Contains (e.mousePosition)) 
			{
				if (e.button == 0)
				{
					if (e.type == EventType.MouseDown)
					{
						GUIUtility.keyboardControl = 0;

						EditorGUIUtility.keyboardControl = 0;
					}
				}
			}

			//float gap = 2f;

			//Vector2 undoRedoButtonSize = new Vector2 (gap, gap) * 15f;

			//Vector2 fieldSize = new Vector2 (75f*gap, 7f*gap);

			//Rect gameObjectToAttachMonoRect, gameObjectToAttachMonoLabelRect;

			//undoRect = new Rect (1f, 1f, undoRedoButtonSize.x, undoRedoButtonSize.y);

			//redoRect = new Rect (1f + undoRect.width + gap, 1f,	undoRedoButtonSize.x, undoRedoButtonSize.y);

			//gameObjectToAttachMonoLabelRect = 
			//	new Rect (4f*gap, 12f*gap, 2f*fieldSize.x, 4f*fieldSize.y); 

			//gameObjectToAttachMonoRect = 
			//	new Rect (
			//		gameObjectToAttachMonoLabelRect.x,
			//		gameObjectToAttachMonoLabelRect.y + gameObjectToAttachMonoLabelRect.height + gap, fieldSize.x, fieldSize.y); 
			
			if (editingLogic)
				return;
			
			if (workspaceRect.Contains (e.mousePosition))
			{
				if (e.button == 1)
				{
					rightClickPosition = e.mousePosition;

					if (e.type == EventType.MouseDown)
					{
						GenericMenu menu = new GenericMenu ();

						menu.AddItem (new GUIContent (Enums.addState), false,  
							RightClickFunction, Enums.addState);


						menu.ShowAsContext ();

						e.Use ();
					}
				}

				CheckLeftHold (e);

			}

			/*if (GUI.Button (undoRect, "U",
				Skins.guiSkin.GetStyle (Skins.button)))
			{
				UndoAction ();
			}

			if (GUI.Button (redoRect, "R",
				Skins.guiSkin.GetStyle (Skins.button)))
			{
				RedoAction ();
			}*/

		/*	EditorGUI.LabelField (gameObjectToAttachMonoLabelRect, "Game Object To Attach \nthe MonoBehaviour On It.\n" +
				"Called in the logics: \n'Main Game Object'",
				Skins.guiSkin.GetStyle (Skins.graphLabel));*/

		/*	gameObjectToAttachMono = EditorGUI.ObjectField (
				gameObjectToAttachMonoRect, gameObjectToAttachMono, typeof (GameObject), true) as GameObject;

			GameObjectFinderAux.AddComponent (gameObjectToAttachMono, gameObjectToAttachMonoOld);*/
		}

		void DrawOptionsButton (Event e, Rect workspaceRect)
		{
			if (editingLogic)
				return;

			float oppoPosX = 20f;

			float posY = 20f;

			Vector2 size = new Vector2 (10f, 30f);

			Rect suitRect = new Rect (workspaceRect.width - oppoPosX, posY, size.x, size.y);

			if (GUI.Button (suitRect, "", Skins.guiSkin.GetStyle (Skins.dotesOptions)))
			{
				OpenGraphOptionsMenu (e);
			}
		}

		//void DrawOnMacDockMaximizeWindowMessage (Rect workspaceRect)
		//{		
		//	//if (editingLogic)
		//	//	return;
		//
		//	//float posY = 15f;
		//
		//	//Vector2 size = new Vector2 (30f, 10f);
		//
		//	//Rect suitRect = new Rect (0.5f*(workspaceRect.width - size.x - 300f), posY, size.x, size.y);
		//
		//	//EditorGUI.LabelField (suitRect, "On Mac, dock or maximize Diamond window to always see it.", 
		//	//	Skins.guiSkin.GetStyle (Skins.projectVariableName));
		//}

		void DrawScriptsGenerationFolderPath (Rect workspaceRect)
		{	
			float posY = 37f;
		
			Vector2 size = new Vector2 (62f, 10f);

			Rect suitRect = new Rect (size.x, posY, size.x, size.y);

			EditorGUI.LabelField (suitRect,
				"Scripts Generation Folder Path: " + scriptsGenerationFolderPath, Skins.guiSkin.GetStyle (Skins.logicNodeLabelLeft));
		}

		void OpenGraphOptionsMenu (Event e)
		{
			GenericMenu menu = new GenericMenu ();

			menu.AddItem (new GUIContent ("Write C# Scripts"), false, WriteCsScript);

			//menu.AddItem (new GUIContent ("Rename Graph"), false, MakeRenameGraphTrue);

			menu.AddItem (new GUIContent ("Show Graph Folder"), false, ShowGraphInFolder);

			menu.AddItem (new GUIContent ("Show Graph In Project"), false, ShowGraphInProject);

			//menu.AddItem (new GUIContent ("Write Mdstr"), false, WriteMdstr);

			menu.ShowAsContext ();

			e.Use ();
		}


		void ShowGraphInProject ()
		{
			EditorGUIUtility.PingObject (this);
		}

		void ShowGraphInFolder ()
		{
			EditorUtility.OpenFolderPanel (graphNameRacine, myPath, "");
		}

		string graphNameRacineBeforeRenaming;

		void MakeRenameGraphTrue ()
		{
			graphNameRacineBeforeRenaming = graphNameRacine;

			renameGraphOpened = true;
		}

		void WriteMdstr ()
		{
			Mdstr.Writer.Write (this);
		}

		void DeleteAllInFolder (string scriptsGraphFolder)
		{
			string absolutScriptsGraphFolder = Application.dataPath + StringTreatment.AfterThisIndex (
				scriptsGraphFolder, 6);

			DirectoryInfo scriptsGraphFolderDirectoryInfo = new DirectoryInfo (absolutScriptsGraphFolder);
			FileInfo [] filesInScriptsGraphFolder = scriptsGraphFolderDirectoryInfo.GetFiles ();
			for (int i = 0; i < filesInScriptsGraphFolder.Length; i++)
			{
				if (filesInScriptsGraphFolder [i].Extension == ".cs" || 
					filesInScriptsGraphFolder [i].Extension == ".meta")
				{
					filesInScriptsGraphFolder [i].Delete ();
				}
			}
			//foreach (DirectoryInfo di in scriptsGraphFolderDirectoryInfo.GetDirectories ())
			//{
			//	di.Delete ();
			//}
		}


		List <string> projectVariablesDeclarationReaden = new List<string> ();

		List <string> projectVariablesInitReaden = new List<string> ();

		bool inDeclaration = false;

		bool inInit = false;

		void ReadPreExistedProjrctVariavlesCs ()
		{
			projectVariablesFolderPath = Auxiliaries.CreateFolder (Auxiliaries.CreateFolder (
				CsScriptWriter.projectFolder, CsScriptWriter.scriptsFolderName), 
				CsScriptWriter.projectVariablesFolderName);

			string projectVariablesCsPath = projectVariablesFolderPath + "/" + "ProjectVariables.cs";
		
			if ( ! File.Exists (projectVariablesCsPath))
				return;

			string [] projectVariablesCsReaden = File.ReadAllLines (projectVariablesCsPath);
		

			projectVariablesDeclarationReaden = new List<string>();

			projectVariablesInitReaden = new List<string>();

			inDeclaration = false;

			inInit = false;

			for (int i = 0; i < projectVariablesCsReaden.Length; i++)
			{
				if (projectVariablesCsReaden [i].Contains (CsScriptWriter.projectVariablesEndOfDeclaration_prefix))
				{
					inDeclaration = false;
				}
				if (inDeclaration)
				{
					projectVariablesDeclarationReaden.Add (projectVariablesCsReaden [i] + "\n");
				}


				if (projectVariablesCsReaden [i].Contains (CsScriptWriter.projectVariablesEndOfInit_prefix))
				{
					inInit = false;
				}
				if (inInit)
				{
					projectVariablesInitReaden.Add (projectVariablesCsReaden [i] + "\n");
				}



				if (projectVariablesCsReaden [i].Contains (CsScriptWriter.projectVariablesBeginOfDeclaration_prefix))
				{
					if (i < projectVariablesCsReaden.Length -1)
					{
						inDeclaration = true;
					}
				}

				if (projectVariablesCsReaden [i].Contains (CsScriptWriter.projectVariablesBeginOfInit_prefix))
				{
					inInit = true;
				}
			}
		}

		string projectVariablesFolderPath;

		void WriteCsScript ()
		{
			//LogicNodesGetAssetsPaths ();

			//GameObjectFinderAction.HasGofToIdentified ();

			//string scriptsFolder = CsScriptWriter.CreateFolder (CsScriptWriter.projectFolder, CsScriptWriter.scriptsFolderName);
			string scriptsFolder = Diamond.namesToSave.scriptsGenerationFolderPath;

			string scriptsGraphFolder = CsScriptWriter.CreateFolder (scriptsFolder, graphNameRacine);

			if ( ! AssetDatabase.IsValidFolder (scriptsFolder))
			{
				Debug.LogWarning ("Trying to write scripts to an invalid folder");
				Debug.LogWarning ("So Diamond had created the scripts at the Assets root");
				Debug.LogWarning ("Choose a valid folder by clicking on the asterix at the top right of the Diamond window");
			}




			ReadPreExistedProjrctVariavlesCs ();


			DeleteAllInFolder (scriptsGraphFolder);

			DeleteAllInFolder (projectVariablesFolderPath);


			WriteCsScriptInterface (scriptsGraphFolder);

			switch (graphType)
			{
			case GraphType.Editor:
				break;

			case GraphType.MonoBehaviour:
				WriteCsScriptsProjectVariables (projectVariablesFolderPath, 
					projectVariablesDeclarationReaden, projectVariablesInitReaden);

				WriteCsScriptsForInterfaces (scriptsGraphFolder);

				WriteCsScriptMonoBehaviour (scriptsGraphFolder);
				break;

			case GraphType.Shader:
				break;

			case GraphType.Static:
				break;
			}

			Auxiliaries.SaveAndRefreshAssetsForced ();
		}


		void WriteCsScriptsProjectVariables (string folder, List <string> declReaden, List <string> initReaden)
		{
			CsScriptWriter.FillCSharpProjectVariablesList (ref csScriptList, declReaden, initReaden);

			CsScriptWriter.CsScriptListToFile (folder, "ProjectVariables", csScriptList, false);
		}

		void WriteCsScriptsForInterfaces (string scriptsGraphFolder)
		{
			string[] statesNames = new string[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				statesNames [i] = nodes [i].nodeName;
			}

			for (int i = 0; i < statesNames.Length; i++)
			{
				WriteCsScriptForInterface (statesNames [i], scriptsGraphFolder, nodes [i]);
			}
		}

		void WriteCsScriptForInterface (string stateName, string scriptsGraphFolder, Node theState)
		{
			for (int i = 0; i < theState.logics.Count; i++)
			{
				for (int j = 0; j < theState.logics [i].nodes.Count; j++)
				{
					CsScriptWriter.FillCSharpLogicNodeList (
						graphNameRacine, stateName, theState.logics [i], 
						theState.logics [i].nodes [j], this, theState, ref csScriptList);

					CsScriptWriter.CsScriptListToFile (scriptsGraphFolder, 
						graphNameRacine + "_" + stateName + "_" + theState.logics [i].logicName + "_" +
						theState.logics [i].nodes [j].nodeName, 
						csScriptList, false);
				}

				CsScriptWriter.FillCSharpLogicList (graphNameRacine, stateName, theState.logics [i], ref csScriptList);
			
				CsScriptWriter.CsScriptListToFile (scriptsGraphFolder, 
					graphNameRacine + "_" + stateName + "_" + theState.logics [i].logicName, csScriptList, false);
			}


			string[] statesNames = new string[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				statesNames [i] = nodes [i].nodeName;
			}

			CsScriptWriter.FillCSharpStateList (graphNameRacine + "_" + stateName, ref csScriptList, 
				new string[] 
				{
					"using UnityEngine;\n",

					"using System.Collections;\n",

					"using System.Collections.Generic;\n"
				}, statesNames, graphNameRacine, theState);

			CsScriptWriter.CsScriptListToFile (scriptsGraphFolder, graphNameRacine + "_" + stateName, csScriptList, false);
		}



		void WriteCsScriptInterface (string scriptsGraphFolder)
		{
			string[] statesNames = new string[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				statesNames [i] = nodes [i].nodeName;
			}

			CsScriptWriter.FillCSharpInterfaceList (graphNameRacine, ref csScriptList, 
				new string[] 
				{
					"using UnityEngine;\n",

					"using System.Collections;\n"
				}, statesNames);

			CsScriptWriter.CsScriptListToFile (scriptsGraphFolder, "I" + graphNameRacine, csScriptList, false);
		}
			
		void WriteCsScriptMonoBehaviour (string scriptsGraphFolder)
		{
			string[] statesNames = new string[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				statesNames [i] = nodes [i].nodeName;
			}

			CsScriptWriter.FillCSharpMonoBehaviourList (graphNameRacine, ref csScriptList, 
				new string[] 
				{
					"using UnityEngine;\n",

					"using System.Collections;\n",

					"using System.Collections.Generic;\n"
				}, statesNames, this);

			CsScriptWriter.CsScriptListToFile (scriptsGraphFolder, graphNameRacine, csScriptList, true);
		}



		public override void SetEditingLogicToFalse ()
		{
			bool atleastEditing = false;

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].callEditLogic)
				{
					atleastEditing = true;

					break;
				}
			}

			editingLogic = atleastEditing;
		}

		void CheckLeftHold (Event e)
		{
			if (e.button == 0)
			{
				if (e.type == EventType.MouseDown)
				{
					leftHold = true;
				}

				if (e.type == EventType.MouseUp)
				{
					leftHold = false;
				}
			}
		}


		void DrawCurrentTransition (Event e)
		{
			if (choosenToConnectId == -1)
				return;

			if (choosenToConnectId > nodes.Count -1)
				return;

			if (e.button == 0)
			{
				if (e.type == EventType.MouseDown)
				{
					choosenToConnectId = -1;

					return;
				}
			}

			Vector2 end = e.mousePosition;

			Vector2 startCenter = nodes [choosenToConnectId].rect.center;

			Vector2 orientationCenter = (end - startCenter).normalized;

			float orthoLength = nodes [choosenToConnectId].rect.height * 0.1f;

			Vector2 orthoNorm = new Vector2 ( - orientationCenter.y, orientationCenter.x);

			Vector2 ortho = orthoNorm * orthoLength;

			Vector2 start = startCenter + ortho;

			Vector2 orientation = (end - start).normalized;

			float transitionWidth = 5f;


			Color color = linksColor;


			Handles.DrawBezier (start, end, start + orientation, end - orientation, color,
				null, transitionWidth);


			Vector2 middle = 0.2f*start + 0.8f*end;

			Vector2 middle_1 = middle 
				- orientation * orthoLength
				+ orthoNorm * orthoLength * 1f;

			Vector2 middle_2 = middle 
				- orientation * orthoLength
				- orthoNorm * orthoLength * 1f;

			Handles.DrawBezier (middle, middle_1, middle_1, middle, color, null, transitionWidth);

			Handles.DrawBezier (middle, middle_2, middle_2, middle, color, null, transitionWidth);
		}


		void RightClickFunction (object obj)
		{
			string choosen = obj.ToString ();

			Auxiliaries.CreateNode (choosen, rightClickPosition, this, false);

			UndoRedoAddVersion ();
		}


		void WriteUndoRedoFile (bool firstTime, string path)
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond.graph != this)
				return;
			
			if (path != diamond.GetGraphPath ())
				path = diamond.GetGraphPath ();


			path = path.Remove (path.Length - graphName.Length - 6);


			UndoRedoFirstBloc (firstTime);
				
			UndoRedoToTxtFile (path + graphName + " UndoRedo.txt");

		}

		void WriteUndoRedoFile (bool firstTime)
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond.graph != this)
				return;


			string	path = diamond.GetGraphPath ();


			path = path.Remove (path.Length - graphName.Length - 6);


			UndoRedoFirstBloc (firstTime);

			UndoRedoToTxtFile (path + graphName + " UndoRedo.txt");

		}

		void UndoRedoFirstBloc (bool firstTime)
		{
			if (firstTime)
			{
				undoRedo = new List<string>();

				undoRedo.Add ("state machine data\n");

				undoRedo.Add (graphName + "\n");

				undoRedo.Add ("" + currentVersion + "\n");

				undoRedo.Add ("" + maxVersion + "\n");

				undoRedo.Add ("v\n");

				undoRedo.Add ("0\n");

				undoRedo.Add ("0\n");
			}
			else
			{
				undoRedo [2] = ("" + currentVersion + "\n");

				undoRedo [3] = ("" + maxVersion + "\n");
			}
		}
		/*
		public override void UndoRedoAddVersion ()
		{			
			currentVersion++;

			bool newVersion = false;

			if (maxVersion < currentVersion)
			{
				maxVersion = currentVersion;

				newVersion = true;
			}

			if (newVersion)
			{
				UndoRedoAddNewVersion ();
			}
			else
			{
				UndoRedoOverrideVersion ();
			}

			WriteUndoRedoFile (false);

			//Auxiliaries.SaveAndRefreshAssetsForced (this);
		}*/

		void UndoRedoAddNewVersion ()
		{
			undoRedo.Add ("v\n");

			undoRedo.Add ("" + currentVersion + "\n");

			undoRedo.Add ("" + nodes.Count + "\n");

			for (int i = 0; i < nodes.Count; i++)
			{
				UndoRedoAddNodeInNewVersion (i);
			}
		}

		void UndoRedoAddNodeInNewVersion (int i)
		{
			undoRedo.Add ("n\n");

			undoRedo.Add ("" + nodes [i].id + "\n");

			undoRedo.Add (nodes [i].nodeName + "\n");

			undoRedo.Add ("" + nodes [i].rect.x + "\n");

			undoRedo.Add ("" + nodes [i].rect.y + "\n");

			undoRedo.Add ("" + nodes [i].rect.width + "\n");

			undoRedo.Add ("" + nodes [i].rect.height + "\n");

			undoRedo.Add ("" + (int)nodes [i].selectionState + "\n");

			string sourcesLine = "";

			for (int s = 0; s < nodes [i].sources.Count; s++)
			{
				sourcesLine += nodes [i].sources [s] + " ";
			}

			undoRedo.Add (sourcesLine + "\n");

			sourcesLine = "";


			string destinationsLine = "";

			for (int d = 0; d < nodes [i].destinations.Count; d++)
			{
				destinationsLine += nodes [i].destinations [d] + " ";
			}

			undoRedo.Add (destinationsLine + "\n");

			destinationsLine = "";
		}

		void UndoRedoOverrideVersion ()
		{
			int indexCurrentVersion = UndoRedoIndexOfCurrentVersion ();

			int nodeCountToOverride = int.Parse (undoRedo [indexCurrentVersion]);

			int indexfirstNodeToOverride = indexCurrentVersion + 1;

			for (int i = indexfirstNodeToOverride; 
				i < indexfirstNodeToOverride + nodeCountToOverride*nodeDataLines; i++)
			{
				undoRedo.RemoveAt (indexfirstNodeToOverride);
			}


			int at = indexfirstNodeToOverride;

			for (int i = 0; i < nodes.Count; i++)
			{
				UndoRedoAddNodeInExistingVersion (ref at, i);
			}
		}

		void UndoRedoAddNodeInExistingVersion (ref int at, int i)
		{
			undoRedo.Insert (at, "n\n"); at++;

			undoRedo.Insert (at, "" + nodes [i].id + "\n"); at++;

			undoRedo.Insert (at, nodes [i].nodeName + "\n"); at++;

			undoRedo.Insert (at, "" + nodes [i].rect.x + "\n"); at++;

			undoRedo.Insert (at, "" + nodes [i].rect.y + "\n"); at++;

			undoRedo.Insert (at, "" + nodes [i].rect.width + "\n"); at++;

			undoRedo.Insert (at, "" + nodes [i].rect.height + "\n"); at++;

			undoRedo.Insert (at, "" + (int)nodes [i].selectionState + "\n"); at++;

			string sourcesLine = "";

			for (int s = 0; s < nodes [i].sources.Count; s++)
			{
				sourcesLine += nodes [i].sources [s] + " ";
			}

			undoRedo.Insert (at, sourcesLine + "\n"); at++;

			sourcesLine = "";


			string destinationsLine = "";

			for (int d = 0; d < nodes [i].destinations.Count; d++)
			{
				destinationsLine += nodes [i].destinations [d] + " ";
			}

			undoRedo.Insert (at, destinationsLine + "\n"); at++;

			destinationsLine = "";
		}

		int UndoRedoIndexOfCurrentVersion ()
		{
			int retVal = -1;

			for (int i = 0; i < undoRedo.Count; i++)
			{
				if (undoRedo [i][0] == 'v')
				{
					if (i < undoRedo.Count -2)
					{
						if (int.Parse (undoRedo [i+1]) == currentVersion)
						{
							retVal = i+2;

							Debug.Log ("version index m 2 found = " + i);
						}
					}
				}
			}

			return retVal;
		}

		void UndoRedoToTxtFile (string path)
		{
			if (undoRedo.Count == 0)
				return;

			File.WriteAllText (path, "");
			
			for(int i = 0; i < undoRedo.Count; i++)
			{
				File.AppendAllText (path, undoRedo [i]);
			}
		}


		/*
		void UndoAction ()
		{			
			if ( ! DownCurrentVersion ())
				return;

			undoRedoVersionReaden = new List<string> ();

			UndoRedoReadVersion ();

			UndoRedoApplyVersion ();
		}*/
		/*
		void RedoAction ()
		{
			if ( ! UpCurrentVersion ())
				return;

			undoRedoVersionReaden = new List<string> ();

			UndoRedoReadVersion ();

			UndoRedoApplyVersion ();
		}*/


		bool DownCurrentVersion ()
		{
			if (currentVersion < 1)
				return false;
			
			currentVersion--;

			return true;
		}

		bool UpCurrentVersion ()
		{
			if (currentVersion > maxVersion - 1)
				return false;
			
			currentVersion++;

			return true;
		}


		void UndoRedoReadVersion ()
		{
			if (undoRedo.Count == 0)
				return;

			for (int i = 0; i < undoRedo.Count; i++)
			{
				if (undoRedo [i][0] == 'v')
				{
					if (i + 1 < undoRedo.Count)
					{
						if (int.Parse (undoRedo [i+1]) == currentVersion)
						{
							if (i+2 < undoRedo.Count)
							{
								int nodesToRead = int.Parse (undoRedo [i+2]);

								if (i + 2 + nodesToRead*nodeDataLines < undoRedo.Count)
								{
									for (int j = i; j < i + 2 + nodesToRead*nodeDataLines+1; j++)
									{
										undoRedoVersionReaden.Add (undoRedo [j]);
									}	
								}
							}

							break;
						}
					}
				}
			}
		}
	
		/*void UndoRedoApplyVersion ()
		{
			if (undoRedoVersionReaden.Count < 3)
				return;

			int nodesToApply = int.Parse (undoRedoVersionReaden [2]);

			if (nodesToApply == 0)
			{
				ClearAndInit (graphName);
			
				return;
			}


			ClearAndInit (graphName);

			int k = nameDataID;

			for (int i = 0; i < nodesToApply; i++)
			{
				k = nameDataID + i * nodeDataLines;

				Node nodeTmp = ScriptableObject.CreateInstance <NodeState> ();

				if (nodeTmp == null)
					continue;
				

				nodeTmp.Init (new Vector2 (float.Parse (undoRedoVersionReaden [k+1]), 
					float.Parse (undoRedoVersionReaden [k+2])),
					this, false);


				nodeTmp.nodeName = undoRedoVersionReaden [k];

				nodeTmp.selectionState = (SelectionState)int.Parse (undoRedoVersionReaden [k+5]);

				string sourcesTmp = undoRedoVersionReaden [k+6];

				string destinationsTmp = undoRedoVersionReaden [k+7];

				if ( ! string.IsNullOrEmpty (sourcesTmp))
					nodeTmp.sources = StringTreatment.IntsFromStringSpaceSeparator (sourcesTmp);

				if ( ! string.IsNullOrEmpty (destinationsTmp))
					nodeTmp.destinations = StringTreatment.IntsFromStringSpaceSeparator (destinationsTmp);
			}
		}*/

		public override void SetEditingLogic (Node nodeToSetIn)
		{
			if (nodeToSetIn == null)
				return;

			for (int i = 0; i < nodes.Count; i++)
			{
				nodes [i].editingLogic = false;

				nodes [i].ChangeSelectionState (SelectionState.notSelected);
			}

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i] == nodeToSetIn)
				{
					nodes [i].editingLogic = true;

					nodes [i].ChangeSelectionState (SelectionState.selected);

					break;
				}
			}
		}

		void FocusIdle ()
		{
			Vector2 focusPos = new Vector2 (100f, 100f);

			Node idleNode = FindIdleNode ();

			if (idleNode != null)
			{
				MoveAllNodes (focusPos - idleNode.rect.position);
			}
		}

		Node FindIdleNode ()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].isIdle)
				{
					return nodes [i];
				}
			}

			return null;
		}

		void MoveAllNodes (Vector2 dist)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes [i].rect.position += dist;
			}
		}
	}
}