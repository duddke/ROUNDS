using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SY_HpBar : MonoBehaviourPun
{

    [SerializeField]
    public Slider hpbar;
    public float maxHp = 100;
    public int curHp = 100;

    float currentTime;
    public float creatTime = 5f;

    public static SY_HpBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpbar.value = (float)curHp / (float)maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.value = (float)curHp / (float)maxHp;
        curHp = Math.Clamp(curHp, 0, 100);
    }

    // nomal������ �Լ�
    public void HandleHp()
    {
        photonView.RPC("RpcHandleHp", RpcTarget.All);
    }
    [PunRPC]
    void RpcHandleHp()
    {
        curHp -= 10;
        if (curHp <= 0)
        {
            curHp = 0;
            // state => Die���·� ����
            GetComponentInParent<SY_PlayerMove>().state = SY_PlayerMove.State.Die;
        }
    }

    // ���� hp 20 ����
    public bool hugeHp;
    public void HugeHp()
    {
        photonView.RPC("RpcHugeHp", RpcTarget.All);
    }
    [PunRPC]
    void RpcHugeHp()
    {
        // hp�� �����ϰ� �ʹ�.
        if (curHp <= maxHp)
        {
            curHp += 20;
        }
    }

    // hp ���� ����
    public void Brawler() //0928
    {
        //curHp += 10;
        StopCoroutine("OnBrawler");
        StartCoroutine("OnBrawler");
    }
    [PunRPC]
    void RpcOnBrawler()
    {
        Debug.Log("rpconbrawler");
        curHp += 3;

        if (curHp <= 0)
        {
            curHp = 0;
            // state => Die���·� ����
            GetComponentInParent<SY_PlayerMove>().state = SY_PlayerMove.State.Die;
        }
    }
    // �� Hp ���� ����.  //0928
    IEnumerator OnBrawler()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 3.0f && curHp <= maxHp)
        {
            photonView.RPC("RpcOnBrawler", RpcTarget.All);
            yield return new WaitForSeconds(0.5f);
            coCurrentTime += 0.5f;
        }
    }


    // poison �Ѿ� ������
    // �����ð� ����  ���� hp����
    public bool poisonHp;
    public void PoisonHp()
    {
        // ���� ���� ü���� �����ϰ� �ϰ� �ʹ�
        StopCoroutine("OnPoison");
        StartCoroutine("OnPoison");
    }
    [PunRPC]
    void RpcOnPoison()
    {
        curHp -= 3;

        // hp�� 0�̸� Die���·� ��ȯ  //0918
        if (curHp <= 0)
        {
            curHp = 0;
            // state => Die���·� ����
            GetComponentInParent<SY_PlayerMove>().state = SY_PlayerMove.State.Die;
        }
    }
    // ��ħ
    IEnumerator OnPoison()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 3.0f && curHp <= maxHp)
        {
            photonView.RPC("RpcOnPoison", RpcTarget.All);
            yield return new WaitForSeconds(0.5f);
            coCurrentTime += 0.5f;
        }
    }

    public bool bigBullet;
    public void BigBullet()
    {
        
        if (curHp <= maxHp)
        {
            curHp -= 20;
        }
    }
    
    public void Cannon() //0928
    {
        if (curHp <= maxHp)
        {
            curHp -= curHp/2; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
   

   

    

    
}
