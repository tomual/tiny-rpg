using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public List<Page> dialogue;
    public int index;
    GameObject player;
    public float cooldown;
    public float lastInteraction;
    public bool talking;
    public bool scrolling;
    float lastPrint;
    float scrollSpeed;
    int scrollingCursor;
    GameObject panel;
    GameObject panelOptions;
    public GameObject panelOptionsCursor;
    Text panelText;
    string currentLine;
    public bool waitingForInput;

    Vector2 yesCursorPosition;
    Vector2 noCursorPosition;
    public int selectedOptionIndex;


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogue = new List<Page>();
        index = 0;
        talking = false;
        cooldown = 0.5f;
        panel = GameObject.FindGameObjectWithTag("DialoguePanel");
        panelOptions = GameObject.FindGameObjectWithTag("DialogueOptionsPanel");
        panelOptionsCursor = GameObject.FindGameObjectWithTag("DialogueOptionCursor");
        panelText = panel.GetComponentInChildren<Text>();
        scrolling = false;
        scrollSpeed = 0.005f;
        scrollingCursor = 0;
        waitingForInput = false;

        panel.SetActive(false);
        panelOptions.SetActive(false);

        yesCursorPosition = new Vector2(414, 133);
        noCursorPosition = new Vector2(414, 104);
    }

    protected virtual void Update()
    {
        if (!waitingForInput)
        {
            if (talking && Time.time - lastInteraction > cooldown)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    if (index >= dialogue.Count)
                    {
                        EndConversation();
                    }
                    else if (scrollingCursor != currentLine.Length)
                    {
                        scrollingCursor = currentLine.Length - 1;
                        panelText.text += currentLine.ToString();
                        lastInteraction = Time.time;
                    }
                    else 
                    {
                        ShowNextPage();
                    }
                }
            }

            if (scrolling && Time.time - lastPrint >= scrollSpeed)
            {
                panelText.text += currentLine.ToCharArray()[scrollingCursor].ToString();
                ++scrollingCursor;
                if (scrollingCursor == currentLine.Length)
                {
                    scrolling = false;
                    Debug.Log(dialogue[index - 1].GetOptions());
                    if (dialogue[index - 1].GetOptions() != null)
                    {
                        waitingForInput = true;
                        panelOptions.SetActive(true);
                        panelOptionsCursor.gameObject.transform.position = yesCursorPosition;
                        selectedOptionIndex = 0;
                    }
                    
                }
                lastPrint = Time.time;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log((Hashtable) dialogue[index - 1].GetOptions()[selectedOptionIndex]);
                panelOptionsCursor.gameObject.transform.position = yesCursorPosition;
                selectedOptionIndex = 0;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log((Hashtable)dialogue[index - 1].GetOptions()[selectedOptionIndex]);
                panelOptionsCursor.gameObject.transform.position = noCursorPosition;
                selectedOptionIndex = 1;
            }
        }
    }

    public void ShowNextPage()
    {
        //SetPanelText(dialogue[index].GetMessage());
        currentLine = dialogue[index].GetMessage();
        panelText.text = "";
        index++;
        scrollingCursor = 0;
        scrolling = true;
        lastInteraction = Time.time;
    }

    public void StartConversation()
    {
        if (dialogue.Count > 0)
        {
            talking = true;
            scrolling = true;
            panel.SetActive(true);
            currentLine = dialogue[index].GetMessage();
            ShowNextPage();
            Debug.Log(index);
            Debug.Log(currentLine);
            scrollingCursor = 0;
        }
    }
    #region code stuff

    public void EndConversation()
    {
        index = 0;
        talking = false;
        player.GetComponent<Player>().SetListening(false);
        panel.SetActive(false);
    }

    public void AddPage(string message, Hashtable options)
    {
        Page page = new Page(message, options);
        dialogue.Add(page);
    }

    public void SetPanelText(string message)
    {
        panelText.text = message;
    }
    #endregion
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

    public Hashtable GetOptions()
    {
        return options;
    }
}