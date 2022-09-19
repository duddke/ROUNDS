using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Gear : MonoBehaviour
{
    //빙글빙글 돌아간다
    public float rotSpeed = 300;
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         time = Time.deltaTime * rotSpeed;
        transform.Rotate(transform.forward*time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어라면
        //플레이어 온데미지 실행
    }
}
