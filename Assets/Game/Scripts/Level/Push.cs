using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private Vector3 dir1, dir2;
    private Vector3 collisionDirection;
    private void Start()
    {
        dir1 = transform.TransformDirection(Vector3.back);
        dir2 = transform.TransformDirection(Vector3.left);
    }

    public Vector3 GetDirection1() => dir1;
    public Vector3 GetDirection2() => dir2;
}
