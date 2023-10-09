using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuyLearning : MonoBehaviour
{
    [SerializeField] private GameObject[] learnScene;
    [SerializeField] private GameObject[] nastia;
    [SerializeField] private GameObject[] otherObj;
    private int nexTXT;
    int sceneWhy;
    void Start()
    {
        sceneWhy = PlayerPrefs.GetInt("ShipScene", 0);
        Debug.Log("sceneWhy-" + sceneWhy);
        PlayerPrefs.SetInt("ShipScene", sceneWhy + 1);
        if (sceneWhy == 0)
        {
            otherObj[3].SetActive(true);
            otherObj[4].SetActive(true);
        }

    }
    private void FixedUpdate()
    {
        if(sceneWhy == 0 && PlayerPrefs.GetInt("ship0Access")== 1)
        {
            otherObj[4].SetActive(false);
            ZeroScene();
            sceneWhy++;
            PlayerPrefs.SetInt("ShipScene", sceneWhy + 1);
        }
    }
    private void ZeroScene()
    {
        learnScene[0].SetActive(true);
        otherObj[0].SetActive(true);
        nastia[0].SetActive(true);
        nexTXT++;
        TxtLor();
    }
    public void NextTXT()
    {
        nexTXT++;
        TxtLor();
    }
    private void TxtLor()
    {
        string sms;
        switch (nexTXT)
        {
            
            case 1:
                sms = "YOU'VE GOT YOUR FIRST SHIP, IT'S A TEST PIECE, BUT IT'S ALREADY CAPABLE OF AMAZING THINGS";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "няэ рх ╡ нрпхлюб оепьхи йнпюаекэ, же унв ╡ реярнбхи ейгелокъп, юке б╡м бфе гдюрмхи мю дхбнбхфм╡ пев╡";
                HintSystem1.Instance.ShowHint(sms);
                nastia[0].SetActive(false);
                nastia[2].SetActive(true);
                break;

            case 2:
                sms = "SUCH AS FLYING IN ANY DIRECTION REGARDLESS OF ITS ANGLE OF ROTATION";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "рюй╡ ъй к╡рюрх б кчанлс мюопълйс ме гбюфючвх мю яб╡и йср наепрюммъ";
                HintSystem1.Instance.ShowHint(sms);
                nastia[2].SetActive(false);
                nastia[3].SetActive(true);
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    otherObj[1].SetActive(true);
                else
                    otherObj[2].SetActive(true);
                break;


            case 3:
                sms = "HELP US DEVELOP OUR SHIPS. THE BEST THING IS TO INVEST IN EACH OF THE INSTANCES TO THE MAXIMUM, THEN THERE IS A GREATER CHANCE OF CREATING THE PERFECT SHIP TO FIGHT ALIENS";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "днонлюцюи мюл пнгбхбюрх мюь╡ йнпюак╡. йпюые бяэнцн - же йнкх йнфмхи г ейгелокъп╡б ╡мбеярсбюрх он люйяхлслс, рнд╡ а╡кэьхи ьюмя мю ярбнпеммъ ╡деюкэмнцн йнпюакъ дкъ анпнрэах г опхаскэжълх";
                HintSystem1.Instance.ShowHint(sms);
                nastia[3].SetActive(false);
                nastia[0].SetActive(true);
                otherObj[1].SetActive(false);
                otherObj[2].SetActive(false);
                break;
            case 4:
                nastia[0].SetActive(false);
                otherObj[0].SetActive(false);
                learnScene[0].SetActive(false);
                otherObj[3].SetActive(false);
                break;

        }
    }
}
