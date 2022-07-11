using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputManager
{
    public Action KeyAction = null;
    public void OnUpdata()
    {
        if (Input.anyKey == false)
        {
            return;
        }
        if (KeyAction != null)
        {
            KeyAction.Invoke();     //전파 시작
        }
    }

}
