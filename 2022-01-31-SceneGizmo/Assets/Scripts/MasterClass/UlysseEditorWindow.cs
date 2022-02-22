using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Rendu.Ulysse.MasterClass
{
    // I like to use masterClasses, but I didn't figured out how to made this one.
    // I must use a const inside my attribute, so I can't init it in ma derived classes
    // Maybe another time for this class, now I'm just gonna create a ShowWindow method inside the window classes.
    // My purpose was to write a clean code with well architecture even if i have only one Window to create
    
    // public class UlysseEditorWindow : EditorWindow
    // {
    //     const string _windowName;
    //     
    //     [MenuItem("UlysseTools/Windows/" + _windowName)]
    //     public void ShowWindow(UlysseEditorWindow windowType)
    //     {
    //         EditorWindow.GetWindow<windowType.type>("Gizmo Editor");
    //     }
    // }
}