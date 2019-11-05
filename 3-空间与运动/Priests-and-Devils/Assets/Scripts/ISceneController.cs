public interface ISceneController
{
    void LoadResources();
    void DestoryResources();
    void Move(Role role);
    GameState MoveAll(out bool arrived);
}
