using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{


    public class pair
    {
        int _first;
        int _second;

        public int first { get { return _first; } set { _first = value; } }
        public int second { get { return _second; } set { _second = value; } }
        public pair(int first,int second)
        {
            _first = first;
            _second = second;

        }

    }

    // GameManager 기본 설정값
    static GameManager instance;
    public static GameManager Instance { get { Init(); return instance; } }
    InputManager _input = new InputManager();
    public static InputManager InputSys { get { return Instance._input; } }

    //Time관련 값들 Time1 -> Player     Time2 -> Tetris
    bool _time1;     //PlayerTime
    bool _time2;     //TetrisTime
    public bool time1
    {
        get { return _time1; }
        set { _time1 = value; _time2 = !(value); }
    }
    public bool time2
    {
        get { return _time2; }
        set { _time2 = value; _time1 = !(value); }
    }

    // Tetris 
    int _tetris_Num;
    int tetrisMaxnum = 5;
    pair[] NextTetris = new pair[5];


    public int tetris_Num { 
        get { return _tetris_Num; } 
        set { 
            _tetris_Num = value; 
            if (_tetris_Num >= tetrisMaxnum) 
            { 
                _tetris_Num = 0;
                time1 = true;
                UiManager.instance.ToTime1();
            } 
        } 
    }


    GameObject Lava;
    Vector3 LavaStartPos;


    //Camera 변수
    float _cameraPlayerHeight;
    public float CameraPlayerHeight { get { return _cameraPlayerHeight; } }
    




    void Start()
    {
        Init();
        
        time2 = true;
        LavaStartPos = new Vector3(0.7f, -60f, 0f);
        _cameraPlayerHeight = 3f;

        if (Lava==null)
        {
            Lava = Resources.Load<GameObject>("Prefab/Lava");
            Lava = Instantiate(Lava);
            Lava.transform.position = LavaStartPos;
            Lava.GetComponent<Lava>().StartLavaMove();

        }

        UiManager.instance.ToTime1();
        UiManager.instance.ToTime2();




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


    public float MoveSliderValue()
    {
        return MoveSlider.Instance.GetValue();
    }

    public void ToTime1()   //PlayerTime
    {

        time1 = true;
        Lava.GetComponent<Lava>().StartLavaMove();

        int index;
        int needleORNorm;

    
        for (int i = 0; i < 5; i++)
        {
            needleORNorm = Random.Range(0, 10);
            index = Random.Range(0, 10);
            NextTetris[i] = new pair(index,needleORNorm);
        }

    }



    public pair PopTetris()
    {
        pair tmp = NextTetris[0];

        for (int i = 0; i < 4; i++)
        {
            NextTetris[i] = NextTetris[i + 1];
        }
        NextTetris[4] = new pair(-1, -1); 
        return tmp;



    }

    public pair[] GetTetrisArr()
    {
        return NextTetris;
    }

    public void ToTime2()   //TT_Time
    {
        tetris_Num = 0;
        time2 = true;
        Lava.GetComponent<Lava>().StopLavaMove();

    }
    // Start is called before the first frame update



    public void GameOver()
    {

    }

}
