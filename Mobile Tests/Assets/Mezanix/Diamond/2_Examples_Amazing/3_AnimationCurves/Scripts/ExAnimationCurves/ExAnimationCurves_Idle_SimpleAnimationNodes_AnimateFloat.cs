using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat 
	{
		ExAnimationCurves_Idle_SimpleAnimationNodes exAnimationCurves_Idle_SimpleAnimationNodes;

		public bool doIT = false;

		public float floatValue = 0f;

		public float [] floatValues = new float[3];

		AnimationCurve animationCurve_;
		public bool [] boolValues = new bool[2];

		public ExAnimationCurves_Idle_SimpleAnimationNodes_AnimateFloat (ExAnimationCurves_Idle_SimpleAnimationNodes setExAnimationCurves_Idle_SimpleAnimationNodes) 
		{
			exAnimationCurves_Idle_SimpleAnimationNodes = setExAnimationCurves_Idle_SimpleAnimationNodes;

			exAnimationCurves_Idle_SimpleAnimationNodes.IAmHere ();

			floatValues [0] = 0f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

			boolValues [0] = false;
			animationCurve_ = new AnimationCurve (
				new Keyframe []
				{
					new Keyframe (0f, 0f, 0f, 0f),
					new Keyframe (1f, 1f, 2f, 2f)
				});

			floatValue = 0f;
		}

		public void LogicNodeUpdate ()
		{
			ComputeFloat ();
		}

		IEnumerator RunAnimationCurve_float ()
		{
			if ( ! boolValues [0])
			{
				boolValues [0] = true;

				float t = 0f;

				float rate = 1f / floatValues [2];

				while (t < 1f)
				{
					t += Time.deltaTime * rate;

					floatValue = Mathf.Lerp (floatValues [0], floatValues [1], animationCurve_.Evaluate (t));

					yield return 0;
				}

				boolValues [0] = false;
			}
			yield return 0;
		}
		void ComputeFloat ()
		{
			if ( ! doIT)
			{
				return;
			}


			exAnimationCurves_Idle_SimpleAnimationNodes.exAnimationCurves_Idle.exAnimationCurves.StartCoroutine (RunAnimationCurve_float ());

		}
	}
}
