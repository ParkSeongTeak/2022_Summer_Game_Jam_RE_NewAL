using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{



    static GameManager instance;
    public static GameManager Instance { get { Init(); return instance; } }

    //ResourceManager _resource = new ResourceManager();
    InputManager _input = new InputManager();
    public static InputManager InputSys { get { return Instance._input; } }
    
    public float MoveSliderValue()
    {
        return MoveSlider.Instance.GetValue();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdata();

        if (Input.GetMouseButtonUp(0))
        {
            MoveSlider.Instance.SliderReset();
           
        }

    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject GM = GameObject.Find("@GameManager");
            if (GM == null)
            {
                GM = new GameObject { name = "@GameManager" };
                GM.AddComponent<GameManager>();

            }

            DontDestroyOnLoad(GM);
            instance = GM.GetComponent<GameManager>();


        }


    }

}
