using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AudioManager
{
    public enum Sounds
    {
        BGM,
        hitsound,
        coinsound,
        sprintsound

    }

    private AudioSource effectsSource;
    private AudioSource bgmSource;
    private Dictionary<Sounds, AudioClip> soundsMap;

    public void onAwake()
    {
        GameObject target = ManagerObject.instance.gameObject;

        soundsMap = new Dictionary<Sounds, AudioClip>();
        soundsMap[Sounds.BGM] = Addressables.LoadAssetAsync<AudioClip>("BGM").WaitForCompletion();
        soundsMap[Sounds.hitsound] = Addressables.LoadAssetAsync<AudioClip>("hitsound").WaitForCompletion();

        effectsSource = Util.AddOrGetComponent<AudioSource>(target);
        bgmSource = Util.AddOrGetComponent<AudioSource>(target); // 이름 지정해서 두 개 분리
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
    }

    public void onStart()
    {
        PlayBGM(AudioManager.Sounds.BGM);
    }

    public void PlaySound(Sounds sound, float volume = 1f)
    {
        effectsSource.PlayOneShot(soundsMap[sound], volume);
    }

    public void PlayAudioClip(AudioClip sound, float volume = 1f)
    {
        effectsSource.PlayOneShot(sound, volume);
    }

    public void PlayBGM(Sounds sound, float volume = 1f)
    {
        if (bgmSource.clip == soundsMap[sound] && bgmSource.isPlaying)
            return; // 이미 재생 중이면 다시 재생 안 함

        bgmSource.clip = soundsMap[sound];
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

}
