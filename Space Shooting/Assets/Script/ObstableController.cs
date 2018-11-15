using UnityEngine;

public class ObstableController : MonoBehaviour {

    [SerializeField, Header("生成間隔")]
    private int MaxCreateTime = 1;
    //横幅の最大と最小
    const float MaxX = 5;
    const float MinX = -5;
    [SerializeField,Header("Poolオブジェクト")]
    private PoolController poolController;

    private void Awake()
    {
        poolController.AttachPoint = transform;
        InvokeRepeating("Create", MaxCreateTime, 1);
    }

    public void Create()
    {
        Vector3 point = new Vector3(Random.Range(MinX, MaxX), transform.position.y, transform.position.z);
        poolController.CreateObj(point, Quaternion.identity);
    }

}
