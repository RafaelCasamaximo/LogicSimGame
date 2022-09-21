using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{

    [SerializeField] private AudioSource musicSource, effectsSource;
    
    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    
    public void ChangeEffectsVolume(float value)
    {
        effectsSource.volume = value;
    }

    public void ToggleEffects()
    {
        effectsSource.mute = !effectsSource.mute;
    }
    
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    
}
