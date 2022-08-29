using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    [SerializeField]
    string Sound;
    // Start is called before the first frame update
    public void Sound_One_Shot()
    {
        GameManager.Instance.sound.Play(Sound);
    }
}
