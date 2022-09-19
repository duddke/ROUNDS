using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_BrokenBlock : MonoBehaviour
{


    // Start is called before the first frame update
    /*void Start()
    {
    }

    private void Update()
    {
        *//*if (Input.GetKeyDown(KeyCode.X))
            Destroy(gameObject);*//*
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==30)
            Destroy(gameObject);
    }
}
