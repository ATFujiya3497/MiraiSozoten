using System.Collections;
using System.Collections.Generic;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class F_StrixEntreRoom : MonoBehaviour
{
    /// <summary>
    /// ルームに参加可能な最大人数
    /// </summary>
    public int capacity = 4;

    /// <summary>
    /// ルーム名
    /// </summary>
    public string roomName = "New Room";

    /// <summary>
    /// ルーム入室完了時イベント
    /// </summary>
    public UnityEvent onRoomEntered;

    /// <summary>
    /// ルーム入室失敗時イベント
    /// </summary>
    public UnityEvent onRoomEnterFailed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //部屋の作成
    public void CreateRoom()
    {
        RoomProperties roomProperties = new RoomProperties
        {
            name = roomName,
            capacity = 4,
            key1 = 0,
            key2 = 0,
            key3 = 0,
        };

        RoomMemberProperties memberProperties = new RoomMemberProperties
        {
            name = StrixNetwork.instance.playerName,
            properties = new Dictionary<string, object>(){
                    {"state",0 },
                    {"nowScene",0 },

                }
        };

        //部屋作成（自前改造）
        StrixNetwork.instance.CreateRoom(
          roomProperties,
           memberProperties,
            args =>
            {
                onRoomEntered.Invoke();
                Debug.Log("部屋の作成に成功しました。");

                RoomStatusInit();
            },
            args =>
            {
                Debug.Log("部屋の作成に失敗しました。error=" + args.cause);
                onRoomEnterFailed.Invoke();
            }
            );


    }


    public void RoomStatusInit()
    {
        //部屋IDを持つ
        StrixNetwork.instance.SetRoom(
            roomId: StrixNetwork.instance.roomSession.room.GetPrimaryKey(),   // The ID of the current room
                    roomProperties: new RoomProperties
                    {
                        name = roomName,
                        capacity = 4,
                        key1 = 0,
                        key2 = 0,
                        key3 = StrixNetwork.instance.selfRoomMember.GetRoomId()


                    },
                    handler: null,  // Printing the new capacity
                    failureHandler: null
                );
        Debug.Log("ルームID：" + StrixNetwork.instance.roomSession.room.GetPrimaryKey());

        //クライアントの部屋情報を設定する
        StrixNetwork.instance.SetRoomMember(
          StrixNetwork.instance.selfRoomMember.GetPrimaryKey(),
          new Dictionary<string, object>(){
                {"properties",new Dictionary<string,object>(){
                    {"nowScene",1 },
                    {"state",0 }
                } }
              },
          args =>
          {
          },
          args =>
          {
          }
          );
    }
}
