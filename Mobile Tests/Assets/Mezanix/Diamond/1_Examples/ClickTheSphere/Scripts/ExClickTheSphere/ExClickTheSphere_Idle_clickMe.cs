using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe 
	{
		public ExClickTheSphere_Idle_clickMe_LeftClick exClickTheSphere_Idle_clickMe_LeftClick;
		public ExClickTheSphere_Idle_clickMe_MousePosition exClickTheSphere_Idle_clickMe_MousePosition;
		public ExClickTheSphere_Idle_clickMe_ScreenPointToRay exClickTheSphere_Idle_clickMe_ScreenPointToRay;
		public ExClickTheSphere_Idle_clickMe_Ray_ exClickTheSphere_Idle_clickMe_Ray_;
		public ExClickTheSphere_Idle_clickMe_Tag exClickTheSphere_Idle_clickMe_Tag;
		public ExClickTheSphere_Idle_clickMe_And exClickTheSphere_Idle_clickMe_And;
		public ExClickTheSphere_Idle_clickMe_increment exClickTheSphere_Idle_clickMe_increment;
		public ExClickTheSphere_Idle_clickMe_toString exClickTheSphere_Idle_clickMe_toString;
		public ExClickTheSphere_Idle_clickMe_Ui exClickTheSphere_Idle_clickMe_Ui;
		
		public ExClickTheSphere_Idle exClickTheSphere_Idle;

		public ExClickTheSphere_Idle_clickMe (ExClickTheSphere_Idle setExClickTheSphere_Idle)
		{
			exClickTheSphere_Idle = setExClickTheSphere_Idle;

			exClickTheSphere_Idle_clickMe_LeftClick = new ExClickTheSphere_Idle_clickMe_LeftClick (this);
			exClickTheSphere_Idle_clickMe_MousePosition = new ExClickTheSphere_Idle_clickMe_MousePosition (this);
			exClickTheSphere_Idle_clickMe_ScreenPointToRay = new ExClickTheSphere_Idle_clickMe_ScreenPointToRay (this);
			exClickTheSphere_Idle_clickMe_Ray_ = new ExClickTheSphere_Idle_clickMe_Ray_ (this);
			exClickTheSphere_Idle_clickMe_Tag = new ExClickTheSphere_Idle_clickMe_Tag (this);
			exClickTheSphere_Idle_clickMe_And = new ExClickTheSphere_Idle_clickMe_And (this);
			exClickTheSphere_Idle_clickMe_increment = new ExClickTheSphere_Idle_clickMe_increment (this);
			exClickTheSphere_Idle_clickMe_toString = new ExClickTheSphere_Idle_clickMe_toString (this);
			exClickTheSphere_Idle_clickMe_Ui = new ExClickTheSphere_Idle_clickMe_Ui (this);
		}

		public void LogicUpdate ()
		{
			exClickTheSphere_Idle_clickMe_LeftClick.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_MousePosition.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_ScreenPointToRay.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_Ray_.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_Tag.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_And.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_increment.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_toString.LogicNodeUpdate ();
			exClickTheSphere_Idle_clickMe_Ui.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
