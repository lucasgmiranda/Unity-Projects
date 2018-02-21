using UnityEngine;
using System.Collections;

namespace Mezanix.Diamond
{	
	public class ColorModifierSimple 
	{
		public static Color SemiDot (Color c, Vector4 v)
		{
			return new Color (
				c.r*v.x, 
				c.g*v.y,
				c.b*v.z, 
				c.a*v.w);
		}
	}
}