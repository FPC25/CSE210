# What is encapsulation and why is it important?

<ul>
    <li>Thoroughly explain the meaning of Encapsulation.</li>
    <li>Highlight a benefit of Encapsulation.</li>
    <li>Provide an application of Encapsulation.</li>
    <li>Use a code example of Encapsulation from the program you wrote (copy and paste a few lines of code that demonstrate the use of the principle).</li>
    <li>Thoroughly explain these concepts (this likely cannot be done in less than 100 words);</li>
</ul>

## Answer

### Definition and Benefits

Encapsulation is the object-oriented programming principle that bundles the attributes and methods that operate on those attributes within a single class, while also restricting direct access to the internal components of the class. This principle also establishes a protective barrier—whether enforced by the language (as in C#) or by convention (as in Python)—that separates an object's internal implementation from its external interface, providing controlled access through public methods known as getters and setters. This ensures that an object's internal representation is hidden and protected from the "outside world" while only allowing interaction via well-defined interfaces.

This separation between internal attributes and the "outside world" is the primary benefit of encapsulation since it grants data protection and integrity. By making attributes private, encapsulation prevents external code from modifying an object's internal state in unexpected, and sometimes malicious, ways. Since this external code can only access data via controlled methods, it may be subjected to logic validation, error checking and business rules. This protection layer leads to more robust and maintainable code that is another benefit of this principle.


### Code Example from Scripture Memorizer
This principle was used in every part of the ScriptureMemorizer code, but one example is the Word class:

```csharp
class Word
{
    private string _text;      // Private data - cannot be accessed directly
    private bool _isHidden;    // Private state - encapsulated within the class

    public Word(string text)   // Constructor controls object creation
    {
        _text = text;
        _isHidden = false;
    }

    public string GetWord()    // Controlled access to private data
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);  // Business logic encapsulated
        }
        return _text;
    }

    public void HideWord()     // Controlled modification of private state
    {
        _isHidden = true;
    }

    public bool IsHidden()     // Safe read-only access to private state
    {
        return _isHidden;
    }
}
```

On this class we can see that the attributes, <code>_text</code> and <code>_isHidden</code>, are declared as private, which in itself protects them from being externally accessed and modified. However, since this information may be needed outside the class, we provide controlled access through getters like <code>GetWord()</code> and <code>IsHidden()</code>, which allow external code to view the object's attribute state and values, as well as the setter <code>HideWord()</code> that allows the outside code to change the value of an internal value of the object when necessary, but also in a controlled way, and if you notice the code comes initially with <code>_isHidden</code> set as <code>false</code> and once the method changes its value to <code>true</code>, there is no way to return it to its original value, since the only available setter cannot perform this operation.

This also makes the maintenance easier since if we want to change how we represent a hidden word we need only to change the <code>GetWord()</code> method and nowhere else, making this behavior centralized and consistent throughout the application. This maintenance benefit is also evident since each class manages its own data and behavior, the <code>Word</code> class knows how to hide itself, the <code>Scripture</code> class knows how to manage a collection of words, the <code>Reference</code> class knows how to format scripture references and so on. This separation of concerns through encapsulation creates a robust architecture where changes to one class won't significantly impact the maintenance of the whole application.  

### Real-World Application
Encapsulation is essential in real-world software systems. For example, in a banking application, account balance should never be directly accessible. Instead, encapsulation ensures balance changes only occur through validated <code>deposit()</code> and <code>withdraw()</code> methods that enforce business rules like minimum balance requirements and transaction limits.