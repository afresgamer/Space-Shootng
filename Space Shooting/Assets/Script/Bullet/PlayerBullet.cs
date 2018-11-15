using UnityEngine;

public class PlayerBullet : BulletBase {
    
    private PoolController poolEffect;

    public override void Start()
    {
        poolEffect = GameObject.FindGameObjectWithTag("Controller").GetComponent<PoolController>();
    }

    public override void Update()
    {
        base.Update();
        base.ShotMove();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteorite")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            PlayerStatus.Instance.Score += 10;
            poolEffect.CreateObj(collision.transform.position, Quaternion.identity);
        }
    }
}
