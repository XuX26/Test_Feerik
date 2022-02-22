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
        [SerializeField] private SceneGizmoAsset _gizmoAsset;

        private int _shortWidthField = 200;
        
        // --- Window Creation
        #region WindowCreation
        private const string _windowName = "Gizmo Editor";

        [MenuItem("UlysseTools/Windows/" + _windowName)]
        public static void ShowWindow()
        {
            GizmoWindow window = GetWindow<GizmoWindow>(_windowName);
        }
        
        [MenuItem("UlysseTools/Windows/" + _windowName)]
        public static void ShowWindow(SceneGizmoAsset gizmoAsset)
        {
            GizmoWindow window = GetWindow<GizmoWindow>(_windowName);
            if (gizmoAsset)
            {
                window._gizmoAsset = gizmoAsset;
                Debug.Log(window._gizmoAsset.ToString());
            }
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
            
            foreach (Gizmo gizmo in _gizmoAsset.Gizmos)
            {
                DisplayGizmos(gizmo);
            }
        }

        void DisplayGizmos(Gizmo gizmo)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField(gizmo.Name, GUILayout.MaxWidth(_shortWidthField));
            GUILayout.Space(10);
            EditorGUILayout.Vector3Field("", gizmo.Position, GUILayout.MaxWidth(400));
            GUILayout.EndHorizontal();
        }
        
        #endregion
    }
#endif
}