using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class joystickAutoAttacker : MonoBehaviour
{
    joystick j;
    int speedlevel = 1;
    Toggle togle;
    public Image img_gage;
    float gage = 0;
    // Start is called before the first frame update
    void Start()
    {
        j = joystick.Instance;
        togle = GetComponent<Toggle>();
        StartCoroutine("click");
    }
    
    IEnumerator click()
    {

        yield return null;
        while(true)
        {
            if(togle.isOn)
            {
                gage = gage + Time.deltaTime;
                
                img_gage.fillAmount = gage / delay();
                if (gage > delay())
                {
                    j.attack();
                    ishold = true;
                    myCountDownTimer.Instance.CountDown(0.4f, () =>
                    {
                        ishold = false;
                        j.attack_long_release();
                    });
                    gage = 0;
                }
                yield return null;
            }else
                yield return null;


        }
    }
    bool ishold=false;
    private void Update()
    {
        if(togle.isOn)
        {
            if(ishold)
            {
                j.attack_long_held();
            }
        }
    }
    float delay()
    {
        float value = 3 - 0.1f * (1 - speedlevel);
        value = 0.2f;
        return Mathf.Clamp(value, 0.2f, 5f);
    }

    
}
