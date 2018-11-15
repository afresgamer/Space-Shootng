using UnityEngine;
using DG.Tweening;

public class BulletBase : MonoBehaviour {

    [SerializeField, Header("弾丸移動スピード")]
    private float BSpeed = 1.0f;

    public virtual void Start(){}

    public virtual void Update()
    {
        if (transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 弾丸移動処理(直線移動)
    /// </summary>
    public virtual void ShotMove()
    {
        transform.Translate(0, BSpeed * Time.deltaTime, 0, Space.World);
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

    /// <summary>
    /// パスによって移動処理
    /// </summary>
    /// <param name="path"></param>
    public virtual void PathMove(Transform[] PosS)
    {
        Vector3[] path = new Vector3[PosS.Length];
        for(int i = 0; i < PosS.Length; i++)
        {
            path[i] = PosS[i].position;
        }
        transform.DOPath(path, 1.0f,PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0.05f,Vector3.forward);
    }

    /// <summary>
    /// ゲームオブジェクトの位置から移動処理
    /// </summary>
    /// <param name="objs"></param>
    public virtual void PathMove(GameObject[] objs)
    {
        Vector3[] path = new Vector3[objs.Length];
        for(int i = 0; i < objs.Length; i++)
        {
            path[i] = objs[i].transform.position;
        }
        transform.DOPath(path, 1.0f, PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0.05f, Vector3.forward);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision){}
}
