using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() => GlobalController.CurrentInstance.SceneLoader.FadeOutToScene(SceneLoader.Scene.Quad);
    public void LoadGame() => Debug.Log("Load Game Function Call Not Yet Implemented");
    public void QuitGame() => Application.Quit();
}
