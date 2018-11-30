using UnityEngine;
using DG.Tweening;

public class BossBulletController : MonoBehaviour {

    //goto NWaySHOT　を実装　一定間隔で揺れるようにする(出来たら)
    [Header("ボス発射オブジェクト")]
    public ObstableController[] obstableController;

	void Start () {
        for(int i = 0; i < obstableController.Length; i++)
        {
            obstableController[i].transform.rotation = 
                Quaternion.Euler(0, 0, (-10 * obstableController.Length) + (10 * obstableController.Length * i));
        }
	}
    
}
