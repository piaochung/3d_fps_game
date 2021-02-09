using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";

    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isGameOver)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }

        move = Input.GetAxisRaw(moveAxisName);
        rotate = Input.GetAxisRaw(rotateAxisName);
        fire = Input.GetButtonDown(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
