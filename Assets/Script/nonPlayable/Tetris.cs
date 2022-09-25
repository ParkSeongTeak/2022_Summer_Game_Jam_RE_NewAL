using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public static Tetris tetris;

    [Header("Editor Objects")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;
    public GameObject gameoverPanel;
    public GameObject Player;

    [Header("Game Settings")]
    [Range(4, 40)]
    int boardWidth = 10;
    [Range(5, 1500)]
    public int boardHeight = 20;
    public int StartHeight = 25;

    public float fallCycle = 1.0f;

    private int halfWidth;
    private int halfHeight;
    //private int TetrisStartinHeight;


    /// <summary>
    /// 디버깅중 이후 모든 퍼블릭 해제할것
    /// 
    /// </summary>
    //int[] NextTetrisIDX = new int[5];
    //int[] NextTetrisNeedle = new int[5];
    public int[] NextTetrisIDX = new int[5];
    public int[] NextTetrisNeedle = new int[5];
    public Sprite[] Sprites;


    private float nextFallTime;
    //Sprite[] Sprites;
    //bool moveDownFast;

    string Path = "Sprite/TetrisBlock/";
    string Norm = "block";
    string Needle = "needleblock";

    int needleORNorm;
    int normPer;
    int _tetrisMaxnum = 5;

    int tetrisMaxnum { get { return _tetrisMaxnum; } set { _tetrisMaxnum = value; } }
    bool isRotate = false;


    bool tick = true;

    private void Awake()
    {
        Debug.Log("RERE");
        tetris = this;

        gameoverPanel.SetActive(false);

        GameManager.Instance.RandArrCreate();

        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);

        nextFallTime = Time.time + fallCycle;

        CreateBackground();

        for (int i = 0; i < boardHeight; ++i)
        {
            var col = new GameObject((boardHeight - i - 1).ToString());
            col.transform.position = new Vector3(0, halfHeight - i, 0);
            col.transform.parent = boardNode;
        }



        StartCoroutine("Tick");
    }

    private void Start()
    {
        /*
        Debug.Log("RERE");
        tetris = this;

        gameoverPanel.SetActive(false);

        GameManager.Instance.RandArrCreate();

        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);

        nextFallTime = Time.time + fallCycle;

        CreateBackground();

        for (int i = 0; i < boardHeight; ++i)
        {
            var col = new GameObject((boardHeight - i - 1).ToString());
            col.transform.position = new Vector3(0, halfHeight - i, 0);
            col.transform.parent = boardNode;
        }



        StartCoroutine("Tick");
        //CreateTetromino();
        */
    }

    void Update()
    {
        if (GameManager.Instance.time2 == true )
        {
            if (gameoverPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
            }
            else
            {
                
                Vector3 moveDir = Vector3.zero;
                
                if (GameManager.Instance.MoveSliderValue()<=0.3f)
                {
                    if (tick)
                    {
                        moveDir.x = -1;
                        tick = false;
                    }

                }
                else if (GameManager.Instance.MoveSliderValue() >= 0.7f)
                {
                    if (tick)
                    {
                        moveDir.x = 1;
                        tick = false;

                    }

                }

                // 아래로 떨어지는 경우는 강제로 이동시킵니다.
                if (Time.time > nextFallTime)
                {
                    nextFallTime = Time.time + fallCycle;
                    moveDir = Vector3.down;
                    isRotate = false;
                }

                if (moveDir != Vector3.zero || isRotate)
                {
                    MoveTetromino(moveDir);
                }
            }
            
        }
        else if (GameManager.Instance.time2 == true && GameManager.Instance.tetris_Num >= tetrisMaxnum)
        {
            GameManager.Instance.ToTime1();
        }
    }
    public void TetrisRotate()
    {
        isRotate = true;
    }

    public void TetrisDown()
    {

        Vector3 moveDir = Vector3.zero;
        moveDir.y -= 1;
        MoveTetromino(moveDir);
    }

    bool MoveTetromino(Vector3 moveDir)
    {
        //if (moveDownFast) { moveDownFast = false; moveDir.y -= 1; }

        Vector3 oldPos = tetrominoNode.transform.position;
        Quaternion oldRot = tetrominoNode.transform.rotation;

        tetrominoNode.transform.position += moveDir;
        if (isRotate)
        {
            tetrominoNode.transform.rotation *= Quaternion.Euler(0, 0, 90);
            isRotate = false;
        }

        if (!CanMoveTo(tetrominoNode))
        {
            GameManager.Instance.sound.Play("BlockDown");

            tetrominoNode.transform.position = oldPos;
            tetrominoNode.transform.rotation = oldRot;

            if ((int)moveDir.y == -1 && (int)moveDir.x == 0 && isRotate == false)
            {
                GameManager.Instance.tetris_Num = GameManager.Instance.tetris_Num + 1;          //38

                AddToBoard(tetrominoNode);
                if (GameManager.Instance.time1) { return false; }
                //CheckBoardColumn();
                CreateTetromino();

                if (!CanMoveTo(tetrominoNode))
                {
                    gameoverPanel.SetActive(true);
                }
            }

            return false;
        }

        return true;
    }

    // 테트로미노를 보드에 추가
    void AddToBoard(Transform root)
    {
        while (root.childCount > 0)
        {
            var node = root.GetChild(0);

            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            node.parent = boardNode.Find(y.ToString());
            node.name = x.ToString();
        }
    }

   
    // 이동 가능한지 체크
    bool CanMoveTo(Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            var node = root.GetChild(i);
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            if (x < 0 || x > boardWidth - 1)
                return false;

            if (y < 0)
                return false;

            var column = boardNode.Find(y.ToString());

            if (column != null && column.Find(x.ToString()) != null)
                return false;
        }

        return true;
    }

    // 타일 생성
    Tile CreateTile(Transform parent, Vector2 position, Sprite[] imgs, int index = 0, bool isNeedle = false, bool isladder = false,int order = 1)
    {
        var go = Instantiate(tilePrefab);
        go.transform.parent = parent;
        go.transform.localPosition = position;
        go.GetComponent<SpriteRenderer>().sprite = imgs[index];
        if (isNeedle)
        {
            go.transform.tag = "Needle";
        }
        var tile = go.GetComponent<Tile>();
        tile.sortingOrder = order;
        tile.UseLadder = isladder;

        return tile;
    }

    // 배경 타일을 생성

    
    void CreateBackground()
    {
        Color color = Color.gray;
        //Sprites = Resources.LoadAll<Sprite>("Sprite/Empty");
        //TetrisBlock
        Sprites = Resources.LoadAll<Sprite>("Sprite/TetrisBlock/block4");
        
        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            //CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), Sprites, 0);
            //CreateTile(backgroundNode, new Vector2(halfWidth, y), Sprites, 0);
 
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), Sprites, 1);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), Sprites, 1);


        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            //CreateTile(backgroundNode, new Vector2(x, -halfHeight), Sprites, 0);
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), Sprites, 1);

        }
    }
    
    // 테트로미노 생성
    public void CreateTetromino()
    {

        bool isNeedle;
        bool isladder = false;
        NextTetrisIDX = GameManager.Instance.GetTetrisArrIDX();
        NextTetrisNeedle = GameManager.Instance.GetTetrisArrNeedle();
        int index = NextTetrisIDX[0];
        needleORNorm = NextTetrisNeedle[0];
  
                
        GameManager.Instance.PopTetris();
        
        if (UiManager.instance)
        UiManager.instance.NextTTShow();

        tetrominoNode.rotation = Quaternion.identity;
        //tetrominoNode.position = new Vector2(0, -halfHeight + StartHeight);
        tetrominoNode.position = new Vector2(0, (int)(Player.transform.position.y) + StartHeight);

        if (needleORNorm < GameManager.Instance.normPer)
        {
            isNeedle = false;
            isladder = false;

            Sprites = Resources.LoadAll<Sprite>(Path + Norm + index.ToString());
        }
        else if (needleORNorm < GameManager.Instance.ladderPer)
        {
            isNeedle = false;
            isladder = true;

            Sprites = Resources.LoadAll<Sprite>(Path + Norm + index.ToString());

        }
        else
        {
            isNeedle = true;
            isladder = false;

            Sprites = Resources.LoadAll<Sprite>(Path + Needle + index.ToString());

        }

        switch (index)
        {
            // ㅁ. 
            case 0:
                CreateTile(tetrominoNode, new Vector2(0f, 1.0f), Sprites,0, isNeedle);
                CreateTile(tetrominoNode, new Vector2(1f, 1.0f), Sprites,1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), Sprites,3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), Sprites,4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(2f, 0.0f), Sprites,5, isNeedle, isladder);

                break;

            // Z 
            case 1:
                CreateTile(tetrominoNode, new Vector2(-1f, 1f), Sprites,0, isNeedle);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), Sprites, 1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                

                break;

            // ㄴ.
            case 2:
                
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), Sprites, 0, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), Sprites, 3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), Sprites, 4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), Sprites, 5, isNeedle, isladder);
                break;

            // O : 노란색
            case 3:
                CreateTile(tetrominoNode, new Vector2(0f, 1f), Sprites, 1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0f),Sprites, 3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                break;

            // S : 녹색
            case 4:
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites, 1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites, 2, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, -1f), Sprites,3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, -1f), Sprites,4, isNeedle, isladder);
                break;

            // L : 자주색
            case 5:
                CreateTile(tetrominoNode, new Vector2(1f, 1f), Sprites, 2, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), Sprites,3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites,4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites,5, isNeedle, isladder);
                break;

            // ㅡ : 빨간색
            case 6:
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), Sprites,0, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites,1, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites,2, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(2f, 0f), Sprites,3, isNeedle, isladder);
                break;

            //ㅁ : 
            case 7:
                CreateTile(tetrominoNode, new Vector2(-1f, 1f), Sprites,0, isNeedle);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), Sprites,1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), Sprites,2, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites,3, isNeedle, isladder);
                break;

            case 8:
                CreateTile(tetrominoNode, new Vector2(0f, 1f), Sprites, 1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), Sprites, 2, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites, 3, isNeedle, isladder);
                break;
            case 9:
                CreateTile(tetrominoNode, new Vector2(0f, 1f), Sprites, 1, isNeedle);
                CreateTile(tetrominoNode, new Vector2(1f, 1f), Sprites, 2, isNeedle);
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), Sprites, 3, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), Sprites, 4, isNeedle, isladder);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), Sprites, 5, isNeedle, isladder);
                break;
            
        }

        
    }


    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            tick = true;
        }
    }
    

}
