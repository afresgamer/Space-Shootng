using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : SingletonMonoBehaviour<PlayerStatus> {
    
    private float playerHp = 10;
    public float PlayerHp
    {
        get { return playerHp; }
        set {
            if (!IsDamage)
            {
                playerHp = value;
                if (playerHp < 0) { playerHp = 0; }
            }
        }
    }

    private int score = 0;
    public int Score {
        get { return score; }
        set { score = value; }
    }
    
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

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
        Init();
    }

    public void Init()
    {
        playerHp = 10;
        score = 0;
    }

}
