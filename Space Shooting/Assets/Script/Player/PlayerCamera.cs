using System.Collections;
using System.Collections.Generic;
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
        transform.position = Vector3.Lerp(transform.position, targetdistance, smoothing * Time.deltaTime);

        Mathf.Clamp(transform.position.x, -2.5f, 2.5f);
    }
}
