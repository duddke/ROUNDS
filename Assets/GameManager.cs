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
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
        {
            

        }
    }

    // Update is called once per frame
    GameObject map;
    void Update()
    {
        if(!photonView.IsMine && map == null)
        {
            map = GameObject.Find("P2_Pos");
            photonView.RPC("CreatePlayer", RpcTarget.All, map);
        }
    }

    [PunRPC]
    void CreatePlayer(Vector3 map)
    {
        PhotonNetwork.Instantiate("Blue_Player", map, Quaternion.identity);
    }
}
