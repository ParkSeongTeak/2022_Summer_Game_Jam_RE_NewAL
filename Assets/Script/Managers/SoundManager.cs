using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{

    public AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Length];

    public float[] pitch = new float[(int)Define.Sound.Length];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    public void init()
    {
        GameObject root= GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            //Object.DontDestroyOnLoad(root);

            string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundName.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundName[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Play( string path, Define.Sound type = Define.Sound.Effect)
    {
        if(path.Contains("Sound/") == false)
        {
            path = $"Sound/{path}";

        }
        AudioClip audioClip = GetOrAddAudioClip(path);
        AudioSource audioSource;
        switch (type)
        {
            case Define.Sound.Effect:
                if (audioClip == null)
                {
                    Debug.Log("No audioClip");
                    return;
                }

                audioSource = _audioSources[(int)Define.Sound.Effect];
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                audioSource.volume = pitch[(int)Define.Sound.Effect];
                audioSource.PlayOneShot(audioClip);
                break;
            
            case Define.Sound.Bgm:
                if(audioClip == null)
                {
                    Debug.Log("No audioClip");
                }

                audioSource = _audioSources[(int)Define.Sound.Bgm];
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.volume = pitch[(int)Define.Sound.Bgm];
                audioSource.clip = audioClip;
                audioSource.Play();

                break;

        }
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;

        if (_audioClips.TryGetValue(path,out audioClip) == false)
        {
            audioClip = Resources.Load<AudioClip>(path);
            _audioClips.Add(path,audioClip);
        }
        return audioClip;
    }
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();

        }

        _audioClips.Clear();
    }

}
