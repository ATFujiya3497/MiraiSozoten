using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeatherChangeKey : MonoBehaviour
{

    public enum Weather
    {
        Clear,
        Rainy,
        Snowy
    }
    [Header("����p�̉��X�N���v�g")]

    [Header("�L�[���� 1:���� 2:�J 3:��")]
    public Weather _weathertype;

    [SerializeField]
    [Header("�V�C��GameObject������")]
    private GameObject _Snowy;
    [SerializeField]
    private GameObject _Rainy;

    Keyboard keyboard;


    // Start is called before the first frame update
    void Start()
    {
        // ���݂̃L�[�{�[�h���
        keyboard = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (keyboard == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        //�V�C�̏�����
        _weathertype = Weather.Clear;

    }

    // Update is called once per frame
    void Update()
    {
        // 1�L�[�̓��͏�Ԏ擾
        var Key_ten1 = keyboard.digit1Key;
        // 1�L�[�̓��͏�Ԏ擾
        var Key_ten2 = keyboard.digit2Key;
        // 1�L�[�̓��͏�Ԏ擾
        var Key_ten3 = keyboard.digit3Key;

        if (Key_ten1.wasPressedThisFrame)
        {
            _weathertype = Weather.Clear;
        }
        if (Key_ten2.wasPressedThisFrame)
        {
            _weathertype = Weather.Rainy;
        }
        if (Key_ten3.wasPressedThisFrame)
        {
            _weathertype = Weather.Snowy;
        }

        switch (_weathertype)
        {
            case Weather.Clear:
                _Snowy.SetActive(false);
                _Rainy.SetActive(false);
                break;
            case Weather.Rainy:
                _Snowy.SetActive(false);
                _Rainy.SetActive(true);
                break;
            case Weather.Snowy:
                _Snowy.SetActive(true);
                _Rainy.SetActive(false);
                break;

        }
    }
}
