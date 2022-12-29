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
            Hexagon[] allObject = Resources.FindObjectsOfTypeAll<Hexagon>();


            // �擾�����I�u�W�F�N�g�̒�������ёւ���I�u�W�F�N�g�Ɠ�������S�č폜����
            foreach (Hexagon obj in allObject)
            {
                Debug.Log(obj.name);
                obj.SetSprite();
                
            }
        }
    }
}
