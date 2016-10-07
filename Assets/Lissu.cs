using System;
using System.Collections;
using System.Collections.Generic;

public class Lissu : NPC
{
	public Lissu() : base("Lissu")
	{
        pages.Add("page1", new DialogPage(DialogPageImage.Lissu, "lissu_page1"));
        pages.Add("page2", new DialogPage(DialogPageImage.Lissu, "lissu_page2"));
        pages.Add("page3", new DialogPage(DialogPageImage.Lissu, "lissu_page3"));
        pages.Add("page4", new DialogPage(DialogPageImage.Lissu, null));
        pages.Add("page5", new DialogPage(DialogPageImage.Lissu, null));
        pages.Add("page6", new DialogPage(DialogPageImage.Lissu, "lissu_page6"));
        pages.Add("page7", new DialogPage(DialogPageImage.Lissu, "lissu_page1"));
        pages.Add("page8", new DialogPage(DialogPageImage.Lissu, "lissu_page8"));

		Random rnd = new Random();

		double n;

		n = rnd.NextDouble ();

		if (n < 0.6) {
			
			pages["page7"].SetText ("Hyi... Joku spuge...");
            pages["page7"].SetReply (0, "Pate vaan, tarvin rahaa", pages["page8"]);
            pages["page7"].SetReply (1, "Olen Pate, entäs sinä?", pages["page8"]);

            pages["page8"].SetText ("Hyi mee pois sä haiset!");
            pages["page8"].SetLast ("Okei...");

			AdvanceDialog (pages["page7"]);

		} else {

            pages["page1"].SetText ("Hyi... Joku spuge...");
            pages["page1"].SetReply (0, "Pate vaan, tarvin rahaa", pages["page3"]);
            pages["page1"].SetReply (1, "Olen Pate, entäs sinä?", pages["page2"]);

            pages["page2"].SetText ("No d44 ei TOD kuulu sulle!");
            pages["page2"].SetLast ("Okei...");

            pages["page3"].SetText ("No jos haet mulle kaupast sidukkaa saat pitää loput, tossa 4e");
            pages["page3"].SetReply (0, "Hae kaupasta 3e hintainen hyvä siideri", pages["page4"]); // Pate saa euron
            pages["page3"].SetReply (1, "Älä hae siideriä ollenkaan", pages["page5"]); // Pate saa 4 euroa
            pages["page3"].SetReply (3, "Hae kaksi halpissiideriä", pages["page6"]); // Pate saa 0,3 euroa

            pages["page4"].SetText ("Käytit neljästä eurosta kolme, sinulle jää euro!");
            pages["page4"].SetLast ("JES!");
            pages["page4"].SetReward (1.0);

            pages["page5"].SetText ("Otit pissiksen rahat, muttet ostanut tälle siideriä. Onnittelut, sait 4 euroa!");
            pages["page5"].SetLast ("HEHHEH!");
            pages["page5"].SetReward (4.0);

            pages["page6"].SetText ("No hyi.. En mä tollasii juo!\n (Saat juoda siiderit itse ja saat 30 senttiä palautusrahaa!)");
            pages["page6"].SetLast ("JES!");
            pages["page6"].SetReward (0.3);

			AdvanceDialog (pages["page1"]);
		}
	}
}
