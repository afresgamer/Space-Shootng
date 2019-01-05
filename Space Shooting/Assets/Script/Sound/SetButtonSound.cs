using UnityEngine;
using UnityEngine.UI;

public class SetButtonSound : MonoBehaviour {

    private Button GetButton;
    [Header("BGM番号")]
    public int num = 1;
    [Header("フェード間隔")]
    public float duration = 0.5f;
    [Header("Fadeするかどうか")]
    public bool IsFade = false;
    
	void Start () {
        GetButton = GetComponent<Button>();
        if(IsFade) GetButton.onClick.AddListener(() => SoundController.Instance.FadePlayBGM(duration, num));
    }

}
