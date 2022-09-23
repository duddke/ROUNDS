using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_Gear : MonoBehaviourPun
{
    //���ۺ��� ���ư���
    public float rotSpeed = 300;
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        time = Time.deltaTime * rotSpeed;
        transform.Rotate(transform.forward*time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        //�÷��̾� ������ ����
        if (collision.gameObject.layer == 29)
        {
            //������ �Լ�
            collision.GetComponentInChildren<SY_HpBar>().HandleHp();
            //�µ����� �����Լ�
            collision.GetComponent<SY_PlayerMove>().Jump();

        }
    }
}
