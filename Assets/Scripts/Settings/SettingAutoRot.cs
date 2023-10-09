using UnityEngine;
using UnityEngine.UI;

public class SettingAutoRot : MonoBehaviour
{
    public Button targetButton; // ��������� �� ������, ���� ��� �� ������ ��������
    public Color defaultColor; // ��������� ���� ������
    public Color changedColor; // ����, ���� SettingAutoRotate == 0

    private void Start()
    {
        // �������� �������� SettingAutoRotate � PlayerPrefs
        int settingAutoRotate = PlayerPrefs.GetInt("SettingAutoRotate", 1);

        // ���������� ���� ������ ������� �� �������� SettingAutoRotate
        if (settingAutoRotate == 0)
        {
            targetButton.image.color = changedColor;
        }
        else
        {
            targetButton.image.color = defaultColor;
        }
    }

    public void ToggleButtonColor()
    {
        // �������� ������� �������� SettingAutoRotate � PlayerPrefs
        int settingAutoRotate = PlayerPrefs.GetInt("SettingAutoRotate", 1);

        // ����������� �������� SettingAutoRotate
        settingAutoRotate = (settingAutoRotate == 0) ? 1 : 0;

        // �������� �������� �������� SettingAutoRotate � PlayerPrefs
        PlayerPrefs.SetInt("SettingAutoRotate", settingAutoRotate);

        // ���������� ���� ������ ������� �� ������ �������� SettingAutoRotate
        if (settingAutoRotate == 0)
        {
            targetButton.image.color = changedColor;
        }
        else
        {
            targetButton.image.color = defaultColor;
        }
    }
}
