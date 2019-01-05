using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleController : MonoBehaviour {

    [Header("タイトルテキスト")]
    public Text Title;
    [Header("マスクエフェクト")]
    public Image MaskEffect;
    [Header("Shot Button")]
    public Button ShotButton;
    [Header("Player")]
    public PlayerMovement playerMovement;
    [Header("敵コントローラー")]
    public EnemyController enemyController;

	void Start () {
        playerMovement.gameObject.SetActive(false);
        enemyController.gameObject.SetActive(false);
        ShotButton.interactable = false;
        Title.DOColor(Color.red, 2.0f).SetLoops(-1, LoopType.Yoyo);
        MaskEffect.rectTransform.DOAnchorPosX(180, 1.5f).SetLoops(-1);
        Title.rectTransform.DOScale(1.2f, 1.5f);
    }

    public void SetGameStart()
    {
        playerMovement.gameObject.SetActive(true);
        enemyController.gameObject.SetActive(true);
        ShotButton.interactable = true;
        gameObject.SetActive(false);
    }
    
}
