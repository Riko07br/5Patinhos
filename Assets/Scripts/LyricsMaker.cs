
public class LyricsMaker{

    public static string MakeLine(int ducksAmount, int currentLine) {
        string line1;
        string lastLine;

        if (ducksAmount > 1) {
            line1 = ducksAmount + " patinhos foram";
            lastLine = "só " + ducksAmount + " patinhos voltaram";
        }
        else {
            line1 = "1 patinho foi";
            lastLine = ducksAmount == 1 ? "só 1 patinho voltou" : "nenhum patinho voltou";
        }

        return currentLine switch {
            0 => line1 + " passear",
            1 => "Além das montanhas\nPara brincar",
            2 => "A mamãe gritou: Quá, quá, quá, quá",
            3 => "Mas " + lastLine + " de lá",
            _ => "Linha não encontrada",
        };
    }
    public static string MakeLastVerse(int ducksAmount, int currentLine) {
        string last_line = ducksAmount > 1 ? "os " + ducksAmount + " patinhos voltaram" : "o patinho voltou";
        return currentLine switch {
            0 => "A mamãe patinha foi procurar",
            1 => "Além das montanhas\nNa beira do mar",
            2 => "A mamãe gritou: Quá, quá, quá, quá",
            3 => "E " + last_line + " de lá",
            _ => "Linha não encontrada",
        };
    }
}
