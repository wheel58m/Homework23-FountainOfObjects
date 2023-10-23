namespace Classes;

public class Game {
    public Room[,] Rooms { get; set; } = new Room[4, 4];

    public Game() {
        for (int i = 0; i < Rooms.GetLength(0); i++) {
            for (int j = 0; j < Rooms.GetLength(1); j++) {
                switch (i, j) {
                    case (0, 0):
                        Rooms[i, j] = new Entrance();
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
}
