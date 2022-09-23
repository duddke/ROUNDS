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
                PhotonNetwork.Instantiate(MAP[ran].name, Vector2.zero, Quaternion.identity);
/*                GameObject map = Instantiate(MAP[ran]);
                map.transform.position = Vector2.zero;*/
        }

    }

    private void Start()
    {
        print("2222222222222222222");
    }

    GameObject go1;
    // Update is called once per frame
    void Update()
    {
        if (go1 != null) return;
        go1 = GameObject.Find("P1_Pos");
        if(go1 != null) StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        yield return new WaitForSeconds(3);
        if (photonView.IsMine)
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Blue_Player", GameObject.Find("P2_Pos").transform.position, Quaternion.identity);
    }
}
