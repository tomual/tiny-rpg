using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<Page> dialogue;
    public int index;
    GameObject player;
    public float cooldown;
    public float lastInteraction;
    public bool talking;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogue = new List<Page>();
        index = 0;
        talking = false;
        cooldown = 1f;
    }

    void Update()
    {
        if (talking && Time.time - lastInteraction > cooldown)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (index >= dialogue.Count)
                {
                    EndConversation();
                } else
                {
                    ShowNextPage();
                }
            }
        }
    }

    public void ShowNextPage()
    {
        Debug.Log(dialogue[index].GetMessage());
        index++;
        lastInteraction = Time.time;
    }

    public void StartConversation()
    {
        Debug.Log(dialogue.Count);
        if (dialogue.Count > 0)
        {
            talking = true;
            ShowNextPage();
        }
    }

    public void EndConversation()
    {
        index = 0;
        talking = false;
        player.GetComponent<Player>().SetListening(false);
    }

    public void AddPage(string message, Hashtable options)
    {
        Page page = new Page(message, options);
        dialogue.Add(page);
    }
}

public class Page
{
    private string message;
    private Hashtable options;

    public Page(string message)
    {
        this.message = message;
        this.options = null;
    }

    public Page(string message, Hashtable options)
    {
        this.message = message;
        this.options = options;
    }

    public bool HasOptions()
    {
        return this.options != null;
    }

    public string GetMessage()
    {
        return message;
    }
}