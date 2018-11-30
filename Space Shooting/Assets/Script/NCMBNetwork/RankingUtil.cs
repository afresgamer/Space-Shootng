using UnityEngine;
using NCMB;
using System.Collections.Generic;

public class RankingUtil : SingletonMonoBehaviour<RankingUtil> {

    //ランキング用保存リスト
    [HideInInspector]
    public List<ScoreRanking> rankingList = new List<ScoreRanking>();

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

    public void FetchRanking(string userName, int score)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreRanking");
        query.WhereEqualTo("Name", userName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if(e != null)//検索失敗時の処理
            {
                Debug.LogError(userName + "名前のユーザーは存在しません。新規登録かお名前の確認をお願いします。\n" + e.ErrorMessage);
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
                }else if(objList.Count <= 0)
                {
                    objList[0]["Name"] = userName;
                    objList[0]["Score"] = score;
                    objList[0].SaveAsync();
                }
            }
        });
    }
    
}
