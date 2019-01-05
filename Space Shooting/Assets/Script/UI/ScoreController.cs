using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class ScoreController : MonoBehaviour {

    [SerializeField, Header("スコア")]
    private Text scoreText;
    [SerializeField, Header("HPバー")]
    private Slider HpSlider;
    [Header("ゲームオーバー画面")]
    public Image GameOverObj;
    [SerializeField, Header("ステージ数")]
    private Text StageText;
    

    private void Start()
    {
        //各種パラメータの初期化
        PlayerStatus.Instance.Init();
        GameController.Instance.InitGameStateProperty();
        SoundController.Instance.Init();
        SoundController.Instance.PlayBGM();

        GameOverObj.gameObject.SetActive(false);

        //各種パラメータ更新処理
        PlayerStatus.Instance.PlayerHp.AsObservable().Subscribe(hp =>
        {
            //HPバーの表示処理
            HpSlider.value = PlayerStatus.Instance.PlayerHp.Value * 0.1f;
        });

        PlayerStatus.Instance.Score.AsObservable().Subscribe(score =>
        {
            //スコアの表示処理
            scoreText.text = "Score: " + PlayerStatus.Instance.Score.Value.ToString();
            //ステージ表示処理
            if (GameController.Instance.IsNextState(PlayerStatus.Instance.Score.Value)) AnimStateMovement();
        });
    }
    
    public void AnimStateMovement()
    {
        StageText.text = "Stage " + GameController.Instance.SetGameState(GameController.Instance.GameStateProperty).ToString();
        StartCoroutine(StateAnimCoroutine());
    }

    IEnumerator StateAnimCoroutine()
    {
        StageText.rectTransform.DOAnchorPosY(0, 1.0f);
        yield return new WaitForSeconds(2.0f);
        StageText.rectTransform.DOAnchorPosY(-450, 1.0f);
        yield return new WaitForSeconds(1.0f);
        StageText.gameObject.SetActive(false);
        StageText.rectTransform.DOAnchorPosY(450, 1.0f);
        yield return new WaitForSeconds(2.0f);
        StageText.gameObject.SetActive(true);
    }
}
