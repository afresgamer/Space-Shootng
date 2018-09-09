using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    [SerializeField, Header("スクロールスピード")]
    private float Scrollspeed = 0.1f;


    void Update () {
        transform.Translate(0, -Scrollspeed, 0);
        if(transform.position.y < -10)
        {
            transform.position = Vector3.zero;
        }
	}
}
