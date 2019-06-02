using UnityEngine;
using UniRx;

public class EnemyController : MonoBehaviour {

    [Header("敵出現タイプ")]
    public ObstableController[] poolControllers;
    [Header("ボスタイプ")]
    public BossEnemy bossEnemy;

    void Start () {

        //ステージ数が変更したときの難易度変更
        GameController.Instance.GameStateProperty.AsObservable().Subscribe(gameState =>
        {
            //難易度を上げていくごとに敵の強さ変更処理
            if (GameController.Instance.GameStateProperty.Value < poolControllers.Length)
            {
                poolControllers[gameState - 1].gameObject.SetActive(true);
                if (gameState - 1 > 0) { poolControllers[gameState - 2].gameObject.SetActive(false); }
            }//ステージ3のときにBGM変更
            if (GameController.Instance.GameStateProperty.Value == 3)
            {
                SoundController.Instance.FadePlayBGM(0.5f, 2);
            }
            //ボス出現処理
            if(GameController.Instance.GameStateProperty.Value >= 5)
            {
                foreach(var poolCon in poolControllers)
                {
                    poolCon.gameObject.SetActive(false);
                }
                bossEnemy.gameObject.SetActive(true);
                bossEnemy.BossLifeSlider.transform.parent.gameObject.SetActive(true);
                SoundController.Instance.FadePlayBGM(0.5f, 3);
            } 
        });
	}
}
