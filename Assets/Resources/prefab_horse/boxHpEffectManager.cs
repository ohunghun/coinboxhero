using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxHpEffectManager : MonoBehaviour
{
    public Transform popups;
    List<UihpText> ps = new List<UihpText>();
    int cur = 0;
    Vector3 boxposToScreenPos;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform ch in popups)
        {
            ps.Add(ch.GetComponent<UihpText>());
        }
        ScreenManager.Instance.addOnCameraSizeChanged(OnScreenSizeUpdate);
        box.Instance.AddBoxHitCallback(onboxhit);
    }
    void onboxhit(float power)
    {
        ShootEffect(power+"");
    }
    float boxlength;
    void OnScreenSizeUpdate()
    {
        boxlength = (Camera.main.WorldToScreenPoint(new Vector3(0, 1)) - Camera.main.WorldToScreenPoint(new Vector3(0, 0))).y*2;
        boxposToScreenPos = Camera.main.WorldToScreenPoint(box.Instance.transform.position)+ new Vector3(0,boxlength*0.5f);
    }
    public void ShootEffect(string power)
    {
        ps[cur].Shoot(power,boxposToScreenPos+new Vector3(Random.Range(-boxlength*0.3f,boxlength*0.3f),0));
        cur = (cur + 1) % ps.Count;
    }

}
