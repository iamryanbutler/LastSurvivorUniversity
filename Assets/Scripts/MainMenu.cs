using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GlobalController.CurrentInstance.PlayerInfoStorage.GetPlayerInfo() == null && !GlobalController.CurrentInstance.PlayerInfoStorage.playedTutorial)
        {
            GlobalController.CurrentInstance.SceneLoader.FadeOutToScene(SceneLoader.Scene.Tutorial);
            GlobalController.CurrentInstance.PlayerInfoStorage.playedTutorial = true;
        }
        else
            GlobalController.CurrentInstance.SceneLoader.FadeOutToScene(SceneLoader.Scene.Quad);
    }
    public void LoadGame() => Debug.Log("Load Game Function Call Not Yet Implemented");
    public void QuitGame() => Application.Quit();
}
