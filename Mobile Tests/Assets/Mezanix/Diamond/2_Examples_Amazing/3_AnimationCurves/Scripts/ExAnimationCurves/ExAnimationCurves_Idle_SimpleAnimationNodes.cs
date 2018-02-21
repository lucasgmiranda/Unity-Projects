using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_SimpleAnimationNodes 
	{
		public ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat exAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat;
		public ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector2 exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector2;
		public ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector3 exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector3;
		public ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector4 exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector4;
		
		public ExAnimationCurves_Idle exAnimationCurves_Idle;

		public ExAnimationCurves_Idle_SimpleAnimationNodes (ExAnimationCurves_Idle setExAnimationCurves_Idle)
		{
			exAnimationCurves_Idle = setExAnimationCurves_Idle;

			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat = new ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat (this);
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector2 = new ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector2 (this);
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector3 = new ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector3 (this);
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector4 = new ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector4 (this);
		}

		public void LogicUpdate ()
		{
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat.LogicNodeUpdate ();
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector2.LogicNodeUpdate ();
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector3.LogicNodeUpdate ();
			exAnimationCurves_Idle_SimpleAnimationNodes_AnimateVector4.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}	}
}
