using UnityEngine;
using UnityEditor;
using technical.test.editor;

[CustomEditor(typeof(SceneGizmoAsset))]
public class CustomShowGizmos : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        if (GUILayout.Button("Open Show Gizmos Tool"))
        {
            ShowGizmos.GetWindow(typeof(ShowGizmos));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
