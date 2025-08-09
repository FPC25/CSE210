# What is polymorphism and why is it important?

- Explain the meaning of Polymorphism.
- Highlight a benefit of Polymorphism.
- Provide an application of Polymorphism.
- Use a code example of Polymorphism from the program you wrote (copy and paste a few lines of code that demonstrate the use of the principle).
- Thoroughly explain these concepts (this likely cannot be done in less than 100 words);

## Answer

### Definition and Benefits

Polymorphism is a fundamental concept in object-oriented programming that allows objects of different classes to be treated as objects of a common base class. This means that a single interface or method can work with different types of objects, each implementing its own version of the behavior. The main benefit of polymorphism is flexibility: it enables code reuse and makes it easier to extend and maintain programs. For example, you can write code that processes a list of quests, regardless of whether they are SimpleQuest, ChecklistQuest, or EternalQuest, because they all inherit from the Quest base class and implement the same methods.

Polymorphism is important because it allows you to design systems that are modular and scalable. You can add new quest types to EternalQuest without changing the code that manages quests, as long as the new types inherit from Quest and implement the required methods. This reduces coupling and increases the maintainability of your code.

### Application in EternalQuest

In EternalQuest, polymorphism is used to manage different quest types (SimpleQuest, ChecklistQuest, EternalQuest, etc.) through a common interface. The QuestManager can load, display, and update quests without knowing their specific types, relying on the abstract methods defined in the Quest base class.

### Code example from the EternalQuest project

```csharp
// Quest.cs (abstract base class)
abstract class Quest
{
    public abstract void RecordEvent(Profile player, bool conditional);
    public abstract void IsComplete(Profile player);
    public abstract Dictionary<string, string> GetDictRepresentation();
    public abstract int CalculateXpPerQuestType(int level);
}

// SimpleQuest.cs (derived class)
class SimpleQuest : Quest
{
    public override void RecordEvent(Profile player, bool conditional) { /* ... */ }
    public override void IsComplete(Profile player) { /* ... */ }
    public override Dictionary<string, string> GetDictRepresentation() { /* ... */ }
    public override int CalculateXpPerQuestType(int level) { /* ... */ }
}

// ChecklistQuest.cs (derived class)
class ChecklistQuest : Quest
{
    public override void RecordEvent(Profile player, bool conditional) { /* ... */ }
    public override void IsComplete(Profile player) { /* ... */ }
    public override Dictionary<string, string> GetDictRepresentation() { /* ... */ }
    public override int CalculateXpPerQuestType(int level) { /* ... */ }
}

// QuestManager.cs (using polymorphism)
foreach (List<Quest> category in _player.GetAllQuests())
{
    foreach (Quest quest in category.Value)
    {
        // Polymorphic call: works for any Quest type
        quest.RecordEvent(_player, true);
        Console.WriteLine(quest.GetDetailsString());
    }
}
```

In EternalQuest, the abstract class Quest defines a set of methods that all quest types must implement, such as RecordEvent and IsComplete. The derived classes (SimpleQuest, ChecklistQuest, EternalQuest) each provide their own implementation of these methods, tailored to their specific logic. The QuestManager and Profile classes interact with quests through the Quest interface, allowing them to manage any quest type without knowing its details. This is the essence of polymorphism: the ability to write code that works with a family of related objects in a generic way.

For example, when displaying active quests, QuestManager loops through all quests and calls GetDetailsString() on each one. The actual method executed depends on the runtime type of the quest object, not the compile-time type. This makes it easy to add new quest types in the future—just create a new class that inherits from Quest and implements the required methods. The rest of the system will work with it automatically, demonstrating the power and flexibility of polymorphism in
