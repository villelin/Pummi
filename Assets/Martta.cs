using System;
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

	DialogPage page8;
	DialogPage page9;
	DialogPage page10;
	DialogPage page11;

    public Martta() : base("Martta")
    {
        page1 = new DialogPage(DialogPageImage.Martta);
        page2 = new DialogPage(DialogPageImage.Martta);
        page3 = new DialogPage(DialogPageImage.Martta);
        page4 = new DialogPage(DialogPageImage.Martta);
        page5 = new DialogPage(DialogPageImage.Martta);
        page6 = new DialogPage(DialogPageImage.Martta);
		page7 = new DialogPage(DialogPageImage.Martta);

		page8 = new DialogPage(DialogPageImage.Martta);
		page9 = new DialogPage(DialogPageImage.Martta);
		page10 = new DialogPage(DialogPageImage.Martta);
		page11 = new DialogPage(DialogPageImage.Martta);

		Random rnd = new Random();

		double n;

		n = rnd.NextDouble ();

		if (n < 0.5) {

			page8.SetText ("Hmph.. Vai että kadun mies.. Mitä sinä haluat?!");
			page8.SetReply (0, "Pate vaan, tarvin rahaa", page9);
			page8.SetReply (1, "Olen Pate, entäs sinä?", page10);

			page9.SetText ("Yök, mikä tungetteleva juoppo! Häivy!");
			page9.SetLast ("Okei...");

			page10.SetText ("Olen Martta-neiti.. Mitä sinä täällä asemalla kuppaat?");
			page10.SetReply (0, "Menossa kattoo kaveria, mut pitäs metrolippu saada ostettua", page11);
			page10.SetReply (1, "Ihastelen teitä kauniita naisia", page9);

			page11.SetText ("Hmph.. No on minulla kolme euroa laukun pohjalla..\nVaikka viinaanhan sinä haisuli sen kuitenkin käytät!");
			page11.SetLast ("Jee!");
			page11.SetReward (3.0);


			AdvanceDialog (page8);

		} else {

			page1.SetText ("Hmph.. Vai että kadun mies.. Mitä sinä haluat?!");
			page1.SetReply (0, "Pate vaan, tarvin rahaa", page2);
			page1.SetReply (1, "Olen Pate, entäs sinä?", page2);

			page2.SetText ("Ja pyh! Menisit töihin pummi!");
			page2.SetReply (0, "Tarkoitus olisi, pitäisi vain ensin päästä metroon", page4);
			page2.SetReply (1, "Älä sinä akka mäkätä siinä!", page3);

			page3.SetText ("Kuinka julkeat! Mene matkoihisi, senkin haisuli!");
			page3.SetLast ("Okei...");

			page4.SetText ("Vai sillä tavalla.. Mitä se metroonpääsy auttaa?");
			page4.SetReply (0, "Auttaahan se elämässä eteenpäin...", page5);
			page4.SetReply (1, "Menen kotiin suihkuun ja siitä työhaastatteluun", page6);
			page4.SetReply (3, "Menen ostamaan viinaa", page7);

			page5.SetText ("No... Ota tästä 50 senttiä...");
			page5.SetLast ("JES!");
			page5.SetReward (0.5);

			page6.SetText ("Pyh! Ja valehteletkin vielä, senkin juoppo! Mene matkoihisi!");
			page6.SetLast ("Okei...");

			page7.SetText ("No.. Olet ainakin rehellinen.. Ota tästä 2 euroa");
			page7.SetLast ("JES!");
			page7.SetReward (2.0);

			AdvanceDialog (page1);
		}
    }
}
