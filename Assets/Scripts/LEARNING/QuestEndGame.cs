using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEndGame : MonoBehaviour
{
    [SerializeField] private GameObject[] otherObj;
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("QuestAll" + $"{6}")==4)
        {
            PlayerPrefs.SetInt("QuestAll" + $"{6}", PlayerPrefs.GetInt("QuestAll" + $"{6}") + 1);

            otherObj[0].SetActive(true);
            otherObj[1].SetActive(true);
            string sms;
            otherObj[2].SetActive(true);
            sms = "WELL DONE, THANKS TO YOU, OUR COMPANY HAS BEEN RECOGNIZED AS AN INTERNATIONAL COMPANY, WHICH HAS BROUGHT US A LOT OF INVESTMENTS. WITH SUCH FUNDS, WE WILL NOT BE AFRAID OF ALIENS OR GODS. NOW IT'S UP TO YOU TO EITHER HELP US FINISH OFF THE REST OF THE ALIENS OR RETIRE. DON'T WORRY, YOUR BONUS WILL BE SO BIG THAT YOU CAN START YOUR OWN COMPANY, LIKE OUR SPACE ROG.\r\nGOOD LUCK, HERO";
            if ((PlayerPrefs.GetString("SelectedLocale") == "uk"))
                sms = "лнкндвхмю, гюбдъйх рна╡ мюьс йнлоюм╡ч опхгмюкх л╡фмюпндмнч, ын опхмеякн мюл йсос ╡мбеярхж╡и. г рюйхлх йньрюлх мюл м╡ ╡мнокюмерълх, м╡ анцх ме асдсрэ ярпюьм╡. дюк╡ бха╡п гю рнанч, юан днонлюцюрх мюл днахбюрх гюкхьйх опхаскэж╡б, юан ирх мю оемя╡ч. ме а╡ияъ, опел╡ъ б реае асде рюйю, ын рна╡ ╡ бкюямс йнлоюм╡ч, рхос мюьн╞ SPACE ROG, лнфмю асде ярбнпхрх\r\nсдюв╡, цепни";
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
