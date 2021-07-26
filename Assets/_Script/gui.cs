using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
public class gui : MonoBehaviour
{
    public Text money, coinLimit;
    cargo mcargo;
    coinGun coingun;
    box b;
    public Animator coinfullani;
    public Slider slider_coinlimit,slider_boxhp;
    private void Start()
    {
        b = box.Instance;
        mcargo = cargo.Instance;
        coingun = coinGun.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        money.text = mcargo.money + "";
        coinLimit.text = coingun.Num_coinOnField() + "/" + coingun.getLimitCoin();
        float ratio= coingun.Num_coinOnField() /(float)coingun.getLimitCoin();
        slider_coinlimit.value = ratio;
        if(coingun.isCoinFieldFull())
            coinfullani.SetFloat("value", Mathf.Repeat(Time.timeSinceLevelLoad, 1));
        else
            coinfullani.SetFloat("value", 1);


    }
}
