using UnityEngine;

[CreateAssetMenu(fileName = "Path")]
public class RootPath : ScriptableObject {

    [Header("通過ポイント")]
    public GameObject[] paths;
}
