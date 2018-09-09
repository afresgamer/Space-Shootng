using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase {
    
    public override void Update()
    {
        base.Update();
        base.ShotMove();
    }
}
