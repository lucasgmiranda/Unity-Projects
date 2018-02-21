using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mezanix.Diamond
{
	public class NamesToSave : ScriptableObject 
	{
		public const string myName = "NamesToSave";

		public List<string> projectVariablesNames;

		public List<string> classesNames;

		public string graphPath;


		public string scriptsGenerationFolderPath;

		public string texturesGenerationFolderPath;


		public Color [] variableTypeColor = new Color[1];


		#region GameDesign
		public BuildTargetGroup GaDeLightMazbuildTargetGroup;
		public int GaDeLightMazbuildTargetGroup_length = 0;
		public string GaDeLightMazBuildTargetGroup_last;
		#endregion GameDesign

		public void Init ()
		{
			projectVariablesNames = new List<string>();

			classesNames = new List<string>();

			graphPath = "";
		}
	}
}