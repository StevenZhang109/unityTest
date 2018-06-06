using UnityEngine;
using System.Collections;

public class SnowBullet :Bullet {
    [Range(0, 1f)]
    public float downRatio;
    public float effectiveTime;

    protected override void HitEffect()
    {
        base.HitEffect();
        if (_target)
            _target.GetComponent<SlowDown>().ActionSlowDown(effectiveTime, downRatio);
    }

}
