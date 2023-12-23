using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuffBuyLearn : MonoBehaviour
{
    [SerializeField] private GameObject[] otherObj;
    private int nexTXT;
    int sceneWhy;
    void Start()
    {
        sceneWhy = PlayerPrefs.GetInt("BuffScene", 0);
        Debug.Log("sceneWhy-" + sceneWhy);
        PlayerPrefs.SetInt("BuffScene", sceneWhy + 1);
        if (sceneWhy == 0)
        {
            otherObj[0].SetActive(true);
            otherObj[1].SetActive(true);
            string sms;
            otherObj[2].SetActive(true);
            sms = "ALSO, DURING THE OUTING, YOU MAY BE PROVIDED WITH BUFFS BY OUR COLLEAGUES FROM ANOTHER DEPARTMENT. YOU ALSO NEED TO INVEST IN THEM TO INCREASE THE DURATION OF THE BUFF";
            if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                sms = "рюйнф о╡д вюя бхкюгйх рна╡ лнфсрэ мюдюбюрх ⌠аюттх■ мюь╡ йнкецх г ╡мьнцн б╡дд╡кс. б мху рюйнф онрп╡амн ╡мбеярсбюрх дкъ га╡кэьеммъ рпхбюкняр╡ ⌠аюттс■";
            HintSystem1.Instance.ShowHint(sms);
        }
    }
    public void NextTXT()
    {
        otherObj[0].SetActive(false);
        otherObj[1].SetActive(false);
        otherObj[2].SetActive(false);
    }

}
