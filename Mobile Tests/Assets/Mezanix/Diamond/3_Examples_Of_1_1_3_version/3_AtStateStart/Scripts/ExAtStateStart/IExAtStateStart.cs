using UnityEngine;
using System.Collections;

namespace ScriptsCreatedByDiamond 
{
	public interface IExAtStateStart
	{
		void StateUpdate ();

		void ToIdle ();

		void ToCounter ();

	}
}
