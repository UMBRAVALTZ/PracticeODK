using System.Collections;
using System.Collections.Generic;


public class AnswerEveryday : EverydayDialogues
{
    public string AnswerString { get; set; }

    public AnswerEveryday(string answerString, int stringKey, int sequenceNumber) : base (answerString, stringKey, sequenceNumber)
    {
        this.AnswerString = answerString;

    }


}
