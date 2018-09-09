using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> poolList;
    private int currentCount;
    private int maxCount;
    private Vector3 originPos;
    private Quaternion originRot;

    public ObjectPool(int _max)
    {
        maxCount = _max;
        currentCount = 0;
        originPos = Vector3.zero;
        originRot = Quaternion.identity;
        poolList = new List<GameObject>();
    }

    public ObjectPool(int _max, Vector3 _position, Quaternion _quaternion)
    {
        maxCount = _max;
        currentCount = 0;
        originPos = _position;
        originRot = _quaternion;
        poolList = new List<GameObject>();
    }

    /// <summary>
    /// オブジェクトをプールする。
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="obj"></param>
    /// <param name="num"></param>
    public void Pool(GameObject parent, GameObject obj, int num)
    {
        int count = num;
        if (num > maxCount) { count = maxCount; }
        for (int i = 0; i < count; i++)
        {
            var poolObj = Object.Instantiate(obj, originPos, originRot);
            if (parent != null) { poolObj.transform.SetParent(parent.transform); }
            poolObj.SetActive(false);
            poolList.Add(poolObj);
        }

        Debug.Log(poolList.Count + "個生成しました");
    }

    /// <summary>
    /// プールしたオブジェクトを返す
    /// </summary>
    /// <returns></returns>
    public GameObject GetPool()
    {
        //Debug.Log ("Poolから取得します。");
        if (poolList == null) { return null; }

        GameObject returnObj = poolList[currentCount];
        returnObj.transform.position = originPos;
        returnObj.transform.rotation = originRot;
        currentCount++;
        if (currentCount >= poolList.Count) { currentCount = 0; }
        returnObj.SetActive(true);

        return returnObj;
    }

    /// <summary>
    /// 位置情報を更新してプールしたオブジェクトを返す
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public GameObject GetPool(Vector3 pos, Quaternion rot)
    {
        GameObject returnObj = poolList[currentCount];
        if (returnObj == null) { return null; }
        returnObj.transform.position = pos;
        returnObj.transform.rotation = rot;
        currentCount++;
        if (currentCount >= poolList.Count) { currentCount = 0; }
        returnObj.SetActive(true);

        return returnObj;
    }
}