using UnityEngine;
using DG.Tweening;

public class SoundController : SingletonMonoBehaviour<SoundController> {

    //各種サウンド
    private AudioSource BGM;
    //サウンド設定用変数
    [Header("サウンド設定用変数")]
    public SoundSetting[] m_SoundSettings;
    public SoundSetting SoundSetting { get { return m_SoundSettings[sceneNum]; } }
    Sequence seq;

    private int sceneNum = 0;
    public int SceneNum {
        get { return sceneNum; }
        set { sceneNum = value; }
    }

    public void Init()
    {
        //BGMの設定
        BGM = GetComponent<AudioSource>();
        BGM.clip = SoundSetting.bgm_AudioClip[0];
        BGM.volume = SoundSetting.BGM;
        BGM.mute = SoundSetting.Mute;
        BGM.loop = SoundSetting.Loop;
        seq = DOTween.Sequence();
    }

    public void ChangeSetting()
    {
        BGM.clip = SoundSetting.bgm_AudioClip[0];
        BGM.volume = SoundSetting.BGM;
        BGM.mute = SoundSetting.Mute;
        BGM.loop = SoundSetting.Loop;
    }
    
    void Start()
    {
        Init();
        PlayBGM(0);
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    public void PlayBGM()
    {
        if (BGM.isPlaying) { BGM.time = 0; BGM.Play(); }
        else { BGM.Play(); }
    }

    /// <summary>
    /// BGM再生(再生するBGM指定)
    /// </summary>
    /// <param name="num"></param>
    public void PlayBGM(int num)
    {
        if (BGM.isPlaying) { BGM.time = 0; BGM.Play(); }
        else
        {
            BGM.clip = SoundSetting.bgm_AudioClip[num];
            BGM.Play();
        }
    }

    public void FadePlayBGM(float duration,int num)
    {
        if (BGM.isPlaying)
        {
            seq.Append(BGM.DOFade(0, duration).OnComplete(() => BGM.clip = SoundSetting.bgm_AudioClip[num]));
            seq.Append(BGM.DOFade(SoundSetting.BGM, duration).SetDelay(duration).OnComplete(() => BGM.Play()));
        }
        else
        {
            seq.Append(BGM.DOFade(0, duration).OnComplete(() => BGM.clip = SoundSetting.bgm_AudioClip[num]));
            seq.Append(BGM.DOFade(SoundSetting.BGM, duration).SetDelay(duration).OnComplete(() => BGM.Play()));
        }
    }

    public void PlaySE(AudioSource se, int index)
    {
        if (!SoundSetting.Mute)
        {
            if (se.isPlaying) se.Stop();
            else { se.PlayOneShot(SoundSetting.se_AudioClip[index]); }
        }
    }

    public void StopBGM()
    {
        BGM.Stop();
        BGM.time = 0;
    }

    public void StopSE(AudioSource se, int index)
    {
        se.Stop();
        se.time = 0;
    }

    void OnDestroy()
    {
        seq.Kill();
    }
}
