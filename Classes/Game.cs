namespace Classes;

public class Game {
    public bool IsRunning { get; set; }
    public (int X, int Y) PlayerPosition { get; set; } = (0, 0);
    public Room? ActiveRoom { get; set; }
    public Room[,] Rooms { get; init; } = new Room[4, 4];

    public void InitializeRooms() {
        for (int i = 0; i < Rooms.GetLength(0); i++) {
            for (int j = 0; j < Rooms.GetLength(1); j++) {
                switch (i, j) {
                    case (0, 0):
                        Rooms[i, j] = new Entrance();
                        ActiveRoom = Rooms[i, j];
                        break;
                    case (0, 2):
                        Rooms[i, j] = new Fountain();
                        break;
                    default:
                        Rooms[i, j] = new Room();
                        break;
                }
            }
        }
    }
    public void DisplayRoomDetails() {
        Console.WriteLine("---------------------------------------------------");
        Utility.WriteInfo($"You are in the room at: (Row={PlayerPosition.X}, Column={PlayerPosition.Y})");
        if (ActiveRoom?.Description != null) {
            Utility.WriteNarration(ActiveRoom.Description);
        }

    }
    public static void GetCommand() {
        Utility.AskForInput("What do you want to do? ", false);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        string command = Console.ReadLine()!;
        Console.ResetColor();

        ValidateCommand(command);
    }
    public static void ValidateCommand(string command) {
        switch (command.ToLower()) {
            case "move north":
                break;
            case "move east":
                break;
            case "move south":
                break;
            case "move west":
                break;
            case "quit":
                break;
            default:
                // Clear Menu
                (int x, int y) = Console.GetCursorPosition();
                Console.Write(new string(' ', Console.WindowWidth * 3));
                Console.SetCursorPosition(x, y);
                // Display Error & Prompt Again
                Console.WriteLine("---------------------------------------------------");
                Utility.WriteError("Invalid command. Please try again.");
                break;
        }
    }

    public Game() {
        IsRunning = true;
        InitializeRooms();

        Console.Clear();
        Utility.WriteTitle();
        Console.WriteLine();

        while(IsRunning) {
            DisplayRoomDetails();
            GetCommand();
        }
    }
}
