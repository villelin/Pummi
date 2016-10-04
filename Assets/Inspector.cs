using UnityEngine;
using System.Collections;

public class Inspector : NPC
{
    private DialogPage page1;

    public Inspector() : base("Lipuntarkastaja")
    {
        page1 = new DialogPage(DialogPageImage.Inspector, null);

        page1.SetText("A WILD INSPECTOR APPEARS!\n\nHemmetin pummi. Nyt saat keppiä!");
        page1.SetLast("Pakene!");
        page1.SetEscapeMiniGame();

        AdvanceDialog(page1);
    }
}
