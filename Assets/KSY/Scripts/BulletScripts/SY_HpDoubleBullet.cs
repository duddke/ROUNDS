using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_HpDoubleBullet : MonoBehaviour
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Ѿ��� target�� �浹�ϸ�
        if (collision.gameObject == target)
        {
            
        }
    }
}
