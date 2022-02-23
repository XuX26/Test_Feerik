using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using technical.test.editor;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;
#endif
 
namespace Rendu.Ulysse.editor
{
#if UNITY_EDITOR
    public class GizmoWindow : EditorWindow
    {
        // --- Window fields
        public static GizmoWindow Instance { get; private set; }
        public static bool IsOpen => Instance != null;
        
        [SerializeField] private SceneGizmoAsset _gizmoAsset;
        private float gizmoSize = 0.5f;
        
        private int _shortWidthField = 200;
        private int _shortWidthButton = 75;
        private bool _isEditingAllGizmo = false;
        private Dictionary<Gizmo, bool> _isEditableGizmoDic = new Dictionary<Gizmo, bool>();

        // --- Window Creation
        #region WindowCreation
        private const string _windowName = "Gizmo Editor";

        [MenuItem("Window/Custom/" + _windowName)]
        public static void ShowWindow()
        {
            GizmoWindow window = GetWindow<GizmoWindow>(_windowName);
            Instance = window;
        }
        
        [MenuItem("Window/Custom/" + _windowName)]
        public static void ShowWindow(SceneGizmoAsset gizmoAsset)
        {
            GizmoWindow window = GetWindow<GizmoWindow>(_windowName);
            if (gizmoAsset)
            {
                window._gizmoAsset = gizmoAsset;
                Debug.Log(window._gizmoAsset.ToString());
            }
        }
        
        void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
            Debug.Log("GizmoWindow created!");
        }
        
        void OnDestroy()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            Debug.Log("GizmoWindow destroyed!");
        }
        #endregion
        
        // --- Window Display 
        #region Window Display
        void OnGUI()
        {
            _gizmoAsset = EditorGUILayout.ObjectField("Gizmo Asset", _gizmoAsset, typeof(SceneGizmoAsset)) as SceneGizmoAsset;
            if (_gizmoAsset == null)
                return;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", EditorStyles.boldLabel, GUILayout.Width(_shortWidthField));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Position", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            UpdateEditAllButton();
            GUILayout.EndHorizontal();

            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                UpdateGizmoInWindow(ref _gizmoAsset.Gizmos[i]);
            }
        }

        void UpdateEditAllButton()
        {
            _isEditingAllGizmo = GUILayout.Toggle(_isEditingAllGizmo, "Edit All", "Button",
                GUILayout.Width(_shortWidthButton));
            if (GUI.changed)
            {
                for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
                {
                    if (_isEditableGizmoDic.ContainsKey(_gizmoAsset.Gizmos[i]))
                    {
                        _isEditableGizmoDic[_gizmoAsset.Gizmos[i]] = _isEditingAllGizmo;
                    }
                }
            }
        }


        void UpdateGizmoInWindow(ref Gizmo gizmo)
        {
            GUILayout.BeginHorizontal();
            gizmo.Name = EditorGUILayout.TextField(gizmo.Name, GUILayout.Width(_shortWidthField));
            GUILayout.Space(10);
            GUILayout.FlexibleSpace();
            gizmo.Position = EditorGUILayout.Vector3Field("", gizmo.Position, GUILayout.MaxWidth(400));
            GUILayout.FlexibleSpace();
            UpdateEditButton(ref gizmo);
            //_isEditableGizmoDic[gizmo] = GUILayout.Toggle(_isEditableGizmoDic[gizmo], "Edit", "Button", GUILayout.Width(_shortWidthButton));
            GUILayout.EndHorizontal();
        }

        void UpdateEditButton(ref Gizmo gizmo)
        {
            if (!_isEditableGizmoDic.ContainsKey(gizmo))
            {
                _isEditableGizmoDic.Add(gizmo, false);
            }
            _isEditableGizmoDic[gizmo] = GUILayout.Toggle(_isEditableGizmoDic[gizmo], "Edit", "Button", GUILayout.Width(_shortWidthButton));
        }
        #endregion
        
        void OnSceneGUI(SceneView sceneView)
        {
            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                Handles.Label(_gizmoAsset.Gizmos[i].Position, _gizmoAsset.Gizmos[i].Name);
                DrawGizmoSphereInSceneView(_gizmoAsset.Gizmos[i]);
                if (_isEditableGizmoDic[_gizmoAsset.Gizmos[i]])
                    ActiveGizmoEditMode(ref _gizmoAsset.Gizmos[i]);
            }
            Debug.Log("ONSCENEGUI!!!");
            sceneView.Repaint();
        }

        void DrawGizmoSphereInSceneView(Gizmo gizmo)
        {
            Handles.color = Color.white;
            Handles.SphereHandleCap(1, gizmo.Position, Quaternion.identity, gizmoSize/3, EventType.Repaint);
        }
        
        void ActiveGizmoEditMode(ref Gizmo gizmo)
        {
            gizmo.Position = Handles.PositionHandle(gizmo.Position, Quaternion.identity);
        }
    }
#endif
}