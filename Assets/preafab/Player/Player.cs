using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Client.Core;

[System.Serializable]
public struct SeaResource
{
    public static SeaResource operator +(SeaResource a, SeaResource b)
    {
        b.plastic += a.plastic;
        b.ePlastic += a.ePlastic;
        b.wood += a.wood;
        b.steel += a.steel;
        b.seaFood += a.seaFood;

        return b;
    }

    public static SeaResource operator -(SeaResource a, SeaResource b)
    {
        b.plastic -= a.plastic;
        b.ePlastic -= a.ePlastic;
        b.wood -= a.wood;
        b.steel -= a.steel;
        b.seaFood -= a.seaFood;

        return b;
    }

    public void SetNoraml(int size)
    {
        if (plastic < 0) plastic = 0;
        if (ePlastic < 0) ePlastic = 0;
        if (wood < 0) wood = 0;
        if (steel < 0) steel = 0;
        if (seaFood < 0) seaFood = 0;

        int allSize = plastic + ePlastic + wood + steel + seaFood;

        if (size < allSize)
        {
            if (plastic > allSize - size)
            {
                plastic -= allSize - size;
            }
            else
            {
                allSize -= seaFood;
                plastic = 0;

                if(ePlastic > allSize - size)
                {
                    ePlastic -= allSize - size;
                }
                else
                {
                    allSize -= seaFood;
                    ePlastic = 0;

                    if (wood > allSize - size)
                    {
                        wood -= allSize - size;
                    }
                    else
                    {
                        allSize -= seaFood;
                        wood = 0;

                        if (seaFood > allSize - size)
                        {
                            seaFood -= allSize - size;
                        }
                        else
                        {
                            allSize -= seaFood;
                            seaFood = 0;

                            steel -= allSize - size;  
                            
                        }
                    }
                }
            }
           
        }
    }


    public int plastic;
    public int ePlastic;
    public int wood;
    public int steel;
    public int seaFood;
}

public class Player : StrixBehaviour
{
    //�\���p�����[�^
    string playerName;
    [StrixSyncField]
    public int money = 1000;
    [StrixSyncField]
    int shipLevel;

    public List<Item> items;
    public MapIndex playerPos;
    public int speed = 1;

    public int resourceStack = 1200;
    public int getPower = 100;

    public int getDepth = 1;
    public int searchPower = 1;

    //�N���t�g�Ώ�
    int dieselEngine = 1;
    int shipBody = 1;
    int whaleMouse = 1;
    int crane = 1;
    int sonar = 1;

    //����
    [StrixSyncField]
    public SeaResource seaResource;

    //�����p�����[�^

    [StrixSyncField]
    public TurnState nowState;

    [StrixSyncField]
    public TurnState nextState;

    HexagonManger hexagonManger;

    public TurnContllor turnContllor;

    UIManager uiManager;

    [StrixSyncField]
    public int turnNum;

    [StrixSyncField]
    public bool isTurn;

    PlayerState playerState;

    //�X�g���N�X
    [StrixSyncField]
    UID ownerUid;

    public int getRVol = 0;

    public enum RpcFunctionName
    {
        INIT_PLAYER,
        SET_TURN,
        SET_WAIT,
        SET_MOVE_STATE,
        SET_COMAND_STATE,
        RANDAM_TELEPORT
    }

    Dictionary<RpcFunctionName, string> rpcFunctions = new Dictionary<RpcFunctionName, string>()
    {
        {RpcFunctionName.INIT_PLAYER , "Init"},
        {RpcFunctionName.SET_TURN, "SetTurn"},
        {RpcFunctionName.SET_WAIT, "SetWait"},
        {RpcFunctionName.SET_MOVE_STATE, "SetMoveState"},
        {RpcFunctionName.SET_COMAND_STATE , "SetComandState" },
        {RpcFunctionName.RANDAM_TELEPORT,"RandomTeleport" }
    };
    // Start is called before the first frame update
    void Start()
    {
        seaResource.ePlastic = 75;
        seaResource.plastic = 150;
        seaResource.seaFood = 0;
        seaResource.steel = 15;
        seaResource.wood = 45;

        FindObjectOfType<ResourceUI>().SetResource(seaResource);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal) return;

        if (nowState != nextState)
        {
            switch (nextState)
            {
                case TurnState.PLAYER_MOVE:
                    playerState = new MoveState();
                    playerState.TurnInit(this,hexagonManger,turnContllor,uiManager);
                    break;
                case TurnState.SELECT_COMAND:
                    playerState = new CommandState();
                    playerState.TurnInit(this, hexagonManger, turnContllor,uiManager);
                    break;
                case TurnState.HAPPNING_EVENT:
                    playerState = new HappningState();
                    playerState.TurnInit(this, hexagonManger, turnContllor, uiManager);
                    break;
            }
            nowState = nextState;
        }

        if (isTurn) playerState.TurnUpdate(this);
       
    }

    [StrixRpc]
    public void Init()
    {
        hexagonManger = FindObjectOfType<HexagonManger>();

        turnContllor = FindObjectOfType<TurnContllor>();

        uiManager = FindObjectOfType<UIManager>();

        nowState = TurnState.TURN_WAIT;
        nextState = TurnState.TURN_WAIT;

        SetWait();

        playerPos.x = 0;
        playerPos.y = 0;

        transform.position = hexagonManger.GetMapPos(playerPos);
    }

    public void CallRPCOwner(RpcFunctionName fName,params object[] param)
    {
        if (isLocal)
        {
            Invoke(rpcFunctions[fName], 0);
        }
        else
        {
            Rpc(strixReplicator.ownerUid, rpcFunctions[fName], param);

            Debug.Log(fName + "Called");
        }

    }

    public void CallRPCAll(RpcFunctionName fName, params object[] param)
    {
        RpcToAll(rpcFunctions[fName], param);
        Debug.Log(fName + "Called");
    }

    public void CallRPCRoomOwner(RpcFunctionName fName, params object[] param)
    {
        RpcToRoomOwner(rpcFunctions[fName], param);
        Debug.Log(fName + "Called");
    }

    [StrixRpc]
    public void SetTurn()
    {
        isTurn = true;
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    [StrixRpc]
    public void SetWait()
    {
        isTurn = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        nextState = TurnState.TURN_WAIT;
    }

    [StrixRpc]
    public void SetMoveState()
    {
        nextState = TurnState.PLAYER_MOVE;
    }

    [StrixRpc]
    public void SetComandState()
    {
        nextState = TurnState.SELECT_COMAND;

        Debug.Log("SetComandState");
    }

    [StrixRpc]
    public void RandomTeleport()
    {
        MapIndex mapIndex;
        mapIndex.x = Random.Range(0, hexagonManger.GetMapScale().x);
        mapIndex.y = Random.Range(0, hexagonManger.GetMapScale().y);

        playerPos = mapIndex;
        transform.position = hexagonManger.GetMapPos(mapIndex);
    }

    public TurnState GetNextState()
    {
        return nextState;
    }

    public void AddSeaResouce(int step)
    {
        SeaResource add;
        add.plastic     = (int)(0.45 * getPower * step);
        add.ePlastic    = (int)(0.25 * getPower * step);
        add.wood        = (int)(0.15 * getPower * step);
        add.steel       = (int)(0.5 * getPower * step);
        add.seaFood     = (int)(0.10 * getPower * step);

        seaResource = seaResource + add;
        seaResource.SetNoraml(resourceStack);
    }
}