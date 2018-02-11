using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridGenerator))]
public class GridGenEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGenerator grid = (GridGenerator)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate")) grid.GridCreation();

        if (GUILayout.Button("Clean")) grid.GridDestroy();

        GUILayout.EndHorizontal();
    }
}
