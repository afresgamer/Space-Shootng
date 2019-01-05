using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour {

    private Text FPSText;
	
    void Start()
    {
        FPSText = GetComponent<Text>();
    }

	void Update () {
        float fps = 1f / Time.deltaTime;
        FPSText.text = fps.ToString("f2");
    }
}
