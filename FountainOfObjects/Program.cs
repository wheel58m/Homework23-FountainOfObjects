﻿// HOMEWORK 23 - THE FOUNTAIN OF OBJECTS ///////////////////////////////////////
// Author: Austin Wheeler
// Class: Object-Oriented Programming (CS-1410-N01)
// Date: October 22, 2023
////////////////////////////////////////////////////////////////////////////////
using Classes;

namespace FountainOfObjects;
class Program {
    public static bool IsRunning { get; set; } = true;
    static void Main(string[] args) {
        while (IsRunning) {
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

            Utility.WriteHint("Type 'help' for a list of commands.");
            Console.WriteLine();

            Utility.AskForInput("Do you want to play a 'small', 'medium', or 'large' game? ", false);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string? input = Console.ReadLine()?.ToLower();
            Console.ResetColor();

            string? size;
            switch (input) {
                case "small":
                    Utility.WriteHint("You have chosen a small game.");
                    Console.WriteLine();
                    size = "small";
                    break;
                case "medium":
                    Utility.WriteHint("You have chosen a medium game.");
                    Console.WriteLine();
                    size = "medium";
                    break;
                case "large":
                    Utility.WriteHint("You have chosen a large game.");
                    Console.WriteLine();
                    size = "large";
                    break;
                default:
                    Utility.WriteError("Invalid input. Defaulting to a small game.");
                    Console.WriteLine();
                    size = "small";
                    break;
            }

            Utility.AskForInput("Press any key to enter the cavern...", false);
            Console.ReadKey(true);

            // Create Game --------------------------------------------------------/
            Game game = new(size);

            // Play Again? --------------------------------------------------------/
            if (game.IsRunning) {
                Console.WriteLine("---------------------------------------------------------------------------");
                Utility.AskForInput("Would you like to play again? (y/n) ", false);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                input = Console.ReadLine()?.ToLower();
                Console.ResetColor();

                switch (input) {
                    case "y":
                    case "yes":
                        Utility.WriteHint("You have chosen to play again.");
                        Console.WriteLine();
                        game.IsRunning = false;
                        break;
                    default:
                        Utility.WriteHint("Thanks for playing! Goodbye!");
                        Console.WriteLine();
                        game.IsRunning = false;
                        IsRunning = false;
                        break;
                }                
            }
        }
    }
}