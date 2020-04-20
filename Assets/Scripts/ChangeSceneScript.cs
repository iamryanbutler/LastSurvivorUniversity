using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public float video_time = 5f;
    void Start() => StartCoroutine(Intro_Wait());

    IEnumerator Intro_Wait()
    {
        yield return new WaitForSeconds(video_time);

        SceneManager.LoadScene(SceneLoader.Scene.MainMenu.ToString());
    }
}
