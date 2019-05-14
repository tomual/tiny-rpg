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
                Debug.Log("Next Page");
            }
        }
    }

    public void ShowNextPage()
    {
        Debug.Log(dialogue[0].GetMessage());
        index++;
        lastInteraction = Time.time;
    }

    public virtual void StartConversation()
    {
        Debug.Log(dialogue.Count);
        if (dialogue.Count > 0)
        {
            talking = true;
            ShowNextPage();
        }
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