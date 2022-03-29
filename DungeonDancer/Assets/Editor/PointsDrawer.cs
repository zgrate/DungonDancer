using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyScript))]
public class PointsDrawer : Editor
{

    private void OnSceneGUI()
    {
        var script = (EnemyScript)target;
        //Debug.Log(script.points);
        foreach (var point in script.points)
        {
        //    Debug.Log((point));
            Handles.Label((point), point.ToString());
        }
    }

}
