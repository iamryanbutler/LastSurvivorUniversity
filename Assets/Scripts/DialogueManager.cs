using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private TextMeshProUGUI typeWriterObj;
    private TypeWriterQueue.TypeWriter activeTypeWriter;
    private int messageIndexToWrite;
    private List<string> messages;
    private KeyCode skipKey;
    private GameObject player;
    private float playerHealth, playerShield, playerHealthUI, playerShieldUI;
    private List<GameObject> enemyControllers;

    void Awake()
    {
        instance = this;
        typeWriterObj = GlobalController.CurrentInstance.transform.Find("Canvas/TypeWriter/TypeWriterText").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("aj@Running");

        enemyControllers = new List<GameObject>();
        for (int _ = 0; _ < GameObject.Find("Enemies").transform.childCount; _++)
            enemyControllers.Add(GameObject.Find("Enemies").transform.GetChild(_).gameObject);
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

                        player.GetComponent<PlayerMovement>().enabled = true;
                        player.GetComponent<Weapon>().enabled = true; ;
                        player.GetComponent<playerHealth>().SetCurrentHealth(playerHealth);
                        player.GetComponent<playerHealth>().PlayerHealth.fillAmount = playerHealthUI;
                        player.GetComponent<playerHealth>().SetCurrentShield(playerShield);
                        player.GetComponent<playerHealth>().PlayerShield.fillAmount = playerShieldUI;

                        foreach (GameObject ec in enemyControllers)
                        {
                            ec.GetComponent<NavMeshAgent>().Resume();
                            ec.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                        }
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
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Weapon>().enabled = false; ;
            player.GetComponent<Animator>().SetBool("isRunning", false);
            player.GetComponent<Animator>().SetBool("isPunching", false);

            playerHealth = player.GetComponent<playerHealth>().GetCurrentHealth();
            playerHealthUI = player.GetComponent<playerHealth>().PlayerHealth.fillAmount;
            playerShield = player.GetComponent<playerHealth>().GetCurrentShield();
            playerShieldUI = player.GetComponent<playerHealth>().PlayerShield.fillAmount;

            player.GetComponent<playerHealth>().SetCurrentHealth(9999f);
            player.GetComponent<playerHealth>().SetCurrentShield(9999f);


            foreach (GameObject ec in enemyControllers)
            {
                ec.GetComponent<NavMeshAgent>().Stop();
                ec.transform.GetChild(0).GetComponent<Animator>().enabled = false;
            }

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
