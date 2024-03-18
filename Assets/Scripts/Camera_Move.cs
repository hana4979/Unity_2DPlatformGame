using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform playerPos;
    Vector3 target;

    void Start()
    {
        // playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        target = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z - 10);

        transform.position = Vector3.Lerp(transform.position, target, 0.02f);
    }
}
