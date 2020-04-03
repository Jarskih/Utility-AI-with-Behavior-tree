using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
   public int index;
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.blue;
      Gizmos.DrawCube(transform.position, Vector3.one/5);
   }
}
