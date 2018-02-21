using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix
{
	[ExecuteInEditMode]
	public class MathMono : MonoBehaviour 
	{
		public enum WhatToDo
		{
			DrawFunction,
		}
		public WhatToDo whatToDo;

		public float a = 1f;
		public float speed = 1f;

		public float min = 0f;
		public float max = 10f;
		public int n = 100;

		// Use this for initialization
		void Start () 
		{
			switch (whatToDo)
			{
			case WhatToDo.DrawFunction:
				DrawFunctionStart ();
				break;
			}
		}
		
		// Update is called once per frame
		void Update () 
		{
			switch (whatToDo)
			{
			case WhatToDo.DrawFunction:
				DrawFunctionUpdate ();
				break;
			}			
		}


		void DrawFunctionStart ()
		{
			
		}

		void DrawFunctionUpdate ()
		{
			float [] v = Aux.Mathm.Ddd (min, max, n);
			Aux.Deb.DrawFunc (v, speed, a, Aux.Mathm.AToMinusAExp, Color.cyan);
		}
	}
}
