using UnityEngine;
using UniRx;

public class EnemyController : MonoBehaviour {

    [Header("敵出現タイプ")]
    public PoolController[] poolControllers;
    [Header("ボスタイプ")]
    public BossEnemy bossEnemy;

    void Start () {

        //ステージ数が変更したときの難易度変更
        GameController.Instance.GameStateProperty.AsObservable().Subscribe(gameState =>
        {
            //難易度を上げていくごとに敵の強さ変更処理
            if(gameState <= poolControllers.Length)
            {
                poolControllers[gameState - 1].enabled = true;
                if(gameState - 2 >= 0) { poolControllers[gameState - 2].enabled = false; }
            }
            //ボス出現処理
            if(GameController.Instance.GameStateProperty.Value == 5)
            {
                bossEnemy.gameObject.SetActive(true);
                bossEnemy.BossLifeSlider.gameObject.SetActive(true);
            }

            Debug.Log(GameController.Instance.GameStateProperty.Value);
        });
	}
}
