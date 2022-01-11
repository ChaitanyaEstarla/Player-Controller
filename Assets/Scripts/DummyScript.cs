using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube( Vector3.zero, Vector3.one);
        Gizmos.color = Color.blue;
    }
}
