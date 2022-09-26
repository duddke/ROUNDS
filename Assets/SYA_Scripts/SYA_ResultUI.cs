using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SYA_ResultUI : MonoBehaviourPun
{
    //������� UI
    public Text duelResult;
    //������ UI
    public Text roundResult;

    // Start is called before the first frame update
    void Start()
    {
    }
    public float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        //���ǽ�
        if (!photonView.IsMine) return;
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            if (!du)
            {
                //��� UI�����ֱ�
                photonView.RPC("RpcDuelResultUI", RpcTarget.All, GameManager.Instance.BdieCount, GameManager.Instance.AdieCount);
                du = false;
            }
        }
        if (GameManager.Instance.gameRule == GameManager.GameRule.RoundResult)
        {
            if (!ra)
            {
                photonView.RPC("RpcRoundResultUI", RpcTarget.All, GameManager.Instance.AroundWinCount, GameManager.Instance.BroundWinCount);
                ra = false;
            }
        }
    }
    bool du;
    bool ra;

    void DuelResultUI()
    {
        photonView.RPC("RpcDuelResultUI", RpcTarget.All, GameManager.Instance.BdieCount, GameManager.Instance.AdieCount);
        du = false;
    }

    [PunRPC]
    void RpcDuelResultUI(int a,int b)
    {
        duelResult.text = "���� Ƚ�� : A " + a + " : B " + b;
        duelResult.enabled = true;
    }

    void RoundResultUI()
    {
        photonView.RPC("RpcRoundResultUI", RpcTarget.All, GameManager.Instance.AroundWinCount, GameManager.Instance.BroundWinCount);
        ra = false;
    }

    [PunRPC]
    void RpcRoundResultUI(int a, int b)
    {
        duelResult.enabled = false;
        //��� UI�����ֱ�
        roundResult.text = "�̱� Ƚ�� : A " + a + " : B " + b;
        roundResult.enabled = true;
        ra = true;
        currentTime += Time.deltaTime;
        if (currentTime > 0.3f)
        {
            currentTime = 0;
            //UI�����
            roundResult.enabled = false;
        }
    }
}
