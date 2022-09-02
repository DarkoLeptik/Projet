using technical.test.editor;
using UnityEditor;
using UnityEngine;

public class ShowGizmos : EditorWindow
{
    SceneGizmoAsset _assetSceneGizmo = null;
    Gizmo[] _gizmos = null;
    //static Gizmo[] _initialGizmos = null;
    static bool _isEditClicked;
    static int _indexGizmoClicked;

    

    [MenuItem("Window/Custom/Show Gizmos")]
    public static void Init()
    {
        ShowGizmos windowShowGizmos = GetWindow<ShowGizmos>();
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        SceneGizmoAsset assetGizmos = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Show Gizmos In Scene.asset", typeof(SceneGizmoAsset));
        Gizmo[] gizmos = assetGizmos.GetGizmos();

        for(int i = 0; i < gizmos.Length; i++)
        {
            Handles.color = Color.white;
            Handles.SphereHandleCap(0, gizmos[i].Position, Camera.current.transform.rotation, 0.5f, EventType.Repaint);
            Handles.color = Color.black;
            Handles.DrawLine(gizmos[i].Position, gizmos[i].Position + new Vector3(0, 0, 0.25f));
            GUI.color = Color.black;
            Handles.Label(gizmos[i].Position + new Vector3(0, 0, 0.3f), gizmos[i].Name);
        }

        EditorGUI.BeginChangeCheck();
        if (_isEditClicked)
        {
            gizmos[_indexGizmoClicked].Position = Handles.PositionHandle(gizmos[_indexGizmoClicked].Position, Quaternion.identity);
        }
        if (EditorGUI.EndChangeCheck())
        {
            
        }

        if(Event.current.button == 1)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Reset Position"), false, ResetPosition, 1);
                menu.AddItem(new GUIContent("Delete Gizmo"), false, DeleteGizmo, 2);
                menu.ShowAsContext();
            }
        }
        HandleUtility.Repaint();
    }

    static void ResetPosition(object obj)
    {
        Debug.Log("Reset Position");
    }
    static void DeleteGizmo(object obj)
    {
        Debug.Log("Delete Gizmo");
    }


    private void OnGUI()
    {
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Show Gizmos In Scene.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();

        GUILayout.Label("Gizmo Editor", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        for( int i = 0; i < _gizmos.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            _gizmos[i].Name = EditorGUILayout.TextField("Text", _gizmos[i].Name, GUILayout.Width(200));
            _gizmos[i].Position.x = EditorGUILayout.FloatField("x", _gizmos[i].Position.x, GUILayout.Width(200));
            _gizmos[i].Position.y = EditorGUILayout.FloatField("y", _gizmos[i].Position.y, GUILayout.Width(200));
            _gizmos[i].Position.z = EditorGUILayout.FloatField("z", _gizmos[i].Position.z, GUILayout.Width(200));
            if (GUILayout.Button("Edit"))
            {
                _indexGizmoClicked = i;
                _isEditClicked = !_isEditClicked;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }
}
