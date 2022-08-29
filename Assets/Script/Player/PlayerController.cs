using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject IMG;
    [SerializeField]
    GameObject Foot;
    [SerializeField]
    GameObject Head;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightHand;

    public string state;
    int ExitUp = 0;
    int ExitHor = 0;

    public enum State
    {
        Horizon,
        Leftvertical,
        Rightvertical,
        LeftExitLadder,
        RightExitLadder,
    }

    //Anim
    public Animator anim;
    
    public bool stay { get { return anim.GetBool("IsStay"); } 
        set { 
            if (value) {
                anim.SetBool("IsStay",true);
                anim.SetBool("IsJumpUp", false);
                anim.SetBool("IsJumpDown", false);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsDie", false);

            }

        } 
    }
    public bool jumpUp
    {
        get { return anim.GetBool("IsJumpUp"); }
        set
        {
            if (value)
            {
                
                anim.SetBool("IsStay", false);
                anim.SetBool("IsJumpUp", true);
                anim.SetBool("IsJumpDown", false);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsDie", false);
            }
        }
    }
    public bool jumpDown
    {
        get { return anim.GetBool("IsJumpjumpDown"); }
        set
        {
            if (value)
            {
                anim.SetBool("IsStay", false);
                anim.SetBool("IsJumpUp", false);
                anim.SetBool("IsJumpDown", true);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsDie", false);
            }
        }
    }
    public bool walk { get { return anim.GetBool("IsWalk"); } set {
            if (value)
            {
                anim.SetBool("IsStay", false);
                anim.SetBool("IsJumpUp", false);
                anim.SetBool("IsJumpDown", false);
                anim.SetBool("IsWalk",true);
                anim.SetBool("IsDie", false);
            }
        }
    }
    
    public bool die { get { return anim.GetBool("IsDie"); } set {
           if (value)
           {
                anim.SetBool("IsStay", false);
                anim.SetBool("IsJumpUp", false);
                anim.SetBool("IsJumpDown", false);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsDie", true);
           }
            Debug.Log(gameObject.name + die);

        }
    }




    public State nowState;
    public State beforeState;

    
    CharacterController Player; // 제어할 캐릭터 컨트롤러
    public float Speed;  // 이동속도
    const float JumpPow = 8.0f;


    private float Gravity; // 중력   
    private Vector3 HorizonDir; // 캐릭터의 가로방향 움직이는 방향.
    private Vector3 VerticalDir; // 캐릭터의 가로방향 움직이는 방향.
    [SerializeField]
    bool _isPlayerGrounded = true;
    private bool isPlayerGrounded { get { return _isPlayerGrounded; } set { _isPlayerGrounded = value; } }  //  최종 점프 버튼 눌림 상태
    private bool isJumpButtonPressing;

    //Sound관련 
    float walkSoundTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        nowState = State.Horizon;
        beforeState = State.Horizon;
        anim =  transform.GetChild(0).GetComponent<Animator>();

        Player = this.gameObject.GetComponent<CharacterController>();

        // 기본값
        Speed = 5.0f;
        Gravity = 10.0f;
        HorizonDir = Vector3.zero;
        VerticalDir = Vector3.zero;
        //JumpPow = 8.0f;
        isJumpButtonPressing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        HorizonDir.x = 0;
        VerticalDir = Vector3.zero;
        
        if (Player == null) return;
        
        switch (nowState)
        {
            case State.Horizon:
                state = "Horizon";
               
                beforeState = State.Horizon;

                break;
            case State.Leftvertical:
                
                state = "Leftvertical";
               
                beforeState = State.Leftvertical;

                break;
            case State.Rightvertical:
                state = "Rightvertical";
               
                beforeState = State.Rightvertical;

                break;

            case State.LeftExitLadder:
                if(ExitUp > 0)
                {
                    VerticalDir.y = 1;
                    VerticalDir *= Speed;
                    Player.Move(VerticalDir * Time.deltaTime);
                    ExitUp -= 1;
                    return;
                }
                else if(ExitHor > 0)
                {
                    VerticalDir.x = -1;
                    VerticalDir *= Speed;
                    Player.Move(VerticalDir * Time.deltaTime);
                    ExitHor -= 1;
                    return;
                }
                beforeState = State.Horizon;
                nowState = State.Horizon;
                break;
            case State.RightExitLadder:
                if (ExitUp > 0)
                {
                    VerticalDir.y = 1;
                    VerticalDir *= Speed;
                    Player.Move(VerticalDir * Time.deltaTime);
                    ExitUp -= 1;
                    return;
                }
                else if (ExitHor > 0)
                {
                    VerticalDir.x = 1;
                    VerticalDir *= Speed;
                    Player.Move(VerticalDir * Time.deltaTime);
                    ExitHor -= 1;
                    return;
                }
                beforeState = State.Horizon;
                nowState = State.Horizon;
                break;
        }
        if(GameManager.Instance.time2)
        {
            stay = true;
        }

        if (GameManager.Instance.time1)
        {
            if (GameManager.Instance.MoveSliderValue() >= 0.7f)
            {
                walk = true;

                IMG.transform.localEulerAngles = new Vector3(0, 0, 0);

                switch (nowState)
                {
                    case State.Horizon:
                        HorizonDir.x = 1;
                        break;
                    case State.Leftvertical:
                        if (OnGround())
                        {
                            HorizonDir.x = 1;
                        }
                        else
                        {
                            HorizonDir.y = -1;
                        }
                        //VerticalDir.y = -1;
                        break;
                    case State.Rightvertical:
                        HorizonDir.y = 1;
                        //VerticalDir.y = 1;
                        break;
                }
                
            }
            else if (GameManager.Instance.MoveSliderValue() <= 0.3f)
            {
                walk = true;
                IMG.transform.localEulerAngles = new Vector3(0, 180, 0);
                switch (nowState)
                {
                    case State.Horizon:
                        HorizonDir.x = -1;
                        break;
                    case State.Leftvertical:
                        HorizonDir.y = 1;
                        break;
                    case State.Rightvertical:
                        if (OnGround())
                        {
                            HorizonDir.x = -1;
                        }
                        else
                        {
                            HorizonDir.y = -1;
                        }
                        //VerticalDir.y = -1;
                        break;
                        
                }
            }
            else
            {
                //anim
                stay = true;
            }
            
        }
        // 속도를 곱해서 적용합니다.
        HorizonDir.x *= Speed;
        

        
        if (nowState == State.Horizon) {
            VerticalDir = Vector3.zero;
            if (isPlayerGrounded || Player.isGrounded)
            {
                HorizonDir.y = 0;
                HorizonDir.z = 0;

                if (isJumpButtonPressing == true)
                {
                    isJumpButtonPressing = false;

                }
            }
            // 캐릭터가 바닥에 붙어 있지 않다면
            else
            {
                if ( (HorizonDir.y > 0 ))
                {
                    jumpUp = true;
                }
                else if(HorizonDir.y < 0)
                {
                    jumpDown = true;
                }
                // 중력의 영향을 받아 아래쪽으로 하강합니다.           
                HorizonDir.y -= Gravity * Time.deltaTime;
            }
            Player.Move(HorizonDir * Time.deltaTime);
        }
        else
        {
            
            HorizonDir *= Speed;
            Player.Move(HorizonDir * Time.deltaTime);
            HorizonDir.y = 0;
        }
    }

    public bool OnGround()
    {
        if (isPlayerGrounded || Player.isGrounded) return true;
        else return false;
    }

    public void IsJumpButtonPressing()
    {
        if (isPlayerGrounded)
        {
            GameManager.Instance.sound.Play("jumpEffect");
            StartCoroutine("JumpStart");
            HorizonDir.y = JumpPow;
            isJumpButtonPressing = true;
            isPlayerGrounded = false;
        }
            
    }
    public void isGrounded()
    {
        isPlayerGrounded = true;
    }
    public void isNotGrounded()
    {
        isPlayerGrounded = false;
    }

    IEnumerator JumpStart()
    {
        Debug.Log("Jumpstart_sequence");
        Foot.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        Foot.SetActive(true);
        Debug.Log("JumpEnd");




    }
}
