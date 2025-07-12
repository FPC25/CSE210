# What is abstraction and why is it important?

<ul>
    <li>Thoroughly explain the meaning of Abstraction.</li>
    <li>Highlight a benefit of Abstraction.</li>
    <li>Provide an application of Abstraction.</li>
    <li>Use a code example of Abstraction from the program you wrote (copy and paste a few lines of code that demonstrate the use of the principle).</li>
    <li>Thoroughly explain these concepts (this likely cannot be done in less than 100 words);</li>
</ul>

R: Abstraction is a process to break complex ideas into a group of simpler ones so we can simplify the code into smaller, easier to program, and easy to attach one to another. The best example we have of abstraction is to create objects in classes where we define what composes one object and how it should behave. It can be formed from primitive variables, arrays and lists, or other objects that are abstractions in themselves.

Breaking complex ideas into smaller ones not only helps us programmers so the code is simpler to write, but it is directly related to how we program. First I mentioned the classes and their methods; well, the method itself, a function that is part of an object, helps us to maintain and to reuse the code as we need it. That is because there are snippets, pieces of code, that we simply must repeat over and over again in our code, and instead of implementing it multiple times in our code, we simply write it once, in a class or in a function and we can call it wherever and whenever we need it with the advantage that if we need to change it due to maintenance or improving we can only change the code once and it will change its behavior for all the callings it receives.

For me, in the Journal activity I developed a class with functions that I think I will call so many times, in so many different places that, and probably not only in this specific project, that I separated them from the rest of the code. The function <code>Utils.Decision()</code> is a great example of it. It is a function that given a list of strings with the options, displays them to the user as an ordered list and recognizes the option selected, returning it to the program, allowing the program to select the path it will take.

<code>
public static string Decision(List<string> options)
    {
        // Display numbered choices
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        int choice;
        string input;
        // Prompt until user enters a valid number
        do
        {
            Console.Write("Please select an option by number: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out choice) || choice < 1 || choice > options.Count);

        // Return zero-based index
        return options[choice - 1];
    }
</code>

This Decision function was called 10 times alone in the Preferences and in the Journal classes, making it the perfect example of abstraction in my code until now, and since it was made to be generic, allowing it to display and receive the user input no matter how many options the user gives in the list, I will be able to use it for a number of future projects ahead if needed.