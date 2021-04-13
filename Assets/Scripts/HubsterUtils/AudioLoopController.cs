using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLoopController : MonoBehaviour
{
    AudioSource audio;

    [SerializeField]AudioClip startClip;
    [SerializeField] float fadeTime; 
    [Range(0f, 1f)] public float volume = 1f;

    float actionTimeStamp;
    bool end = false;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    public void BeginAudioLoop()
    {
        audio.Play();
        actionTimeStamp = Time.time;
        end = false;
        start = true;
    }

    public void EndAudioLoop()
    {
        actionTimeStamp = Time.time;
        end = true;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            BeginAudioLoop();
        if (Input.GetMouseButtonUp(0))
            EndAudioLoop();

        if (start)
        {
            audio.volume = Mathf.Lerp(0f, volume, Time.time - actionTimeStamp / fadeTime);
            if (Time.time - actionTimeStamp > fadeTime)
            {
                audio.volume = volume;
                start = false;
            }

        }

        if (end)
        {
            audio.volume = Mathf.Lerp(volume, 0f, Time.time - actionTimeStamp / fadeTime);
            if(Time.time - actionTimeStamp > fadeTime)
            {
                audio.volume = 0f;
                audio.Stop();
                end = false;
            }
                
        }
    }
}
