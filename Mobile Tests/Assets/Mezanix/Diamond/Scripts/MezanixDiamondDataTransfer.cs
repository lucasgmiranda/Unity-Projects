using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsCreatedByDiamond 
{
	/// <summary>
	/// Mezanix diamond data transfer.
	/// MonoBehaviour class that manage global data transfer (global broadcasting)
	/// letting generated scripts communicate.
	/// Diamond automatically creates the object of this class in the scene and 
	/// attache it to an empty gameobject called "MezanixDiamondDataTransfer".
	/// </summary>
	public class MezanixDiamondDataTransfer : MonoBehaviour 
	{
		public const string gameObjectHolderName = "MezanixDiamondDataTransfer";

		public static GameObject gameObjectHolder = null;

		[Header("Bools")]
		public List <string> boolNames = new List<string> ();

		public List <bool> bools = new List<bool> ();

		[Header("Bool Lists")]
		public List <string> boolListsNames = new List<string> ();

		public List <List <bool>> boolLists = new List<List<bool>> ();

		[Header("Colors")]
		public List <string> colorNames = new List<string>();

		public List <Color> colors = new List<Color>();

		[Header("Color Lists")]
		public List <string> colorListsNames = new List<string>();

		public List<List <Color>> colorLists = new List<List<Color>>();

		[Header("Floats")]
		public List <string> floatNames = new List<string> ();

		public List <float> floats = new List<float> ();

		[Header("Float Lists")]
		public List <string> floatListsNames = new List<string>();

		public List<List <float>> floatLists = new List<List<float>>();

		[Header("GameObjects")]
		public List <string> gameObjectNames = new List<string> ();

		public List <GameObject> gameObjects = new List<GameObject> ();

		[Header("GameObject Lists")]
		public List <string> gameObjectListsNames = new List<string>();

		public List<List <GameObject>> gameObjectLists = new List<List<GameObject>>();

		[Header("Ints")]
		public List <string> intNames = new List<string> ();

		public List <int> ints = new List<int> ();

		[Header("Int Lists")]
		public List <string> intListsNames = new List<string>();

		public List<List <int>> intLists = new List<List<int>>();

		[Header("Strings")]
		public List <string> stringNames = new List<string> ();

		public List <string> strings = new List<string> ();

		[Header("String Lists")]
		public List <string> stringListsNames = new List<string>();

		public List<List <string>> stringLists = new List<List<string>>();

		[Header("Vectors 2")]
		public List <string> vector2Names = new List<string> ();

		public List <Vector2> vector2s = new List<Vector2> ();

		[Header("Vector2 Lists")]
		public List <string> vector2ListsNames = new List<string>();

		public List<List <Vector2>> vector2Lists = new List<List<Vector2>>();


		[Header("Vectors 3")]
		public List <string> vector3Names = new List<string> ();

		public List <Vector3> vector3s = new List<Vector3> ();

		[Header("Vector3 Lists")]
		public List <string> vector3ListsNames = new List<string>();

		public List<List <Vector3>> vector3Lists = new List<List<Vector3>>();


		[Header("Vectors 4")]
		public List <string> vector4Names = new List<string> ();

		public List <Vector4> vector4s = new List<Vector4> ();

		[Header("Vector4 Lists")]
		public List <string> vector4ListsNames = new List<string>();

		public List<List <Vector4>> vector4Lists = new List<List<Vector4>>();


		[Header("Rects")]
		public List <string> rectNames = new List<string> ();

		public List <Rect> rects = new List<Rect> ();

		[Header("Rect Lists")]
		public List <string> rectListsNames = new List<string>();

		public List<List <Rect>> rectLists = new List<List<Rect>>();


		[Header("Materials")]
		public List <string> materialNames = new List<string> ();

		public List <Material> materials = new List<Material> ();

		[Header("Material Lists")]
		public List <string> materialListsNames = new List<string>();

		public List<List <Material>> materialLists = new List<List<Material>>();


		[Header("Textures 2D")]
		public List <string> texture2DNames = new List<string> ();

		public List <Texture2D> texture2Ds = new List<Texture2D> ();

		[Header("Texture2D Lists")]
		public List <string> texture2DListsNames = new List<string>();

		public List<List <Texture2D>> texture2DLists = new List<List<Texture2D>>();


		[Header("Shaders")]
		public List <string> shaderNames = new List<string> ();

		public List <Shader> shaders = new List<Shader> ();

		[Header("Shader Lists")]
		public List <string> shaderListsNames = new List<string>();

		public List<List <Shader>> shaderLists = new List<List<Shader>>();

		void Start () 
		{
			Init ();
		}

		void Init ()
		{
			boolNames = new List<string> ();

			bools = new List<bool> ();


			boolListsNames = new List<string> ();

			boolLists = new List<List<bool>> ();


			colorNames = new List<string>();

			colors = new List<Color>();


			colorListsNames = new List<string>();

			colorLists = new List<List<Color>>();


			floatNames = new List<string>();

			floats = new List<float>();


			floatListsNames = new List<string>();

			floatLists = new List<List<float>>();


			gameObjectNames = new List<string>();

			gameObjects = new List<GameObject>();


			gameObjectListsNames = new List<string>();

			gameObjectLists = new List<List<GameObject>>();


			intNames = new List<string>();

			ints = new List<int>();


			intListsNames = new List<string>();

			intLists = new List<List<int>>();


			stringNames = new List<string>();

			strings = new List<string>();


			stringListsNames = new List<string>();

			stringLists = new List<List<string>>();


			vector2Names = new List<string>();

			vector2s = new List<Vector2>();


			vector2ListsNames = new List<string>();

			vector2Lists = new List<List<Vector2>>();


			vector3Names = new List<string>();

			vector3s = new List<Vector3>();


			vector3ListsNames = new List<string>();

			vector3Lists = new List<List<Vector3>>();


			vector4Names = new List<string>();
				  
			vector4s = new List<Vector4>();
				  
				  
			vector4ListsNames = new List<string>();
				  
			vector4Lists = new List<List<Vector4>>();


			rectNames = new List<string>();

			rects = new List<Rect>();


			rectListsNames = new List<string>();

			rectLists = new List<List<Rect>>();


			materialNames = new List<string>();

			materials = new List<Material>();


			materialListsNames = new List<string>();

			materialLists = new List<List<Material>>();


			texture2DNames = new List<string>();

			texture2Ds = new List<Texture2D>();


			texture2DListsNames = new List<string>();

			texture2DLists = new List<List<Texture2D>>();


			shaderNames = new List<string>();

			shaders = new List<Shader>();


			shaderListsNames = new List<string>();

			shaderLists = new List<List<Shader>>();
		}


		public void SetBool (string eName, bool eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = boolNames.IndexOf (eName);

			if (IsInRange (eNameIndex, bools))
			{
				bools [eNameIndex] = eValue;

				return;
			}

			boolNames.Add (eName);

			bools.Add (eValue);
		}
		public bool GetBool (string eName, bool defaultValue)
		{
			int eNameIndex = boolNames.IndexOf (eName);

			if (IsInRange (eNameIndex, bools))
			{
				return bools [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveBool (string eName)
		{
			int eNameIndex = boolNames.IndexOf (eName);

			if (IsInRange (eNameIndex, bools))
			{
				bools.RemoveAt (eNameIndex);

				boolNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<bool> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetBoolList (string eName, List <bool> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = boolListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, boolLists))
			{
				boolLists [eNameIndex] = eValue;

				return;
			}

			boolListsNames.Add (eName);

			boolLists.Add (eValue);

		}
		public List<bool> GetBoolList (string eName, List <bool> defaultValue)
		{
			int eNameIndex = boolListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, boolLists))
			{
				return boolLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveBoolList (string eName)
		{
			int eNameIndex = boolListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, boolLists))
			{
				boolLists.RemoveAt (eNameIndex);

				boolListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<bool>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetColor (string eName, Color eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = colorNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colors))
			{
				colors [eNameIndex] = eValue;

				return;
			}

			colorNames.Add (eName);

			colors.Add (eValue);
		}
		public Color GetColor (string eName, Color defaultValue)
		{
			int eNameIndex = colorNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colors))
			{
				return colors [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveColor (string eName)
		{
			int eNameIndex = colorNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colors))
			{
				colors.RemoveAt (eNameIndex);

				colorNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Color> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetColorList (string eName, List <Color> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = colorListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colorLists))
			{
				colorLists [eNameIndex] = eValue;

				return;
			}

			colorListsNames.Add (eName);

			colorLists.Add (eValue);

		}
		public List<Color> GetColorList (string eName, List <Color> defaultValue)
		{
			int eNameIndex = colorListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colorLists))
			{
				return colorLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveColorList (string eName)
		{
			int eNameIndex = colorListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, colorLists))
			{
				colorLists.RemoveAt (eNameIndex);

				colorListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Color>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetFloat (string eName, float eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = floatNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floats))
			{
				floats [eNameIndex] = eValue;

				return;
			}

			floatNames.Add (eName);

			floats.Add (eValue);
		}
		public float GetFloat (string eName, float defaultValue)
		{
			int eNameIndex = floatNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floats))
			{
				return floats [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveFloat (string eName)
		{
			int eNameIndex = floatNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floats))
			{
				floats.RemoveAt (eNameIndex);

				floatNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<float> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetFloatList (string eName, List <float> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = floatListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floatLists))
			{
				floatLists [eNameIndex] = eValue;

				return;
			}

			floatListsNames.Add (eName);

			floatLists.Add (eValue);

		}
		public List<float> GetFloatList (string eName, List <float> defaultValue)
		{
			int eNameIndex = floatListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floatLists))
			{
				return floatLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveFloatList (string eName)
		{
			int eNameIndex = floatListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, floatLists))
			{
				floatLists.RemoveAt (eNameIndex);

				floatListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<float>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetGameObject (string eName, GameObject eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = gameObjectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjects))
			{
				gameObjects [eNameIndex] = eValue;

				return;
			}

			gameObjectNames.Add (eName);

			gameObjects.Add (eValue);
		}
		public GameObject GetGameObject (string eName, GameObject defaultValue)
		{
			int eNameIndex = gameObjectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjects))
			{
				return gameObjects [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveGameObject (string eName)
		{
			int eNameIndex = gameObjectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjects))
			{
				gameObjects.RemoveAt (eNameIndex);

				gameObjectNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<GameObject> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetGameObjectList (string eName, List <GameObject> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = gameObjectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjectLists))
			{
				gameObjectLists [eNameIndex] = eValue;

				return;
			}

			gameObjectListsNames.Add (eName);

			gameObjectLists.Add (eValue);

		}
		public List<GameObject> GetGameObjectList (string eName, List <GameObject> defaultValue)
		{
			int eNameIndex = gameObjectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjectLists))
			{
				return gameObjectLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveGameObjectList (string eName)
		{
			int eNameIndex = gameObjectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, gameObjectLists))
			{
				gameObjectLists.RemoveAt (eNameIndex);

				gameObjectListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<GameObject>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetInt (string eName, int eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = intNames.IndexOf (eName);

			if (IsInRange (eNameIndex, ints))
			{
				ints [eNameIndex] = eValue;

				return;
			}

			intNames.Add (eName);

			ints.Add (eValue);
		}
		public int GetInt (string eName, int defaultValue)
		{
			int eNameIndex = intNames.IndexOf (eName);

			if (IsInRange (eNameIndex, ints))
			{
				return ints [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveInt (string eName)
		{
			int eNameIndex = intNames.IndexOf (eName);

			if (IsInRange (eNameIndex, ints))
			{
				ints.RemoveAt (eNameIndex);

				intNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<int> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetIntList (string eName, List <int> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = intListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, intLists))
			{
				intLists [eNameIndex] = eValue;

				return;
			}

			intListsNames.Add (eName);

			intLists.Add (eValue);

		}
		public List<int> GetIntList (string eName, List <int> defaultValue)
		{
			int eNameIndex = intListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, intLists))
			{
				return intLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveIntList (string eName)
		{
			int eNameIndex = intListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, intLists))
			{
				intLists.RemoveAt (eNameIndex);

				intListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<int>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetString (string eName, string eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = stringNames.IndexOf (eName);

			if (IsInRange (eNameIndex, strings))
			{
				strings [eNameIndex] = eValue;

				return;
			}

			stringNames.Add (eName);

			strings.Add (eValue);
		}
		public string GetString (string eName, string defaultValue)
		{
			int eNameIndex = stringNames.IndexOf (eName);

			if (IsInRange (eNameIndex, strings))
			{
				return strings [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveString (string eName)
		{
			int eNameIndex = stringNames.IndexOf (eName);

			if (IsInRange (eNameIndex, strings))
			{
				strings.RemoveAt (eNameIndex);

				stringNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<string> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetStringList (string eName, List <string> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = stringListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, stringLists))
			{
				stringLists [eNameIndex] = eValue;

				return;
			}

			stringListsNames.Add (eName);

			stringLists.Add (eValue);

		}
		public List<string> GetStringList (string eName, List <string> defaultValue)
		{
			int eNameIndex = stringListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, stringLists))
			{
				return stringLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveStringList (string eName)
		{
			int eNameIndex = stringListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, stringLists))
			{
				stringLists.RemoveAt (eNameIndex);

				stringListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<string>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector2 (string eName, Vector2 eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector2Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2s))
			{
				vector2s [eNameIndex] = eValue;

				return;
			}

			vector2Names.Add (eName);

			vector2s.Add (eValue);
		}
		public Vector2 GetVector2 (string eName, Vector2 defaultValue)
		{
			int eNameIndex = vector2Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2s))
			{
				return vector2s [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector2 (string eName)
		{
			int eNameIndex = vector2Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2s))
			{
				vector2s.RemoveAt (eNameIndex);

				vector2Names.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Vector2> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector2List (string eName, List <Vector2> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector2ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2Lists))
			{
				vector2Lists [eNameIndex] = eValue;

				return;
			}

			vector2ListsNames.Add (eName);

			vector2Lists.Add (eValue);

		}
		public List<Vector2> GetVector2List (string eName, List <Vector2> defaultValue)
		{
			int eNameIndex = vector2ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2Lists))
			{
				return vector2Lists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector2List (string eName)
		{
			int eNameIndex = vector2ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector2Lists))
			{
				vector2Lists.RemoveAt (eNameIndex);

				vector2ListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Vector2>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector3 (string eName, Vector3 eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector3Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3s))
			{
				vector3s [eNameIndex] = eValue;

				return;
			}

			vector3Names.Add (eName);

			vector3s.Add (eValue);
		}
		public Vector3 GetVector3 (string eName, Vector3 defaultValue)
		{
			int eNameIndex = vector3Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3s))
			{
				return vector3s [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector3 (string eName)
		{
			int eNameIndex = vector3Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3s))
			{
				vector3s.RemoveAt (eNameIndex);

				vector3Names.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Vector3> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector3List (string eName, List <Vector3> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector3ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3Lists))
			{
				vector3Lists [eNameIndex] = eValue;

				return;
			}

			vector3ListsNames.Add (eName);

			vector3Lists.Add (eValue);

		}
		public List<Vector3> GetVector3List (string eName, List <Vector3> defaultValue)
		{
			int eNameIndex = vector3ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3Lists))
			{
				return vector3Lists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector3List (string eName)
		{
			int eNameIndex = vector3ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector3Lists))
			{
				vector3Lists.RemoveAt (eNameIndex);

				vector3ListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Vector3>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector4 (string eName, Vector4 eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector4Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4s))
			{
				vector4s [eNameIndex] = eValue;

				return;
			}

			vector4Names.Add (eName);

			vector4s.Add (eValue);
		}
		public Vector4 GetVector4 (string eName, Vector4 defaultValue)
		{
			int eNameIndex = vector4Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4s))
			{
				return vector4s [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector4 (string eName)
		{
			int eNameIndex = vector4Names.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4s))
			{
				vector4s.RemoveAt (eNameIndex);

				vector4Names.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Vector4> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetVector4List (string eName, List <Vector4> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = vector4ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4Lists))
			{
				vector4Lists [eNameIndex] = eValue;

				return;
			}

			vector4ListsNames.Add (eName);

			vector4Lists.Add (eValue);

		}
		public List<Vector4> GetVector4List (string eName, List <Vector4> defaultValue)
		{
			int eNameIndex = vector4ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4Lists))
			{
				return vector4Lists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveVector4List (string eName)
		{
			int eNameIndex = vector4ListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, vector4Lists))
			{
				vector4Lists.RemoveAt (eNameIndex);

				vector4ListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Vector4>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetRect (string eName, Rect eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = rectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rects))
			{
				rects [eNameIndex] = eValue;

				return;
			}

			rectNames.Add (eName);

			rects.Add (eValue);
		}
		public Rect GetRect (string eName, Rect defaultValue)
		{
			int eNameIndex = rectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rects))
			{
				return rects [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveRect (string eName)
		{
			int eNameIndex = rectNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rects))
			{
				rects.RemoveAt (eNameIndex);

				rectNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Rect> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetRectList (string eName, List <Rect> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = rectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rectLists))
			{
				rectLists [eNameIndex] = eValue;

				return;
			}

			rectListsNames.Add (eName);

			rectLists.Add (eValue);

		}
		public List<Rect> GetRectList (string eName, List <Rect> defaultValue)
		{
			int eNameIndex = rectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rectLists))
			{
				return rectLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveRectList (string eName)
		{
			int eNameIndex = rectListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, rectLists))
			{
				rectLists.RemoveAt (eNameIndex);

				rectListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Rect>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}


		public void SetMaterial (string eName, Material eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = materialNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materials))
			{
				materials [eNameIndex] = eValue;

				return;
			}

			materialNames.Add (eName);

			materials.Add (eValue);
		}
		public Material GetMaterial (string eName, Material defaultValue)
		{
			int eNameIndex = materialNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materials))
			{
				return materials [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveMaterial (string eName)
		{
			int eNameIndex = materialNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materials))
			{
				materials.RemoveAt (eNameIndex);

				materialNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Material> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetMaterialList (string eName, List <Material> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = materialListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materialLists))
			{
				materialLists [eNameIndex] = eValue;

				return;
			}

			materialListsNames.Add (eName);

			materialLists.Add (eValue);

		}
		public List<Material> GetMaterialList (string eName, List <Material> defaultValue)
		{
			int eNameIndex = materialListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materialLists))
			{
				return materialLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveMaterialList (string eName)
		{
			int eNameIndex = materialListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, materialLists))
			{
				materialLists.RemoveAt (eNameIndex);

				materialListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Material>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}


		public void SetTexture2D (string eName, Texture2D eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = texture2DNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2Ds))
			{
				texture2Ds [eNameIndex] = eValue;

				return;
			}

			texture2DNames.Add (eName);

			texture2Ds.Add (eValue);
		}
		public Texture2D GetTexture2D (string eName, Texture2D defaultValue)
		{
			int eNameIndex = texture2DNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2Ds))
			{
				return texture2Ds [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveTexture2D (string eName)
		{
			int eNameIndex = texture2DNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2Ds))
			{
				texture2Ds.RemoveAt (eNameIndex);

				texture2DNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Texture2D> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetTexture2DList (string eName, List <Texture2D> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = texture2DListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2DLists))
			{
				texture2DLists [eNameIndex] = eValue;

				return;
			}

			texture2DListsNames.Add (eName);

			texture2DLists.Add (eValue);

		}
		public List<Texture2D> GetTexture2DList (string eName, List <Texture2D> defaultValue)
		{
			int eNameIndex = texture2DListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2DLists))
			{
				return texture2DLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveTexture2DList (string eName)
		{
			int eNameIndex = texture2DListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, texture2DLists))
			{
				texture2DLists.RemoveAt (eNameIndex);

				texture2DListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Texture2D>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}


		public void SetShader (string eName, Shader eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = shaderNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaders))
			{
				shaders [eNameIndex] = eValue;

				return;
			}

			shaderNames.Add (eName);

			shaders.Add (eValue);
		}
		public Shader GetShader (string eName, Shader defaultValue)
		{
			int eNameIndex = shaderNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaders))
			{
				return shaders [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveShader (string eName)
		{
			int eNameIndex = shaderNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaders))
			{
				shaders.RemoveAt (eNameIndex);

				shaderNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<Shader> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}

		public void SetShaderList (string eName, List <Shader> eValue)
		{
			if (string.IsNullOrEmpty (eName))
				return;

			int eNameIndex = shaderListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaderLists))
			{
				shaderLists [eNameIndex] = eValue;

				return;
			}

			shaderListsNames.Add (eName);

			shaderLists.Add (eValue);

		}
		public List<Shader> GetShaderList (string eName, List <Shader> defaultValue)
		{
			int eNameIndex = shaderListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaderLists))
			{
				return shaderLists [eNameIndex];
			}

			return defaultValue;
		}
		public void RemoveShaderList (string eName)
		{
			int eNameIndex = shaderListsNames.IndexOf (eName);

			if (IsInRange (eNameIndex, shaderLists))
			{
				shaderLists.RemoveAt (eNameIndex);

				shaderListsNames.Remove (eName);
			}
		}
		bool IsInRange (int eNameIndex, List<List<Shader>> theList)
		{
			return eNameIndex > -1 && eNameIndex < theList.Count;
		}


		public static void CreateGameObjectHolder ()
		{
			if (gameObjectHolder != null)
				return;

			gameObjectHolder = GameObject.Find (gameObjectHolderName);

			if (gameObjectHolder != null)
				return;

			gameObjectHolder = new GameObject (gameObjectHolderName);

			gameObjectHolder.AddComponent <MezanixDiamondDataTransfer> ();
		}

	}
}
