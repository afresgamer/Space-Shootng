using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour {

    [SerializeField, Header("移動時間")]
    private float ESpeed = 1.0f;
    Rigidbody2D rb2d;
    public enum EnemyType { Easy = 1, Normal, Hard, Boss }
    [Header("敵の強さ")]
    public EnemyType enemyType = EnemyType.Easy;

	public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        if (transform.position.y <= -20)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 通常移動
    /// </summary>
    public virtual void Move()
    {
        rb2d.velocity = -transform.up * ESpeed;
    }

    /// <summary>
    /// ターゲットを指定して一定距離近づいて、近づき過ぎたときは離れるようにする
    /// </summary>
    /// <param name="target"></param>
    public virtual void TargetMove(GameObject target, float duration)
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);
        if(_distance > 8)//通常
        {
            Move();
        }
        else if(_distance <= 3)//離れる
        {
            transform.DOMove(-target.transform.position, duration);
        }
    }

    /// <summary>
    /// パスによって移動処理
    /// </summary>
    /// <param name="path"></param>
    public virtual void PathMove(Transform[] PosS)
    {
        Vector3[] path = new Vector3[PosS.Length];
        for (int i = 0; i < PosS.Length; i++)
        {
            path[i] = PosS[i].position;
        }

        transform.DOLocalPath(path, ESpeed, PathType.CatmullRom).SetOptions(false).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// ゲームオブジェクトの位置から移動処理
    /// </summary>
    /// <param name="objs"></param>
    public virtual void PathMove(GameObject[] objs, PathType pathType)
    {
        Vector3[] path = new Vector3[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            path[i] = objs[i].transform.position;
        }

        Tweener tweener;
        tweener = transform.DOLocalPath(path, ESpeed, pathType).SetOptions(false);
        tweener.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            if(enemyType != EnemyType.Boss)
            {
                PlayerStatus.Instance.PlayerHp.Value -= (int)enemyType;
            }
            PlayerStatus.Instance.Damage(collision.gameObject);
        }
    }

}
