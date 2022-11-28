using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FileNumDisplay : EditorWindow
{
    private const string REMOVE_STR = "Assets";

    private static readonly int mRemoveCount = REMOVE_STR.Length;
    private static Color mColor = new Color(0.0f, 0.635f, 0.635f, 1); // �����w�i�F
    private static Color mTextColor = new Color(1.0f, 1.0f, 1.0f, 1); // �����w�i�F

    private static bool isDisp = true;  // �\��/��\���̃t���O

    // ���j���[�ɍ��ڂ�ǉ�
    [MenuItem("Window/Others/FileNumDisplay")]
    static void Init() // �E�B���h�E���쐬
    {
        GetWindow<FileNumDisplay>(true, "FileNumDisplay");
    }

    // �\������UI
    void OnGUI()
    {
        try
        {
            // ON/OFF�`�F�b�N�{�b�N�X�\��
            isDisp = EditorGUILayout.Toggle("ON/OFF", isDisp);

            // �����w�i�F�ݒ荀�ڂ�ǉ�
            mColor = EditorGUILayout.ColorField("BackColor", mColor);

            // �����w�i�F�ݒ荀�ڂ�ǉ�
            mTextColor = EditorGUILayout.ColorField("TextColor", mTextColor);
        }
        catch (System.FormatException) { }
    }

    // �G�f�B�^�N�����E�R���p�C�����ɌĂяo��
    [InitializeOnLoadMethod]
    private static void Example()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    // �t�@�C�����\��
    private static void OnGUI(string guid, Rect selectionRect)
    {
        if (isDisp) // �؂�ւ������ (����������)
        {
            var dataPath = Application.dataPath;
            var startIndex = dataPath.LastIndexOf(REMOVE_STR);
            var dir = dataPath.Remove(startIndex, mRemoveCount);
            var path = dir + AssetDatabase.GUIDToAssetPath(guid);

            if (!Directory.Exists(path))
            {
                return;
            }

            var fileCount = Directory
                .GetFiles(path)
                .Where(c => !c.EndsWith(".meta"))
                .Count();

            var dirCount = Directory
                .GetDirectories(path)
                .Length;

            var count = fileCount + dirCount;

            if (count == 0)
            {
                return;
            }

            var text = count.ToString();
            var label = EditorStyles.label;
            var content = new GUIContent(text);
            var width = label.CalcSize(content).x + 1.0f;

            var pos = selectionRect;
            pos.x = pos.xMax - width;
            pos.width = width;
            pos.yMin++;

            // GUIStyle�𕡐� �i�������Ȃ��ƑS�̂̐ݒ肪�����ς��j
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = mTextColor;

            var color = GUI.color;
            GUI.color = mColor;
            GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
            GUI.color = color;
            GUI.Label(pos, text, style);
        }
    }
}