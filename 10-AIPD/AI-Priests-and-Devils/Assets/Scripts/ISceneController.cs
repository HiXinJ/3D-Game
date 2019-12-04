public enum GameState{win, defeat, playing};

public interface ISceneController
{
    void LoadResources();
    void DestoryResources();
    void Move(Role role);
    GameState MoveAll(out bool arrived);
    void solverMove();

    GameState  Defeat();
}
