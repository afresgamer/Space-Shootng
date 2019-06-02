using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Header("動くスピード")]
    private float speed = 1.0f;
    [Header("PlayerBulletオブジェクト")]
    public PoolController poolController;
    Sequence seq;
    Rigidbody2D rb2d;
    AudioSource PlayerSE;
    private PoolController effectController;
    [SerializeField,Header("ゲームオーバー画面")]
    ScoreController scoreController;
    [Header("ショットButton")]
    public Button ShotButton;

    private void Awake()
    {
        gameObject.SetActive(true);
        poolController.AttachPoint = poolController.transform;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PlayerSE = GetComponent<AudioSource>();
        effectController = GameObject.FindGameObjectWithTag("Controller").GetComponent<PoolController>();
    }

    void Update () {
        //移動処理
        TouchInfo touchInfo = TouchUtil.GetTouch();

        if (touchInfo == TouchInfo.Began && !PlayerStatus.Instance.IsDamage) { Move();}

        //弾丸処理
        if (poolController.IsCreate())
        {
            poolController.CreateObj(poolController.AttachPoint.position, poolController.AttachPoint.rotation);
            SoundController.Instance.PlaySE(PlayerSE, 2);
        }

        //死亡処理
        if (PlayerStatus.Instance.PlayerHp.Value <= 0.0f) { Death(); }
    }

    public void Move()
    {
        //UI選択中は動かさない
        if (TouchUtil.IsGetTouch()) return;

        seq = DOTween.Sequence();
        Vector3 move = TouchUtil.GetTouchWorldPosition(Camera.main) - Camera.main.transform.position;
        //移動制限
        //move.x = Mathf.Clamp(move.x, -5.75f, 5.75f);
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
        ShotButton.interactable = false;
        SoundController.Instance.PlaySE(PlayerSE, 3);
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
