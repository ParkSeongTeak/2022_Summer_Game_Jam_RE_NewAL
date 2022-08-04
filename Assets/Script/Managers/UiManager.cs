using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverScene;

    public static UiManager instance;
    [SerializeField]
    GameObject TetrisStage;
    GameObject tilePrefab;
    Tetris Stage;

    [SerializeField]
    GameObject[] NextBlock_Pnt = new GameObject[5];
    GameObject[] NextBlock_Dummy;

    //Sprite[] NextBlockIMG;


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


    private void Awake()
    {
        instance = this;
        tilePrefab = Resources.Load<GameObject>("Prefab/UItile");

    }
    private void Start()
    {
        //NextBlock_Dummy = new GameObject[5];
        Stage = TetrisStage.GetComponent<Tetris>();
        tilePrefab = Resources.Load<GameObject>("Prefab/UItile");

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

    public void TetrisRotate()
    {
        Stage.TetrisRotate();
    }

    public void TetrisDown()
    {
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
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1.0f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 1.0f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0.0f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0.0f), Sprites, 4, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(2f, 0.0f), Sprites, 5, isNeedle, isladder);
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
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 2, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, -1f), Sprites, 3, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, -1f), Sprites, 4, isNeedle, isladder);
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
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 0, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 1, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(1f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(2f, 0f), Sprites, 3, isNeedle, isladder);
                     break;

                 //ㅁ : 
                 case 7:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 1f), Sprites, 0, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 3, isNeedle, isladder);
                     break;

                 case 8:
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 1f), Sprites, 1, isNeedle);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(-1f, 0f), Sprites, 2, isNeedle, isladder);
                     CreateUiTile(NextBlock_Dummy[index], new Vector2(0f, 0f), Sprites, 3, isNeedle, isladder);
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
    }
    public void Best_Score_Ui_Update()
    {
        Best_Score.text = GameManager.Instance.Best_Score.ToString();
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
        Time.timeScale = 1.0f;
        GameManager.Instance.Lava.GetComponent<Lava>().StartLavaMove();

    }

}



