using System;
using System.Collections;
using System.Collections.Generic;
using CastleEscape.Control;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIController))]
public class AiControllerEditor : Editor
{
    private void OnSceneGUI()
    {
        AIController _aiController = (AIController) target;
        
        Color c = Color.green;

        if (_aiController.alertStage == AlertStage.Suspicious)
        {
             c = Color.Lerp(Color.green,Color.red, _aiController.alertLevel / 100f);
        }
        else if (_aiController.alertStage == AlertStage.Alerted)
        {
            c = Color.red;
        }
        
        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(_aiController.transform.position,
            _aiController.transform.up, 
            Quaternion.AngleAxis(-_aiController.fieldOfViewAngle / 2f,_aiController.transform.up) * _aiController.transform.forward,
            _aiController.fieldOfViewAngle,
            _aiController.fieldOfView);

        Handles.color = c;
        _aiController.fieldOfView = Handles.ScaleValueHandle(_aiController.fieldOfView, 
            _aiController.transform.position + _aiController.transform.forward * _aiController.fieldOfView,
            _aiController.transform.rotation,
            3f,
            Handles.ConeHandleCap,
            1f);
    }

    
}
