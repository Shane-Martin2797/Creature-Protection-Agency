using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicController : SingletonBehaviour<BackgroundMusicController> 
{
    protected BackgroundMusicController() {}

    public bool isRandom;
    public AudioSource music;
    public AudioClip loopClip;
    public AudioClip[] musicArray;

    //void Start ()
    //{
    //    music.PlayOneShot(initialClip);
    //}

	void Awake ()
	{
		music = GetComponent<AudioSource> ();
	}

    void Update ()
    {
        if (!music.isPlaying)
        {
            PlayMusic ();
        }
		Debug.Log (music.isPlaying);
    }

    private void PlayMusic ()
    {
        if (isRandom)
        {
            music.loop = false;
            music.PlayOneShot(musicArray[Random.Range(0, musicArray.Length)]);
        }
        else
        {
            music.loop = true;
            music.PlayOneShot(loopClip);
        }
    }
}
