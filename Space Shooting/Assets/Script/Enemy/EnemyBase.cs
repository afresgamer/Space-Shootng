using UnityEngine;

public class EnemyBase : MonoBehaviour {

    [SerializeField, Header("移動時間")]
    private float ESpeed = 1.0f;
    Rigidbody2D rb2d;

	public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public virtual void Update(){}

    /// <summary>
    /// 通常移動
    /// </summary>
    public virtual void Move()
    {
        rb2d.velocity = -Vector2.up * ESpeed;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Player")
        //{
        //    gameObject.SetActive(false);
        //}
    }
    
}
