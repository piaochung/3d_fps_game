using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class server : MonoBehaviour
{
    void Start()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
