using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    string name;
    int level;
    enum Screen  {Name, MainMenu, Password, Win };
    Screen currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        GetName();
    }

    private void GetName()
    {
        currentScreen = Screen.Name;
        Terminal.WriteLine("Please enter your name:");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method to show main menu
    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello " + name);
        Terminal.WriteLine("Where would you like to hack into?");
        Terminal.WriteLine("Press 1 for Library");
        Terminal.WriteLine("Press 2 for Police Station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter Your Selection:");
    }

    void OnUserInput(string input)
    {
        if(input == "menu")
        {
            ShowMainMenu();
        }
        else
        {
            switch (currentScreen)
            {
                case Screen.Name:
                    name = input;
                    ShowMainMenu();
                    break;
                case Screen.MainMenu:
                    RunMainMenu(input);
                    break;
                case Screen.Password:
                    break;
                default:
                    break;
            }
        }
       
            
    }

    private void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
                level = 1;
                StartGame();
                break;
            case "2":
                level = 2;
                StartGame();
                break;
            case "3":
                level = 3;
                StartGame();
                break;
            case "007":
                Terminal.WriteLine("Please select a level Mr Bond!");
                break;
            default:
                Terminal.WriteLine("Invalid choice!");
                break;
        }
    }

    private void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine("You chose level " + level);
    }
}
