using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TorchIsHit : MonoBehaviour
{
    bool isColliding;
    public bool isLit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    public bool GetIsColliding()
    {
        return isColliding;
    }
}
