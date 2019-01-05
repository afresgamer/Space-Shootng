using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

	void Start () {
        //BGM初期化
        //SoundController.Instance.PlayBGM();
        //ランキング表示初期化
        if (RankingUtil.Instance.rankingList.Count > 0)
        {
            RankingUtil.Instance.FetchTopRankers();
            if (RankingUtil.Instance.rankingList.Count < RankingTexts.Length)
            {
                for(int i = 0;  i < RankingUtil.Instance.rankingList.Count; i++)
                {
                    RankingTexts[i].text = (i + 1) + RankingUtil.Instance.rankingList[i].GetRanking();
                }
                for(int j = RankingUtil.Instance.rankingList.Count;  j < RankingTexts.Length; j++)
                {
                    RankingTexts[j].text = (j + 1) + "  ---" + "        -----";
                }
            }
            else if(RankingUtil.Instance.rankingList.Count == RankingTexts.Length)
            {
                for(int i = 0; i < RankingTexts.Length; i++)
                {
                    RankingTexts[i].text = (i + 1) + RankingUtil.Instance.rankingList[i].GetRanking();
                }
            }
        }
	}

    public void AnimRankingWindow()
    {
        MoveUI(StartWindow, 500, 0);
        MoveUI(RankingWindow, 0, 2);
    }

    public void AnimRecordWindow()
    {
        MoveUI(StartWindow, 500, 0);
        MoveUI(RecordWindow, 0, 2);
    }

    public void InitWindow()
    {
        if(RankingWindow.rectTransform.anchoredPosition3D.y == 0)
        {
            MoveUI(RankingWindow, 550, 0);
        }
        else if(RecordWindow.rectTransform.anchoredPosition3D.y == 0)
        {
            MoveUI(RecordWindow, 550, 0);
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
    
}
