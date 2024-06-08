using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public float timetofade = 2f; // Total time to fade in and out
    private CanvasGroup canvasGroup;
    private bool isFading = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing on this GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !isFading)
        {
            StartCoroutine(FadeInOutRoutine());
        }
    }

    private IEnumerator FadeInOutRoutine()
    {
        isFading = true;
        float halfTime = timetofade / 2f;

        // Fade In
        for (float t = 0; t < halfTime; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / halfTime);
            yield return null;
        }
        canvasGroup.alpha = 1;

        // Fade Out
        for (float t = 0; t < halfTime; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / halfTime);
            yield return null;
        }
        canvasGroup.alpha = 0;

        isFading = false;
    }
}
