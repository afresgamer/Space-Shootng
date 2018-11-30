using DG.Tweening;

public class HardEnemy : NormalEnemy {
    
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.PathMove(GetRootPath(rootPath).paths, PathType.CatmullRom);
    }
}
