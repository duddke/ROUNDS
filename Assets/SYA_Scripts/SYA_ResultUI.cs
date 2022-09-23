using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ResultUI : MonoBehaviour
{
    //������� UI
    public Text duelResult;
    //������ UI
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
            //��� UI�����ֱ�
            duelResult.text = "���� Ƚ�� : A " + GameManager.Instance.BdieCount + " : B " + GameManager.Instance.AdieCount;
            duelResult.enabled = true;
            currentTime += Time.deltaTime;
            if (currentTime > 0.5f)
            {
                currentTime = 0;
                //UI�����
                duelResult.enabled = false;
            }
        }
        if (GameManager.Instance.gameRule == GameManager.GameRule.DuelResult)
        {
            //��� UI�����ֱ�
            roundResult.text = "�̱� Ƚ�� : A " + GameManager.Instance.AroundWinCount + " : B " + GameManager.Instance.BroundWinCount;
            roundResult.enabled = true;
            currentTime += Time.deltaTime;
            if (currentTime > 0.5f)
            {
                currentTime = 0;
                //UI�����
                roundResult.enabled = false;
            }
        }
    }
}
