using UnityEngine;
using UnityEditor;

namespace Mezanix.Diamond
{
	[InitializeOnLoad]
	public class Startup 
	{
		public static int ticTacFrames;

		static Startup()
		{
			//Debug.Log("Diamond Up and running");

			ClassesNamesManager.Init ();

			ticTacFrames = 0;

			//Debug.Log ("ticTacFrames = 0;");

			Graph.objectFieldsEnabled = false;


			//if (Diamond.IsOpen)
			//	Debug.Log ("diamond is open");
	

			/*
			if (diamond != null)
			{
				if (diamond.GetType () == typeof (Diamond))
				{
					Graph graph = diamond.
					{
						if (graph != null)
						{
							graph.objectFieldsEnabled = false;

							graph.InitObjectFieldsByUniqueIds ();
						}
					}
				}
			}*/
		}
	}
}
