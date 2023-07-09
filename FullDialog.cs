using System.Collections.Generic;
using System.Linq;

public class FullDialog
{
    
    public List<string> DialogStrings { get; set; }


    public FullDialog (IEnumerable<string> dialogStrings)
    {
        this.DialogStrings = dialogStrings.ToList<string>();

    }

}
