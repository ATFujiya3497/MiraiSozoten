//==============================================================
//
//   Title:  CameraManager
//   Writer: Ryuidhi Saito
//   Date:   10/14
//
//   Overview: Cameracontroller���R���|�[�l���g�Ɏ����Ă���
//             �J�������Ǘ�����N���X�ł��B
//==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private List<GameObject> Cameras = new List<GameObject>();  // �J�����̃��X�g
    private int NowCamera;                                      // ���ݎʂ��Ă���J�����̔ԍ�

    // Start is called before the first frame update
    void Start()
    {
        // �Ƃ肠����0�ŏ�����
        NowCamera = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �J�����S��~
    public void CameraAllInactive()
    {
        for (int i = 0; i < Cameras.Count; i++)
        {
            Cameras[i].SetActive(false);
        }
    }

    // �J���������X�g�ɃZ�b�g
    // 
    // ���� :camera  �J������GameObject
    //
    public int AddCameras(GameObject camera)
    {
        // �J�������A�N�e�B�u�����ă��X�g�ɒǉ�
        camera.SetActive(false);
        Cameras.Add(camera);

        // ���X�g�̃J������-1�̐��l���Ǘ��ԍ��Ƃ��ĕԂ�
        return Cameras.Count - 1;
    }

    // �ʂ��J�������Z�b�g
    // 
    // ���� :num  CameraController�N���X�������Ă���CameraID
    //
    public void CameraActive(int num)
    {
        // �J������1���Ȃ��ꍇ���ɓ���Ȃ�
        if (Cameras.Count <= 0) return;

        // ���ݓ����Ă���J�������A�N�e�B�u��
        Cameras[NowCamera].SetActive(false);

        // �V���Ɏʂ��J�������A�N�e�B�u��
        Cameras[num].SetActive(true);
        NowCamera = num;
    }
}
