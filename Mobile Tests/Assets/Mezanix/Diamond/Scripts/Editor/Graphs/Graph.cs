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
	/// Graph.
	/// Basic class for Graph, Diamond window can hold a one graph at a time.
	/// The Graph class handle basic functionalities of graph, like holding nodes,
	/// updating nodes, and receiving and handling events.
	/// </summary>
	[Serializable]
	public class Graph : ScriptableObject 
	{
		public string graphName = "New Graph";

		public string graphNameRacine = "";


		[Header ("Nodes")]
		public List <Node> nodes;


		//Node nodeSelected;


		[Header ("Output Choosen To connect")]
		public int choosenToConnectId = -1;


		public bool leftHold = false;


		public Color linksColor;

		public Color linksSelectedColor = new Color (191f/255f, 110f/255f, 48f/255f);


		protected Vector2 rightClickPosition;


		public string uniqueID = "";


		public bool editingLogic;


		public GraphType graphType;


		public int framesFromLoad = 0;

		public int framesToActiveObjectFields = 360;

		public GameObject [] identifiedGameObjects;

		public static bool objectFieldsEnabled;

		public string myPath = "";



		public virtual void Init (
			string setGraphName,
			string setGraphNameRacine, 
			bool firstTime, 
			string creationPath, 
			GraphType setGraphType)
		{
			if (firstTime)
			{
				uniqueID = DatesTimesAndFrequences.DateTimeNow ();
			}

			graphType = setGraphType;
			
			graphName = setGraphName;

			graphNameRacine = setGraphNameRacine;


			nodes = new List<Node> ();

			//nodeSelected = null;


			choosenToConnectId = -1;


			leftHold = false;

			SetLinkColor ();

			linksSelectedColor = new Color (191f/255f, 110f/255f, 48f/255f);

			editingLogic = false;

			objectFieldsEnabled = true;
		}

		public virtual void Init (string setGraphName, bool firstTime)
		{
			if (firstTime)
			{
				uniqueID = DatesTimesAndFrequences.DateTimeNow ();
			}

			graphName = setGraphName;


			nodes = new List<Node> ();

			//nodeSelected = null;


			choosenToConnectId = -1;


			leftHold = false;

			SetLinkColor ();

			linksSelectedColor = new Color (191f/255f, 110f/255f, 48f/255f);

			editingLogic = false;

			objectFieldsEnabled = true;
		}

		public virtual void ClearAndInit (string setGraphName)
		{
			ClearNodes ();
			
			Init (setGraphName, false);

			//Auxiliaries.SaveAndRefreshAssets ();
		}

		void ClearNodes ()
		{
			for (int i = 0; i < nodes.Count; i++)
				UnityEngine.Object.DestroyImmediate (nodes [i], true);
		}

		public virtual void ScriptGenerationFolderPathFromNamesToSave ()
		{
		}

		public virtual void ScriptGenerationFolderPathToNamesToSave ()
		{
			
		}

		public virtual void GraphUpdate (Event e, Rect workspaceRect)
		{
			

			NodesUpdate (e, workspaceRect);

			EventProcess (e, workspaceRect);

			//GUIFocusControl ();

			//DrawCurrentLink (e);

			//Test (e);
		}

		void NodesUpdate (Event e, Rect workspaceRect)
		{
			if (nodes.Count == 0)
				return;

			for (int i = 0; i < nodes.Count; i++)
				nodes [i].NodeUpdate (e, workspaceRect);
		}

		protected virtual void UpdateMyPath ()
		{
			
		}


		void EventProcess (Event e, Rect workspaceRect)
		{
			if (workspaceRect.Contains (e.mousePosition))
			{
				/*
				if (e.button == 1)
				{
					rightClickPosition = e.mousePosition;

					if (e.type == EventType.MouseDown)
					{
						GenericMenu menu = new GenericMenu ();



						menu.ShowAsContext ();

						e.Use ();
					}
				}
				*/

				CheckLeftHold (e);
			}
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

		void RightClickFunction (object obj)
		{
			string choosen = obj.ToString ();

			Auxiliaries.CreateNode (choosen, rightClickPosition, this, true);
		}



		public void RemoveNode (int at)
		{
			if (at < 0 || at > nodes.Count -1)
				return;
			

			
			for (int i = 0; i < nodes.Count; i++)
			{
				//nodes [i].DecreaseSourcesDestinationAtNodeRemove (at);

				if (nodes [i].id > at)
				{
					nodes [i].id--;
				}
			}


			Node nodeTemp = nodes [at];

			nodes.Remove (nodeTemp);

			UnityEngine.Object.DestroyImmediate (nodeTemp, true);
		}


		void SetLinkColor ()
		{
			linksColor = LogicNode.ColorsArithmetic.RGB_255_To_Normalized (
				31f, 208f, 235f, 1f);
		}


		public bool AmIFirstSelectedFound (Node amIFirstSelected)
		{
			int indexOfSelected = -1;
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes [i].selectionState == SelectionState.selected)
				{
					indexOfSelected = i;

					break;
				}
			}

			bool r = false;

			if (indexOfSelected > -1 && indexOfSelected < nodes.Count)
			{
				if (nodes [indexOfSelected] == amIFirstSelected)
				{
					r = true;
				}
			}

			return r;
		}


		public virtual void SetEditingLogicToFalse ()
		{
		}

		public virtual void SetEditingLogic (Node nodeToSetIn)
		{
		}
	
	
		public string GetPath ()
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond.graph != this)
				return "";


			return diamond.GetGraphPath ();
		}

		public string GetPathFolder ()
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond.graph != this)
				return "";


			string	path = diamond.GetGraphPath ();

			return path.Remove (path.Length - graphName.Length - 6);
		}
	


		public virtual void UndoRedoAddVersion ()
		{
		}

		public void UndoRedoWriteVersion ()
		{
			UndoRedoWriterReader.WriteVersion (this);
		}

		public virtual void NodeSelected (Node node)
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
	}
}
