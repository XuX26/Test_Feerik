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
        // Window fields
        [SerializeField] private SceneGizmoAsset _gizmoAsset;
        
        
        // Window Creation
        #region WindowCreation
        private const string _windowName = "Gizmo Editor";

        [MenuItem("UlysseTools/Windows/" + _windowName)]
        public static void ShowWindow(SceneGizmoAsset gizmoAsset = null)
        {
            GizmoWindow window = GetWindow<GizmoWindow>(_windowName);
            if (gizmoAsset)
            {
                window._gizmoAsset = gizmoAsset;
                Debug.Log(window._gizmoAsset.ToString());
            }
        }
        #endregion
        
        // Window Display 
        void OnGUI()
        {

        }
    }
#endif
}