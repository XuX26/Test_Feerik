using System.Collections;
using System.Collections.Generic;
using technical.test.editor;
using UnityEditor;
using UnityEngine;

namespace Rendu.Ulysse.editor
{
    [CustomEditor(typeof(SceneGizmoAsset))]
    //[CanEditMultipleObjects]
    public class GizmosInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Gizmo Window"))
            {
                GizmoWindow.ShowWindow();
            }
            base.OnInspectorGUI();
        }
    }
}