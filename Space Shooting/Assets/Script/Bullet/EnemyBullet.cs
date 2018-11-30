using UnityEngine;

public class EnemyBullet : BulletBase {

    public enum BulletType { Normal, Homing }
    [Header("弾の種類")]
    public BulletType bulletType = BulletType.Normal;

    //生存時間
    [SerializeField, Header("生存時間")]
    private float survivalTime = 3;
    private float time = 0;

    public override void Start()
    {
        //ランダム弾種設定
        //int ranNum = Random.Range(0, 2);
        //bulletType = (BulletType)ranNum;
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

        //生存時間の管理
        time += Time.deltaTime;
        if(time > survivalTime)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerStatus.Instance.Damage(collision.gameObject);
            PlayerStatus.Instance.PlayerHp.Value -= 1;
            gameObject.SetActive(false);
        }
    }

}
