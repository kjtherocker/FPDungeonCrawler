using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GridFormations))]
public class GridEditor : Editor
{

    public int X;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridFormations myScript = (GridFormations)target;
        if (GUILayout.Button("Build Grid"))
        {
            myScript.CreateGrid();
        }
    }
}
#endif