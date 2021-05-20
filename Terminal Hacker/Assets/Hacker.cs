
using UnityEngine;

public class Hacker : MonoBehaviour
{
    string[][] passwords = { new string[] { "books", "aisle", "shelf", "password", "borrow"} ,
                             new string[] { "prisoner", "hancuffs", "holster", "uniform", "arrest"  } ,
                             new string[] { "starfield", "telescope", "environment", "exploration", "astronauts" }};
    string password;
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
        Terminal.ClearScreen();
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
        else if(input == "name")
        {
            GetName();
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
                    CheckPassword(input);
                    break;
                case Screen.Win:
                    GetName();
                    break;
                default:
                    break;
            }
        } 
    }

    private void CheckPassword(string input)
    {
        if(input == password)
        {
            ShowWinScreen();
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Access Denied! - Please try again:");
            StartGame();
        }
    }

    private void ShowWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine("**** " + GetLevelName() + " ****" + 
                           "\nAccess Granted!\n" +
                           "Well done " + name);
        Terminal.WriteLine("\n\nPress enter to restart");
    }

    private void RunMainMenu(string input)
    {
        int index;
        switch (input)
        {
            case "1":
                level = 0;
                index = Random.Range(0, passwords[level].Length);
                password = passwords[level][index];
                StartGame();
                break;
            case "2":
                level = 1;
                index = Random.Range(0, passwords[level].Length);
                password = passwords[level][index];
                StartGame();
                break;
            case "3":
                level = 2;
                index = Random.Range(0, passwords[level].Length);
                password = passwords[level][index];
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
        Terminal.WriteLine("\n**** " + GetLevelName() + " ****");
        Terminal.WriteLine("Please enter the password");
        Terminal.WriteLine("hint: " + password.Anagram());
    }


    private string GetLevelName()
    {
        switch (level)
        {
            case 0:
                return "Library";
            case 1:
                return "Police Station";
            case 2:
                return "NASA";
            default:
                return "";
        }
    }
}
