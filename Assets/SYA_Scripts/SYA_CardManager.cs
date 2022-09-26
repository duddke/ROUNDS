using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_CardManager : MonoBehaviourPun
{
    public static SYA_CardManager Instance;

    private void Awake()
    {
        //if (!photonView.IsMine) return;
        if (Instance == null)
        {
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            Destroy(gameObject);
        }
    }


    public bool[] redCard = new bool[10];
    public bool[] blueCard = new bool[10];
    
    public void OnOneRedCard()
    {
        photonView.RPC("RpcOnOneRedCard", RpcTarget.All);
    }
    [PunRPC]
    void RpcOnOneRedCard()
    {
        redCard[0] = true;
    }

    public void OnTwoRedCard()
    {
        photonView.RPC("RpcOnTwoRedCard", RpcTarget.All);
    }
    [PunRPC]
    void RpcOnTwoRedCard()
    {
        redCard[1] = true;
    }

    public void OnThreeRedCard()
    {
        photonView.RPC("RpcOnThreeRedCard", RpcTarget.All);
    }
    [PunRPC]
    void RpcOnThreeRedCard()
    {
        redCard[2] = true;
    }

    public void OnFourRedCard()
    {
        photonView.RPC("RpcOnFourRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnFourRedCard()
    {
        redCard[3] = true;
    }

    public void OnFiveRedCard()
    {
        photonView.RPC("RpcOnFiveRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnFiveRedCard()
    {
        redCard[4] = true;
    }

    public void OnSixRedCard()
    {
        photonView.RPC("RpcOnSixRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnSixRedCard()
    {
        redCard[5] = true;
    }

    public void OnSevenRedCard()
    {
        photonView.RPC("RpcOnSevenRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnSevenRedCard()
    {
        redCard[6] = true;
    }

    public void OnEightRedCard()
    {
        photonView.RPC("RpcOnEightRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnEightRedCard()
    {
        redCard[7] = true;
    }

    public void OnNineRedCard()
    {
        photonView.RPC("RpcOnNineRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnNineRedCard()
    {
        redCard[8] = true;
    }

    public void OnTenRedCard()
    {
        photonView.RPC("RpcOnTenRedCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnTenRedCard()
    {
        redCard[9] = true;
    }

    public void OnOneBlueCard()
    {
        photonView.RPC("RpcOnOneBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnOneBlueCard()
    {
        blueCard[0] = true;
    }

    public void OnTwoBlueCard()
    {
        photonView.RPC("RpcOnTwoBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnTwoBlueCard()
    {
        blueCard[1] = true;
    }

    public void OnThreeBlueCard()
    {
        photonView.RPC("RpcOnThreeBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnThreeBlueCard()
    {
        blueCard[2] = true;
    }

    public void OnFourBlueCard()
    {
        photonView.RPC("RpcOnFourBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnFourBlueCard()
    {
        blueCard[3] = true;
    }

    public void OnFiveBlueCard()
    {
        photonView.RPC("RpcOnFiveBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnFiveBlueCard()
    {
        blueCard[4] = true;
    }

    public void OnSixBlueCard()
    {
        photonView.RPC("RpcOnSixBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnSixBlueCard()
    {
        blueCard[5] = true;
    }

    public void OnSevenBlueCard()
    {
        photonView.RPC("RpcOnSevenBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnSevenBlueCard()
    {
        blueCard[6] = true;
    }

    public void OnEightBlueCard()
    {
        photonView.RPC("RpcOnEightBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnEightBlueCard()
    {
        blueCard[7] = true;
    }

    public void OnNineBlueCard()
    {
        photonView.RPC("RpcOnNineBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnNineBlueCard()
    {
        blueCard[8] = true;
    }

    public void OnTenBlueCard()
    {
        photonView.RPC("RpcOnTenBlueCard", RpcTarget.All);
    }
    [PunRPC]
    public void RpcOnTenBlueCard()
    {
        blueCard[9] = true;
    }

}
