using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Mezanix.Diamond
{
	public class ClassesNamesManager 
	{
		const string auxiliaryDataFolderName = "AuxiliaryData_Read_Only";

		const string classesNamesFileName = "classesNames.txt";

		const string graphPathFileName = "graphPath.txt";

		//static readonly string graphsPathsFileName = "graphsNames.txt";

		static List <string> classesNames = new List<string> ();

		public static string graphPath = "";

		//static List <string> graphsPaths = new List<string> ();

		public static void Init ()
		{
			//Auxiliaries.CreateFolder (Auxiliaries.resourcesFolderPath, auxiliaryDataFolderName);
			//
			//FileToClassesNames ();
			//
			//FileToGraphPath ();

			//FileToGraphsPaths ();
		}


		static void FileToClassesNames ()
		{
			classesNames = new List<string> ();

			string [] cn = File.ReadAllLines (
				Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
				classesNamesFileName);

			if (cn.Length == 0)
				return;

			for (int i = 0; i < cn.Length; i++)
			{
				classesNames.Add (cn [i]);
				/*
				string s = "";

				if (cn [i].Length < 2)
					continue;

				for (int j = 0; j < cn [i].Length-2; j++)
				{
					s += cn [i][j];
				}

				classesNames.Add (s);*/
			}

			for (int i = 0; i < classesNames.Count; i++)
			{
				//Debug.Log (classesNames [i]);
			}
		}


		static void FileToGraphPath ()
		{
			graphPath = "";

			string [] cn = File.ReadAllLines (
				Auxiliaries.resourcesFolderPath + 
				"/" + auxiliaryDataFolderName + "/" +
				graphPathFileName);

			if (cn.Length == 0)
				return;

			graphPath = cn [0];

			//Debug.Log ("graphPath = " + graphPath);
		}


		static void ClassesNamesToFile ()
		{
			string p = Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
				classesNamesFileName;

			File.WriteAllText (p, "");

			if (classesNames.Count == 0)
				return;

			for (int i = 0; i < classesNames.Count; i++)
			{
				File.AppendAllText (p, classesNames [i] + "\n");
			}
		}


		public static void GraphPathToFile (bool toScriptable, Diamond diamond)
		{
			if (diamond == null)
			{
				Debug.LogWarning ("Diamond can't register the graph path, no instance of Diamond opened");

				return;
			}

			graphPath = diamond.GetGraphPath ();

			if (graphPath == "")
			{
				//Debug.LogWarning ("Diamond can't register the graph path, trying to register a null or empty path");

				return;
			}

			if (toScriptable)
			{
				if (Diamond.namesToSave.graphPath == graphPath)
				{
					//Debug.LogWarning ("No need that Diamond register the graph path, work is already done");

					return;
				}

				Diamond.namesToSave.graphPath = graphPath;

				Auxiliaries.SaveAndRefreshAssetsForced ();
			}
			else if ( ! toScriptable)
			{
				string p = Auxiliaries.resourcesFolderPath + 
					"/" + auxiliaryDataFolderName + "/" +
					graphPathFileName;

				File.WriteAllText (p, "");

				File.AppendAllText (p, graphPath + "\n");
			}
		}
	
		public const string namesConflictSameGraphOrStateName = 
			"This name already exist as a " +
			"Graph or a State name in your project,\n" +
			"please use an another name";

		public static bool CheckNewName (string n)
		{
			bool r = true;

			if (Diamond.namesToSave.classesNames.Contains (n))
			{
				EditorUtility.DisplayDialog ("Names Conflict", 
					"The name '" + n + "' already exist as a " +
					"Graph or a Class name in your project,\n" +
					"please use an another name", "Ok");

				return false;
			}

			if (AllCsScriptsNamesInProjectFolders ().Contains (n))
			{
				EditorUtility.DisplayDialog ("Names Conflict", 
					"This name '" + n + "' already exist as a class name in your project,\n" +
					"please use an another name", "Ok");

				return false;
			}

			if (n == "Editor" || n == "editor")
			{
				EditorUtility.DisplayDialog ("Editor name not allowed", 
					"Editor name is not allowed as a class name in your project,\n" +
					"please use an another name", "Ok");

				return false;
			}
				
			if (n == "MonoBehaviour" || n == "monoBehaviour")
			{
				EditorUtility.DisplayDialog ("MonoBehaviour name not allowed", 
					"MonoBehaviour name is not allowed as a class name in your project,\n" +
					"please use an another name", "Ok");

				return false;
			}

			return r;
		}

		public static void AddNewName (string n)
		{			
			Diamond.namesToSave.classesNames.Add (n);

			//else if ( ! toScriptable)
			//{
			//	if (classesNames.Count == 0)
			//		classesNames = new List<string> ();
			//
			//	if (classesNames.Contains (n))
			//	{
			//		EditorUtility.DisplayDialog ("Names Conflict", 
			//			"This name '" + n + "' already exist as a Graph or a State name in your project,\n" +
			//			"please use an another name", "Ok");
			//
			//		return false;
			//	}
			//
			//	if (AllCsScriptsNamesInProjectFolders ().Contains (n))
			//	{
			//		EditorUtility.DisplayDialog ("Names Conflict", 
			//			"This name '" + n + "' already exist as a class name in your project,\n" +
			//			"please use an another name", "Ok");
			//
			//		return false;
			//	}
			//
			//	if (n == "Editor" || n == "editor")
			//	{
			//		EditorUtility.DisplayDialog ("Editor name not allowed", 
			//			"Editor name is not allowed as a class name in your project,\n" +
			//			"please use an another name", "Ok");
			//
			//		return false;
			//	}
			//
			//	classesNames.Add (n);
			//
			//	File.AppendAllText (Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
			//		classesNamesFileName, classesNames [classesNames.Count-1] + "\n"); 
			//}
		}
	
		public static void RemoveName (string n, bool fromScriptable)
		{
			if (fromScriptable)
			{
				Diamond.namesToSave.classesNames.Remove (n);
			}
			else if ( ! fromScriptable)
			{
				classesNames.Remove (n);

				ClassesNamesToFile ();
			}
		}
	
		/*public static void UpdateEntireNames (List <Node> lst)
		{
			classesNames = new List<string> ();

			for (int i = 0; i < lst.Count; i++)
			{
				classesNames.Add (lst [i].nodeName);	
			}

			ClassesNamesToFile ();
		}
		
		static void FileToGraphsPaths ()
		{
			graphsPaths = new List<string> ();

			string [] cn = File.ReadAllLines (
				Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
				graphsPathsFileName);

			if (cn.Length == 0)
				return;

			for (int i = 0; i < cn.Length; i++)
			{
				graphsPaths.Add (cn [i]);
			}

			for (int i = 0; i < graphsPaths.Count; i++)
			{
				Debug.Log (graphsPaths [i]);
			}
		}

		static void GraphsPathsToFile ()
		{
			string p = Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
				graphsPathsFileName;

			File.WriteAllText (p, "");

			if (graphsPaths.Count == 0)
				return;

			for (int i = 0; i < graphsPaths.Count; i++)
			{
				File.AppendAllText (p, graphsPaths [i] + "\n");
			}
		}

		public static bool AddNewGraphPath (string n)
		{
			bool r = true;

			if (graphsPaths.Count == 0)
				graphsPaths = new List<string> ();

			graphsPaths.Add (n);

			File.AppendAllText (Auxiliaries.resourcesFolderPath + "/" + auxiliaryDataFolderName + "/" +
				graphsPathsFileName, graphsPaths [graphsPaths.Count-1] + "\n");

			return r;
		}

		public static void RemoveGraphPath (string n)
		{
			graphsPaths.Remove (n);

			GraphsPathsToFile ();
		}*/
	


		public static List <string> AllCsScriptsNamesInProjectFolders ()
		{			
			string projectRoot = @"Assets";

			string [] cs = Directory.GetFiles (projectRoot, "*.cs", SearchOption.AllDirectories); 

			List <string> csL = new List<string> ();

			for (int i = 0; i < cs.Length; i++)
			{
				csL.Add (StringTreatment.WithoutExtention (StringTreatment.AfterSlash (cs [i])));
			}


			List <string> a = new List<string> ();

			for (int i = 0; i < 4; i++)
			{
				a.Add (i.ToString ());
			}

			List <string> aP = csL;

			aP.AddRange (a);

			//for (int i = 0; i < aP.Count; i++)
			//{
			//	Debug.Log (aP [i]);
			//}

			return csL;
		}
	

	}
}
