using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mezanix.Diamond
{
	public class RectOperations 
	{
		public static bool Contains (Vector2 position, Rect rect, Vector2 expand)
		{
			bool r = false;

			if (position.x > rect.x - expand.x &&
				position.x < rect.x + expand.x &&

				position.y > rect.y - expand.y &&
				position.y < rect.y + expand.y)
			{
				r = true;
			}

			return r;
		}

		public static Rect RectInRect (Rect big, Rect ratios)
		{
			Rect r = new Rect (
				big.x + ratios.x*(1f-ratios.width)*big.width,
				big.y + ratios.y*(1f-ratios.height)*big.height,
						ratios.width*big.width,
						ratios.height*big.height);

			return r;
		}
	
		public static Rect Offset (Rect baseRect, Vector2 offset)
		{
			return new Rect (baseRect.position + offset, baseRect.size);
		}

		public static Rect RatioToAbsolute (Rect big, Rect ratios)
		{
			Rect r = new Rect (
				big.x + ratios.x*big.width,
				big.y + ratios.y*big.height,
				ratios.width*big.width,
				ratios.height*big.height);

			return r;
		}

		public static Rect TwoPositionsToRect (Vector2 pos_0, Vector2 pos_1)
		{
			Rect r = new Rect ();

			float w = Mathf.Abs (pos_1.x - pos_0.x);

			float h = Mathf.Abs (pos_1.y - pos_0.y);

			r = new Rect (Mathf.Min (pos_0.x, pos_1.x), Mathf.Min (pos_0.y, pos_1.y), w, h);

			return r;
		}
	
	
		/// <summary>
		/// Fio
		/// </summary>
		/// <returns>The square big.</returns>
		/// <param name="rect">Rect.</param>
		public static Rect ToSquareBig (Rect rect)
		{
			float bigest = Mathf.Max (rect.width, rect.height);

			//if (rect == null)
			//	return new Rect (Vector2.zero, Vector2.one * 2000f); 

			return new Rect (rect.position, Vector2.one*bigest);
		}


		//public static Rect ToShadowRect (Rect rect, float hc, float a, float maxWb)
		//{
		//	float h = rect.height;
		//	float w = rect.width;
		//
		//	if (h == 0f || w == 0f)
		//		return new Rect (0f, 0f, 10f, 10f);
		//
		//	float hb = h + hc;
		//	float wb = w + ShadowRect_hb_to_wc (a, hb);
		//	
		//	wb = Mathf.Clamp (wb, w + 30f, maxWb);
		//
		//	return new Rect (rect.position, new Vector2 (wb, hb));
		//}
		//
		//public static float ShadowRect_hb_to_wc (float a, float hb)
		//{
		//	return Mathf.Sign (a) * ( hb / (
		//		Mathf.Abs (
		//			Mathf.Tan (a) ) ) );
		//}
	
	

		public static Rect AbsoluteToRatio (Rect big, Rect small)
		{
			Vector2 smallRel = small.position - big.position;

			return new Rect (
				smallRel.x / big.width,
				smallRel.y / big.height,
				small.width / big.width,
				small.height / big.height);
		}

		public static Rect ZoomPivotCenter (Rect rect, float speed)
		{
			Vector2 center = rect.center;

			Vector2 size = rect.size * speed;

			return new Rect (center - 0.5f*size, size);
		}
	}
}
