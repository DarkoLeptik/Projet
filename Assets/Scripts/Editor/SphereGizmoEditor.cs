using UnityEngine;
using UnityEditor;
using technical.test.editor;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(SphereGizmo))]
public class SphereGizmoEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    SceneGizmoAsset _assetSceneGizmo = null;
    Gizmo[] _gizmos = null;
    Gizmo[] _initialGizmos = null;

    public void OnSceneGUI()
    {
        var t = target as SphereGizmo;
        var tr = t.transform;
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Scene Gizmo Asset.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();
        Gizmos.color = Color.red;
        for (int i = 0; i < _gizmos.Length; i++)
        {
            Handles.DrawWireDisc(_gizmos[i].Position, tr.up, 1.0f);
            GUI.color = Color.black;
            Handles.Label(_gizmos[i].Position, _gizmos[i].Name);
        }
    }
}