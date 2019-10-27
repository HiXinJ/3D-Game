using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{gameOver,playing};
public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public Factory patrol_factory;                               //巡逻者工厂
    // public ScoreRecorder recorder;                                   //记录员
    public CCActionManager action_manager;                       //运动管理器
    public int playerArea = -1;                                       //当前玩家所处哪个格子
    public GameObject player;                                        //玩家
    public Camera main_camera;                                       //主相机
    private List<GameObject> patrols;                                //场景中巡逻者列表
    public GameState gameState=GameState.playing;
    private int score = 0;
    void Awake()
    {
        SSDirector.GetInstance().CurrentScenceController = this;

    }

    void Start()
    {
        score=0;
        gameState=GameState.playing;

        patrol_factory = Singleton<Factory>.Instance;
        action_manager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        LoadResources();
        for (int i = 0; i < patrols.Count; i++)
            action_manager.GoPatrol(patrols[i]);
        main_camera.GetComponent<CameraFlow>().follow = player;
    }

    void Update()
    {
    }

    public void LoadResources()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-1, 0, 2), Quaternion.identity) as GameObject;
        player.AddComponent<JoyStick>();
        player.GetComponent<Collider>().enabled=true;
        patrols = patrol_factory.GetPatrols();
    }

    public int GetPlayerArea()
    {
        return playerArea;
    }

    public void Restart()
    {

    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public int GetScore()
    {
        return score;
    }

    void AddScore()
    {
        score++;
    }

    void GameOver()
    {
        gameState=GameState.gameOver;
        Destroy(player.GetComponent<JoyStick>());
    }
    void OnEnable()
    {
        PatrolCollide.escapeHandle += AddScore;
        PlayerCollide.gameOverHandle += GameOver;
    }
    void OnDisable()
    {
        PatrolCollide.escapeHandle -= AddScore;
        PlayerCollide.gameOverHandle -= GameOver;
    }
}