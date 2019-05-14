using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable1 : Interactable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AddPage("Hello hello", null);
        AddPage("Tis a nice day today", null);
        AddPage("Would be a pity if I killed your wife", null);
    }
}
