using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioSource audio_;
    public string nameS;
    private void Start()
    {
        if (!PlayerPrefs.HasKey(nameS)) audio_.volume = 0.5f;
        else audio_.volume = PlayerPrefs.GetFloat(nameS);
    }
    private void FixedUpdate()
    {
        if (audio_.volume != PlayerPrefs.GetFloat(nameS)) audio_.volume = PlayerPrefs.GetFloat(nameS);

    }
}
