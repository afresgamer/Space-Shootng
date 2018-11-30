using UnityEngine;

public class PlayerBullet : BulletBase {
    
    private PoolController poolEffect;

    public override void Start()
    {
        poolEffect = GameObject.FindGameObjectWithTag("Controller").GetComponent<PoolController>();
    }

    public override void Update()
    {
        if (transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y)
        {
            gameObject.SetActive(false);
        }
        base.ShotMove();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteorite")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            PlayerStatus.Instance.Score.Value += 10;
            poolEffect.CreateObj(collision.transform.position, Quaternion.identity);
        }
        else if(collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            PlayerStatus.Instance.Score.Value += 20;
            poolEffect.CreateObj(collision.transform.position, Quaternion.identity);
        }
        else if(collision.tag == "Boss")
        {
            gameObject.SetActive(false);
            PlayerStatus.Instance.Damage(collision.gameObject);
            PlayerStatus.Instance.Score.Value += 30;
            collision.GetComponent<BossEnemy>().BossLife.Value--;
        }
    }
}
