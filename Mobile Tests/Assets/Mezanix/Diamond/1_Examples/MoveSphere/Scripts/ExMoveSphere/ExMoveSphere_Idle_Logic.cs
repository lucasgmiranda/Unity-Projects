using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle_Logic 
	{
		public ExMoveSphere_Idle_Logic_PutX exMoveSphere_Idle_Logic_PutX;
		public ExMoveSphere_Idle_Logic_PutY exMoveSphere_Idle_Logic_PutY;
		public ExMoveSphere_Idle_Logic_AddVector exMoveSphere_Idle_Logic_AddVector;
		public ExMoveSphere_Idle_Logic_Speed exMoveSphere_Idle_Logic_Speed;
		public ExMoveSphere_Idle_Logic_ApplyVelocity exMoveSphere_Idle_Logic_ApplyVelocity;
		public ExMoveSphere_Idle_Logic_Horizontal exMoveSphere_Idle_Logic_Horizontal;
		public ExMoveSphere_Idle_Logic_Vertical exMoveSphere_Idle_Logic_Vertical;
		
		public ExMoveSphere_Idle exMoveSphere_Idle;

		public ExMoveSphere_Idle_Logic (ExMoveSphere_Idle setExMoveSphere_Idle)
		{
			exMoveSphere_Idle = setExMoveSphere_Idle;

			exMoveSphere_Idle_Logic_PutX = new ExMoveSphere_Idle_Logic_PutX (this);
			exMoveSphere_Idle_Logic_PutY = new ExMoveSphere_Idle_Logic_PutY (this);
			exMoveSphere_Idle_Logic_AddVector = new ExMoveSphere_Idle_Logic_AddVector (this);
			exMoveSphere_Idle_Logic_Speed = new ExMoveSphere_Idle_Logic_Speed (this);
			exMoveSphere_Idle_Logic_ApplyVelocity = new ExMoveSphere_Idle_Logic_ApplyVelocity (this);
			exMoveSphere_Idle_Logic_Horizontal = new ExMoveSphere_Idle_Logic_Horizontal (this);
			exMoveSphere_Idle_Logic_Vertical = new ExMoveSphere_Idle_Logic_Vertical (this);
		}

		public void LogicUpdate ()
		{
			exMoveSphere_Idle_Logic_PutX.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_PutY.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_AddVector.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_Speed.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_ApplyVelocity.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_Horizontal.LogicNodeUpdate ();
			exMoveSphere_Idle_Logic_Vertical.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
