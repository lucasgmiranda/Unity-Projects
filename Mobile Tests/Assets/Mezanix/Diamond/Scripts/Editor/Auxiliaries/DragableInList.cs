using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mezanix.Diamond
{
	public class DragableInList  
	{
		public Rect rect;

		public string label = "";

		public bool selected = false;

		public UnityEngine.Object obj;

		public DragableInList (Rect setRect, UnityEngine.Object setObj)
		{
			rect = setRect;

			obj = setObj;
		}

		public void Draw (GUIStyle rectStyle, GUIStyle labelStyle)
		{
			GUI.Box (rect, "", rectStyle);

			EditorGUI.LabelField (rect, label, labelStyle);
		}
	}
}
