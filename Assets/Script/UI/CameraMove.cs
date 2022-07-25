using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    Vector3 CameraPos;

    // Start is called before the first frame update

    private void Awake()
    {
        CameraPos = this.gameObject.transform.position;
        Screen.SetResolution(1440  , 2560, true);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraPos.y = Player.transform.position.y + GameManager.Instance.CameraPlayerHeight;
        this.gameObject.transform.position = CameraPos;
    }
}
