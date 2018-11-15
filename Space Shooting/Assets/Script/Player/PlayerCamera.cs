using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField, Header("追従ターゲット")]
    private Transform Target;
    [SerializeField, Header("スピード")]
    private float smoothing = 1;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Target.position;
    }

    void Update()
    {
        Vector3 targetdistance = Target.position + offset;
        targetdistance.x = Mathf.Clamp(targetdistance.x, -2.5f, 2.5f);
        targetdistance.y = Mathf.Clamp(targetdistance.y, -10, 20);
        transform.position = Vector3.Lerp(transform.position, targetdistance, smoothing * Time.deltaTime);
        
    }
}
