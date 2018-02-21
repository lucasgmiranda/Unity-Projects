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
	/// Node.
	/// Basic class for node, responsible of
	/// basic functionalities of a node like 
	/// handling events, drawing itself, moving, handling links with other nodes
	/// </summary>
	[Serializable]
	public class Node : ScriptableObject
	{
		public int id;

		public Graph graph;

		public string nodeName;

		protected string oldNodeName = "";

		public Rect rect;

		//[SerializeField]
		public SelectionState selectionState;


		public List <int> sources;

		public List <int> destinations;


		public List <Logic> logics;

		public bool callEditLogic;


		public Rect editLogicRect;


		public bool editingLogic;

		public int editingLogicId;


		[HideInInspector]
		public bool isIdle = false;

		public string uniqueID = "";

		public string stateToGoToNameValue = "";

		public virtual void Init (
			Vector2 setInitialPosition,
			Graph setGraph, 
			bool withRename, 
			bool setIsIdle, 
			bool writeStatesNamesEnumScript)
		{
			uniqueID = DatesTimesAndFrequences.DateTimeNow ();
			
			graph = setGraph;

			selectionState = SelectionState.selected;

			rect = new Rect (
				setInitialPosition.x, 
				setInitialPosition.y, 
				Skins.nodeRect.width, 
				Skins.nodeRect.height);

			isIdle = setIsIdle;
		}

		public virtual void Init (
			Vector2 setInitialPosition,
			Graph setGraph, 
			bool withRename, 
			bool setIsIdle, 
			bool writeStatesNamesEnumScript, bool forBackup)
		{
			uniqueID = DatesTimesAndFrequences.DateTimeNow ();

			graph = setGraph;

			selectionState = SelectionState.selected;

			rect = new Rect (
				setInitialPosition.x, 
				setInitialPosition.y, 
				Skins.nodeRect.width, 
				Skins.nodeRect.height);

			isIdle = setIsIdle;
		}

		public virtual void NodeUpdate (Event e, Rect workspaceRect)
		{
			string guiStyle = Skins.node;

			if (selectionState == SelectionState.selected)
			{
				guiStyle = Skins.nodeSelected;

				//if (graph != null)
				//	graph.nodeSelected = this;
			}

			Draw (Skins.guiSkin.GetStyle (guiStyle));

			EventProcess (e, workspaceRect);
		}


		void Draw (GUIStyle style)
		{
			GUI.Box (RectOperations.Offset (rect, new Vector2 (-4f, 8f)), "", GetGuiStyle (Skins.nodeBG));

			GUI.Box (rect, "", style);

			EditorGUI.LabelField (
				RectOperations.RatioToAbsolute (rect, new Rect(0f, 0.1f, 1f, 0.15f)), nodeName, GetGuiStyle (Skins.nodeStateName));

			Skins.DrawSeparator (new Rect (rect.x, rect.y + rect.height, rect.width, Skins.separatorThickness));
		}

		GUIStyle GetGuiStyle (string styleName)
		{
			return Skins.guiSkin.GetStyle (styleName);
		}

		public virtual void EventProcess (Event e, Rect workspaceRect)
		{
			if (e == null)
				return;

			SelectionStateUpdate (e, workspaceRect);


			if (e.button == 0 && selectionState == SelectionState.selected)
			{
				if (workspaceRect.Contains (e.mousePosition))
				{
					if (e.type == EventType.MouseDrag)
					{
						Move (e.delta);
					}
				}
				float arrowMoveDelta = 5f;
				if (e.alt)
				{
					if (e.keyCode == KeyCode.RightArrow)
					{
						Move (new Vector2 (arrowMoveDelta, 0f));
					}
					else if (e.keyCode == KeyCode.LeftArrow)
					{
						Move (new Vector2 (-arrowMoveDelta, 0f));
					}
					if (e.keyCode == KeyCode.UpArrow)
					{
						Move (new Vector2 (0f, -arrowMoveDelta));
					}
					else if (e.keyCode == KeyCode.DownArrow)
					{
						Move (new Vector2 (0f, arrowMoveDelta));
					}
				}
			}				
			else if (e.button == 2)
			{				
				if (e.type == EventType.MouseDrag)
				{
					if ( ! graph.editingLogic)		
						Move (e.delta);
				}
			}
		}


		public virtual void DecreaseGatesIDsOnGateRemove (int removedGatID)
		{
			if (removedGatID < 0)
				return;
		}


		void SelectionStateUpdate (Event e, Rect workspaceRect)
		{
			if (workspaceRect.Contains (e.mousePosition))
			{
				if (e.type == EventType.MouseDown)
				{
					if (e.button == 0)
					{
						if (rect.Contains (e.mousePosition))
						{
							bool hidenByLogicEditor = false;

							for (int i = 0; i < graph.nodes.Count; i++)
							{
								if (graph.nodes [i] == this)
									continue;
								
								if ( ! graph.nodes [i].callEditLogic)
									continue;

								if (graph.nodes [i].callEditLogic)
								{
									if (graph.nodes [i].editingLogic)
									{
										if (graph.nodes [i].editLogicRect.Contains (e.mousePosition))
										{
											hidenByLogicEditor = true;

											break;
										}
									}
								}
							}

							if( ! hidenByLogicEditor)
							{
								ChangeSelectionState (SelectionState.selected);
							}
						}
						else
						{
							ChangeSelectionState (SelectionState.notSelected);

							//if (graph != null)
							//	graph.nodeSelected = null;
						}
					}
					else if (e.button == 1)
					{
						if ( ! rect.Contains (e.mousePosition))
						{
							ChangeSelectionState (SelectionState.notSelected);

							//if (graph != null)
							//	graph.nodeSelected = null;
						}
					}
				}
			}
		}

		public void ChangeSelectionState (SelectionState newState)
		{
			switch (selectionState)
			{
			case SelectionState.notSelected:
				switch (newState)
				{
				case SelectionState.notSelected:
					break;

				case SelectionState.selected:
					selectionState = newState;
					if (graph != null)
						graph.NodeSelected (this);
					break;
				}
				break;

			case SelectionState.selected:
				switch (newState)
				{
				case SelectionState.notSelected:
					selectionState = newState;
					break;

				case SelectionState.selected:
					break;
				}
				break;
			}
		}
		public void ChangeSelectionState (SelectionState newState, bool insideRect)
		{
			switch (selectionState)
			{
			case SelectionState.notSelected:
				switch (newState)
				{
				case SelectionState.notSelected:
					break;

				case SelectionState.selected:
					selectionState = newState;
					if ( ! insideRect)
					if (graph != null)
						graph.NodeSelected (this);
					break;
				}
				break;

			case SelectionState.selected:
				switch (newState)
				{
				case SelectionState.notSelected:
					selectionState = newState;
					break;

				case SelectionState.selected:
					break;
				}
				break;
			}
		}


		public void Move (Vector2 d)
		{
			if (editingLogic)
			if (logics [editingLogicId].rectMaximized)
				return;

			float dX = d.x;

			float dY = d.y;

			rect.x += dX;
				
			rect.y += dY;

			for (int i = 0; i < logics.Count; i++) 
			{
				if (logics [i] == null)
					continue;
				
				for (int j = 0; j < logics [i].nodes.Count; j++) 
				{
					if (logics [i].rectMaximized)
						continue;
				}
			}
		}



		public virtual void AddSource (int sourceToAd)
		{
		}

		public virtual void AddDestination (int destinationToAdd)
		{
		}
	
	
		public virtual void RemoveSource (int sourceToRemove)
		{
		}

		public virtual void RemoveDestination (int destinationToRemove)
		{
		}


		public virtual void DecreaseSourcesDestinationAtNodeRemove (int at)
		{
		}



		public virtual void DrawTransitions ()
		{
		}
	}
}
