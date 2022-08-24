using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    Sprite[] Tutorial_Sprite = new Sprite[6];
    int idx = 1;
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Tutorial_Sprite[i] = Resources.Load<Sprite>("Sprite/Tutorial/tutorial_" + i.ToString());

        }
    }
    public void Tutorial_Touch()
    {
        if (idx<6)
        {
            this.transform.GetComponent<Image>().sprite = Tutorial_Sprite[idx];

            idx++;

        }
        else
        {
            idx = 1;
            this.transform.GetComponent<Image>().sprite = Tutorial_Sprite[0];
            UiManager.instance.Pause_End();
            this.gameObject.SetActive(false);
        }
    }
}
