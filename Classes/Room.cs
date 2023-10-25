namespace Classes;

public class Room {
    public bool FountainActive { get; set; }
    public string? Description { get; set; }

    public Room() { }
}
public class Entrance : Room {
    public Entrance() {
        Description = "You see light coming from the cavern entrance.";
    }
}
public class Fountain : Room {
    public void ActivateFountain() {
        FountainActive = true;
        Description = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
    }

    public Fountain() {
        FountainActive = false;
        Description = "You hear water dripping in this room. The Fountain of Objects is here!";
    }
}