using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Node state.
	/// class for nodes represinting a state in the state machine.
	/// Diamond is based on state machine. Generated scripts use C# Interfaces to handle states relations.
	/// A Diamond Graph is structured like following:
	/// 1. Stage One: The States, in generated scripts, at each game's frame only one State is executed.
	/// 	For example, if with an AI you have a Patrol state and a Chase state, these two states can't be executed
	/// 	at the same time, here a state machine is used to decide which state to activate at which time.
	/// 	Usually, people explain the state machine principle by an AI example, but state machine is widely
	/// 	used in programming. For example, in a software, a minimized or maximized window represent two 
	/// 	diffrent states of this window.
	/// 2. Stage Two: The Logics, each state can hold many Logics, the user can create/delete and change the 
	/// 	execution order of Logics. 
	/// 3. Stage Three: The Logic Nodes, each Logic can hold many Logic Nodes. In the Logic Node things are executed,
	/// 	the Logic Node is the building block of a Diamond Graph. 
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
	[Serializable]
	public class NodeState : Node
	{
		Rect optionsButtonRect;

		bool renameCalled;

		Event eGlobal;

		Rect workSpaceRectGlobal;

		bool callAddLogic;

		string logicName = "";

		public override void Init (
			Vector2 setInitialPosition, 
			Graph setGraph, 
			bool withRename,
			bool setIsIdle, 
			bool writeStatesNamesEnumScript)
		{
			base.Init (
				setInitialPosition, 
				setGraph, 
				withRename, 
				setIsIdle, 
				writeStatesNamesEnumScript);

			nodeName = "State";

			//UpdateOldNodeName (nodeName);

			if (setIsIdle)
			{
				//nodeName = setGraph.graphNameRacine + "Idle";

				nodeName = "Idle";

				UpdateOldNodeName (nodeName);

				//ClassesNamesManager.AddNewName (nodeName, true);
			}

			name = "Scriptable Node State";

			graph.nodes.Add (this);

			id = graph.nodes.Count - 1;


			sources = new List<int> ();

			destinations = new List<int> ();

			if (withRename)
				renameCalled = true;
			else
				renameCalled = false;


			logics = new List<Logic> ();

			callAddLogic = false;

			callEditLogic = false;

			editingLogicId = -1;

			editLogicRect = new Rect ();

			editingLogic = false;


			if (writeStatesNamesEnumScript)
			{
				CsScriptWriter.WriteStatesNamesEnumScript ();
			}

			AssetDatabase.AddObjectToAsset (this, graph);
		
			//Auxiliaries.AddObjectToAssetSaveAndRefresh (this, graph);

			graph.UndoRedoWriteVersion ();
		}

		public override void Init (
			Vector2 setInitialPosition, 
			Graph setGraph, 
			bool withRename,
			bool setIsIdle, 
			bool writeStatesNamesEnumScript, bool forBackup)
		{
			base.Init (
				setInitialPosition, 
				setGraph, 
				withRename, 
				setIsIdle, 
				writeStatesNamesEnumScript, forBackup);

			nodeName = "State" + StringTreatment.backup;

			//UpdateOldNodeName (nodeName);

			if (setIsIdle)
			{
				//nodeName = setGraph.graphNameRacine + "Idle" + StringTreatment.backup;

				nodeName = "Idle" + StringTreatment.backup;

				UpdateOldNodeName (nodeName);

				//ClassesNamesManager.AddNewName (nodeName, true);
			}

			name = "Scriptable Node State";

			graph.nodes.Add (this);

			id = graph.nodes.Count - 1;


			sources = new List<int> ();

			destinations = new List<int> ();

			if (withRename)
				renameCalled = true;
			else
				renameCalled = false;


			logics = new List<Logic> ();

			callAddLogic = false;

			callEditLogic = false;

			editingLogicId = -1;

			editLogicRect = new Rect ();

			editingLogic = false;


			if (writeStatesNamesEnumScript)
			{
				CsScriptWriter.WriteStatesNamesEnumScript ();
			}

			AssetDatabase.AddObjectToAsset (this, graph);

			//Auxiliaries.AddObjectToAssetSaveAndRefresh (this, graph);

			graph.UndoRedoWriteVersion ();
		}
	
		void RepairLogicsList ()
		{
			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i] == null)
				{
					logics.Remove (logics [i]);
					i--;
				}
			}
		}

		public override void NodeUpdate (Event e, Rect workspaceRect)
		{
			//if (Diamond.changesOccured)
			//	Undo.RecordObject (this, "Diamond StateNode change");

			base.NodeUpdate (e, workspaceRect);

			RepairLogicsList ();

			if ( ! graph.editingLogic)
			{
				DrawOptionsButton ();

				DrawLogicInside ();

				DrawRenameField ();

				DrawChangeLogicsExecutionOrderWindow ();

				DrawDeleteMessage ();
			}

			if (callAddLogic)
			{
				AddLogic ();
			}

			if (callEditLogic) 
			{
				EditLogic ();
			}

			SetEditingLogic ();



			if (callRenameLogic && ( ! string.IsNullOrEmpty (logicToRenameName)))
			{
				RenameLogicAction ();
			}

			CheckGotoState ();
		}

		public override void EventProcess (Event e, Rect workspaceRect)
		{
			base.EventProcess (e, workspaceRect);

			eGlobal = e;

			workSpaceRectGlobal = workspaceRect;

			if (workspaceRect.Contains (e.mousePosition))
			{
				if ( ! graph.editingLogic)
				{
					if (optionsButtonRect.Contains (e.mousePosition))
					{
						if (e.type == EventType.MouseUp)
						{
							if (e.button == 0)
							{
								OpenOptionsMenu ();
							}
						}
					}
				}
				/*
				if (rect.Contains (e.mousePosition))
				{
					if (graph.choosenToConnectId > -1)
					{
						if (graph.choosenToConnectId < graph.nodes.Count)
						{
							if (graph.choosenToConnectId != id)
							{
								graph.nodes [graph.choosenToConnectId].AddDestination (id);

								AddSource (graph.choosenToConnectId);

								graph.choosenToConnectId = -1;

								graph.UndoRedoAddVersion ();
							}
						}
					}
				}*/
			}

			if (graph.AmIFirstSelectedFound (this))
			{
				if (MouseKeysEvents.ControlCommandAltKey (KeyCode.O, e))
				{
					if ( ! graph.editingLogic)
					{
						OpenOptionsMenu ();
					}
				}
			}

			//DeselectOnEscButton (e);

			RemoveNode (e);
		}

		//void DeselectOnEscButton (Event e)
		//{
		//	if (e.keyCode == KeyCode.Escape)
		//	{
		//		callAddLogic = false;
		//
		//		ChangeSelectionState (SelectionState.notSelected);
		//	}
		//}

		void OpenOptionsMenu ()
		{
			eGlobal.mousePosition = new Vector2 (rect.x + rect.width, rect.y);

			GenericMenu menu = new GenericMenu ();

			if ( ! isIdle)
				menu.AddItem (new GUIContent ("Rename"), false, Rename);

			//menu.AddItem (new GUIContent ("Make Transition"), false, SetChoosenToConnectID);

			if (logics.Count > 1)
			{
				menu.AddItem (new GUIContent ("Change Logics Execution Order"), false, ChangeLogicsExecutionOrder);
			}

			menu.AddItem (new GUIContent ("Add Logic"), false, SetCallAddLogicToTrue);

			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i] == null)
					continue;

				menu.AddItem (new GUIContent ("Edit Logic/" + (i+1).ToString ()
					+ ". " + logics [i].logicName), false, 
					SetCallEditLogicToTrue, i.ToString ());
			}

			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i] == null)
					continue;

				menu.AddItem (new GUIContent ("Rename Logic/" + (i+1).ToString ()
					+ ". " + logics [i].logicName), false, 
					RenameLogic, logics [i].logicName);
			}

			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i] == null)
					continue;

				menu.AddItem (new GUIContent ("Delete Logic/" + (i+1).ToString ()
					+ ". " + logics [i].logicName), false, 
					DeleteLogic, i.ToString ());
			}

			if ( ! isIdle)
				menu.AddItem (new GUIContent ("Delete"), false, RemoveNodeAction);

			menu.ShowAsContext ();
		}

		public bool changingLogicsExecutionOrder = false;
		Rect cleoRect;
		void ChangeLogicsExecutionOrder ()
		{
			SetCleoDragabls ();
			
			changingLogicsExecutionOrder = true;
		}
		void DrawChangeLogicsExecutionOrderWindow ()
		{
			if ( ! changingLogicsExecutionOrder)
				return;

			cleoRect = GetChangeLogicsExecutionOrderRect ();

			GUI.Box (cleoRect, "", Skins.guiSkin.GetStyle (Skins.leftUpMessageInfo));		



			DragCleoDragabls ();

			DrawCleoDragables ();


			float doneWidth = stepWHChangingLogicsExecutionOrder.x * 4f;
			if (GUI.Button (new Rect (
				cleoRect.x + cleoRect.width - doneWidth, 
				cleoRect.y + cleoRect.height, doneWidth, stepWHChangingLogicsExecutionOrder.y*0.65f),
					" Done", Skins.guiSkin.GetStyle (Skins.LittleNamedRects)))
			{
				changingLogicsExecutionOrder = false;
			}
		}

		List <DragableInList> cleoDragabls = new List<DragableInList> ();

		void SetCleoDragabls ()
		{
			cleoDragabls = new List<DragableInList> ();

			Rect cleoRect = GetChangeLogicsExecutionOrderRect ();

			cleoDragableInFieldRatio = new Rect(0.5f, 0.5f, 0.9f, 0.6f);

			Rect cleoDragableRect = new Rect ();

			Rect cleoDragableInFieldRect = new Rect ();


			for (int i = 0; i < logics.Count; i++)
			{
				cleoDragableRect = new Rect (
					cleoRect.x, 
					cleoRect.y + stepWHChangingLogicsExecutionOrder.y * (float)i, 
					cleoRect.width, stepWHChangingLogicsExecutionOrder.y);

				cleoDragableInFieldRect = RectOperations.RectInRect (
					cleoDragableRect, cleoDragableInFieldRatio);

				cleoDragabls.Add (new DragableInList (cleoDragableInFieldRect, logics [i]));
			}
		}

		void DrawCleoDragables ()
		{
			for (int i = 0; i < cleoDragabls.Count; i++)
			{
				if ( ! cleoDragabls [i].selected)
				{
					Rect cleoDragableRect = new Rect (
						cleoRect.x, 
						cleoRect.y + stepWHChangingLogicsExecutionOrder.y * (float)i, 
						cleoRect.width, stepWHChangingLogicsExecutionOrder.y);

					Rect cleoDragableInFieldRect = RectOperations.RectInRect (
						cleoDragableRect, cleoDragableInFieldRatio);

					cleoDragabls [i].rect = cleoDragableInFieldRect;
				}

				Logic logicTmp = (Logic)cleoDragabls [i].obj;

				GUI.Box (cleoDragabls [i].rect, " " + (i+1).ToString () + ". " + logicTmp.logicName, 
					Skins.guiSkin.GetStyle (Skins.LittleNamedRects));
			}
		}

		void DragCleoDragabls ()
		{
			for (int i = 0; i < cleoDragabls.Count; i++)
			{
				if (eGlobal.type == EventType.MouseDown)
				{
					if (eGlobal.button == 0)
					{
						if (cleoDragabls [i].rect.Contains (eGlobal.mousePosition))
						{
							cleoDragabls [i].selected = true;
						}
					}
				}

				if (cleoDragabls [i].selected)
				{
					Rect dragRect = new Rect (
						eGlobal.mousePosition.x - cleoDragabls [i].rect.width*0.5f,
						eGlobal.mousePosition.y - cleoDragabls [i].rect.height*0.5f,
						cleoDragabls [i].rect.width, cleoDragabls [i].rect.height);


					cleoDragabls [i].rect = dragRect;

					if (eGlobal.type == EventType.MouseUp)
					{
						if (eGlobal.button == 0)
						{
							if (eGlobal.mousePosition.y < cleoRect.y)
							{
								ListOperations.Permute (ref logics, i, 0);
							}
							else if (eGlobal.mousePosition.y > cleoRect.y + cleoRect.height)
							{
								ListOperations.Permute (ref logics, i, logics.Count-1);
							}
							else
							{
								float releaseMouseLevel = eGlobal.mousePosition.y - cleoRect.y;

								int i1 = Mathf.FloorToInt (
									releaseMouseLevel / stepWHChangingLogicsExecutionOrder.y);

								ListOperations.Permute (ref logics, i, i1);
							}


							SetCleoDragabls ();

							cleoDragabls [i].selected = false;
						}
					}
				}
			}
		}

		Vector2 stepWHChangingLogicsExecutionOrder;

		Rect cleoDragableInFieldRatio;

		Rect GetChangeLogicsExecutionOrderRect ()
		{
			stepWHChangingLogicsExecutionOrder = new Vector2 (8f, 23f);

			return new Rect (rect.x + rect.width, 
				rect.y, 
				((float)GetMaxLogicNameLength () + 3f)*stepWHChangingLogicsExecutionOrder.x,
				(float)logics.Count * stepWHChangingLogicsExecutionOrder.y);
		}
		int GetMaxLogicNameLength ()
		{
			return StringTreatment.GetMaxPhrasesLength (LogicsNames ().ToArray ());
		}


		void SetCallAddLogicToTrue ()
		{
			callAddLogic = true;
		}

		void SetCallEditLogicToTrue (object iS)
		{
			if (callEditLogic)
				return;

			int i = int.Parse (iS.ToString ());

			if (i < 0 || i > logics.Count - 1)
				return;

			editingLogicId = i;

			callEditLogic = true;

			graph.editingLogic = true;
		}

		void UpdateOldNodeName (string n)
		{
			if (oldNodeName == n)
				return;

			oldNodeName = n;
		}

		void Rename ()
		{
			renameCalled = true;
		}

		void DrawDeleteMessage ()
		{
			if (selectionState == SelectionState.notSelected)
				return;

			if (isIdle)
				return;

			Rect dmREct = RectOperations.RectInRect (rect,
				new Rect (0f, 1.5f, 1.1f, 0.3f));

			string messageFinale = "alt / option + X for Delete";

			GUI.Box (dmREct, messageFinale, Skins.guiSkin.GetStyle (Skins.leftUpMessageInfo));
		}

		void DrawRenameField ()
		{
			if ( ! renameCalled)
				return;

			Vector2 nodeSize = new Vector2 (rect.width, rect.height);

			Vector2 fieldSizeNormalized = new Vector2 (1.5f, 0.4f);

			Vector2 fieldPositionNormalized = new Vector2 (0.3f, 0.1f);

			Rect renameRect = new Rect (
				rect.x + fieldPositionNormalized.x*nodeSize.x,
				rect.y + fieldPositionNormalized.y*nodeSize.y,
				fieldSizeNormalized.x*nodeSize.x,
				fieldSizeNormalized.y*nodeSize.y);
			

			//GUI.SetNextControlName ("nodeStateNameTextField");
			nodeName = EditorGUI.TextField (renameRect, nodeName);

			nodeName = StringTreatment.ScriptName (nodeName);

			if (string.IsNullOrEmpty (nodeName))
			{
				nodeName = oldNodeName;

				return;
			}


			//GUI.FocusControl ("nodeStateNameTextField");

			fieldSizeNormalized = new Vector2 (0.12f, 0.35f);

			fieldPositionNormalized = new Vector2 (0.07f, 0.1f);

			Rect okRect = new Rect (
				rect.x + fieldPositionNormalized.x*nodeSize.x,
				rect.y + fieldPositionNormalized.y*nodeSize.y,
				fieldSizeNormalized.x*nodeSize.x,
				fieldSizeNormalized.y*nodeSize.y);

			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.B, eGlobal))
			{
				renameCalled = false;

				return;
			}
			

			if (GUI.Button (okRect, "Ok", Skins.guiSkin.GetStyle (Skins.LittleNamedRectsCenterDark)) ||
				MouseKeysEvents.KeyIsUp (KeyCode.Return, eGlobal))
			{
				if (nodeName == graph.graphNameRacine + "Idle")
				{
					EditorUtility.DisplayDialog ("Names Conflict", ClassesNamesManager.namesConflictSameGraphOrStateName,
						"Ok");

					return;
				}

				if (oldNodeName != nodeName)
				{
					if ( SimilarStateNameInGraph ())
					{
						EditorUtility.DisplayDialog ("State Name", "This State Name Already exist in your graph", "Ok");

						return;
					}
					
					//ClassesNamesManager.RemoveName (graph.graphNameRacine + "_" + oldNodeName, true);

					UpdateOldNodeName (nodeName);
				}

				renameCalled = false;

				CsScriptWriter.WriteStatesNamesEnumScript ();

				graph.UndoRedoAddVersion ();
			}
		}

		bool SimilarStateNameInGraph ()
		{
			bool r = false;

			for (int i = 0; i < graph.nodes.Count; i++)
			{
				if (graph.nodes [i] == this)
					continue;

				if (graph.nodes [i].nodeName == nodeName)
				{
					r = true;

					break;
				}
			}

			return r;
		}

		void DrawOptionsButton ()
		{			
			Vector2 nodeSize = new Vector2 (rect.width, rect.height);

			Vector2 fieldSizeNormalized = new Vector2 (0.05f, 0.44f);

			Vector2 fieldPositionNormalized = new Vector2 (0.915f, 0.4f);

			optionsButtonRect = new Rect (
				rect.x + fieldPositionNormalized.x*nodeSize.x,
				rect.y + fieldPositionNormalized.y*nodeSize.y,
				fieldSizeNormalized.x*nodeSize.x,
				fieldSizeNormalized.y*nodeSize.y);

			GUI.Box (optionsButtonRect, "", Skins.guiSkin.GetStyle (Skins.dotesOptions));
		}
	
		void DrawLogicInside ()
		{
			//if (editingLogicId < 0 || editingLogicId > logics.Count -1)
			//	return;

			if (editingLogic)
			if (editingLogicId >= 0 && editingLogicId <= logics.Count -1)
			if (logics [editingLogicId].rectMaximized)
				return;
			
			if (logics.Count == 0)
				return;
			
			Vector2 nodeSize = new Vector2 (rect.width, rect.height);

			Vector2 fieldSizeNormalized = new Vector2 (0.2f, 0.28f);

			Vector2 fieldPositionNormalized = new Vector2 (0.43f, 0.625f);

			Rect logicInsideRect = new Rect (
				rect.x + fieldPositionNormalized.x*nodeSize.x,
				rect.y + fieldPositionNormalized.y*nodeSize.y,
				fieldSizeNormalized.x*nodeSize.x,
				fieldSizeNormalized.y*nodeSize.y);

			GUI.Box (logicInsideRect, "", Skins.guiSkin.GetStyle (Skins.logicInside));
		}

		void RemoveNode (Event e)
		{
			if (selectionState == SelectionState.selected)
			{
				if (MouseKeysEvents.ControlCommandAltKey (KeyCode.X, e))
				{
					if ( ! isIdle)
					{
						RemoveNodeAction ();
					}
				}
			}
		}

		void RemoveNodeAction ()
		{
			if (EditorUtility.DisplayDialog ("Delete State",
				"Are you sure you want to delete this state? \n" +
				"\n" +
				"It is an irreversible action !",
				"Yes", "no"))
			{
				FreeTansitions ();

				RemoveLogics ();


				graph.RemoveNode (id);

				graph.SetEditingLogicToFalse ();

				ClassesNamesManager.RemoveName (graph.graphNameRacine + "_" + nodeName, true);

				CsScriptWriter.WriteStatesNamesEnumScript ();
			}
		}

		void RemoveLogics ()
		{
			for (int i = 0; i < logics.Count; i++)
			{
				RemoveLogic (i);

				i--;
			}
		}
	
		void FreeTansitions ()
		{
			FreeSources ();

			FreeDestinations ();
		}

		void FreeSources ()
		{
			for (int i = 0; i < sources.Count; i++)
			{
				if (sources [i] < 0 || sources [i] > graph.nodes.Count -1)
					continue;

				graph.nodes [sources [i]].RemoveDestination (id);
			}
		}

		void FreeDestinations ()
		{
			for (int i = 0; i < destinations.Count; i++)
			{
				if (destinations [i] < 0 || destinations [i] > graph.nodes.Count -1)
					continue;

				graph.nodes [destinations [i]].RemoveSource (id);
			}
		}

		/*
		void SetChoosenToConnectID ()
		{
			graph.choosenToConnectId = id;
		}*/


		public override void DrawTransitions ()
		{
			for (int i = 0; i < destinations.Count; i++)
			{
				DrawTransitionOut (destinations [i]);
			}
			/*
			for (int i = 0; i < sources.Count; i++)
			{
				DrawTransitionIn (sources [i]);
			}*/
		}

		void DrawTransitionOut (int dest)
		{
			if (dest < 0 || dest > graph.nodes.Count -1)
				return;

			Vector2 startCenter = rect.center;

			Vector2 endCenter = graph.nodes [dest].rect.center;

			Vector2 orientation = (endCenter - startCenter).normalized;

			Vector2 orthoNorm = new Vector2 ( - orientation.y, orientation.x);

			float orthoLength = rect.height * 0.1f;

			Vector2 ortho = orthoNorm * orthoLength;

			Vector2 start = startCenter + ortho;

			Vector2 end = endCenter + ortho;


			float transitionWidth = 5f;


			Color color = graph.linksColor;

			//color.a = 0.5f;

			Handles.DrawBezier (start, end, start + orientation, end - orientation, color,
				null, transitionWidth);


			Vector2 middle = 0.47f*start + 0.53f*end;

			Vector2 middle_1 = middle 
				- orientation * orthoLength
				+ orthoNorm * orthoLength * 1f;

			Vector2 middle_2 = middle 
				- orientation * orthoLength
				- orthoNorm * orthoLength * 1f;

			Handles.DrawBezier (middle, middle_1, middle_1, middle, color, null, transitionWidth);

			Handles.DrawBezier (middle, middle_2, middle_2, middle, color, null, transitionWidth);
		}

		void DrawTransitionIn (int sour)
		{
			if (sour < 0 || sour > graph.nodes.Count -1)
				return;

			Vector2 startCenter = graph.nodes [sour].rect.center;

			Vector2 endCenter = rect.center;

			Vector2 orientation = (endCenter - startCenter).normalized;

			Vector2 orthoNorm = new Vector2 ( - orientation.y, orientation.x);

			float orthoLength = rect.height * 0.1f;

			Vector2 ortho = orthoNorm * orthoLength;

			Vector2 start = startCenter + ortho;

			Vector2 end = endCenter + ortho;


			float transitionWidth = 5f;


			Color color = graph.linksColor;

			//color.a = 0.5f;


			Handles.DrawBezier (start, end, start + orientation, end - orientation, color,
				null, transitionWidth);



			Vector2 middle = 0.47f*start + 0.53f*end;

			Vector2 middle_1 = middle 
				- orientation * orthoLength
				+ orthoNorm * orthoLength * 1f;

			Vector2 middle_2 = middle 
				- orientation * orthoLength
				- orthoNorm * orthoLength * 1f;

			Handles.DrawBezier (middle, middle_1, middle_1, middle, color, null, transitionWidth);

			Handles.DrawBezier (middle, middle_2, middle_2, middle, color, null, transitionWidth);
		}


		public override void AddSource (int sourceToAd)
		{
			if (sources.Contains (sourceToAd))
				return;
			
			sources.Add (sourceToAd);
		}

		void CheckGotoState ()
		{
			destinations = new List<int> ();

			for (int i = 0; i < logics.Count; i++)
			{
				for (int j = 0; j < logics [i].nodes.Count; j++)
				{
					if (logics [i].nodes [j].logicType == LogicType.computeOrOperation)
					{
						if (logics [i].nodes [j].variableType == VariableType.Bool)
						{
							if (logics [i].nodes [j].computeBoolType == ComputeBoolType.goToState)
							{
								string goToStateFound = logics [i].nodes [j].currentStateNames.ToString ();

								for (int k = 0; k < graph.nodes.Count; k++)
								{
									if (graph.nodes [k].nodeName == goToStateFound)
									{
										AddDestination (k);
									}
								}
							}
						}
					}
				}
			}

			//if (logicType == LogicType.computeOrOperation)
			//{
			//	if (variableType == VariableType.Bool)
			//	{
			//		if (computeBoolType == ComputeBoolType.goToState)
			//		{
			//			return;
			//		}
			//	}
			//}
			//
			//for (int i = 0; i < logic.node.graph.nodes.Count; i++)
			//{
			//	if (logic.node.graph.nodes [i].nodeName == currentStateNames.ToString ())
			//	{
			//		logic.node.RemoveDestination (i);
			//	}
			//}
		}

		public override void AddDestination (int destinationToAdd)
		{
			if (destinations.Contains (destinationToAdd))
				return;

			for (int i = 0; i < graph.nodes.Count; i++)
			{
				if (graph.nodes [i] == this)
				{
					if (i == destinationToAdd)
						return;
				}
			}

			destinations.Add (destinationToAdd);
		}


		public override void RemoveSource (int sourceToRemove)
		{
			sources.Remove (sourceToRemove);
		}

		public override void RemoveDestination (int destinationToRemove)
		{
			destinations.Remove (destinationToRemove);
		}
	

		public override void DecreaseSourcesDestinationAtNodeRemove (int at)
		{
			DecreaseDestinationsAtNodeRemove (at);

			DecreaseSourcesAtNodeRemove (at);
		}

		void DecreaseDestinationsAtNodeRemove (int at)
		{
			if (at < 0 || at > graph.nodes.Count -1)
				return;

			for (int i = 0; i < destinations.Count; i++)
			{
				if (destinations [i] > at)
					destinations [i]--;
			}
		}

		void DecreaseSourcesAtNodeRemove (int at)
		{
			if (at < 0 || at > graph.nodes.Count -1)
				return;
		
			for (int i = 0; i < sources.Count; i++)
			{
				if (sources [i] > at)
					sources [i]--;
			}
		}
	

		void ShowHotKeys ()
		{
			showHotKeys = true;
		}

		bool showHotKeys = false;
		void ShowHotKeysAction ()
		{
			if (! showHotKeys)
				return;


			Vector2 showHotKeysRect_size = new Vector2 (500f, 600f);
			Rect showHotKeysRect = new Rect (editLogicRect.x + editLogicRect.width - showHotKeysRect_size.x, 
				editLogicRect.y + 90f, 
				showHotKeysRect_size.x, showHotKeysRect_size.y);

			Rect cadreRect = new Rect (showHotKeysRect.position + new Vector2 (-10f, -45f),
				new Vector2 (showHotKeysRect.size.x, showHotKeysRect.size.y));

			GUI.Box (cadreRect, "", Skins.guiSkin.GetStyle (Skins.leftUpMessageInfo));

			GUI.Box (showHotKeysRect, "alt + arrows : move selected node\narrows : move links end of selected node (when linking)\n" +
				"arrows + shift: move slowly links end of selected node (when linking)\n\nreturn (enter key) : done\n\n" +
				"alt + y : select all nodes\nalt + d : deselect all nodes\nalt + s : change selected node\n" +
				"alt + x : delete selected node\n\nalt + o : open options menu, of selected node or of graph (if any selected node)\n" +
				"alt + i : open field options menu of selected node\n\nalt + r : exit current operation\nalt + b : back\n\n" +
				"alt + l : link an output of selected node\nalt + p : link an input of selected node\n\n" +
				"alt + k : focus your nodes in the view\n\n" + 
				"Zoom:\n" +
				"cursor inside the node + mouse wheel: zoom out/in\n" +
				"cursor inside the workspace + alt/option + mouse wheel: zoom out/in for all nodes\n\n" +
				"Linking:\n" +
				"No need to drag, left click once in your input or output triangle\n" +
				"and Diamond will draw a link ended at your mouse cursor. \n" +
				"All you have to do is to move your mouse to the destination and\n" +
				"it will be linked if the types between input and output matches.\n" +
				"P.S. Since you don't need to drag for linking you can use your\n" +
				"middle mouse button to pan and to link with a far node for example.\n\n" +
				"Moving several nodes:\nLeft click and drag a rectangle to select nodes,\n" +
				"then hold alt/option key and move your nodes with arrow keys.",
				Skins.guiSkin.GetStyle (Skins.projectVariableName));

			if (GUI.Button (new Rect(cadreRect.position + new Vector2 (cadreRect.width-50f, 7f), 
				Vector2.one * 40f), "", 
				GetGuiStyle (Skins.forward)))
				showHotKeys = false;
		}

		bool callRenameLogic = false;

		void RenameLogic (object o_logicToRenameName)
		{
			logicToRenameName = o_logicToRenameName.ToString ();


			if ( ! string.IsNullOrEmpty (logicToRenameName))
			{
				logicName = logicToRenameName;
			}
			else if (string.IsNullOrEmpty (logicToRenameName))
			{
				if (LogicNode.Aux_InRange (editingLogicId, 0, logics.Count))
				{
					logicName = logics [editingLogicId].logicName;
				}
			}

			callRenameLogic = true;
		}

		string logicToRenameName = "";

		void RenameLogicAction ()
		{
			if ( ! callRenameLogic)
				return;
			
			Rect renamelogicRect = new Rect (editLogicRect.x + editLogicRect.width - rect.width * 0.75f, 
				editLogicRect.y + 20f, 
				rect.width * 0.75f, rect.height * 2f);

			if ( ! string.IsNullOrEmpty (logicToRenameName))
			{
				renamelogicRect = new Rect (rect.x + rect.width, rect.y, 
					rect.width * 0.75f, rect.height * 2f);
			}

			GUI.Box (renamelogicRect, "", Skins.guiSkin.GetStyle (Skins.node));

			Rect lableRectNormalized = new Rect (0.1f, 0.1f, 0.8f, 0.2f);

			GUI.Label (new Rect (
				renamelogicRect.x + renamelogicRect.width * lableRectNormalized.x, 
				renamelogicRect.y + renamelogicRect.height * lableRectNormalized.y,
				renamelogicRect.width * lableRectNormalized.width, 
				renamelogicRect.height * lableRectNormalized.height),
				"Logic Name", Skins.guiSkin.GetStyle (Skins.node));



			Rect textFieldRectNormalized = new Rect (0.1f, 0.4f, 0.8f, 0.2f);

			//GUI.SetNextControlName ("logicNameTextField" + id);
			logicName = GUI.TextField (new Rect (
				renamelogicRect.x + renamelogicRect.width * textFieldRectNormalized.x,
				renamelogicRect.y + renamelogicRect.height * textFieldRectNormalized.y,
				renamelogicRect.width * textFieldRectNormalized.width, 
				renamelogicRect.height * textFieldRectNormalized.height),
				logicName);

			//GUI.FocusControl ("logicNameTextField" + id);
					

			Rect buttonOkRectNormalized = new Rect (0.35f, 0.65f, 0.3f, 0.3f);

			if (GUI.Button (new Rect (
				renamelogicRect.x + renamelogicRect.width * buttonOkRectNormalized.x,
				renamelogicRect.y + renamelogicRect.height * buttonOkRectNormalized.y,
				renamelogicRect.width * buttonOkRectNormalized.width, renamelogicRect.height * buttonOkRectNormalized.height),
				"Ok", Skins.guiSkin.GetStyle (Skins.button))
				||
				MouseKeysEvents.KeyIsUp (KeyCode.Return, eGlobal))
			{
				logicName = StringTreatment.ScriptName (logicName);

				if (string.IsNullOrEmpty (logicName))
				{
					logicToRenameName = "";

					EditorUtility.DisplayDialog ("Invalid Name", Enums.invalidNameDialog, "Ok");

					return;
				}

				if (string.IsNullOrEmpty (logicToRenameName))
				{
					if (LogicNode.Aux_InRange (editingLogicId, 0, logics.Count))
					{
						if (logics [editingLogicId].logicName != logicName)
						{
							if (LogicsNames ().Contains (logicName))
							{
								logicToRenameName = "";

								EditorUtility.DisplayDialog ("Logic Same Name", logicSameNameMessage, "Ok");

								return;
							}
						}
					}
				}
				else if ( ! string.IsNullOrEmpty (logicToRenameName))
				{
					if (logicToRenameName != logicName)
					{
						if (LogicsNames ().Contains (logicName))
						{
							logicToRenameName = "";

							EditorUtility.DisplayDialog ("Logic Same Name", logicSameNameMessage, "Ok");

							return;
						}
					}
				}

				if (string.IsNullOrEmpty (logicToRenameName))
				{
					logics [editingLogicId].logicName = logicName;
				}
				else if ( ! string.IsNullOrEmpty (logicToRenameName))
				{
					if (LogicNode.Aux_InRange (Aux_IndexOfLogicOfName (logicToRenameName), 0, logics.Count))
					{
						logics [Aux_IndexOfLogicOfName (logicToRenameName)].logicName = logicName;
					}
				}
				callRenameLogic = false;
				logicToRenameName = "";
			}


			if ( ! renamelogicRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseUp)
					{
						callRenameLogic = false;
						logicToRenameName = "";
					}
				}
			}

			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.B, eGlobal))
			{
				callRenameLogic = false;
				logicToRenameName = "";
			}
		}


		bool callEditVariablesTypeColor = false;

		void EditVariablesTypeColor ()
		{
			callEditVariablesTypeColor = true;
		}

		public VariableType variableTypeForColor;

		void EditVariablesTypeColorAction ()
		{
			if ( ! callEditVariablesTypeColor)
				return;

			Rect drawingRect = new Rect (editLogicRect.x + editLogicRect.width - rect.width * 0.75f, 
				editLogicRect.y + 20f, 
				rect.width * 0.75f, rect.height * 3f);

			GUI.Box (drawingRect, "", Skins.guiSkin.GetStyle (Skins.node));

			variableTypeForColor = (VariableType)EditorGUI.EnumPopup (RectOperations.RatioToAbsolute (drawingRect,
				new Rect (0.1f, 0.26f, 0.8f, 0.13f)), variableTypeForColor);

			int variableTypeEnumIndex = LogicNode.GetVariableTypeEnumIndex (variableTypeForColor);

			Diamond.namesToSave.variableTypeColor [variableTypeEnumIndex] =
				EditorGUI.ColorField (RectOperations.RatioToAbsolute (drawingRect,
					new Rect (0.1f, 0.4f, 0.8f, 0.13f)), 
					Diamond.namesToSave.variableTypeColor [variableTypeEnumIndex]);

			if (GUI.Button (RectOperations.RatioToAbsolute (drawingRect,
				new Rect (0.1f, 0.65f, 0.8f, 0.13f)), "All to default"))
			{
				if (EditorUtility.DisplayDialog ("Reset Default", "Are you sure you want to reset all " +
					"Variable Types Colors to their default values?", "Yes", "No"))
				{
					int variableTypesCount = Diamond.namesToSave.variableTypeColor.Length;

					float chromStep = 1f / (float)variableTypesCount;

					for (int i = 0; i < Diamond.namesToSave.variableTypeColor.Length; i++)
					{
						Diamond.namesToSave.variableTypeColor [i] = Color.HSVToRGB (i*chromStep, 1f, 1f);
					}
				}
			}

			if ( ! drawingRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseUp)
					{
						callEditVariablesTypeColor = false;
						Auxiliaries.SaveAndRefreshAssetsForced ();
					}
				}
			}

			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.B, eGlobal))
			{
				callEditVariablesTypeColor = false;
				Auxiliaries.SaveAndRefreshAssetsForced ();
			}
		}


		CurrentStatesNames currentStateNames;

		bool DuplicateLogicOpened = false;

		void OpenDuplicateLogic (object uID_o)
		{
			logicToBeDuplicated_uID = uID_o.ToString ();

			DuplicateLogicOpened = true;
		}

		void DuplicateLogic ()
		{
			if ( ! DuplicateLogicOpened)
				return;

			Rect duplicatelogicRect = new Rect (editLogicRect.x + editLogicRect.width - rect.width * 1f, 
				editLogicRect.y + 20f, 
				rect.width * 1f, rect.height * 2f);

			GUI.Box (duplicatelogicRect, "", Skins.guiSkin.GetStyle (Skins.node));

			EditorGUI.LabelField (RectOperations.RatioToAbsolute (duplicatelogicRect,
				new Rect(0.2f, 0.05f, 0.6f, 0.3f)) , "To which state?", GetGuiStyle (Skins.logicNodeLabel)); 

			currentStateNames = (CurrentStatesNames)EditorGUI.EnumPopup (RectOperations.RatioToAbsolute (duplicatelogicRect,
				new Rect(0.2f, 0.35f, 0.6f, 0.3f)) , currentStateNames); 

			if (GUI.Button (RectOperations.RatioToAbsolute (duplicatelogicRect, new Rect(0.2f, 0.6f, 0.6f, 0.17f)), "Duplicate"))
			{
				DuplicateLogicAction (currentStateNames.ToString ());

				DuplicateLogicOpened = false;
			}

			if (GUI.Button (RectOperations.RatioToAbsolute (duplicatelogicRect, new Rect(0.2f, 0.8f, 0.6f, 0.17f)), "Cancel"))
			{
				DuplicateLogicOpened = false;
			}
		}

		void DuplicateLogicAction (string stateNameToDuplicateTo)
		{
			if (logics [editingLogicId].uniqueID != logicToBeDuplicated_uID)
				return;


			Logic logicToClone = logics [editingLogicId];

			Logic newLogic = AddLogicActionForDuplicate (logicToClone.logicName + "_copy", stateNameToDuplicateTo);

			if (newLogic == null)
				return;

			LogicsCopy (logicToClone, newLogic);


			logicToBeDuplicated_uID = "";
		}


		void LogicsCopy (Logic from_, Logic to)
		{
			for (int i = 0; i < from_.nodes.Count; i++)
			{
				LogicNode logicNodeToClone = from_.nodes [i];

				LogicNode newLogicNode = Auxiliaries.CreateLogicNodeForDuplication (
					logicNodeToClone.rect.position, to, logicNodeToClone.specialNodeType);

				LogicNodesCopy (logicNodeToClone, newLogicNode);
			}

			Auxiliaries.SaveAndRefreshAssetsForced ();
		}

		void LogicNodesCopy (LogicNode from_, LogicNode to)
		{
			//Logic toLogic = to.logic;
			//
			//string toUID = to.uniqueID;
			//
			//to = from_;
			//
			//to.logic = toLogic;
			//
			//to.uniqueID = toUID;


			to.nodeName = from_.nodeName;
			
			to.logicType = from_.logicType;
			
			to.variableType = from_.variableType;

			to.logicNodeExtensions = from_.logicNodeExtensions;

			to.materialType = from_.materialType;
			
			switch (to.variableType)
			{
			case VariableType.Bool:
				to.computeBoolType = from_.computeBoolType;
				break;
			
			
			
			case VariableType.boolsList:
				to.computeBoolListType = from_.computeBoolListType;
				break;
			
			case VariableType.Camera:
				to.computeCameraType = from_.computeCameraType;
				break;
			
			case VariableType.Color:
				to.computeColorType = from_.computeColorType;
				break;
			
			case VariableType.colorsList:
				to.computeColorListType = from_.computeColorListType;
				break;			
			
			case VariableType.componentCollider:
				to.computeColliderType = from_.computeColliderType;
				break;
			
			
			
			case VariableType.componentCollider2D:
				to.computeCollider2DType = from_.computeCollider2DType;
				break;
			
			case VariableType.componentNavMeshAgent:
				to.computeNavMeshAgentType = from_.computeNavMeshAgentType;
				break;
			
			case VariableType.componentParticleSystem:
				to.computeParticleSystemType = from_.computeParticleSystemType;
				break;
			
			case VariableType.componentRenderer:
				to.computeRendererType = from_.computeRendererType;
				break;
			
			case VariableType.componentRigidBody:
				to.computeRigidBodyType = from_.computeRigidBodyType;
				break;
			
			
			
			case VariableType.componentRigidBody2D:
				to.computeRigidBody2DType = from_.computeRigidBody2DType;
				break;
			
			case VariableType.componentTransform:
				to.computeTransformType = from_.computeTransformType;
				break;
			
			case VariableType.componentUIUnityText:
				to.computeUnityTextType = from_.computeUnityTextType;
				break;
			
			case VariableType.componentUIImage:
				to.computeUiImageType = from_.computeUiImageType;
				break;
			
			case VariableType.componentUIButton:
				to.computeUiButtonType = from_.computeUiButtonType;
				break;
			
			
			
			case VariableType.Float:
				to.computeFloatType = from_.computeFloatType;
				break;
			
			case VariableType.floatsList:
				to.computeFloatsListType = from_.computeFloatsListType;
				break;
			
			case VariableType.GameObject:
				to.computeGameObjectType = from_.computeGameObjectType;
				break;
			
			case VariableType.GameObjectList:
				to.computeGameObjectListType = from_.computeGameObjectListType;
				break;
			
			case VariableType.Int:
				to.computeIntType = from_.computeIntType;
				break;
			
			
			
			
			
			case VariableType.intsList:
				to.computeIntListType = from_.computeIntListType;
				break;
			
			case VariableType.Ray:
				to.computeRayType = from_.computeRayType;
				break;
			
			case VariableType.Ray2D:
				to.computeRay2DType = from_.computeRay2DType;
				break;
			
			case VariableType.String:
				to.computeStringType = from_.computeStringType;
				break;
			
			case VariableType.stringsList:
				to.computeStringsListType = from_.computeStringsListType;
				break;
			
			
			
			
			case VariableType.Vector2:
				to.computeVector2Type = from_.computeVector2Type;
				break;
			
			case VariableType.vector2List:
				to.computeVector2ListType = from_.computeVector2ListType;
				break;
			
			case VariableType.Vector3:
				to.computeVector3Type = from_.computeVector3Type;
				break;
			
			case VariableType.vector3List:
				to.computeVector3ListType = from_.computeVector3ListType;
				break;
			
			case VariableType.Vector4:
				to.computeVector4Type = from_.computeVector4Type;
				break;
			
			
			
			
			
			case VariableType.vector4List:
				to.computeVector4ListType = from_.computeVector4ListType;
				break;
			
			case VariableType.rect:
				to.computeRectType = from_.computeRectType;
				break;
			
			case VariableType.rectsList:
				to.computeRectListType = from_.computeRectListType;
				break;
			
			case VariableType.Matrix4x4:
				to.computeMatrix44Type = from_.computeMatrix44Type;
				break;
			
			case VariableType.Material:
				to.computeMaterialType = from_.computeMaterialType;
				break;
			
			
			
			case VariableType.materialsList:
				to.computeMaterialListType = from_.computeMaterialListType;
				break;
			
			case VariableType.Texture2D:
				to.computeTexture2DType = from_.computeTexture2DType;
				break;
			
			case VariableType.texture2DList:
				to.computeTexture2DListType = from_.computeTexture2DListType;
				break;
			
			case VariableType.Shader:
				to.computeShaderType = from_.computeShaderType;
				break;
			
			case VariableType.shadersList:
				to.computeShaderListType = from_.computeShaderListType;
				break;
			
			}
			
			for (int i = 0; i < from_.inputsSources.Length; i++)
			{
				string fromInputSource = from_.inputsSources [i];
			
				if (string.IsNullOrEmpty (fromInputSource))
					continue;
			
				if (fromInputSource [0] == 'v')
				{
					to.inputsSources [i] = fromInputSource;
			
					continue;
				}
			
				string fromSourceUID = from_.InOutAdressCurrentToLinkToUniqueID (fromInputSource);
			
				LogicNode fromSourceNode = from_.logic.LogicNodeByUniqueID (fromSourceUID);
			
				string toSourceUID = "";
			
				for (int j = 0; j < to.logic.nodes.Count; j++)
				{
					if (to.logic.nodes [j].nodeName == fromSourceNode.nodeName)
					{
						toSourceUID = to.logic.nodes [j].uniqueID;
			
						continue;
					}
				}
			
				if (string.IsNullOrEmpty (toSourceUID))
					continue;
			
				to.inputsSources [i] = toSourceUID + 
					(StringTreatment.SubtractWeak (fromInputSource,
						StringTreatment.BeforeThat (fromInputSource, '.')));
			}
		
		
			for (int i = 0; i < from_.inputsIDs.Length; i++)
			{
				InOuttIdToValue (to, from_, from_.inputsIDs [i]);
			}
			for (int i = 0; i < from_.outputsIDs.Length; i++)
			{
				InOuttIdToValue (to, from_, from_.outputsIDs [i]);
			}
		}

		void InOuttIdToValue (LogicNode to, LogicNode from_, string s)
		{
			string retValBefore_ = StringTreatment.BeforeThat (s, '_');
			if (StringTreatment.IsEndWith_List (retValBefore_))
			{
				switch (retValBefore_)
				{
				case Enums.boolsList_ID:
					to.boolsListValue = from_.boolsListValue;
					break;

				case Enums.colorsList_ID:
					to.colorsListValue = from_.colorsListValue;
					break;

				case Enums.intsList_ID:
					to.intsListValue = from_.intsListValue;
					break;

				case Enums.floatsList_ID:
					to.floatsListValue = from_.floatsListValue;
					break;

				case Enums.vector2List_ID:
					to.vector2ListValue = from_.vector2ListValue;
					break;

				case Enums.vector3List_ID:
					to.vector3ListValue = from_.vector3ListValue;
					break;

				case Enums.vector4List_ID:
					to.vector4ListValue = from_.vector4ListValue;
					break;

				case Enums.stringsList_ID:
					to.stringsListValue = from_.stringsListValue;
					break;

				case Enums.gameObjectsList_ID:
					to.gameObjectsListValue = from_.gameObjectsListValue;
					break;

				case Enums.materialList_ID:
					to.materialsListValue = from_.materialsListValue;
					break;

				case Enums.texture2DList_ID:
					to.texture2DListValue = from_.texture2DListValue;
					break;

				case Enums.shaderList_ID:
					to.shaderListValue = from_.shaderListValue;
					break;

				case Enums.rectList_ID:
					to.rectListValue = from_.rectListValue;
					break;
				}
			}

			switch (s)
			{
			case Enums.hit2D_centroid_ID:
				to.hit2D_centroid = from_.hit2D_centroid;
				break;

			case Enums.hit2D_distance_ID:
				to.hit2D_distance = from_.hit2D_distance;
				break;

			case Enums.hit2D_fraction_ID:
				to.hit2D_fraction = from_.hit2D_fraction;
				break;

			case Enums.hit2D_gameObject_ID:
				to.hit2D_gameObject = from_.hit2D_gameObject;
				break;

			case Enums.hit2D_normal_ID:
				to.hit2D_normal = from_.hit2D_normal;
				break;



			case Enums.hit2D_point_ID:
				to.hit2D_point = from_.hit2D_point;
				break;

			case Enums.boolValue_ID:
				to.boolValue = from_.boolValue;
				break;

			case Enums.boolValues_0_ID:
				to.boolValues [0] = from_.boolValues [0];
				break;

			case Enums.boolValues_1_ID:
				to.boolValues [1] = from_.boolValues [1];
				break;

			case Enums.boolsListEntire_ID:
				to.boolsListValue = from_.boolsListValue;
				break;



			case Enums.boolsListEntire0_ID:
				to.boolsListValues [0] = from_.boolsListValues [0];
				break;

			case Enums.boolsListEntire1_ID:
				to.boolsListValues [1] = from_.boolsListValues [1];
				break;

			case Enums.colorValue_ID:
				to.colorValue = from_.colorValue;
				break;

			case Enums.colorValues_0_ID:
				to.colorValues [0] = from_.colorValues [0];
				break;

			case Enums.colorValues_1_ID:
				to.colorValues [1] = from_.colorValues [1];
				break;




			case Enums.colorsListEntire_ID:
				to.colorsListValue = from_.colorsListValue;
				break;

			case Enums.colorsListEntire0_ID:
				to.colorsListValues [0] = from_.colorsListValues [0];
				break;

			case Enums.colorsListEntire1_ID:
				to.colorsListValues [1] = from_.colorsListValues [1];
				break;

			case Enums.intValue_ID:
				to.intValue = from_.intValue;
				break;

			case Enums.intValues_0_ID:
				to.intValues [0] = from_.intValues [0];
				break;



			case Enums.intValues_1_ID:
				to.intValues [1] = from_.intValues [1];
				break;

			case Enums.intValues_2_ID:
				to.intValues [2] = from_.intValues [2];
				break;

			case Enums.intsListEntire_ID:
				to.intsListValue = from_.intsListValue;
				break;

			case Enums.intsListEntire0_ID:
				to.intsListValues [0] = from_.intsListValues [0];
				break;

			case Enums.intsListEntire1_ID:
				to.intsListValues [0] = from_.intsListValues [0];
				break;




			case Enums.floatValue_ID:
				to.floatValue = from_.floatValue;
				break;

			case Enums.floatValues_0_ID:
				to.floatValues [0] = from_.floatValues [0];
				break;

			case Enums.floatValues_1_ID:
				to.floatValues [1] = from_.floatValues [1];
				break;

			case Enums.floatValues_2_ID:
				to.floatValues [2] = from_.floatValues [2];
				break;

			case Enums.floatsListEntire_ID:
				to.floatsListValue = from_.floatsListValue;
				break;



			case Enums.floatsListEntire0_ID:
				to.floatsListValues [0] = from_.floatsListValues [0];
				break;

			case Enums.floatsListEntire1_ID:
				to.floatsListValues [1] = from_.floatsListValues [1];
				break;

			case Enums.vector2Value_ID:
				to.vector2Value = from_.vector2Value;
				break;

			case Enums.vector2Values_0_ID:
				to.vector2Values [0] = from_.vector2Values [0];
				break;

			case Enums.vector2Values_1_ID:
				to.vector2Values [1] = from_.vector2Values [1];
				break;



			case Enums.vector2ListEntire_ID:
				to.vector2ListValue = from_.vector2ListValue;
				break;

			case Enums.vector2ListEntire0_ID:
				to.vector2ListValues [0] = from_.vector2ListValues [0];
				break;

			case Enums.vector2ListEntire1_ID:
				to.vector2ListValues [1] = from_.vector2ListValues [1];
				break;

			case Enums.vector3Value_ID:
				to.vector3Value = from_.vector3Value;
				break;

			case Enums.vector3Values_0_ID:
				to.vector3Values [0] = from_.vector3Values [0];
				break;



			case Enums.vector3Values_1_ID:
				to.vector3Values [1] = from_.vector3Values [1];
				break;

			case Enums.vector3ListEntire_ID:
				to.vector3ListValue = from_.vector3ListValue;
				break;

			case Enums.vector3ListEntire0_ID:
				to.vector3ListValues [0] = from_.vector3ListValues [0];
				break;

			case Enums.vector3ListEntire1_ID:
				to.vector3ListValues [1] = from_.vector3ListValues [1];
				break;

			case Enums.vector4Value_ID:
				to.vector4Value = from_.vector4Value;
				break;



			case Enums.vector4Values_0_ID:
				to.vector4Values [0] = from_.vector4Values [0];
				break;

			case Enums.vector4Values_1_ID:
				to.vector4Values [1] = from_.vector4Values [1];
				break;


			case Enums.vector4ListEntire_ID:
				to.vector4ListValue = from_.vector4ListValue;
				break;

			case Enums.vector4ListEntire0_ID:
				to.vector4ListValues [0] = from_.vector4ListValues [0];
				break;

			case Enums.vector4ListEntire1_ID:
				to.vector4ListValues [1] = from_.vector4ListValues [1];
				break;



			case Enums.rayDirection_ID:
				to.rayDirectionValueNotNormalized = from_.rayDirectionValueNotNormalized;
				break;

			case Enums.rayOrigin_ID:
				to.rayValueOrigin = from_.rayValueOrigin;
				break;

			case Enums.ray2DDirection_ID:
				to.ray2DDirectionValueNotNormalized = from_.ray2DDirectionValueNotNormalized;
				break;

			case Enums.ray2DOrigin_ID:
				to.ray2DValueOrigin = from_.ray2DValueOrigin;
				break;


			case Enums.stringValue_ID:
				to.stringValue = from_.stringValue;
				break;



			case Enums.stringValues_0_ID:
				to.stringValues [0] = from_.stringValues [0];
				break;

			case Enums.stringValues_1_ID:
				to.stringValues [1] = from_.stringValues [1];
				break;

			case Enums.stringsListEntire_ID:
				to.stringsListValue = from_.stringsListValue;
				break;

			case Enums.stringsListEntire0_ID:
				to.stringsListValues [0] = from_.stringsListValues [0];
				break;

			case Enums.stringsListEntire1_ID:
				to.stringsListValues [1] = from_.stringsListValues [1];
				break;




			case Enums.gameObjectValue_ID:
				to.gameObjectValue = from_.gameObjectValue;
				break;

			case Enums.gameObjectValues_0_ID:
				to.gameObjectValues [0] = from_.gameObjectValues [0];
				break;

			case Enums.gameObjectValues_1_ID:
				to.gameObjectValues [1] = from_.gameObjectValues [1];
				break;

			case Enums.gameObjectsListEntire_ID:
				to.gameObjectsListValue = from_.gameObjectsListValue;
				break;

			case Enums.gameObjectsListEntire0_ID:
				to.gameObjectsListValues [0] = from_.gameObjectsListValues [0];
				break;



			case Enums.gameObjectsListEntire1_ID:
				to.gameObjectsListValues [1] = from_.gameObjectsListValues [1];
				break;

			case Enums.doIt_ID:
				to.doIT = from_.doIT;
				break;

			case Enums.boundsCenterValue_ID:
				to.boundsCenterValue = from_.boundsCenterValue;
				break;

			case Enums.boundsExtentsValue_ID:
				to.boundsExtentsValue = from_.boundsExtentsValue;
				break;

			case Enums.boundsMaxValue_ID:
				to.boundsMaxValue = from_.boundsMaxValue;
				break;



			case Enums.boundsMinValue_ID:
				to.boundsMinValue = from_.boundsMinValue;
				break;

			case Enums.boundsSizeValue_ID:
				to.boundsSizeValue = from_.boundsSizeValue;
				break;

			case Enums.raycastHitBarycentricCoordinate_ID:
				to.raycastHitBarycentricCoordinate = from_.raycastHitBarycentricCoordinate;
				break;

			case Enums.raycastHitTriangleIndex_ID:
				to.raycastHitTriangleIndex = from_.raycastHitTriangleIndex;
				break;

			case Enums.raycastHitPoint_ID:
				to.raycastHitPoint = from_.raycastHitPoint;
				break;



			case Enums.raycastHitNormal_ID:
				to.raycastHitNormal = from_.raycastHitNormal;
				break;

			case Enums.raycastHitDistance_ID:
				to.raycastHitDistance = from_.raycastHitDistance;
				break;

			case Enums.raycastHitGameObject_ID:
				to.raycastHitGameObject = from_.raycastHitGameObject;
				break;

			case Enums.raycastHitLightmapCoord_ID:
				to.raycastHitLightmapCoord = from_.raycastHitLightmapCoord;
				break;

			case Enums.raycastHitTextureCoord_ID:
				to.raycastHittextureCoord = from_.raycastHittextureCoord;
				break;



			case Enums.raycastHitTextureCoord2_ID:
				to.raycastHittextureCoord2 = from_.raycastHittextureCoord2;
				break;

			case Enums.m44Value_entier_ID:
				to.m44Value_entier = from_.m44Value_entier;
				break;

			case Enums.m44Value_Input_0_entier_ID:
				to.m44Value_Input_entier [0] = from_.m44Value_Input_entier [0];
				break;

			case Enums.m44Value_Input_1_entier_ID:
				to.m44Value_Input_entier [1] = from_.m44Value_Input_entier [1];
				break;

			case Enums.m44Value_0_ID:
				to.m44Value [0] = from_.m44Value [0];
				break;



			case Enums.m44Value_1_ID:
				to.m44Value [1] = from_.m44Value [1];
				break;

			case Enums.m44Value_2_ID:
				to.m44Value [2] = from_.m44Value [2];
				break;

			case Enums.m44Value_3_ID:
				to.m44Value [3] = from_.m44Value [3];
				break;

			case Enums.m44Value_4_ID:
				to.m44Value [4] = from_.m44Value [4];
				break;

			case Enums.m44Value_5_ID:
				to.m44Value [5] = from_.m44Value [5];
				break;



			case Enums.m44Value_6_ID:
				to.m44Value [6] = from_.m44Value [6];
				break;

			case Enums.m44Value_7_ID:
				to.m44Value [7] = from_.m44Value [7];
				break;

			case Enums.m44Value_8_ID:
				to.m44Value [8] = from_.m44Value [8];
				break;

			case Enums.m44Value_9_ID:
				to.m44Value [9] = from_.m44Value [9];
				break;

			case Enums.m44Value_10_ID:
				to.m44Value [10] = from_.m44Value [10];
				break;




			case Enums.m44Value_11_ID:
				to.m44Value [11] = from_.m44Value [11];
				break;

			case Enums.m44Value_12_ID:
				to.m44Value [12] = from_.m44Value [12];
				break;

			case Enums.m44Value_13_ID:
				to.m44Value [13] = from_.m44Value [13];
				break;

			case Enums.m44Value_14_ID:
				to.m44Value [14] = from_.m44Value [14];
				break;

			case Enums.m44Value_15_ID:
				to.m44Value [15] = from_.m44Value [15];
				break;



			case Enums.m44ValueDeterminant_ID:
				to.m44ValueDeterminant = from_.m44ValueDeterminant;
				break;

			case Enums.m44ValueIsIdentity_ID:
				to.m44ValueIsIdentity = from_.m44ValueIsIdentity;
				break;

			case Enums.m44ValueInvertible_ID:
				to.m44ValueInvertible = from_.m44ValueInvertible;
				break;

			case Enums.materialValue_ID:
				to.materialValue = from_.materialValue;
				break;

			case Enums.materialValues_0_ID:
				to.materialValues [0] = from_.materialValues [0];
				break;



			case Enums.materialValues_1_ID:
				to.materialValues [1] = from_.materialValues [1];
				break;

			case Enums.materialListEntire_ID:
				to.materialsListValue = from_.materialsListValue;
				break;

			case Enums.materialListEntire0_ID:
				to.materialsListValues [0] = from_.materialsListValues [0];
				break;

			case Enums.materialListEntire1_ID:
				to.materialsListValues [1] = from_.materialsListValues [1];
				break;

			case Enums.texture2DValue_ID:
				to.texture2DValue = from_.texture2DValue;
				break;




			case Enums.texture2DValues_0_ID:
				to.texture2DValues [0] = from_.texture2DValues [0];
				break;

			case Enums.texture2DValues_1_ID:
				to.texture2DValues [1] = from_.texture2DValues [1];
				break;

			case Enums.texture2DListEntire_ID:
				to.texture2DListValue = from_.texture2DListValue;
				break;

			case Enums.texture2DListEntire0_ID:
				to.texture2DListValues [0] = from_.texture2DListValues [0];
				break;

			case Enums.texture2DListEntire1_ID:
				to.texture2DListValues [1] = from_.texture2DListValues [1];
				break;




			case Enums.shaderValue_ID:
				to.shaderValue = from_.shaderValue;
				break;

			case Enums.shaderValues_0_ID:
				to.shaderValues [0] = from_.shaderValues [0];
				break;

			case Enums.shaderValues_1_ID:
				to.shaderValues [1] = from_.shaderValues [1];
				break;

			case Enums.shaderListEntire_ID:
				to.shaderListValue = from_.shaderListValue;
				break;

			case Enums.shaderListEntire0_ID:
				to.shaderListValues [0] = from_.shaderListValues [0];
				break;



			case Enums.shaderListEntire1_ID:
				to.shaderListValues [1] = from_.shaderListValues [1];
				break;

			case Enums.OffMeshLinkData_activated_ID:
				to.OffMeshLinkData_activated = from_.OffMeshLinkData_activated;
				break;

			case Enums.OffMeshLinkData_startPosition_ID:
				to.OffMeshLinkData_startPosition = from_.OffMeshLinkData_startPosition;
				break;

			case Enums.OffMeshLinkData_endPosition_ID:
				to.OffMeshLinkData_endPosition = from_.OffMeshLinkData_endPosition;
				break;

			case Enums.OffMeshLinkData_valid_ID:
				to.OffMeshLinkData_valid = from_.OffMeshLinkData_valid;
				break;




			case Enums.NavMeshHit_distance_ID:
				to.NavMeshHit_distance = from_.NavMeshHit_distance;
				break;

			case Enums.NavMeshHit_hit_ID:
				to.NavMeshHit_hit = from_.NavMeshHit_hit;
				break;

			case Enums.NavMeshHit_mask_ID:
				to.NavMeshHit_mask = from_.NavMeshHit_mask;
				break;

			case Enums.NavMeshHit_normal_ID:
				to.NavMeshHit_normal = from_.NavMeshHit_normal;
				break;

			case Enums.NavMeshHit_position_ID:
				to.NavMeshHit_position = from_.NavMeshHit_position;
				break;



				//
				//case Enums.meshValue_ID:
				//	r = "meshValue";
				//	break;
				//
				//case Enums.meshValues_0_ID:
				//	r = "meshValues [0]";
				//	break;
				//
				//case Enums.meshValues_1_ID:
				//	r = "meshValues [1]";
				//	break;


			case Enums.rectValue_ID:
				to.rectValue = from_.rectValue;
				break;

			case Enums.rectValues_0_ID:
				to.rectValues [0] = from_.rectValues [0];
				break;

			case Enums.rectValues_1_ID:
				to.rectValues [1] = from_.rectValues [1];
				break;

			case Enums.rectListEntire_ID:
				to.rectListValue = from_.rectListValue;
				break;

			case Enums.rectListEntire0_ID:
				to.rectListValues [0] = from_.rectListValues [0];
				break;



			case Enums.rectListEntire1_ID:
				to.rectListValues [1] = from_.rectListValues [1];
				break;
			}

		}

		const string logicSameNameMessage = "A Logic with the same name already exists in this state,\n" +
			"please choose an another name";

		void AddLogic ()
		{
			Rect addlogicRect = new Rect (rect.x + rect.width, rect.y, 
				                    rect.width * 0.75f, rect.height * 2f);
			
			GUI.Box (addlogicRect, "", Skins.guiSkin.GetStyle (Skins.node));

			Rect lableRectNormalized = new Rect (0.1f, 0.1f, 0.8f, 0.2f);

			GUI.Label (new Rect (
				addlogicRect.x + addlogicRect.width * lableRectNormalized.x, 
				addlogicRect.y + addlogicRect.height * lableRectNormalized.y,
				addlogicRect.width * lableRectNormalized.width, 
				addlogicRect.height * lableRectNormalized.height),
				"Logic Name", Skins.guiSkin.GetStyle (Skins.node));



			Rect textFieldRectNormalized = new Rect (0.1f, 0.4f, 0.8f, 0.2f);

			logicName = GUI.TextField (new Rect (
				addlogicRect.x + addlogicRect.width * textFieldRectNormalized.x,
				addlogicRect.y + addlogicRect.height * textFieldRectNormalized.y,
				addlogicRect.width * textFieldRectNormalized.width, 
				addlogicRect.height * textFieldRectNormalized.height),
				logicName);


			Rect buttonOkRectNormalized = new Rect (0.35f, 0.65f, 0.3f, 0.3f);

			if (GUI.Button (new Rect (
				addlogicRect.x + addlogicRect.width * buttonOkRectNormalized.x,
				addlogicRect.y + addlogicRect.height * buttonOkRectNormalized.y,
				addlogicRect.width * buttonOkRectNormalized.width, addlogicRect.height * buttonOkRectNormalized.height),
				"Ok", Skins.guiSkin.GetStyle (Skins.button))
				||
				MouseKeysEvents.KeyIsUp (KeyCode.Return, eGlobal))
			{
				logicName = StringTreatment.ScriptName (logicName);

				if (string.IsNullOrEmpty (logicName))
				{
					EditorUtility.DisplayDialog ("Invalid Name", Enums.invalidNameDialog, "Ok");

					return;
				}

				if (LogicsNames ().Contains (logicName))
				{
					EditorUtility.DisplayDialog ("Logic Same Name", logicSameNameMessage, "Ok");

					return;
				}

				AddLogicAction (logicName);
			}

	
			if ( ! addlogicRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseUp)
					{
						callAddLogic = false;
					}
				}
			}

			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.B, eGlobal))
			{
				callAddLogic = false;
			}
		}

		List <string> LogicsNames ()
		{
			List <string> r = new List<string> ();

			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i] == null)
					continue;

				r.Add (logics [i].logicName);
			}

			return r;
		}

		Logic AddLogicActionForDuplicate (string logicName, string stateNameToDuplicateTo)
		{
			callAddLogic = false;

			Logic logicTmp = ScriptableObject.CreateInstance <Logic> ();

			if (logicTmp == null)
				return null;

			logicName = StringTreatment.ScriptName (logicName);



			/*if (ClassesNamesManager.AddNewName (logicName))
			{
				logicTmp.Init (logicName, this);
			}*/


			//	logicTmp.Init (logicName, this);

			for (int i = 0; i < graph.nodes.Count; i++)
			{
				if (stateNameToDuplicateTo == graph.nodes [i].nodeName)
				{
					logicTmp.Init (logicName, graph.nodes [i]);

					break;
				}
			}


			SetCleoDragabls ();

			return logicTmp;
		}

		void AddLogicAction (string logicName)
		{
			callAddLogic = false;

			Logic logicTmp = ScriptableObject.CreateInstance <Logic> ();

			if (logicTmp == null)
				return;

			logicName = StringTreatment.ScriptName (logicName);



			/*if (ClassesNamesManager.AddNewName (logicName))
			{
				logicTmp.Init (logicName, this);
			}*/

			logicTmp.Init (logicName, this);

			SetCleoDragabls ();
		}


		void EditLogic ()
		{
			if (editingLogicId < 0 || editingLogicId > logics.Count -1)
				return;
			
			int i = editingLogicId;

			float gap = 10f;

			Vector2 editLogicRectSizesNormalized = new Vector2 (1f, 1f);

			Vector2 editLogicRectPosition = new Vector2 ();

			Vector2 editLogicRectPositionMax = new Vector2 (
				workSpaceRectGlobal.x + 0.5f * gap,
				workSpaceRectGlobal.y);

			Vector2 editLogicRectPositionMin = new Vector2 (
				rect.x + rect.width, rect.y);

			if (logics [i].rectMaximized)
				editLogicRectPosition = editLogicRectPositionMax;
			else
				editLogicRectPosition = editLogicRectPositionMin;

			/*
			Vector2 editLogicRectPositionMove = editLogicRectPositionMax -
			                                    editLogicRectPositionMin;*/

			editLogicRect = new Rect (editLogicRectPosition.x, editLogicRectPosition.y, 
				editLogicRectSizesNormalized.x*workSpaceRectGlobal.width,
				editLogicRectSizesNormalized.y*workSpaceRectGlobal.height);

			GUI.Box (editLogicRect, logics [i].logicName, Skins.guiSkin.GetStyle (Skins.view));
		
			Skins.DrawSeparator (new Rect (editLogicRect.x, editLogicRect.y, editLogicRect.width, Skins.separatorThickness));


			Vector2 backRectPosition = new Vector2 ();

			if (logics [i].rectMaximized)
				backRectPosition = new Vector2(
					editLogicRectPosition.x + gap, 
					editLogicRectPosition.y + gap + 10f);
			else
				backRectPosition = new Vector2(rect.x + rect.width + rect.width * 0.1f,
					rect.y + rect.height * 0.2f);

			Rect backRect = new Rect (backRectPosition.x, backRectPosition.y, 
				rect.height * 0.8f, 
				rect.height * 0.8f);

			/*Rect maxMinRect = new Rect (backRect.x + backRect.width + rect.width * 0.1f,
				                  backRect.y,
				rect.height * 0.4f,
				                  rect.height * 0.4f);*/

			if (GUI.Button (backRect, "", Skins.guiSkin.GetStyle (Skins.back)))
			{
				StopEditingLogic ();
			}
			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.B, eGlobal))
			{
				StopEditingLogic ();
			}
			/*
			if (GUI.Button (maxMinRect, "", Skins.guiSkin.GetStyle (Skins.button)))
			{
				logics [i].rectMaximized = !logics [i].rectMaximized;


				if (logics [i].rectMaximized)
				{
					for (int n = 0; n < logics [i].nodes.Count; n++)
					{
						logics [i].nodes [n].ParentNodeMove (editLogicRectPositionMove);
					}

					graph.SetEditingLogic (this);
				}
				else
				{
					for (int n = 0; n < logics [i].nodes.Count; n++)
					{
						logics [i].nodes [n].ParentNodeMove ( - editLogicRectPositionMove);
					}
				}					
			}
*/


			string logicSignature = graph.graphNameRacine + StringTreatment.rArrow 
				+ nodeName + StringTreatment.rArrow 
				+ logics [i].logicName;

			Rect logicSignatureRect = new Rect (backRect.x + backRect.width*1.3f,
				backRect.y, 500f, backRect.height);

			EditorGUI.LabelField (logicSignatureRect, logicSignature, 
				Skins.guiSkin.GetStyle (Skins.logicSignature));



			ProjectVariable.logic = logics [i];

			Rect projectVariablesRect = new Rect (backRect.position + new Vector2 (0f, backRect.height + 20f), 
				new Vector2 (200f, editLogicRect.height));

			logics [i].LogicUpdate (eGlobal, editLogicRect, projectVariablesRect);

			DrawProjectVariables (projectVariablesRect);

			logicOptionsRect = new Rect (backRect.x + editLogicRect.width - 4f*gap, backRect.y,
				gap, 3f*gap);


			DrawLogicOptionsButton (logicOptionsRect, logics [i]);


			DrawPlayPauseButton ();


			DuplicateLogic ();


			RenameLogicAction ();

			EditVariablesTypeColorAction ();

			ShowHotKeysAction ();
		}

		void DrawPlayPauseButton ()
		{
			if (editingLogicId < 0 || editingLogicId > logics.Count-1)
				return;
			
			float gap = 33f;

			Rect playPauseButtonRect = new Rect (
				editLogicRect.x + 0.5f*editLogicRect.width - gap, 2f*gap, 
				gap, gap);
			
			if (GUI.Button (playPauseButtonRect, "", GetGuiStyle (
				logics [editingLogicId].playing? Skins.pauseButton: Skins.playButton)))
			{
				logics [editingLogicId].playing = ! logics [editingLogicId].playing;
			}
		}

		string logicToBeDuplicated_uID = "";

		Rect logicOptionsRect;

		void DrawLogicOptionsButton (Rect logicOptionsRect, Logic currentEditingLogic)
		{
			if (GUI.Button (logicOptionsRect, "", GetGuiStyle (Skins.dotesOptions)))
			{
				GenericMenu menu = new GenericMenu ();

				menu.AddItem (new GUIContent ("Rename Logic"), false, RenameLogic, "");

				menu.AddItem (new GUIContent ("Duplicate Logic"), false, OpenDuplicateLogic, currentEditingLogic.uniqueID);

				menu.AddItem (new GUIContent ("Edit Variables type color"), false, EditVariablesTypeColor);

				menu.AddItem (new GUIContent ("Show hotkeys"), false, ShowHotKeys);

				menu.ShowAsContext ();
			}
		}


		void DrawProjectVariables (Rect projectVariablesRect)
		{
			GUI.Box (projectVariablesRect, "", GetGuiStyle (Skins.viewProjectVariables));

			if (Diamond.projectVariables == null)
				return;

			Diamond.projectVariables.UpdateProjectVariables ();
			
			for (int i = 0; i < Diamond.projectVariables.projectVariablesToShow.Count; i++)
			{
				Diamond.projectVariables.projectVariablesToShow [i].UpdateProjectVariable (eGlobal, projectVariablesRect);
			}

			//for (int i = 0; i < Diamond.projectVariables.projectVariables.Count; i++)
			//{
			//	Diamond.projectVariables.projectVariables [i].UpdateProjectVariable (eGlobal, projectVariablesRect);
			//}

			DrawProjectVariablesControlBar (projectVariablesRect);
		}

		void DrawProjectVariablesControlBar (Rect projectVariablesRect)
		{
			Rect projectVariablesControlBarRect = new Rect (projectVariablesRect.position, new Vector2 (250f, 50f));

			GUI.Box (projectVariablesControlBarRect, "", GetGuiStyle (Skins.nodeSelected));

			Rect pvcbNameRect = RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.09f, 0.17f, 0.8f, 0.1f));

			EditorGUI.LabelField (pvcbNameRect, "Project Variables", GetGuiStyle (Skins.logicNodeName));

			if(GUI.Button (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (-0.02f, 0.48f, 0.13f, 0.4f)), "", GetGuiStyle (Skins.down)))
			{
				ProjectVariable.ChangeGlobalIndex (false);
			}

			if(GUI.Button (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.07f, 0.48f, 0.13f, 0.4f)), "", GetGuiStyle (Skins.up)))
			{
				ProjectVariable.ChangeGlobalIndex (true);
			}

			if(GUI.Button (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.17f, 0.48f, 0.085f, 0.4f)), "", GetGuiStyle (Skins.add)))
			{
				if (editingLogicId > -1 && editingLogicId < logics.Count)
				{
					logics [editingLogicId].OpenAddProjectVariableMenu ();
				}
			}

			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.26f, 0.48f, 0.1f, 0.4f)), "Shown:", GetGuiStyle (Skins.filterLabel));
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.43f, 0.48f, 0.1f, 0.4f)), Diamond.projectVariables.projectVariablesToShow.Count.ToString (), 
				GetGuiStyle (Skins.filterLabel));

			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.50f, 0.48f, 0.1f, 0.4f)), "Total:", GetGuiStyle (Skins.filterLabel));
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (0.63f, 0.48f, 0.1f, 0.4f)), Diamond.projectVariables.projectVariables.Count.ToString (), 
				GetGuiStyle (Skins.filterLabel));


			DrawProjectVariablesFilter (projectVariablesControlBarRect);
		}

		float filterPanelHeight = 8.25f;
		void DrawProjectVariablesFilter (Rect projectVariablesControlBarRect)
		{
			if (Diamond.projectVariables == null)
				return;

			float labelRight = 0.77f;

			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f, 0.1f, 0.4f)), "Filter:", GetGuiStyle (Skins.filterLabel));

			Diamond.projectVariables.showFilter = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight+0.15f, 0.5f, 0.1f, 0.4f)), Diamond.projectVariables.showFilter);

			if ( ! Diamond.projectVariables.showFilter)
				return;



			float toggRight = labelRight + 0.34f;
			float g = 0.35f;
			int n = 1;
			float ng = (float)n*g;
			GUI.Box (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+g, 0.4f, filterPanelHeight*g)), "", GetGuiStyle (Skins.nodeSelected));

			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show All:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showAll = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showAll);

			if (Diamond.projectVariables.showAll)
			{
				Diamond.projectVariables.ActiveAllFilters ();

				filterPanelHeight = 1.35f;

				return;
			}


			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Hide All:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.hideAll = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.hideAll);

			if (Diamond.projectVariables.hideAll)
			{
				Diamond.projectVariables.DiactiveAllFilters ();
			}

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Bool:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showBool = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showBool);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Float:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showFloat = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showFloat);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Int:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showInt = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showInt);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show String:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showString = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showString);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Vector2:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showVector2 = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showVector2);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Vector3:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showVector3 = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showVector3);

			n++;
			ng = (float)n*g;
			EditorGUI.LabelField (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (labelRight, 0.48f+ng + 0.1f, 0.1f, 0.4f)), "Show Vector4:", GetGuiStyle (Skins.filterLabel));
			Diamond.projectVariables.showVector4 = EditorGUI.Toggle (RectOperations.RatioToAbsolute (projectVariablesControlBarRect,
				new Rect (toggRight, 0.5f+ng + 0.1f, 0.1f, 0.4f)), Diamond.projectVariables.showVector4);
		
			Diamond.projectVariables.DiactiveHideAll ();

			filterPanelHeight = (float)n + 0.25f;
		}

		GUIStyle GetGuiStyle (string styleName)
		{
			return Skins.guiSkin.GetStyle (styleName);
		}


		void StopEditingLogic ()
		{
			callEditLogic = false;

			editingLogic = false;

			editingLogicId = -1;

			graph.SetEditingLogicToFalse ();
		}
	
		void DeleteLogic (object iS)
		{
			if (EditorUtility.DisplayDialog ("Delete Logic",
				"Are you sure you want to delete this logic? \n" +
				"\n" +
				"It is an irreversible action !",
				"Yes", "no"))
			{
				int i = int.Parse (iS.ToString ());
			
				RemoveLogic (i);
			}
		}

		void RemoveLogic (int i)
		{
			for (int j = 0; j < logics [i].nodes.Count; j++)
			{
				if (logics [i].nodes[j] == null)
					continue;

				switch (logics [i].nodes[j].logicType)
				{
				case LogicType.computeOrOperation:
					switch (logics [i].nodes[j].variableType)
					{
					case VariableType.Bool:
						switch (logics [i].nodes[j].computeBoolType)
						{
						case ComputeBoolType.goToState:
							for (int k = 0; k < logics [i].node.graph.nodes.Count; k++)
							{
								if (logics [i].node.graph.nodes [k].nodeName == logics [i].nodes[j].currentStateNames.ToString ())
								{
									logics [i].node.RemoveDestination (k);
								}
							}
							break;
						}
						break;
					}
					break;
				}
			}

			Logic logicTmp = logics [i];

			logics.Remove (logics [i]);

			DecreaseLogicIDOnLogicDelete ();

			logicTmp.RemoveLogicNodes ();


			UnityEngine.Object.DestroyImmediate (logicTmp, true);

			SetCleoDragabls ();
		}

		void DecreaseLogicIDOnLogicDelete (int at)
		{
			if (at < 0 || at > logics.Count - 2)
				return;
			
			for (int i = at + 1; i < logics.Count; i++) 
			{
				logics [i].id--;
			}
		}

		void DecreaseLogicIDOnLogicDelete ()
		{
			for (int i = 0; i < logics.Count; i++)
			{
				logics [i].id = i;
			}
		}

		void SetEditingLogic ()
		{
			if ( ! callEditLogic)
				return;

			if (selectionState == SelectionState.selected)
				graph.SetEditingLogic (this);
		}

		#region aux
		int Aux_IndexOfLogicOfName (string lnam)
		{
			int r = -1;

			for (int i = 0; i < logics.Count; i++)
			{
				if (logics [i].logicName == lnam)
				{
					r = i;

					break;
				}
			}

			return r;
		}
		#endregion aux
	}
}
