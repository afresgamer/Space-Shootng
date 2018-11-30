using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    private Button ReStartButton;

	void Start () {
        ReStartButton = GetComponent<Button>();
        ReStartButton.onClick.AddListener(() => PlayerStatus.Instance.ChangeScene(0));
	}
	
}
