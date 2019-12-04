using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FirstController:MonoBehaviour,ISceneController
{
    List<Priest> priestsList= new List<Priest>();
    List<Devil> devilsList =new List<Devil>();
    River river;
    Boat boat;
    Bank leftBank;
    Bank rightBank; 
    Node currentNode;
    Node endNode;
    List<Node> solutionPath;
    public CCActionManager actionManager;

    public void Awake(){
        SceneDirector.GetInstance().CSController = this;
        actionManager = GetComponent<CCActionManager>() as CCActionManager;
        
        BFSsolution solver = new BFSsolution();
        Node beginNode = new Node(3,3,true,3,3);
        currentNode = beginNode;
        endNode = new Node(0,0,false,3,3);
        this.solutionPath = solver.BFSearch(beginNode, endNode);

    }
    public  void Start(){
        // Debug.Log("Constructor");
        // actionManager = new CCActionManager();
        // Debug.Log(actionManager.GetInstanceID());
        // actionManager = new CCActionManager();
        // actionManager = GetComponent<CCActionManager>() as CCActionManager;
        // LoadResources();
    }
    public void LoadResources(){
        //Load Priests and Devils
        for(int i=0;i<3;i++)
        {
            Priest p = new Priest(new Vector3(13.5f+i*1.2f, 3.5f, 0));
            priestsList.Add(p);
            Devil d = new Devil(new Vector3(17.6f+2f*i,3.5f,0));
            devilsList.Add(d);
        }
        // Load river, boat, bank
        river = new River(River.riverPos);
        boat = new Boat(Boat.rightBoatPos);
        leftBank = new Bank(Bank.rightBankPos);
        rightBank = new Bank(Bank.leftBankPos);
    }

    public void DestoryResources()
    {
        
        leftBank.RemoveModel();
        rightBank.RemoveModel();
        river.RemoveModel();
        boat.RemoveModel();
        for(int i = 0; i< 3;i++)
        {
            priestsList[i].RemoveModel();
            devilsList[i].RemoveModel();
        }
        priestsList.Clear();
        devilsList.Clear();
    }

    public void solverMove(){
        Node nextNode = solutionPath[solutionPath.Count - 2];
        currentNode = solutionPath[solutionPath.Count-1];
        Debug.Log("current "+currentNode);
        Debug.Log("next "+nextNode);
        int moveDevil =Mathf.Abs(nextNode.devil - currentNode.devil);
        int movePriest =Mathf.Abs(nextNode.priest - currentNode.priest);
        for(int i = 0; i < priestsList.Count; i++)
        {
            if(currentNode.boat==true)
            {
                if(priestsList[i].GetState() == State.right && movePriest > 0)
                {
                    priestsList[i].model.GetComponent<Click>().OnMouseDown();
                    movePriest--;

                }
                if(devilsList[i].GetState() == State.right && moveDevil > 0)
                {
                    devilsList[i].model.GetComponent<Click>().OnMouseDown();
                    moveDevil--;

                }
            }
            else if(currentNode.boat == false)
            {
                if(priestsList[i].GetState() == State.left && movePriest > 0)
                {
                    priestsList[i].model.GetComponent<Click>().OnMouseDown();
                    movePriest--;
                }
                if(devilsList[i].GetState() == State.left && moveDevil > 0)
                {
                    devilsList[i].model.GetComponent<Click>().OnMouseDown();
                    moveDevil--;
                }
                
            }
        }
        solutionPath.RemoveAt(solutionPath.Count-1);
    }
    public void Move(Role role){
        //船在右边
        if(boat.model.transform.position.x >= Boat.rightBoatPos.x )
        {
            Vector3 toSeat=boat.GetSeat();
            int side = toSeat == boat.GetLeftSeatPos() ? 1:2;
            //人在右岸
            if(role.GetState() == State.right && toSeat!=Vector3.zero)
            {
                // role.Move(new Vector3(-1, 0, 0), 13);
                actionManager.moveRole(role.model, new Vector3(toSeat.x, 3.5f, 0), 13);
                boat.canMove = false;
                //到达目的地
                // if(role.model.transform.position.x <= toSeat.x)
                {
                    if(side == 1)
                        boat.seatable1 = false;
                    else
                        boat.seatable2 = false;                                       
                    role.SetState((side==1)?State.onBoat1:State.onBoat2);
                    role.isClicked = false;  
                    Role.clickable = true;
                    boat.canMove = true;
                }   
            } 
            //人在船上
            else if(role.GetState() == State.onBoat1 || role.GetState() == State.onBoat2)
            {
                // role.Move(Vector3.right, 13);//
                actionManager.moveRole(role.model, role.rightBankPos, 13);
                boat.canMove = false;
                // if(role.model.transform.position.x >= role.rightBankPos.x)
                {
                    if(role.GetState()==State.onBoat1)
                        boat.seatable1 = true;
                    else 
                        boat.seatable2 = true;

                    role.SetState(State.right);
                    role.isClicked = false;
                    Role.clickable = true;     
                    boat.canMove=true;               
                }
            }
            else{
                role.isClicked = false;
                Role.clickable = true;
            }     
        }

        //船在左边
        else if (boat.model.transform.position.x <= Boat.leftBoatPos.x )
        {
            Vector3 toSeat=boat.GetSeat();
            int side = toSeat == boat.GetLeftSeatPos() ? 1:2;
            //人在左岸
            if(role.GetState() == State.left && toSeat!=Vector3.zero)
            {
                // role.Move(Vector3.right, 13);
                actionManager.moveRole(role.model, new Vector3(toSeat.x, 3.5f, 0), 13);
                boat.canMove = false;
                //到达目的地
                // if(role.model.transform.position.x >= toSeat.x)
                {
                    if(side == 1)
                        boat.seatable1 = false;
                    else
                        boat.seatable2 = false;                    
                    
                    role.SetState((side==1)?State.onBoat1:State.onBoat2);
                    role.isClicked = false;  
                    Role.clickable = true;
                    boat.canMove=true;
                }   
            } 
            //人在船上
            else if(role.GetState() == State.onBoat1 || role.GetState() == State.onBoat2)
            {
                // role.Move(Vector3.left, 13);
                actionManager.moveRole(role.model, role.leftBankPos, 13);
                boat.canMove=false;
                // if(role.model.transform.position.x <= role.leftBankPos.x)
                {
                    if(role.GetState()==State.onBoat1)
                        boat.seatable1 = true;
                    else 
                        boat.seatable2 = true;

                    role.SetState(State.left);
                    role.isClicked = false;
                    Role.clickable = true;       
                    boat.canMove=true;             
                }
            }
            else{
                role.isClicked = false;
                Role.clickable = true;
            }
        }
    }

    //按下Go按钮后调用
    //船载着牧师与恶魔过河
    //return false if defeated
    public GameState MoveAll(out bool arrived)
    {
        arrived = false;

        if(!boat.canMove ||(boat.seatable1 && boat.seatable2))
        {
            arrived = true;
            return GameState.playing;
        }   
        Role.clickable = false;
        //确定方向
        Vector3 target = State.right == boat.GetState()? Boat.leftBoatPos:Boat.rightBoatPos;
        actionManager.moveBoat(boat.model, target, 25);
        for(int i = 0; i < 3; i++){
            if(priestsList[i].GetState() == State.onBoat1 ||priestsList[i].GetState() == State.onBoat2)
                    // priestsList[i].Move(direction, 20);
                    actionManager.moveRole(priestsList[i].model, new Vector3(priestsList[i].model.transform.position.x+2*target.x, 3.5f, 0), 25f);
            if(devilsList[i].GetState() == State.onBoat1 ||devilsList[i].GetState() == State.onBoat2)
                    // devilsList[i].Move(direction, 20);
                    actionManager.moveRole(devilsList[i].model, new Vector3(devilsList[i].model.transform.position.x+2*target.x, 3.5f, 0), 25f);
            // Debug.Log(priestsList[i].model.transform.position.x+2*target.x);
            // Debug.Log(devilsList[i].model.transform.position.x+2*target.x);
        }

        // if(target == Boat.leftBoatPos &&  boat.model.transform.position.x <= Boat.leftBoatPos.x ||
        //     target == Boat.rightBoatPos && boat.model.transform.position.x >= Boat.rightBoatPos.x)
        {
            arrived = true;
            boat.SetState(target == Boat.leftBoatPos?State.left:State.right);
            Role.clickable = true;
            return Defeat();
        } 
        return GameState.playing;
    }

    //return 1 if 牧师数量小于恶魔数量，失败
    public GameState  Defeat(){
        int nleftPriests = 0;
        int nleftDevils= 0;
        int nrightPriests = 0;
        int nrightDevils = 0;
        for(int i = 0; i < 3; i++)
        {
            if(priestsList[i].model.transform.position.x<0)
                nleftPriests++;
            else 
                nrightPriests++;
            
            if(devilsList[i].model.transform.position.x<0)
                nleftDevils++;
            else 
                nrightDevils++;
        }

        if((nrightDevils>nrightPriests&&nrightPriests!=0) || (nleftDevils>nleftPriests&&nleftPriests!=0))
            return GameState.defeat;
        else if(nrightPriests == 0 && nrightDevils == 0)
            return GameState.win;
        else 
            return GameState.playing;
    }
}
