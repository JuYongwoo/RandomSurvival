using UnityEngine;

public class AudioManager
{
    private AudioSource effectsSource;
    private AudioSource bgmSource;


    public void onAwake()
    {
        GameObject target = ManagerObject.instance.gameObject;

        effectsSource = ComponentUtility.AddOrGetComponent<AudioSource>(target);
        bgmSource = ComponentUtility.AddOrGetComponent<AudioSource>(target); // �̸� �����ؼ� �� �� �и�
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        effectsSource.PlayOneShot(clip, volume);
    }

    public void PlayBGM(AudioClip bgmClip, float volume = 1f)
    {
        if (bgmSource.clip == bgmClip && bgmSource.isPlaying)
            return; // �̹� ��� ���̸� �ٽ� ��� �� ��

        bgmSource.clip = bgmClip;
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

}
