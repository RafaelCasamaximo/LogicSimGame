using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de som do jogo. Deve ser atrelado ao EmpryObject respectivo. Cada uma das funções funciona de maneira independente. Os sons podem ser passados através do inspector.
/// </summary>
public class SoundManager : Singleton<SoundManager>
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
