using technical.test.editor;
using UnityEditor;
using UnityEngine;

public class ShowGizmos : EditorWindow
{
    SceneGizmoAsset _assetSceneGizmo = null;
    Gizmo[] _gizmos = null;


    [MenuItem("Window/Custom/Show Gizmos")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ShowGizmos));
    }

    private void OnGUI()
    {
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Editor/SGA.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();


        GUILayout.Label("Gizmo Editor", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        for( int i = 0; i < _gizmos.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            _gizmos[i].Name = EditorGUILayout.TextField("Text", _gizmos[i].Name, GUILayout.ExpandWidth(true));
            _gizmos[i].Position.x = EditorGUILayout.FloatField("x", _gizmos[i].Position.x, GUILayout.ExpandWidth(true));
            _gizmos[i].Position.y = EditorGUILayout.FloatField("y", _gizmos[i].Position.y, GUILayout.ExpandWidth(true));
            _gizmos[i].Position.z = EditorGUILayout.FloatField("z", _gizmos[i].Position.z, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Edit"))
            {
                Debug.Log(i);
            }
            EditorGUILayout.EndHorizontal();
            
        }
        

        EditorGUILayout.EndVertical();
    }


    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.white;
        for(int i = 0; i < _gizmos.Length; i++)
        {
            Gizmos.DrawSphere(_gizmos[i].Position, 1);
        }
#endif
    }

    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.white;
        for (int i = 0; i < _gizmos.Length; i++)
        {
            Gizmos.DrawSphere(_gizmos[i].Position, 1);
        }
#endif
    }
}
