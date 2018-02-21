using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Mezanix.Diamond
{
	public class CsScriptWriter 
	{
		#region global variables
		public static readonly string projectFolder = @"Assets";

		public static readonly string auxiliariesFolder = @"Assets/Mezanix/Diamond/Scripts/Editor/Auxiliaries";

		//public static readonly string scriptsFolderName = "Mezanix/GlobalScriptsCreatedByDiamond";
		public static readonly string scriptsFolderName = "Mezanix/Diamond/Scripts/GlobalScriptsCreatedByDiamond";
		//static string scriptsFolderPath = "scripts folder here";

		public static readonly string projectVariablesFolderName = "1_ProjectVariables";


		public const string projectVariablesEndOfInit_prefix = "//#pveoi";

		public const string projectVariablesBeginOfInit_prefix = "//#pvboi";

		public const string projectVariablesEndOfDeclaration_prefix = "//#pveod";

		public const string projectVariablesBeginOfDeclaration_prefix = "//#pvbod";


		public const string projectVariablesEndOfInit = projectVariablesEndOfInit_prefix +
			" project variables end of init\n";

		public const string projectVariablesBeginOfInit = projectVariablesBeginOfInit_prefix +
			" project variables begin of init\n";

		public const string projectVariablesEndOfDeclaration = projectVariablesEndOfDeclaration_prefix +
			" project variables end of declaration\n";

		public const string projectVariablesBeginOfDeclaration = projectVariablesBeginOfDeclaration_prefix +
			" project variables begin of declaration\n";


		static List<string> statesNamesEnumScriptList = new List<string> ();

		public static string [] tags;
		#endregion global variables

		public static string CreateFolder (string parentFolder, string folderName)
		{
			if (AssetDatabase.IsValidFolder (parentFolder + "/" + folderName))
				return parentFolder + "/" + folderName;
		
			return AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (parentFolder, folderName));
		}



		#region mother methods
		static List <string> projectVariableRecentlyWriten = new List<string> ();

		static void AddProjectVariablesLigne (ref List <string> lst, string lineToAdd)
		{
			lst.Add (lineToAdd);

			projectVariableRecentlyWriten.Add (lineToAdd);
		}

		public static void FillCSharpProjectVariablesList (ref List<string> lst,
			List <string> declReaden, List <string> initReaden)
		{
			projectVariableRecentlyWriten = new List<string> ();

			lst = new List<string>();


			AddProjectVariablesLigne (ref lst, "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\nnamespace ScriptsCreatedByDiamond \n{\n\tpublic class ProjectVariables \n\t{\n");

			AddProjectVariablesLigne (ref lst, projectVariablesBeginOfDeclaration);


			//AddProjectVariablesLigne (ref lst, "\t\tpublic static List <int> boolValueIndices = new List<int>();\n\n\t\tpublic static List <bool> boolValue = new List<bool>();\n\n\n\t\tpublic static List <int> floatValueIndices = new List<int>();\n\n\t\tpublic static List <float> floatValue = new List<float>();\n\n\n\t\tpublic static List <int> intValueIndices = new List<int>();\n\n\t\tpublic static List <int> intValue = new List<int>();\n\n\n\t\tpublic static List <int> stringValueIndices = new List<int>();\n\n\t\tpublic static List <string> stringValue = new List<string>();\n\n\n\t\tpublic static List <int> vector2ValueIndices = new List<int>();\n\n\t\tpublic static List <Vector2> vector2Value = new List<Vector2>();\n\n\n\n\t\tpublic static List <int> vector3ValueIndices = new List<int>();\n\n\t\tpublic static List <Vector3> vector3Value = new List<Vector3>();\n\n\n\n\t\tpublic static List <int> vector4ValueIndices = new List<int>();\n\n\t\tpublic static List <Vector4> vector4Value = new List<Vector4>();\n\n\n");
			for (int i = 0; i < Diamond.projectVariables.projectVariables.Count; i++)
			{
				AddProjectVariablesLigne (ref lst, "\t\t" + "// " + Diamond.projectVariables.projectVariables [i].myName + "\n");

				AddProjectVariablesLigne (ref lst, "\t\tpublic static " + Diamond.projectVariables.projectVariables [i].TypeToVarDeclaration () + " " +
					Diamond.projectVariables.projectVariables [i].uniqueID + ";\n\n");
			}
			ProjectVariablesAddReadenMissings (declReaden, ref lst);
			AddProjectVariablesLigne (ref lst, projectVariablesEndOfDeclaration);





			AddProjectVariablesLigne (ref lst, "\t\tpublic static void Init ()\n\t\t{\n");
		


			AddProjectVariablesLigne (ref lst, projectVariablesBeginOfInit);
			projectVariableRecentlyWriten = new List<string> ();

	
			for (int i = 0; i < Diamond.projectVariables.projectVariables.Count; i++)
			{
				AddProjectVariablesLigne (ref lst, "\t\t" + "// " + Diamond.projectVariables.projectVariables [i].myName + "\n");
				string varName = Diamond.projectVariables.projectVariables [i].uniqueID;

				switch (Diamond.projectVariables.projectVariables [i].variableTypeForProject)
				{
				case VariableTypeForProject.Bool:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						StringTreatment.FirstToLower (Diamond.projectVariables.projectVariables [i].boolValue.ToString ()) +
						";\n\n");
					break;

				case VariableTypeForProject.Float:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						Diamond.projectVariables.projectVariables [i].floatValue.ToString () + "f" +
						";\n\n");
					break;

				case VariableTypeForProject.Int:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						Diamond.projectVariables.projectVariables [i].intValue.ToString () +
						";\n\n");
					break;

				case VariableTypeForProject.String:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						"\"" + Diamond.projectVariables.projectVariables [i].stringValue + "\"" +
						";\n\n");
					break;

				case VariableTypeForProject.Vector2:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						LogicNode.Vector2ToScriptWrite (Diamond.projectVariables.projectVariables [i].vector2Value) +
						";\n\n");
					break;

				case VariableTypeForProject.Vector3:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						LogicNode.Vector3ToScriptWrite (Diamond.projectVariables.projectVariables [i].vector3Value) +
						";\n\n\n");
					break;

				case VariableTypeForProject.Vector4:
					AddProjectVariablesLigne (ref lst, "\t\t\t" + varName + " = " +
						LogicNode.Vector4ToScriptWrite (Diamond.projectVariables.projectVariables [i].vector4Value) +
						";\n\n");
					break;
				}
			}

			ProjectVariablesAddReadenMissings (initReaden, ref lst);
			AddProjectVariablesLigne (ref lst, projectVariablesEndOfInit);
			AddProjectVariablesLigne (ref lst, "\t\t}\n");


			AddProjectVariablesLigne (ref lst, "\t}\n}");




		}

		static void ProjectVariablesAddReadenMissings (List <string> declInitReaden, ref List <string> lst)
		{

			for (int i = 0; i < declInitReaden.Count; i++)
			{
				bool readenHere = false;
				for (int j = 0; j < projectVariableRecentlyWriten.Count; j++)
				{
					if (projectVariableRecentlyWriten [j].Contains (declInitReaden [i]))
					{
						readenHere = true;

						break;
					}
				}
				if ( ! readenHere)
				{
					lst.Add (declInitReaden [i]);
				}
			}
		}

		public static void FillCSharpMonoBehaviourList (string scriptName, ref List<string> lst,
			string[] usingNames, string[] statesNames, Graph theGraph)
		{
			int tabNb = 0;

			CSharpMonoBehaviourFirstBlocToList (scriptName, ref lst, ref tabNb, usingNames);
		
			CSharpMonoBehaviourMethodsToList (ref lst, ref tabNb, statesNames, scriptName, theGraph); 

			CSharpCloseClassBlocToList (ref lst, ref tabNb);
		}

		public static void FillCSharpInterfaceList (string scriptName, ref List<string> lst,
			string[] usingNames, string[] statesNames)
		{
			int tabNb = 0;

			CSharpInterfaceFirstBlocToList (scriptName, ref lst, ref tabNb, usingNames);

			CSharpInterfaceMethodsToList (ref lst, ref tabNb, statesNames); 

			CSharpCloseClassBlocToList (ref lst, ref tabNb);
		}

		public static void FillCSharpStateList (string scriptName, ref List<string> lst, 
			string[] usingNames, string[] statesNames, string graphName, Node theState)
		{
			int tabNb = 0;

			CSharpStateFirstBlocToList (scriptName, ref lst, ref tabNb, usingNames, graphName, theState);

			CSharpStateMethodsToList (ref lst, ref tabNb, statesNames, graphName, scriptName, 
				theState);

			CSharpCloseClassBlocToList (ref lst, ref tabNb);
		}

		public static void FillCSharpLogicList (string graphName, string stateName, Logic theLogic,
			ref List<string> lst)
		{
			int tabNb = 0;
			
			CSharpLogicFirstBlocToList (
				new string[] 
				{
					"using UnityEngine;\n",
					"using System;\n",
					"using System.IO;\n",
					"using System.Collections;\n",
					"using System.Collections.Generic;\n"
				}, graphName, stateName, theLogic, ref lst, ref tabNb);

			CSharpLogicMethodsToList (theLogic, graphName, stateName, ref lst, ref tabNb);

			CSharpCloseClassBlocToList (ref lst, ref tabNb);
		}

		public static void FillCSharpLogicNodeList (string graphName, string stateName, Logic theLogic, 
			LogicNode theLogicNode, Graph theGraph, Node theState, 
			ref List<string> lst)
		{
			int tabNb = 0;

			CSharpLogicNodeFirstBlocToList (
				new string[] 
				{
					"using UnityEngine;\n",
					"using UnityEngine.UI;\n",
					"using System;\n",
					"using System.IO;\n",
					"using System.Collections;\n",
					"using System.Collections.Generic;\n"
				}, graphName, stateName, theLogic, theLogicNode.nodeName, theLogicNode, theGraph, theState, ref lst, ref tabNb);


			CSharpCloseClassBlocToList (ref lst, ref tabNb);
		}
		#endregion mother methods


		#region logic node
		static void CSharpLogicFirstBlocToList (string [] usingNames, string graphName, string stateName, Logic theLogic,
			ref List<string> lst, ref int tabNb)
		{
			lst = new List<string> ();

			for (int i = 0; i < usingNames.Length; i++)
			{
				lst.Add (usingNames [i]);
			}

			lst.Add ("\n");

			lst.Add ("namespace ScriptsCreatedByDiamond \n");
			lst.Add ("{\n");
			tabNb++;


			lst.Add (WriteTabs (tabNb) + "public class " + graphName + "_" + stateName + "_" + theLogic.logicName + " \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			for (int i = 0; i < theLogic.nodes.Count; i++)
			{
				lst.Add (WriteTabs (tabNb) + "public " +
					graphName + "_" + stateName + "_" + theLogic.logicName + "_" + theLogic.nodes [i].nodeName + " " +
					StringTreatment.FirstToLower (
						graphName + "_" + stateName + "_" + theLogic.logicName + "_" + theLogic.nodes [i].nodeName) +
					";\n");
			}
			lst.Add (WriteTabs (tabNb) + "\n");


			lst.Add (WriteTabs (tabNb) + "public " +
				graphName + "_" + stateName + " " + 
				StringTreatment.FirstToLower (graphName + "_" + stateName) + ";\n\n");

			lst.Add (WriteTabs (tabNb) + "public " +
				graphName + "_" + stateName + "_" + theLogic.logicName + " (" + 
				graphName + "_" + stateName + " set" + graphName + "_" + stateName + ")\n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + 
				StringTreatment.FirstToLower (graphName + "_" + stateName) + " = " + "set" + graphName + "_" + stateName + ";\n\n");

			for (int i = 0; i < theLogic.nodes.Count; i++)
			{
				lst.Add (WriteTabs (tabNb) + 
					StringTreatment.FirstToLower (
						graphName + "_" + stateName + "_" + theLogic.logicName + "_" + theLogic.nodes [i].nodeName) + 
					" = new " +
					graphName + "_" + stateName + "_" + theLogic.logicName + "_" + theLogic.nodes [i].nodeName
					+ " " + "(this);\n");
			}

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");
		}

		static void CSharpLogicMethodsToList (Logic theLogic, string graphName, string stateName,
			ref List<string> lst, ref int tabNb)
		{
			lst.Add (WriteTabs (tabNb) + "public void LogicUpdate ()\n");

			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			for (int i = 0; i < theLogic.nodes.Count; i++)
			{
				lst.Add (WriteTabs (tabNb) +
					StringTreatment.FirstToLower (
						graphName + "_" + stateName + "_" + theLogic.logicName + "_" + theLogic.nodes [i].nodeName) +
					".LogicNodeUpdate ();\n");
			}

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");

			lst.Add ("\t\tpublic void IAmHere ()\n\t\t{\n\t\t}\n");
		}


		static void CSharpLogicNodeFirstBlocToList (string [] usingNames, string graphName, string stateName, Logic theLogic,
			string logicNodeName, LogicNode theLogicNode, Graph theGraph, Node theState, 
			ref List<string> lst, ref int tabNb)
		{
			lst = new List<string> ();

			for (int i = 0; i < usingNames.Length; i++)
			{
				lst.Add (usingNames [i]);
			}

			lst.Add ("\n");

			lst.Add ("namespace ScriptsCreatedByDiamond \n");
			lst.Add ("{\n");
			tabNb++;

			//if (theLogicNode.logicType == LogicType.computeOrOperation)
			//{
			//	if (theLogicNode.variableType == VariableType.Color)
			//	{
			//		lst.Add ("\tpublic class ColorsArithmetic \n\t{\t\n\t\tpublic static Color RGB_255_To_Normalized (float r, float g, float b, float a)\n\t\t{\n\t\t\tfloat _255 = 255f;\n\t\t\t\n\t\t\treturn new Color (r/_255, g/_255, b/_255, a);\n\t\t}\n\n\t\tpublic static Color Opacity (Color c0, Color c1, float o)\n\t\t{\n\t\t\to = Mathf.Clamp (o, 0f, 1f);\n\t\t\t\n\t\t\treturn o * c0 + (1f - o) * c1;\n\t\t}\n\n\t\tpublic static Color Darken (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tMathf.Min (c0.r, c1.r), \n\t\t\t\tMathf.Min (c0.g, c1.g), \n\t\t\t\tMathf.Min (c0.b, c1.b), \n\t\t\t\tMathf.Min (c0.a, c1.a));\n\t\t}\n\n\t\tpublic static Color Lighten (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tMathf.Max (c0.r, c1.r), \n\t\t\t\tMathf.Max (c0.g, c1.g), \n\t\t\t\tMathf.Max (c0.b, c1.b), \n\t\t\t\tMathf.Max (c0.a, c1.a));\n\t\t}\n\n\t\tpublic static Color Multiply (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tc0.r * c1.r, \n\t\t\t\tc0.g * c1.g, \n\t\t\t\tc0.b * c1.b, \n\t\t\t\tc0.a * c1.a);\n\t\t}\n\n\t\tpublic static Color Screen (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\t1f - (1f-c0.r) * (1f-c1.r), \n\t\t\t\t1f - (1f-c0.g) * (1f-c1.g), \n\t\t\t\t1f - (1f-c0.b) * (1f-c1.b), \n\t\t\t\t1f - (1f-c0.a) * (1f-c1.a));\n\t\t}\n\n\t\tpublic static Color ColorDodge (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tColorDodge (c0.r, c1.r), \n\t\t\t\tColorDodge (c0.g, c1.g), \n\t\t\t\tColorDodge (c0.b, c1.b), \n\t\t\t\tColorDodge (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float ColorDodge (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tif (f0 == 1f)\n\t\t\t\tf = 1f;\n\t\t\telse\n\t\t\t\tf = Mathf.Min (1f, f1 / (1f - f0));\n\n\t\t\treturn f;\n\t\t}\n\n\t\tpublic static Color ColorBurn (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tColorBurn (c0.r, c1.r), \n\t\t\t\tColorBurn (c0.g, c1.g), \n\t\t\t\tColorBurn (c0.b, c1.b), \n\t\t\t\tColorBurn (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float ColorBurn (float f0, float f1)\n\t\t{\n\t\t\treturn f0 == 0f ? 0f : Mathf.Max (0f, (1f - (1f - f1)) / f0); \n\t\t}\n\n\t\tpublic static Color LinearDodge (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tc0.r + c1.r, \n\t\t\t\tc0.g + c1.g, \n\t\t\t\tc0.b + c1.b, \n\t\t\t\tc0.a + c1.a);\n\t\t}\n\n\t\tpublic static Color LinearBurn (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tc0.r + c1.r - 1f, \n\t\t\t\tc0.g + c1.g - 1f, \n\t\t\t\tc0.b + c1.b - 1f, \n\t\t\t\tc0.a + c1.a - 1f);\n\t\t}\n\n\t\tpublic static Color Overlay (Color c0, Color c1)\n\t\t{\t\t\n\t\t\treturn new Color (\n\t\t\t\tOverlay (c0.r, c1.r),\n\t\t\t\tOverlay (c0.g, c1.g),\n\t\t\t\tOverlay (c0.b, c1.b),\n\t\t\t\tOverlay (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float Overlay (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\t\t\t\n\t\t\tif (f1 <= 0.5f)\n\t\t\t\tf = 2f * f0 * f1;\n\t\t\telse\n\t\t\t\tf = 1f - 2f * (1f - f0) * (1f - f1);\n\n\t\t\treturn f;\n\t\t}\n\n\t\tpublic static Color HardLight (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tHardLight (c0.r, c1.r),\n\t\t\t\tHardLight (c0.g, c1.g),\n\t\t\t\tHardLight (c0.b, c1.b),\n\t\t\t\tHardLight (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float HardLight (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tif (f0 <= 0.5f)\n\t\t\t\tf = 2f * f0 * f1;\n\t\t\telse\n\t\t\t\tf = 1f - 2f * (1f - f0) * (1f - f1);\n\n\t\t\treturn f;\n\t\t}\n\n\t\tpublic static Color SoftLight (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tSoftLight (c0.r, c1.r),\n\t\t\t\tSoftLight (c0.g, c1.g),\n\t\t\t\tSoftLight (c0.b, c1.b),\n\t\t\t\tSoftLight (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float SoftLight (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tif (f0 <= 0.5f)\n\t\t\t\tf = (2f * f0 - 1f) * (f1 - f1*f1) + f1;\n\t\t\telse\n\t\t\t\tf = (2f * f0 - 1f) * (Mathf.Pow(f1,0.5f) - f1) + f1;\n\n\t\t\treturn f;\n\t\t}\n\n\t\tpublic static Color VividLight (Color c0, Color c1)\n\t\t{\n\n\t\t\treturn new Color (\n\t\t\t\tVividLight (c0.r, c1.r),\n\t\t\t\tVividLight (c0.g, c1.g),\n\t\t\t\tVividLight (c0.b, c1.b),\n\t\t\t\tVividLight (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float VividLight (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tif (f0 <= 0.5f) \n\t\t\t{\n\t\t\t\tif (f0 == 0f)\n\t\t\t\t\tf = 0f;\n\t\t\t\telse\n\t\t\t\t\tf = 1f - (0.5f * (1f - f1) / f0);\n\t\t\t} \n\t\t\telse \n\t\t\t{\n\t\t\t\tif (f0 == 1f)\n\t\t\t\t\tf = 1f;\n\t\t\t\telse\n\t\t\t\t\tf = 0.5f * (f1 / (1f - f0));\n\t\t\t}\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color LinearLight (Color c0, Color c1)\n\t\t{\n\t\t\t\n\t\t\treturn new Color (\n\t\t\t\tLinearLight (c0.r, c1.r),\n\t\t\t\tLinearLight (c0.g, c1.g),\n\t\t\t\tLinearLight (c0.b, c1.b),\n\t\t\t\tLinearLight (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float LinearLight (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tf = f1 + 2f*f0 - 1f;\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color PinLight (Color c0, Color c1)\n\t\t{\n\t\t\t\n\t\t\treturn new Color (\n\t\t\t\tPinLight (c0.r, c1.r),\n\t\t\t\tPinLight (c0.g, c1.g),\n\t\t\t\tPinLight (c0.b, c1.b),\n\t\t\t\tPinLight (c0.a, c1.a));\n\n\t\t}\n\n\t\tstatic float PinLight (float f0, float f1)\n\t\t{\n\t\t\tfloat f = 0f;\n\n\t\t\tif (f1 <= 2f * f0 - 1f)\n\t\t\t\tf = 2f * f0 - 1f;\n\t\t\telse if (f1 > 2f * f0 - 1f && f1 <= 2f * f0)\n\t\t\t\tf = f1;\n\t\t\telse if (f1 > 2f * f0)\n\t\t\t\tf = 2 * f0;\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color HardMix (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tHardMix (c0.r, c1.r),\n\t\t\t\tHardMix (c0.g, c1.g),\n\t\t\t\tHardMix (c0.b, c1.b),\n\t\t\t\tHardMix (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float HardMix (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tif (f0 < 1f - f1)\n\t\t\t\tf = 0f;\n\t\t\telse\n\t\t\t\tf = 1f;\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color Difference (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tDifference (c0.r, c1.r),\n\t\t\t\tDifference (c0.g, c1.g),\n\t\t\t\tDifference (c0.b, c1.b),\n\t\t\t\tDifference (c0.a, c1.a));\n\t\t}\n\n\t\tstatic float Difference (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tf = Mathf.Abs (f0 - f1);\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color Exclusion (Color c0, Color c1)\n\t\t{\n\t\t\treturn new Color (\n\t\t\t\tExclusion (c0.r, c1.r),\n\t\t\t\tExclusion (c0.g, c1.g),\n\t\t\t\tExclusion (c0.b, c1.b),\n\t\t\t\tExclusion (c0.a, c1.a));\n\n\t\t}\n\n\t\tstatic float Exclusion (float f0, float f1)\n\t\t{\n\t\t\tfloat f;\n\n\t\t\tf = f0 + f1 - 2f * f0 * f1;\n\n\t\t\treturn f;\n\t\t}\n\n\n\t\tpublic static Color Hue (Color c0, Color c1)\n\t\t{\n\t\t\treturn Color.HSVToRGB (RGBToHSV (c0) [0], RGBToHSV (c1) [1], RGBToHSV (c1) [2]);\n\t\t}\n\n\t\tpublic static Color Saturation (Color c0, Color c1)\n\t\t{\n\t\t\treturn Color.HSVToRGB (RGBToHSV (c1) [0], RGBToHSV (c0) [1], RGBToHSV (c1) [2]);\n\t\t}\n\n\t\tpublic static Color Color_ (Color c0, Color c1)\n\t\t{\n\t\t\treturn Color.HSVToRGB (RGBToHSV (c0) [0], RGBToHSV (c0) [1], RGBToHSV (c1) [2]);\n\t\t}\n\n\t\tpublic static Color Luminosity (Color c0, Color c1)\n\t\t{\n\t\t\treturn Color.HSVToRGB (RGBToHSV (c1) [0], RGBToHSV (c1) [1], RGBToHSV (c0) [2]);\n\t\t}\n\n\t\tpublic static float [] RGBToHSV (Color c)\n\t\t{\n\t\t\tfloat [] retVal = new float[3];\n\n\t\t\tColor.RGBToHSV (c, out retVal [0], out retVal [1], out retVal [2]);\n\n\t\t\treturn retVal;\n\t\t}\n\n\n\t\tpublic static Color Add (Color c0, Color c1)\n\t\t{\n\t\t\treturn c0 + c1;\n\t\t}\n\n\t\tpublic static Color Subtract (Color c0, Color c1)\n\t\t{\n\t\t\treturn c0 - c1;\n\t\t} \n\t}" + "\n\n");
			//	}
			//}

			lst.Add (WriteTabs (tabNb) + "public class " + graphName + "_" + stateName + "_" + theLogic.logicName + "_" + logicNodeName + " \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + graphName + "_" + stateName + "_" + theLogic.logicName + 
				" " + StringTreatment.FirstToLower (graphName + "_" + stateName + "_" + theLogic.logicName) + ";\n\n");

			CSharpLogicNodeVariablesToList (theGraph, theState, theLogic, theLogicNode, ref lst, ref tabNb, 
				CsLogicNodeWhere.globalVariables);

			lst.Add (WriteTabs (tabNb) + "public " + graphName + "_" + stateName + "_" + theLogic.logicName + "_" + logicNodeName 
				+ " (" + graphName + "_" + stateName + "_" + theLogic.logicName +
				" set" + graphName + "_" + stateName + "_" + theLogic.logicName + ") \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName + "_" + stateName + "_" + theLogic.logicName) + " = " +
				"set" + graphName + "_" + stateName + "_" + theLogic.logicName + ";\n\n");

			lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName + "_" + stateName + "_" + theLogic.logicName) +
				".IAmHere ();" + "\n\n");


			CSharpLogicNodeVariablesToList (theGraph, theState, theLogic, theLogicNode, ref lst, ref tabNb,
				CsLogicNodeWhere.constructor);

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");



			lst.Add (WriteTabs (tabNb) + "public void LogicNodeUpdate ()\n");

			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			CSharpLogicNodeVariablesToList (theGraph, theState, theLogic, theLogicNode, ref lst, ref tabNb, 
				CsLogicNodeWhere.logicNodeUpdate);

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");


			CSharpLogicNodeVariablesToList (theGraph, theState, theLogic, theLogicNode, ref lst, ref tabNb, 
				CsLogicNodeWhere.methods);
		}

		static void CSharpLogicNodeVariablesToList (Graph theGraph, Node theState, Logic theLogic, LogicNode theLogicNode,
			ref List<string> lst, ref int tabNb, CsLogicNodeWhere csLogicNodeWhere)
		{
			switch (theLogicNode.logicType)
			{
			case LogicType.GetSetOtherGameObjectsScriptsVariables:
				GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;

			//case LogicType.test:
			//	GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
			//		csLogicNodeWhere);
			//	break;

			case LogicType.Ads:
				GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;

			case LogicType.computeOrOperation:
				GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;


			case LogicType.mouseInput:
				GetCodeFromLogicNodeToList (theLogicNode, false, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;

			case LogicType.input:
				GetCodeFromLogicNodeToList (theLogicNode, false, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;

			case LogicType.unityInputClassAndCrossPlatform:
				GetCodeFromLogicNodeToList (theLogicNode, false, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;
	
			case LogicType.timeOperation:
				GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
					csLogicNodeWhere);
				break;

			//case LogicType.UseHighLevelNodes:
			//	GetCodeFromLogicNodeToList (theLogicNode, true, ref lst, ref tabNb, theGraph, theState, theLogic,
			//		csLogicNodeWhere);
			//	break;
			}
		}
		#endregion logic node


		static void GetCodeFromLogicNodeToList (LogicNode theLogicNode, bool needToLinkInputs, 
			ref List <string> lst, ref int tabNb, Graph theGraph, Node theState, Logic theLogic, 
			CsLogicNodeWhere csLogicNodeWhere)
		{
			switch (csLogicNodeWhere)
			{
			case CsLogicNodeWhere.constructor:
				lst.Add (theLogicNode.GetCodeFrom (CsLogicNodeWhere.constructor));

				//if (needToLinkInputs)
				//	LogicNodeUpdateLinkInputs (theLogicNode, theGraph, theState, theLogic, ref tabNb, ref lst);
				break;

			case CsLogicNodeWhere.globalVariables:
				lst.Add (theLogicNode.GetCodeFrom (CsLogicNodeWhere.globalVariables));
				break;

			case CsLogicNodeWhere.logicNodeUpdate:
				if (needToLinkInputs)
					LogicNodeUpdateLinkInputs (theLogicNode, theGraph, theState, theLogic, ref tabNb, ref lst);

				lst.Add (theLogicNode.GetCodeFrom (CsLogicNodeWhere.logicNodeUpdate));
				break;

			case CsLogicNodeWhere.methods:
				lst.Add (theLogicNode.GetCodeFrom (CsLogicNodeWhere.methods));
				break;
			}
		}

		#region link
		static string SourceLogicNodeVarName (Graph theGraph, Node theState, Logic theLogic, 
			LogicNode sourceLogicNode)
		{
			return StringTreatment.FirstToLower (theGraph.graphNameRacine)
				+ "_" + theState.nodeName + "_" + theLogic.logicName
				+ "." 
				+  StringTreatment.FirstToLower (theGraph.graphNameRacine)
				+ "_" + theState.nodeName + "_" + theLogic.logicName
				+ "_" + sourceLogicNode.nodeName;
		}


		static List <int> ActiveInputs (LogicNode theLogicNode)
		{
			List <int> activeInputs = new List<int> ();

			for (int i = 0; i < theLogicNode.activeInputs.Length; i++)
			{
				if (theLogicNode.activeInputs [i])
				{
					activeInputs.Add (i);
				}
			}

			return activeInputs;
		}

		static List <int> Nv_ActiveInputs (LogicNode theLogicNode)
		{
			List <int> nv_activeInputs = new List<int> ();

			for (int i = 0; i < theLogicNode.nv_activeInputs.Length; i++)
			{
				if (theLogicNode.nv_activeInputs [i])
				{
					nv_activeInputs.Add (i);
				}
			}

			return nv_activeInputs;
		}


		static List <int> PublicInputs (LogicNode theLogicNode)
		{
			List <int> publicInputs = new List<int> ();

			for (int i = 0; i < theLogicNode.publicInputs.Length; i++)
			{
				if (theLogicNode.publicInputs [i])
				{
					if (theLogicNode.activeInputsFields [i])
					{
						publicInputs.Add (i);
					}
				}
			}

			return publicInputs;
		}

		static List <int> Nv_PublicInputs (LogicNode theLogicNode)
		{
			List <int> nv_publicInputs = new List<int> ();

			for (int i = 0; i < theLogicNode.nv_publicInputs.Length; i++)
			{
				if (theLogicNode.nv_publicInputs [i])
				{
					if (theLogicNode.nv_activeInputsFields [i])
					{
						nv_publicInputs.Add (i);
					}
				}
			}

			return nv_publicInputs;
		}



		const string projectVariablesClassName = "ProjectVariables";

		static void LogicNodeUpdateLinkInputs_ProjectVariables (LogicNode theLogicNode, Graph theGraph, Node theState,
			Logic theLogic, 
			string inSource, string inSource_permission, string inVarName, ref int tabNb, ref List <string> lst)
		{
			string uID = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource);

			string uID_permission = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource_permission);

			if (string.IsNullOrEmpty (uID))
				return;

			ProjectVariable pvSource = Diamond.projectVariables.ProjectVariableOnUniqueID (uID);

			LogicNode sourceLogicNode_permission = theLogicNode.GetLogicNodeOnUniqueID (uID_permission);

			if (pvSource == null)
				return;

			//int pvSourceIndex = Diamond.projectVariables.ProjectVariableIndexOnUniqueID (uID);

			string sourceNodeOutID_permission = "";
			if (sourceLogicNode_permission != null)
			sourceNodeOutID_permission = sourceLogicNode_permission.InOutAdressCurrentToLinkToInOutID (
				inSource_permission);

			int sourceNodeOutIndex_permission = -1;
			if (sourceLogicNode_permission != null)
			sourceNodeOutIndex_permission = sourceLogicNode_permission.IndexOfOutID (sourceNodeOutID_permission);

			string outVarName = projectVariablesClassName + "." + pvSource.uniqueID;

			string outVarName_permission = "";
			if (sourceLogicNode_permission != null)
			if (sourceNodeOutIndex_permission > -1 && 
				sourceNodeOutIndex_permission < sourceLogicNode_permission.activeOutputs.Length)
			if (  sourceLogicNode_permission.activeOutputs [sourceNodeOutIndex_permission])
			if ( ! string.IsNullOrEmpty (sourceNodeOutID_permission))
			{
				lst.Add ("\t\t" + "// " + pvSource.myName + "\n");
				outVarName_permission = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode_permission,
					sourceNodeOutID_permission);
			}

			int tabNbNow = tabNb;

			if ( ! string.IsNullOrEmpty(outVarName_permission))
			{
				lst.Add (WriteTabs (tabNb) + "if (" + outVarName_permission + ")\n");
				tabNbNow = tabNb + 1;
			}
			if ( ! string.IsNullOrEmpty (outVarName))
				WriteInEqualOut (ref lst, ref tabNbNow, inVarName, outVarName);
			else
				lst.Add (WriteTabs (tabNbNow) + "Debug.LogWarning (" + variableLinkMissing + ");\n");
		}

		static void SendTo_ProjectVariables (LogicNode theLogicNode, ref int tabNb, ref List <string> lst,
			Graph theGraph, Node theState, Logic theLogic)
		{
			for (int i = 0; i < Diamond.projectVariables.projectVariables.Count; i++)
			{
				string uID = theLogicNode.InOutAdressCurrentToLinkToUniqueID (
					Diamond.projectVariables.projectVariables [i].setSource);
				
				string uID_permission = theLogicNode.InOutAdressCurrentToLinkToUniqueID (
					Diamond.projectVariables.projectVariables [i].setPermissionInputSource);
				

				if (string.IsNullOrEmpty (uID))
					continue;

				if (uID != theLogicNode.uniqueID)
					continue;

				LogicNode sourceLogicNode_permission = theLogicNode.GetLogicNodeOnUniqueID (uID_permission);



				string inOutID = theLogicNode.InOutAdressCurrentToLinkToInOutID (
					Diamond.projectVariables.projectVariables [i].setSource);


				string inOutID_permission = theLogicNode.InOutAdressCurrentToLinkToInOutID (
					Diamond.projectVariables.projectVariables [i].setPermissionInputSource);

				int sourceNodeOutIndex_permission = theLogicNode.IndexOfOutID (inOutID_permission);


				if (theLogicNode.IndexOfOutID (inOutID) > -1 && 
					theLogicNode.IndexOfOutID (inOutID) < theLogicNode.activeOutputs.Length)
				if ( ! theLogicNode.activeOutputs [theLogicNode.IndexOfOutID (inOutID)])
					continue;

				string inVarName = projectVariablesClassName + "." +
					Diamond.projectVariables.projectVariables [i].uniqueID;


				string outVarName = InOutIdToVarName (inOutID, false);

				string outVarName_permission = InOutIdToVarName (inOutID_permission, false);

				if (sourceLogicNode_permission != null)
				if (sourceNodeOutIndex_permission > -1 && 
					sourceNodeOutIndex_permission < sourceLogicNode_permission.activeOutputs.Length)
				if (  sourceLogicNode_permission.activeOutputs [sourceNodeOutIndex_permission])
				if (! string.IsNullOrEmpty (inOutID_permission))
					outVarName_permission = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode_permission,
						inOutID_permission);



				int tabNbNow = tabNb;

				lst.Add ("\t\t" + "// " + Diamond.projectVariables.projectVariables [i].myName + "\n");
				if ( ! string.IsNullOrEmpty (outVarName_permission))
				{
					lst.Add (WriteTabs (tabNb) + "if (" + outVarName_permission + ")\n");
					tabNbNow = tabNb + 1;
				}
				if ( ! string.IsNullOrEmpty(outVarName))
				{					
					WriteInEqualOut (ref lst, ref tabNbNow, inVarName, outVarName);
				}
				else
				{
					lst.Add (WriteTabs (tabNbNow) + "Debug.LogWarning (" + variableLinkMissing + ");\n");
				}
			}
		}

		static bool HasFoundMonoAndMonoField (LogicNode theLogicNode)
		{
			if (theLogicNode.logicType != LogicType.GetSetOtherGameObjectsScriptsVariables)
				return true;

			if (theLogicNode.FindMonoByName () == null)
				return false;

			if (theLogicNode.FindMonoFieldByName () == null)
				return false;

			return true;
		}

		static void LogicNodeUpdateLinkInputs (LogicNode theLogicNode, Graph theGraph, Node theState,
			Logic theLogic, ref int tabNb, ref List <string> lst)
		{
			SendTo_ProjectVariables (theLogicNode, ref tabNb, ref lst, theGraph, theState, theLogic);


			List <int> publicInputs = PublicInputs (theLogicNode);

			for (int i = 0; i < publicInputs.Count; i++)
			{
				string inID = theLogicNode.inputsIDs [publicInputs [i]];

				string inVarName = InOutIdToVarName (inID, false);

				string outVarName = ToPublicVariableInMonoBehaviour (theGraph, theState, theLogic, theLogicNode,
					inID);

				if (theLogicNode.inputsTypes [publicInputs [i]] 
					== VariableType.GameObject)
					if (theLogicNode.attachedToGameObject [publicInputs [i]])
						outVarName = FromLogicNodeToMonoBehaviour (theGraph, theState, theLogic) + 
						"." + "attachedToGameObject";

				if (HasFoundMonoAndMonoField (theLogicNode))
					WriteInEqualOut (ref lst, ref tabNb, inVarName, outVarName);
			}


			List <int> nv_publicInputs = Nv_PublicInputs (theLogicNode);

			for (int i = 0; i < nv_publicInputs.Count; i++)
			{
				string nv_inID = theLogicNode.nv_inputsIDs [nv_publicInputs [i]];

				VariableType nv_type = theLogicNode.nv_inputsTypes [nv_publicInputs [i]];

				string inVarName = Nv_InOutIdToVarName (nv_inID, nv_type, theLogicNode);

				string outVarName = ToPublicVariableInMonoBehaviour (theGraph, theState, theLogic, theLogicNode,
					nv_inID);

				if (HasFoundMonoAndMonoField (theLogicNode))
					WriteInEqualOut (ref lst, ref tabNb, inVarName, outVarName);
			}




			List <int> activeInputs = ActiveInputs (theLogicNode);

			for (int i = 0; i < activeInputs.Count; i++)
			{
				string inSource = theLogicNode.inputsSources [activeInputs [i]];

				string inSource_permission = theLogicNode.inputsSources_forPermition [activeInputs [i]];

				if (string.IsNullOrEmpty (inSource))
				{
					continue;
				}

				string inID = theLogicNode.inputsIDs [activeInputs [i]];

				string inVarName = InOutIdToVarName (inID, false);


				if (inSource.Length > 0)
				if (inSource [0] == 'v')
				{
					LogicNodeUpdateLinkInputs_ProjectVariables (theLogicNode, theGraph, theState, theLogic,
						inSource, inSource_permission, inVarName, ref tabNb, ref lst);

					continue;
				}


				string uID = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource);

				string uID_permission = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource_permission);


				if (string.IsNullOrEmpty (uID))
				{
					continue;
				}

				LogicNode sourceLogicNode = theLogicNode.GetLogicNodeOnUniqueID (uID);

				LogicNode sourceLogicNode_permission = theLogicNode.GetLogicNodeOnUniqueID (uID_permission);

				if (sourceLogicNode == null)
				{
					continue;
				}

				string sourceNodeOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (inSource);

				string sourceNodeOutID_permission = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (
					inSource_permission);
			
				int sourceNodeOutIndex = sourceLogicNode.IndexOfOutID (sourceNodeOutID);

				int sourceNodeOutIndex_permission = sourceLogicNode.IndexOfOutID (sourceNodeOutID_permission);

				if (sourceNodeOutIndex > -1 && sourceNodeOutIndex < sourceLogicNode.activeOutputs.Length)
				if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
				{								
					continue;
				}

				//string sourceLogicNodeVarName = SourceLogicNodeVarName (theGraph, theState, theLogic, sourceLogicNode);

				string outVarName = "";

				if ((sourceLogicNode != null) && ( ! string.IsNullOrEmpty (sourceNodeOutID)))
				outVarName = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode,
					sourceNodeOutID);

				string outVarName_permission = "";

				if (sourceLogicNode_permission != null)
				if (sourceNodeOutIndex_permission > -1 && 
					sourceNodeOutIndex_permission < sourceLogicNode_permission.activeOutputs.Length)
				if (  sourceLogicNode_permission.activeOutputs [sourceNodeOutIndex_permission])
				if ( ! string.IsNullOrEmpty (sourceNodeOutID_permission))
					outVarName_permission = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode_permission,
						sourceNodeOutID_permission);


				bool permissionNotConnectedButFalse = false;

				int tabNbNow = tabNb;
				if ( ! string.IsNullOrEmpty(outVarName_permission))
				{
					lst.Add (WriteTabs (tabNb) + "if (" + outVarName_permission + ")\n");
					tabNbNow = tabNb + 1;
				}
				else if ( ! theLogicNode.permittedInputs [activeInputs [i]])
				{
					permissionNotConnectedButFalse = true;
				}


				if (theLogicNode.activeInputsFields [activeInputs [i]])
				{
					if ( ( ! string.IsNullOrEmpty(inVarName)) && ( ! string.IsNullOrEmpty(outVarName)))
					{
						if ( ! permissionNotConnectedButFalse)
						if (HasFoundMonoAndMonoField (theLogicNode))
						if (HasFoundMonoAndMonoField (sourceLogicNode))
							WriteInEqualOut (ref lst, ref tabNbNow, inVarName, outVarName);
					}
					else
					{
						lst.Add (WriteTabs (tabNbNow) + "Debug.LogWarning (" + variableLinkMissing + ");\n");
					}
				}
			}	


			List <int> nv_activeInputs = Nv_ActiveInputs (theLogicNode);

			for (int i = 0; i < nv_activeInputs.Count; i++)
			{
				string inSource = theLogicNode.nv_inputsSources [nv_activeInputs [i]];

				string inSource_permission = theLogicNode.nv_inputsSources_forPermition [nv_activeInputs [i]];

				if (string.IsNullOrEmpty (inSource))
				{
					continue;
				}

				string inID = theLogicNode.nv_inputsIDs [nv_activeInputs [i]];

				VariableType nv_type = theLogicNode.nv_inputsTypes [nv_activeInputs [i]];

				string inVarName = Nv_InOutIdToVarName (inID, nv_type, theLogicNode);


				string uID = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource);

				string uID_permission = theLogicNode.InOutAdressCurrentToLinkToUniqueID (inSource_permission);


				if (string.IsNullOrEmpty (uID))
				{
					continue;
				}

				LogicNode sourceLogicNode = theLogicNode.GetLogicNodeOnUniqueID (uID);

				LogicNode sourceLogicNode_permission = theLogicNode.GetLogicNodeOnUniqueID (uID_permission);

				if (sourceLogicNode == null)
				{
					continue;
				}

				string sourceNodeOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (inSource);

				string sourceNodeOutID_permission = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (
					inSource_permission);

				int sourceNodeOutIndex = sourceLogicNode.IndexOfOutID (sourceNodeOutID);

				int sourceNodeOutIndex_permission = sourceLogicNode.IndexOfOutID (sourceNodeOutID_permission);

				if (sourceNodeOutIndex > -1 && sourceNodeOutIndex < sourceLogicNode.activeOutputs.Length)
				if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
				{								
					continue;
				}

				//string sourceLogicNodeVarName = SourceLogicNodeVarName (theGraph, theState, theLogic, sourceLogicNode);

				string outVarName = "";

				if ((sourceLogicNode != null) && ( ! string.IsNullOrEmpty (sourceNodeOutID)))
					outVarName = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode,
						sourceNodeOutID);

				string outVarName_permission = "";

				if (sourceLogicNode_permission != null)
				if (sourceNodeOutIndex_permission > -1 && 
					sourceNodeOutIndex_permission < sourceLogicNode_permission.activeOutputs.Length)
				if (  sourceLogicNode_permission.activeOutputs [sourceNodeOutIndex_permission])
				if ( ! string.IsNullOrEmpty (sourceNodeOutID_permission))
					outVarName_permission = ToSourceOutput (theGraph, theState, theLogic, sourceLogicNode_permission,
						sourceNodeOutID_permission);


				bool nv_permissionNotConnectedButFalse = false;

				int tabNbNow = tabNb;
				if ( ! string.IsNullOrEmpty(outVarName_permission))
				{
					lst.Add (WriteTabs (tabNb) + "if (" + outVarName_permission + ")\n");
					tabNbNow = tabNb + 1;
				}
				else if ( ! theLogicNode.nv_permittedInputs [nv_activeInputs [i]])
				{
					nv_permissionNotConnectedButFalse = true;
				}

				if (theLogicNode.nv_activeInputsFields [nv_activeInputs [i]])
				{
					if ( ( ! string.IsNullOrEmpty(inVarName)) && ( ! string.IsNullOrEmpty(outVarName)))
					{
						if ( ! nv_permissionNotConnectedButFalse)
						if (HasFoundMonoAndMonoField (theLogicNode))
						if (HasFoundMonoAndMonoField (sourceLogicNode))
							WriteInEqualOut (ref lst, ref tabNbNow, inVarName, outVarName);
					}
					else
					{
						lst.Add (WriteTabs (tabNbNow) + "Debug.LogWarning (" + variableLinkMissing + ");\n");
					}
				}
			}	





			if (theLogicNode.alwaysDoIt)
			{
				for (int i = 0; i < theLogicNode.activeInputsFields.Length; i++)
				{
					if ( ! theLogicNode.activeInputsFields [i])
						continue;
					
					if (theLogicNode.inputsIDs [i] == Enums.doIt_ID)
					{
						lst.Add (WriteTabs (tabNb) + InOutIdToVarName (Enums.doIt_ID, false) + " = true;\n\n");

						break;
					}
				}
			}
		}

		const string variableLinkMissing = "\"Variable link is missing\"";

		static void WriteInEqualOut (ref List <string> lst, ref int tabNb, string inVarName, string outVarName)
		{
			if (string.IsNullOrEmpty (inVarName) || string.IsNullOrEmpty (outVarName))
				return;

			lst.Add (WriteTabs (tabNb) + inVarName + " = " + outVarName + ";\n\n");
		}

		static string ToSourceOutput (Graph theGraph, Node theState, Logic theLogic,
			LogicNode sourceLogicNode, string sourceNodeOutID)
		{			
			return SourceLogicNodeVarName (theGraph, theState, theLogic, sourceLogicNode) 
				+ "." + InOutIdToVarName (sourceNodeOutID, false);
		}

		static string ToPublicVariableInMonoBehaviour (Graph theGraph, Node theState, Logic theLogic,
			LogicNode theLogicNode, string inID)
		{
			return FromLogicNodeToMonoBehaviour (theGraph, theState, theLogic) + "." 
				+ MonoBehaviourPublicVariableName (theState, theLogic, theLogicNode, inID); 
		}

		public static string FromLogicNodeToMonoBehaviour (Graph theGraph, Node theState, Logic theLogic)
		{
			return FromLogicNodeToLogic (theGraph,theState, theLogic) + "."
				+ FromLogicToState (theGraph, theState) + "." 
				+ FromStateToGraph (theGraph);
		}

		static string FromMonoBehaviourToLogicNode (Graph theGraph, Node theState, Logic theLogic, 
			LogicNode theLogicNode)
		{
			return StringTreatment.FirstToLower (theState.nodeName) + "."

				+ StringTreatment.FirstToLower (theGraph.graphNameRacine) + "_"
				+ theState.nodeName + "_" + theLogic.logicName + "."

				+ StringTreatment.FirstToLower (theGraph.graphNameRacine) + "_"
				+ theState.nodeName + "_" + theLogic.logicName + "_"
				+ theLogicNode.nodeName;
		}

		static string FromMonoBehaviourToLogicNode (LogicNode theLogicNode)
		{
			return StringTreatment.FirstToLower (theLogicNode.logic.node.nodeName) + "."

				+ StringTreatment.FirstToLower (theLogicNode.logic.node.graph.graphNameRacine) + "_"
				+ theLogicNode.logic.node.nodeName + "_" + theLogicNode.logic.logicName + "."

				+ StringTreatment.FirstToLower (theLogicNode.logic.node.graph.graphNameRacine) + "_"
				+ theLogicNode.logic.node.nodeName + "_" + theLogicNode.logic.logicName + "_"
				+ theLogicNode.nodeName;
		}

		static string FromLogicNodeToLogic (Graph theGraph, Node theState, Logic theLogic)
		{
			return StringTreatment.FirstToLower (theGraph.graphNameRacine) + "_"
				+ theState.nodeName + "_" + theLogic.logicName;
		}

		static string FromLogicToState (Graph theGraph, Node theState)
		{
			return StringTreatment.FirstToLower (theGraph.graphNameRacine) + "_"
				+ theState.nodeName;
		}

		static string FromStateToGraph (Graph theGraph)
		{
			return StringTreatment.FirstToLower (theGraph.graphNameRacine);
		}

		public static string InOutIdOfObjectToDeclarationString (string s)
		{
			string r = "";

			switch (s)
			{
			case Enums.audioClip_LocalID:
				r = "AudioClip";
				break;

			case Enums.gameObjectsList_ID:
				r = "GameObject";
				break;

			case Enums.gameObjectValues_0_ID:
				r = "GameObject";
				break;

			case Enums.gameObjectValues_1_ID:
				r = "GameObject";
				break;

			case Enums.gameObjectValue_ID:
				r = "GameObject";
				break;

			case Enums.hit2D_gameObject_ID:
				r = "GameObject";
				break;



			case Enums.materialList_ID:
				r = "Material";
				break;

			case Enums.materialValues_0_ID:
				r = "Material";
				break;

			case Enums.materialValues_1_ID:
				r = "Material";
				break;

			case Enums.materialValue_ID:
				r = "Material";
				break;



			case Enums.raycastHitGameObject_ID:
				r = "GameObject";
				break;


			case Enums.shaderList_ID:
				r = "Shader";
				break;

			case Enums.shaderValues_0_ID:
				r = "Shader";
				break;

			case Enums.shaderValues_1_ID:
				r = "Shader";
				break;

			case Enums.shaderValue_ID:
				r = "Shader";
				break;


			case Enums.texture2DList_ID:
				r = "Texture2D";
				break;

			case Enums.texture2DValues_0_ID:
				r = "Texture2D";
				break;

			case Enums.texture2DValues_1_ID:
				r = "Texture2D";
				break;

			case Enums.texture2DValue_ID:
				r = "Texture2D";
				break;
			}

			return r;
		}

		public static string InOutIdToVarName (string s, bool listAdd)
		{
			string r = "";

			string retValBefore_ = StringTreatment.BeforeThat (s, '_');
			if (StringTreatment.IsEndWith_List (retValBefore_))
			{
				switch (retValBefore_)
				{
				case Enums.boolsList_ID:
					r = "boolsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "boolsListValue";
					}
					break;

				case Enums.colorsList_ID:
					r = "colorsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "colorsListValue";
					}
					break;

				case Enums.intsList_ID:
					r = "intsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "intsListValue";
					}
					break;

				case Enums.floatsList_ID:
					r = "floatsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "floatsListValue";
					}
					break;

				case Enums.vector2List_ID:
					r = "vector2ListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "vector2ListValue";
					}
					break;

				case Enums.vector3List_ID:
					r = "vector3ListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "vector3ListValue";
					}
					break;

				case Enums.vector4List_ID:
					r = "vector4ListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "vector4ListValue";
					}
					break;

				case Enums.stringsList_ID:
					r = "stringsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "stringsListValue";
					}
					break;

				case Enums.gameObjectsList_ID:
					r = "gameObjectsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "gameObjectsListValue";
					}
					break;

				case Enums.materialList_ID:
					r = "materialsListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "materialsListValue";
					}
					break;

				case Enums.texture2DList_ID:
					r = "texture2DListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "texture2DListValue";
					}
					break;

				case Enums.shaderList_ID:
					r = "shaderListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "shaderListValue";
					}
					break;

				case Enums.rectList_ID:
					r = "rectListValue [" + LogicNode.OutIDToListIndex (s).ToString () + "]";

					if (listAdd)
					{
						r = "rectListValue";
					}
					break;
				}
			}

			switch (s)
			{
			case Enums.audioClip_LocalID:
				r = "audioClipValue_Local";
				break;
				
			case Enums.hit2D_centroid_ID:
				r = "hit2D_centroid";
				break;

			case Enums.hit2D_distance_ID:
				r = "hit2D_distance";
				break;

			case Enums.hit2D_fraction_ID:
				r = "hit2D_fraction";
				break;

			case Enums.hit2D_gameObject_ID:
				r = "hit2D_gameObject";
				break;

			case Enums.hit2D_normal_ID:
				r = "hit2D_normal";
				break;



			case Enums.hit2D_point_ID:
				r = "hit2D_point";
				break;

			case Enums.boolValue_ID:
				r = "boolValue";
				break;

			case Enums.boolValues_0_ID:
				r = "boolValues [0]";
				break;

			case Enums.boolValues_1_ID:
				r = "boolValues [1]";
				break;

			case Enums.boolsListEntire_ID:
				r = "boolsListValue";
				break;



			case Enums.boolsListEntire0_ID:
				r = "boolsListValues [0]";
				break;

			case Enums.boolsListEntire1_ID:
				r = "boolsListValues [1]";
				break;

			case Enums.colorValue_ID:
				r = "colorValue";
				break;

			case Enums.colorValues_0_ID:
				r = "colorValues [0]";
				break;

			case Enums.colorValues_1_ID:
				r = "colorValues [1]";
				break;




			case Enums.colorsListEntire_ID:
				r = "colorsListValue";
				break;

			case Enums.colorsListEntire0_ID:
				r = "colorsListValues [0]";
				break;

			case Enums.colorsListEntire1_ID:
				r = "colorsListValues [1]";
				break;

			case Enums.intValue_ID:
				r = "intValue";
				break;

			case Enums.intValues_0_ID:
				r = "intValues [0]";
				break;



			case Enums.intValues_1_ID:
				r = "intValues [1]";
				break;

			case Enums.intValues_2_ID:
				r = "intValues [2]";
				break;

			case Enums.intsListEntire_ID:
				r = "intsListValue";
				break;

			case Enums.intsListEntire0_ID:
				r = "intsListValues [0]";
				break;

			case Enums.intsListEntire1_ID:
				r = "intsListValues [1]";
				break;




			case Enums.floatValue_ID:
				r = "floatValue";
				break;

			case Enums.floatValues_0_ID:
				r = "floatValues [0]";
				break;

			case Enums.floatValues_1_ID:
				r = "floatValues [1]";
				break;

			case Enums.floatValues_2_ID:
				r = "floatValues [2]";
				break;

			case Enums.floatsListEntire_ID:
				r = "floatsListValue";
				break;



			case Enums.floatsListEntire0_ID:
				r = "floatsListValues [0]";
				break;

			case Enums.floatsListEntire1_ID:
				r = "floatsListValues [1]";
				break;

			case Enums.vector2Value_ID:
				r = "vector2Value";
				break;

			case Enums.vector2Values_0_ID:
				r = "vector2Values [0]";
				break;

			case Enums.vector2Values_1_ID:
				r = "vector2Values [1]";
				break;



			case Enums.vector2ListEntire_ID:
				r = "vector2ListValue";
				break;

			case Enums.vector2ListEntire0_ID:
				r = "vector2ListValues [0]";
				break;

			case Enums.vector2ListEntire1_ID:
				r = "vector2ListValues [1]";
				break;

			case Enums.vector3Value_ID:
				r = "vector3Value";
				break;

			case Enums.vector3Values_0_ID:
				r = "vector3Values [0]";
				break;



			case Enums.vector3Values_1_ID:
				r = "vector3Values [1]";
				break;

			case Enums.vector3ListEntire_ID:
				r = "vector3ListValue";
				break;

			case Enums.vector3ListEntire0_ID:
				r = "vector3ListValues [0]";
				break;

			case Enums.vector3ListEntire1_ID:
				r = "vector3ListValues [1]";
				break;

			case Enums.vector4Value_ID:
				r = "vector4Value";
				break;



			case Enums.vector4Values_0_ID:
				r = "vector4Values [0]";
				break;

			case Enums.vector4Values_1_ID:
				r = "vector4Values [1]";
				break;


			case Enums.vector4ListEntire_ID:
				r = "vector4ListValue";
				break;

			case Enums.vector4ListEntire0_ID:
				r = "vector4ListValues [0]";
				break;

			case Enums.vector4ListEntire1_ID:
				r = "vector4ListValues [1]";
				break;



			case Enums.rayDirection_ID:
				r = "rayDirectionValueNotNormalized";
				break;

			case Enums.rayOrigin_ID:
				r = "rayValueOrigin";
				break;

			case Enums.ray2DDirection_ID:
				r = "ray2DDirectionValueNotNormalized";
				break;

			case Enums.ray2DOrigin_ID:
				r = "ray2DValueOrigin";
				break;

			case Enums.stringValue_ID:
				r = "stringValue";
				break;



			case Enums.stringValues_0_ID:
				r = "stringValues [0]";
				break;

			case Enums.stringValues_1_ID:
				r = "stringValues [1]";
				break;

			case Enums.stringsListEntire_ID:
				r = "stringsListValue";
				break;

			case Enums.stringsListEntire0_ID:
				r = "stringsListValues [0]";
				break;

			case Enums.stringsListEntire1_ID:
				r = "stringsListValues [1]";
				break;




			case Enums.gameObjectValue_ID:
				r = "gameObjectValue";
				break;

			case Enums.gameObjectValues_0_ID:
				r = "gameObjectValues [0]";
				break;

			case Enums.gameObjectValues_1_ID:
				r = "gameObjectValues [1]";
				break;

			case Enums.gameObjectsListEntire_ID:
				r = "gameObjectsListValue";
				break;

			case Enums.gameObjectsListEntire0_ID:
				r = "gameObjectsListValues [0]";
				break;



			case Enums.gameObjectsListEntire1_ID:
				r = "gameObjectsListValues [1]";
				break;

			case Enums.doIt_ID:
				r = "doIT";
				break;

			case Enums.boundsCenterValue_ID:
				r = "boundsCenterValue";
				break;

			case Enums.boundsExtentsValue_ID:
				r = "boundsExtentsValue";
				break;

			case Enums.boundsMaxValue_ID:
				r = "boundsMaxValue";
				break;



			case Enums.boundsMinValue_ID:
				r = "boundsMinValue";
				break;

			case Enums.boundsSizeValue_ID:
				r = "boundsSizeValue";
				break;

			case Enums.raycastHitBarycentricCoordinate_ID:
				r = "raycastHitBarycentricCoordinate";
				break;

			case Enums.raycastHitTriangleIndex_ID:
				r = "raycastHitTriangleIndex";
				break;

			case Enums.raycastHitPoint_ID:
				r = "raycastHitPoint";
				break;



			case Enums.raycastHitNormal_ID:
				r = "raycastHitNormal";
				break;

			case Enums.raycastHitDistance_ID:
				r = "raycastHitDistance";
				break;

			case Enums.raycastHitGameObject_ID:
				r = "raycastHitGameObject";
				break;

			case Enums.raycastHitLightmapCoord_ID:
				r = "raycastHitLightmapCoord";
				break;

			case Enums.raycastHitTextureCoord_ID:
				r = "raycastHittextureCoord";
				break;



			case Enums.raycastHitTextureCoord2_ID:
				r = "raycastHittextureCoord2";
				break;

			case Enums.m44Value_entier_ID:
				r = "m44Value_entier";
				break;

			case Enums.m44Value_Input_0_entier_ID:
				r = "m44Value_Input_entier [0]";
				break;

			case Enums.m44Value_Input_1_entier_ID:
				r = "m44Value_Input_entier [1]";
				break;

			case Enums.m44Value_0_ID:
				r = "m44Value [0]";
				break;



			case Enums.m44Value_1_ID:
				r = "m44Value [1]";
				break;

			case Enums.m44Value_2_ID:
				r = "m44Value [2]";
				break;

			case Enums.m44Value_3_ID:
				r = "m44Value [3]";
				break;

			case Enums.m44Value_4_ID:
				r = "m44Value [4]";
				break;

			case Enums.m44Value_5_ID:
				r = "m44Value [5]";
				break;



			case Enums.m44Value_6_ID:
				r = "m44Value [6]";
				break;

			case Enums.m44Value_7_ID:
				r = "m44Value [7]";
				break;

			case Enums.m44Value_8_ID:
				r = "m44Value [8]";
				break;

			case Enums.m44Value_9_ID:
				r = "m44Value [9]";
				break;

			case Enums.m44Value_10_ID:
				r = "m44Value [10]";
				break;




			case Enums.m44Value_11_ID:
				r = "m44Value [11]";
				break;

			case Enums.m44Value_12_ID:
				r = "m44Value [12]";
				break;

			case Enums.m44Value_13_ID:
				r = "m44Value [13]";
				break;

			case Enums.m44Value_14_ID:
				r = "m44Value [14]";
				break;

			case Enums.m44Value_15_ID:
				r = "m44Value [15]";
				break;



			case Enums.m44ValueDeterminant_ID:
				r = "m44ValueDeterminant";
				break;

			case Enums.m44ValueIsIdentity_ID:
				r = "m44ValueIsIdentity";
				break;

			case Enums.m44ValueInvertible_ID:
				r = "m44ValueInvertible";
				break;

			case Enums.materialValue_ID:
				r = "materialValue";
				break;

			case Enums.materialValues_0_ID:
				r = "materialValues [0]";
				break;



			case Enums.materialValues_1_ID:
				r = "materialValues [1]";
				break;

			case Enums.materialListEntire_ID:
				r = "materialsListValue";
				break;

			case Enums.materialListEntire0_ID:
				r = "materialsListValues [0]";
				break;

			case Enums.materialListEntire1_ID:
				r = "materialsListValues [1]";
				break;

			case Enums.texture2DValue_ID:
				r = "texture2DValue";
				break;




			case Enums.texture2DValues_0_ID:
				r = "texture2DValues [0]";
				break;

			case Enums.texture2DValues_1_ID:
				r = "texture2DValues [1]";
				break;

			case Enums.texture2DListEntire_ID:
				r = "texture2DListValue";
				break;

			case Enums.texture2DListEntire0_ID:
				r = "texture2DListValues [0]";
				break;

			case Enums.texture2DListEntire1_ID:
				r = "texture2DListValues [1]";
				break;




			case Enums.shaderValue_ID:
				r = "shaderValue";
				break;

			case Enums.shaderValues_0_ID:
				r = "shaderValues [0]";
				break;

			case Enums.shaderValues_1_ID:
				r = "shaderValues [1]";
				break;

			case Enums.shaderListEntire_ID:
				r = "shaderListValue";
				break;

			case Enums.shaderListEntire0_ID:
				r = "shaderListValues [0]";
				break;



			case Enums.shaderListEntire1_ID:
				r = "shaderListValues [1]";
				break;

			case Enums.OffMeshLinkData_activated_ID:
				r = "OffMeshLinkData_activated";
				break;

			case Enums.OffMeshLinkData_startPosition_ID:
				r = "OffMeshLinkData_startPosition";
				break;

			case Enums.OffMeshLinkData_endPosition_ID:
				r = "OffMeshLinkData_endPosition";
				break;

			case Enums.OffMeshLinkData_valid_ID:
				r = "OffMeshLinkData_valid";
				break;




			case Enums.NavMeshHit_distance_ID:
				r = "NavMeshHit_distance";
				break;

			case Enums.NavMeshHit_hit_ID:
				r = "NavMeshHit_hit";
				break;

			case Enums.NavMeshHit_mask_ID:
				r = "NavMeshHit_mask";
				break;

			case Enums.NavMeshHit_normal_ID:
				r = "NavMeshHit_normal";
				break;

			case Enums.NavMeshHit_position_ID:
				r = "NavMeshHit_position";
				break;



				//
			//case Enums.meshValue_ID:
			//	r = "meshValue";
			//	break;
			//
			//case Enums.meshValues_0_ID:
			//	r = "meshValues [0]";
			//	break;
			//
			//case Enums.meshValues_1_ID:
			//	r = "meshValues [1]";
			//	break;


			case Enums.rectValue_ID:
				r = "rectValue";
				break;

			case Enums.rectValues_0_ID:
				r = "rectValues [0]";
				break;

			case Enums.rectValues_1_ID:
				r = "rectValues [1]";
				break;

			case Enums.rectListEntire_ID:
				r = "rectListValue";
				break;

			case Enums.rectListEntire0_ID:
				r = "rectListValues [0]";
				break;

			case Enums.rectListEntire1_ID:
				r = "rectListValues [1]";
				break;




			case Enums.touch_altitudeAngle_ID:
				r = "touch_altitudeAngle";
				break;

			case Enums.touch_azimuthAngle_ID:
				r = "touch_azimuthAngle";
				break;

			case Enums.touch_deltaTime_ID:
				r = "touch_deltaTime";
				break;


			case Enums.touch_maximumPossiblePressure_ID:
				r = "touch_maximumPossiblePressure";
				break;

			case Enums.touch_pressure_ID:
				r = "touch_pressure";
				break;

			case Enums.touch_radius_ID:
				r = "touch_radius";
				break;


			case Enums.touch_radiusVariance_ID:
				r = "touch_radiusVariance";
				break;

			case Enums.touch_fingerId_ID:
				r = "touch_fingerId";
				break;

			case Enums.touch_tapCount_ID:
				r = "touch_tapCount";
				break;


			case Enums.touch_phase_ID:
				r = "touch_phase";
				break;

			case Enums.touch_type_ID:
				r = "touch_type";
				break;


			case Enums.touch_position_ID:
				r = "touch_position";
				break;

			case Enums.touch_rawPosition_ID:
				r = "touch_rawPosition";
				break;

			case Enums.touch_deltaPosition_ID:
				r = "touch_deltaPosition";
				break;
			}

			return r;
		}


		static string Nv_InOutIdToVarName_bool (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Bool_IDs.Length; i++)
			{
				if (theLogicNode.nv_Bool_IDs [i] == id)
				{
					r = "nv_Bool [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_color (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Color_IDs.Length; i++)
			{
				if (theLogicNode.nv_Color_IDs [i] == id)
				{
					r = "nv_Color [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_float (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_float_IDs.Length; i++)
			{
				if (theLogicNode.nv_float_IDs [i] == id)
				{
					r = "nv_float [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_int (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_int_IDs.Length; i++)
			{
				if (theLogicNode.nv_int_IDs [i] == id)
				{
					r = "nv_int [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_material (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Material_IDs.Length; i++)
			{
				if (theLogicNode.nv_Material_IDs [i] == id)
				{
					r = "nv_Material [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_texture2D (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Texture2D_IDs.Length; i++)
			{
				if (theLogicNode.nv_Texture2D_IDs [i] == id)
				{
					r = "nv_Texture2D [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_shader (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Shader_IDs.Length; i++)
			{
				if (theLogicNode.nv_Shader_IDs [i] == id)
				{
					r = "nv_Shader [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_vector2 (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Vector2_IDs.Length; i++)
			{
				if (theLogicNode.nv_Vector2_IDs [i] == id)
				{
					r = "nv_Vector2 [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_vector3 (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Vector3_IDs.Length; i++)
			{
				if (theLogicNode.nv_Vector3_IDs [i] == id)
				{
					r = "nv_Vector3 [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_vector4 (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Vector4_IDs.Length; i++)
			{
				if (theLogicNode.nv_Vector4_IDs [i] == id)
				{
					r = "nv_Vector4 [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_rect (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_Rect_IDs.Length; i++)
			{
				if (theLogicNode.nv_Rect_IDs [i] == id)
				{
					r = "nv_Rect [" + i + "]";
				}
			}

			return r;
		}
		static string Nv_InOutIdToVarName_string (string id, LogicNode theLogicNode)
		{
			string r = "";

			for (int i = 0; i < theLogicNode.nv_String_IDs.Length; i++)
			{
				if (theLogicNode.nv_String_IDs [i] == id)
				{
					r = "nv_String [" + i + "]";
				}
			}

			return r;
		}

		public static string Nv_InOutIdToVarName (string id, VariableType varType, LogicNode theLogicNode)
		{
			string r = "";

			switch (varType)
			{
			case VariableType.Bool://1
				r = Nv_InOutIdToVarName_bool (id, theLogicNode);
				break;

			case VariableType.Color://2
				r = Nv_InOutIdToVarName_color (id, theLogicNode);
				break;

			case VariableType.Float://3
				r = Nv_InOutIdToVarName_float (id, theLogicNode);
				break;

			case VariableType.Int://4
				r = Nv_InOutIdToVarName_int (id, theLogicNode);
				break;

			case VariableType.Material://5
				r = Nv_InOutIdToVarName_material (id, theLogicNode);
				break;

			case VariableType.rect://6
				r = Nv_InOutIdToVarName_rect (id, theLogicNode);
				break;

			case VariableType.Shader://7
				r = Nv_InOutIdToVarName_shader (id, theLogicNode);
				break;

			case VariableType.String://8
				r = Nv_InOutIdToVarName_string (id, theLogicNode);
				break;

			case VariableType.Texture2D://9
				r = Nv_InOutIdToVarName_texture2D (id, theLogicNode);
				break;

			case VariableType.Vector2://10
				r = Nv_InOutIdToVarName_vector2 (id, theLogicNode);
				break;

			case VariableType.Vector3://11
				r = Nv_InOutIdToVarName_vector3 (id, theLogicNode);
				break;

			case VariableType.Vector4://12
				r = Nv_InOutIdToVarName_vector4 (id, theLogicNode);
				break;
			}

	
			return r;
		}
		#endregion link

		#region project variables

		#endregion project variables

		#region MonoBehaviour
		static void CSharpMonoBehaviourFirstBlocToList (string scriptName, ref List<string> lst, ref int tabNb,
			string[] usingNames)
		{			
			lst = new List<string> ();

			for (int i = 0; i < usingNames.Length; i++)
			{
				lst.Add (usingNames [i]);
			}

			lst.Add ("\n");

			lst.Add ("namespace ScriptsCreatedByDiamond \n");
			lst.Add ("{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + "public class " + scriptName + " : MonoBehaviour \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;
		}

		static void CSharpMonoBehaviourMethodsToList (ref List<string> lst, ref int tabNb, string[] statesNames, 
			string scriptName, Graph theGraph)
		{
			lst.Add (WriteTabs (tabNb) + "[HideInInspector] \n");
			lst.Add (WriteTabs (tabNb) + "public I" + scriptName + " currentState; \n\n");

			for (int i = 0; i < statesNames.Length; i++)
			{
				lst.Add (WriteTabs (tabNb) + "[HideInInspector] \n");
				lst.Add (WriteTabs (tabNb) + "public " + scriptName + "_" + statesNames [i] + " " + StringTreatment.FirstToLower (statesNames [i]) + "; \n\n");
			}

			CSharpMonoBehaviourLogicNodesPublicVariablesToList (theGraph, ref lst, ref tabNb);


			lst.Add (WriteTabs (tabNb) + "[HideInInspector]" + "\n");
			lst.Add (WriteTabs (tabNb) + "public GameObject attachedToGameObject;" + "\n\n");



			//lst.Add ("\t\tList<string> tagsForGameObjectsTriggers = new List<string> ();" + "\n\n");

			lst.Add (WriteTabs (tabNb) + "[HideInInspector]" + "\n");
			lst.Add ("\t\tpublic GameObject gameObjectsFoundBytrigger = null;" + "\n\n");

			lst.Add (WriteTabs (tabNb) + "[HideInInspector]" + "\n");
			lst.Add ("\t\tpublic int gameObjectsFoundBytriggerIndex = -1;" + "\n\n");


			lst.Add (WriteTabs (tabNb) + "\n");

			lst.Add (WriteTabs (tabNb) + "void Awake () \n");

			lst.Add (WriteTabs (tabNb) + "{ \n");
			lst.Add ("\t\t\tProjectVariables.Init ();\n\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + "attachedToGameObject = gameObject;" + "\n");

			for (int i = 0; i < statesNames.Length; i++)
			{
				lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (statesNames [i]) + " = new " + scriptName + "_" + statesNames [i] + " (this);\n"); 
			}

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "} \n\n");


			lst.Add (WriteTabs (tabNb) + "void Start () \n");

			lst.Add (WriteTabs (tabNb) + "{ \n");

			tabNb++;
			lst.Add (WriteTabs (tabNb) + "currentState = " + StringTreatment.FirstToLower (statesNames [0]) + "; \n\n");

			CSharpMonoBehaviourLogicNodesWithGameObjectsTrigger (theGraph, ref lst, ref tabNb);

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "} \n\n");


			lst.Add (WriteTabs (tabNb) + "void Update () \n");

			lst.Add (WriteTabs (tabNb) + "{ \n");

			tabNb++;
			lst.Add (WriteTabs (tabNb) + "currentState.StateUpdate (); \n");

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "} \n\n");


			lst.Add ("\t\tvoid LateUpdate ()\n\t\t{\n\t\t\tFreeameObjectsFoundBytriggerIndex ();\n\t\t}" + "\n\n");


			lst.Add ("\t\tvoid FreeameObjectsFoundBytriggerIndex ()\n\t\t{\n\t\t\tif (gameObjectsFoundBytrigger == null)\n\t\t\t{\n\t\t\t\tgameObjectsFoundBytriggerIndex = -1;\n\t\t\t}\n\t\t}" + "\n\n");


			//lst.Add ("\t\tvoid OnTriggerEnter(Collider other) \n\t\t{\n\t\t\tif (tagsForGameObjectsTriggers.Contains (other.gameObject.tag))\n\t\t\t{\n\t\t\t\tgameObjectsFoundBytriggerIndex = tagsForGameObjectsTriggers.IndexOf (other.gameObject.tag);\n\n\t\t\t\tgameObjectsFoundBytrigger = other.gameObject;\n\t\t\t}\n\t\t}" + "\n\n");

			//lst.Add ("\t\tvoid OnTriggerExit(Collider other) \n\t\t{\n\t\t\tif (tagsForGameObjectsTriggers.Contains (other.gameObject.tag))\n\t\t\t{\n\t\t\t\tgameObjectsFoundBytriggerIndex = -1;\n\n\t\t\t\tgameObjectsFoundBytrigger = null;\n\t\t\t}\n\t\t}" + "\n\n");

			//lst.Add ("\t\tvoid OnTriggerEnter(Collider other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//			
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadEnteredTriggerWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//lst.Add ("\t\tvoid OnTriggerExit(Collider other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadExitTriggerWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//
			//
			//lst.Add ("\t\tvoid OnTriggerEnter2D(Collider2D other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadEnteredTrigger2DWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//lst.Add ("\t\tvoid OnTriggerExit2D(Collider2D other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadExitTrigger2DWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//
			//
			//
			//
			//
			//lst.Add ("\t\tvoid OnCollisionEnter(Collision other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadEnteredCollisionWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.gameObject.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//			
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//lst.Add ("\t\tvoid OnCollisionExit(Collision other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadExitCollisionWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.gameObject.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//
			//
			//lst.Add ("\t\tvoid OnCollisionEnter2D(Collision2D other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadEnteredCollision2DWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.gameObject.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
			//
			//lst.Add ("\t\tvoid OnCollisionExit2D(Collision2D other) \n\t\t{\n");
			//for (int i = 0; i < theGraph.nodes.Count; i++)
			//{
			//	for (int j = 0; j < theGraph.nodes[i].logics.Count; j++)
			//	{
			//		for (int k = 0; k < theGraph.nodes[i].logics[j].nodes.Count; k++)
			//		{
			//			//theGraph.nodes[i].logics[j].nodes[k];
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].variableType != VariableType.GameObject)
			//				continue;
			//
			//			if (theGraph.nodes[i].logics[j].nodes[k].computeGameObjectType != 
			//				ComputeGameObjectType.hadExitCollision2DWithGameObjectOfTag)
			//				continue;
			//
			//			lst.Add ("\t\t\t" + "if (other.gameObject.tag == " +
			//				FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//					theGraph.nodes[i].logics[j].nodes[k]) +
			//				".stringValues [0])\n");
			//
			//			lst.Add ("\t\t\t{\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".gameObjectValue = other.gameObject;\n\n");
			//
			//			lst.Add ("\t\t\t\t" + FromMonoBehaviourToLogicNode (theGraph, theGraph.nodes[i], theGraph.nodes[i].logics[j],
			//				theGraph.nodes[i].logics[j].nodes[k]) +
			//				".boolValue = true;\n\n");
			//
			//			lst.Add ("\t\t\t}\n");
			//		}
			//	}
			//}
			//lst.Add ("\t\t}\n\n");
		}

		static void CSharpMonoBehaviourLogicNodesWithGameObjectsTrigger (Graph theGraph,
			ref List<string> lst, ref int tabNb)
		{
			//int n = -1;

			for (int i = 0; i < theGraph.nodes.Count; i++)
			{
				for (int j = 0; j < theGraph.nodes [i].logics.Count; j++)
				{
					for (int k = 0; k < theGraph.nodes [i].logics [j].nodes.Count; k++)
					{

						if (theGraph.nodes [i].logics [j].nodes [k].variableType == VariableType.GameObject)
						{
							//if (theGraph.nodes [i].logics [j].nodes [k].conditionGameObjectType == ConditionGameObjectType.hadEnteredTriggerWithGameObjectOfTag)
							//{
							//	lst.Add (WriteTabs (tabNb) + "tagsForGameObjectsTriggers. Add (" + "\"" + theGraph.nodes [i].logics [j].nodes [k].theTagToGameObjectTagCompare + "\"" + ");\n");
							//
							//	n++;
							//
							//	lst.Add (WriteTabs (tabNb) + FromMonoBehaviourToLogicNode (theGraph, 
							//		theGraph.nodes [i], theGraph.nodes [i].logics [j], theGraph.nodes [i].logics [j].nodes [k]) 
							//		+ "." + "gameObjectsToFindBytriggerIndex" + " = " + n.ToString () + ";" + "\n\n");
							//}
						}
					}
				}
			}
		}

		static void CSharpMonoBehaviourLogicNodesPublicVariablesToList (Graph theGraph,
			ref List<string> lst, ref int tabNb)
		{
			for (int i = 0; i < theGraph.nodes.Count; i++)
			{
				for (int j = 0; j < theGraph.nodes [i].logics.Count; j++)
				{
					for (int k = 0; k < theGraph.nodes [i].logics [j].nodes.Count; k++)
					{
						List <int> publicInputs = PublicInputs (theGraph.nodes [i].logics [j].nodes [k]);

						List <int> nv_publicInputs = Nv_PublicInputs (theGraph.nodes [i].logics [j].nodes [k]);

						for (int l = 0; l < publicInputs.Count; l++)
						{
							string varName = MonoBehaviourPublicVariableName (theGraph.nodes [i],
								theGraph.nodes [i].logics [j],
								theGraph.nodes [i].logics [j].nodes [k],
								theGraph.nodes [i].logics [j].nodes [k].inputsIDs [publicInputs [l]]);

							string varDecl = VariableTypeToDeclarationString (
								theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]]);

							//string varValue = VariableValueToString (
							//	theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]],
							//	theGraph.nodes [i].logics [j].nodes [k],
							//	theGraph.nodes [i].logics [j].nodes [k].inputsIDs [publicInputs [l]]);

							if (theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]] 
								== VariableType.GameObject)
							{
								if ( ! theGraph.nodes [i].logics [j].nodes [k].attachedToGameObject [publicInputs [l]])
									lst.Add (WriteTabs (tabNb) + 
										"public " + varDecl + " " + varName + ";\n\n");
							}
							else 
								//if (theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]] 
								//== VariableType.Shader ||
								//theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]] 
								//== VariableType.Texture2D ||
								//theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]] 
								//== VariableType.Material)
							{
								lst.Add (WriteTabs (tabNb) + 
									"public " + varDecl + " " + varName + ";\n\n");
							}
							//else
							//{
							//	lst.Add (WriteTabs (tabNb) + 
							//		"public " + varDecl + " " + varName + " = " + varValue + ";\n\n");
							//}

						}

						for (int l = 0; l < nv_publicInputs.Count; l++)
						{
							string nv_varName = MonoBehaviourPublicVariableName (theGraph.nodes [i],
								theGraph.nodes [i].logics [j],
								theGraph.nodes [i].logics [j].nodes [k],
								theGraph.nodes [i].logics [j].nodes [k].nv_inputsIDs [nv_publicInputs [l]]);

							string nv_varDecl = VariableTypeToDeclarationString (
								theGraph.nodes [i].logics [j].nodes [k].nv_inputsTypes [nv_publicInputs [l]]);

							//string varValue = VariableValueToString (
							//	theGraph.nodes [i].logics [j].nodes [k].inputsTypes [publicInputs [l]],
							//	theGraph.nodes [i].logics [j].nodes [k],
							//	theGraph.nodes [i].logics [j].nodes [k].inputsIDs [publicInputs [l]]);

							lst.Add (WriteTabs (tabNb) + 
								"public " + nv_varDecl + " " + nv_varName + ";\n\n");
						}
					}
				}
			}
		}

		static string MonoBehaviourPublicVariableName (Node theState, Logic theLogic,
			LogicNode theLogicNode, string inID)
		{
			return theState.nodeName + "_"
				+ theLogic.logicName + "_"
				+ theLogicNode.nodeName + "_"
				+ inID;
		}
		#endregion MonoBehaviour

		#region interface
		static void CSharpInterfaceFirstBlocToList (string scriptName, ref List<string> lst, ref int tabNb,
			string[] usingNames)
		{			
			lst = new List<string> ();

			for (int i = 0; i < usingNames.Length; i++)
			{
				lst.Add (usingNames [i]);
			}

			lst.Add ("\n");

			lst.Add ("namespace ScriptsCreatedByDiamond \n");
			lst.Add ("{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + "public interface I" + scriptName + "\n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;
		}
	
		static void CSharpInterfaceMethodsToList (ref List<string> lst, ref int tabNb, string[] statesNames)
		{
			lst.Add ("" + WriteTabs (tabNb) + "void StateUpdate ();\n\n");

			for (int i = 0; i < statesNames.Length; i++)
			{
				lst.Add (WriteTabs (tabNb) + "void " + "To" + statesNames [i] + " ();\n\n");
			}
		}
		#endregion interface

		#region state
		static void CSharpStateMethodsToList (ref List<string> lst, ref int tabNb, string[] statesNames,
			string graphName, string scriptName, Node theState)
		{
			lst.Add (WriteTabs (tabNb) + "public " + graphName + " " + StringTreatment.FirstToLower (graphName) + 
				"; \n\n");


			lst.Add (WriteTabs (tabNb) + "public " +  scriptName + " (" + graphName + " set" + graphName + ") \n");

			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName) + " = set" + graphName + ";\n\n");

			for (int i = 0; i < theState.logics.Count; i++)
			{
				string s0 = graphName + "_" + theState.nodeName + "_" + theState.logics [i].logicName;

				lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (s0) + 
					" = " + "new " + s0 + " (" + "this" + ");\n");
			}

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");


			lst.Add (WriteTabs (tabNb) + "public void StateUpdate () \n");

			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;


			//string s = new string ('"', 1);

			if (theState.logics.Count == 0)
			{				
				//lst.Add (WriteTabs (tabNb) + "Debug.Log (" + s + "StateUpdate" + s + ");\n");
			}
			else
			{
				for (int i = 0; i < theState.logics.Count; i++)
				{
					string s1 = graphName + "_" + theState.nodeName + "_" + theState.logics [i].logicName;

					lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (s1) + ".LogicUpdate ();\n");					
				}
			}

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");



			for (int i = 0; i < statesNames.Length; i++)
			{
				lst.Add (WriteTabs (tabNb) + "public void " + "To" + statesNames [i] + " ()\n");

				lst.Add (WriteTabs (tabNb) + "{\n");
				tabNb++;

				CSharpStateInitInts0ForStateStart (ref lst, ref tabNb, statesNames [i], graphName, 
					scriptName, theState);

				lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName) + ".currentState = " 
					+ StringTreatment.FirstToLower (graphName) +
					"." + StringTreatment.FirstToLower (statesNames [i]) + ";\n\n");


				tabNb--;
				lst.Add (WriteTabs (tabNb) + "}\n\n");
			}
		}
		static void CSharpStateInitInts0ForStateStart (ref List<string> lst, ref int tabNb, string statesName,
			string graphName, string scriptName, Node theState)
		{
			Node destinationState = null;

			for (int i = 0; i < theState.graph.nodes.Count; i++)
			{
				if (theState.graph.nodes [i].nodeName == statesName)
				{
					destinationState = theState.graph.nodes [i];

					break;
				}
			}

			if (destinationState == null)
				return;

			for (int i = 0; i < destinationState.logics.Count; i++)
			{
				for (int j = 0; j < destinationState.logics [i].nodes.Count; j++)
				{
					if (destinationState.logics [i].nodes [j].logicType == LogicType.computeOrOperation)
					{
						if (destinationState.logics [i].nodes [j].variableType == VariableType.Bool)
						{
							if (destinationState.logics [i].nodes [j].computeBoolType == ComputeBoolType.atStateStart)
							{
								lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName) + "." +
									FromMonoBehaviourToLogicNode (destinationState.logics [i].nodes [j]) +
									"." + "intValues [0] = 0;\n");

								lst.Add (WriteTabs (tabNb) + StringTreatment.FirstToLower (graphName) + "." +
									FromMonoBehaviourToLogicNode (destinationState.logics [i].nodes [j]) +
									"." + "boolValue = true;\n\n");
							}
						}
					}
				}
			}
		}

		static void CSharpStateFirstBlocToList (string scriptName, ref List<string> lst, ref int tabNb,
			string[] usingNames, string graphName, Node theState)
		{			
			lst = new List<string> ();

			for (int i = 0; i < usingNames.Length; i++)
			{
				lst.Add (usingNames [i]);
			}

			lst.Add ("\n");

			lst.Add ("namespace ScriptsCreatedByDiamond \n");
			lst.Add ("{\n");
			tabNb++;


			lst.Add (WriteTabs (tabNb) + "public class " + scriptName + " : I" + graphName + " \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;



			for (int i = 0; i < theState.logics.Count; i++)
			{
				string s = graphName + "_" + theState.nodeName + "_" + theState.logics [i].logicName;
				
				lst.Add (WriteTabs (tabNb) + "public " + s + " " + StringTreatment.FirstToLower (s) + ";\n");					
			}
			lst.Add (WriteTabs (tabNb) + "\n");
		}
		#endregion state


		//static string VariableTypeToDeclarationString (VariableType vt)
		//{
		//	string r = "";
		//
		//	string rStr = vt.ToString ();
		//
		//	if (StringTreatment.IsEndWith_List (rStr))
		//	{
		//		switch (vt)
		//		{
		//		case VariableType.boolsList:
		//			 r ="List <bool>";
		//			break;
		//
		//		case VariableType.colorsList:
		//			r = "List <Color>";
		//			break;
		//
		//		case VariableType.floatsList:
		//			r = "List <float>";
		//			break;
		//
		//		case VariableType.GameObjectList:
		//			r = "List <GameObject>";
		//			break;
		//
		//		case VariableType.intsList:
		//			r = "List <int>";
		//			break;
		//
		//		case VariableType.materialsList:
		//			r = "List <Material>";
		//			break;
		//
		//		case VariableType.rectsList:
		//			r = "List <Rect>";
		//			break;
		//
		//		case VariableType.shadersList:
		//			r = "List <Shader>";
		//			break;
		//
		//		case VariableType.stringsList:
		//			r = "List <string>";
		//			break;
		//
		//		case VariableType.texture2DList:
		//			r = "List <Texture2D>";
		//			break;
		//
		//		case VariableType.vector2List:
		//			r = "List <Vector2>";
		//			break;
		//
		//		case VariableType.vector3List:
		//			r = "List <Vector3>";
		//			break;
		//
		//		case VariableType.vector4List:
		//			r = "List <Vector4>";
		//			break;
		//		}
		//	}
		//	else if ( ! StringTreatment.IsEndWith_List (rStr))
		//	{
		//		if (vt == VariableType.Bool || vt == VariableType.Float ||
		//			vt == VariableType.Int || vt == VariableType.String)
		//		{
		//			r = StringTreatment.FirstToLower (vt.ToString ());
		//		}
		//		else
		//		{
		//			r = rStr;
		//		}
		//	}
		//
		//	return r;
		//}

		public static string VariableTypeToDeclarationString (VariableType vt)
		{
			string r = "";

			switch (vt)
			{
			case VariableType.Bool:
				r = "bool";
				break;

			case VariableType.boolsList:
				r = "List<bool>";
				break;

			case VariableType.Camera:
				r = "GameObject";
				break;

			case VariableType.Color:
				r = "Color";
				break;

			case VariableType.colorsList:
				r = "List<Color>";
				break;

			case VariableType.componentCollider:
				r = "GameObject";
				break;

			case VariableType.componentCollider2D:
				r = "GameObject";
				break;

			case VariableType.componentNavMeshAgent:
				r = "GameObject";
				break;

			case VariableType.componentParticleSystem:
				r = "GameObject";
				break;

			case VariableType.componentRenderer:
				r = "GameObject";
				break;

			case VariableType.componentRigidBody:
				r = "GameObject";
				break;

			case VariableType.componentRigidBody2D:
				r = "GameObject";
				break;

			case VariableType.componentTransform:
				r = "GameObject";
				break;

			case VariableType.Float:
				r = "float";
				break;

			case VariableType.floatsList:
				r = "List<float>";
				break;

			case VariableType.GameObject:
				r = "GameObject";
				break;

			case VariableType.GameObjectList:
				r = "List<GameObject>";
				break;

			case VariableType.Int:
				r = "int";
				break;

			case VariableType.intsList:
				r = "List<int>";
				break;

			case VariableType.Material:
				r = "Material";
				break;

			case VariableType.materialsList:
				r = "List<Material>";
				break;

			case VariableType.Matrix4x4:
				r = "Matrix4x4";
				break;

			case VariableType.Ray:
				break;

			case VariableType.Ray2D:
				break;

			case VariableType.rect:
				r = "Rect";
				break;

			case VariableType.rectsList:
				r = "List<Rect>";
				break;

			case VariableType.Shader:
				r = "Shader";
				break;

			case VariableType.shadersList:
				r = "List<Shader>";
				break;

			case VariableType.String:
				r = "string";
				break;

			case VariableType.stringsList:
				r = "List<string>";
				break;

			case VariableType.Texture2D:
				r = "Texture2D";
				break;

			case VariableType.texture2DList:
				r = "List<Texture2D>";
				break;

			case VariableType.Vector2:
				r = "Vector2";
				break;

			case VariableType.vector2List:
				r = "List<Vector2>";
				break;

			case VariableType.Vector3:
				r = "Vector3";
				break;

			case VariableType.vector3List:
				r = "List<Vector3>";
				break;

			case VariableType.Vector4:
				r = "Vector4";
				break;

			case VariableType.vector4List:
				r = "List<Vector4>";
				break;
			}

			return r;
		}

		//static string VariableValueToString (VariableType vt, LogicNode theLogicNode, string varID)
		//{
		//	string r = "";
		//
		//	if (vt == VariableType.Bool)
		//	{
		//		r = (varID == Enums.boolValues_0_ID)? 
		//			StringTreatment.FirstToLower (theLogicNode.boolValues [0].ToString ()):
		//				(varID == Enums.boolValues_1_ID)?
		//			StringTreatment.FirstToLower (theLogicNode.boolValues [1].ToString ()):
		//			(varID == Enums.doIt_ID)? 
		//			StringTreatment.FirstToLower (theLogicNode.doIT.ToString ()): "";
		//	}
		//	else if (vt == VariableType.Color)
		//	{
		//		r = (varID == Enums.colorValues_0_ID)? 
		//			"new Color ("
		//			+ theLogicNode.colorValues [0].r.ToString () + "f, "
		//			+ theLogicNode.colorValues [0].g.ToString () + "f, "
		//			+ theLogicNode.colorValues [0].b.ToString () + "f, "
		//			+ theLogicNode.colorValues [0].a.ToString () + "f"
		//			+ ")":
		//			(varID == Enums.colorValues_1_ID)?
		//			"new Color ("
		//			+ theLogicNode.colorValues [1].r.ToString () + "f, "
		//			+ theLogicNode.colorValues [1].g.ToString () + "f, "
		//			+ theLogicNode.colorValues [1].b.ToString () + "f, "
		//			+ theLogicNode.colorValues [1].a.ToString () + "f"
		//			+ ")": "";
		//	}
		//	else if (vt == VariableType.Float)
		//	{
		//		r = (varID == Enums.floatValues_0_ID)? 
		//			theLogicNode.floatValues [0].ToString () + "f":
		//			(varID == Enums.floatValues_1_ID)?
		//			theLogicNode.floatValues [1].ToString () + "f": theLogicNode.floatValues [2].ToString () + "f";
		//	}
		//	else if (vt == VariableType.GameObject)
		//	{
		//		r = "";
		//	}
		//	else if (vt == VariableType.Int)
		//	{
		//		r = (varID == Enums.intValues_0_ID)? 
		//			theLogicNode.intValues [0].ToString ():
		//			(varID == Enums.intValues_1_ID)?
		//			theLogicNode.intValues [1].ToString (): theLogicNode.intValues [2].ToString ();
		//	}
		//	else if (vt == VariableType.String)
		//	{
		//		r = (varID == Enums.stringValues_0_ID)? 
		//			"\"" + theLogicNode.stringValues [0] + "\"":
		//			(varID == Enums.stringValues_1_ID)?
		//			"\"" + theLogicNode.stringValues [1] + "\"": "";
		//	}
		//	else if (vt == VariableType.Vector2)
		//	{
		//		r = (varID == Enums.vector2Values_0_ID)? 
		//			"new Vector2 ("
		//			+ theLogicNode.vector2Values [0].x.ToString () + "f, "
		//			+ theLogicNode.vector2Values [0].y.ToString () + "f"
		//			+ ")":
		//			(varID == Enums.vector2Values_1_ID)?
		//			"new Vector2 ("
		//			+ theLogicNode.vector2Values [1].x.ToString () + "f, "
		//			+ theLogicNode.vector2Values [1].y.ToString () + "f"
		//			+ ")":
		//			(varID == Enums.ray2DOrigin_ID)?
		//			"new Vector2 ("
		//			+ theLogicNode.ray2DValueOrigin.x.ToString () + "f, "
		//			+ theLogicNode.ray2DValueOrigin.y.ToString () + "f"
		//			+ ")": 
		//			(varID == Enums.ray2DDirection_ID)?
		//			"new Vector2 ("
		//			+ theLogicNode.ray2DDirectionValueNotNormalized.x.ToString () + "f, "
		//			+ theLogicNode.ray2DDirectionValueNotNormalized.y.ToString () + "f"
		//			+ ")": "";
		//	}
		//	else if (vt == VariableType.Vector3)
		//	{
		//		r = (varID == Enums.vector3Values_0_ID)? 
		//			"new Vector3 ("
		//			+ theLogicNode.vector3Values [0].x.ToString () + "f, "
		//			+ theLogicNode.vector3Values [0].y.ToString () + "f, "
		//			+ theLogicNode.vector3Values [0].z.ToString () + "f"
		//			+ ")":
		//			(varID == Enums.vector3Values_1_ID)?
		//			"new Vector3 ("
		//			+ theLogicNode.vector3Values [1].x.ToString () + "f, "
		//			+ theLogicNode.vector3Values [1].y.ToString () + "f, "
		//			+ theLogicNode.vector3Values [1].z.ToString () + "f"
		//			+ ")":
		//			(varID == Enums.rayOrigin_ID)?
		//			"new Vector3 ("
		//			+ theLogicNode.rayValueOrigin.x.ToString () + "f, "
		//			+ theLogicNode.rayValueOrigin.y.ToString () + "f, "
		//			+ theLogicNode.rayValueOrigin.z.ToString () + "f"
		//			+ ")": 
		//			(varID == Enums.rayDirection_ID)?
		//			"new Vector3 ("
		//			+ theLogicNode.rayDirectionValueNotNormalized.x.ToString () + "f, "
		//			+ theLogicNode.rayDirectionValueNotNormalized.y.ToString () + "f, "
		//			+ theLogicNode.rayDirectionValueNotNormalized.z.ToString () + "f"
		//			+ ")": "";
		//	}
		//
		//	return r;
		//}


		#region writers
		public static void WriteStatesNamesEnumScript ()
		{
			string [] statesNames = null;

			int tabNb = 0;

			statesNamesEnumScriptList = new List<string> ();


			statesNamesEnumScriptList.Add ("namespace Mezanix.Diamond\n");

			statesNamesEnumScriptList.Add ("{\n");
			tabNb++;


			Diamond diamond = EditorWindow.GetWindow <Diamond> ();

			if (diamond == null)
				return;

			Graph graph = null;

			graph = diamond.graph;

			if (graph == null)
				return;


			
			statesNamesEnumScriptList.Add (WriteTabs (tabNb) + "public enum " + "CurrentStatesNames\n");

			statesNamesEnumScriptList.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			statesNames = new string[graph.nodes.Count];

			for (int k = 0; k < graph.nodes.Count; k++)
			{
				statesNames [k] = graph.nodes [k].nodeName;
			}

			for (int j = 0; j < statesNames.Length; j++)
			{
				statesNamesEnumScriptList.Add (WriteTabs (tabNb) + statesNames [j] + ",\n\n");
			}

			tabNb--;
			statesNamesEnumScriptList.Add (WriteTabs (tabNb) + "}\n\n");

			tabNb--;
			statesNamesEnumScriptList.Add (WriteTabs (tabNb) + "}\n");


			CsScriptListToFile (auxiliariesFolder, "StatesNamesEnum", statesNamesEnumScriptList, false);
		}

		/*
		public static void WriteTagsEnumScript ()
		{
			int tabNb = 0;


			List <string> tagsEnumScriptList = new List<string> ();

			tagsEnumScriptList.Add ("namespace Mezanix.Diamond\n");

			tagsEnumScriptList.Add ("{\n");
			tabNb++;

			tagsEnumScriptList.Add (WriteTabs (tabNb) + "public enum " + "CurrentTags\n");

			tagsEnumScriptList.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			tags = UnityEditorInternal.InternalEditorUtility.tags;

			for (int j = 0; j < tags.Length; j++)
			{
				tagsEnumScriptList.Add (WriteTabs (tabNb) 
					+ tags [j].Replace (" ", "w_DiaSpaceMond_w") + ",\n\n");
			}

			tabNb--;
			tagsEnumScriptList.Add (WriteTabs (tabNb) + "}\n\n");

			tabNb--;
			tagsEnumScriptList.Add (WriteTabs (tabNb) + "}\n");


			CsScriptListToFile (auxiliariesFolder, "TagsEnum", tagsEnumScriptList);

			Debug.Log ("tags written");
		}
		*/



		public static void WriteNestedStatesFunctions (string nameRacine, string [][] enumNames, 
			string [][] variablesDelaration)
		{
			string p = @"Assets";

			string fn = "Fn"; 

			string ft = ".cs";

			string ns = "Mezanix.Diamond";

			string path = p + "/" + fn + ft;

			int tabNb = 0;

			List<string> lst = new List<string> ();


			lst.Add (WriteTabs (tabNb) + "namespace " + ns + " \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			lst.Add (WriteTabs (tabNb) + "public class " + fn + " \n");
			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;


			lst.Add (DeclareStates (enumNames, ns, ref tabNb));


			lst.Add (WriteTabs (tabNb) + FunctionSignature ("public ", "", "void ", nameRacine, variablesDelaration) + "\n");

			lst.Add (WriteTabs (tabNb) + "{\n");
			tabNb++;

			//lst.Add (WriteTabs (tabNb) + "string r = " + "\"" + "\"" + ";" + "\n\n");


			List <string> nameRacines = new List<string>();

			lst.Add (NestedSwitchs (enumNames, 0, 2, nameRacine, variablesDelaration, ref nameRacines, ref tabNb));


			//lst.Add (WriteTabs (tabNb) + "return r;" + "\n");

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");


			//string functionsContent = "\t\t\tstring r = \"\";\n\n\t\t\tswitch (csLogicNodeWhere)\n\t\t\t{\n\t\t\tcase CsLogicNodeWhere.constructor:\n\t\t\t\tbreak;\n\n\t\t\tcase CsLogicNodeWhere.globalVariables:\n\t\t\t\tbreak;\n\n\t\t\tcase CsLogicNodeWhere.logicNodeUpdate:\n\t\t\t\tbreak;\n\n\t\t\tcase CsLogicNodeWhere.methods:\n\t\t\t\tbreak;\n\t\t\t}\n\n\t\t\treturn r;" + "\n";

			string functionsContent = "";


			//lst.Add (FunctionSignature ("", "static ", "string ", nameRacines, variablesDelaration, functionsContent, ref tabNb));
			lst.Add (FunctionSignature ("", "", "void ", nameRacines, variablesDelaration, functionsContent, ref tabNb));


			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n\n");

			File.WriteAllText (path, "");

			for (int i = 0; i < lst.Count; i++)
			{
				File.AppendAllText (path, lst [i]);
			}
		}

		static string DeclareStates (string [][] enumNames, string ns, ref int tabNb)
		{
			string r = "";
			
			for (int i = 0; i < enumNames.Length; i++)
			{
				r += WriteTabs (tabNb) + ns + "." + "Enums" + "." 
					+ enumNames [i][1] + " " + enumNames [i][0] + ";\n\n";
			}

			return r;
		}

		static string NestedSwitchs (string [][] enumNames, int startState, int startName, 
			string nameRacine, string [][] variablesDeclaration, ref List <string> nameRacines, ref int tabNb)
		{
			string r = "";
	
			int s = startState;


			r += WriteTabs (tabNb) + "switch (" + enumNames [s][0] + ")" + "\n";
			r += WriteTabs (tabNb) + "{\n";

			if (s == enumNames.Length-1)
			{
				int n = 2;
				for (int j = n; j < enumNames[s].Length; j++)
				{
					r += WriteTabs (tabNb) + "case " + enumNames[s][1] + "." + enumNames [s][j] + ":" + "\n";
					tabNb++;

					string nameRacineTmp = nameRacine;

					nameRacine += "_" + enumNames [s][j];

					nameRacines.Add (nameRacine);

					//r += WriteTabs (tabNb) + "r = " + FunctionCall (nameRacine, variablesDeclaration) + ";\n";
					r += WriteTabs (tabNb) + "" + FunctionCall (nameRacine, variablesDeclaration) + ";\n";

					nameRacine = nameRacineTmp;

					r += WriteTabs (tabNb) + "break;\n\n";
					tabNb--;
				}
				r += WriteTabs (tabNb) + "}\n\n";
			}
			else
			{
				int n = 2;
				for (int j = n; j < enumNames[s].Length; j++)
				{
					r += WriteTabs (tabNb) + "case " + enumNames[s][1] + "." + enumNames [s][j] + ":" + "\n";
					tabNb++;

					string nameRacineTmp = nameRacine;

					nameRacine += "_" + enumNames [s][j];

					r += NestedSwitchs (enumNames, s+1, 2, nameRacine, variablesDeclaration, ref nameRacines, ref tabNb);

					nameRacine = nameRacineTmp;

					r += WriteTabs (tabNb) + "break;\n\n";
					tabNb--;
				}
				r += WriteTabs (tabNb) + "}\n\n";
			}

			return r;
		}

		static string Switch (string [] enumNames, ref int tabNb)
		{
			string r = "";
			
			r += WriteTabs (tabNb) + "switch (" + enumNames [0] + ")" + "\n";

			r += WriteTabs (tabNb) + "{\n";

			for (int j = 2; j < enumNames.Length; j ++)
			{
				r += WriteTabs (tabNb) + "case " + enumNames [1] + "." + enumNames [j] + ":" + "\n";
				tabNb++;

				r += WriteTabs (tabNb) + "break;" + "\n\n";
				tabNb--;
			}

			tabNb--;
			r += WriteTabs (tabNb) + "}\n"; 			

			return r;
		}

		static string FunctionSignature (string access, string staticF, string outF,
			List<string> nameRacines, string [][] variablesDelaration, string content, ref int tabNb)
		{
			string r = "";

			for (int i = 0; i < nameRacines.Count; i++)
			{
				r += WriteTabs (tabNb) + FunctionSignature (access, staticF, outF, nameRacines [i], variablesDelaration) + "\n";

				r += WriteTabs (tabNb) + "{\n";
				tabNb++;

				r += content;

				tabNb--;
				r += WriteTabs (tabNb) + "}\n\n";
			}

			return r;
		}

		static string FunctionSignature (string access, string staticF, string outF,
			string nameRacine, string [][] variablesDelaration)
		{
			return access + staticF + outF + nameRacine + 
				VariablesDeclarationsInFunction (variablesDelaration);
		}

		static string FunctionCall (string nameRacine, string [][] variablesDelaration)
		{
			return nameRacine + VariablesCallInFunctionCall (variablesDelaration);
		}

		static string VariablesDeclarationsInFunction (string [][] variablesDelaration)
		{
			string r = " (";

			for (int i = 0; i < variablesDelaration.Length; i++)
			{
				for (int j = 0; j < variablesDelaration [i].Length; j++)
				{
					r += " " + variablesDelaration [i][j];
				}
				if (i == variablesDelaration.Length - 1)
					r += ")";
				else
					r += ", ";
			}

			return r;
		}

		static string VariablesCallInFunctionCall (string [][] variablesDelaration)
		{
			string r = " (";

			for (int i = 0; i < variablesDelaration.Length; i++)
			{
				r += " " + variablesDelaration [i][variablesDelaration [i].Length-1];

				if (i == variablesDelaration.Length - 1)
					r += ")";
				else
					r += ", ";
			}

			return r;
		}

		static void CSharpCloseClassBlocToList (ref List<string> lst, ref int tabNb)
		{
			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n");

			tabNb--;
			lst.Add (WriteTabs (tabNb) + "}\n");
		}

		static string WriteTabs (int tabNb)
		{
			string retVal = "";

			for (int i = 0; i < tabNb; i++)
			{
				retVal += "\t";
			}

			return retVal;
		}
		#endregion writers
	

		//public static void CsScriptListToFile (string folder, 
		//	string fileName, List<string> lst)
		//{
		//	string filePath = folder + "/" + fileName + ".cs";
		//
		//	File.WriteAllText (filePath, "");
		//
		//	for (int i = 0; i < lst.Count; i++)
		//	{
		//		File.AppendAllText (filePath, lst [i]);
		//	}
		//}

		public static void CsScriptListToFile (string folder, 
			string fileName, List<string> lst, bool mono)
		{
			string filePath = folder + "/" + fileName + ".cs";

			File.WriteAllText (filePath, "");

			for (int i = 0; i < lst.Count; i++)
			{
				File.AppendAllText (filePath, lst [i]);
			}

			if (mono)
			{
				Object texObj = AssetDatabase.LoadAssetAtPath <Object> (filePath);


				if (texObj != null)
				{
					EditorGUIUtility.PingObject (texObj);
				}
			}
		}

		/*static void statesNamesEnumScriptFileToList ()
		{
			statesNamesEnumScriptList = new List<string> ();

			string[] s = File.ReadAllLines (auxiliariesFolder + "/" + "StatesNamesEnum" + ".cs");

			if (s.Length == 0)
				return;

			for (int i = 0; i < s.Length; i++)
			{
				statesNamesEnumScriptList.Add (s [i]);

				Debug.Log (s [i]);
			}
		}*/
	}
}
