using UnityEngine;

public class PoolController : MonoBehaviour {

    [SerializeField, Header("Poolするオブジェクト")]
    private GameObject obj;
    [SerializeField, Header("生成最大数")]
    private int MaxCount = 5;
    private Transform attachPoint;
    public Transform AttachPoint { get { return attachPoint; } set { attachPoint = value; } }

    private ObjectPool objectPool;

    private void Awake()
    {
        if(attachPoint != null)
        {
            objectPool = new ObjectPool(MaxCount, attachPoint.position, attachPoint.rotation);
        }
        else { objectPool = new ObjectPool(MaxCount); }
        objectPool.Pool(null, obj, MaxCount);
    }

    /// <summary>
    /// 生成条件決定処理
    /// </summary>
    /// <returns></returns>
    public bool IsCreate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;
        return false;
    }

    /// <summary>
    /// 生成処理
    /// </summary>
    public void SetCreate()
    {
        CreateObj(attachPoint.position, attachPoint.rotation);
    }

    /// <summary>
    /// オブジェクトプール処理
    /// </summary>
    public void CreateObj(Vector3 pos ,Quaternion rot)
    {
        objectPool.GetPool(pos,rot);
    }

    public GameObject GetPoolObj(Vector3 pos, Quaternion quaternion)
    {
        return objectPool.GetPool(pos, quaternion);
    }
}
