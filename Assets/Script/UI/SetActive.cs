using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;

    public void ActiveTrue()
    {
        Obj.SetActive(true);
    }
    public void ActiveFalse()
    {
        Obj.SetActive(false);
    }

}
