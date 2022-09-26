using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
 * 가로 1440 
 * 
 * 세로 
 * 2560
 * 
 * 사이사이 휴리스틱
 * 
 * 2880
 * 2960
 * 3040
 * 3200
 * 3120
 * 
 * 가로 1080
 * 
 * 세로 
 * 1920
 * 2100
 * 2280
 * 2400
 */

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject HardCodingFormat;

    public RectTransform Pos;
    public GameObject IMGIMG;

    Camera MyCamera = new Camera();

    Vector3 CameraPos;

    Vector3 BottomBGPosition;
    float yUp = 8.15f;
    int i_width;
    int i_height;

    // Start is called before the first frame update

    private void Awake()
    {
        CameraPos = this.gameObject.transform.position;
        MyCamera = this.transform.GetComponent<Camera>();


        i_width = Screen.width;
        i_height = Screen.height;


        /*사이사이 휴리스틱
        *
        * 2880
        * 2960
        * 3040
        * 3200
        * 3120
        *
        *가로 1080
        *
        *세로
        * 1920
        * 2100
        * 2280
        * 2400
        */

        Debug.Log(i_width + "" + i_height);
        if (i_height <= 2560)
        {
            HardCodingFormat.transform.localScale = new Vector3(i_width / 1440f, i_height / 2560f);
            yUp = (yUp * 2560f / i_height);
        }
        if (i_width >= 1400)    //1440기준
        {
            if (i_height <= 2560) { MyCamera.orthographicSize = 13.86f;  }
            if (i_height <= 2880) { }
            else if (i_height <= 2960) { }
            else if (i_height <= 3040) { MyCamera.orthographicSize = 15.86f; }//        //size 15.86
            else if (i_height <= 3120) { MyCamera.orthographicSize = 16.3f; }//-741.9  //16.3
            else if (i_height <= 3200) { MyCamera.orthographicSize = 16.61f; }          //16.61


        }
        else//  1080기준
        {
            if (i_height <= 1920) { yUp = 8f; MyCamera.orthographicSize = 13.79f; }
            else if (i_height <= 2100) { yUp = 9f; MyCamera.orthographicSize = 15.1f; }
            else if (i_height <= 2280) { yUp = 9.5f;  MyCamera.orthographicSize = 15.86f; }//        //size 15.86
            else if (i_height <= 2400) { yUp = 10f; MyCamera.orthographicSize = 16.57f; }//-741.9  //16.3
            
        }

     


    }

    void Start()
    {
        //Screen.SetResolution(1440, 2560, true);

        Debug.Log(MyCamera.orthographicSize);

        Transform BG = UiManager.instance.TetrisStage.transform.GetChild(1);
        BottomBGPosition = BG.GetChild(BG.childCount - 1).transform.position;

        Debug.Log("BottomBGPosition.y" + BottomBGPosition.y);


       

    }

    // Update is called once per frame
    void Update()
    {
        
        CameraPos.y = Player.transform.position.y + GameManager.Instance.CameraPlayerHeight;
        if (CameraPos.y < BottomBGPosition.y  + yUp)
        {
            CameraPos.y = BottomBGPosition.y + yUp;
            this.gameObject.transform.position = CameraPos;
        }
        else
        {
            this.gameObject.transform.position = CameraPos;
        }
        
        
    }
}
