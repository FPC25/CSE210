using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Mindfulness Project.");

        const string
        BREATH = "Start breathing activity",
        REFLECT = "Start reflecting activity",
        LIST = "Start listing activity",
        QUIT = "Quit";


        List<string> options = new List<string>()
        {
            BREATH,
            REFLECT,
            LIST,
            QUIT
        };

        Console.WriteLine("Menu Options:");
        string selectedOption;

        do
        {
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