using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

		LevelManager level = (LevelManager)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate")) level.GridCreation();

        if (GUILayout.Button("Clean")) level.GridDestroy();

        GUILayout.EndHorizontal();
    }
}
