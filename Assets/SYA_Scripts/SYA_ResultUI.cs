using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SYA_ResultUI : MonoBehaviourPun
{
    //결투결과 UI
    public Text duelResult;
    //라운드결과 UI
    public Text roundResult;
    //배경지
    public Image bgUI;

    public Image B1;
    public Image B2;
    public Image B3;

    public Image R1;
    public Image R2;
    public Image R3;

    // Start is called before the first frame update
    void Start()
    {
    }
    public float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        //알피시
        if (!photonView.IsMine) return;
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            if (!du)
            {
                //결과 UI보여주기
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
        bgUI.enabled = true;
        if (a == 0)
            R1.enabled = true;
        else if (a == 1)
            R2.enabled = true;
        else if (a == 2)
            R3.enabled = true;
        if (b == 0)
            B1.enabled = true;
        else if (b == 1)
            B2.enabled = true;
        else if (b == 2)
            B3.enabled = true;
    }

    void RoundResultUI()
    {
        photonView.RPC("RpcRoundResultUI", RpcTarget.All, GameManager.Instance.AroundWinCount, GameManager.Instance.BroundWinCount);
        ra = false;
    }

    [PunRPC]
    void RpcRoundResultUI(int a, int b)
    {
        if(a==2)
        {
            R3.rectTransform.position = Vector3.Lerp(R3.rectTransform.position, new Vector3(0, 0, 0), Time.deltaTime * 50 );
            R3.rectTransform.sizeDelta = Vector2.Lerp(R3.rectTransform.sizeDelta, new Vector2(600, 600), Time.deltaTime * 50f);
        }
        else
        {
            B3.rectTransform.position = Vector3.Lerp(B3.rectTransform.position, new Vector3(0, 0, 0), Time.deltaTime * 50f);
            B3.rectTransform.sizeDelta = Vector2.Lerp(B3.rectTransform.sizeDelta, new Vector2(600, 600), Time.deltaTime * 50f);
        }
    }
}
