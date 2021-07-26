using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyEnough : MonoBehaviour
{
    Text text;
    cargo mcargo;
    public Color defaltColor = Color.black;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        this.mcargo = cargo.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        long num = long.Parse(text.text);
        if ( mcargo.money<num)
            text.color = Color.red;
        else
            text.color = defaltColor;
    }
}
