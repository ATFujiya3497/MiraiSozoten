using UnityEditor;
using UnityEngine;
using System.IO;

namespace djkusuha.Utility
{
    /// <summary>
    /// Unity�G�f�B�^�ォ��Game�r���[�̃X�N���[���V���b�g���B��Editor�g��
    /// </summary>
    public class CaptureScreenshotFromEditor : Editor
    {
        /// <summary>
        /// �L���v�`�����B��
        /// </summary>
        /// <remarks>
        /// Edit > CaptureScreenshot �ɒǉ��B
        /// HotKey�� Ctrl + Alt + F12�B
        /// </remarks>
        [MenuItem("Edit/CaptureScreenshot %&F12")]
        private static void CaptureScreenshot()
        {
            // ���ݎ�������t�@�C����������
            var filename = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
            //var filepath = "Assets/Capture/" + filename;
            var filepath = "Capture/" + filename;

            // Capture�t�H���_������������t�H���_���쐬
            if(!Directory.Exists("Capture"))
            {
                Directory.CreateDirectory("Capture");
                AssetDatabase.Refresh();
            }

            //if (AssetDatabase.IsValidFolder("Assets/Capture") == false)
            //{
            //    AssetDatabase.CreateFolder("Assets", "Capture");
            //}

            // �L���v�`�����B��
#if UNITY_2017_1_OR_NEWER
            ScreenCapture.CaptureScreenshot(filepath); // �� GameView�Ƀt�H�[�J�X���Ȃ��ꍇ�A���̎��_�ł͎B���Ȃ�
#else
            Application.CaptureScreenshot(filepath); // �� GameView�Ƀt�H�[�J�X���Ȃ��ꍇ�A���̎��_�ł͎B���Ȃ�
#endif
            // GameView���擾���Ă���
            var assembly = typeof(EditorWindow).Assembly;
            var type = assembly.GetType("UnityEditor.GameView");
            var gameview = EditorWindow.GetWindow(type);
            // GameView���ĕ`��
            gameview.Repaint();

            Debug.Log("ScreenShot: " + filename);
        }
    }
}