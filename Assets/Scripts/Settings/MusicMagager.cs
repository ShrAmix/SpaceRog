using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMagager : MonoBehaviour
{
    public static MusicMagager Instance;
    public AudioSource audioSource;
    public AudioClip[] firstSceneTracks;
    public AudioClip[] secondSceneTracks;

    private AudioClip currentTrack;
    private AudioClip[] currentSceneTracks;
    private int currentTrackIndex;
    public int currentSceneIndex;

    public bool TrackPlay;

    private void Start()
    {
        TrackPlay = true;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        LoadSceneMusic(currentSceneIndex);
        PlayNextTrack();
    }

    private void LoadSceneMusic(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            currentSceneTracks = firstSceneTracks;
        }
        else if (sceneIndex == 1)
        {
            currentSceneTracks = secondSceneTracks;
        }
    }

    private void FixedUpdate()
    {
        if (currentSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 0)
            {
                currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                TrackPlay = false;
                if (currentSceneIndex == 1 && !TrackPlay)
                    StartCoroutine(NewPresetMusic());
                else if (currentSceneIndex == 0 && !TrackPlay)
                    StartCoroutine(NewPresetMusic());
            }
        }
    }

    private System.Collections.IEnumerator NewPresetMusic()
    {
        float save = PlayerPrefs.GetFloat("volume");

        for (float i = save; i > 0; i -= 0.01f)
        {
            PlayerPrefs.SetFloat("volume", i);
            yield return new WaitForSeconds(0.01f);
        }

        TrackPlay = true;
        LoadSceneMusic(currentSceneIndex);
        PlayNextTrack();

        for (float i = 0; i < save; i += 0.01f)
        {
            PlayerPrefs.SetFloat("volume", i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void PlayNextTrack()
    {
        if (currentSceneTracks.Length == 0)
        {
            return;
        }

        int newTrackIndex;
        do
        {
            newTrackIndex = Random.Range(0, currentSceneTracks.Length);
        } while (newTrackIndex == currentTrackIndex);

        currentTrackIndex = newTrackIndex;
        currentTrack = currentSceneTracks[currentTrackIndex];
        audioSource.clip = currentTrack;
        audioSource.Play();
    }
}
