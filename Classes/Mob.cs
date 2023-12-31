namespace Classes;

public class Mob {
    public (int X, int Y) Position { get; set; }
    public Game GameReference { get; init; }
    public bool IsAlive { get; set; } = true;
    public int Health { get; set; } = 3;

    public Mob((int x, int y) position, Game game) {
        Position = position;
        GameReference = game;
    }
    public void Move(string direction) {
        switch (direction) {
            case "north":
                Move(0, -1);
                break;
            case "south":
                Move(0, 1);
                break;
            case "east":
                Move(1, 0);
                break;
            case "west":
                Move(-1, 0);
                break;
        }
    }
    public void Move(int x, int y) {
        int newX = Position.X + x;
        int newY = Position.Y + y;

        // Check if new position is out of bounds. If so wrap around.
        if (newX < 0) {
            newX = GameReference.Rooms!.GetLength(0) - 1;
        } else if (newX >= GameReference.Rooms!.GetLength(0)) {
            newX = 0;
        } else if (newY < 0) {
            newY = GameReference.Rooms.GetLength(1) - 1;
        } else if (newY >= GameReference.Rooms.GetLength(1)) {
            newY = 0;
        }

        Position = (newX, newY);
    }
    public virtual void Attack(Mob mob) {
        if (mob != null && mob.IsAlive) {
            mob.IsAlive = false;
        }
    }
}
public class Player : Mob {
    public int NumArrows { get; set; } = 5;
    public Player((int x, int y) position, Game game) : base(position, game) { }
}
public class Maelstrom : Mob {
    public Maelstrom((int x, int y) position, Game game) : base(position, game) { }
    public override void Attack(Mob player) {
        player.Move(2, -1); // Move player 1 space north and two spaces east
        Move(-2, 1); // Move maelstrom 1 space south and two spaces west
        player.Health--; // Player loses 1 health
        Console.WriteLine("---------------------------------------------------------------------------");
            Utility.WriteError("You were attacked by a maelstrom! Your position has moved.");
    }
}
public class Amarok : Mob {
    public Amarok((int x, int y) position, Game game) : base(position, game) { }
    public override void Attack(Mob player) {
        player.Health--; // Player loses 1 health
        Console.WriteLine("---------------------------------------------------------------------------");
        Utility.WriteError("You were attacked by an amarok!");
    }
}