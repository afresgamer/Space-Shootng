using UnityEngine;

public class ObstableController : MonoBehaviour {

    //生成間隔
    [Header("生成間隔")]
    public float maxCreateTime = 2;
    //横幅の最大と最小
    const float MaxX = 5;
    const float MinX = -5;
    [SerializeField,Header("Poolオブジェクト")]
    private PoolController poolController;
    [Header("振り幅ありかどうか")]
    public bool IsSwingWidth = false;

    private void Start()
    {
        poolController.AttachPoint = transform;
        InvokeRepeating("Create", 1, maxCreateTime);
    }

    public void Create()
    {
        if (IsSwingWidth)
        {
            Vector3 point = new Vector3(Random.Range(MinX, MaxX), transform.position.y, transform.position.z);
            poolController.CreateObj(point, Quaternion.identity);
        }
        else
        {
            GameObject obj = poolController.GetPoolObj(transform.position, transform.rotation);
            obj.transform.SetParent(transform);
        }
    }



}
