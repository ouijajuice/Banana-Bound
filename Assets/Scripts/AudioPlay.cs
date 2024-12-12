using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public bool singleAudio;
    public bool multipleAudio;
    public string tagToPlayFor;
    public AudioClip[] audioClips;
    public int clipIndex;
    public bool showSubtitle = false;

    // Static flag to manage audio playing state
    private static bool isAudioPlaying = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleAudioTrigger(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleAudioTrigger(collision.gameObject);
    }

    private void HandleAudioTrigger(GameObject triggeringObject)
    {
        if (triggeringObject.tag != tagToPlayFor)
        {
            return;
        }

        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        if (singleAudio)
        {
            if (!isAudioPlaying)
            {
                isAudioPlaying = true;
                audioSource.Play();
                StartCoroutine(ResetAudioState(audioSource.clip.length));
            }
        }

        if (multipleAudio)
        {
            if (!isAudioPlaying)
            {
                isAudioPlaying = true;
                clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
                audioSource.clip = audioClips[clipIndex];
                audioSource.Play();
                showSubtitle = true;
                StartCoroutine(ResetAudioState(audioSource.clip.length));
            }
        }
    }

    private IEnumerator ResetAudioState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAudioPlaying = false;
    }

}
