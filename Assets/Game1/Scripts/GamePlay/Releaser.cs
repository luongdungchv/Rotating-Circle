using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Releaser : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("click" + Time.timeScale.ToString());
        
        if (!Ball.ins.isReleased && !Manager.isPaused)
        {
            Ball.ins.gameObject.transform.parent.gameObject.tag = "edge";
            Ball.ins.Release();
        }
    }
}
