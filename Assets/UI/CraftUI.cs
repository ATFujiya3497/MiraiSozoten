using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    Player player;

    // �X�e�[�^�X�\���p�e�L�X�g
    Text NowSpeedText;
    Text AfterSpeedText;

    Text NowLadingText;
    Text AfterLadingText;

    Text NowSalvageText;
    Text AfterSalvageText;

    Text NowSalvageDepthText;
    Text AfterSalvageDepthText;

    Text NowRaderText;
    Text AfterRaderText;

    // DieselEngineUpgrade�̃A�b�v�f�[�g�̃��X�g
    [SerializeField] List<int> DieselPalamUpList;// = new List<int>() { 0, 2, 3, 5, 6, 7, 9, 11, 13, 15 };
    [SerializeField] List<Vector3Int> BodyPalamUpList;
    [SerializeField] List<Vector3> WhaleMousePalamUpList;
    [SerializeField] List<float> CranePalamUpList;
    [SerializeField] List<int> SonarPalamUpList;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainPlayer").GetComponent<Player>();

        if (player == null)
        {
            Debug.Log("MainPlayer NULL");
        }

        GameObject Centerui = this.gameObject.transform.Find("CenterUIPanel").gameObject;
        GameObject UpdateView = Centerui.transform.Find("CraftUIPanel").gameObject.transform.Find("StatusBackground").gameObject.transform.Find("UpdateView").gameObject;

        if (UpdateView == null)
        {
            Debug.Log("UpdateView NULL");
        }

        GameObject SpeedText = UpdateView.gameObject.transform.Find("UnderLine_Speed").gameObject;
        NowSpeedText = SpeedText.transform.Find("Speed_NowStatus").GetComponent<Text>();
        AfterSpeedText = SpeedText.transform.Find("Speed_NextStatus").GetComponent<Text>();

        if (SpeedText == null)
        {
            Debug.Log("SpeedText NULL");
        }
        //NowSpeedText.text = player.speed.ToString();

        GameObject LadingText = UpdateView.gameObject.transform.Find("UnderLine_Lading").gameObject;
        NowLadingText = LadingText.transform.Find("Lading_NowStatus").GetComponent<Text>();
        AfterLadingText = LadingText.transform.Find("Lading_NextStatus").GetComponent<Text>();

        if (LadingText == null)
        {
            Debug.Log("LadingText NULL");
        }

        GameObject SalvageText = UpdateView.gameObject.transform.Find("UnderLine_Salvage").gameObject;
        NowSalvageText = SalvageText.transform.Find("Salvage_NowStatus").GetComponent<Text>();
        AfterSalvageText = SalvageText.transform.Find("Salvage_NextStatus").GetComponent<Text>();

        if (SalvageText == null)
        {
            Debug.Log("SalvageText NULL");
        }

        GameObject SalvageDepthText = UpdateView.gameObject.transform.Find("UnderLine_SalvageDepth").gameObject;
        NowSalvageDepthText = SalvageDepthText.transform.Find("SalvageDepth_NowStatus").GetComponent<Text>();
        AfterSalvageDepthText = SalvageDepthText.transform.Find("SalvageDepth_NextStatus").GetComponent<Text>();

        if (SalvageDepthText == null)
        {
            Debug.Log("SalvageDepthText NULL");
        }

        GameObject RaderText = UpdateView.gameObject.transform.Find("UnderLine_Rader").gameObject;
        NowRaderText = RaderText.transform.Find("Rader_NowStatus").GetComponent<Text>();
        AfterRaderText = RaderText.transform.Find("Rader_NextStatus").GetComponent<Text>();

        if (RaderText == null)
        {
            Debug.Log("RaderText NULL");
        }

        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DieselEngineUpgrade()
    {
        //������
        if (true)
        {
            if (player.dieselEngine < DieselPalamUpList.Count-1)
            {
                // �������x���A�b�v
                player.dieselEngine++;

                //��������

                //�p�����[�^����
                player.speed += DieselPalamUpList[player.dieselEngine - 1];

                // UI�\��(��)
                NowSpeedText.text = player.speed.ToString();

                float afterSpeedStatus = player.speed + DieselPalamUpList[player.dieselEngine];
                AfterSpeedText.text = afterSpeedStatus.ToString();


                Debug.Log("�G���W������");
            }

        }
        else//���s��
        {

        }
    }

    public void ShipBodyUpgrade()
    {
        //������
        if (true)
        {
            if(player.shipBody< BodyPalamUpList.Count-1)
            {
                // �������x���A�b�v
                player.shipBody++;

                //��������
                //�p�����[�^����
                // �X�s�[�h
                player.speed += BodyPalamUpList[player.dieselEngine - 1].x;

                // �ύڗ�
                player.resourceStack += BodyPalamUpList[player.dieselEngine - 1].y;

                // UI�\���X�V
                NowSpeedText.text = player.speed.ToString();

                float afterSpeedStatus = player.speed + DieselPalamUpList[player.dieselEngine];
                AfterSpeedText.text = afterSpeedStatus.ToString();

                NowLadingText.text = player.resourceStack.ToString();
                float afterStackText = player.resourceStack + BodyPalamUpList[player.dieselEngine].y;
                AfterLadingText.text = afterStackText.ToString();
            }            
        }
        else//���s��
        {

        }
        Debug.Log("�D�̋���");
    }

    public void WhaleMouseUpgrade()
    {
        //������
        if (true)
        {
            if(player.whaleMouse< WhaleMousePalamUpList.Count-1)
            {
                // �������x���A�b�v
                player.whaleMouse++;

                //��������
                //�p�����[�^����
                // �X�s�[�h
                player.speed += (int)WhaleMousePalamUpList[player.whaleMouse - 1].x;

                // �����
                float power = player.getPower;
                player.getPower = (int)(power * WhaleMousePalamUpList[player.whaleMouse - 1].y);

                // UI�\��(��)
                // �X�s�[�h
                NowSpeedText.text = player.speed.ToString();

                float afterSpeedStatus = player.speed + WhaleMousePalamUpList[player.whaleMouse].x;
                AfterSpeedText.text = afterSpeedStatus.ToString();

                // �����
                NowSalvageText.text = player.getPower.ToString();

                float AfterText = player.getPower + WhaleMousePalamUpList[player.whaleMouse].y;
                AfterSalvageText.text = AfterText.ToString();
            }            
        }
        else//���s��
        {

        }
        Debug.Log("�����g���ʋ���");
    }

    public void CraneUpgrade()
    {
        //������
        if (true)
        {
            if(player.crane< CranePalamUpList.Count-1)
            {
                // �������x���A�b�v
                player.crane++;

                //��������
                //�p�����[�^����
                float power = player.getPower;
                player.getPower = (int)(power * CranePalamUpList[player.crane - 1]);

                // UI�\���X�V
                NowSalvageText.text = player.getPower.ToString();

                float AfterText = player.getPower + CranePalamUpList[player.crane];
                AfterSalvageText.text = AfterText.ToString();
            }            
        }
        else//���s��
        {

        }
        Debug.Log("�N���[������");
    }

    public void SonarUpgrade()
    {
        //������
        if (true)
        {
            if(player.sonar< SonarPalamUpList.Count-1)
            {
                // �������x���A�b�v
                player.sonar++;

                //��������
                //�p�����[�^����
                player.searchPower += SonarPalamUpList[player.sonar - 1];

                // UI�\��(��)
                NowRaderText.text = player.searchPower.ToString();

                float AfterText = player.searchPower + SonarPalamUpList[player.sonar];
                AfterRaderText.text = AfterText.ToString();
            }            
        }
        else//���s��
        {

        }
        Debug.Log("�\�i�[����");
    }

    // �N���t�g���n�܂鎞�ɌĂԏ���������
    void Init()
    {
        // �X�s�[�h�̃X�e�[�^�X�\��
        NowSpeedText.text = player.speed.ToString();

        float afterSpeedStatus = player.speed + DieselPalamUpList[player.dieselEngine];
        AfterSpeedText.text = afterSpeedStatus.ToString();

        // �ύڗʂ̃X�e�[�^�X�\��
        NowLadingText.text = player.resourceStack.ToString();

        float afterStackText = player.resourceStack + BodyPalamUpList[player.dieselEngine].y;
        AfterLadingText.text = afterStackText.ToString();

        // ����ʂ̃X�e�[�^�X�\��
        NowSalvageText.text = player.getPower.ToString();

        float afterSalvageText = player.getPower + CranePalamUpList[player.crane];
        AfterSalvageText.text = afterSalvageText.ToString();

        // �T�m�͂̃X�e�[�^�X�\��
        NowRaderText.text = player.searchPower.ToString();

        float AfterText = player.searchPower + SonarPalamUpList[player.sonar];
        AfterRaderText.text = AfterText.ToString();
    }
}
