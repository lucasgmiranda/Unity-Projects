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
	/// Logic.
	/// The logic is the graph handling the logic nodes. The Logic Nodes are the building blocks of a 
	/// Diamond Graph, in the Logic Nodes the user, define what he wants to do.
	/// </summary>
	[Serializable]
	public class Logic : ScriptableObject 
	{
		public string logicName;

		public int id;

		public Node node;

		//LogicNode logicNodeSelected;

		public List <LogicNode> nodes;

		Vector2 rightClickPosition;

		public bool rectMaximized;


		public string inOutAdressCurrentToLink;


		public string uniqueID = "";


		public bool playing = false;


		public Vector2 nodesRectsCenter = new Vector2 ();

		public void Init (
			string setLogicName, 
			Node setNode)
		{
			uniqueID = DatesTimesAndFrequences.DateTimeNow ();
			
			logicName = setLogicName;

			name = "Scriptable logic";

			node = setNode;

			node.logics.Add (this);

			id = node.logics.Count -1;

			//logicNodeSelected = null;

			nodes = new List<LogicNode> ();

			rightClickPosition = new Vector2 ();

			rectMaximized = true;

			//doingTransitionInOut = "";

			inOutAdressCurrentToLink = "";

			InitVariableTypeColor ();

			AssetDatabase.AddObjectToAsset (this, node);
		}


		void AllLogicNodesUpdateUniqueID ()
		{
			string _4RandDigites = DatesTimesAndFrequences._4RandomDigitsToString ();

			for (int i = 0; i < nodes.Count; i++)
			{
				nodes [i].Update_4RandDigitesByLogicDuplication (_4RandDigites);
			}
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

		Event eGlobal;

		public Rect editLogicRectGlobal;

		public Rect zoomedEditingLogicRect = new Rect ();
		public float zoom = 0f;
		public const float zoomMin = 0f;
		public const float zoomMax = 0.1f;
		public const float zoomSpeed = 0.005f;

		void Zoom ()
		{
			float maxGlobalRectDim = Mathf.Max (editLogicRectGlobal.width, editLogicRectGlobal.height); 
	
			float zoomAbs = zoom * maxGlobalRectDim;

			zoomedEditingLogicRect = Mezanix.Aux.Rectan.Zoom (editLogicRectGlobal, zoomAbs);

			//Drawer.DrawRectBorders (zoomedEditingLogicRect, Color.cyan, 3f);

			if (! eGlobal.alt)
				return;

			if (eGlobal.type != EventType.ScrollWheel)
				return;



			if (eGlobal.delta.y > 0f)
			{
				zoom += zoomSpeed;
			}
			else
			{
				zoom -= zoomSpeed;
			}
			zoom = Mathf.Clamp (zoom, zoomMin, zoomMax);
		}

		public void LogicUpdate (Event e, Rect editLogicRect, Rect projectVariablesRect)
		{
			RepairNodesList ();

			eGlobal = e;
			editLogicRectGlobal = editLogicRect;

			MouseMovedForZoom ();

			Zoom ();

			NodesUpdate (e, editLogicRect);
			//NodesUpdate (e, editLogicRect, true);




			EventProcess (e, editLogicRect, projectVariablesRect);



			//DrawCurrentTransition (e);


			DrawDragRect (e);

			HotKeys (e, editLogicRect);

			ComputeNodesRectCenter (editLogicRect);

			InitVariableTypeColor ();
		}

		void ComputeNodesRectCenter (Rect elrect)
		{
			nodesRectsCenter = LogicNode.Aux_RectsCenter (LogicNode.Aux_LogicToNodesRects (this, elrect),
				elrect.center);
		}

		void InitVariableTypeColor ()
		{
			int variableTypesCount = Enum.GetNames (typeof (VariableType)).Length;

			if (variableTypesCount == Diamond.namesToSave.variableTypeColor.Length)
				return;

			Color [] oldVariableTypeColor = Diamond.namesToSave.variableTypeColor;

			Diamond.namesToSave.variableTypeColor = new Color[variableTypesCount];

			float chromStep = 1f / (float)variableTypesCount;

			for (int i = 0; i < Diamond.namesToSave.variableTypeColor.Length; i++)
			{
				if (i < oldVariableTypeColor.Length)
				{
					Diamond.namesToSave.variableTypeColor [i] = oldVariableTypeColor [i];
				}
				else
				{
					Diamond.namesToSave.variableTypeColor [i] = Color.HSVToRGB (i*chromStep, 1f, 1f);
				}
			}

			Debug.Log ("Diamond.namesToSave.variableTypeColor has been initialized");
		}

		void HotKeys (Event e, Rect elRect)
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
			else if (MouseKeysEvents.ControlCommandAltKey (KeyCode.K, e))
			{
				FocusFirst ();
			}

			OpenLogicNodeOptionsMenuByHotKeys ();
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


		void OpenLogicNodeOptionsMenuByHotKeys ()
		{
			if (MouseKeysEvents.ControlCommandAltKey (KeyCode.O, eGlobal))
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					if (nodes [i].selectionState == SelectionState.selected)
					{
						nodes [i].OpenOptionsMenu ();

						break;
					}
				}
			}
		}

		void NodesUpdate (Event e, Rect editLogicRect, bool noCatch)
		{
			if (nodes.Count == 0)
				return;
			
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes [i].NodeUpdate (e, editLogicRect);
			}
		}

		void NodesUpdate (Event e, Rect editLogicRect)
		{
			if (nodes.Count == 0)
				return;

			//Exception exc_g = null;

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i] == null)
					continue;

				try
				{
					nodes [i].NodeUpdate (e, editLogicRect);
				}
				catch (AccessViolationException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (ActionOnDotNetUnhandledException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (AndroidJavaException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (AppDomainUnloadedException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (ApplicationException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (ArgumentException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (ArgumentNullException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (ArgumentOutOfRangeException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (ArithmeticException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (ArrayTypeMismatchException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (BadImageFormatException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (CannotUnloadAppDomainException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (ContextMarshalException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (DataMisalignedException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (DivideByZeroException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (DllNotFoundException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (DuplicateWaitObjectException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (EntryPointNotFoundException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (Exception exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (Exception exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (ExecutionEngineException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (ExitGUIException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (exception exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (FieldAccessException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (FormatException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (IndexOutOfRangeException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (InsufficientMemoryException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (InvalidOperationException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (InvalidCastException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (InvalidProgramException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (InvalidTimeZoneException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (KeyNotFoundException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (MemberAccessException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (MethodAccessException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (MissingComponentException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (MissingFieldException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (MissingMemberException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (MissingMethodException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (MissingReferenceException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (MulticastNotSupportedException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (NotFiniteNumberException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (NotImplementedException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (NotSupportedException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (NullReferenceException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (ObjectDisposedException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (OperationCanceledException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (OutOfMemoryException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (OverflowException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}

				//catch (PlatformNotSupportedException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (PlayerPrefsException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (RankException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}

				catch (StackOverflowException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				catch (SystemException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (TimeoutException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				catch (TimeZoneNotFoundException exc)
				{
					Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
					Debug.LogWarning (exc.Message);
				}
				//catch (TypeInitializationException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (TypeLoadException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (TypeUnloadedException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UnassignedReferenceException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UnauthorizedAccessException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UnhandledExceptionEventArgs exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UnhandledExceptionEventHandler exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UnityException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}
				//catch (UriFormatException exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}

				//catch (WebGLExceptionSupport exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}


				//catch (System.Exception exc)
				//{
				//	Debug.LogWarning ("Exception catched in the Logic Node: " + nodes [i].nodeName);
				//	Debug.LogWarning (exc.Message);
				//}

			}
		}

		void EventProcess (Event e, Rect editLogicRect, Rect projectVariablesRect)
		{
			if (editLogicRect.Contains (e.mousePosition))
			{
				if ( ! projectVariablesRect.Contains (e.mousePosition))
				{
					if (e.button == 1)
					{
						rightClickPosition = e.mousePosition;

						if (e.type == EventType.MouseDown)
						{
							GenericMenu menu = new GenericMenu ();

							menu.AddItem (new GUIContent (Enums.addLogicNode), false,
								RightClickFunction, Enums.addLogicNode);

							//menu.AddItem (new GUIContent (Enums.add_Terra_LogicNode), false,
							//	RightClickFunction, Enums.add_Terra_LogicNode);
							//
							//menu.AddItem (new GUIContent (Enums.add_Wood_LogicNode), false,
							//	RightClickFunction, Enums.add_Wood_LogicNode);

							menu.ShowAsContext ();

							e.Use ();
						}
					}
				}
				else if (projectVariablesRect.Contains (e.mousePosition))
				{
					if (e.button == 1)
					{
						rightClickPosition = e.mousePosition;

						if (e.type == EventType.MouseDown)
						{
							OpenAddProjectVariableMenu ();

							//e.Use ();
						}
					}
				}
			}
		}

		public void OpenAddProjectVariableMenu ()
		{
			GenericMenu menu = new GenericMenu ();

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_bool), false,
				RightClickFunction, Enums.addProjectVariable_bool);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_float), false,
				RightClickFunction, Enums.addProjectVariable_float);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_int), false,
				RightClickFunction, Enums.addProjectVariable_int);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_string), false,
				RightClickFunction, Enums.addProjectVariable_string);


			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_vector2), false,
				RightClickFunction, Enums.addProjectVariable_vector2);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_vector3), false,
				RightClickFunction, Enums.addProjectVariable_vector3);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_vector4), false,
				RightClickFunction, Enums.addProjectVariable_vector4);

			menu.AddItem (new GUIContent (Enums.addProjectVariable + "/" + Enums.addProjectVariable_gameObject), false,
				RightClickFunction, Enums.addProjectVariable_gameObject);
			
			menu.ShowAsContext ();
		}

		void RightClickFunction (object obj)
		{
			string choosen = obj.ToString ();

			Auxiliaries.CreateNode (choosen, rightClickPosition, this);

			UndoRedoAddVersion ();
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
				SelectNodesInside (dragRect);

				dragRect = new Rect ();
			}
		}

		public Vector2 linkExtremity = new Vector2 (50f, 50f);

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
				if (nodes [i] == null)
					continue;

				if (nodes [i].selectionState == SelectionState.selected)
				{
					r = false;

					break;
				}
			}

			return r;
		}



		void UndoRedoAddVersion ()
		{
			return;
		}
	

		public LogicNode LogicNodeByUniqueID (string ui)
		{
			LogicNode r = null;

			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].uniqueID == ui)
				{
					r = nodes [i];

					break;
				}
			}

			return r;
		}

		public void RemoveLogicNodes ()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				RemoveLogicNode (i);

				i--;
			}
		}

		void RemoveLogicNode (int i)
		{
			if (i < 0 || i > nodes.Count -1)
				return;
			
			LogicNode logicNodeTmp = nodes [i];

			if (logicNodeTmp != null)
			{
				logicNodeTmp.RemoveNode ();

				nodes.Remove (nodes [i]);

				UnityEngine.Object.DestroyImmediate (logicNodeTmp, true);
			}
		}

		public void RemoveLogicNode (LogicNode nodeToRemove)
		{
			LogicNode logicNodeTmp = nodeToRemove;

			nodes.Remove (nodeToRemove);

			UnityEngine.Object.DestroyImmediate (logicNodeTmp, true);
		}

		public void LogicNodeSelected (LogicNode node)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i] != node)
				{
					nodes [i].ChangeSelectionState (SelectionState.notSelected);

					continue;
				}

				//nodeSelected = node;
			}
		}

		void FocusFirst ()
		{
			Vector2 focusPos = new Vector2 (300f, 100f);

			if (nodes.Count > 0)
			if (nodes [0] != null)
			{
				MoveAllNodes (focusPos - nodes [0].rect.position);
			}
		}

		void MoveAllNodes (Vector2 dist)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes [i].rect.position += dist;

				nodes [i].SetNormalizedPosition ();
			}
		}
	
		Vector2 mouseLastPositionForZoom;
		void MouseMovedForZoom ()
		{
			mouseMovedForZoom = Mezanix.Aux.Inp.MouseMoved (ref mouseLastPositionForZoom, eGlobal, 100f);
		}
		public bool mouseMovedForZoom = false;
	}
}
