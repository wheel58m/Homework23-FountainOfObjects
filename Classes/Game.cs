namespace Classes;

public class Game {
    public static bool IsRunning { get; set; }
    public static (int X, int Y) PlayerPosition { get; set; } = (0, 0);
    public static Room? ActiveRoom { get; set; }
    public static Room? FountainRoom { get; set; }
    public static Room[,]? Rooms { get; set; }
    public static int NumberOfPits { get; set; } = 0;

    public static void InitializeRooms() {
        // Randomly Generate Rooms (The entrance should always be first)
        Random random = new();
        int fountainX = random.Next(1, Rooms!.GetLength(0));
        int fountainY = random.Next(1, Rooms.GetLength(1));
        int pitsInitialized = 0;

        // Generate Entrance
        Rooms[0, 0] = new Entrance();
        ActiveRoom = Rooms[0, 0];

        // Generate Fountain
        Rooms[fountainX, fountainY] = new Fountain();
        FountainRoom = Rooms[fountainX, fountainY];

        // Generate Pits
        while (pitsInitialized < NumberOfPits) {
            int pitX = random.Next(0, Rooms.GetLength(0));
            int pitY = random.Next(0, Rooms.GetLength(1));

            if (Rooms[pitX, pitY] == null) {
                Rooms[pitX, pitY] = new Pit();
                pitsInitialized++;
            }
        }

        // Generate Remaining Rooms
        for (int i = 0; i < Rooms.GetLength(0); i++) {
            for (int j = 0; j < Rooms.GetLength(1); j++) {
                if (Rooms[i, j] == null) {
                    Rooms[i, j] = new Room();
                }
            }
        }

    }
    public static bool IsPitAdjacent() {
        int x = PlayerPosition.X;
        int y = PlayerPosition.Y;

        if (x > 0 && Rooms?[x - 1, y] is Pit) {
            return true;
        } else if (x < Rooms?.GetLength(0) - 1 && Rooms?[x + 1, y] is Pit) {
            return true;
        } else if (y > 0 && Rooms?[x, y - 1] is Pit) {
            return true;
        } else if (y < Rooms?.GetLength(1) - 1 && Rooms?[x, y + 1] is Pit) {
            return true;
        } else {
            return false;
        }
    }
    public static void DisplayRoomDetails() {
        Console.WriteLine("---------------------------------------------------");
        Utility.WriteInfo($"You are in the room at: (Row={PlayerPosition.Y}, Column={PlayerPosition.X})");
        if (ActiveRoom?.Description != null) {
            Utility.WriteNarration(ActiveRoom.Description);
        }
        if (IsPitAdjacent()) {
            Utility.WriteNarration("You hear a breeze coming from a nearby pit.");
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
                    ActiveRoom = Rooms?[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move east":
                if (PlayerPosition.X == Rooms?.GetLength(0) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move east any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X + 1, PlayerPosition.Y);
                    ActiveRoom = Rooms?[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move south":
                if (PlayerPosition.Y == Rooms?.GetLength(1) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move south any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X, PlayerPosition.Y + 1);
                    ActiveRoom = Rooms?[PlayerPosition.X, PlayerPosition.Y];
                    CheckForWin();
                }
                break;
            case "move west":
                if (PlayerPosition.X == 0) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move west any further.");
                } else {
                    PlayerPosition = (PlayerPosition.X - 1, PlayerPosition.Y);
                    ActiveRoom = Rooms?[PlayerPosition.X, PlayerPosition.Y];
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
        } else if (ActiveRoom is Pit) {
            Console.WriteLine("---------------------------------------------------");
            Utility.WriteNarration("You have fallen into a bottomless pit!");
            Utility.WriteError("You lose!");
            IsRunning = false;
        }
    }

    public Game(string size) {
        switch (size) {
            case "small":
                Rooms = new Room[4, 4];
                NumberOfPits = 1;
                break;
            case "medium":
                Rooms = new Room[6, 6];
                NumberOfPits = 2;
                break;
            case "large":
                Rooms = new Room[8, 8];
                NumberOfPits = 4;
                break;
        }

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
