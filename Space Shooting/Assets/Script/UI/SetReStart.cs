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
            ReStartButton.onClick.AddListener(() => PlayerStatus.Instance.InitScene());
        }
        else
        {
            ReStartButton.onClick.AddListener(() => PlayerStatus.Instance.ChangeScene(1));
        }
	}

    public void SetSE(AudioSource audioSource)
    {
        SoundController.Instance.PlaySE(audioSource, 0);
    }
}
