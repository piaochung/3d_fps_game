using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string verticalAxisName = "Vertical";
    public string horizontalAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";

    public float vertical { get; private set; }
    public float horizontal { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isGameOver)
        {
            vertical = 0;
            horizontal = 0;
            fire = false;
            reload = false;
            return;
        }

        vertical = Input.GetAxisRaw(verticalAxisName);
        horizontal = Input.GetAxisRaw(horizontalAxisName);
        fire = Input.GetButtonDown(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
