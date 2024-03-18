using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone_Move : MonoBehaviour
{
    private Rigidbody2D rigid;
    private AudioSource audioSource;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        rigid.AddForce(-transform.right * 2000 * Time.deltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 5f); // 5초 뒤 삭제
    }

}
