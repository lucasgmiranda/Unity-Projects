using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIButtonData))]
public class UIButtonDataEditor : Editor {

	UIButtonData myTarget;
	SerializedObject SOTarget;

	SerializedProperty enabledSprite;
	SerializedProperty disabledSprite;

	void OnEnable()
	{
		myTarget = (UIButtonData)target;
		SOTarget = new SerializedObject(target);

		enabledSprite = SOTarget.FindProperty("enabledSprite");
		disabledSprite = SOTarget.FindProperty("disabledSprite");

	}

	public override void OnInspectorGUI()
	{
		SOTarget.Update();

		EditorGUI.BeginChangeCheck();

		myTarget.buttonType = (UIButtonData.ButtonType)EditorGUILayout.EnumPopup("Button Type", myTarget.buttonType);

		switch (myTarget.buttonType)
		{
			case UIButtonData.ButtonType.Switch:
				EditorGUILayout.PropertyField(enabledSprite);
				EditorGUILayout.PropertyField(disabledSprite);
				break;
			case UIButtonData.ButtonType.Click:
				break;
			case UIButtonData.ButtonType.Hold:
				break;			
		}

		if (EditorGUI.EndChangeCheck())
		{
			SOTarget.ApplyModifiedProperties();
			GUI.FocusControl(null);
		}
	}
}
