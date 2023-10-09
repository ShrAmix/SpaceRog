using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarSceneLearning : MonoBehaviour
{
    int sceneWhy;
    [SerializeField] private GameObject[] objScene;
    void Start()
    {
        sceneWhy = PlayerPrefs.GetInt("HangarSceneLearn", 0);
        PlayerPrefs.SetInt("HangarSceneLearn", sceneWhy + 1);
        if(sceneWhy == 0)
        {
            objScene[0].SetActive(false);
            objScene[2].SetActive(false);
            objScene[3].SetActive(false);
        }
        else if(sceneWhy == 1)
        {
            objScene[1].SetActive(false);
            objScene[2].SetActive(false);
            objScene[3].SetActive(false);
        }

    }
}
