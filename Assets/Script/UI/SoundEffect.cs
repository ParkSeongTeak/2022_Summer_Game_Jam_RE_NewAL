using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    [SerializeField]
    string Sound;
    [SerializeField]
    Define.Sound Type;

    // Start is called before the first frame update
    public void Sound_Player()
    {
        GameManager.Instance.sound.Play(Sound, Type);
    }
}
