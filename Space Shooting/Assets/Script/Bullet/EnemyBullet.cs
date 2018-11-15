using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase {

    public enum BulletType { Normal, Homing }
    [HideInInspector]
    public BulletType bulletType = BulletType.Normal;

    public override void Start()
    {
        //ランダム弾種設定
        int ranNum = Random.Range(0, 2);
        bulletType = (BulletType)ranNum;
    }

    public override void Update()
    {
        switch (bulletType)
        {
            case BulletType.Normal:
                base.ShotMove();
                break;
            case BulletType.Homing:
                base.HomingMove(GameObject.FindGameObjectWithTag("Player"));
                break;
        }
        base.Update();
    }
}
