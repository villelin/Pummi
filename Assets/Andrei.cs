using System;
using System.Collections;
using System.Collections.Generic;

public class Andrei : NPC
{
    public Andrei() : base("Andrei")
    {
        pages.Add("page1", new global::DialogPage(DialogPageImage.Andrei, "andrei_page1"));
        pages.Add("page2", new global::DialogPage(DialogPageImage.Andrei, "andrei_page2"));
        pages.Add("page3", new global::DialogPage(DialogPageImage.Andrei, "andrei_page3"));
        pages.Add("page4", new global::DialogPage(DialogPageImage.Andrei, null));
        pages.Add("page5", new global::DialogPage(DialogPageImage.Andrei, "andrei_page5"));
        pages.Add("page6", new global::DialogPage(DialogPageImage.AndreiAngry, "andrei_page6"));
        pages.Add("page7", new global::DialogPage(DialogPageImage.AndreiAngry, "andrei_page6"));

		Random rnd = new Random();

		double n;

		n = rnd.NextDouble ();

		if (n < 0.4) {
			pages["page7"].SetText ("JUMANSVIIDU MIKÄ MIELISTELIJÄ! RAHAT TÄNNE");
			pages["page7"].SetLast ("APUAAA");
			pages["page7"].SetReward (-float.MaxValue);

			AdvanceDialog(pages["page7"]);

		} else {
			pages["page1"].SetText ("Jahashh... Mikässhh miässh she shinä olethh?");
			pages["page1"].SetReply (0, "Pate vaan, tarvin rahaa", pages["page2"]);
            pages["page1"].SetReply (1, "Olen Pate, entäs sinä?", pages["page3"]);

            pages["page2"].SetText ("PAINU HIITEEN, PUMMI!");
            pages["page2"].SetLast ("Okei...");

            pages["page3"].SetText ("Voi että, et uskokaan, kun minulla on ollut niin rankkaa… Bisness pyöri ja elämä sujui.. Mutta nyt on kaikki kuviot kuule kusseet ja pahasti…");
            pages["page3"].SetReply (0, "Lähde pois", pages["page4"]);
            pages["page3"].SetReply (1, "Jatka kuuntelua", pages["page5"]);
            pages["page3"].SetReply (3, "Sano jotain myötätuntoista", pages["page6"]);

            pages["page4"].SetText ("Et saanut rahaa");
            pages["page4"].SetLast ("HÖH");

            // Andrei antaa 20e:
            pages["page5"].SetText ("...ensin puukottaa liikekumppanit selkään, vaikka monenmoista jeesiä hänelle tarjosin ja koulutin kuule sen pikkuvesselistä alalle, oli mulle kuin oma poika.. Ja vaimokin saamari läx sen matkaan ja koirakin perkele karkasi… Kuulee… Sinä.. Pate? Sinä oot kuule hyvä jätkä, Pate! Ota kuule tästä, saat kakskymppiä!");
            pages["page5"].SetLast ("JES!");
            pages["page5"].SetReward (20.0);

            // Andrei ryöstää:
            pages["page6"].SetText ("JUMANSVIIDU MIKÄ MIELISTELIJÄ! RAHAT TÄNNE");
            pages["page6"].SetLast ("APUAAA");
            pages["page6"].SetReward (-float.MaxValue);

			AdvanceDialog (pages["page1"]);
		}
    }
}
