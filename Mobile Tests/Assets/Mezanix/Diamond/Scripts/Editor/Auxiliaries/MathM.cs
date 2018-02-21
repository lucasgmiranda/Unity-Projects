using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mezanix.Diamond
{
	public class MathM 
	{
		public static float NotZero (float v, float eps = 0.001f)
		{
			if (v > -eps && v <= 0f)
				return -eps;

			if (v >= 0f && v < eps)
				return eps;

			return v;
		}
	}
}