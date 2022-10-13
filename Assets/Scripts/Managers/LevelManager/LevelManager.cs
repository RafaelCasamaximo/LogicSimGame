using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonPersistent<LevelManager>
{
    public async void LoadScene(string sceneName)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        GUIManager.Instance.HideAll();
        GUIManager.Instance.Show("LoadingGUI");
        do
        {
            continue;
        } while (scene.progress < 0.9f);
        GUIManager.Instance.HideAll();
        scene.allowSceneActivation = true;
    }
    
    public async void LoadScene(string sceneName, GameState nextGameState)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        GUIManager.Instance.HideAll();
        GUIManager.Instance.Show("LoadingGUI");
        do
        {
            continue;
        } while (scene.progress < 0.9f);
        GUIManager.Instance.HideAll();
        scene.allowSceneActivation = true;
        GameManager.Instance.ChangeState(nextGameState);
    }

    
    
}
