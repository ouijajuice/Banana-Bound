using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip running;
    public AudioClip jump;
    public AudioClip grab;
    private Movement movement;
    private bool runningPlayed = false;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        if (this.gameObject.GetComponent<Movement>().running)
        {
            source.clip = running;
            
            source.loop = true;
            if (runningPlayed == false )
            {
                source.Play();
                runningPlayed = true;
            }
        }
        
        else
        {
            runningPlayed = false;
            source.loop = false;
        }

        if (this.gameObject.GetComponent<Movement>().jumping)
        {
            source.clip = jump;
            source.Play();
        }
        if (this.gameObject.GetComponent<Movement>().grab)
        {
            source.clip = grab;
            source.Play();
        }
        //Debug.Log($"Running: {this.gameObject.GetComponent<Movement>().running}, Jumping: {this.gameObject.GetComponent<Movement>().jumping}, Grab: {this.gameObject.GetComponent<Movement>().grab}");
    }
}
