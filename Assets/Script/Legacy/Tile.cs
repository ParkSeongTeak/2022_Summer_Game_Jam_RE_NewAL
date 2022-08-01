using UnityEngine;

public class Tile : MonoBehaviour
{

    GameObject Ladder;
    bool _useLadder = false;
    public bool UseLadder { get { return _useLadder; }  set { _useLadder = value; } }

    public Color color
    {
        set
        {
            spriteRenderer.color = value;
        }

        get
        {
            return spriteRenderer.color;
        }
    }

    public int sortingOrder
    {
        set
        {
            spriteRenderer.sortingOrder = value;
        }

        get
        {
            return spriteRenderer.sortingOrder;
        }
    }

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("You need to SpriteRenderer for Block");
        }
    }

    private void Start()
    {
        Ladder = gameObject.transform.GetChild(1).gameObject;

        if(_useLadder)
        {
            Ladder.SetActive(true);
        }
        else
        {
            Ladder.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (this.transform.tag == "Needle"  && collision.gameObject.layer == 3)
        {
            Debug.Log("Needle");
            GameManager.Instance.GameOver();
        }
    }

}
