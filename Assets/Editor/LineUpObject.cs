using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LineUpObject : EditorWindow
{
    private GameObject prefab;
    private int numX = 1;
    private int numZ = 1;
    private float intervalX = 1;
    private float intervalZ = 1;

    private float positionOffsetX = 0.0f;
    private float positionOffsetY = 0.0f;
    private float positionOffsetZ = 0.0f;

    // ���j���[�ɍ��ڂ�ǉ�
    [MenuItem("Window/Others/LineUpObject")]
    static void Init() // �E�B���h�E���쐬
    {
        GetWindow<LineUpObject>(true, "LineUpObject");
    }

    // �\������UI
    void OnGUI()
    {
        try
        {
            prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true) as GameObject;


            GUILayout.Label("X : ", EditorStyles.boldLabel);
            numX = int.Parse(EditorGUILayout.TextField("num", numX.ToString()));
            intervalX = float.Parse(EditorGUILayout.TextField("interval", intervalX.ToString()));

            GUILayout.Label("Z : ", EditorStyles.boldLabel);
            numZ = int.Parse(EditorGUILayout.TextField("num", numZ.ToString()));
            intervalZ = float.Parse(EditorGUILayout.TextField("interval", intervalZ.ToString()));

            GUILayout.Label("PositionOffset : ", EditorStyles.boldLabel);
            positionOffsetX = float.Parse(EditorGUILayout.TextField("X", positionOffsetX.ToString()));
            positionOffsetY = float.Parse(EditorGUILayout.TextField("Y", positionOffsetX.ToString()));
            positionOffsetZ = float.Parse(EditorGUILayout.TextField("Z", positionOffsetZ.ToString()));


            GUILayout.Label("", EditorStyles.boldLabel);
            if (GUILayout.Button("Create")) Create();
        }
        catch (System.FormatException) { }
    }

    // �N���������̏���
    void OnEnable()
    {
        if (Selection.gameObjects.Length > 0) prefab = Selection.gameObjects[0];
    }

    // prefab�̃I�u�W�F�N�g����ꂩ������
    void OnSelectionChange()
    {
        if (Selection.gameObjects.Length > 0) prefab = Selection.gameObjects[0];
        Repaint();
    }

    // Create�{�^�������������ɕ��בւ���
    private void Create()
    {
        if (prefab == null) return;   // prefab�ɉ��������Ă��Ȃ��ꍇ��reutrn
        DeleteAllSerectPrefab();      // prefab�Ɠ����I�u�W�F�N�g���q�G�����L�[����T���o���đS�č폜����

        int count = 0;
        Vector3 pos;
        pos.y = positionOffsetY;

        pos.x = (-(numX - 1) * intervalX / 2) + positionOffsetX;
        for (int X = 0; X < numX; X++)
        {
            pos.z = (-(numZ - 1) * intervalZ / 2) + positionOffsetZ;
            for (int Z = 0; Z < numZ; Z++)
            {
                GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                /*�㉺�����炵�ĕ��ׂ�*/
                if (X % 2 == 0)
                {
                    obj.transform.position = new Vector3(pos.x, pos.y, pos.z);
                }
                else if (X % 2 == 1)
                {
                    obj.transform.position = new Vector3(pos.x, pos.y, pos.z - (intervalZ * 0.5f));
                }
                obj.name = prefab.name + count++;
                Undo.RegisterCreatedObjectUndo(obj, "LineUpObject");

                // obj�̃C���f�b�N�X�R���|�[�l���g�Ƀ|�W�V���������
                Hexagon hex = obj.GetComponent<Hexagon>();                
                var hsxSerialFeald = new SerializedObject(hex);
                hex.SetMapindex(X, Z);                

                hsxSerialFeald.ApplyModifiedProperties();
                pos.z -= intervalZ;
            }
            pos.x += intervalX;
        }
    }

    // ���ёւ���I�u�W�F�N�g�Ɠ�����ނ̃q�G�����L�[�̃I�u�W�F�N�g��S�č폜����
    private void DeleteAllSerectPrefab()
    {
        // �q�G�����L�[�̑S�Ă�GameObject���擾����
        Object[] allObject = Resources.FindObjectsOfTypeAll(typeof(GameObject));


        // �擾�����I�u�W�F�N�g�̒�������ёւ���I�u�W�F�N�g�Ɠ�������S�č폜����
        foreach (GameObject obj in allObject)
        {
            // ���������ǂ����𔻒�
            if (obj.name.Contains(prefab.name))
            {
                Debug.Log(obj.name);
                DestroyImmediate(obj);
            }
        }
    }
}
