using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade effect in seconds
    public float delayBeforeFadeIn = 1f; // Delay before fading in after enabling the new scene
    public GameObject fadeOutPlane; // Reference to the FadeOutPlane
    public GameObject oldScene; // The GameObject to be disabled
    public GameObject newScene; // The GameObject to be enabled
    private Material fadeOutMat; // Reference to the FadeOutMat material

    private void Start()
    {
        // Get the FadeOutMat material from the FadeOutPlane
        fadeOutMat = fadeOutPlane.GetComponent<Renderer>().material;
    }

    public void TransitionToNextPhase()
    {
        StartCoroutine(FadeOutAndTransition());
    }

    private IEnumerator FadeOutAndTransition()
    {
        // Fade out visuals and audio sources in the old scene
        yield return FadeOutCoroutine();

        // Disable the old scene
        DisableOldScene();

        // Enable the new scene
        EnableNewScene();

        // Delay before fading in
        yield return new WaitForSeconds(delayBeforeFadeIn);

        // Fade in visuals and audio sources in the new scene
        yield return FadeInCoroutine();
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Fade out visuals
        yield return FadeCoroutine(0, 1, fadeDuration);

        // Fade out audio sources in the old scene
        FadeOutAudioSources(oldScene, fadeDuration);
    }

    private IEnumerator FadeInCoroutine()
    {
        // Fade in audio sources in the new scene
        FadeInAudioSources(newScene, fadeDuration);

        // Fade in visuals
        yield return FadeCoroutine(1, 0, fadeDuration);
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        fadeOutMat.color = new Color(0, 0, 0, startAlpha);

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeOutMat.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeOutMat.color = new Color(0, 0, 0, endAlpha);
    }

    private void DisableOldScene()
    {
        // Disable the oldScene GameObject and all its child GameObjects
        oldScene.SetActive(false);
    }

    private void EnableNewScene()
    {
        // Enable the newScene GameObject and all its child GameObjects
        newScene.SetActive(true);
    }

    private void FadeOutAudioSources(GameObject rootObject, float duration)
    {
        // Find all audio sources under the root object
        AudioSource[] audioSources = rootObject.GetComponentsInChildren<AudioSource>();

        // Start fading out audio sources
        foreach (AudioSource audioSource in audioSources)
        {
            StartCoroutine(FadeAudioSource(audioSource, 1, 0, duration));
        }
    }

    private void FadeInAudioSources(GameObject rootObject, float duration)
    {
        // Find all audio sources under the root object
        AudioSource[] audioSources = rootObject.GetComponentsInChildren<AudioSource>();

        // Start fading in audio sources
        foreach (AudioSource audioSource in audioSources)
        {
            StartCoroutine(FadeAudioSource(audioSource, 0, 1, duration));
        }
    }

    private IEnumerator FadeAudioSource(AudioSource audioSource, float startVolume, float endVolume, float duration)
    {
        float elapsedTime = 0f;
        audioSource.volume = startVolume;

        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = endVolume;
    }
}