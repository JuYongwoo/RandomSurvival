using UnityEngine;

public class AudioManager
{
    private AudioSource effectsSource;

    public void onAwake()
    {
        effectsSource = ComponentUtility.AddOrGetComponent<AudioSource>(ManagerObject.instance.gameObject);
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        effectsSource.PlayOneShot(clip, volume);
    }
}
