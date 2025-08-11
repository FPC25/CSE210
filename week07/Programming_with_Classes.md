# For each of the 4 principles of Programming with Classes, answer the following

- Briefly define the principle.
- How did you use that principle in one of your programs.
- How did using that principle help that program become more flexible for future changes?
- Your response must:
  - Explain the meaning of each principle.
  - Highlight how each principle was used in a program that you wrote.
  - Explain how each principles make makes that program more flexible for future changes.
  - Thoroughly explain these concepts. (This likely cannot be done in less than 100 words.)

## Answer

### Definition

While programming with classes we will face challenges that requires that we use some, if not all, principle of object-oriented programming (OOP): abstraction, encapsulation, inheritance and polymorphism. Together they make more secure, maintainable, scalable, flexible and cleaner programs.

The first principle, abstraction is the principle of hiding complex implementation details and exposing only the necessary features of an object. In my Eternal Quest program, I used abstraction by creating an abstract Quest class that defines the common interface for all quest types, such as methods for completion and displaying information. The specific details of how a quest is completed are implemented in subclasses like SimpleQuest, ChecklistQuest, and EternalQuest. This abstraction allowed me to treat all quests uniformly in the quest manager, making it easy to add new quest types in the future without changing the overall program structure. By focusing on what a quest does rather than how it does it, abstraction made my codebase easier to maintain and extend.

Encapsulation in the other hand is the practice of bundling data and methods that operate on that data within a single unit, and restricting direct access to some of the object's components. In my program, I encapsulated player data and quest data by making fields private and providing public methods for accessing and modifying them. For example, the Profile class stores personal information and only allows changes through specific methods, preventing accidental or unauthorized modifications. Encapsulation helped protect the integrity of my data and made it easier to debug and update the program, since changes to internal implementation did not affect other parts of the code that relied on the public interface.

Inheritance allows a class to inherit properties and behaviors from another class, promoting code reuse and logical hierarchy. In my project, all quest types inherit from the abstract Quest class, which provides shared attributes and methods. This means that SimpleQuest, ChecklistQuest, and EternalQuest automatically have the basic quest functionality, and I only needed to implement or override methods that are unique to each type. Inheritance made my code more organized and reduced duplication, so if I need to change something common to all quests, I can do it in one place. Again, making it easier to introduce new quest types in the future.

At last, polymorphism is the ability for different classes to be treated as instances of the same base class, typically through a shared interface or abstract class. In my program, I used polymorphism by storing all quests in a single list and calling methods like IsComplete() or RecordEvent() on them, regardless of their specific type. The actual behavior depends on the subclass implementation. This allowed my quest manager to handle any quest type seamlessly, making the program highly flexible. If I add a new quest type, the manager can interact with it without needing to know its details, which greatly simplifies future changes and expansions.

By applying these four principles, my programs became more modular, maintainable, and adaptable to future requirements. Each principle contributed to a codebase that is easier to understand, extend, and debug, ensuring long-term success as the program evolves.
