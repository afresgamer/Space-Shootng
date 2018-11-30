using System.Collections;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

    [SerializeField, Header("Poolオブジェクト")]
    private PoolController poolController;
    [SerializeField, Header("発射間隔")]
    private float ShotInterval = 3.0f;
    bool isShot = false;
	
	void Update () {
        if(isShot) Shot();
	}

    void Shot()
    {
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        yield return new WaitForSeconds(ShotInterval);
        poolController.SetCreate();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        isShot = true;
    //        transform.parent.LookAt(collision.gameObject.transform);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        isShot = false;
    //        transform.parent.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}
}
