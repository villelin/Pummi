using UnityEngine;
using System.Collections;

public class Lissu : NPC
{
	DialogPage page1;
	DialogPage page2;
	DialogPage page3;
	DialogPage page4;
	DialogPage page5;
	DialogPage page6;

	public Lissu() : base("Lissu")
	{
		page1 = new DialogPage();
		page2 = new DialogPage();
		page3 = new DialogPage();
		page4 = new DialogPage();
		page5 = new DialogPage();
		page5 = new DialogPage();
		page6 = new DialogPage();

		page1.SetText("Hyi... Joku spuge...");
		page1.SetReply(0, "Pate vaan, tarvin rahaa", page3);
		page1.SetReply(1, "Olen Pate, entäs sinä?", page2);

		page2.SetText("No d44 ei TOD kuulu sulle!");
		page2.SetLast();

		page3.SetText("No jos haet mulle kaupast sidukkaa saat pitää loput, tossa 4e");
		page3.SetReply(0, "Hae kaupasta 3e hintainen hyvä siideri", page4); // Pate saa euron
		page3.SetReply(1, "Älä hae siideriä ollenkaan", page5); // Pate saa 4 euroa
		page3.SetReply(3, "Hae kaksi halpissiideriä", page6); // Pate saa 0,3 euroa

		page4.SetText("Käytit neljästä eurosta kolme, sinulle jää euro!");
		page4.SetLast();
        page4.SetReward(1.0);

		page5.SetText("Otit pissiksen rahat, muttet ostanut tälle siideriä. Onnittelut, sait 4 euroa!");
		page5.SetLast();
        page5.SetReward(4.0);

		page6.SetText("No hyi.. En mä tollasii juo!\n (Saat juoda siiderit itse ja saat 30 senttiä palautusrahaa!)");
		page6.SetLast();
        page6.SetReward(0.3);

		AdvanceDialog(page1);
	}
}
