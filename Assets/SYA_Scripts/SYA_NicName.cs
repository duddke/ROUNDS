using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_NicName : MonoBehaviour
{
    public Text userId;
    private PhotonView pv = null;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        userId.text = GetComponentInParent<SY_PlayerMove>().nicName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
