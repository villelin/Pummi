using UnityEngine;
using System.Collections;

public class Martta : NPC
{
    DialogPage page1;
    DialogPage page2;
    DialogPage page3;
    DialogPage page4;
    DialogPage page5;
    DialogPage page6;
    DialogPage page7;

    public Martta() : base("Martta")
    {
        page1 = new DialogPage(DialogPageImage.Martta);
        page2 = new DialogPage(DialogPageImage.Martta);
        page3 = new DialogPage(DialogPageImage.Martta);
        page4 = new DialogPage(DialogPageImage.Martta);
        page5 = new DialogPage(DialogPageImage.Martta);
        page6 = new DialogPage(DialogPageImage.Martta);
        page7 = new DialogPage(DialogPageImage.Martta);

        page1.SetText("Hmph.. Vai että kadun mies.. Mitä sinä haluat?!");
        page1.SetReply(0, "Pate vaan, tarvin rahaa", page2);
        page1.SetReply(1, "Olen Pate, entäs sinä?", page2);

        page2.SetText("Ja pyh! Menisit töihin pummi!");
        page2.SetReply(0, "Tarkoitus olisi, pitäisi vain ensin päästä metroon", page4);
        page2.SetReply(1, "Älä sinä akka mäkätä siinä!", page3);

        page3.SetText("Kuinka julkeat! Mene matkoihisi, senkin haisuli!");
        page3.SetLast("Okei...");

        page4.SetText("Vai sillä tavalla.. Mitä se metroonpääsy auttaa?");
        page4.SetReply(0, "Auttaahan se elämässä eteenpäin...", page5);
        page4.SetReply(1, "Menen kotiin suihkuun ja siitä työhaastatteluun", page6);
        page4.SetReply(3, "Menen ostamaan viinaa", page7);

        page5.SetText("No... Ota tästä 50 senttiä...");
        page5.SetLast("JES!");
        page5.SetReward(0.5);

        page6.SetText("Pyh! Ja valehteletkin vielä, senkin juoppo! Mene matkoihisi!");
        page6.SetLast("Okei...");

        page7.SetText("No.. Olet ainakin rehellinen.. Ota tästä 2 euroa");
        page7.SetLast("JES!");
        page7.SetReward(2.0);

        AdvanceDialog(page1);
    }
}