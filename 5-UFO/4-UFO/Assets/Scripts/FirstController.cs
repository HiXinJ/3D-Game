using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus { GameOver, GameStart, GamePlaying };
public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public CCActionManager actionManager;
    public UFOFactory UFOfactory;


    // 玩家生命值。 如果未击落UFO，则生命值减1
    private int life = 5;

    // 出现在场景中的飞碟队列
    private Queue<GameObject> UFOList = new Queue<GameObject>();

    // 逃离飞碟的队列
    private List<GameObject> UFOFlyingList = new List<GameObject>();

    private int round = 1;

    //发射UFO的时间间隔
    private float sendInterval = 0.8f;

    // 定义3个游戏状态
    private bool gamePlaying = false;
    private bool gameOver = false;
    private GameStatus gameStatus;
    //每个round有10个trails
    private int trails = 10;
    private int scored = 0;

    void Awake()
    {
        SceneDirector director = SceneDirector.GetInstance();
        director.CSController = this;
    }
    void Start()
    {

        UFOfactory = Singleton<UFOFactory>.Instance;
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        StartGame();
    }

    void Update()
    {
        //游戏结束，取消定时发送飞碟
        if (gameStatus == GameStatus.GameOver)
        {
            CancelInvoke("LoadResources");
            return;
        }
        //设定一个定时器，发送飞碟，游戏开始
        else if (gameStatus == GameStatus.GameStart)
        {
            InvokeRepeating("LoadResources", 1f, sendInterval);
            gameStatus = GameStatus.GamePlaying;
        }
        SendUFO();
        UpdateLife();


        //进入下一回合增加难度
        if (trails == 0)
        {
            round++;
            trails = 10;
            CancelInvoke("LoadResources");
            gameStatus = GameStatus.GameStart;
            if (round > 3)
                sendInterval = 0.3f;
            else
                sendInterval = sendInterval - 0.2f;
        }
    }

    public void LoadResources()
    {
        UFOList.Enqueue(UFOfactory.GetUFO(round));
    }

    private void SendUFO()
    {
        // UFOList.Enqueue(UFOfactory.GetUFO(round)); 

        if (UFOList.Count != 0)
        {
            // GameObject ufo=UFOfactory.GetUFO(round);
            GameObject ufo = UFOList.Dequeue();//UFOfactory.GetUFO(round);
            UFOFlyingList.Add(ufo);
            ufo.SetActive(true);
            actionManager.SendUFO(ufo);
            trails--;
        }
    }


    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            //射线打中物体
            if (hit.collider.gameObject.GetComponent<UFOData>() != null)
            {
                for (int j = 0; j < UFOFlyingList.Count; j++)
                {
                    if (hit.collider.gameObject.GetInstanceID() == UFOFlyingList[j].gameObject.GetInstanceID())
                    {
                        UFOFlyingList.Remove(hit.collider.gameObject);
                        AddScore(hit.collider.gameObject);
                        UFOfactory.FreeUFO(hit.collider.gameObject);
                        return;
                    }
                }
            }
        }
    }

    public void AddScore(GameObject disk)
    {
        scored += disk.GetComponent<UFOData>().score;
    }

    public int GetScore()
    {
        return scored;
    }

    public void UpdateLife()
    {
        for (int i = 0; i < UFOFlyingList.Count; i++)
        {
            GameObject ufo = UFOFlyingList[i];
            //UFO没被打中
            if ((Mathf.Abs(ufo.transform.position.x) > 20 ||
                Mathf.Abs(ufo.transform.position.y) > 13) &&
                ufo.gameObject.activeSelf == true)
            {
                UFOfactory.FreeUFO(UFOFlyingList[i]);
                UFOFlyingList.Remove(UFOFlyingList[i]);
                life--;
            }
        }
    }

    public int GetLife()
    {
        return life;
    }

    public void StartGame()
    {
        gameStatus = GameStatus.GameStart;
    }
    public void ReStart()
    {
        foreach (GameObject ufo in UFOFlyingList)
            UFOfactory.FreeUFO(ufo);
        UFOFlyingList.Clear();
        UFOList.Clear();
        gameStatus = GameStatus.GameStart;
        scored = 0;
        round = 1;
        sendInterval = 2f;
        life = 5;
        trails = 10;
    }

    public void GameOver()
    {
        gameStatus = GameStatus.GameOver;
    }
}
