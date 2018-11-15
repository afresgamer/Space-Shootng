using UnityEngine;

public class Meteorite : BulletBase {

    [SerializeField, Header("回転スピード")]
    private float RSpeed = 1.0f;

    public override void Update()
    {
        base.ShotMove();
        transform.Rotate(0, 0, RSpeed * Random.value);
        if (transform.position.y < -25)
        {
            gameObject.SetActive(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerStatus.Instance.PlayerHp -= 2;
            PlayerStatus.Instance.Damage(collision.gameObject);
        }
    }
}
