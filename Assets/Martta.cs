using System;
using System.Collections;
using System.Collections.Generic;

public class Martta : NPC
{
    public Martta() : base("Martta")
    {
        pages.Add("page1", new global::DialogPage(DialogPageImage.Martta, "martta_page1"));
        pages.Add("page2", new global::DialogPage(DialogPageImage.Martta, "martta_page2"));
        pages.Add("page3", new global::DialogPage(DialogPageImage.Martta, "martta_page3"));
        pages.Add("page4", new global::DialogPage(DialogPageImage.Martta, "martta_page4"));
        pages.Add("page5", new global::DialogPage(DialogPageImage.Martta, "martta_page5"));
        pages.Add("page6", new global::DialogPage(DialogPageImage.Martta, "martta_page6"));
        pages.Add("page7", new global::DialogPage(DialogPageImage.Martta, "martta_page7"));

        pages.Add("page8", new global::DialogPage(DialogPageImage.Martta, "martta_page1"));
        pages.Add("page9", new global::DialogPage(DialogPageImage.Martta, "martta_page9"));
        pages.Add("page10", new global::DialogPage(DialogPageImage.Martta, "martta_page10"));
        pages.Add("page11", new global::DialogPage(DialogPageImage.Martta, "martta_page11"));

		Random rnd = new Random();

		double n;

		n = rnd.NextDouble ();

		if (n < 0.5) {

			pages["page8"].SetText ("Hmph.. Vai että kadun mies.. Mitä sinä haluat?!");
            pages["page8"].SetReply (0, "Pate vaan, tarvin rahaa", pages["page9"]);
            pages["page8"].SetReply (1, "Olen Pate, entäs sinä?", pages["page10"]);

            pages["page9"].SetText ("Yök, mikä tungetteleva juoppo! Häivy!");
            pages["page9"].SetLast ("Okei...");

            pages["page10"].SetText ("Olen Martta-neiti.. Mitä sinä täällä asemalla kuppaat?");
            pages["page10"].SetReply (0, "Menossa kattoo kaveria, mut pitäs metrolippu saada ostettua", pages["page11"]);
            pages["page10"].SetReply (1, "Ihastelen teitä kauniita naisia", pages["page9"]);

            pages["page11"].SetText ("Hmph.. No on minulla kolme euroa laukun pohjalla..\nVaikka viinaanhan sinä haisuli sen kuitenkin käytät!");
            pages["page11"].SetLast ("Jee!");
            pages["page11"].SetReward (3.0);


			AdvanceDialog (pages["page8"]);

		} else {

            pages["page1"].SetText ("Hmph.. Vai että kadun mies.. Mitä sinä haluat?!");
            pages["page1"].SetReply (0, "Pate vaan, tarvin rahaa", pages["page2"]);
            pages["page1"].SetReply (1, "Olen Pate, entäs sinä?", pages["page2"]);

            pages["page2"].SetText ("Ja pyh! Menisit töihin pummi!");
            pages["page2"].SetReply (0, "Tarkoitus olisi, pitäisi vain ensin päästä metroon", pages["page4"]);
            pages["page2"].SetReply (1, "Älä sinä akka mäkätä siinä!", pages["page3"]);

            pages["page3"].SetText ("Kuinka julkeat! Mene matkoihisi, senkin haisuli!");
            pages["page3"].SetLast ("Okei...");

            pages["page4"].SetText ("Vai sillä tavalla.. Mitä se metroonpääsy auttaa?");
            pages["page4"].SetReply (0, "Auttaahan se elämässä eteenpäin...", pages["page5"]);
            pages["page4"].SetReply (1, "Menen kotiin suihkuun ja siitä työhaastatteluun", pages["page6"]);
            pages["page4"].SetReply (3, "Menen ostamaan viinaa", pages["page7"]);

            pages["page5"].SetText ("No... Ota tästä 50 senttiä...");
            pages["page5"].SetLast ("JES!");
            pages["page5"].SetReward (0.5);

            pages["page6"].SetText ("Pyh! Ja valehteletkin vielä, senkin juoppo! Mene matkoihisi!");
            pages["page6"].SetLast ("Okei...");

            pages["page7"].SetText ("No.. Olet ainakin rehellinen.. Ota tästä 2 euroa");
            pages["page7"].SetLast ("JES!");
            pages["page7"].SetReward (2.0);

			AdvanceDialog (pages["page1"]);
		}
    }
}
