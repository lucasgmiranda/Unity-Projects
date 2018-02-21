using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Mezanix.Diamond
{
	public class Mdstr 
	{
		#region global variables
		public static readonly string projectFolder = @"Assets";

		#endregion global variables

		static string GraphFolderPath (Graph graph)
		{
			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond.graph != graph)
				return "";

			string graphPath = diamond.GetGraphPath ();

			return StringTreatment.SubtractWeakFromEnd (graphPath, graph.graphName + ".asset");
		}

		static void ListToFile (string folder, 
			string fileName, List<string> lst)
		{
			string filePath = folder + "/" + fileName + ".mdstr";

			File.WriteAllText (filePath, "");

			for (int i = 0; i < lst.Count; i++)
			{
				File.AppendAllText (filePath, lst [i]);
			}

			Auxiliaries.SaveAndRefreshAssetsForced ();
		}

		static void AddToList (string s)
		{
			lst.Add (tabs + s + ret);
		}

		class Openers
		{
			public const string GraphStateMachine = "{GraphStateMachine";

			public const string NodeState = "{NodeState";

			public const string Logic = "{Logic";

			public const string LogicNode = "{LogicNode";
			//
		}

		class Closers
		{
			public const string GraphStateMachine = "GraphStateMachine}";

			public const string NodeState = "NodeState}";

			public const string Logic = "Logic}";

			public const string LogicNode = "LogicNode}";
		}

		class VarNames
		{
			public const string uniqueID = "uniqueID";

			public const string graphType = "graphType";

			public const string graphNameRacine = "graphNameRacine";

			public const string nodeName = "nodeName";

			public const string rect_position = "rect.position";

			public const string logicName = "logicName";
			//
		}

		const string ret = ";\n";
		static string tabs = "";
		const string eq = "=";

		static List<string> lst;

		public class Writer
		{
			public static void Write (Graph graph)
			{
				string graphFolder = GraphFolderPath (graph);


				lst = new List<string> ();

				AddToList (Openers.GraphStateMachine);
				{tabs = "\t";
					AddToList (VarNames.uniqueID + eq + graph.uniqueID);
					AddToList (VarNames.graphType + eq + graph.graphType.ToString ());
					AddToList (VarNames.graphNameRacine + eq + graph.graphNameRacine);

					for (int i = 0; i < graph.nodes.Count; i++)
					{
						Node nodeState = graph.nodes [i];
						if (nodeState == null)
							continue;
						AddToList (Openers.NodeState);
						{tabs = "\t\t";
							AddToList (VarNames.uniqueID + eq + nodeState.uniqueID);
							AddToList (VarNames.nodeName + eq + nodeState.nodeName);
							AddToList (VarNames.rect_position + eq + nodeState.rect.position.ToString ());

							for (int j = 0; j < nodeState.logics.Count; j++)
							{
								Logic logic = nodeState.logics [j];
								if (logic == null)
									continue;
								AddToList (Openers.Logic);
								{tabs = "\t\t\t";
									AddToList (VarNames.uniqueID + eq + logic.uniqueID);
									AddToList (VarNames.logicName + eq + logic.logicName);

									for (int k = 0; k < logic.nodes.Count; k++)
									{
										LogicNode logicNode = logic.nodes [k];
										if (logicNode == null)
											continue;
										AddToList (Openers.LogicNode);
										{tabs = "\t\t\t\t";
											AddToList (VarNames.uniqueID + eq + logicNode.uniqueID);
											AddToList (VarNames.nodeName + eq + logicNode.nodeName);
											AddToList (VarNames.rect_position + eq + logicNode.rect.position.ToString ());
										}tabs = "\t\t\t";
										AddToList (Closers.LogicNode);
									}
								}tabs = "\t\t";
								AddToList (Closers.Logic);
							}
						}tabs = "\t";
						AddToList (Closers.NodeState);
					}
				}tabs = "";
				AddToList (Closers.GraphStateMachine);


				ListToFile (graphFolder, graph.graphNameRacine, lst);
			}
		}

		public class Reader
		{
		}
	}
}
