using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class HexagonSpreader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OwnerCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OwnerCheck()
    {
        //���[���I�[�i�[�ȊO�͂��̃I�u�W�F�N�g��j��
        var strixNetwork = StrixNetwork.instance;
        if (!strixNetwork.isRoomOwner)
        {
            Destroy(gameObject);
            Debug.Log("���[���I�[�i�[�ȊO��HexagonSpreader���������܂����B");
        }
    }
}