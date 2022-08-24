using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prol_ : MonoBehaviour
{

    Image image;

    public Sprite[] sprites;

    private void Awake()
    {
        image = this.gameObject.GetComponent<Image>();
        StartCoroutine("Anim");

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    

    IEnumerator Anim()
    {
        while (true)
        {
            image.sprite = sprites[0];
            yield return new WaitForSecondsRealtime(0.25f);
            image.sprite = sprites[1];
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

}
