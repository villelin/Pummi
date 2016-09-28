using System.Collections;

public class Andrei : NPC
{
    DialogPage page1;
    DialogPage page2;
    DialogPage page3;
    DialogPage page4;
    DialogPage page5;
    DialogPage page6;

    public Andrei()
    {
        page1 = new DialogPage();
        page2 = new DialogPage();
        page3 = new DialogPage();
        page4 = new DialogPage();
        page5 = new DialogPage();
		page5 = new DialogPage();
		page6 = new DialogPage();

        page1.SetText("Jahashh... Mikässhh miässh she shinä olethh?");
        page1.SetReply(0, "Pate vaan, tarvin rahaa", page2);
        page1.SetReply(1, "Olen Pate, entäs sinä?", page3);

        page2.SetText("PAINU HIITEEN, PUMMI!");
        page2.SetLast();

        page3.SetText("Voi että, et uskokaan, kun minulla on ollut niin rankkaa… Bisness pyöri ja elämä sujui.. Mutta nyt on kaikki kuviot kuule kusseet ja pahasti…");
        page3.SetReply(0, "Lähde pois", page4);
        page3.SetReply(1, "Jatka kuuntelua", page5);
        page3.SetReply(3, "Sano jotain myötätuntoista", page6);

        page4.SetText("Et saanut rahaa :(");
        page4.SetLast();

// Andrei antaa 20e:
        page5.SetText("...ensin puukottaa liikekumppanit selkään, vaikka monenmoista jeesiä hänelle tarjosin ja koulutin kuule sen pikkuvesselistä alalle, oli mulle kuin oma poika.. Ja vaimokin saamari läx sen matkaan ja koirakin perkele karkasi… Kuulee… Sinä.. Pate? Sinä oot kuule hyvä jätkä, Pate! Ota kuule tästä, saat kakskymppiä!");
        page5.SetLast();

// Andrei ryöstää:
        page6.SetText("JUMANSVIIDU MIKÄ MIELISTELIJÄ! RAHAT TÄNNE");
        page6.SetLast();

        AdvanceDialog(page1);
    }
}
