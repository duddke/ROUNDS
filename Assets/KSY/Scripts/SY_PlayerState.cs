using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerState : MonoBehaviourPun
{
    //플레이어 상태 정의
    public enum State
    {
        IDLE,
        MOVE,
        DIE
    }

    //현재 상태
    public State currState;
    //Animator
    public Animator anim;

    //DIE 상태일때 비활성화 되어야하는 게임오브젝트들
    public GameObject[] disableGo;
    //DIE 상태일때 비활성화 되어야하는 컴포넌트들
    public MonoBehaviour[] disableCom;


    //상태 변경
    public void ChangeState(State s)
    {
        photonView.RPC("RpcChangeState", RpcTarget.All, s);
    }

    [PunRPC]
    public void RpcChangeState(State s)
    {
        //현재 상태가 s와 같다면 함수를 나간다.
        if (currState == s) return;

        //현재 상태를 s로 셋팅
        currState = s;

        //s에 따른 animation 플레이
        switch (s)
        {
            case State.IDLE:
                anim.SetTrigger("Idle");
                break;
            case State.MOVE:
                anim.SetTrigger("Move");
                break;
            case State.DIE:
                //게임오브젝트들 비활성화
                for (int i = 0; i < disableGo.Length; i++)
                    disableGo[i].SetActive(false);
                //컴포넌트들 비활성화
                for (int i = 0; i < disableCom.Length; i++)
                    disableCom[i].enabled = false;
                break;
        }
    }
}