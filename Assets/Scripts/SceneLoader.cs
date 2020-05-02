using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;

    public enum Scene
    {
        IntroScene,
        MainMenu,
        Quad,
        PathToLockett,
        LockettBasement,
        LockettBasementBossArea,
        LockettUpstairs,
        Tutorial
    }

    private Scene? sceneToLoad;

    public void FadeOutToScene(Scene scene)
    {
        animator.SetTrigger("FadeOut");
        sceneToLoad = scene;
    }

    public void FadeOutToSceneCompleted()
    {
        animator.SetTrigger("FadeIn");
        SceneManager.LoadScene(sceneToLoad.ToString());
        sceneToLoad = null;
    }
}
