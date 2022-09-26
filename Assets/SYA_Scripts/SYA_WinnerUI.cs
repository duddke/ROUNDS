using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SYA_WinnerUI : MonoBehaviourPun
{
    public Text win;
    // Start is called before the first frame update
    void Start()
    {
        win.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            if (!wins)
            {
                winReUI();
            }
        }
    }
    bool wins;

    void winReUI()
    {
        photonView.RPC("RpcwinReUI", RpcTarget.All, GameManager.Instance.winner);
        wins = true;
    }

    [PunRPC]
    void RpcwinReUI(string a)
    {
        win.text = a + "½Â¸®!!";
        win.enabled = true;
    }

}
