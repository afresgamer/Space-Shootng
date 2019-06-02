using UnityEngine;
using NCMB;
using System.Collections.Generic;

public class RankingUtil : SingletonMonoBehaviour<RankingUtil> {

    //ランキング用保存リスト
    [HideInInspector]
    public List<ScoreRanking> rankingList = new List<ScoreRanking>();

    /// <summary>
    /// ランキング記録更新
    /// </summary>
    /// <param name="userName">ユーザー名</param>
    /// <param name="score">スコア</param>
    public void SaveRanking(string userName, int score)
    {
        NCMBObject ncmbObj = new NCMBObject("ScoreRanking");
        ncmbObj["Name"] = userName;
        ncmbObj["Score"] = score;

        ncmbObj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                //失敗したら
                Debug.LogError("保存失敗です。" + e.ErrorMessage);
            }
            else
            {
                //成功したら
                Debug.Log("保存成功です。ランキング作成しました");
            }
        });
    }

    /// <summary>
    /// 存在するユーザー名から記録更新
    /// </summary>
    /// <param name="userName">ユーザー名</param>
    /// <param name="score">スコア</param>
    public void FetchRanking(string userName, int score)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreRanking");
        query.WhereEqualTo("Name", userName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if(e != null)//検索失敗時の処理
            {
                Debug.LogError(userName + "という名前は存在しません。" + e.ErrorMessage);
            }
            else
            {
                if(objList.Count == 1)//ユーザー名が一つだけの時
                {
                    int cloudScore = System.Convert.ToInt32(objList[0]["Score"]);
                    if(cloudScore < score)
                    {
                        objList[0]["Score"] = score;
                        objList[0].SaveAsync();
                    }
                }else if(objList.Count <= 0)//ユーザー名が存在しない時
                {
                    objList[0]["Name"] = userName;
                    objList[0]["Score"] = score;
                    objList[0].SaveAsync();
                }
            }
        });
    }

    /// <summary>
    ///　ランキングボード用取得関数
    /// </summary>    
    public void FetchTopRankers()
    {
        // データストアの「ScoreRanking」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreRanking");
        query.OrderByDescending("Score");
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)
            {
                //検索失敗時の処理
                Debug.LogError("ランキング取得失敗しました。" + e.ErrorMessage);
            }
            else
            {
                //検索成功時の処理
                List<ScoreRanking> list = new List<ScoreRanking>();
                // 取得したレコードをScoreRankingクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    string n = System.Convert.ToString(obj["Name"]);
                    int s = System.Convert.ToInt32(obj["Score"]);              
                    list.Add(new ScoreRanking(n,s));
                }
                rankingList = list;
            }
        });
    }
    
}
