using UnityEngine;

public class BulletBase : MonoBehaviour {

    [SerializeField, Header("弾丸移動スピード")]
    private float BSpeed = 1.0f;

    public virtual void Start(){}

    public virtual void Update() {}

    /// <summary>
    /// 弾丸移動処理(直線移動)
    /// </summary>
    public virtual void ShotMove()
    {
        transform.Translate(Vector3.up * BSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 弾丸移動処理(追従移動)
    /// </summary>
    /// <param name="target"></param>
    public virtual void HomingMove(GameObject target)
    {
        Vector2 pos = new Vector2(0, BSpeed * Time.deltaTime);
        transform.Translate(pos);
        Vector3 diff = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collision){}
}
