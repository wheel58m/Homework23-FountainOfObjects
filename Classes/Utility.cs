namespace Classes;

public static class Utility {
    public static string[] TitleArt { get; } = {
        @"  _____ _  _ ___   ___ ___  _   _ _  _ _____ _   ___ _  _  ",
        @" |_   _| || | __| | __/ _ \| | | | \| |_   _/_\ |_ _| \| | ",
        @"   | | | __ | _|  | _| (_) | |_| | .` | | |/ _ \ | || .` | ",
        @"   |_| |_||_|___| |_| \___/ \___/|_|\_| |_/_/ \_\___|_|\_| ",
        @"                                                           ",
        @"         ___  ___    ___  ___    _ ___ ___ _____ ___       ",
        @"        / _ \| __|  / _ \| _ )_ | | __/ __|_   _/ __|      ",
        @"       | (_) | _|  | (_) | _ \ || | _| (__  | | \__ \      ",
        @"        \___/|_|    \___/|___/\__/|___\___| |_| |___/      "
    };

    public static void WriteTitle() {
        foreach (string line in TitleArt) {
            Console.WriteLine(line);
        }
    }
    public static void WriteNarration(string narration) {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(narration);
        Console.ResetColor();
    }
    public static void WriteInfo(string instruction) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(instruction);
        Console.ResetColor();
    }
    public static void WriteHint(string hint) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(hint);
        Console.ResetColor();
    }
    public static void WriteError(string error) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }
    public static void AskForInput(string instruction, bool newLine = true) {
        Console.ForegroundColor = ConsoleColor.White;
        if (newLine) {
            Console.WriteLine(instruction);
        } else {
            Console.Write(instruction);
        }
        Console.ResetColor();
    }
}
                                                                                                         