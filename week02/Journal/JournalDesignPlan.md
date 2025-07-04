# Classes

## PromptGuide Class

### Responsibilities
<ul>
    <li> _promptsFilePath: file; </li>
    <li> _prompts: List&lt;string&gt;<li>
</ul>

### Behaviors
<ul>
    <li> ReadPromptsFile: List&lt;string&gt;</li>
    <li> SelectPrompt(): string </li>
</ul>

## Entry Class

### Responsibilities
<ul>
    <li> _prompt: string </li>
    <li> _humor: string</li>
    <li> _entry: string</li>
    <li> _entryTime: DateTime </li>
</ul>

### Behaviors
<ul>
    <li> AskHumor(): string </li>
    <li> MakeEntry(): string </li>
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
    <li> SaveEntry: Entry</li>
    <li> SaveJournal(): void</li>
    <li> LoadJournal(): List&lt;Entry&gt;</li>
    <li> DisplayJournal(): void</li>
    <li> Search(): List&lt;Entry&gt;</li>
</ul>

# Libraries and Misc

<ul>
    <li> Random: I will use this to 'randomly' select the prompt the user will answer in their entries;</li>
    <li> System.IO: to read the text</li>
    <li> System.Text.Json: to read and save journal as Json if the user preferred</li>
    <li> DateTime: To get the date and time the user makes the entry</li>
    <li> CsvHelper: If the user prefers to use CSV to save their journal it can be done</li>
</ul>