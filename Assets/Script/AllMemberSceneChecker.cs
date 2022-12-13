using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class AllMemberSceneChecker : MonoBehaviour
{
    [SerializeField] private int SceneNum;
    private bool isCheck;

    // Start is called before the first frame update
    void Start()
    {
        isCheck = false;


        //���݂̃V�[���ԍ���StrixNetwork.instance�̒ǉ��v���p�e�B�unowScene�v�ɕۑ��i�O��Ƃ��āA���[���쐬���ɒǉ��v���p�e�B��ݒ肵�Ă���j
        //���̏����́A�V�[���ړ�����ł���΁A���̃X�N���v�g����ł������\
        StrixNetwork.instance.SetRoomMember(
          StrixNetwork.instance.selfRoomMember.GetPrimaryKey(),
          new Dictionary<string, object>(){
                {"properties",new Dictionary<string,object>(){
                    {"nowScene",2 }
                } }
              },
          args =>
          {
              Debug.Log("������Ԃ�ύX���܂���");
          },
          args =>
          {
              Debug.Log("������Ԃ̕ύX�Ɏ��s���܂����Berror = " + args.cause);
          }
          );

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAllMemberinGameScene(SceneNum) && !isCheck)
        {
            Debug.Log("�S�v���C���[���Q�[���V�[���Ɉړ����܂����B");
            isCheck = true;
        }
    }

    private bool CheckAllMemberinGameScene(int num)
    {
        //���݂̑S���[�������o�[���Q��

        var A = StrixNetwork.instance.roomMembers;

        int membercount = 1;

        foreach (var roomMember in A)
        {
            membercount++;
            //�����������̃v���p�e�B���Ȃ������玸�s
            if (!roomMember.Value.GetProperties().TryGetValue("nowScene", out object value))
            {
                Debug.Log("�v���p�e�B�unowScene�v������܂���ł����B");
                return false;

            }

            //���̃v���p�e�B�����Ғl����Ȃ������玸�s
            if ((int)value != num)
            {
                Debug.Log("�l��" + (int)value);
                return false;
            }



        }

        return true;
    }

}
