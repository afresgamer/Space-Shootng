using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController> {

    /// <summary>
    /// ゲーム難易度
    /// </summary>
	public enum GameDifficulty { Easy = 1 , Normal , Hard }
    private GameDifficulty gameDifficulty = GameDifficulty.Easy;
    public GameDifficulty GetGameDifficulty { get { return gameDifficulty; } }
    
    [HideInInspector]
    public int GameState = 1;
    //最大ステージ数
    const int MaxGameState = 5;

    /// <summary>
    /// ステート数を上昇条件
    /// </summary>
    /// <returns></returns>
    public bool IsState()
    {
        if (PlayerStatus.Instance.Score % 100 == 0) return true;
        return false;
    }

    /// <summary>
    /// 次のステート数を返す
    /// </summary>
    /// <param name="gameState"></param>
    /// <returns></returns>
    public int SetGameState(int gameState)
    {
        if(MaxGameState >= gameState) { return MaxGameState; }
        return gameState + 1;
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
