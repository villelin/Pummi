using UnityEngine;
using System.Collections;

public class Andrei : NPC
{
    DialogPage page1;
    DialogPage page2;
    DialogPage page3;
    DialogPage page4;
    DialogPage page5;

    public Andrei()
    {
        page1 = new DialogPage();
        page2 = new DialogPage();
        page3 = new DialogPage();
        page4 = new DialogPage();
        page5 = new DialogPage();

        page1.SetText("TÖTTÖRÖÖ");
        page1.SetReply(0, "REPLY1", page2);
        page1.SetReply(1, "REPLY2", page3);

        page2.SetText("REPLY1 RESULT");
        page2.SetLast();

        page3.SetText("DIALOG PAGE 2");
        page3.SetReply(0, "REPLY1", page4);
        page3.SetReply(1, "REPLY2", page5);

        page4.SetText("D2 REPLY1 RESULT");
        page4.SetLast();

        page5.SetText("D2 REPLY2 RESULT");
        page5.SetLast();

        AdvanceDialog(page1);
    }
}
