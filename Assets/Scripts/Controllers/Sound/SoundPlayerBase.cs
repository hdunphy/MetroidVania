using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SoundPlayerBase : MonoBehaviour
{
    [SerializeField, Tooltip("List of sounds available to play")] protected List<Sound> Sounds;

    private void Awake()
    {
        OnAwake();

        Sounds.ForEach((s) =>
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.volume = s.Volume;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.loop = s.Loop;
            s.AudioSource.clip = s.Clip;
            s.AudioSource.outputAudioMixerGroup = s.AudioMixerGroup; //Not sure if this is accurate
        });
    }

    private void Start()
    {
        OnStart();

        Sounds.ForEach((s) =>
        {
            if (s.PlayOnStart)
                s.AudioSource.Play();
        });
    }

    public void PlaySound(string name)
    {
        Sound _sound = Sounds.First(s => s.name == name);

        if (_sound != null)
        {
            _sound.AudioSource.Play();
        }
    }

    public IEnumerator PlayAndWaitForSound(string name, Action playAfterSound)
    {
        Sound _sound = Sounds.First(s => s.name == name);
        if (_sound != null)
        {
            _sound.AudioSource.Play();

            while (_sound.AudioSource.isPlaying)
            {
                yield return null;
            }
        }

        playAfterSound?.Invoke();
    }

    public void ToggleSound(string name, bool play)
    {
        Sound _sound = Sounds.First(s => s.name == name);

        if (_sound != null)
        {
            if (play)
            {
                _sound.AudioSource.Play();
            }
            else
            {
                _sound.AudioSource.Stop();
            }
        }
    }

    protected abstract void OnAwake();

    protected abstract void OnStart();
}
