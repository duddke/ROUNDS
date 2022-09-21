using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_RedSellect : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(GameManager.Instance.CardSellectRed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnclickF()
    {
        GameManager.Instance.CardSellectRed();
    }*/
    
}
