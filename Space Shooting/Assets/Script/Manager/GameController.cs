using UniRx;

public class GameController : SingletonMonoBehaviour<GameController> {

    /// <summary>
    /// ゲーム難易度
    /// </summary>
	public enum GameDifficulty { Easy = 1 , Normal , Hard }
    private GameDifficulty gameDifficulty = GameDifficulty.Easy;
    public GameDifficulty GetGameDifficulty { get { return gameDifficulty; } }

    //ゲームステージ表示のReactiveProperty(値の変更を通知してくれる変数)
    public ReactiveProperty<int> GameStateProperty { get; set; }
    //最大ステージ数
    const int MaxGameState = 5;

    /// <summary>
    /// パラメータ初期化
    /// </summary>
    public void InitGameStateProperty()
    {
        GameStateProperty = new ReactiveProperty<int>(0);
    }

    /// <summary>
    /// ステージ表示の判定処理
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public bool IsNextState(int score)
    {
        if (score > 0)
        {
            if(score % 100  == 0) return true;
        }
        return false;
    }

    /// <summary>
    /// 次のステート数を返す
    /// </summary>
    /// <param name="reactiveProperty"></param>
    /// <returns></returns>
    public int SetGameState(ReactiveProperty<int> reactiveProperty)
    {
        if (MaxGameState <= reactiveProperty.Value) { return MaxGameState; }
        else { reactiveProperty.Value += 1; }
        return reactiveProperty.Value;
    }

    /// <summary>
    /// ゲーム難易度設定(番号)
    /// </summary>
    /// <param name="difficulty"></param>
    public void SetGameDifficulty(int difficulty)
    {
        gameDifficulty = (GameDifficulty)difficulty;
    }

    /// <summary>
    /// ゲーム難易度設定(列挙型)
    /// </summary>
    /// <param name="difficulty"></param>
    public void SetGameDifficulty(GameDifficulty difficulty)
    {
        gameDifficulty = difficulty;
    }

}
