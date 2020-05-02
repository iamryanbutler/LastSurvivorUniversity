using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<string> messages;

    void OnTriggerEnter(Collider other)
    {
        if (this.enabled && other.CompareTag("Player"))
        {
            DialogueManager.instance.PushDialogue(messages);
            this.enabled = false;
        }
    }
}