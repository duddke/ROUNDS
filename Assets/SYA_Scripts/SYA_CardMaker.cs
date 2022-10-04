using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_CardMaker : MonoBehaviourPun
{
    public List<GameObject> card = new List<GameObject>();
    public List<RectTransform> cardTr = new List<RectTransform>();
    public Transform tr;

        int exran = 0;
        int countMax = 5;
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool end;
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (end) return;
            for (int i = 0; i < countMax; i++)
            {
                int ran = Random.Range(0, card.Count);
                if (exran == ran)
                {
                    i--;
                    continue;
                }
                exran = ran;
                PhotonNetwork.Instantiate(card[ran].name, cardTr[i].position, cardTr[i].rotation);
                //GameObject map = Instantiate(card[ran], tr);
                //map.transform.position = cardTr[i].position;
                if (i < countMax)
                    end = true;
            }
        }
    }
}
