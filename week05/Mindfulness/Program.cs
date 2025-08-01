using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Hello World! This is the Mindfulness Project.\n");

        //creating the constants that are the options to the menu
        const string
        BREATH = "Start breathing activity",
        REFLECT = "Start reflecting activity",
        LIST = "Start listing activity",
        QUIT = "Quit";

        //Creating the List to call the menu function
        List<string> options = new List<string>()
        {
            BREATH,
            REFLECT,
            LIST,
            QUIT
        };

        //Menu Calling and run logic depending on the selected option that keeps running while not selecting the QUIT option;
        string selectedOption;

        do
        {
            Console.WriteLine("Menu Options:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case BREATH:
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    break;

                case REFLECT:
                    ReflectingActivity reflecting = new ReflectingActivity();
                    reflecting.Run();
                    break;

                case LIST:
                    //ListingActivity listing = new ListingActivity();
                    //listing.Run();
                    Console.WriteLine("Work in Progress");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);
    }
}