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
        private List<GizmoHandler> _gizmoHandlerList = new List<GizmoHandler>();
        // I tried many ways before using a GizmoHandler
        // private Dictionary<Gizmo, bool> _isEditableGizmoDic = new Dictionary<Gizmo, bool>();
        // private List<bool> _isEditableGizmoList = new List<bool>();
        
        private float gizmoSize = 0.5f;
        
        private int _shortWidthField = 200;
        private int _shortWidthButton = 75;
        private bool _isEditingAllGizmo = false;
       

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
                window.SetGizmoAsset(gizmoAsset);
            }
        }

        void SetGizmoAsset(SceneGizmoAsset newGizmoAsset)
        {
            if (_gizmoAsset != newGizmoAsset)
            {
                Debug.Log("Asset Gizmo changed");
                _gizmoAsset = newGizmoAsset;
                InitVariables();
            }
        }

        void InitVariables()
        {
            InitGizmoHandlerList();
        }

        void InitGizmoHandlerList()
        {
            if (_gizmoAsset.Gizmos.Length != _gizmoHandlerList.Count)
            {
                _gizmoHandlerList.Clear();
                for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
                {
                    _gizmoHandlerList.Add(new GizmoHandler(_gizmoAsset.Gizmos[i]));
                }
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
            SceneGizmoAsset tmpGizmoAsset = EditorGUILayout.ObjectField("Gizmo Asset", _gizmoAsset, typeof(SceneGizmoAsset)) as SceneGizmoAsset;
            if (!tmpGizmoAsset) return;
            if (_gizmoAsset != tmpGizmoAsset)
            {
                SetGizmoAsset(tmpGizmoAsset);
            }
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", EditorStyles.boldLabel, GUILayout.Width(_shortWidthField));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Position", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            UpdateEditAllButton();
            GUILayout.EndHorizontal();

            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                UpdateGizmoInWindow(ref _gizmoAsset.Gizmos[i], i);
            }
            Repaint();
        }

        void UpdateEditAllButton()
        {
            _isEditingAllGizmo = GUILayout.Toggle(_isEditingAllGizmo, "Edit All", "Button",
                GUILayout.Width(_shortWidthButton));
            if (GUI.changed)
            {
                if (_gizmoAsset.Gizmos.Length != _gizmoHandlerList.Count)
                    InitGizmoHandlerList();
                
                for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
                {
                    _gizmoHandlerList[i].IsEditable = _isEditingAllGizmo;
                    //_gizmoHandlerList.Add(new GizmoHandler(_gizmoAsset.Gizmos[i]));
                }
            }
        }

        void UpdateGizmoInWindow(ref Gizmo gizmo, int index)
        {
            EditorStyles.textField.normal.textColor = _gizmoHandlerList[index].IsEditable ? Color.red : Color.white;
            GUILayout.BeginHorizontal();
            
            gizmo.Name = EditorGUILayout.TextField(gizmo.Name,GUILayout.Width(_shortWidthField));
            GUILayout.Space(10);
            
            GUILayout.FlexibleSpace();
            gizmo.Position = EditorGUILayout.Vector3Field("", gizmo.Position, GUILayout.MaxWidth(400));
            GUILayout.FlexibleSpace();
            
            UpdateEditButton(index);
            GUILayout.EndHorizontal();
        }
        
        void UpdateEditButton(int index)
        {
            _gizmoHandlerList[index].IsEditable = GUILayout.Toggle(_gizmoHandlerList[index].IsEditable, "Edit", "Button", GUILayout.Width(_shortWidthButton));

            if (GUI.changed)
            {
                _gizmoHandlerList[index].UpdateHandlerPositions();
            }
        }
        #endregion
        
        // --- Scene Display
        #region Scene Display
        void OnSceneGUI(SceneView sceneView)
        {
            if (!_gizmoAsset) return;
            
            EditorGUI.BeginChangeCheck();
                
            for (int i = 0; i < _gizmoAsset.Gizmos.Length; i++)
            {
                _gizmoHandlerList[i].controlId = GUIUtility.GetControlID(FocusType.Passive);
                Handles.Label(_gizmoAsset.Gizmos[i].Position + Vector3.up, _gizmoAsset.Gizmos[i].Name);
                Handles.DrawLine(_gizmoAsset.Gizmos[i].Position + Vector3.up * 0.8f, _gizmoAsset.Gizmos[i].Position);
                DrawGizmoSphereInSceneView(_gizmoAsset.Gizmos[i], i);
                //if (_isEditableGizmoDic.TryGetValue(_gizmoAsset.Gizmos[i], out bool isEditable) && isEditable)
                //if (_isEditableGizmoList[i])
                if (_gizmoHandlerList[i].IsEditable)
                        ActiveGizmoEditMode(ref _gizmoAsset.Gizmos[i]);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_gizmoAsset, "_gizmoAsset changed");
            }

            sceneView.Repaint();
        }

        void DrawGizmoSphereInSceneView(Gizmo gizmo, int index)
        {
            Handles.color = Color.white;
            Handles.SphereHandleCap(index, gizmo.Position, Quaternion.identity, gizmoSize/3, EventType.Repaint);
        }
        
        void ActiveGizmoEditMode(ref Gizmo gizmo)
        {
            gizmo.Position = Handles.PositionHandle(gizmo.Position, Quaternion.identity);
        }
        #endregion

    }
#endif
}