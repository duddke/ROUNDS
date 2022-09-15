using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float deg; // 각도
    public float turretSpeed; // 포신 스피드
    public GameObject firePos;
    public GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            deg = deg + Time.deltaTime * turretSpeed;
            float rad = deg * Mathf.Deg2Rad; // 각도를 라디안으로
            firePos.transform.localPosition = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            firePos.transform.eulerAngles = new Vector3(0, 0, deg);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            deg = deg - Time.deltaTime * turretSpeed;
            float rad = deg * Mathf.Deg2Rad;
            firePos.transform.localPosition = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            firePos.transform.eulerAngles = new Vector3(0, 0, deg);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bulletFactory);
            go.transform.position = firePos.transform.position;
        }
    } 
}
