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

    public GameObject Pos;
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
        Debug.Log(i_width + "" + i_height);
        if (i_height <= 2560) {
            HardCodingFormat.transform.localScale = new Vector3(i_width / 1440f, i_height / 2560f);
            yUp = (yUp * 2560f / i_height);
        }
        if(i_height == 3040)
        {
            yUp = 8f;
        }
        if (i_height == 2560)
        {
            yUp = 9.35f;
        }



    }

    void Start()
    {
        //Screen.SetResolution(1440, 2560, true);

        Debug.Log(MyCamera.orthographicSize);

        Transform BG = UiManager.instance.TetrisStage.transform.GetChild(1);
        BottomBGPosition = BG.GetChild(BG.childCount - 1).transform.position;

        Debug.Log("BottomBGPosition.y" + BottomBGPosition.y);

        
        RaycastHit hit;
        Ray ray = MyCamera.ScreenPointToRay(Pos.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            IMGIMG.transform.position = objectHit.position;

            // Do something with the object that was hit by the raycast.
        }


    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        */
        
    }
}
