using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_MapManager : MonoBehaviourPun
{
    public List<GameObject> MAP = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            int ran = Random.Range(0, MAP.Count);
            if (ran >= 0)
            {
                PhotonNetwork.Instantiate(MAP[ran].name, Vector2.zero, Quaternion.identity);
/*                GameObject map = Instantiate(MAP[ran]);
                map.transform.position = Vector2.zero;*/
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
