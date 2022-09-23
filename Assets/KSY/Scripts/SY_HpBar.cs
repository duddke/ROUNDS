using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SY_HpBar : MonoBehaviour
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



    // nomal데미지 함수
    public void HandleHp()
    {
        curHp -= 10;
        if (curHp <= 0)
        {
            curHp = 0;
            // state => Die상태로 변경
            SY_PlayerMove.instance.state = SY_PlayerMove.State.Die;
        }
    }

    // 나의 hp 20 증가
    public bool hugeHp;
    public void HugeHp()
    {
        // hp가 증가하고 싶다.
        if (curHp <= maxHp)
        {
            curHp += 20;
        }
    }

    // poison 총알 맞으면
    // 일정시간 동안  상대방 hp감소
    public bool poisonHp;
    public void PoisonHp()
    {
        // 몇초 동안 체력을 감소하게 하고 싶다
        StopCoroutine("OnPoison");
        StartCoroutine("OnPoison");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    IEnumerator OnPoison()
    {
        float coCurrentTime = 0;
        while (coCurrentTime < 3.0f && curHp <= maxHp)
        {
            curHp -= 10;
            yield return new WaitForSeconds(0.5f);
            coCurrentTime += 0.5f;
        }
    }
}
