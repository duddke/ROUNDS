using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SYA_CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 sca;
    Vector3 change;

    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        change = new Vector3(1.5f, 1.5f, 1.5f);
        sca = transform.localScale;
        go = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = change;
        go.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = sca;
        go.SetActive(true);
    }

    public void OnClick()
    {
        //반영할 거
        ChangeCard(gameObject.name);
    }

    void ChangeCard(string cardname)
    {
        if (cardname.Contains("Red"))
        {
            if (cardname.Contains("0"))
                SYA_CardManager.Instance.OnOneRedCard();
            else if(cardname.Contains("1"))
                SYA_CardManager.Instance.OnTwoRedCard();
            else if (cardname.Contains("2"))
                SYA_CardManager.Instance.OnThreeRedCard();
            else if (cardname.Contains("3"))
                SYA_CardManager.Instance.OnFourRedCard();
            else if (cardname.Contains("4"))
                SYA_CardManager.Instance.OnFiveRedCard();
            else if (cardname.Contains("5"))
                SYA_CardManager.Instance.OnSixRedCard();
            else if (cardname.Contains("6"))
                SYA_CardManager.Instance.OnSevenRedCard();
            else if (cardname.Contains("7"))
                SYA_CardManager.Instance.OnEightRedCard();
            else if (cardname.Contains("8"))
                SYA_CardManager.Instance.OnNineRedCard();
            else if (cardname.Contains("9"))
                SYA_CardManager.Instance.OnTenRedCard();
        }
        else if (cardname.Contains("Blue"))
        {
            if (cardname.Contains("0"))
                SYA_CardManager.Instance.OnOneBlueCard();
            else if (cardname.Contains("1"))
                SYA_CardManager.Instance.OnTwoBlueCard();
            else if (cardname.Contains("2"))
                SYA_CardManager.Instance.OnThreeBlueCard();
            else if (cardname.Contains("3"))
                SYA_CardManager.Instance.OnFourBlueCard();
            else if (cardname.Contains("4"))
                SYA_CardManager.Instance.OnFiveBlueCard();
            else if (cardname.Contains("5"))
                SYA_CardManager.Instance.OnSixBlueCard();
            else if (cardname.Contains("6"))
                SYA_CardManager.Instance.OnSevenBlueCard();
            else if (cardname.Contains("7"))
                SYA_CardManager.Instance.OnEightBlueCard();
            else if (cardname.Contains("8"))
                SYA_CardManager.Instance.OnNineBlueCard();
            else if (cardname.Contains("9"))
                SYA_CardManager.Instance.OnTenBlueCard();
        }
    }
}
