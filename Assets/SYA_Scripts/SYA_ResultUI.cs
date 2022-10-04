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

    public Text result;

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
        if (photonView.IsMine)
        {
            if (GameManager.Instance.gameRule == GameManager.GameRule.Duel)
            {
                photonView.RPC("resultUI", RpcTarget.All,
        GameManager.Instance.players[0].GetComponent<SY_PlayerMove>().nicName,
        GameManager.Instance.players[1].GetComponent<SY_PlayerMove>().nicName,
        GameManager.Instance.AroundWinCount, GameManager.Instance.BroundWinCount);
            }
        }
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            if (!du)
            {
                //결과 UI보여주기
                photonView.RPC("RpcDuelResultUI", RpcTarget.All, GameManager.Instance.BdieCount, GameManager.Instance.AdieCount);
                du = true;
            }
        }
        if (GameManager.Instance.gameRule == GameManager.GameRule.RoundResult)
        {
            if (!ra)
            {
                RoundResultUI();
                ra = true;
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
        if (GameManager.Instance.BdieCount >= 2)
        {
            //photonView.RPC("RpcRoundResultUIRed", RpcTarget.All, R3.rectTransform.anchoredPosition, new Vector2 (0,0),
            //R3.rectTransform.sizeDelta, new Vector2(0,0));
            photonView.RPC("RpcRoundResultUIRed", RpcTarget.All, R3.rectTransform.sizeDelta, new Vector2(600, 600));
            R3.rectTransform.anchoredPosition = Vector2.Lerp(R3.rectTransform.anchoredPosition, new Vector2(0, 0), Time.deltaTime * 300);
            B2.enabled = false;
            B1.enabled = false;
        }
        else
        {
            //photonView.RPC("RpcRoundResultUIBlue", RpcTarget.All, B3.rectTransform.anchoredPosition, new Vector2(0, 0),
            //B3.rectTransform.sizeDelta, new Vector2(0, 0));
            photonView.RPC("RpcRoundResultUIBlue", RpcTarget.All, B3.rectTransform.sizeDelta, new Vector2(600, 600));
            B3.rectTransform.anchoredPosition = Vector2.Lerp(B3.rectTransform.anchoredPosition, new Vector2(0, 0), Time.deltaTime * 300);
            R2.enabled = false;
            R1.enabled = false;
        }
    }

    bool next;
    bool nextnext;
    [PunRPC]
    void RpcRoundResultUIRed( Vector2 size, Vector2 size2)
    {
        R3.rectTransform.sizeDelta = Vector2.Lerp(size, size2, Time.deltaTime * 200);

        B2.enabled = false;
        B1.enabled = false;
    }

    [PunRPC]
    void RpcRoundResultUIBlue(Vector2 size, Vector2 size2)
    {
        B3.rectTransform.sizeDelta = Vector2.Lerp(size, size2, Time.deltaTime * 200);

        R2.enabled = false;
        R1.enabled = false;
    }

    [PunRPC]
    void resultUI(string red, string blue, int c, int d)
    {
        result.text = red + "\n"
+ c
+ "\nVS\n"
+ d + "\n"
+ blue;
    }

}
