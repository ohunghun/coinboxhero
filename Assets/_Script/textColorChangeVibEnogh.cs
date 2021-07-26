using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textColorChangeVibEnogh : MonoBehaviour
{
    string beforeText;
    Text text;
    long cost;
    int id;
    cargo c;
    void Start()
    {
        
        text = GetComponent<Text>();
        beforeText = text.text;
        cost = long.Parse(text.text);
        id = Random.Range(1, 10);
        c = cargo.Instance;
    }
    private void Update()
    {
        if (Time.frameCount % id == 0)
        {
            if(text.text!=beforeText)
            {
                beforeText = text.text;
                cost = long.Parse(text.text);
            }
            if (c.isEnoughVib(cost))
                text.color = Color.black;
            else
                text.color = Color.red;
        }
    }
}