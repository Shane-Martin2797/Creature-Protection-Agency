using UnityEngine;
using System.Collections;

public class SoundEffectController : SingletonBehaviour<SoundEffectController> 
{
    protected SoundEffectController () {}
    
    /// <summary>
    /// Plays the specified sound effect (2D sound)
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlaySoundEffect (AudioClip audioClip)
    {
        //Creates an AudioSource on this GameObject as 2D sounds don't need position.
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        //Plays a single shot of the audio clip
        audioSource.PlayOneShot(audioClip/*, sfxVolume*/);

        //Destroys the AudioSource after playing the sound effect.
        Destroy(audioSource, audioClip.length);
    }

    /// <summary>
    /// Plays the specified sound effect (3D sound)
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="audioGameObject"></param>
    public void PlaySoundEffect(AudioClip audioClip, GameObject audioGameObject)
    {
        //Creates an AudioSource on the object that needs to produce the sound.
        AudioSource audioSource = audioGameObject.AddComponent<AudioSource>();

        //Plays a single shot of the audio clip
        audioSource.PlayOneShot(audioClip/*, sfxVolume*/);

        //Destroys the AudioSource after playing the sound effect.
        Destroy(audioSource, audioClip.length);
    }

    /// <summary>
    /// Plays the specified sound effect (3D sound)
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="locationOfSound"></param>
    public void PlaySoundEffect(AudioClip audioClip, Vector3 locationOfSound)
    {
        //Creates a gameObject for the audioSource to be added to.
        GameObject audioSource = GameObject.CreatePrimitive(PrimitiveType.Plane);

        //Sets the position of the GameObject
        audioSource.transform.position = locationOfSound;
        
        //Plays a single shot of the audio clip
        audioSource.gameObject.GetComponent<Renderer>().enabled = false;

        //Adds an AudioSource and plays one shot of the audio clip.
        audioSource.AddComponent<AudioSource>();
        audioSource.GetComponent<AudioSource>().PlayOneShot(audioClip/*, sfxVolume*/);

        //Destroys the AudioSource GameObject after playing the sound effect.
        Destroy(audioSource, audioClip.length);
    }
}
