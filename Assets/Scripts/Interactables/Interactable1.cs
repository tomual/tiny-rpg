using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable1 : Interactable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        string message = "Hello hello";
        Page page = new Page(message);
        dialogue.Add(page);
    }
}
