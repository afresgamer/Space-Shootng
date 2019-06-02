using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class PlayerStatus : SingletonMonoBehaviour<PlayerStatus> {

    //プレイヤーHPのReactiveProperty(値の変更を通知してくれる変数)
    public ReactiveProperty<float> PlayerHp { get; set; }
    //スコアのReactiveProperty(値の変更を通知してくれる変数)
    public ReactiveProperty<int> Score { get; set; }
    
    /// <summary>
    /// ダメージフラグ
    /// </summary>
    [HideInInspector]
    public bool IsDamage = false;

    /// <summary>
    /// プレイヤーダメージ処理
    /// </summary>
    /// <param name="obj"></param>
    public void Damage(GameObject obj)
    {
        IsDamage = true;
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        StartCoroutine(DamageEffect(obj));
    }

    IEnumerator DamageEffect(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        IsDamage = false;
    }

    public void ChangeScene(int num)
    {
        //シーン番号が０以上でないと返す
        if (num < 0) return;
        GameController.Instance.InitGameStateProperty();
        SceneManager.LoadScene(num);
        SoundController.Instance.SceneNum = 1;
        SoundController.Instance.ChangeSetting();
    }

    public void InitScene()
    {
        GameController.Instance.InitGameStateProperty();
        Init();
        SceneManager.LoadScene(0);
        SoundController.Instance.SceneNum = 0;
        SoundController.Instance.Init();
    }

    /// <summary>
    /// パラメータ初期化
    /// </summary>
    public void Init()
    {
        PlayerHp = new ReactiveProperty<float>(10);
        Score = new ReactiveProperty<int>(0);
    }

}
