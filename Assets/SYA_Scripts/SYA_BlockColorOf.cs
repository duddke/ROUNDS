using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_BlockColorOf : MonoBehaviour
{
    public static SYA_BlockColorOf Instance;
    private void Awake()
    {
        Instance = this;
    }
    public float ran;
    public float ran1;
    public float ran2;
    public bool up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!up)
        {
            ran = Random.Range(0.3f, 2);
            ran1 = Random.Range(0.3f, 2);
            ran2 = Random.Range(0.3f, 2);
            up = true;
        }
    }
}
