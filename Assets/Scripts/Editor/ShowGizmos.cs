using technical.test.editor;
using UnityEditor;
using UnityEngine;

public class ShowGizmos : EditorWindow
{
    SceneGizmoAsset _assetSceneGizmo = null;
    Gizmo[] _gizmos = null;
    Gizmo[] _initialGizmos = null;

    [MenuItem("Window/Custom/Show Gizmos")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ShowGizmos));

    }

    private void OnGUI()
    {
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Scene Gizmo Asset.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();
        _initialGizmos = _assetSceneGizmo.GetGizmos();

        if(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            _gizmos = _initialGizmos;
            Debug.Log("yes");
        }


        GUILayout.Label("Gizmo Editor", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        for( int i = 0; i < _gizmos.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            _gizmos[i].Name = EditorGUILayout.TextField("Text", _gizmos[i].Name);
            EditorGUILayout.Space();
            _gizmos[i].Position.x = EditorGUILayout.FloatField("x", _gizmos[i].Position.x);
            _gizmos[i].Position.y = EditorGUILayout.FloatField("y", _gizmos[i].Position.y);
            _gizmos[i].Position.z = EditorGUILayout.FloatField("z", _gizmos[i].Position.z);
            EditorGUILayout.Space();
            if (GUILayout.Button("Edit"))
            {
                Debug.Log(i);
            }
            EditorGUILayout.EndHorizontal();
            
        }
        

        EditorGUILayout.EndVertical();
    }
}
