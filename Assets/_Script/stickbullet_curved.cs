using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class stickbullet_curved : stickbullet
{
    public override void init(float power, Vector3 initpos, SortingGroup sortgroup)
    {
        base.init(power, initpos, sortgroup);
        posRatio = 0;
        endpos = box.Instance.transform.position;
        heightpos = endpos.y + 4 + 3 * Random.Range(0f, 1f);
        this.initpos = initpos;
    }
    Vector2 initpos;
    float posRatio = 0;
    Vector2 endpos;
    float heightpos;
    
    protected override void Update()
    {
        posRatio += Time.deltaTime * speed;
        posRatio = Mathf.Clamp(posRatio, 0, 1);
        transform.position = MathParabola.Parabola(initpos, endpos, heightpos, posRatio);
    }
}
