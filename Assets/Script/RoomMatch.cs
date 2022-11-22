using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class RoomMatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool CheckAllRoomMembersState(int desiredState)//�����o�[�S���̏�Ԃ��m�F����֐�     ��Strix�̃T���v���R�[�h��
    {
        foreach (var roomMember in StrixNetwork.instance.roomMembers)
        {
            if (!roomMember.Value.GetProperties().TryGetValue("state", out object value))
            {
                return false;
            }

            if ((int)value != desiredState)
            {
                return false;
            }
        }

        Debug.Log("CheckAllRoomMembersState OK");
        return true;
    }

    public void CheckMenberState()
    {
        bool state = CheckAllRoomMembersState(0);

        Debug.Log("state:"+state);
    }

    public void CheckChangeState()
    {
        var StrixSelf = StrixNetwork.instance.selfRoomMember;
        
        var dic = new Dictionary<string,object>();
        dic.Add("state", 1);
        StrixSelf.SetProperties(dic);//state�̏㏑��

        StrixSelf.GetProperties().TryGetValue("state", out object value);

      
        Debug.Log("Now State :" +value);
    }

    //*******************�������牺*******************

    private void testEnterRoom_self()//���[���ɓ��������ۂɌĂяo��      �V���ɃX�e�[�g���v���p�e�B���ɍ��A���̃X�e�[�g�ŏ������������f����
    {
        // ���[�������o�[�̏�Ԃ�ύX���܂�: 0�́u�������v�A1�́u���������v
        StrixNetwork.instance.SetRoomMember(
            StrixNetwork.instance.selfRoomMember.GetPrimaryKey(),//�������g�̏�Ԃ�ύX
            new Dictionary<string, object>() {
                { "properties", new Dictionary<string, object>()
                    {
                        { "state", 0 }
                    }
                }
            },
            args => {
                Debug.Log("SetRoomMember succeeded");
            },
            args => {
                Debug.Log("SetRoomMember failed. error = " + args.cause);
            }
        );
    }

    private void testChangeState_self()//�{�^���������ŏ���������ԂɕύX����֐�
    {
        var dic = new Dictionary<string, object>();
        dic.Add("state", 1);
        StrixNetwork.instance.selfRoomMember.SetProperties(dic);//state�̏㏑��    ��������
    }

    private void testAllCheckState_Master()//���[���}�X�^�[�������o�[�S���̃X�e�[�g���m�F����֐�     ���ƓK��
    {
        var StrixMember = StrixNetwork.instance;

        bool gameStart = CheckAllRoomMembersState(StrixMember.room.GetMemberCount());
        if (gameStart == false)
            return;

        Debug.Log("�����o�[�S������������");
    }
}
