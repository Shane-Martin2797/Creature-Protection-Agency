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
    }

    private void PlayMusic ()
    {
        if (isRandom)
        {
            music.loop = false;
			music.clip = musicArray[Random.Range(0, musicArray.Length)];

			music.Play();
		}
        else
        {
            music.loop = true;
			music.clip = musicArray[Random.Range(0, musicArray.Length)];

            music.Play();
        }
    }
}
