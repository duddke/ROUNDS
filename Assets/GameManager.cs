using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
            PhotonNetwork.Instantiate("Player1", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Player2", GameObject.Find("P2_Pos").transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
