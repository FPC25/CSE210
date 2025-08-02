# What is inheritance and why is it important?

<ul>
    <li>Explain the meaning of Inheritance.</li>
    <li>Highlight a benefit of Inheritance.</li>
    <li>Provide an application of Inheritance.</li>
    <li>Use a code example of Inheritance from the program you wrote (copy and paste a few lines of code that demonstrate the use of the principle).</li>
    <li>Thoroughly explain these concepts (this likely cannot be done in less than 100 words);</li>
</ul>

## Answer

### Definition and Benefits

Inheritance is a fundamental concept in Object-Oriented programming (OOP) where a class, called a derived, sub or child class, automatically receives (a.k.a. inherit) the attributes and behaviors (fields and methods) of another class. called a base, super or parent class. This characteristic allows that we reuse and avoid duplicating code throughout the code, making the program easier to maintain. All those benefits allows the developer to place the shared logic and code in a base class and the specifics to the derived class. If needed to update or fix a shared behavior, instead of having to do the process in multiple places, with Inheritance we can do this process in one place and having it reflected in all derived classes.

To better visualize imagine a game in which we have multiple kind of animals, all of them have their specificities, but at the same time they also share some characteristics and behaviors. To program this we can simplify our code by creating a class <code>Animal()</code> that have all the shared attributes and methods such as <code>_numberOfLegs</code>, <code>Sleep()</code>, <code>Eat()</code> and other classes, such as <code>Wolf()</code> and <code>Bird()</code> will share it and on top of it, they will aso have the specific attributes and methods, such as <code>Run()</code> and <code>Fly()</code>, respectively.


### Code example from the Mindfulness project


```csharp

//base class
class Activity
{
    private string _activityName, _message;
    private int _activityDurationInSeconds;

    public Activity(string name, string message)
    {
        _activityName = name;
        _message = message;
    }

    //A method to display a start message, based on the name of the activity and request the time it should take
    public int DisplayStartMessage()
    {
        //Display basic messages of welcoming and description of the activity
        Console.WriteLine($"Welcome to the {_activityName} Activity.\n");
        Console.WriteLine(_message + "\n");
        //Setting some variables of time and input
        int time;
        string input;
        //Requesting the user to enter the how long each activity will take, but added some safe guards in order to guarantee that the value entered can be converted to int, otherwise request keep requesting
        do
        {
            Console.Write("For how long, in seconds, would you like to do this session? ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out time));
        return time;
    }
    //... rest of the code
}

//derived classes
class BreathingActivity : Activity
{
    //constants to run this program 
    const string NAME = "Breathing", MESSAGE = "This activity will help you relax by walking your through breathing routine. Clear your mind and focus on your breathing.";
    const int STEP_TIME = 4; //s
    const int NUM_STEPS = 4; // per cycle
    //An empty constructor

    public BreathingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        Console.Clear();
        //initiate the program running it, displaying the messages and getting for how long it should run
        int time = DisplayStartMessage();
        //... rest of the method
    }
    //... rest of the code
}

class ListingActivity : Activity
{
    const string NAME = "Listing", MESSAGE = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    private List<string> _prompts = new List<string>() {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?"
        //rest of the prompts
    };
    private int _count;
    private Random _random = new Random();

    public ListingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        Console.Clear();
        int time = DisplayStartMessage();
        //... rest of the method
    }
    //... rest of the code
}
```

Here we can see that the base class <code>Activity()</code> we see that all activities have at least a name and a message that describes it that are required to a method called <code>DisplayStartMessage()</code> that with the simple declarations <code>class ListingActivity : Activity</code> and the constructor <code>public ListingActivity() : base(NAME, MESSAGE){}</code>, for example inherits from <code>Activity()</code> not only the behaviors, although can't access them due them being private, but also its methods, as shown in the <code>Run()</code> method (for both the breathing and Listing activities) access the <code>DisplayStartMessage()</code> method from the base class, removing the necessity of declaring the same code in each specific activity class.
