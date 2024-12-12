using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public GameObject audioTrigger;
    public GameObject[] subtitles;
    public int subtitleIndex;
    private GameObject subtitleToBeDisplayed;
    public float subtitleDuration;
    // Update is called once per frame
    void Update()
    {
        if (audioTrigger.GetComponent<AudioPlay>().showSubtitle)
        {
            subtitleIndex = audioTrigger.GetComponent<AudioPlay>().clipIndex;
            subtitleToBeDisplayed = subtitles[subtitleIndex];
            StartCoroutine("EnableDisableCoroutine");
        }
        audioTrigger.GetComponent<AudioPlay>().showSubtitle = false;
    }

    public IEnumerator EnableDisableCoroutine()
    {
        subtitleToBeDisplayed.SetActive(true);
        yield return new WaitForSeconds(subtitleDuration);
        subtitleToBeDisplayed.SetActive(false);
    }
}
