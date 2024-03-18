using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Move : MonoBehaviour
{
    public Rigidbody2D rigid;

    void Start()
    {
        rigid.AddForce(transform.right * 3000 * Time.deltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 2.0f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") // 적과 충돌 시
        {
            Destroy(gameObject); // 총알 사라짐
        }
    }

}
