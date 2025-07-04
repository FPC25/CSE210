# Classes

## PromptGuide Class

### Responsibilities
<ul>
    <li> _prompts: file; </li>
</ul>

### Behaviors
<ul>
    <li> SelectPrompt(): string </li>
</ul>

## Entry Class

### Responsibilities
<ul>
    <li> _prompt: string </li>
    <li> _date: DateTime </li>
    <li> _humor: string</li>
    <li> _entry: string</li>
</ul>

### Behaviors
<ul>
    <li> Display(): void </li>
</ul>

## Journal.cs

### Responsibilities
<ul>
    <li> _name: string</li>
    <li> _preferredFormat: string </li>
    <li> _entries: List&lt;Entry&gt;</li>
</ul>

### Behaviors
<ul>
    <li> FindFile: string</li>
    <li> DisplayMenu(): int</li>
    <li> MakeEntry: Entry</li>
    <li> SaveJournal(): void</li>
    <li> LoadJournal(): List&lt;Entry&gt;</li>
    <li> DisplayJournal(): void</li>
</ul>