using System.Collections;
using System.Collections.Generic;


public class QuestionEveryday : EverydayDialogues
{
    public string QuestionString { get; set; }

    public QuestionEveryday(string questionString, int stringKey, int sequenceNumber) : base(questionString, stringKey, sequenceNumber)
    {
        this.QuestionString = questionString;

    }

}
