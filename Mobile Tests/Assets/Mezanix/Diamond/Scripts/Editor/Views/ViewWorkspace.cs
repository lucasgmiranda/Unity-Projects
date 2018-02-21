using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// View workspace.
	/// Here the Graph is running, at every Diamond window OnGUI frame, the graph runs in the method "ViewUpdate".
	/// </summary>
	[Serializable]
	public class ViewWorkspace : View 
	{
		public ViewWorkspace (string setTitle) : base (setTitle)
		{
			
		}

		public override void ViewUpdate (Event e, Graph graph, Rect rect)
		{
			base.ViewUpdate (e, graph, rect);

			if (graph != null)
			{
				//title = graph.graphNameRacine + "\nState Machine for " + graph.graphType.ToString ();
				title = graph.graphNameRacine;
			}
			else
			{
				title = Enums.virginWorkSpaceTitle;
			}


			GUI.Box (rect, title, Skins.guiSkin.GetStyle (Skins.view));

			Skins.DrawSeparator (new Rect (rect.x, rect.y, rect.width, Skins.separatorThickness));

			GUILayout.BeginArea (rect);
			{
				if (graph != null)
					graph.GraphUpdate (e, rect);
			}
			GUILayout.EndArea ();

			EventProcess (e);
		}


		void EventProcess (Event e)
		{

		}
	}
}
