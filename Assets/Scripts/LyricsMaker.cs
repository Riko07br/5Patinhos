
public class LyricsMaker{

    public static string MakeLine(int ducksAmount, int currentLine) {
        string line1;
        string lastLine;

        if (ducksAmount > 1) {
            line1 = ducksAmount + " patinhos foram";
            lastLine = "s� " + ducksAmount + " patinhos voltaram";
        }
        else {
            line1 = "1 patinho foi";
            lastLine = ducksAmount == 1 ? "s� 1 patinho voltou" : "nenhum patinho voltou";
        }

        return currentLine switch {
            0 => line1 + " passear",
            1 => "Al�m das montanhas\nPara brincar",
            2 => "A mam�e gritou: Qu�, qu�, qu�, qu�",
            3 => "Mas " + lastLine + " de l�",
            _ => "Linha n�o encontrada",
        };
    }
    public static string MakeLastVerse(int ducksAmount, int currentLine) {
        string last_line = ducksAmount > 1 ? "os " + ducksAmount + " patinhos voltaram" : "o patinho voltou";
        return currentLine switch {
            0 => "A mam�e patinha foi procurar",
            1 => "Al�m das montanhas\nNa beira do mar",
            2 => "A mam�e gritou: Qu�, qu�, qu�, qu�",
            3 => "E " + last_line + " de l�",
            _ => "Linha n�o encontrada",
        };
    }
}
