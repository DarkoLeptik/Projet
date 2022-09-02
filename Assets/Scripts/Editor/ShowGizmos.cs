using technical.test.editor;
using UnityEditor;
using UnityEngine;

public class ShowGizmos : EditorWindow
{
    static SceneGizmoAsset _assetSceneGizmo = null;
    static Gizmo[] _gizmos = null;
    static Gizmo[] _initialGizmos = null;
    static bool _isEditClicked;
    static int _indexGizmoEditClicked;
    static int _indexGizmoRightClicked;
    static bool _initializedGizmos = false;

    

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
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Show Gizmos In Scene.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();
        if (_initializedGizmos == false)
        {
            _initialGizmos = _gizmos;
        }
        for(int i = 0; i < _gizmos.Length; i++)
        {
            Handles.color = Color.white;
            Handles.SphereHandleCap(0, _gizmos[i].Position, Camera.current.transform.rotation, 0.5f, EventType.Repaint);
            Handles.color = Color.black;
            Handles.DrawLine(_gizmos[i].Position, _gizmos[i].Position + new Vector3(0, 0, 0.25f));
            GUI.color = Color.black;
            Handles.Label(_gizmos[i].Position + new Vector3(0, 0, 0.3f), _gizmos[i].Name);
        }

        EditorGUI.BeginChangeCheck();
        if (_isEditClicked)
        {
            _gizmos[_indexGizmoEditClicked].Position = Handles.PositionHandle(_gizmos[_indexGizmoEditClicked].Position, Quaternion.identity);
        }
        if (EditorGUI.EndChangeCheck())
        {
            
        }

        switch (Event.current.type)
        {
            case EventType.MouseDown:
                {
                    if(Event.current.button == 1)
                    {
                        for(int i = 0; i < _gizmos.Length; i++)
                        {
                            float dist = Vector2.Distance(Event.current.mousePosition, HandleUtility.WorldToGUIPoint(_gizmos[i].Position));
                            if (dist < 20)
                            {
                                _indexGizmoRightClicked = i;
                                GenericMenu menu = new GenericMenu();
                                menu.AddItem(new GUIContent("Reset Position"), false, ResetPosition, 1);
                                menu.AddItem(new GUIContent("Delete Gizmo"), false, DeleteGizmo, 2);
                                menu.ShowAsContext();
                            }
                        }
                    }
                    break;
                }
        }
        //Debug.Log(_initialGizmos[0].Position);
        //Debug.Log(_gizmos[0].Position);
        Debug.Log(_initializedGizmos);
        HandleUtility.Repaint();
    }

    static void ResetPosition(object obj)
    {
        _gizmos[_indexGizmoRightClicked].Position = _initialGizmos[_indexGizmoRightClicked].Position;
    }
    static void DeleteGizmo(object obj)
    {
        Gizmo[] gizmos = new Gizmo[_gizmos.Length-1];
        int lastIndexAdded = 0;
        for(int i = 0; i < _gizmos.Length; i++)
        {
            if(i != _indexGizmoRightClicked)
            {
                gizmos[lastIndexAdded] = _gizmos[i];
                _initialGizmos[lastIndexAdded] = _gizmos[i];
                lastIndexAdded++;
            }
        }
        _assetSceneGizmo.SetGizmos(gizmos);
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
                _indexGizmoEditClicked = i;
                _isEditClicked = !_isEditClicked;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }
}
