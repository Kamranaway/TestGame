using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = SceneList.Scene;

/*
 * Script for loading different scenes.
 * Meant for transition from menus and possibly later for changing levels.
 */
public class LoadScene : MonoBehaviour
{
    [SerializeField] Scene loadableScene;
    int currentIndex;

    private void Awake()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    //Loads the scene of the next index up 
    public void LoadSceneOfNextIndex() 
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        currentIndex++;
        SceneManager.LoadScene(currentIndex);
    }

    //Load a scene by the Unity scene build index. Change @loadableScene rather than passing a parameter. 
    public void LoadSceneByIndex()
    {
        currentIndex = (int) loadableScene;
        SceneManager.LoadScene(currentIndex);
      
    }

    //Load a scene by it's given name
    public void LoadSceneByName(string name)
    {
        currentIndex = SceneManager.GetSceneByName(name).buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
