using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration) {
        if (audioSource == null)
            yield break;
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration) {
            if (audioSource == null)
                yield break;
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
