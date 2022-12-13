using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDayCycle : MonoBehaviour
{
    [SerializeField]
    private float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // x�������ɂ��Ė��b2�x�A��]������Quaternion���쐬�i�ϐ���rot�Ƃ���j
        Quaternion rot = Quaternion.AngleAxis(-RotationSpeed, Vector3.up);
        // ���݂̎��M�̉�]�̏����擾����B
        Quaternion q = this.transform.rotation;
        // �������āA���g�ɐݒ�
        this.transform.rotation = q * rot;
    }
}
