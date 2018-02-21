using UnityEngine;
using System.Collections;

namespace ScriptsCreatedByDiamond 
{
	public interface IExSwitchMovement
	{
		void StateUpdate ();

		void ToIdle ();

		void TomoveRight ();

		void TomoveLeft ();

	}
}
