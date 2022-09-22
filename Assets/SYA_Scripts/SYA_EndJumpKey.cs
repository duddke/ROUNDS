using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SYA_EndJumpKey : MonoBehaviour
{
    public Text text;
    Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        textColor = text.GetComponent<Text>().color;
    }

    public float fadeTime = 0.7f;
    float time;

    // Update is called once per frame
    void Update()
    {
        //���İ� õõ�� ���� ��
        time += fadeTime * Time.deltaTime;
        textColor.a = Mathf.Lerp(0, 1, time);
        text.GetComponent<Text>().color = textColor;
        //���İ��� 1�� �ǰ�
        if (text.GetComponent<Text>().color.a >= 0.9f)
        {
            //������ ������
            if(Input.GetButtonDown("Jump"))
            //�κ������ ����
            {
                SceneManager.LoadScene("SYA_LoadScene");
                gameObject.SetActive(false);
            }
        }
    }
}
