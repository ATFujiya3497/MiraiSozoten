//==============================================================
//
//   Title:  CameraController
//   Writer: Ryuidhi Saito
//   Date:   10/14
//
//   Overview: �J�����̋@�\���L�q����N���X�ł��B
//             
//==============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int CameraID;                 // �J�����Ǘ��ԍ�
    private CameraManager cameraManager;  // �J�����}�l�[�W��

    // Start is called before the first frame update
    void Start()
    {
        // �J�����}�l�[�W���ɃJ������o�^
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        CameraID = cameraManager.AddCameras(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �J�������A�N�e�B�u�ɂ���
    //
    // �J�������؂�ւ�鎞�ɌĂ�
    public void CameraActive()
    {
        cameraManager.CameraActive(CameraID);
    }
}
