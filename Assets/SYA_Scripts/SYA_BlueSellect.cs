using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SYA_BlueSellect : MonoBehaviourPun
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(cl);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void cl()
    {
        photonView.RPC("RpcCardSellectBlue", RpcTarget.MasterClient);
    }
     
    [PunRPC]
    void RpcCardSellectBlue()
    {
        GameManager.Instance.CardSellectBlue();
    }
}
