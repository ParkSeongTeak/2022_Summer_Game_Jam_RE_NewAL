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

    
    CharacterController Player; // ������ ĳ���� ��Ʈ�ѷ�
    public float Speed;  // �̵��ӵ�
    const float JumpPow = 8.0f;


    private float Gravity; // �߷�   
    private Vector3 HorizonDir; // ĳ������ ���ι��� �����̴� ����.
    private Vector3 VerticalDir; // ĳ������ ���ι��� �����̴� ����.
    [SerializeField]
    bool _isPlayerGrounded = true;
    private bool isPlayerGrounded { get { return _isPlayerGrounded; } set { _isPlayerGrounded = value; } }  //  ���� ���� ��ư ���� ����
    private bool isJumpButtonPressing;

    //Sound���� 
    float walkSoundTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        nowState = State.Horizon;
        beforeState = State.Horizon;
        anim =  transform.GetChild(0).GetComponent<Animator>();

        Player = this.gameObject.GetComponent<CharacterController>();

        // �⺻��
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
        // �ӵ��� ���ؼ� �����մϴ�.
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
            // ĳ���Ͱ� �ٴڿ� �پ� ���� �ʴٸ�
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
                // �߷��� ������ �޾� �Ʒ������� �ϰ��մϴ�.           
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
