using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace ScriptsCreatedByDiamond 
{
	/// <summary>
	/// Identified objects.
	/// A MonoBehaviour class holding the objects (game objects or prefabs) used by Diamond's graph fields
	/// The object of this class is created automatically by Diamond in your scene and affected to a game object 
	/// called "MezanixDiamondIdentifiedObjects", so all you object are held in it, this object goes in the build 
	/// of your game as a needed objects holder, each time one of your generated scripts need an object it will find it.
	/// </summary>
	public class IdentifiedObjects : MonoBehaviour
	{
		public List <UnityEngine.Object> identifiedObjects;

		public List <string> identifiedObjectIDs;


		public void Init ()
		{
			identifiedObjects = new List<Object> ();

			identifiedObjectIDs = new List<string> ();
		}

		public UnityEngine.Object GetIdentifiedObject (string ueoID)
		{
			if (string.IsNullOrEmpty (ueoID))
				return null;

			for (int i = 0; i < identifiedObjectIDs.Count; i++)
			{
				if (string.Equals (identifiedObjectIDs [i], ueoID))
				{
					return identifiedObjects [i];
				}
			}

			return null;
		}
	}

	public class IdentifiedObjectsActions
	{
		public const string gameObjectHolderName = "MezanixDiamondIdentifiedObjects";

		public static GameObject gameObjectHolder = null;


		public static void CreateGameObjectHolder_CreateIdentifiedObjects()
		{
			CreateGameObjectHolder ();

			CreateIdentifiedObjects ();
		}

		static void CreateGameObjectHolder ()
		{
			if (gameObjectHolder != null)
				return;
			
			gameObjectHolder = GameObject.Find (gameObjectHolderName);

			if (gameObjectHolder != null)
				return;
			

			gameObjectHolder = new GameObject (gameObjectHolderName);
		}


		static IdentifiedObjects identifiedObjects = null;

		static void CreateIdentifiedObjects ()
		{
			if (identifiedObjects != null)
				return;

			CreateGameObjectHolder ();

			identifiedObjects = gameObjectHolder.GetComponent <IdentifiedObjects> ();
			if (identifiedObjects == null)
			{
				identifiedObjects = gameObjectHolder.AddComponent <IdentifiedObjects> ();

				identifiedObjects.Init ();
			}
		}
	

		static int IndexOfID (string id)
		{
			CreateIdentifiedObjects ();

			return identifiedObjects.identifiedObjectIDs.IndexOf (id);
		}
		static int IndexOfIDNoCreation (string id)
		{
			if (identifiedObjects == null)
				return -1;

			if (identifiedObjects.identifiedObjectIDs == null)
				return -1;

			if (identifiedObjects.identifiedObjectIDs.Count == 0)
				return -1;


			return identifiedObjects.identifiedObjectIDs.IndexOf (id);
		}

		static bool InRange (int index)
		{
			CreateIdentifiedObjects ();

			if (index > -1 && index < identifiedObjects.identifiedObjectIDs.Count)
			{
				return true;
			}

			return false;
		}
		static bool InRangeNoCreation (int index)
		{
			if (identifiedObjects == null)
				return false;

			if (identifiedObjects.identifiedObjectIDs == null)
				return false;

			if (identifiedObjects.identifiedObjectIDs.Count == 0)
				return false;
			

			if (index > -1 && index < identifiedObjects.identifiedObjectIDs.Count)
			{
				return true;
			}

			return false;
		}


		public static void SetIdentifiedObject (string id, UnityEngine.Object obj)
		{
			int indexOfId = IndexOfID (id);

			if (InRange (indexOfId))
			{
				identifiedObjects.identifiedObjects [indexOfId] = obj;
			}
			else
			{
				AddNewPair (id, obj);
			}
		}

		public static UnityEngine.Object GetIdentifiedObject (string id)
		{
			UnityEngine.Object obj = null;

			int indexOfId = IndexOfIDNoCreation (id);

			if (InRangeNoCreation (indexOfId))
			{
				obj = identifiedObjects.identifiedObjects [indexOfId];
			}

			return obj;
		}

		static void AddNewPair (string id, UnityEngine.Object obj)
		{
			CreateIdentifiedObjects ();

			identifiedObjects.identifiedObjectIDs.Add (id);

			identifiedObjects.identifiedObjects.Add (obj);
		}

		static void RemovePair (string id)
		{
			int indexOfId = IndexOfID (id);

			if (InRange (indexOfId))
			{
				identifiedObjects.identifiedObjectIDs.Remove (id);

				identifiedObjects.identifiedObjects.RemoveAt (indexOfId);
			}
		}

		public static void RemoveNodeFieldPaires (string nodeUniqueID)
		{
			if (string.IsNullOrEmpty (nodeUniqueID))
				return;

			if (gameObjectHolder == null)
				return;

			if (identifiedObjects == null)
				return;

			if (identifiedObjects.identifiedObjectIDs == null)
				return;

			if (identifiedObjects.identifiedObjectIDs.Count == 0)
				return;

			for (int i = 0; i < identifiedObjects.identifiedObjectIDs.Count; i++)
			{
				string nodeUID = BeforeThat (
					identifiedObjects.identifiedObjectIDs [i], '.');

				if (nodeUID == nodeUniqueID)
				{
					RemovePair (identifiedObjects.identifiedObjectIDs [i]);

					i--;
				}
			}
		}

		static string BeforeThat (string s, char that)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			for (int i = 0; i < c.Length; i++)
			{
				if (c [i] == that)
					break;

				cR.Add (c [i]);
			}

			return new string (cR.ToArray ());
		}
	}
}
