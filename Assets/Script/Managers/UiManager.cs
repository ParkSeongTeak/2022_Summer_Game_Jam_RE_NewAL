using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField]
    GameObject TetrisStage;
    Tetris Stage;

    [SerializeField]
    GameObject[] NextBlock = new GameObject[5];
    //Sprite[] NextBlockIMG;


    [SerializeField]
    GameObject PlayerTimeUI;

    [SerializeField]
    GameObject TetrisTimeUI;
    int[] NextTetrisIDX = new int [5];
    int[] NextTetrisNeedle = new int[5];



    string Path = "Sprite/TetrisRawIMG/";
    string Norm = "block";
    string Needle = "needleblock";

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        
        Stage = TetrisStage.GetComponent<Tetris>();


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
        
        NextTetrisIDX = GameManager.Instance.GetTetrisArrIDX();
        NextTetrisNeedle = GameManager.Instance.GetTetrisArrNeedle();

        Debug.Log("INUI Index"  +NextTetrisIDX[0] + NextTetrisIDX[1] + NextTetrisIDX[2] + NextTetrisIDX[3] + NextTetrisIDX[4]);
    

        for (int index = 0; index < 5; index++)
        {
            if (NextTetrisIDX[index] != -1)
            {
                if (NextTetrisNeedle[index]< GameManager.Instance.normPer)
                {
                    if (Resources.Load<Sprite>(Path + Norm + NextTetrisIDX[index].ToString()) != null)
                    {
                        NextBlock[index].GetComponent<Image>().sprite = Resources.Load<Sprite>(Path + Norm + NextTetrisIDX[index].ToString());
                        
                    }
                    else
                    {
                        Debug.Log("????");
                    }
                }
                else
                {
                    if (Resources.Load<Sprite>(Path + Needle + NextTetrisIDX[index].ToString()) != null)
                    {

                        NextBlock[index].GetComponent<Image>().sprite = Resources.Load<Sprite>(Path + Needle + NextTetrisIDX[index].ToString());
                        
                    }
                    else
                    {
                        Debug.Log("!!!!!");

                    }
                }
            }
            else
            {
                NextBlock[index].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Empty");
            }


        }
    }
}
