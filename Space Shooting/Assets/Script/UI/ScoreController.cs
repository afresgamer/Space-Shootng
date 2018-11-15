using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreController : MonoBehaviour {

    [SerializeField, Header("スコア")]
    private Text scoreText;
    [SerializeField, Header("HPバー")]
    private Slider HpSlider;
    [Header("ゲームオーバー画面")]
    public Image GameOverObj;
    [SerializeField, Header("ステージ数")]
    private Text StateText;

    private void Start()
    {
        scoreText.text += PlayerStatus.Instance.Score.ToString();
        GameOverObj.gameObject.SetActive(false);
        StateText.text += GameController.Instance.GameState;
        StartCoroutine(StateAnimCoroutine());
    }

    private void Update()
    {
        scoreText.text = "Score:" + PlayerStatus.Instance.Score.ToString();
        HpSlider.value = PlayerStatus.Instance.PlayerHp * 0.1f;
        //goto 後は一定スコアごとにステージ数と出現する敵を増やす(最終ステージはボス戦)
    }
    
    public void AnimStateMovement()
    {
        StateText.text = "State " + GameController.Instance.SetGameState(GameController.Instance.GameState);
        StartCoroutine(StateAnimCoroutine());
    }

    IEnumerator StateAnimCoroutine()
    {
        StateText.rectTransform.DOAnchorPosY(0, 1.0f);
        yield return new WaitForSeconds(2.0f);
        StateText.rectTransform.DOAnchorPosY(-450, 1.0f);
        yield return new WaitForSeconds(1.0f);
        StateText.gameObject.SetActive(false);
        StateText.rectTransform.DOAnchorPosY(450, 1.0f);
        yield return new WaitForSeconds(2.0f);
        StateText.gameObject.SetActive(true);
    }
}
