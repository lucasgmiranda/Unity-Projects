using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix
{
	[ExecuteInEditMode]
	public class FuncDrawer : MonoBehaviour 
	{
		public Vector2 p0;
		public Vector2 p1;

		public Vector2 p2;
		public Vector2 p3;

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			AuxMono.Deb.DrawFunc (AuxMono.Mathm.Ddd (p0.x-1f, p3.x+1f, 200), p0, p1, p2, p3, 
				AuxMono.Mathm.Segment3,
				Color.cyan);
		}
	}
}