# Classes

## Parent Class

### Assignment

#### Responsibilities
<ul>
    <li>_studentName: string</li>
    <li>_topic: string</li>
</ul>

#### Constructor
<ul>
    <li>Assignment(string name, string topic)</li>
</ul>

#### Behaviors
<ul>
    <li>GetSummary(): string</li>
</ul>

## Child Classes 

### MathAssignment

#### Responsibilities
<ul>
    <li>_textbookSection: string</li>
    <li>_problems: string</li>
</ul>

#### Constructor
<ul>
    <li>MathAssignment(string name, string topic, string textbookSection, string problems)</li>
</ul>

#### Behaviors
<ul>
    <li>GetHomeworkList(): string</li>
</ul>

### WritingAssignment

#### Responsibilities
<ul>
    <li>_title: string</li>
</ul>

#### Constructor
<ul>
    <li>WritingAssignment(string name, string topic, string title)</li>
</ul>

#### Behaviors
<ul>
    <li>GetWritingInfo(): string</li>
</ul>
