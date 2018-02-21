using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_Move 
	{
		public ExMoveAndColor_Idle_Move_Horizontal exMoveAndColor_Idle_Move_Horizontal;
		public ExMoveAndColor_Idle_Move_Vertical exMoveAndColor_Idle_Move_Vertical;
		public ExMoveAndColor_Idle_Move_HorizontalToX exMoveAndColor_Idle_Move_HorizontalToX;
		public ExMoveAndColor_Idle_Move_VerticalToY exMoveAndColor_Idle_Move_VerticalToY;
		public ExMoveAndColor_Idle_Move_Direction exMoveAndColor_Idle_Move_Direction;
		public ExMoveAndColor_Idle_Move_Speed exMoveAndColor_Idle_Move_Speed;
		public ExMoveAndColor_Idle_Move_RigidBodyVelocity exMoveAndColor_Idle_Move_RigidBodyVelocity;
		
		public ExMoveAndColor_Idle exMoveAndColor_Idle;

		public ExMoveAndColor_Idle_Move (ExMoveAndColor_Idle setExMoveAndColor_Idle)
		{
			exMoveAndColor_Idle = setExMoveAndColor_Idle;

			exMoveAndColor_Idle_Move_Horizontal = new ExMoveAndColor_Idle_Move_Horizontal (this);
			exMoveAndColor_Idle_Move_Vertical = new ExMoveAndColor_Idle_Move_Vertical (this);
			exMoveAndColor_Idle_Move_HorizontalToX = new ExMoveAndColor_Idle_Move_HorizontalToX (this);
			exMoveAndColor_Idle_Move_VerticalToY = new ExMoveAndColor_Idle_Move_VerticalToY (this);
			exMoveAndColor_Idle_Move_Direction = new ExMoveAndColor_Idle_Move_Direction (this);
			exMoveAndColor_Idle_Move_Speed = new ExMoveAndColor_Idle_Move_Speed (this);
			exMoveAndColor_Idle_Move_RigidBodyVelocity = new ExMoveAndColor_Idle_Move_RigidBodyVelocity (this);
		}

		public void LogicUpdate ()
		{
			exMoveAndColor_Idle_Move_Horizontal.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_Vertical.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_HorizontalToX.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_VerticalToY.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_Direction.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_Speed.LogicNodeUpdate ();
			exMoveAndColor_Idle_Move_RigidBodyVelocity.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
