using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{


    void Start()
    {
        // �������� ������� ������
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // �������� ������� ������� �������
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        float visibleHeight = Camera.main.orthographicSize * 2.0f;

        // ������� ����������
        Debug.Log("������ ������� �������: " + visibleWidth/2);
        Debug.Log("������ ������� �������: " + visibleHeight/2);

        Debug.Log("SpeedHorizontal:" + PlayerPrefs.GetFloat("SpeedHorizontal"));
        Debug.Log("SpeedVertical:" + PlayerPrefs.GetFloat("SpeedVertical"));
        Debug.Log("RotationSpeed:" + PlayerPrefs.GetFloat("rotationSpeed"));
        Debug.Log("TimeShoot:" + PlayerPrefs.GetFloat("timeShoot"));
    }

}
