using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public PenguinScript penguin;

    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip background;
    public AudioClip wallTouch;
    public AudioClip die;
    public AudioClip jetpack;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        // if (penguin == null)
        // {
        //     penguin = GameObject.FindObjectOfType<PenguinScript>();
        // }
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void Stop()
    {
        musicSource.clip = background;
        musicSource.Stop();
    }
}
