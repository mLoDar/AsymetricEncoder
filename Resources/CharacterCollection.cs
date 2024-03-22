internal class CharacterCollection
{
    internal readonly static string letters_Lowercase = "abcdefghijklmnopqrstuvwxyz";
    internal readonly static string letters_Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    internal readonly static string chars_Numbers = "0123456789";
    internal readonly static string latin_Specials = @"^°!²§³$%&/{([)]=}ß?\´`+*~#'-_.:,;<>|@öÖäÄüÜµ€" + Convert.ToString('"');

    internal readonly static string allCombined = $"{letters_Lowercase}{letters_Uppercase}{chars_Numbers}{latin_Specials}";
}
