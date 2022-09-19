using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //총알 삭제
        if (collision.gameObject.layer == 30)
            Destroy(collision.gameObject);
        //플레이어 데미지 점프
        if (collision.gameObject.layer == 29)
        {
            //데미지 함수
            //온데미지 점프함수
        }
    }
}
