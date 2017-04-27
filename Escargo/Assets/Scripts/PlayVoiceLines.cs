using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLines : MonoBehaviour {

    /* should be in this order:
     * finish
     * load
     * salt
     * selection
     * win
     */
    public AudioClip[] pierre;
    public AudioClip[] kenta;
    public AudioClip[] liljim;
    public AudioClip[] bertha;

    public void playLine(string player, int lineCase)
    {
        AudioSource audio = GetComponent<AudioSource>();
        switch (player)
        {
            case "pierre":
                audio.clip = pierre[lineCase];
                break;
            case "kenta":
                audio.clip = kenta[lineCase];
                break;
            case "liljim":
                audio.clip = liljim[lineCase];
                break;
            case "bertha":
                audio.clip = bertha[lineCase];
                break;
        }
        audio.Play();
    }

    public void playLoadLine(string player, double t)
    {
        AudioSource audio = GetComponent<AudioSource>();
        switch (player)
        {
            case "pierre":
                audio.clip = pierre[1];
                break;
            case "kenta":
                audio.clip = kenta[1];
                break;
            case "liljim":
                audio.clip = liljim[1];
                break;
            case "bertha":
                audio.clip = bertha[1];
                break;
        }
        audio.PlayScheduled(t);
    }
}
