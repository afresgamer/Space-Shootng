using UnityEngine;
using UnityEngine.UI;

public class SetReStart : MonoBehaviour {

    private Button ReStartButton;
    [Header("初期化するかどうか")]
    public bool isInit = false;

	void Start () {
        ReStartButton = GetComponent<Button>();
        if (isInit)
        {
            ReStartButton.onClick.AddListener(() => PlayerStatus.Instance.ChangeScene(0));
        }
        else
        {
            ReStartButton.onClick.AddListener(() => PlayerStatus.Instance.ChangeScene());
        }
	}

    public void SetSE()
    {
        SoundController.Instance.PlaySE(GetComponent<AudioSource>(), 0, false);
    }
}
