using UnityEngine;

public class NormalEnemy : EnemyBase {

    [Header("通過するポイント")]
    public RootPath[] rootPath;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    
    public virtual void OnEnable()
    {
        base.PathMove(GetRootPath(rootPath).paths,DG.Tweening.PathType.Linear);
    }

    /// <summary>
    /// ランダム経路選択
    /// </summary>
    /// <param name="rootPath"></param>
    /// <returns></returns>
    public RootPath GetRootPath(RootPath[] rootPath)
    {
        int ranNum = Random.Range(0, rootPath.Length);
        return rootPath[ranNum];
    }
}
