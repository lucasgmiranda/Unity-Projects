using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mezanix.Diamond
{
	public class Drawer 
	{
		public static void DrawRectBorders (Rect r, Color color, float w)
		{
			Vector3 a = new Vector3(r.x, r.y);

			Vector3 b = new Vector3(r.x + r.width, r.y);

			Vector3 c = new Vector3(r.x + r.width, r.y + r.height);

			Vector3 d = new Vector3(r.x, r.y + r.height);


			DrawStraightBezier (a, b, color, w);

			DrawStraightBezier (b, c, color, w);

			DrawStraightBezier (c, d, color, w);

			DrawStraightBezier (d, a, color, w);
		}
		public static void DrawStraightBezier (Vector3 a, Vector3 b, Color c, float w)
		{
			Handles.DrawBezier (a, b, b, a, c, null, w);
		}

	}
}
