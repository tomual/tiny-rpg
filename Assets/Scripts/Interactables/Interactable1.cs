using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Interactable1 : Interactable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AddPage("Hello hello hello hello hello hello hello hello hello hello hello hello hello", null);
        AddPage("Tis a nice day today", null);
        Hashtable options = new Hashtable();
        options.Add("Yes", "SelectYes");
        options.Add("No", "SelectNo");
        AddPage("Would be a pity if I killed your wife", options);


    }

    protected override void Update()
    {
        base.Update();
        if (waitingForInput)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (selectedOptionIndex == 0)
                {
                    Debug.Log(currentOptions["Yes"].ToString());
                    Debug.Log(currentOptions.Keys.GetEnumerator());

                    //Type thisType = this.GetType();
                    //MethodInfo theMethod = thisType.GetMethod(currentOptions["Yes"].ToString());
                    //theMethod.Invoke(this, null);

                    //var mi = typeof(Interactable1).GetMethod(currentOptions["Yes"].ToString());
                    //MethodInfo miConstructed = mi.MakeGenericMethod(typeof(string));
                    //miConstructed.Invoke(null, null);

                    string APIValue = currentOptions["Yes"].ToString();

                    var method = typeof(Interactable1).GetMethod(APIValue);

                    // Returns "22"
                    // As BB is static, the first parameter of Invoke is null
                    string result = (string)method.Invoke(null, null);
                }
                if (selectedOptionIndex == 1)
                {
                    Debug.Log(currentOptions["No"]);
                }
            }
        }
    }

    public static void SelectYes()
    {
        Debug.Log("Yes");
    }

    public static void SelectNo()
    {
        Debug.Log("No");
    }
}
