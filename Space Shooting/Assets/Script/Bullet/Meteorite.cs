using UnityEngine;
using DG.Tweening;

public class Meteorite : BulletBase {

    Rigidbody2D rb2d;
    [SerializeField, Header("回転スピード")]
    private float RSpeed = 1.0f;

    public override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        rb2d.AddTorque(RSpeed);
        if (transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerStatus.Instance.PlayerHp.Value -= 1;
            PlayerStatus.Instance.Damage(collision.gameObject);
        }
    }
}
