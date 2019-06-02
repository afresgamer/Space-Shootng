using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class BossEnemy : EnemyBase {

    //bossライフ用のReactiveProperty変数
    public ReactiveProperty<int> BossLife { get; private set; }
    [Header("ゲームクリア画面")]
    public Image GameClearImage;
    [Header("ボスライフUI")]
    public Slider BossLifeSlider;
    [Header("ショットButton")]
    public Button ShotButton;

    public override void Start()
    {
        base.Start();
        BossLife = new ReactiveProperty<int>(10);
        //ボスライフゲージ更新処理
        BossLife.AsObservable().Subscribe(bossLife =>
        {
            BossLifeSlider.value = bossLife * 0.1f;
        });
        //ボスのライフがゼロになったらゲームクリア処理
        BossLife.AsObservable().Where(bosslife => bosslife <= 0).Subscribe(bossLife => 
        {
            //ボス討伐後ゲームクリア処理
            GameClearImage.rectTransform.DOAnchorPosY(0, 1.0f);
            gameObject.SetActive(false);
            ShotButton.interactable = false;
            SoundController.Instance.FadePlayBGM(0.5f, 4);
            //全発射するオブジェクトを停止する
            foreach(var ObstableCon in FindObjectsOfType<ObstableController>())
            {
                ObstableCon.gameObject.SetActive(false);
            }
        });
    }

    public override void Update()
    {
        //playerがいないときも考慮
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            base.TargetMove(GameObject.FindGameObjectWithTag("Player"), 5.0f);
        }
        else { base.Move(); }
        //下の方まで移動したときのための位置初期化
        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(0, 8, 0);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision){}
}
