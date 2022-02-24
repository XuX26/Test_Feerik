using System;
using System.Collections;
using System.Collections.Generic;
using technical.test.editor;
using UnityEngine;

[Serializable]
public class GizmoHandler
{
    public Gizmo Gizmo;
    public bool IsEditable;
    public Vector3 lastPos;
    public Vector3 currPos; // updated when edit ends
    public int controlId;

    public GizmoHandler(Gizmo _gizmo)
    {
        Gizmo = _gizmo;
        IsEditable = false;
        lastPos = _gizmo.Position;
        currPos = lastPos;
    }

    public void UpdateHandlerPositions()
    {
        // We don't want to update previous position if Gizmo didn't move
        if (currPos == Gizmo.Position) 
            return;
        lastPos = currPos;
        currPos = Gizmo.Position;
    }
}
