using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Auxiliaries.
	/// class providing utilities for Diamond especially for creating/loading graphs	
	/// and saving them.
	/// </summary>
	public class Auxiliaries 
	{
		public static readonly string projectFolderPath = @"Assets";

		public static readonly string resourcesFolderPath = @"Assets/Mezanix/Diamond/Resources";

		public static readonly string graphsFolderName = "ForGraphs_Read_Only";

		//public static readonly string stateMachinesFolderName = "StateMachines";

		public static readonly string projectVariablesFolderName = "ProjectVariables";

		public static readonly string namesToSaveFolderName = "namesToSave_Read_Only";


		public static void CreateAllFolders ()
		{
			//string graphsFolderPath = CreateFolder (resourcesFolderPath, graphsFolderName);

			CreateFolder (resourcesFolderPath, graphsFolderName);
			
			//CreateFolder (graphsFolderPath, stateMachinesFolderName);
		}

		void testtest ()
		{
			if (true)
			{
			}
		}

		public static string CreateFolder (string parentPath, string name)
		{
			if (AssetDatabase.IsValidFolder (parentPath + "/" + name))
				return parentPath + "/" + name;

			return AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (parentPath, name));
		}

		public static string CreateFolder(string[] path)
		{
			string r = "";
			for (int i = 0; i < path.Length; i++)
			{
				if (string.IsNullOrEmpty(path[i]))
				{
					return r;
				}
			}

			if (path.Length < 2)
				return r;

			for (int i = 0; i < path.Length; i++)
			{
				if (i < path.Length - 1)
					r += path[i] + "/";
				else
					r += path[i];
			}
			if (AssetDatabase.IsValidFolder(r))
				return r;

			r = Auxiliaries.CreateFolder(path [0], path [1]);
			for (int i = 2; i < path.Length; i++)
			{
				r = Auxiliaries.CreateFolder(r, path[i]);
			}
			return r;
		}

		public static string CreateFolderCsAPI (string path)
		{
			DirectoryInfo di = Directory.CreateDirectory (path);

			return di.Exists? path: "";
		}

		public static bool CreateGraph (string graphName, GraphType graphType)
		{
			//string graphsFolderPath = CreateFolder (resourcesFolderPath, graphsFolderName);

			//string stateMachinesFolderPath = CreateFolder (graphsFolderPath, stateMachinesFolderName);

			string folderToCreateIn = OpenChooseFolderMenu_GraphCreation ("Create Graph in Folder");

			if (string.IsNullOrEmpty(folderToCreateIn))
				return false;

			return CreateGraphStateMachine (folderToCreateIn, graphName, graphType);
		}


		static bool CreateGraphStateMachine (string path, string graphName, GraphType graphType)
		{
			if ( ! AssetDatabase.IsValidFolder (path)) 
			{
				Debug.LogWarning ("Diamond can't found the path: " + path);

				Debug.LogWarning ("So he created the graph at the assets root");

				//Debug.Log ("if you want him to create graphs in a resources folder so create the following folder: " + resourcesFolderPath);
			
				path = AbsoluteToProjectRelativePath (Application.dataPath);
			}

			string graphPath = CreateFolder (path, graphName);

			//Debug.Log (graphPath);

			Graph graph = ScriptableObject.CreateInstance <GraphStateMachine> ();

			if (graph != null)
			{
				Diamond diamond = EditorWindow.GetWindow <Diamond> ();

				if (diamond != null) 
				{
					string graphnRacine = graphName;

					graphName += "_SM_" + GraphTypeToName (graphType);

					AssetDatabase.CreateAsset (graph, graphPath + "/" + graphName + ".asset");


					diamond.graph = graph;

					ClassesNamesManager.GraphPathToFile (true, diamond);

					graph.Init (graphName, graphnRacine, true, graphPath, graphType);

					SaveAndRefreshAssetsForced ();

					return true;
				}

				return false;
			}

			return false;
		}

		static string GraphTypeToName (GraphType gt)
		{
			string r = "";

			switch (gt)
			{
			case GraphType.Editor:
				r = "E";
				break;

			case GraphType.MonoBehaviour:
				r = "M";
				break;

			case GraphType.Shader:
				r = "Sh";
				break;

			case GraphType.Static:
				r = "S";
				break;
			}

			return r;
		}



		//public static void BackupGraph (Graph graph, string path)
		//{
		//	//Debug.Log ("tictac");
		//
		//	if (graph == null)
		//		return;
		//	
		//	if (string.IsNullOrEmpty (path))
		//		return;
		//	
		//	if (path.Length < 7)
		//		return;
		//
		//	Graph graphClone = UnityEngine.Object.Instantiate (graph) as Graph;
		//	AssetDatabase.CreateAsset (graphClone, StringTreatment.SubtractWeakFromEnd(path, ".asset") + StringTreatment.backup + ".asset");
		//	for (int i = 0; i < graphClone.nodes.Count; i++)
		//	{
		//		Node nodeClone = UnityEngine.Object.Instantiate (graphClone.nodes [i]) as Node;
		//		nodeClone.name = graphClone.nodes [i].name + StringTreatment.backup;
		//		nodeClone.Init (graphClone.nodes [i].rect.position, graphClone, false, graphClone.nodes [i].isIdle, false, true);
		//		if ( ! graphClone.nodes [i].isIdle)
		//			nodeClone.nodeName = graphClone.nodes [i].nodeName;
		//		AssetDatabase.AddObjectToAsset (nodeClone, graphClone);
		//
		//		for (int j = 0; j < graphClone.nodes [i].logics.Count; j++)
		//		{
		//			Logic logicClone = UnityEngine.Object.Instantiate (graphClone.nodes [i].logics [j]) as Logic;				
		//			logicClone.name = graphClone.nodes [i].logics [j].name;
		//			logicClone.Init (graphClone.nodes [i].logics [j].name, graphClone.nodes [i]);
		//			AssetDatabase.AddObjectToAsset (logicClone, graphClone);
		//
		//			for (int k = 0; k < graphClone.nodes [i].logics [j].nodes.Count; k++)
		//			{
		//				LogicNode logicNodeClone = UnityEngine.Object.Instantiate (graphClone.nodes [i].logics [j].nodes [k]) as LogicNode;
		//				logicNodeClone.name = graphClone.nodes [i].logics [j].nodes [k].name;
		//				logicNodeClone.LogicNodeInit (graphClone.nodes [i].logics [j], 
		//					graphClone.nodes [i].logics [j].nodes [k].rect.position);
		//				AssetDatabase.AddObjectToAsset (logicNodeClone, graphClone);
		//			}
		//		}
		//	}
		//
		//	SaveAndRefreshAssetsForced ();
		//}

		public static void BackupGraph (Graph graph, string path)
		{
			//Debug.Log (Diamond.graphBackupTimeCount);

			string from_ = RelativeToAbsolutePath (path);

			string to = RelativeToGraphBackupPath (path) + "/" + graph.name + ".asset";

			if (string.IsNullOrEmpty (from_))
				return;

			if (string.IsNullOrEmpty (to))
				return;

			if (System.IO.File.Exists (to))
				System.IO.File.Delete (to);

			System.IO.File.Copy (from_, to);
		}

		public static string AbsoluteToProjectRelativePath (string p)
		{
			int applicationDataPathLength = Application.dataPath.Length;

			return p.Substring (applicationDataPathLength - 6);
		}
		public static string AbsoluteToProjectRelativePath_withoutAssets (string p)
		{
			int applicationDataPathLength = Application.dataPath.Length;

			return p.Substring (applicationDataPathLength);
		}

		public static string RelativeToAbsolutePath (string p)
		{
			int applicationDataPathLength = Application.dataPath.Length;

			return StringTreatment.SubtractWeakFromEnd (Application.dataPath, "Assets") + p;
		}

		static string RelativeToGraphBackupPath (string p)
		{
			int applicationDataPathLength = Application.dataPath.Length;

			string path = StringTreatment.SubtractWeakFromEnd (Application.dataPath, "Assets") + StringTreatment.GraphsBackupFolderName;

			DirectoryInfo di = Directory.CreateDirectory (path);

			return di.Exists? path: "";
		}

		public static bool IsPathInProjectAssets (string AbsPath)
		{
			if (AbsPath.Length < Application.dataPath.Length)
				return false;

			return Application.dataPath == AbsPath.Substring (0, Application.dataPath.Length);
		}


		public static void LoadGraph ()
		{
			Graph graph;

			//Debug.Log (Application.dataPath);

			//string pathInput = Application.dataPath + "/Mezanix/Chocolate/Resources/Graphs";

			//string pathInput = resourcesFolderPath + "/" + graphsFolderName + "/" + stateMachinesFolderName;

			string pathInput = projectFolderPath;

			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			string graphNameExt = "";

			if (diamond != null)
			{				
				pathInput = diamond.GetGraphPath ();
			}

			if (pathInput == "Searching a Graph Path for a null graph")
			{
				pathInput = Application.dataPath;
			}
			else if (pathInput != "Searching a Graph Path for a null graph")
			{
				graphNameExt = diamond.graph.name + ".asset";

				pathInput = StringTreatment.SubtractWeakFromEnd (pathInput, graphNameExt + "/");
			}

			if ( ! AssetDatabase.IsValidFolder (pathInput))
			{
				pathInput = Application.dataPath;
			}

			string path = EditorUtility.OpenFilePanel (
				"Load Graph",
				pathInput,
				"asset");



			if (string.IsNullOrEmpty (path))
				return;



			string pathNew = AbsoluteToProjectRelativePath (path);




			//GameObjectFinderAction.HasGofToIdentified ();

			graph = AssetDatabase.LoadAssetAtPath (
				pathNew, typeof (Graph)) as Graph;

			if (graph != null)
			{
				diamond = EditorWindow.GetWindow <Diamond> ();

				if (diamond != null)
				{
					diamond.graph = graph;

					ClassesNamesManager.GraphPathToFile (true, diamond);

					graph.ScriptGenerationFolderPathToNamesToSave ();

					SaveAndRefreshAssetsForced ();

					graph.framesFromLoad = 0;

					Graph.objectFieldsEnabled = false;

					//graph.GetIdentifiedGameObjects ();

					//graph.InitObjectFieldsByUniqueIds ();

					//Debug.Log ("graph.framesFromLoad = " + graph.framesFromLoad.ToString ());

					CsScriptWriter.WriteStatesNamesEnumScript ();

					SaveAndRefreshAssetsForced ();
				}
				else if (diamond == null)
				{
					EditorUtility.DisplayDialog ("Diamond window lost", "Diamond is lost, " +
						"Where are you Diamond :-) " +
						"open a new Diamond and reload the graph.", "Ok");
				}
			}
			else if (graph == null)
			{
				//EditorUtility.DisplayDialog ("Asset type mismatch", "The choosen asset can not be loaded as a Diamond Graph", "Ok");
			}
		}

		public static bool LoadGraph (string path)
		{
			Graph graph;
			
			if (string.IsNullOrEmpty (path))
			{
				//Debug.LogWarning ("Graph failed to load, searching it with an path empty");
				return false;
			}


			//GameObjectFinderAction.HasGofToIdentified ();

			graph = AssetDatabase.LoadAssetAtPath (
				path, typeof (Graph)) as Graph;

			if (graph != null)
			{
				Diamond diamond = EditorWindow.GetWindow <Diamond> ();

				if (diamond != null)
				{
					diamond.graph = graph;

					ClassesNamesManager.GraphPathToFile (true, diamond);

					graph.ScriptGenerationFolderPathToNamesToSave ();

					graph.framesFromLoad = 0;

					Graph.objectFieldsEnabled = false;

					//graph.GetIdentifiedGameObjects ();

					//graph.InitObjectFieldsByUniqueIds ();

					//Debug.Log ("graph.framesFromLoad = " + graph.framesFromLoad.ToString ());

					CsScriptWriter.WriteStatesNamesEnumScript ();

					SaveAndRefreshAssetsForced ();

					return true;
				}
				else if (diamond == null)
				{
					//EditorUtility.DisplayDialog ("Diamond window lost", "Diamond is lost, " +
					//	"Where are you Diamond :-) " +
					//	"open a new Diamond and reload the graph.", "Ok");

					Debug.LogWarning ("Diamond is lost, " +
						"Where are you Diamond :-) " +
						"open a new Diamond and reload the graph.");
					return false;
				}
			}
			else if (graph == null)
			{
				//EditorUtility.DisplayDialog ("Asset type mismatch", "The choosen asset can not be loaded as a Diamond Graph", "Ok");

				//Debug.LogWarning ("Graph failed to load, the found asset is null");
				return false;
			}

			return false;
		}

		public static void ClearGraph ()
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond != null)
			{
				if (EditorUtility.DisplayDialog ("Clear Graph", 
					"Are you sure you want to remove all nodes of the current graph? \n" +
					"\n" +
					"It is an irreversible action !",
					"Yes", "no"))
				{
					Graph graph = diamond.graph;

					if (graph != null)
					{
						graph.ClearAndInit (graph.graphName);
					}
				}
			}
		}


		public static string OpenChooseFolderMenu (string MenuTitle)
		{
			//string pathInput = resourcesFolderPath + "/" + graphsFolderName + "/" + stateMachinesFolderName;

			string pathInput = projectFolderPath;

			if ( ! AssetDatabase.IsValidFolder (pathInput))
				pathInput = Application.dataPath;

			string path = EditorUtility.OpenFolderPanel (
				MenuTitle,
				pathInput,
				"");



			if (string.IsNullOrEmpty (path))
				return "";
			
			if ( ! IsPathInProjectAssets (path))
			{
				return "";
			}

			int applicationDataPathLength = Application.dataPath.Length;

			string pathNew = path.Substring (applicationDataPathLength - 6);


			SaveAndRefreshAssetsForced ();

			return pathNew; 
		}

		public static string OpenChooseFolderMenu_GraphCreation (string MenuTitle)
		{
			//string pathInput = resourcesFolderPath + "/" + graphsFolderName + "/" + stateMachinesFolderName;

			string pathInput = projectFolderPath;

			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			string graphNameExt = "";

			if (diamond != null)
			{				
				pathInput = diamond.GetGraphPath ();
			}

			if (pathInput == "Searching a Graph Path for a null graph")
			{
				pathInput = Application.dataPath;
			}
			else if (pathInput != "Searching a Graph Path for a null graph")
			{
				graphNameExt = diamond.graph.name + ".asset";

				pathInput = StringTreatment.SubtractWeakFromEnd (pathInput, graphNameExt + "/");

				pathInput = StringTreatment.BeforeLastSlash (pathInput, false);
			}

			if ( ! AssetDatabase.IsValidFolder (pathInput))
			{
				pathInput = Application.dataPath;
			}

			string path = EditorUtility.OpenFolderPanel (
				MenuTitle,
				pathInput,
				"");



			if (string.IsNullOrEmpty (path))
				return "";

			if ( ! IsPathInProjectAssets (path))
			{
				return "";
			}

			int applicationDataPathLength = Application.dataPath.Length;

			string pathNew = path.Substring (applicationDataPathLength - 6);


			SaveAndRefreshAssetsForced ();

			return pathNew; 
		}


		public static void CreateNode (string itemName, Vector2 rightClickPosition, Graph graph, bool writeStatesNamesEnumScript)
		{
			if (itemName == Enums.addState)
			{
				CreateNodeState (rightClickPosition, graph, writeStatesNamesEnumScript);
			}
		}

		public static void CreateNode (string itemName, Vector2 rightClickPosition, Logic logic)
		{

			if (itemName == Enums.addLogicNode)
			{
				CreateLogicNode (rightClickPosition, logic, "");
			}
			else if (
				itemName == Enums.addProjectVariable_bool ||
				itemName == Enums.addProjectVariable_float ||
				itemName == Enums.addProjectVariable_int ||
				itemName == Enums.addProjectVariable_string ||
				itemName == Enums.addProjectVariable_vector2 ||
				itemName == Enums.addProjectVariable_vector3 ||
				itemName == Enums.addProjectVariable_vector4 ||
				itemName == Enums.addProjectVariable_gameObject)
			{
				CreateProjectVariable (rightClickPosition, itemName, logic);
			}
			else
			{
				CreateLogicNode (rightClickPosition, logic, itemName);
			}
		}

		#region Nodes Creation Functions
		static void CreateProjectVariable (Vector2 rightClickPosition, string itemName, Logic logic)
		{
			ProjectVariable projectVariable = ScriptableObject.CreateInstance <ProjectVariable> ();

			if (projectVariable == null)
				return;

			projectVariable.name = DatesTimesAndFrequences.DateTimeNow ();

			if (itemName == Enums.addProjectVariable_bool)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, false, 
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_float)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, 1f, 
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_int)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, 0,
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_string)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, "", 
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_vector2)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, Vector2.zero,
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_vector3)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, Vector3.zero, 
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_vector4)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true, Vector4.zero, 
					rightClickPosition, logic);
			}
			else if (itemName == Enums.addProjectVariable_gameObject)
			{
				projectVariable.Init ("v" + DatesTimesAndFrequences.DateTimeNow (), true, true,
					VariableTypeForProject.GameObject, 
					rightClickPosition, logic);
			}

			//string projectVariablesFolderPath = CreateProjectVariablesFolder ();

			//AssetDatabase.CreateAsset (projectVariable, projectVariablesFolderPath + "/" + projectVariable.name + ".asset");
		}

		public static string CreateProjectVariablesFolder ()
		{
			string graphsFolderPath = CreateFolder (resourcesFolderPath, graphsFolderName);

			return CreateFolder (graphsFolderPath, projectVariablesFolderName);
		}

		public static string CreateNamesToSaveFolder ()
		{
			string graphsFolderPath = CreateFolder (resourcesFolderPath, graphsFolderName);

			return CreateFolder (graphsFolderPath, namesToSaveFolderName);
		}

		static void CreateLogicNode (Vector2 rightClickPosition, Logic logic, string setSpecialNodeType)
		{
			LogicNode node = ScriptableObject.CreateInstance <LogicNode> ();

			if (node != null)
			{
				node.LogicNodeInit (logic, rightClickPosition, setSpecialNodeType, false);
			}
		}

		public static LogicNode CreateLogicNodeForDuplication (Vector2 rightClickPosition, Logic logic, string logicNodeSpecialType)
		{
			LogicNode node = ScriptableObject.CreateInstance <LogicNode> ();

			if (node == null)
				return null;

			node.LogicNodeInit (logic, rightClickPosition, logicNodeSpecialType, false);

			return node;
		}

		static void CreateNodeState (Vector2 rightClickPosition, Graph graph, bool writeStatesNamesEnumScript)
		{
			Node node = ScriptableObject.CreateInstance <NodeState> ();

			if (node != null)
			{
				node.Init (rightClickPosition, graph, true, false, writeStatesNamesEnumScript);
			}
		}

		public static void CreateNodeState (Vector2 rightClickPosition, Graph graph, bool setIsIdle, 
			bool writeStatesNamesEnumScript)
		{
			Node node = ScriptableObject.CreateInstance <NodeState> ();

			if (node != null)
			{
				node.Init (rightClickPosition, graph, ! setIsIdle, setIsIdle, writeStatesNamesEnumScript);
			}
		}



		#endregion Nodes Creation Functions



	

		public static void AddObjectToAssetSaveAndRefresh (UnityEngine.Object objectToAdd, UnityEngine.Object asset)
		{
			AssetDatabase.AddObjectToAsset (objectToAdd, asset);

			SaveAndRefreshAssets ();
		}

		public static bool SaveAndRefreshAssets ()
		{
			bool r = false;

			try
			{
				AssetDatabase.SaveAssets ();

				AssetDatabase.Refresh ();
			}
			catch
			{
				return r;
			}

			r = true;
			return r;
		}

		public static void MakeActiveSceneDirty ()
		{
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (
				UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
		}

		public static void SaveActiveScene ()
		{
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (
				UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());

			UnityEditor.SceneManagement.EditorSceneManager.SaveScene (
				UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
		}

		public static bool SaveAndRefreshAssetsForced ()
		{
			bool r = false;

			Graph graph = null;

			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond != null) 
			{
				if (diamond.graph != null)
				{
					graph = diamond.graph;
				}
			}

			//CreateNode (Enums.addState, Vector2.one, graph, false);
			//
			//graph.RemoveNode (graph.nodes.Count-1);

			if (graph != null)
				EditorUtility.SetDirty (graph);

			if (Diamond.projectVariables != null)
				EditorUtility.SetDirty (Diamond.projectVariables);

			if (Diamond.namesToSave != null)
				EditorUtility.SetDirty (Diamond.namesToSave);

			r = SaveAndRefreshAssets ();

			//GameObjectFinderAction.HasGofToIdentified ();

			//Debug.Log ("Diamond Graph saved");
			Diamond.changesOccured = false;

			return r;
		}


	}
}
