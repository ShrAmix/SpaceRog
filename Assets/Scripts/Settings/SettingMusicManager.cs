using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMusicManager : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;
    public string namePref;
    public string nameToggle;
    public TextMeshProUGUI procentText;
    public Image toggleImage;

    public Sprite enabledSprite; // Додайте спрайт для включеного стану в інспекторі Unity
    public Sprite disabledSprite; // Додайте спрайт для вимкненого стану в інспекторі Unity
    

    private void Start()
    {
        if (PlayerPrefs.GetFloat(nameToggle, 0) == 0)
            toggle.isOn = true;
        else toggle.isOn = false;
        slider.value = PlayerPrefs.GetFloat((namePref));
        int txt = ((int)(PlayerPrefs.GetFloat(namePref) * 100));
        procentText.text = txt + "%";
        UpdateToggleImage();
    }
    private void UpdateToggleImage()
    {
        if (toggle.isOn&& (slider.value > 0))
        {
            toggleImage.sprite = enabledSprite;
        }
        else 
        {
            toggleImage.sprite = disabledSprite;
        }
    }
    private void FixedUpdate()
    {
        if (PlayerPrefs.GetFloat((namePref)) != slider.value && PlayerPrefs.GetFloat(nameToggle) == 0)
        {
            PlayerPrefs.SetFloat((namePref), slider.value);
            int txt = ((int)(PlayerPrefs.GetFloat(namePref) * 100));
            procentText.text = txt + "%";
        }
        if (slider.value == 0)
        {
            toggle.isOn = false;
        }
        else 
        {
            toggle.isOn = true;
        }
    }
    public void TorgetMusic()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetFloat(nameToggle, 0);
        }
        else
        {
            PlayerPrefs.SetFloat(nameToggle, 1);
        }
        if (!toggle.isOn)
        {
            PlayerPrefs.SetFloat(namePref, 0);
            slider.value = 0;
            int txt = ((int)(PlayerPrefs.GetFloat(namePref) * 100));
            procentText.text = txt + "%";
        }
        UpdateToggleImage();
    }

}
