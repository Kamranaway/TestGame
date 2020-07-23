using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = SceneList.Scene;

public class LoadScene : MonoBehaviour
{
    [SerializeField] Scene loadableScene;
    int currentIndex;

    private void Awake()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadSceneOfNextIndex() 
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        currentIndex++;
        SceneManager.LoadScene(currentIndex);
    }

    public void LoadSceneByIndex()
    {
        currentIndex = (int) loadableScene;
        SceneManager.LoadScene(currentIndex);
      
    }

    public void LoadSceneByName(string name)
    {
        currentIndex = SceneManager.GetSceneByName(name).buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
