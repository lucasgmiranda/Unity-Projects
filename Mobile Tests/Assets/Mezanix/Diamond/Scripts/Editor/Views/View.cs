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
	/// View.
	/// Basic class for views. A view is a sub-window in the Diamond window.
	/// </summary>
	[Serializable]
	public class View
	{
		protected string title;

		public View (string setTitle)
		{
			title = setTitle;
		}

		public virtual void ViewUpdate (Event e, Graph graph, Rect rect)
		{
			
		}
	}
}