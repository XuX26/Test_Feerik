using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using technical.test.editor;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rendu.Ulysse.editor
{
#if UNITY_EDITOR
    public class GizmoWindow : EditorWindow
    {
        #region WindowCreation
        private const string _windowName = "Gizmo Editor";

        [MenuItem("UlysseTools/Windows/" + _windowName)]
        public static void ShowWindow()
        {
            GetWindow<GizmoWindow>(_windowName);
        }
        #endregion
        
        // Window fields
        [SerializeField] private SceneGizmoAsset GizmoAsset;
        
        // Window Display 
        void OnGUI()
        {

        }
    }
#endif
}