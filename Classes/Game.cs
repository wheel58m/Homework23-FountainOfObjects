namespace Classes;

public class Game {
    public bool IsRunning { get; set; }
    public Player Player { get; init; }
    public Room? FountainRoom { get; private set; }
    public Room[,]? Rooms { get; private set; }
    public int NumberOfPits { get; init; } = 0;
    public Mob[]? Mobs { get; set; }

    public void InitializeRooms() {
        // Randomly Generate Rooms (The entrance should always be first)
        Random random = new();
        int fountainX = random.Next(1, Rooms!.GetLength(0));
        int fountainY = random.Next(1, Rooms.GetLength(1));
        int pitsInitialized = 0;

        // Generate Entrance
        Rooms[0, 0] = new Entrance();

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
    public void GenerateMobs() {
        for (int i = 0; i < Mobs!.Length; i++) {
            Random random = new();
            while(true) {
                int mobX = random.Next(0, Rooms!.GetLength(0));
                int mobY = random.Next(0, Rooms.GetLength(1));

                if (Rooms[mobX, mobY] is not Entrance && Rooms[mobX, mobY] is not Pit) {
                    Mobs[i] = new Maelstrom((mobX, mobY), this);
                    break;
                }
            }
        }
    }
    public bool IsPitAdjacent() {
        int x = Player.Position.X;
        int y = Player.Position.Y;

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
    public bool IsMaelstromAdjacent() {
        int x = Player.Position.X;
        int y = Player.Position.Y;

        foreach (Mob mob in Mobs!) {
            if (mob.Position == (x - 1, y)) {
                return true;
            } else if (mob.Position == (x + 1, y)) {
                return true;
            } else if (mob.Position == (x, y - 1)) {
                return true;
            } else if (mob.Position == (x, y + 1)) {
                return true;
            }
        }
        return false;
    }
    public void DisplayRoomDetails() {
        Console.WriteLine("---------------------------------------------------");
        Utility.WriteInfo($"You are in the room at: (Row={Player.Position.Y}, Column={Player.Position.X})");
        if (Rooms![Player.Position.X, Player.Position.Y]?.Description != null) {
            Utility.WriteNarration(Rooms![Player.Position.X, Player.Position.Y]?.Description!);
        }
        if (IsPitAdjacent()) {
            Utility.WriteNarration("You hear a breeze coming from a nearby pit.");
        }
        if (Mobs!.Length > 0) {
            if (IsMaelstromAdjacent()) {
                Utility.WriteNarration("You hear the growling and groaning of a maelstrom nearby.");
            }
        }
    }
    public void GetCommand() {
        Utility.AskForInput("What do you want to do? ", false);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        string command = Console.ReadLine()!;
        Console.ResetColor();

        ValidateCommand(command);
    }
    public void ValidateCommand(string command) {
        switch (command.ToLower()) {
            case "move north":
                if (Player.Position.Y == 0) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move north any further.");
                } else {
                    Player.Move("north");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move east":
                if (Player.Position.X == Rooms?.GetLength(0) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move east any further.");
                } else {
                    Player.Move("east");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move south":
                if (Player.Position.Y == Rooms?.GetLength(1) - 1) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move south any further.");
                } else {
                    Player.Move("south");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move west":
                if (Player.Position.X == 0) {
                    Console.WriteLine("---------------------------------------------------");
                    Utility.WriteError("You cannot move west any further.");
                } else {
                    Player.Move("west");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "activate fountain":
                if (Rooms?[Player.Position.X, Player.Position.Y] is Fountain fountain) {
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

    public void CheckForMob() {
        foreach (Mob mob in Mobs!) {
            if (mob.Position == Player.Position) {
                mob.Attack(Player);
            }
        }
    }

    public void CheckForWin() {
        if (FountainRoom!.FountainActive && Player.Position == (0, 0)) {
            Console.WriteLine("---------------------------------------------------");
            Utility.WriteNarration("The Fountain of Objects has been reactivated, and you have escaped with your life!");
            Utility.WriteHint("You win!");
            IsRunning = false;
        } else if (Rooms?[Player.Position.X, Player.Position.Y] is Pit) {
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
                Mobs = new Mob[0];
                break;
            case "medium":
                Rooms = new Room[6, 6];
                NumberOfPits = 2;
                Mobs = new Mob[1];
                break;
            case "large":
                Rooms = new Room[8, 8];
                NumberOfPits = 4;
                Mobs = new Mob[2];
                break;
        }

        IsRunning = true;
        Player = new Player((0, 0), this);
        InitializeRooms();
        GenerateMobs();

        Console.Clear();
        Utility.WriteTitle();
        Console.WriteLine();

        while(IsRunning) {
            DisplayRoomDetails();
            GetCommand();
        }
    }
}
