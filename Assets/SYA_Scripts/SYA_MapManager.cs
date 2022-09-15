using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_MapManager : MonoBehaviour
{
    public List<GameObject> MAP = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int ran = Random.Range(0, MAP.Count);
        if (ran >= 0)
        {
            GameObject map = Instantiate(MAP[ran]);
            map.transform.position = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
