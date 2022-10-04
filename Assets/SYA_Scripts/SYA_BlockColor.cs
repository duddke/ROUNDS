using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_BlockColor : MonoBehaviour
{
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        
    }

    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Color co = sp.color;

        co.r = Mathf.Lerp(co.r, SYA_BlockColorOf.Instance.ran, time*0.1f);
        co.g = Mathf.Lerp(co.g, SYA_BlockColorOf.Instance.ran1, time * 0.1f);
        co.b = Mathf.Lerp(co.b, SYA_BlockColorOf.Instance.ran2, time * 0.1f);

        sp.color = co;
        if (co.r == SYA_BlockColorOf.Instance.ran && co.g == SYA_BlockColorOf.Instance.ran1 && co.b == SYA_BlockColorOf.Instance.ran2)
        {
            time = 0;
            SYA_BlockColorOf.Instance.up = false;
        }
    }
}
