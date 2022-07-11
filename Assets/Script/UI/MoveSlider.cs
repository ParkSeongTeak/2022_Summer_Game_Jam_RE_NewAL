using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveSlider : MonoBehaviour
{
    public static MoveSlider instance;
    public static MoveSlider Instance { get { return instance; } }



    //float speed = 10.0f;
    //float Rotatespeed = 0.2f;

    private void Awake()
    {
        instance = this;
    }

    public float GetValue()
    {
        return instance.gameObject.GetComponent<Slider>().value;

    }

    public void SliderReset()
    {
        instance.gameObject.GetComponent<Slider>().value = 0.5f;

    }
}
