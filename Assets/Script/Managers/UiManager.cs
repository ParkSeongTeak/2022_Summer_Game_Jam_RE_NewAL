using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    [SerializeField]
    GameObject GameOverScene;
    [SerializeField]
    GameObject TetrisStage;
    GameObject tilePrefab;
    [SerializeField]
    GameObject[] NextBlock_Pnt = new GameObject[5];
    GameObject[] NextBlock_Dummy;
    [SerializeField]
    GameObject Option;
    
    Tetris Stage;



    //Sound관련 슬라이더
    [SerializeField]
    Slider SFXSlider;
    [SerializeField]
    Slider BGMSlider;
    string SFXSliderstr = "askldnfaekdsfafasdfadwsf";
    string BGMSliderstr = "aklf;njenf;kjblaef";
    float SFXSliderbefore;
    float BGMSliderbefore;


    [SerializeField]
    GameObject StartLogo;


    [SerializeField]
    GameObject PlayerTimeUI;

    [SerializeField]
    GameObject TetrisTimeUI;
    int[] NextTetrisIDX = new int[5];
    int[] NextTetrisNeedle = new int[5];


    string Path = "Sprite/TetrisBlock/";
    string Norm = "block";
    string Needle = "needleblock";


    float ALine = 60f;  //Ui 한변의 길이 

    //포인트
    [SerializeField]
    TextMeshProUGUI now_Score;
    [SerializeField]
    TextMeshProUGUI Best_Score;
    [SerializeField]
    TextMeshProUGUI End_Now_Score;
    [SerializeField]
    TextMeshProUGUI End_Best_Score;
    [SerializeField]
    GameObject Best_Score_IMG;




    //프롤로그
    [SerializeField]
    GameObject Prologue;
    [SerializeField]
    GameObject ProlChar;
    bool Wait = false;
    int PrologueIdx =1;
    Vector3 First = new Vector3();
    float time1 = 1f;
    Vector3 Second = new Vector3();
    float time2 = 1f;
    Vector3 Third = new Vector3();
    float time3 = 1f;
    Vector3 Fourth = new Vector3();
    float AFrame = 0.02f;
    [SerializeField]
    GameObject _tutorial;


    private void Awake()
    {
        instance = this;
        tilePrefab = Resources.Load<GameObject>("Prefab/UItile");
        SFXSlider.value = PlayerPrefs.GetFloat(SFXSliderstr, 1f);
        SFXSliderbefore = SFXSlider.value;
        BGMSlider.value = PlayerPrefs.GetFloat(BGMSliderstr, 1f);
        BGMSliderbefore = BGMSlider.value;
        First = ProlChar.transform.position;
        Second = ProlChar.transform.position + new Vector3(516, 86, 0);
        Third = ProlChar.transform.position + new Vector3(359, 229, 0);
        Fourth = ProlChar.transform.position + new Vector3(645, 315, 0);


    }
    private void Start()
    {
        First = ProlChar.transform.position;
        Best_Score_Ui_Update();
        Stage = TetrisStage.GetComponent<Tetris>();
        tilePrefab = Resources.Load<GameObject>("Prefab/UItile");
        if(GameManager.Instance.prolInt == 0)
        {
            Prologue_Show();
        }
    }

    public void ToTime1()           //player
    {
        GameManager.Instance.ToTime1();
        PlayerTimeUI.SetActive(true);
        TetrisTimeUI.SetActive(false);

        NextTTShow();
    }
    public void ToTime2()
    {
        GameManager.Instance.ToTime2();
        PlayerTimeUI.SetActive(false);
        TetrisTimeUI.SetActive(true);

    }
    public void ButtonSound()
    {
        GameManager.Instance.sound.Play("BlockDown");
    }
    public void TetrisRotate()
    {
        
        Stage.TetrisRotate();
    }

    public void TetrisDown()
    {
        GameManager.Instance.sound.Play("BlockDown");
        Stage.TetrisDown();
        
    }


    public void NextTTShow()
    {
        bool isNeedle;
        bool isladder = false;

        NextTetrisIDX = GameManager.Instance.GetTetrisArrIDX();
        NextTetrisNeedle = GameManager.Instance.GetTetrisArrNeedle();

        
        if (NextBlock_Dummy != null)
        {
            for (int i=4; i>=0; i--)
            {
                Destroy(NextBlock_Dummy[i]);
            }
        }

        NextBlock_Dummy = new GameObject[5];
        
        for (int i = 0; i < 5; i++)
        {
           
            NextBlock_Dummy[i] = new GameObject("Dummy");
            NextBlock_Dummy[i].transform.SetParent(NextBlock_Pnt[i].transform,true);
            NextBlock_Dummy[i].transform.position = NextBlock_Pnt[i].transform.position;
        }

        isNeedle = false;

       
        for (int index = 0; index < 5; index++)   {

            if (NextTetrisIDX[index] == -1) { break; }
            Sprite[] Sprites;

            if (NextTetrisNeedle[index] < GameManager.Instance.normPer)
            {
                isNeedle = false;
                isladder = false;

                Sprites = Resources.LoadAll<Sprite>(Path + Norm + NextTetrisIDX[index].ToString());
                //Debug.Log(Path + Norm + NextTetrisIDX[index].ToString());
            }
            else if (NextTetrisNeedle[index] < GameManager.Instance.ladderPer)
            {
                isNeedle = false;
                isladder = true;
                Sprites = Resources.LoadAll<Sprite>(Path + Norm + NextTetrisIDX[index].ToString());
                //Debug.Log(Path + Norm + NextTetrisIDX[index].ToString());

            }
            else
            {
                isNeedle = true;
                isladder = false;

                Sprites = Resources.LoadAll<Sprite>(Path + Needle + NextTetrisIDX[index].ToString());
                //Debug.Log(Path + Needle + NextTetrisIDX[index].ToString());
            }
            

            
            switch (NextTetrisIDX[index])
             {
                 // ㅁ. 
                 case 0:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f - 1f , 1.0f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f - 1f, 1.0f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f - 1f, 0.0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f - 1f, 0.0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(2f - 1f, 0.0f), Sprites, 5, isNeedle, isladder);
                     break;

                 // Z 
                 case 1:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 1f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                     break;

                 // ㄴ.
                 case 2:

                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 1.0f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0.0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0.0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0.0f), Sprites, 5, isNeedle, isladder);
                     break;

                 // O : 노란색
                 case 3:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                     break;

                 // S : 녹색
                 case 4:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 1f), Sprites, 2, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                     break;

                 // L : 자주색
                 case 5:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 1f), Sprites, 2, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                     break;

                 // ㅡ : 빨간색
                 case 6:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f - 0.5f, 0f), Sprites, 0, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f - 0.5f, 0f), Sprites, 1, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f - 0.5f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(2f - 0.5f, 0f), Sprites, 3, isNeedle, isladder);
                     break;

                 //ㅁ : 
                 case 7:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f + 0.5f, 1f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f + 0.5f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f + 0.5f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f + 0.5f, 0f), Sprites, 3, isNeedle, isladder);
                     break;

                 case 8:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f + 0.5f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f + 0.5f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f + 0.5f, 0f), Sprites, 3, isNeedle, isladder);
                     break;
                 case 9:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 1f), Sprites, 2, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                     break;

                default:
                    Debug.Log("SwitchError " + NextTetrisIDX[index]);
                    break;
            }

            
        }
        
    }
    void CreateUiTile(GameObject dummy,  Vector2 pos, Sprite[] sprites,int index, bool needlessd = false, bool ladder = false)
    {

        Vector2 _pos = new Vector2(pos.x * ALine, pos.y * ALine);
 
        var go = Instantiate(tilePrefab);
        //var go = tilePrefab;
        
        go.transform.SetParent(dummy.transform, true);

        go.GetComponent<RectTransform>().localPosition = _pos;

        go.transform.localPosition = _pos;
        if (index < sprites.Length && sprites[index] != null) {
            go.GetComponent<Image>().sprite = sprites[index];
        }
        else
        {
            Debug.Log("Error  " + index);
        }
        
        if (ladder)
        {
            go.transform.GetChild(0).gameObject.SetActive(true); 
        } 
       
        
    }

    public void Now_Score_Ui_Update()
    {
        now_Score.text = GameManager.Instance.now_Score.ToString();
        End_Now_Score.text = GameManager.Instance.now_Score.ToString();

    }
    public void Best_Score_Ui_Update()
    {
        
        Best_Score.text = GameManager.Instance.Best_Score.ToString();
        End_Best_Score.text = GameManager.Instance.Best_Score.ToString();

        //Debug.Log(GameManager.Instance.Best_Score);
    }


    public void GameOverIMG()
    {
        GameOverScene.SetActive(true);
    }

    public void Load_Game_Scene()
    {
        
        SceneManager.LoadScene("GameScene");


    }

    public void Pause_Start()
    {
        Time.timeScale = 0;
        GameManager.Instance.Lava.GetComponent<Lava>().StopLavaMove();
    }

    public void Pause_End()
    {
        if (StartLogo.activeSelf != true)
        {
            Time.timeScale = 1.0f;
            GameManager.Instance.Lava.GetComponent<Lava>().StartLavaMove();
        }
    }
    
    void Prologue_Show()
    {
        Prologue.SetActive(true);
        GameManager.Instance.SetPrologue(1);
    }
    public void PrologueReset()
    {
        PlayerPrefs.SetInt("BBEESSTT__SSCCRREE", 0);
        GameManager.Instance.SetPrologue(0);

    }

    void Prologue_Time1()
    {
        if (!Wait)
        {
            StartCoroutine("ProlMove1");
            PrologueIdx++;
        }
        // Pause_Start();
    }
    void Prologue_Time2()
    {
        if (!Wait)
        {
            StartCoroutine("ProlMove2");
            PrologueIdx++;
        }
        // Pause_Start();
    }
    void Prologue_Time3()
    {
        if (!Wait)
        {
            StartCoroutine("ProlMove3");        // 재생 후 접고 튜토 킴;
            PrologueIdx = 1;
            
        }
        // Pause_Start();
    }

    public void Touch_Prologue()
    {
        switch (PrologueIdx)
        {
            case 1:
                Prologue_Time1();
                break;
            case 2:
                Prologue_Time2();
                break;
            case 3:
                Prologue_Time3();

                break;
        }


    }


    public void Prologue_Button()
    {
        Prologue.SetActive(false);
        GameManager.Instance.prolRead();
        //Pause_End();
    }

    public float SFXSliderValue()
    {
        return SFXSlider.value;
    }
    public float BGMSliderValue()
    {
        return BGMSlider.value;

    }
    private void Update()
    {
        if(Option.activeSelf == true)
        {
            //Debug.Log("Option");
            if(SFXSliderbefore != SFXSlider.value)
            {
                //Debug.Log("SFX.volume");

                GameManager.Instance.sound.volume[(int)Define.Sound.Effect] = SFXSlider.value;
                SFXSliderbefore = SFXSlider.value;
                GameManager.Instance.sound._audioSources[(int)Define.Sound.Effect].volume = SFXSlider.value;
                PlayerPrefs.SetFloat(SFXSliderstr, SFXSlider.value);

            }
            if (BGMSliderbefore != BGMSlider.value)
            {
                GameManager.Instance.sound.volume[(int)Define.Sound.Bgm1] = BGMSlider.value;
                GameManager.Instance.sound.volume[(int)Define.Sound.Bgm2] = BGMSlider.value;

                BGMSliderbefore = BGMSlider.value;
                GameManager.Instance.sound._audioSources[(int)Define.Sound.Bgm1].volume = BGMSlider.value;
                GameManager.Instance.sound._audioSources[(int)Define.Sound.Bgm2].volume = BGMSlider.value;

                PlayerPrefs.SetFloat(BGMSliderstr, BGMSlider.value);

            }
        }

    }
    bool SameVec(Vector3 One, Vector3 Another)
    {
        float Bound = 0.0001f;
        if((One.x < Another.x + Bound && One.x > Another.x - Bound) && (One.y < Another.y + Bound && One.y > Another.y - Bound))
        {
            return true;
        }
        return false;
    }
    
    IEnumerator ProlMove1()
    {
        Wait = true;
        Vector3 AMove;
        AMove = Second - ProlChar.transform.position;
        AMove = AMove / (time1 / AFrame);
        for (int i = 0; i < (time1 / AFrame); i++)
        {
            yield return new WaitForSecondsRealtime(AFrame);
            ProlChar.transform.position += AMove;
        }
        
        Wait = false;


    }
    IEnumerator ProlMove2()
    {
        Wait = true;
        Vector3 AMove;
        ProlChar.transform.localEulerAngles= new Vector3(0, 180, 0); 
        AMove = Third - ProlChar.transform.position;
        AMove = AMove / (time2 / AFrame);

        for (int i = 0; i < (time2 / AFrame); i++)
        {
            yield return new WaitForSecondsRealtime(AFrame);
            ProlChar.transform.position += AMove;
        }
       
        Wait = false;


    }

    public void QuitGame() {
        GameManager.Instance.GameOver();
        Application.Quit();
    }



    IEnumerator ProlMove3()
    {
        Wait = true;
        Vector3 AMove;
        ProlChar.transform.localEulerAngles = new Vector3(0, 0, 0);

        AMove = Fourth - ProlChar.transform.position;
        AMove = AMove / (time3 / AFrame);

        for (int i = 0; i < (time3 / AFrame); i++)
        {
            yield return new WaitForSecondsRealtime(AFrame);
            ProlChar.transform.position += AMove;
        }

        Wait = false;
        _tutorial.SetActive(true);
        Prologue.SetActive(false);


    }

    public void Best_Score_Img()
    {
        Best_Score_IMG.SetActive(true);
    }
}



