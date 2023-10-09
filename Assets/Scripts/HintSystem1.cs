using System.Collections;
using TMPro;
using UnityEngine;

public class HintSystem1 : MonoBehaviour
{
    public static HintSystem1 Instance; // Singleton instance

    public TextMeshProUGUI hintText; // Reference to the TextMeshProUGUI element
    public float typingSpeed = 0.1f; // Speed at which the hint text is typed
    public float displayDuration = 2f; // Duration for which the hint is displayed after typing completion
    public int TxetGo = 3;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one instance of the HintSystem
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowHint(string hint)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            hintText.text = hint; // Show the full hint immediately if another hint was in progress
        }

        typingCoroutine = StartCoroutine(TypeHintText(hint));
    }

    private IEnumerator TypeHintText(string hint)
    {
        hintText.text = string.Empty;

        for (int i = 0; i < hint.Length; i += TxetGo)
        {
            int charsToShow = Mathf.Min(TxetGo, hint.Length - i);
            hintText.text += hint.Substring(i, charsToShow);
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayDuration);

        hintText.text = string.Empty;
        typingCoroutine = null;
    }
}
