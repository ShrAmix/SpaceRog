using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSceneLeran : MonoBehaviour
{
    [SerializeField] bool boolZero;
    [SerializeField] private GameObject[] learnObj;

    [SerializeField] private float timerSecond = 2.0f; // “Ë‚‡Î≥ÒÚ¸ ‡Ì≥Ï‡ˆ≥ø ‚ ÒÂÍÛÌ‰‡ı
    private float chekTime = 0f;
    int sceneWhy;
    void Start()
    {
        if(boolZero)
        PlayerPrefs.SetInt("SetingSceneLearn", 0);

        sceneWhy = PlayerPrefs.GetInt("SetingSceneLearn", 0);
        PlayerPrefs.SetInt("SetingSceneLearn", sceneWhy + 1);
        if (0 != sceneWhy)
            learnObj[0].SetActive(false);
        else
        {
           // Debug.Log("sms");
            string sms = "ADJUST THE VOLUME OF MUSIC AND SOUNDS, AND THEN SELECT A LANGUAGE\nÕ¿À¿ÿ“”…“≈ √”◊Õ≤—“‹ Ã”«» » ≤ «¬” ≤¬, œ≤—Àﬂ ◊Œ√Œ Œ¡≈–≤“‹ ÃŒ¬”";


            HintSystem.Instance.ShowHint(sms);
            learnObj[0].SetActive(true);
        }
        if(1!=sceneWhy)
            learnObj[1].SetActive(false);
        else learnObj[1].SetActive(true);
    }
    private void FixedUpdate()
    {
        switch (sceneWhy)
        {
            case 0:
                ZeroS();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        chekTime += Time.deltaTime;
    }
    private void ZeroS()
    {
        
    }
}
