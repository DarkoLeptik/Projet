using technical.test.editor;
using UnityEditor;
using UnityEngine;

public class ShowGizmos : EditorWindow
{
    static SceneGizmoAsset _assetSceneGizmo = null;
    static Gizmo[] _gizmos = null;
    static Gizmo[] _initialGizmos = null; //Get initial gizmos to reset their position with the right click
    static bool _initializedGizmos = false; //Bool to get initial gizmos only one time
    static bool _isEditClicked;
    static int _indexGizmoEditClicked;
    static int _indexGizmoRightClicked;

    int _spaceBetweenElements = 20;
    

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

        //Check if the initial gizmos are already get or not
        if (_initializedGizmos == false)
        {
            _initialGizmos = _gizmos;
            _initializedGizmos = true;
        }

        //Draw the shperes and description on the sceneViews
        for (int i = 0; i < _gizmos.Length; i++)
        {
            Handles.color = Color.white;
            Handles.SphereHandleCap(0, _gizmos[i].Position, Camera.current.transform.rotation, 0.5f, EventType.Repaint);
            Handles.color = Color.black;
            Handles.DrawLine(_gizmos[i].Position, _gizmos[i].Position + new Vector3(0, 0, 0.25f));
            GUI.color = Color.black;
            Handles.Label(_gizmos[i].Position + new Vector3(0, 0, 0.3f), _gizmos[i].Name);
        }

        //Logic arount edit Button in EditorWindow
        if (_isEditClicked)
        {
            _gizmos[_indexGizmoEditClicked].Position = Handles.PositionHandle(_gizmos[_indexGizmoEditClicked].Position, Quaternion.identity);
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
        HandleUtility.Repaint();
    }

    //Callbacks of the menu items when right clicking on gizmo in sceneView
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
        _gizmosChanged = true;
    }

    private void OnGUI()
    {
        _assetSceneGizmo = (SceneGizmoAsset)AssetDatabase.LoadAssetAtPath("Assets/Data/Editor/Show Gizmos In Scene.asset", typeof(SceneGizmoAsset));
        _gizmos = _assetSceneGizmo.GetGizmos();

        //Draw Infos of gizmos in the EditorWindow
        GUILayout.Label("Gizmo Editor", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Text");
        for( int i = 0; i < _gizmos.Length; i++)
        {
            if(i == _indexGizmoEditClicked && _isEditClicked)
            {
                GUI.color = Color.red;
            }
            else
            {
                GUI.color = Color.white;
            }
            EditorGUILayout.BeginHorizontal();
            _gizmos[i].Name = EditorGUILayout.TextField(_gizmos[i].Name);
            EditorGUILayout.Space(_spaceBetweenElements);
            _gizmos[i].Position.x = EditorGUILayout.FloatField("x", _gizmos[i].Position.x);
            EditorGUILayout.Space(_spaceBetweenElements);
            _gizmos[i].Position.y = EditorGUILayout.FloatField("y", _gizmos[i].Position.y);
            EditorGUILayout.Space(_spaceBetweenElements);
            _gizmos[i].Position.z = EditorGUILayout.FloatField("z", _gizmos[i].Position.z);
            EditorGUILayout.Space(_spaceBetweenElements);
            if (GUILayout.Button("Edit"))
            {
                _indexGizmoEditClicked = i;
                _isEditClicked = !_isEditClicked;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    //Repaint every frame even if it doesn't have to focus (to perform well when we change position on sceneView or when we delete a gizmo)
    private void Update()
    {
        Repaint();
    }
}
