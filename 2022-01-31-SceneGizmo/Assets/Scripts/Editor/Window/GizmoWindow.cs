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
        private int _shortWidthField = 200;

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
            EditorGUILayout.ObjectField("Gizmo Asset", _gizmoAsset, typeof(SceneGizmoAsset));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", EditorStyles.boldLabel);
            GUILayout.Label("Position", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                UpdateGizmoInWindow(ref _gizmoAsset.Gizmos[i]);
            }
        }

        void UpdateGizmoInWindow(ref Gizmo gizmo)
        {
            GUILayout.BeginHorizontal();
            gizmo.Name = EditorGUILayout.TextField(gizmo.Name, GUILayout.MaxWidth(_shortWidthField));
            GUILayout.Space(10);
            gizmo.Position = EditorGUILayout.Vector3Field("", gizmo.Position, GUILayout.MaxWidth(400));
            GUILayout.EndHorizontal();
        }
        #endregion
        
        void OnSceneGUI(SceneView sceneView)
        {
            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                DrawGizmoInSceneView(_gizmoAsset.Gizmos[i]);
            }
            Debug.Log("ONSCENEGUI!!!");
        }
        
        void DrawGizmoInSceneView(Gizmo gizmo)
        {
            Handles.Label(gizmo.Position, gizmo.Name);
        }
    }
#endif
}