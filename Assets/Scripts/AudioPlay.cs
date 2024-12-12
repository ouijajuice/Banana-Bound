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
                this.gameObject.GetComponent<AudioSource>().clip = audioClips[UnityEngine.Random.Range(0,audioClips.Length)];
                this.gameObject.GetComponent<AudioSource>().Play();
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
                this.gameObject.GetComponent<AudioSource>().clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

}
