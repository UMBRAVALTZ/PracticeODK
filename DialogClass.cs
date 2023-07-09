using System.Collections;
using System.Collections.Generic;

public abstract class DialogClass
{
    public string DialogString { get; set; }

    public DialogClass() { }
    public DialogClass (string dialogStr)
    {
        this.DialogString = dialogStr;
    }

    

}
