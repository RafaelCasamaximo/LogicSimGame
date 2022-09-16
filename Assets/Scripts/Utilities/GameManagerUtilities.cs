using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUtilities : MonoBehaviour
{
    public static void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    
}
