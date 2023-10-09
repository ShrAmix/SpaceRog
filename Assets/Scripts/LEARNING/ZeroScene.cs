using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class ZeroScene : MonoBehaviour
{
    [SerializeField] private GameObject[] learnScene;
    [SerializeField] private GameObject[] nastia;
    [SerializeField] private GameObject[] otherObj;

    [SerializeField] private float timerSecond = 2.0f; // ��������� ������� � ��������
    private float chekTime = 0f;
    int sceneWhy;
    private int nexTXT=0;
    void Start()
    {
        sceneWhy = PlayerPrefs.GetInt("ZeroScene", 0);
        Debug.Log("sceneWhy-" + sceneWhy);
        PlayerPrefs.SetInt("ZeroScene",sceneWhy+1);
        ZeroS();
        SceneOne();
        SceneTwo();
    }
    private void FixedUpdate()
    {
        if (0 == sceneWhy) { ZeroS(); }
        chekTime += Time.deltaTime;
    }
    private void ZeroS()
    {
        if (0 != sceneWhy)
            learnScene[0].SetActive(false);
        else
            learnScene[0].SetActive(true);
        if (chekTime>timerSecond)
        {
            otherObj[0].SetActive(true);
            //otherObj[0].SetActive(true);
        }
    }
    private void SceneOne()
    {
        if (1 != sceneWhy)
            learnScene[1].SetActive(false);
        else
        {
            learnScene[1].SetActive(true);
            otherObj[1].SetActive(true);

        }
    }
    private void SceneTwo()
    {
        if (2 != sceneWhy)
            learnScene[2].SetActive(false);
        else
        {
            learnScene[2].SetActive(true);
            otherObj[1].SetActive(true);
            nastia[1].SetActive(true);
            string sms = "YOU KNOW THE BASIC INFORMATION. IN THE \"QUESTS\" SECTION, YOU CAN SEE THE MAIN GOALS FOR WHICH THE GOVERNMENT GIVES REWARDS. NOW, LET'S GET TO WORK";
            if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                sms = "������� �������ֲ� �� �����. � ���Ĳ˲ ������Ȕ �� ������ �������� �����Ͳ ֲ˲ �� �ʲ ����� ��� ����������. �����, �� ������";
            HintSystem.Instance.ShowHint(sms);
            
        }
    }
    public void SceneOneNo()
    {
        //PlayerPrefs.SetInt("DefaultLearning", 3);
        PlayerPrefs.SetInt("ZeroScene", 100);
        PlayerPrefs.SetInt("SetingSceneLearn", 100);
        learnScene[1].SetActive(false);
        PlayerPrefs.SetInt("ship0Access", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("HangarSceneLearn", 100);
        PlayerPrefs.SetInt("BuffScene", 100);
        PlayerPrefs.SetInt("ShipScene", 100);
        MenuScore.Instance.SetScore(0, -100);
        otherObj[1].SetActive(false);
    }
    public void SceneOneYes()
    {
        learnScene[1].SetActive(false);
        learnScene[2].SetActive(true);
        TxtLor();
    }
    public void NextTXT()
    {
        nexTXT++;
        TxtLor();
        if(2 == sceneWhy)
        {
            learnScene[2].SetActive(false);
            otherObj[1].SetActive(false);
            nastia[1].SetActive(false);
        }
    }
    private void TxtLor()
    {
        string sms;
        switch (nexTXT)
        {
            case 0:
                 sms= "HI, I'M NASTYA. I'VE BEEN ASSIGNED TO EXPLAIN THINGS TO YOU. FOR THE SAKE OF ARGUMENT, I'M GOING TO START FROM SCRATCH, SO LISTEN UP. WE, THE SPACE ROG COMPANY, WERE CREATED TO DESTROY ALIENS";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "���²�, � �����. ���� ��������� �������� ���� �� �� ����. ��� ��������� � ��� �������� ��� � ����, ���� ������. ��, �����Ͳ� SPACE ROG, ���� �������Ͳ ��� ���� ��� ������� �������ֲ�";
                HintSystem.Instance.ShowHint(sms);
                nastia[0].SetActive(true);
                break;
                case 1:
                sms = "THEY, IN TURN, APPEARED AS A RESULT OF ENDLESS ATTEMPTS OF MANKIND TO ESTABLISH COMMUNICATION WITH OTHER LIFE FORMS. UNFORTUNATELY, WE WERE NOT LUCKY AND THEY TURNED OUT TO BE INVADERS OF THE UNIVERSE";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "���� � � ���� ����� ǒ������� � ��������Ҳ ���ʲ������� ����� ������� ���������� ����� � ������ ������� �����. �� ���� ��� �� ��������� � ���� ��������� ������������ ����²��";
                HintSystem.Instance.ShowHint(sms);
                nastia[0].SetActive(false);
                nastia[1].SetActive(true);
                break;

                case 2:
                sms = "YOU, ZERO ROG, ARE THE FIRST PERSON TO BECOME COMPATIBLE WITH THE TECHNOLOGY OF CONSCIOUSNESS TRANSFER INTO A SPACECRAFT BUILT BY OUR COMPANY";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "��, ZERO ROG, ����� ������, ��� ����� ��̲��� � �������ò�� �������� �²�����Ҳ � ���̲���� ��������, ����������� ����� �����Ͳ��";
                HintSystem.Instance.ShowHint(sms);
                nastia[1].SetActive(false);
                nastia[2].SetActive(true);
                break;

                case 3:
                sms = "YOUR TASK IS TO GLORIFY OUR COMPANY, THE MORE PEOPLE KNOW US, THE MORE INVESTMENTS IN OUR PROJECT TO PROTECT HUMANITY";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "���� ������ ���������� ���� �����Ͳ�, ��� ������ ��� ������, ��� ������ �������ֲ� � ���� ��� ������ �� ������� �������";
                HintSystem.Instance.ShowHint(sms);
                nastia[2].SetActive(false);
                nastia[3].SetActive(true);
                break;

            case 4:
                sms = "THE SYSTEM IS VERY SIMPLE: THE MORE YOU DESTROY ALIEN SHIPS OR CREATURES THEY HAVE TAMED, THE MORE THEY INVEST IN US. WE, IN TURN, WILL INVEST MONEY IN IMPROVING OUR SHIPS TO INVENT THE BEST COPY TO FIGHT THESE CREATURES";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "������� ���� ������, ��� ������ �� �����Ӫ� ���������Ͳ �����˲, ��� ����в��, �ʲ ���� ���������, ��� ������ � ��� ����������. �� � � ���� ����� ���ز ������ ���������� � ���������� ����� �����˲�, ��� ������� ��������� ��������� ��� �������� � ���� ����в�����";
                HintSystem.Instance.ShowHint(sms);
                nastia[3].SetActive(false);
                nastia[0].SetActive(true);
                break;
            case 5:
                sms = "WE ARE CURRENTLY FINALIZING THE DEVELOPMENT OF THE FIRST EXEMPLAR. YOU WILL FLY FOR IT AND IF YOU DO WELL, WE WILL HAVE MONEY TO INSTALL THE \"LIFE\" SYSTEM";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "�� ������ ������ �� ��ʲ��Ӫ�� �������� ������� ����������. ��˲���� �� ����� � ���� ����� ���� ������� �� � ��� ǒ�������� ���ز ��� ������������ ������� \"�����\"";
                HintSystem.Instance.ShowHint(sms);
                nastia[0].SetActive(false);
                nastia[2].SetActive(true);
                break;

            case 6:
                sms = "LIFE IS A SYSTEM THAT ALLOWS A SHIP TO SHOOT A HAIL OF BULLETS A HAIL OF BULLETS AROUND IT IF THE ENEMY GETS TOO CLOSE. CONSIDER IT A SECOND LIFE, HENCE THE NAME";
                if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                    sms = "����� � �� ������� �� ���� �������� ����� ����в������ ������� ���� ���� ����, ���� ����� ϲ��������� ����� �������. ����� �� �� ����� �����, ²� ���� � �����";
                HintSystem.Instance.ShowHint(sms);
                nastia[2].SetActive(false);
                nastia[0].SetActive(true);
                break;
                case 7:
                learnScene[2].SetActive(false);
                otherObj[1].SetActive(false);
                nastia[0].SetActive(false);
                learnScene[3].SetActive(true);
                otherObj[2].SetActive(true);
                break;
        }
    }
}
