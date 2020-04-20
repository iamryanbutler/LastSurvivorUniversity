using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    public SceneLoader.Scene scene;

    void OnTriggerEnter(Collider other)
    {
        if (scene != null && this.enabled && other.CompareTag("Player"))
        {
            PlayerManager.SavePlayerInfo();
            this.enabled = false;
            GlobalController.CurrentInstance.SceneLoader.FadeOutToScene(scene);
        }
    }
}
