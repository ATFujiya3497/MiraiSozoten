using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine.Events;

public class EnterRoom_Strix : MonoBehaviour
{
    /// <summary>
    /// ���[���ɎQ���\�ȍő�l��
    /// </summary>
    public int capacity = 4;

    /// <summary>
    /// ���[����
    /// </summary>
    public string roomName = "New Room";

    /// <summary>
    /// ���[�������������C�x���g
    /// </summary>
    public UnityEvent onRoomEntered;

    /// <summary>
    /// ���[���������s���C�x���g
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

    //�����ɃG���g���[
    public void EnterRoom()
    {
        StrixNetwork.instance.JoinRandomRoom(StrixNetwork.instance.playerName,
            args => {
                onRoomEntered.Invoke();
            }, args => {
                CreateRoom();
            });
    }


    public void CreateRoom()
    {
        RoomProperties roomProperties = new RoomProperties
        {
            name = roomName,
            capacity = 4,
            key1 = 0
        };

        RoomMemberProperties memberProperties = new RoomMemberProperties
        {
            name = StrixNetwork.instance.playerName,
            properties = new Dictionary<string, object>(){
                    {"nowScene",0 }
                }
        };

        StrixNetwork.instance.CreateRoom(
          roomProperties,
           memberProperties,
            args =>
            {
                onRoomEntered.Invoke();
                Debug.Log("�����̍쐬�ɐ������܂����B");
            },
            args =>
            {
                Debug.Log("�����̍쐬�Ɏ��s���܂����Berror=" + args.cause);
                onRoomEnterFailed.Invoke();
            }

            );


    }

    public void RoomStatusInit()
    {
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
