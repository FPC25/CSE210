# Classes

## Preferences Class

### Responsibilities
<ul>
    <li> _filePath: string <li>
    <li> _userName: string </li>
    <li> _dateFormat: string </li>
    <li> _journalFormat: string</li>
</ul>

### Behaviors
<ul>
    <li> CreatePreferencesFile(): List&lt;string&gt;</li>
    <li> ReadPreferences(): List&lt;string&gt; </li>
</ul>

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
    <li> _dateFormat: string </li>
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
    <li> _userName: string</li>
    <li> _preferredExtension: string </li>
    <li> _entries: List&lt;Entry&gt;</li>
</ul>

### Behaviors
<ul>
    <li> FindFile: bool</li>
    <li> LoadJournal(): List&lt;Entry&gt;</li>
    <li> Menu(): void</li>
    <li> SaveEntry: Entry</li>
    <li> EditEntry: Entry</li>
    <li> SaveJournal(): void</li>
    <li> DisplayJournal(): void</li>  
</ul>

# Libraries

<ul>
    <li> Random: I will use this to 'randomly' select the prompt the user will answer in their entries;</li>
    <li> System.IO: to read the text</li>
    <li> System.Text.Json: to read and save journal as Json if the user preferred</li>
    <li> DateTime: To get the date and time the user makes the entry</li>
    <li> CsvHelper: If the user prefers to use CSV to save their journal it can be done</li>
</ul>

# Files

Here we find the json or csv files for each situation 
<ul>
    <li> prompts.json: I collection of questions to help and guide the user to write their entries</li>
    <li> preferences.json: save the user's preferences such as data format, preferred journal file format to be saved</li>
    <li> journal.json/.csv: the journal in itself</li>
</ul>
