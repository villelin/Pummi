using System;
using System.Collections;

public class Lissu : NPC
{
	DialogPage page1;
	DialogPage page2;
	DialogPage page3;
	DialogPage page4;
	DialogPage page5;
	DialogPage page6;
	DialogPage page7;
	DialogPage page8;

	public Lissu() : base("Lissu")
	{
		page1 = new DialogPage(DialogPageImage.Lissu);
		page2 = new DialogPage(DialogPageImage.Lissu);
		page3 = new DialogPage(DialogPageImage.Lissu);
		page4 = new DialogPage(DialogPageImage.Lissu);
		page5 = new DialogPage(DialogPageImage.Lissu);
		page5 = new DialogPage(DialogPageImage.Lissu);
		page6 = new DialogPage(DialogPageImage.Lissu);
		page7 = new DialogPage(DialogPageImage.Lissu);
		page8 = new DialogPage(DialogPageImage.Lissu);

		Random rnd = new Random();

		double n;

		n = rnd.NextDouble ();

		if (n < 0.6) {
			
			page7.SetText ("Hyi... Joku spuge...");
			page7.SetReply (0, "Pate vaan, tarvin rahaa", page8);
			page7.SetReply (1, "Olen Pate, entäs sinä?", page8);

			page8.SetText ("Hyi mee pois sä haiset!");
			page8.SetLast ("Okei...");

			AdvanceDialog (page7);

		} else {

			page1.SetText ("Hyi... Joku spuge...");
			page1.SetReply (0, "Pate vaan, tarvin rahaa", page3);
			page1.SetReply (1, "Olen Pate, entäs sinä?", page2);

			page2.SetText ("No d44 ei TOD kuulu sulle!");
			page2.SetLast ("Okei...");

			page3.SetText ("No jos haet mulle kaupast sidukkaa saat pitää loput, tossa 4e");
			page3.SetReply (0, "Hae kaupasta 3e hintainen hyvä siideri", page4); // Pate saa euron
			page3.SetReply (1, "Älä hae siideriä ollenkaan", page5); // Pate saa 4 euroa
			page3.SetReply (3, "Hae kaksi halpissiideriä", page6); // Pate saa 0,3 euroa

			page4.SetText ("Käytit neljästä eurosta kolme, sinulle jää euro!");
			page4.SetLast ("JES!");
			page4.SetReward (1.0);

			page5.SetText ("Otit pissiksen rahat, muttet ostanut tälle siideriä. Onnittelut, sait 4 euroa!");
			page5.SetLast ("HEHHEH!");
			page5.SetReward (4.0);

			page6.SetText ("No hyi.. En mä tollasii juo!\n (Saat juoda siiderit itse ja saat 30 senttiä palautusrahaa!)");
			page6.SetLast ("JES!");
			page6.SetReward (0.3);

			AdvanceDialog (page1);
		}
	}
}
