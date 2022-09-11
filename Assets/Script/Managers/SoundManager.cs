using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{

    public AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Length];
    AudioClip audioClip_BGM1; ////BlockBgm
    AudioClip audioClip_BGM2; ////JumpBgm

    float FadeInTime = 0.05f;
    float FadeOutTime = 0.05f;

    public float[] volume = new float[(int)Define.Sound.Length];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    public void init()
    {
        GameObject root= GameObject.Find("@Sound");
        Debug.Log("@Sound");
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
            _audioSources[(int)Define.Sound.Bgm1].loop = true;
            _audioSources[(int)Define.Sound.Bgm2].loop = true;
            audioClip_BGM1 = GetOrAddAudioClip("Sound/JumpBgm");
            audioClip_BGM2 = GetOrAddAudioClip("Sound/BlockBgm");

            _audioSources[(int)Define.Sound.Bgm1].volume = 0;
            _audioSources[(int)Define.Sound.Bgm2].volume = 0;

            _audioSources[(int)Define.Sound.Bgm1].clip = audioClip_BGM1;
            _audioSources[(int)Define.Sound.Bgm2].clip = audioClip_BGM2;

            _audioSources[(int)Define.Sound.Bgm1].Play();
            _audioSources[(int)Define.Sound.Bgm2].Play();




        }
    }

    public void Play( string path = "", Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sound/") == false)
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
                audioSource.volume = volume[(int)Define.Sound.Effect];
                audioSource.PlayOneShot(audioClip);
                break;
            


            case Define.Sound.Bgm1:

                GameManager.Instance.FadeOut(Define.Sound.Bgm2);
                GameManager.Instance.FadeIn(Define.Sound.Bgm1);

                //_audioSources[(int)Define.Sound.Bgm1].volume = volume[(int)Define.Sound.Bgm1];
                //_audioSources[(int)Define.Sound.Bgm2].volume = 0;

                break;
            case Define.Sound.Bgm2:

                GameManager.Instance.FadeOut(Define.Sound.Bgm1);
                GameManager.Instance.FadeIn(Define.Sound.Bgm2);

                //_audioSources[(int)Define.Sound.Bgm1].volume = 0;
                //_audioSources[(int)Define.Sound.Bgm2].volume = volume[(int)Define.Sound.Bgm2];

                break;
            default:
                Debug.Log("Sound__Sound");

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

    public IEnumerator FadeIn(Define.Sound sound)
    {
        yield return new WaitForSeconds(1.0f);
        float tmp = Mathf.Max(volume[(int)Define.Sound.Bgm1], volume[(int)Define.Sound.Bgm2]);
        for (int i = 0; i < (1.0f / FadeInTime) && _audioSources[(int)sound].volume <= volume[(int)sound]; i++)
        {

            yield return new WaitForSeconds(FadeInTime);
            _audioSources[(int)sound].volume += tmp / (1.0f/ FadeInTime);
            Debug.Log("InVolume: " + _audioSources[(int)sound].volume+ " Point " + tmp / (1.0f / FadeInTime));
            
        }
        _audioSources[(int)sound].volume = volume[(int)sound];
        Debug.Log("InVolume: " + _audioSources[(int)sound].volume + " Point " + tmp / (1.0f / FadeInTime));

    }

    public IEnumerator FadeOut(Define.Sound sound)
    {
        float tmp = Mathf.Max(volume[(int)Define.Sound.Bgm1], volume[(int)Define.Sound.Bgm2]);
        for (int i = 0; i < (1.0 / FadeOutTime) && _audioSources[(int)sound].volume >= 0; i++)
        {

            yield return new WaitForSeconds(FadeInTime);
            _audioSources[(int)sound].volume -= tmp / (1.0f / FadeOutTime);
            Debug.Log("OutVolume: " + _audioSources[(int)sound].volume + " Point " + tmp / (1.0f / FadeInTime));


        }
        _audioSources[(int)sound].volume = 0;
        Debug.Log("OutVolume: " + _audioSources[(int)sound].volume + " Point " + tmp / (1.0f / FadeInTime));

    }

}
