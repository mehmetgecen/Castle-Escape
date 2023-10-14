using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleEscape.Control
{
    public class PatrolPath : MonoBehaviour
    {
       private const float _gizmoSphereRadius = .3f;

       private void OnDrawGizmos()
       {
           for (int i = 0; i<transform.childCount; i++)
           {
               int j = GetNextIndex(i);
               
               Gizmos.color = Color.cyan;
               Gizmos.DrawSphere(GetWaypoint(i), _gizmoSphereRadius);
               Gizmos.DrawLine(GetWaypoint(i),GetWaypoint(j));
           }
       }

       public int GetNextIndex(int i)
       {
           if (i + 1 >= transform.childCount)
           {
               return 0;
           }

           return i + 1;
       }

       public Vector3 GetWaypoint(int i)
       {
           return transform.GetChild(i).position;
       }
    }
}

