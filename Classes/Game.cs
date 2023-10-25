namespace Classes;

public class Game {
    public static bool IsRunning { get; set; }
    public static (int X, int Y) PlayerPosition { get; set; } = (0, 0);
    public static Room? ActiveRoom { get; set; }
    public static Room? FountainRoom { get; set; }
    public static Room[,] Rooms { get; set; } = new Room[4, 4];

    public static void InitializeRooms() {
        for (int i = 0; i < Rooms.GetLength(0); i++) {
            for (int j = 0; j < Rooms.GetLength(1); j++) {
                switch (i, j) {
                    case (0, 0):
                        Rooms[i, j] = new Entrance();
                        ActiveRoom = Rooms[i, j];
                        break;
                    case (0, 2):
                        Rooms[i, j] = new Fountain();
                        FountainRoom = Rooms[i, j];
                        break;
                    default:
                        Rooms[i, j] = new Room();
                        break;
                }
            }
        }
    }
    public static void DisplayRoomDetails() {
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
                if (PlayerPosition.Y == 0) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move north any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X, PlayerPosition.Y - 1);
                    ActiveRoom = Rooms[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move east":
                if (PlayerPosition.X == Rooms.GetLength(0) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move east any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X + 1, PlayerPosition.Y);
                    ActiveRoom = Rooms[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move south":
                if (PlayerPosition.Y == Rooms.GetLength(1) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move south any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X, PlayerPosition.Y + 1);
                    ActiveRoom = Rooms[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move west":
                if (PlayerPosition.X == 0) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move west any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X - 1, PlayerPosition.Y);
                    ActiveRoom = Rooms[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "activate fountain":
                if (ActiveRoom is Fountain fountain) {
                        fountain.ActivateFountain();
                } else {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("There is no fountain in this room.");
                }
                break;
            case "quit":
                break;
            default:
                // Display Error & Prompt Again
                Console.WriteLine("---------------------------------------------------");
                Utility.WriteError("Invalid command. Please try again.");
                break;
        }
    }

    public static void CheckForWin() {
        if (FountainRoom!.FountainActive && PlayerPosition == (0, 0)) {
            Console.WriteLine("---------------------------------------------------");
            Utility.WriteNarration("The Fountain of Objects has been reactivated, and you have escaped with your life!");
            Utility.WriteHint("You win!");
            IsRunning = false;
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
