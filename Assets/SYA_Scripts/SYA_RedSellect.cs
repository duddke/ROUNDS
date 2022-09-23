using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SYA_RedSellect : MonoBehaviourPun
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
        photonView.RPC("RpcCardSellectRed", RpcTarget.MasterClient);
    }

    [PunRPC]
    void RpcCardSellectRed()
    {
        GameManager.Instance.CardSellectRed();
    }

}
