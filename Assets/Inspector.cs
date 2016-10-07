using UnityEngine;
using System.Collections;

public class Inspector : NPC
{
    public Inspector() : base("Lipuntarkastaja")
    {
        pages.Add("page1", new DialogPage(DialogPageImage.Inspector, null));

        pages["page1"].SetText("A WILD INSPECTOR APPEARS!\n\nHemmetin pummi. Nyt saat keppiä!");
        pages["page1"].SetLast("OK");
        pages["page1"].SetEscapeMiniGame();

        AdvanceDialog(pages["page1"]);
    }
}
