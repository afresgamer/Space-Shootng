using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Header("動くスピード")]
    private float speed = 1.0f;
    [Header("PlayerBulletオブジェクト")]
    public PoolController poolController;
    Sequence seq;
    Rigidbody2D rb2d;
    private PoolController effectController;
    [SerializeField,Header("ゲームオーバー画面")]
    ScoreController scoreController;

    private void Awake()
    {
        gameObject.SetActive(true);
        poolController.AttachPoint = poolController.transform;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        effectController = GameObject.FindGameObjectWithTag("Controller").GetComponent<PoolController>();
    }

    void Update () {
        //移動処理
        TouchInfo touchInfo = TouchUtil.GetTouch();

        if (touchInfo == TouchInfo.Began && !PlayerStatus.Instance.IsDamage) { Move();}

        //弾丸処理
        if (poolController.IsCreate())
        { poolController.CreateObj(poolController.AttachPoint.position, poolController.AttachPoint.rotation); }

        //死亡処理
        if (PlayerStatus.Instance.PlayerHp.Value <= 0.0f) { Death(); }
    }

    public void Move()
    {
        seq = DOTween.Sequence();
        Vector3 move = TouchUtil.GetTouchWorldPosition(Camera.main) - Camera.main.transform.position;
        //移動制限
        move.x = Mathf.Clamp(move.x, -5.5f, 5.5f);
        //move.y = Mathf.Clamp(move.y, -10f, 20f);
        seq.Append(rb2d.DOMove(move * speed, 2.0f));

        Vector3 dir =  transform.position - move;
        if(Mathf.Abs(dir.x) > 0.8f) //誤差が0.8以上のときだけ移動する方向に向く
        {
            seq.Join(rb2d.DORotate(Mathf.Sign(dir.x) * 45, 0.5f));
            seq.Append(rb2d.DORotate(0, 0.5f));
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
        effectController.CreateObj(transform.position, Quaternion.identity);
        //ゲームオーバー画面表示
        scoreController.GameOverObj.gameObject.SetActive(true);
        scoreController.GameOverObj.rectTransform.DOAnchorPosY(0, 1.0f);
    }

    public void OnDestroy()
    {
        seq.Kill();
    }
}
