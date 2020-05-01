using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterQueue : MonoBehaviour
{
    private List<TypeWriter> typeWriterList;

    public TypeWriterQueue() => typeWriterList = new List<TypeWriter>();

    public TypeWriter AddWriter(TextMeshProUGUI uiTextObj, string messageToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
            RemoveWriter(uiTextObj);

        TypeWriter typewriter = new TypeWriter(uiTextObj, messageToWrite, timePerCharacter, invisibleCharacters);
        typeWriterList.Add(typewriter);
        return typewriter;
    }

    private void RemoveWriter(TextMeshProUGUI uiTextObj)
    {
        for(int i = 0; i < typeWriterList.Count; i++)
        {
            if(typeWriterList[i].GetUITextObj() == uiTextObj)
                typeWriterList.RemoveAt(i--);
        }
    }

    void Update()
    {
        for (int i = 0; i < typeWriterList.Count; i++) {
            bool destroyInstance = typeWriterList[i].Update();

            if (destroyInstance)
                typeWriterList.RemoveAt(i--);
        }
    }

    public class TypeWriter
    {
        private TextMeshProUGUI uiTextObj;
        private string messageToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public TypeWriter(TextMeshProUGUI uiTextObj, string messageToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            this.uiTextObj = uiTextObj;
            this.messageToWrite = messageToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            characterIndex = 0;
        }

        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = messageToWrite.Substring(0, characterIndex);

                if (invisibleCharacters)
                    text += "<color=#00000000>" + messageToWrite.Substring(characterIndex) + "</color>";

                uiTextObj.text = text;

                if (characterIndex >= messageToWrite.Length)
                    return true;
            }
            return false;
        }

        public TextMeshProUGUI GetUITextObj() => uiTextObj;

        public bool IsActive() => characterIndex < messageToWrite.Length;

        public void WriteAllAndDestroy()
        {
            uiTextObj.text = messageToWrite;
            characterIndex = messageToWrite.Length;
            GlobalController.CurrentInstance.TypeWriterQueue.RemoveWriter(uiTextObj);
        }
    }
}
