using UnityEngine;

public class TransparencyAnimation : MonoBehaviour
{
    [SerializeField] private float animationDuration = 2.0f; // Тривалість анімації в секундах

    private SpriteRenderer spriteRenderer;
    private float startTime;
    private bool fadingOut = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;
        float normalizedTime = Mathf.Clamp01(elapsedTime / animationDuration);

        if (fadingOut)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1.0f, 0.392f, normalizedTime));
        }
        else
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.392f, 1.0f, normalizedTime));
        }

        if (normalizedTime >= 1.0f)
        {
            fadingOut = !fadingOut;
            startTime = Time.time;
        }
    }
}
