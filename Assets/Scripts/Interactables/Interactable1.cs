using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable1 : Interactable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AddPage("Hello hello hello hello hello hello hello hello hello hello hello hello hello", null);
        AddPage("Tis a nice day today", null);
        Dictionary<string, string> options = new Dictionary<string, string>();
        options.Add("Yes", "pressyes");
        options.Add("No", "pressno");
        AddPage("Would be a pity if I killed your wife", options);
    }
}
