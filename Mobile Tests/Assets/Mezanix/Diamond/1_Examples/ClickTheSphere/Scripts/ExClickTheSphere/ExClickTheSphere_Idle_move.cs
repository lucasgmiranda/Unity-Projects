using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_move 
	{
		public ExClickTheSphere_Idle_move_Start exClickTheSphere_Idle_move_Start;
		public ExClickTheSphere_Idle_move_RandomDirection exClickTheSphere_Idle_move_RandomDirection;
		public ExClickTheSphere_Idle_move_GetX exClickTheSphere_Idle_move_GetX;
		public ExClickTheSphere_Idle_move_GetY exClickTheSphere_Idle_move_GetY;
		public ExClickTheSphere_Idle_move_Set_X exClickTheSphere_Idle_move_Set_X;
		public ExClickTheSphere_Idle_move_Set_Y exClickTheSphere_Idle_move_Set_Y;
		public ExClickTheSphere_Idle_move_Set_Z exClickTheSphere_Idle_move_Set_Z;
		public ExClickTheSphere_Idle_move_AddVector exClickTheSphere_Idle_move_AddVector;
		public ExClickTheSphere_Idle_move_AddVector_1 exClickTheSphere_Idle_move_AddVector_1;
		public ExClickTheSphere_Idle_move_Intensity exClickTheSphere_Idle_move_Intensity;
		public ExClickTheSphere_Idle_move_SetVelocity exClickTheSphere_Idle_move_SetVelocity;
		
		public ExClickTheSphere_Idle exClickTheSphere_Idle;

		public ExClickTheSphere_Idle_move (ExClickTheSphere_Idle setExClickTheSphere_Idle)
		{
			exClickTheSphere_Idle = setExClickTheSphere_Idle;

			exClickTheSphere_Idle_move_Start = new ExClickTheSphere_Idle_move_Start (this);
			exClickTheSphere_Idle_move_RandomDirection = new ExClickTheSphere_Idle_move_RandomDirection (this);
			exClickTheSphere_Idle_move_GetX = new ExClickTheSphere_Idle_move_GetX (this);
			exClickTheSphere_Idle_move_GetY = new ExClickTheSphere_Idle_move_GetY (this);
			exClickTheSphere_Idle_move_Set_X = new ExClickTheSphere_Idle_move_Set_X (this);
			exClickTheSphere_Idle_move_Set_Y = new ExClickTheSphere_Idle_move_Set_Y (this);
			exClickTheSphere_Idle_move_Set_Z = new ExClickTheSphere_Idle_move_Set_Z (this);
			exClickTheSphere_Idle_move_AddVector = new ExClickTheSphere_Idle_move_AddVector (this);
			exClickTheSphere_Idle_move_AddVector_1 = new ExClickTheSphere_Idle_move_AddVector_1 (this);
			exClickTheSphere_Idle_move_Intensity = new ExClickTheSphere_Idle_move_Intensity (this);
			exClickTheSphere_Idle_move_SetVelocity = new ExClickTheSphere_Idle_move_SetVelocity (this);
		}

		public void LogicUpdate ()
		{
			exClickTheSphere_Idle_move_Start.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_RandomDirection.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_GetX.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_GetY.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_Set_X.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_Set_Y.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_Set_Z.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_AddVector.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_AddVector_1.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_Intensity.LogicNodeUpdate ();
			exClickTheSphere_Idle_move_SetVelocity.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
