using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_Pnic : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.p.Add(photonView);
        photonView.transform.parent=GameObject.Find("GameManager").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
