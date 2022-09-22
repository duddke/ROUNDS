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
        //알파값 천천히 증가 후
        time += fadeTime * Time.deltaTime;
        textColor.a = Mathf.Lerp(0, 1, time);
        text.GetComponent<Text>().color = textColor;
        //알파값이 1이 되고
        if (text.GetComponent<Text>().color.a >= 0.9f)
        {
            //점프를 누르면
            if(Input.GetButtonDown("Jump"))
            //로비씬으로 가기
            {
                SceneManager.LoadScene("SYA_LoadScene");
                gameObject.SetActive(false);
            }
        }
    }
}
