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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (singleAudio)
        {
            if (collision.gameObject.tag == tagToPlayFor)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (multipleAudio)
        {
            if (collision.gameObject.tag == tagToPlayFor)
            {
                clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
                this.gameObject.GetComponent<AudioSource>().clip = audioClips[clipIndex];
                this.gameObject.GetComponent<AudioSource>().Play();
                showSubtitle = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (singleAudio)
        {
            if (collision.gameObject.tag == tagToPlayFor)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (multipleAudio)
        {
            if (collision.gameObject.tag == tagToPlayFor)
            {
                clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
                this.gameObject.GetComponent<AudioSource>().clip = audioClips[clipIndex];
                this.gameObject.GetComponent<AudioSource>().Play();
                showSubtitle = true;
            }
        }
    }

}
