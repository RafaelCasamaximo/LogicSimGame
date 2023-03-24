using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Essa classe faz o gerenciamento de cenas e levels dentro da Unity
/// TODO: Fazer com que ela identifique os scripts herdados no DontDestroyOnLoad (todos os base). Isso faz com que o jogo tenha que iniciar em uma cena de FreeGameplay ou gerencie o estado por cena com o GameManager
/// </summary>
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
        GUIManager.Instance.UnregisterAll();
        scene.allowSceneActivation = true;
        GameManager.Instance.ChangeState(nextGameState);
    }

    
    
}
