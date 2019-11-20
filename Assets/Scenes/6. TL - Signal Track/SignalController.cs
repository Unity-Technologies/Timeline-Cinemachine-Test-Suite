using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SignalController : MonoBehaviour
{

    public GameObject Disable, RetroactiveDisable, ToggleOnce, ToggleRepeat;
    public PlayableDirector timeline;

    public Slider timelineSlider;

    void Start()
    {
        timeline.initialTime = 0.7f;
        timeline.Play();
    }

    public void RestartTimeline()
    {
        timeline.Stop();
        Disable.SetActive(true);
        RetroactiveDisable.SetActive(true);
        ToggleOnce.SetActive(true);
        ToggleRepeat.SetActive(true);
        timeline.Play();
    }

    void Update()
    {
        timelineSlider.value = (float)timeline.time;
    }

    public void DisableObject()
    {
        Disable.SetActive(false);
    }

    public void RetroactiveDisableObject()
    {
        RetroactiveDisable.SetActive(false);
    }

    public void ToggleOnceObject()
    {
        ToggleOnce.SetActive(!ToggleOnce.activeSelf);
    }

    public void ToggleRepeatObject()
    {
        ToggleRepeat.SetActive(!ToggleRepeat.activeSelf);
    }
}
