using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ScriptsCreatedByDiamond 
{
	/// <summary>
	/// Mezanix diamond events.
	/// MonoBehaviour class that manage global events (global broadcasting)
	/// letting generated scripts communicate.
	/// Diamond automatically creates the object of this class in the scene and 
	/// attache it to an empty gameobject called "MezanixDiamondEvents".
	/// </summary>
	public class MezanixDiamondEvents : MonoBehaviour
	{
		public const string gameObjectHolderName = "MezanixDiamondEvents";

		public static GameObject gameObjectHolder = null;


		public List <string> eventNames = new List<string> ();

		void Start () 
		{
			eventNames = new List<string> ();
		}

		public void SetEvent (string eName)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			if (eventNames.Contains (eName))
			{
				return;
			}

			eventNames.Add (eName);
		}

		public bool GetEvent (string eName)
		{
			if (eventNames.Contains (eName))
			{
				return true;
			}

			return false;
		}

		public void RemoveEvent (string eName)
		{
			eventNames.Remove (eName);
		}

		public static void CreateGameObjectHolder ()
		{
			if (gameObjectHolder != null)
				return;

			gameObjectHolder = GameObject.Find (gameObjectHolderName);

			if (gameObjectHolder != null)
				return;
			
			gameObjectHolder = new GameObject (gameObjectHolderName);

			gameObjectHolder.AddComponent <MezanixDiamondEvents> ();
		}
	}
}
