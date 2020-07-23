using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * List of all scenes and expected index values. 
 * 
 * Will later replace with script that indexes scenes automatically on Awake().
 */
public class SceneList : MonoBehaviour
{
    public enum Scene
    {
        MENU_START = 0,
        ROOM_DEV = 1,
    }
}
