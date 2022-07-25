using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    GameManager.pair[] NextTetris;



    string Path = "Sprite/TetrisRawIMG/";
    string Norm = "block";
    string Needle = "needleblock";


    private void Start()
    {
        Stage = TetrisStage.GetComponent<Tetris>();
        instance = this;
        //NextBlockIMG = Resources.LoadAll<Sprite>(Path + Norm)
    }

    public void ToTime1()           //player
    {
        GameManager.Instance.ToTime1();
        PlayerTimeUI.SetActive(true);
        TetrisTimeUI.SetActive(false);

        NextTetris = GameManager.Instance.GetTetrisArr();
        for(int index = 0; index < 5; index++)
        {
            if(NextTetris[index].first != -1)
            {
                if (NextTetris[index].second < 5)
                {
                    if(Resources.Load<Sprite>(Path + Norm + index.ToString()) !=null )
                        NextBlock[index].GetComponent<Image>       <Image>().sprite = Resources.Load<Sprite>(Path + Norm + index.ToString());
                    else
                    {
                        Debug.Log("????");
                    }
                }
                else
                {
                    if (Resources.Load<Sprite>(Path + Needle + index.ToString()) != null)

                        NextBlock[index].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Path + Needle + index.ToString());
                    else
                    {
                        Debug.Log("!!!!!");

                    }
                }
            }
            else
            {
                NextBlock[index].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Empty");
            }
        }

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

}
