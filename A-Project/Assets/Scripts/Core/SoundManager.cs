using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null;

    public void Init()
    {
        if(_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if(_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                UnityEngine.Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = Enum.GetNames(typeof(Define.Sound));
                for(int count = 0; count < soundTypeNames.Length; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSource[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSource[(int)Define.Sound.Bgm].loop = true;
                _audioSource[(int)Define.Sound.SubBgm].loop = true;
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSource)
            audioSource.Stop();

        _audioClips.Clear();
    }

    public void Play(Define.Sound type)
    {
        AudioSource audioSource = _audioSource[(int)type];
        audioSource.Play();
    }

    public void Play(Define.Sound type, string Key, float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSource[(int)type];

        if(type == Define.Sound.Bgm)
        {
            LoadAudioClip(Key, (audioClip) =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = audioClip;

                if (Managers.Game.BGMOn)
                    audioSource.Play();
            });
        }
        else if(type == Define.Sound.SubBgm)
        {
            LoadAudioClip(Key, (audioClip) =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = audioClip;

                if (Managers.Game.EffectSoundOn)
                    audioSource.Play();
            });
        }
        else
        {
            LoadAudioClip(Key, (audioClip) =>
            {
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            });
        }
    }

    public void Stop(Define.Sound type)
    {
        AudioSource audioSource = _audioSource[(int)type];
        audioSource.Stop();
    }

    public void LoadAudioClip(string key, Action<AudioClip> callback)
    {
        AudioClip audioClip = null;

        if(_audioClips.TryGetValue(key, out audioClip))
        {
            callback?.Invoke(audioClip);
            return;
        }

        audioClip = Managers.Resource.Load<AudioClip>(key);

        if (!_audioClips.ContainsKey(key))
            _audioClips.Add(key, audioClip);

        callback?.Invoke(audioClip);
    }
}
