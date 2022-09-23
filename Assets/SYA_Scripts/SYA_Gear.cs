using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_Gear : MonoBehaviourPun
{
    //빙글빙글 돌아간다
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
        //플레이어 데미지 점프
        if (collision.gameObject.layer == 29)
        {
            //데미지 함수
            collision.GetComponentInChildren<SY_HpBar>().HandleHp();
            //온데미지 점프함수
            collision.GetComponent<SY_PlayerMove>().Jump();

        }
    }
}
