namespace Classes;

public class Game {
    public bool IsRunning { get; set; }
    public Player Player { get; init; }
    public Room? FountainRoom { get; private set; }
    public Room[,]? Rooms { get; private set; }
    public int NumberOfPits { get; init; } = 0;
    public int NumberOfMaelsroms { get; init; } = 0;
    public int NumberOfAmaroks { get; init; } = 0;
    public Mob[]? Mobs { get; set; }
    public bool SenseDiagonal { get; } = true;

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
        Random random = new();
        int mobsInitialized = 0;

        // Generate Maelstroms
        while (mobsInitialized < NumberOfMaelsroms) {
            int maelstromX = random.Next(0, Rooms!.GetLength(0));
            int maelstromY = random.Next(0, Rooms.GetLength(1));

            if (Rooms[maelstromX, maelstromY] is not Pit && Rooms[maelstromX, maelstromY] is not Fountain) {
                foreach (Mob mob in Mobs!) {
                    if (mob != null && mob.Position == (maelstromX, maelstromY)) {
                        continue;
                    }
                }
                Mobs![mobsInitialized] = new Maelstrom((maelstromX, maelstromY), this);
                mobsInitialized++;
            }
        }

        // Generate Amaroks
        while (mobsInitialized < NumberOfMaelsroms + NumberOfAmaroks) {
            int amarokX = random.Next(0, Rooms!.GetLength(0));
            int amarokY = random.Next(0, Rooms.GetLength(1));

            if (Rooms[amarokX, amarokY] is not Pit && Rooms[amarokX, amarokY] is not Fountain && Rooms[amarokX, amarokY] is not Entrance) {
                foreach (Mob mob in Mobs!) {
                    if (mob != null && mob.Position == (amarokX, amarokY)) {
                        continue;
                    }
                }
                Mobs![mobsInitialized] = new Amarok((amarokX, amarokY), this);
                mobsInitialized++;
            }
        }
    }
    public bool IsPitAdjacent() {
        int x = Player.Position.X;
        int y = Player.Position.Y;

        if (x > 0 && Rooms?[x - 1, y] is Pit) { // Check West
            return true;
        } else if (x < Rooms?.GetLength(0) - 1 && Rooms?[x + 1, y] is Pit) { // Check East
            return true;
        } else if (y > 0 && Rooms?[x, y - 1] is Pit) { // Check North
            return true;
        } else if (y < Rooms?.GetLength(1) - 1 && Rooms?[x, y + 1] is Pit) { // Check South
            return true;
        } else if (SenseDiagonal) {
            if (x < Rooms?.GetLength(0) - 1 && y > 0 && Rooms?[x + 1, y - 1] is Pit) { // Check Northeast
                return true;
            } else if (x < Rooms?.GetLength(0) - 1 && y < Rooms?.GetLength(1) - 1 && Rooms?[x + 1, y + 1] is Pit) { // Check Southeast
                return true;
            } else if (x > 0 && y < Rooms?.GetLength(1) - 1 && Rooms?[x - 1, y + 1] is Pit) { // Check Southwest
                return true;
            } else if (x > 0 && y > 0 && Rooms?[x - 1, y - 1] is Pit) { // Check Northwest
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
    public bool IsMaelstromAdjacent() {
        int x = Player.Position.X;
        int y = Player.Position.Y;

        foreach (Mob mob in Mobs!) {
            if (mob is Maelstrom && mob.IsAlive) {
                if (mob.Position == (x - 1, y)) { // Check West
                    return true;
                } else if (mob.Position == (x + 1, y)) { // Check East
                    return true;
                } else if (mob.Position == (x, y - 1)) { // Check North
                    return true;
                } else if (mob.Position == (x, y + 1)) { // Check South
                    return true;
                } else if (SenseDiagonal) {
                    if (mob.Position == (x + 1, y - 1)) { // Check Northeast
                        return true;
                    } else if (mob.Position == (x + 1, y + 1)) { // Check Southeast
                        return true;
                    } else if (mob.Position == (x - 1, y + 1)) { // Check Southwest 
                        return true;
                    } else if (mob.Position == (x - 1, y - 1)) { // Check Northwest
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool IsAmarokAdjacent() {
        int x = Player.Position.X;
        int y = Player.Position.Y;

        foreach (Mob mob in Mobs!) {
            if (mob is Amarok && mob.IsAlive) {
                if (mob.Position == (x - 1, y)) { // Check West
                    return true;
                } else if (mob.Position == (x + 1, y)) { // Check East
                    return true;
                } else if (mob.Position == (x, y - 1)) { // Check North
                    return true;
                } else if (mob.Position == (x, y + 1)) { // Check South
                    return true;
                } else if (SenseDiagonal) {
                    if (mob.Position == (x + 1, y - 1)) { // Check Northeast
                        return true;
                    } else if (mob.Position == (x + 1, y + 1)) { // Check Southeast
                        return true;
                    } else if (mob.Position == (x - 1, y + 1)) { // Check Southwest 
                        return true;
                    } else if (mob.Position == (x - 1, y - 1)) { // Check Northwest
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void DisplayRoomDetails() {
        Console.WriteLine("---------------------------------------------------------------------------");
        Utility.WriteInfo($"You are in the room at: (Row={Player.Position.Y}, Column={Player.Position.X}). You have {Player.NumArrows} arrows.");
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
            if (IsAmarokAdjacent()) {
                Utility.WriteNarration("You can smell the rotten stench of an amarok in a nearby room.");
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
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You cannot move north any further.");
                } else {
                    Player.Move("north");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move east":
                if (Player.Position.X == Rooms?.GetLength(0) - 1) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You cannot move east any further.");
                } else {
                    Player.Move("east");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move south":
                if (Player.Position.Y == Rooms?.GetLength(1) - 1) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You cannot move south any further.");
                } else {
                    Player.Move("south");
                    CheckForMob();
                    CheckForWin();
                }
                break;
            case "move west":
                if (Player.Position.X == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
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
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("There is no fountain in this room.");
                }
                break;
            case "shoot north":
                if (Player.Position.Y == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("Your arrow hits the wall and falls to the ground.");
                    Player.NumArrows--; // Still use an arrow
                } else if (Player.NumArrows == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You don't have any arrows left.");
                } else {
                    Player.NumArrows--;
                    Player.Attack(TargetMob("north"));
                }
                break;
            case "shoot east":
                if (Player.Position.X == Rooms?.GetLength(0)) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("Your arrow hits the wall and falls to the ground.");
                    Player.NumArrows--; // Still use an arrow
                } else if (Player.NumArrows == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You don't have any arrows left.");
                } else {
                    Player.NumArrows--;
                    Player.Attack(TargetMob("east"));
                }
                break;
            case "shoot south":
                if (Player.Position.Y == Rooms?.GetLength(1)) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("Your arrow hits the wall and falls to the ground.");
                    Player.NumArrows--; // Still use an arrow
                } else if (Player.NumArrows == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You don't have any arrows left.");
                } else {
                    Player.NumArrows--;
                    Player.Attack(TargetMob("south"));
                }
                break;
            case "shoot west":
                if (Rooms?[Player.Position.X, Player.Position.Y] is Entrance) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("Your arrow exits the cavern. You hear a cat shriek.");
                    Player.NumArrows--; // Still use an arrow
                } else if (Player.Position.X == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("Your arrow hits the wall and falls to the ground.");
                    Player.NumArrows--; // Still use an arrow
                } else if (Player.NumArrows == 0) {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Utility.WriteError("You don't have any arrows left.");
                } else {
                    Player.NumArrows--;
                    Player.Attack(TargetMob("west"));
                }
                break;
            case "quit":
                break;
            default:
                // Display Error & Prompt Again
                Console.WriteLine("---------------------------------------------------------------------------");
                Utility.WriteError("Invalid command. Please try again.");
                break;
        }
    }

    public void CheckForMob() {
        foreach (Mob mob in Mobs!) {
            if (mob.Position == Player.Position && mob.IsAlive) {
                mob.Attack(Player);
            }
        }
    }
    public Mob TargetMob(string direction) {
        int x = Player.Position.X;
        int y = Player.Position.Y;

        switch (direction) {
            case "north":
                y--;
                break;
            case "south":
                y++;
                break;
            case "east":
                x++;
                break;
            case "west":
                x--;
                break;
        }

        foreach (Mob mob in Mobs!) {
            if (mob.Position == (x, y)) {
                return mob;
            }
        }
        return null!;
    }

    public void CheckForWin() {
        if (FountainRoom!.FountainActive && Player.Position == (0, 0)) {
            Console.WriteLine("---------------------------------------------------------------------------");
            Utility.WriteNarration("The Fountain of Objects has been reactivated, and you have escaped with your life!");
            Utility.WriteHint("You win!");
            IsRunning = false;
        } else if (Rooms?[Player.Position.X, Player.Position.Y] is Pit) {
            Console.WriteLine("---------------------------------------------------------------------------");
            Utility.WriteNarration("You have fallen into a bottomless pit!");
            Utility.WriteError("You lose!");
            IsRunning = false;
        } else {
            foreach (Mob mob in Mobs!) {
                if (mob is Amarok && mob.Position == Player.Position && mob.IsAlive) {
                    Utility.WriteError("You lose!");
                    IsRunning = false;
                }
            }
        }
    }

    public Game(string size) {
        switch (size) {
            case "small":
                Rooms = new Room[4, 4];
                NumberOfPits = 1;
                NumberOfMaelsroms = 0;
                NumberOfAmaroks = 1;
                Mobs = new Mob[NumberOfMaelsroms + NumberOfAmaroks];
                break;
            case "medium":
                Rooms = new Room[6, 6];
                NumberOfPits = 2;
                NumberOfMaelsroms = 1;
                NumberOfAmaroks = 2;
                Mobs = new Mob[NumberOfMaelsroms + NumberOfAmaroks];
                break;
            case "large":
                Rooms = new Room[8, 8];
                NumberOfPits = 4;
                NumberOfMaelsroms = 2;
                NumberOfAmaroks = 3;
                Mobs = new Mob[NumberOfMaelsroms + NumberOfAmaroks];
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
