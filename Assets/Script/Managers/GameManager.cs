using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{


    
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
    int _normPer = 7;            // n/10 per n0 퍼센트
    public int normPer { get { return _normPer; } set { _normPer = value; }  }

    int[] NextTetrisIDX = new int[5];
    int[] NextTetrisNeedle = new int[5];



    public int tetris_Num { 
        get { return _tetris_Num; } 
        set { 
            _tetris_Num = value; 
            if (NextTetrisIDX[0] == -1) 
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



    //tmp
    bool start = true;

    private void Awake()
    {
        for (int i = 0; i < 5; i++) { NextTetrisIDX[i] = -1; }
    }

    

    void Start()
    {

        Init();
           
        time2 = true;
        LavaStartPos = new Vector3(0.7f, -60f, 0f);
        _cameraPlayerHeight = 3f; 
        RandArrCreate();

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
        RandArrCreate();

    }


    
    public int PopTetris()         //[0]번이 출력 [4]번이 삭제 앞으로 1칸씩 Queue구조
    {
        int tmp = NextTetrisIDX[0] + NextTetrisNeedle[0]*100;

        for (int i = 0; i < 4; i++)
        {
            NextTetrisIDX[i] = NextTetrisIDX[i + 1];
            NextTetrisNeedle[i] = NextTetrisNeedle[i + 1];
        }
        NextTetrisIDX[4] = -1;
        NextTetrisNeedle[4] = -1;

        return tmp;
    }

    public int[] GetTetrisArrIDX()
    {
        return NextTetrisIDX;
    }
    public int[] GetTetrisArrNeedle()
    {
        return NextTetrisNeedle;
    }

    public void ToTime2()   //TT_Time
    {
        tetris_Num = 0;
        time2 = true;
        Lava.GetComponent<Lava>().StopLavaMove();

    }
    // Start is called before the first frame update

    public void RandArrCreate()
    {
        if (NextTetrisIDX ==null ||NextTetrisIDX[0] == -1)
        {
            if (NextTetrisIDX == null) { Debug.Log("It's NULL"); }
            else { Debug.Log("It's NextTetris[0].first == -1" + " And " + NextTetrisIDX[0] + NextTetrisIDX[1] + NextTetrisIDX[2] + NextTetrisIDX[3] + NextTetrisIDX[4]); }

            for (int i = 0; i < 5; i++)
            {
                NextTetrisIDX[i] = Random.Range(0, 10);
                NextTetrisNeedle[i] = Random.Range(0, 10);
                
            }
            Debug.Log("Now" + " And " + NextTetrisIDX[0] + NextTetrisIDX[1] + NextTetrisIDX[2] + NextTetrisIDX[3] + NextTetrisIDX[4]);

        }
    }

    public void GameOver()
    {

    }

}
