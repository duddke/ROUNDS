using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_CardManager : MonoBehaviour
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
        redCard[0] = true;
    }
    public void OnTwoRedCard()
    {
        redCard[1] = true;
    }
    public void OnThreeRedCard()
    {
        redCard[2] = true;
    }
    public void OnFourRedCard()
    {
        redCard[3] = true;
    }
    public void OnFiveRedCard()
    {
        redCard[4] = true;
    }
    public void OnSixRedCard()
    {
        redCard[5] = true;
    }
    public void OnSevenRedCard()
    {
        redCard[6] = true;
    }
    public void OnEightRedCard()
    {
        redCard[7] = true;
    }
    public void OnNineRedCard()
    {
        redCard[8] = true;
    }
    public void OnTenRedCard()
    {
        redCard[9] = true;
    }

    public void OnOneBlueCard()
    {
        blueCard[0] = true;
    }
    public void OnTwoBlueCard()
    {
        blueCard[1] = true;
    }
    public void OnThreeBlueCard()
    {
        blueCard[2] = true;
    }
    public void OnFourBlueCard()
    {
        blueCard[3] = true;
    }
    public void OnFiveBlueCard()
    {
        blueCard[4] = true;
    }
    public void OnSixBlueCard()
    {
        blueCard[5] = true;
    }
    public void OnSevenBlueCard()
    {
        blueCard[6] = true;
    }
    public void OnEightBlueCard()
    {
        blueCard[7] = true;
    }
    public void OnNineBlueCard()
    {
        blueCard[8] = true;
    }
    public void OnTenBlueCard()
    {
        blueCard[9] = true;
    }
}
