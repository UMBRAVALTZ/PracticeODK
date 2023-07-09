using System.Collections;
using System.Collections.Generic;

public abstract class EverydayDialogues : DialogClass
{
    public int StringKey { get; set; }
    public int SequenceNumber { get; set; }

    public EverydayDialogues(string str, int stringKey, int SequenceNumber) : base (str)
    {
        this.StringKey = stringKey;
        this.SequenceNumber = SequenceNumber;
    }

}
