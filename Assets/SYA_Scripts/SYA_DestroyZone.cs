using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Ѿ� ����
        if (collision.gameObject.layer == 30)
            Destroy(collision.gameObject);
        //�÷��̾� ������ ����
        if (collision.gameObject.layer == 29)
        {
            //������ �Լ�
            //�µ����� �����Լ�
        }
    }
}
