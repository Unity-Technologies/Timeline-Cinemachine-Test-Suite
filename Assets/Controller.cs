using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public Text title, description;
    public PlayableDirector NoAudioSource, AudioSource, BlendClips, ManyTracks;


    public void PlayTimeline(int id)
    {
        StopTimelines();
        switch (id)
        {
            case 0:
                title.text = "Playing: No Audio Source";
                description.text = "Playing audio clip on an audio track without Audio Source attached";
                NoAudioSource.Play();
                break;
            case 1:
                title.text = "Playing: Audio Source";
                description.text = "Playing audio clip on an audio track with an Audio Source attached. Spatial Blend set to 3D";
                AudioSource.Play();
                break;
            case 2:
                title.text = "Playing: Blend Clips";
                description.text = "Clips blending over eachother";
                BlendClips.Play();
                break;
            case 3:
                title.text = "Playing: Many Tracks";
                description.text = "Clips played in sequence over many audio tracks";
                ManyTracks.Play();
                break;
        }
    }

    public void StopTimelines()
    {
        title.text = "Playing:";
        description.text = "Description";
        NoAudioSource.Stop();
        AudioSource.Stop();
        BlendClips.Stop();
        ManyTracks.Stop();
    }
}
