# Classes

## Parent Class

### Activity

#### Responsibilities
<ul>
    <li>_activityName: string</li>
    <li>_message: string</li>
    <li>_activityDurationInSeconds: int</li>
</ul>

#### Constructor
<ul>
    <li>Activity(string name, string message)</li>
</ul>

#### Behaviors
<ul>
    <li>DisplayStartMessage(): int</li>
    <li>SetTimer(int time): void</li>
    <li>DisplayEndMessage(): void</li>
    <li>Countdown(int timeInSeconds): void</li>
    <li>Spinner(int timeInSeconds): void</li>
    <li>Ellipsis(int timeInSeconds): void</li>
</ul>

## Child Classes

### BreathingActivity

#### Responsibilities
<ul>
    <li>NAME: string (const)</li>
    <li>MESSAGE: string (const)</li>
    <li>STEP_TIME: int (const)</li>
    <li>NUM_STEPS: int (const)</li>
</ul>

#### Constructor
<ul>
    <li>BreathingActivity()</li>
</ul>

#### Behaviors
<ul>
    <li>Run(): void</li>
    <li>BreatheIn(int timeInSeconds): void</li>
    <li>BreatheOut(int timeInSeconds): void</li>
    <li>Hold(int timeInSeconds): void</li>
    <li>NextFullCycle(int ogTime, int stepTime, int numSteps): int</li>
</ul>

### ReflectingActivity

#### Responsibilities
<ul>
    <li>NAME: string (const)</li>
    <li>MESSAGE: string (const)</li>
    <li>_prompts: List&lt;string&gt;</li>
    <li>_questions: List&lt;string&gt;</li>
</ul>

#### Constructor
<ul>
    <li>ReflectingActivity()</li>
</ul>

#### Behaviors
<ul>
    <li>Run(): void</li>
    <li>GetRandomPrompt(): string</li>
    <li>GetRandomQuestion(List&lt;string&gt; usedQuestions): string</li>
    <li>DisplayPrompt(): void</li>
    <li>DisplayQuestion(DateTime time): void</li>
</ul>

### ListeningActivity

#### Responsibilities
<ul>
    <li>NAME: string (const)</li>
    <li>MESSAGE: string (const)</li>
    <li>_prompts: List&lt;string&gt;</li>
    <li>_count: int</li>
</ul>

#### Constructor
<ul>
    <li>ListeningActivity()</li>
</ul>

#### Behaviors
<ul>
    <li>Run(): void</li>
    <li>GetRandomPrompt(): void</li>
    <li>GetListFromUser(): List&lt;string&gt;</li>