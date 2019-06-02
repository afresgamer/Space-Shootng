using UnityEngine;
using UnityEngine.UI;

public class RecordRanking : MonoBehaviour {

    [Header("名前入力フォーム")]
    public InputField NameInputField;
    [Header("現在得点")]
    public Text ScoreText;
    [Header("記録更新ウィンドウ表示ボタン")]
    public Button Recordbutton;
    [Header("記録更新ウィンドウ移動ボタン")]
    public Button RecordObj;

    void Start () {
        //現在の得点を記録表示
        if(PlayerStatus.Instance.Score.Value > 0)
        {
            ScoreText.text += " " + PlayerStatus.Instance.Score.Value.ToString();
        }
	}
	
    public void Record()
    {
        if (NameInputField.text != null)
        {
            RankingUtil.Instance.SaveRanking(NameInputField.text, PlayerStatus.Instance.Score.Value);
            Recordbutton.interactable = false;
            RecordObj.interactable = false;
        }
    }
}
