using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI_MainSubjectController : MonoBehaviour
{
    GameObject SubjectCursol;
    GameObject IconFragment;
    RectTransform CursolTransform;
    int SubjectNum;
    [SerializeField] CraftUI craftUI;

    // Start is called before the first frame update
    void Start()
    {
        IconFragment = this.gameObject.transform.Find("IconFragment").gameObject;
        IconFragment.SetActive(false);

        SubjectCursol = this.gameObject.transform.Find("SubjectCursol").gameObject;
        CursolTransform = SubjectCursol.GetComponent<RectTransform>();
        SubjectNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ���͏����ړ�����
        CurcolMove();
    }

    // �J�[�\���ړ�����
    void CurcolMove()
    {
        // �㉺���͂ō��ڂ�I��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SubjectNum++;
            if (SubjectNum > 4)
            {
                SubjectNum = 4;
            }

            if (SubjectNum == 3)
            {
                IconFragment.SetActive(true);
            }
            else if (SubjectNum != 3)
            {
                IconFragment.SetActive(false);
            }

            CursolPositionCalculation();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SubjectNum--;
            if (SubjectNum < 0)
            {
                SubjectNum = 0;
            }

            if (SubjectNum == 3)
            {
                IconFragment.SetActive(true);
            }
            else if (SubjectNum != 3)
            {
                IconFragment.SetActive(false);
            }

            CursolPositionCalculation();
        }

        // Enter�L�[���͂ŋ���
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SubjectNum == 0)
            {
                craftUI.DieselEngineUpgrade();
                //Debug.Log("Upgrade");
            }
            else if (SubjectNum == 1)
            {
                craftUI.ShipBodyUpgrade();
            }
            else if (SubjectNum == 2)
            {
                craftUI.WhaleMouseUpgrade();
            }
            else if (SubjectNum == 3)
            {
                craftUI.CraneUpgrade();
            }
            else if (SubjectNum == 4)
            {
                craftUI.SonarUpgrade();
            }
        }
    }

    // �J�[�\���̃|�W�V�������v�Z���ăZ�b�g����
    void CursolPositionCalculation()
    {
        // y=-5+160*x
        CursolTransform.anchoredPosition = new Vector2(-45.0f, -5.0f + 160.0f * (2 - SubjectNum));
    }

    public int GetSubjectNum()
    {
        return SubjectNum;
    }
}
