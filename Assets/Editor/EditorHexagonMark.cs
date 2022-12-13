using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorHexagonMark : EditorWindow
{
    [MenuItem("Window/Others/Hexagon")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EditorHexagonMark));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("SpriteSet"))
        {
            // �q�G�����L�[�̑S�Ă�GameObject���擾����
            Object[] allObject = Resources.FindObjectsOfTypeAll(typeof(GameObject));


            // �擾�����I�u�W�F�N�g�̒�������ёւ���I�u�W�F�N�g�Ɠ�������S�č폜����
            foreach (GameObject obj in allObject)
            {
                // ���������ǂ����𔻒�
                if (obj.name.Contains("Hexagon"))
                {
                    //Debug.Log(obj.name);
                    Hexagon hex = obj.GetComponent<Hexagon>();
                    hex.SetSprite();
                }
            }
        }
    }
}
