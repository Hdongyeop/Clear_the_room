using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        Vector3 Pos = target.position;
        Pos.Set(target.position.x, target.position.y + 1.5f, transform.position.z);
        cam.transform.SetPositionAndRotation(Pos, Quaternion.identity);
    }
}
