using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ResultUI : MonoBehaviour
{
    //결투결과 UI
    public Text duelResult;
    //라운드결과 UI
    public Text roundResult;

    // Start is called before the first frame update
    void Start()
    {
    }
    public float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            //결과 UI보여주기
            duelResult.text = "죽인 횟수 : A " + GameManager.Instance.BdieCount + " : B " + GameManager.Instance.AdieCount;
            duelResult.enabled = true;
            currentTime += Time.deltaTime;
            if (currentTime > 0.5f)
            {
                currentTime = 0;
                //UI숨기기
                duelResult.enabled = false;
            }
        }
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            //결과 UI보여주기
            roundResult.text = "이긴 횟수 : A " + GameManager.Instance.AroundWinCount + " : B " + GameManager.Instance.BroundWinCount;
            roundResult.enabled = true;
            currentTime += Time.deltaTime;
            if (currentTime > 0.5f)
            {
                currentTime = 0;
                //UI숨기기
                roundResult.enabled = false;
            }
        }
    }
}
