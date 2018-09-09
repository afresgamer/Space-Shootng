using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Header("動くスピード")]
    private float speed = 1.0f;
    [Header("Poolオブジェクト")]
    public PoolController poolController;
    Sequence seq;
    Rigidbody2D rb2d;
    
    bool isMove = true;

    private void Awake()
    {
        poolController.AttachPoint = poolController.transform;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update () {
        //移動処理
        TouchInfo touchInfo = TouchUtil.GetTouch();

        if (touchInfo == TouchInfo.Began && isMove) { Move();}

        //弾丸処理
        if (poolController.IsCreate())
        { poolController.CreateObj(poolController.AttachPoint.position, poolController.AttachPoint.rotation); }
	}

    public void Move()
    {
        seq = DOTween.Sequence();
        Vector3 move = TouchUtil.GetTouchWorldPosition(Camera.main) - Camera.main.transform.position;
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //移動制限
        move.x = Mathf.Clamp(move.x, -5.5f, 5.5f);
        move.y = Mathf.Clamp(move.y, min.y, max.y);

        seq.Append(rb2d.DOMove(move * speed, 2.0f));

        Vector3 dir =  move - transform.position;

        seq.Join(rb2d.DORotate(-Mathf.Sign(dir.x) * 45, 0.5f));
        seq.Append(rb2d.DORotate(0, 0.5f));
    }

    public void OnDestroy()
    {
        seq.Kill();
    }
}
