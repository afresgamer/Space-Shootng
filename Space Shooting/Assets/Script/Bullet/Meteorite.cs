using UnityEngine;
using DG.Tweening;

public class Meteorite : BulletBase {

    [SerializeField, Header("回転スピード")]
    private float RSpeed = 1.0f;

    public override void Update()
    {
        transform.DORotateQuaternion(transform.rotation * Quaternion.AngleAxis(45.0f, transform.forward), RSpeed).SetLoops(-1);
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
