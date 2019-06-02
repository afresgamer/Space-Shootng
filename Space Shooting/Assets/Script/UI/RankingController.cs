using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;

public class RankingController : MonoBehaviour {

    [Header("スタート画面")]
    public Image StartWindow;
    [Header("ランキング上位")]
    public Image RankingWindow;
    [Header("記録更新画面")]
    public Image RecordWindow;

    [Header("ランキング表示テキスト")]
    public Text[] RankingTexts;
    [Header("移動時間")]
    public float MoveDuration = 2;
    bool isRanking = false;

	void Start () {
        //BGM初期化
        SoundController.Instance.PlayBGM();
        //ランキング表示初期化
        RankingUtil.Instance.FetchTopRankers();

        this.UpdateAsObservable().Where(_ => RankingUtil.Instance.rankingList.Count > 0).Subscribe(_ =>
        {
            if (!isRanking) SetRanking();
        });

        this.UpdateAsObservable().Where(_ => isRanking).Subscribe(_ =>
        {
            RankingUtil.Instance.rankingList = new List<NCMB.ScoreRanking>();
            isRanking = false;
        });
    }

    /// <summary>
    /// ランキングボード書き込み
    /// </summary>
    public void SetRanking()
    {
        if (RankingUtil.Instance.rankingList.Count < RankingTexts.Length)
        {
            for (int i = 0; i < RankingUtil.Instance.rankingList.Count; i++)
            {
                RankingTexts[i].text = (i + 1) + RankingUtil.Instance.rankingList[i].GetRanking();
            }
            for (int j = RankingUtil.Instance.rankingList.Count; j < RankingTexts.Length; j++)
            {
                RankingTexts[j].text = (j + 1) + "  ---" + "        -----";
            }
        }
        else if (RankingUtil.Instance.rankingList.Count == RankingTexts.Length)
        {
            for (int i = 0; i < RankingTexts.Length; i++)
            {
                RankingTexts[i].text = (i + 1) + RankingUtil.Instance.rankingList[i].GetRanking();
            }
        }
        isRanking = true;
    }

    /// <summary>
    /// ランキング初期化
    /// </summary>
    public void RankingInit()
    {
        //ランキング表示初期化
        RankingUtil.Instance.FetchTopRankers();
    }
    
    /// <summary>
    /// ランキングボード表示
    /// </summary>
    public void AnimRankingWindow()
    {
        MoveUI(StartWindow, 850, 0);
        MoveUI(RankingWindow, 0, 2);
    }

    /// <summary>
    /// 記録更新UI表示
    /// </summary>
    public void AnimRecordWindow()
    {
        MoveUI(StartWindow, 850, 0);
        MoveUI(RecordWindow, 0, 2);
    }

    /// <summary>
    /// UI初期化
    /// </summary>
    public void InitWindow()
    {
        if(RankingWindow.rectTransform.anchoredPosition3D.y == 0)
        {
            MoveUI(RankingWindow, 850, 0);
        }
        else if(RecordWindow.rectTransform.anchoredPosition3D.y == 0)
        {
            MoveUI(RecordWindow, 850, 0);
        }
        MoveUI(StartWindow, 0, 2);
    }
	
    /// <summary>
    /// UI移動
    /// </summary>
    /// <param name="image"></param>
    /// <param name="endPos"></param>
    public void MoveUI(Image image, float endPos, float delay)
    {
        image.rectTransform.DOAnchorPosY(endPos, MoveDuration).SetDelay(delay);
    }
    
    /// <summary>
    /// 一定時間ボタン非表示からボタン表示関数
    /// </summary>
    /// <returns></returns>
    public System.Collections.IEnumerable VisibleUI()
    {
        //goto ボタン処理完成させる
        yield return new WaitForSeconds(3);

    }
}
