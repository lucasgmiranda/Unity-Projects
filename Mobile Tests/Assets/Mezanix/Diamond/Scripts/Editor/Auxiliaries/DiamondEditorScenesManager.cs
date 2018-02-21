using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Mezanix.Diamond
{
	public class DiamondEditorScenesManager
	{
		public static void MakeActiveSceneDirty ()
		{
			Scene scene = EditorSceneManager.GetActiveScene ();

			EditorSceneManager.MarkSceneDirty (scene);
		}
	}
}