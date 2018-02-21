using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Mezanix.Diamond
{
	public class UndoRedoVersion
	{
		public string uniqueID = "";

		public List<string> lst = new List<string> ();

		public UndoRedoVersion (string setUniqueId)
		{
			uniqueID = setUniqueId;
		}
	}

	public class UndoRedoWriterReader 
	{
		const string tab_1 = "\t";
		const string tab_2 = "\t\t";
		const string tab_3 = "\t\t\t";
		const string tab_4 = "\t\t\t\t";

		const string ret = "\n";

		const string start = "{";
		const string end = "}";

		const string refAffector = " = ";

		const string versionRef = "version";
		const string nodeStateRef = "nodeState";
		const string logicRef = "logic";
		const string logicNodeRef = "logicNode";

		const string graphNameRef = "grapheName";
		const string grapheNameRacineName = "graphNameRacine";
		const string nodeSelectedIdRef = "nodeSelectedId";
		const string uniqueIDRef = "uniqueId";
		const string editingLogicRef = "editingLogic";
		const string grapheTypeRef = "grapheType";

		const string idRef = "id";
		const string nodeNameRef = "nodeName";
		const string rectRef = "rect";
		const string selectionStateRef = "selectionState";
		const string sourcesRef = "sources";
		const string destinatinosRef = "destinations";
		const string callEditingLogicRef = "callEditingLogic";
		const string editingLogicIdRef = "editingLogicId";
		const string changingLogicsExecutionOrderRef = "changingLogicsExecutionOrder";

		const string logicNameRef = "logicName";
		const string logicNodeSelectedIdRef = "logicNodeSelectedId";
		const string rectMaximizedRef = "rectMaximized";
		const string inOutAdressCurrentToLinkRef = "inOutAdressCurrentToLink";

		const string rectPositionRef = "rectPosition";
		const string logicTypeRef = "logicType";
		const string variableTypeRef = "variableType";
		const string computeTypeRef = "computeType";
		const string maximizedRef = "maximized";


		static List <UndoRedoVersion> versions = new List<UndoRedoVersion> ();

		public static void WriteVersion (Graph graph)
		{
			//string graphPathFolder = graph.GetPathFolder ();

			versions.Add (
				new UndoRedoVersion (DatesTimesAndFrequences.DateTimeNow ()));
		}
	}
}
