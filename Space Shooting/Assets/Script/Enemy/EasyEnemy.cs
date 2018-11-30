using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : EnemyBase {

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Move();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

}
