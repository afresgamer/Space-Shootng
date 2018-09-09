using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour {

    public enum PoolType { Player, Enemy }
    [Header("Poolタイプ")]
    public PoolType poolType;
    [SerializeField, Header("Poolするオブジェクト")]
    private GameObject obj;
    [SerializeField, Header("生成最大数")]
    private int MaxCount = 5;
    private Transform attachPoint;
    public Transform AttachPoint { get { return attachPoint; } set { attachPoint = value; } }

    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = new ObjectPool(MaxCount, attachPoint.position, attachPoint.rotation);
        objectPool.Pool(null, obj, MaxCount);
    }

    /// <summary>
    /// 生成条件処理(生成する条件を増やす場合はここを修正)
    /// </summary>
    /// <returns></returns>
    public bool IsCreate()
    {
        if (poolType == PoolType.Player && Input.GetKeyDown(KeyCode.Space)) return true;

        return false;
    }

    /// <summary>
    /// 生成処理
    /// </summary>
    public void CreateObj(Vector3 pos ,Quaternion rot)
    {
        objectPool.GetPool(pos,rot);
    }
}
