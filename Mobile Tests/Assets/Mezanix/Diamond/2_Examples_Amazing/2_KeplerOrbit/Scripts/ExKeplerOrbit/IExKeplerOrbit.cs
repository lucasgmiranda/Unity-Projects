using UnityEngine;
using System.Collections;

namespace ScriptsCreatedByDiamond 
{
	public interface IExKeplerOrbit
	{
		void StateUpdate ();

		void ToIdle ();

	}
}
