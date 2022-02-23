using System.Collections;
using System.Collections.Generic;
using technical.test.editor;
using UnityEditor;
using UnityEngine;

namespace Rendu.Ulysse.editor
{
    [CustomEditor(typeof(SceneGizmoAsset))]
    public class GizmoAssetInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Gizmo Window"))
            {
                GizmoWindow.ShowWindow((SceneGizmoAsset)this.target);
            }
            base.OnInspectorGUI();
        }
    }
}