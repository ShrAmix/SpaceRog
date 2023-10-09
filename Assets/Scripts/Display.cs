using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{


    void Start()
    {
        // Получить размеры экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Получить размеры видимой области
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        float visibleHeight = Camera.main.orthographicSize * 2.0f;

        // Вывести результаты
        Debug.Log("Ширина видимой области: " + visibleWidth/2);
        Debug.Log("Высота видимой области: " + visibleHeight/2);

        Debug.Log("SpeedHorizontal:" + PlayerPrefs.GetFloat("SpeedHorizontal"));
        Debug.Log("SpeedVertical:" + PlayerPrefs.GetFloat("SpeedVertical"));
        Debug.Log("RotationSpeed:" + PlayerPrefs.GetFloat("rotationSpeed"));
        Debug.Log("TimeShoot:" + PlayerPrefs.GetFloat("timeShoot"));
    }

}
