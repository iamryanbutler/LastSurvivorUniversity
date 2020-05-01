using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private TextMeshProUGUI typeWriterObj;
    private TypeWriterQueue.TypeWriter activeTypeWriter;
    private int messageIndexToWrite;
    private List<string> messages;
    private KeyCode skipKey;

    void Awake()
    {
        instance = this;
        typeWriterObj = GlobalController.CurrentInstance.transform.Find("Canvas/TypeWriter/TypeWriterText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (messages != null)
        {
            if (Input.GetKeyDown(skipKey) && messageIndexToWrite <= messages.Count || messageIndexToWrite == 0)
            {
                if (activeTypeWriter != null && activeTypeWriter.IsActive())
                    activeTypeWriter.WriteAllAndDestroy();
                else
                {
                    if (messageIndexToWrite == 0)
                        GlobalController.CurrentInstance.transform.Find("Canvas/TypeWriter").gameObject.SetActive(true);
                    else if (messageIndexToWrite == messages.Count)
                    {
                        GlobalController.CurrentInstance.transform.Find("Canvas/TypeWriter").gameObject.SetActive(false);
                        messages = null;
                        return;
                    }
                    activeTypeWriter = GlobalController.CurrentInstance.TypeWriterQueue.AddWriter(typeWriterObj, messages[messageIndexToWrite++], 0.05f, true, true);
                }
            }
        }
    }

    public void PushDialogue(List<string> messages, KeyCode skipKey = KeyCode.Space)
    {
        if (this.messages == null)
        {
            this.messageIndexToWrite = 0;
            this.skipKey = skipKey;
            this.messages = messages;
        }
        else
        {
            foreach (string s in messages)
                this.messages.Add(s);
        }
    }

    public void ReplayPrevious()
    {
        if (this.messageIndexToWrite < 2)
            return;
        this.messageIndexToWrite -= 2;
        activeTypeWriter = GlobalController.CurrentInstance.TypeWriterQueue.AddWriter(typeWriterObj, messages[messageIndexToWrite++], 0.05f, true, true);
    }
}
