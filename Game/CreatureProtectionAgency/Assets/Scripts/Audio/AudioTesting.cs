using UnityEngine;
using System.Collections;

public class AudioTesting : MonoBehaviour 
{
    public AudioClip soundClip;
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundEffectController.Instance.PlaySoundEffect(soundClip);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundEffectController.Instance.PlaySoundEffect(soundClip, this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundEffectController.Instance.PlaySoundEffect(soundClip, new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f)));
        }
	}
}
