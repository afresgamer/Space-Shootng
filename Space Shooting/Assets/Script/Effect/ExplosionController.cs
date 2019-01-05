using UnityEngine;

public class ExplosionController : MonoBehaviour {

    ParticleSystem explosion;
    AudioSource ExplosionSE;
    bool isPlay = false;

    void Start () {
        explosion = GetComponent<ParticleSystem>();
        ExplosionSE = GetComponent<AudioSource>();
        SoundController.Instance.PlaySE(ExplosionSE, 3, false);
        isPlay = true;
    }

    void OnEnable()
    {
        if (isPlay)
        {
            SoundController.Instance.PlaySE(ExplosionSE, 3, false);
            isPlay = false;
        }
    }
	
	void Update () {
        if (!explosion.isPlaying)
        {
            gameObject.SetActive(false);
            explosion.time = 0;
            isPlay = true;
        }
	}
}
