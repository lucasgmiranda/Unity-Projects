using UnityEngine;
using System.Collections;

namespace ScriptsCreatedByDiamond 
{
	public interface IExDataTransferListener
	{
		void StateUpdate ();

		void ToIdle ();

	}
}
