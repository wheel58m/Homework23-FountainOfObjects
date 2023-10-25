// HOMEWORK 23 - THE FOUNTAIN OF OBJECTS ///////////////////////////////////////
// Author: Austin Wheeler
// Class: Object-Oriented Programming (CS-1410-N01)
// Date: October 22, 2023
////////////////////////////////////////////////////////////////////////////////
using Classes;

namespace FountainOfObjects;
class Program {
    static void Main(string[] args) {
        // Display Title Page -------------------------------------------------/
        Console.Clear();
        Utility.WriteTitle();
        Console.WriteLine();

        Utility.WriteNarration("You have made your way to the Cavern of Objects, high atop jagged mountains. Within these caverns lies the Fountain of Objects, the one-time source of the River of Objects that gave life to this entire island. By returning the Heart of Object-Oriented Programming—the gem you received from Simula after arriving on this island—to the Fountain of Objects, you can repair and restore the fountain to its former glory.");
        Console.WriteLine();

        Utility.WriteNarration("The cavern is a grid of rooms, and no natural or human-made light works within due to unnatural darkness. You can see nothing, but you can hear and smell your way through the caverns to find the Fountain of Objects, restore it, and escape to the exit.");
        Console.WriteLine();

        Utility.WriteNarration("The cavern is full of dangers. Bottomless pits and monsters lurk in the caverns, placed here by the Uncoded One to prevent you from restoring the Fountain of Objects and the land to its former glory.");
        Console.WriteLine();

        Utility.WriteNarration("By returning the Heart of Object-Oriented Programming to the Fountain of Objects, you can save the Island of Object-Oriented Programming!");
        Console.WriteLine();

        Utility.AskForInput("Press any key to continue...", false);
        Console.ReadKey(true);

        // Display Instructions -----------------------------------------------/
        Console.Clear();
        Utility.WriteTitle();
        Console.WriteLine();

        Utility.WriteInfo("Once you have entered the cavern, you will be able to move around the cavern by typing the direction you want to move. You can move north, south, east, or west. You can type \"quit\" to quit the game at any time.");
        Console.WriteLine();

        // Utility.WriteHint("A list of available commands will always be displayed at the bottom of the screen.");
        // Console.WriteLine();

        Utility.AskForInput("Press any key to enter the cavern...", false);
        Console.ReadKey(true);

        // Create Game --------------------------------------------------------/
        Game game = new();
    }
}