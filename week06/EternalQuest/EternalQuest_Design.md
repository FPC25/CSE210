# Classes

## Normal Classes

### Profile

#### Responsibilities
<ul>
    <li>_name: string</li>
    <li>_level: int</li>
    <li>_currentXP: int</li>
    <li>_age: int</li>
    <li>_married: bool</li>
    <li>_dominicalEducation: ?string</li>
    <li>_male: bool</li>
    <li>_priesthood: ?string</li>
    <li>_ordinances: dictionary&lt;string, DateTime&gt;</li>
    <li>_calling: List<String></li>
    <li>_familysearchLink: string</li>
    <li>_ldsAccount: string</li>
    <li>_patriarcalBlessing: bool</li>
    <li>_sacramentalTime: DateTime</li>
    <li>_working: bool</li>
    <li>_activeRecommendation: bool</li>
    <li>_recommendationDueDate: DateTime</li>
    <li>_quests: Dict&lt;string, List&lt;Quest&gt;&gt;</li>
</ul>

#### Constructor
<ul>
    <li>Profile(string Name, int age, string sex)</li>
</ul>

#### Behaviors
<ul>
    <li>GetDominicalEducation: string</li>
    <li>SetDominicalEducation: void</li>
    <li>GetPriesthood: string</li>
    <li>SetPriesthood: void</li>
    <li>GetOrdinances: Dict<string, DateTime></li>
    <li>AddOrdinance: void</li>
    <li>GetCalling: List<string></li>
    <li>AddCalling: void</li>
    <li>RemoveCalling: void</li>
    <li>GetFamilysearchLink: string</li>
    <li>SetFamilysearchLink: void</li>
    <li>GetAccount: string</li>
    <li>SetAccount: void</li>
    <li>GetPatriarcalBlessing: bool</li>
    <li>SetPatriarcalBlessing: void</li>
    <li>GetSacramentalTime: DateTime</li>
    <li>SetSacramentalTime: void</li>
    <li>GetWorking: bool</li>
    <li>SetWorking: void</li>
    <li>ProfileMenu: void</li>
    <li>GetLevel: int</li>
    <li>GetXP: int</li>
    <li>AddXP: void</li>
    <li>CalculateNextLevelXp: int</li>
    <li>DisplayLevelProgress: void</li>
    <li>DisplayUserInfo: void</li>
</ul>

### Game

#### Responsibilities
<ul>
    <li>_user: Profile</li>
</ul>

#### Constructor
<ul>
    <li>Run()</li>
</ul>

#### Behaviors
<ul>
    <li>InitialMenu(): void</li>
    <li>GameMenu(): void</li>
</ul>


### SaveLoadProgress

#### Responsibilities
<ul>
    <li>_file: string</li>
</ul>

#### Constructor
<ul>
    <li>SaveLoadProgress(string filepath)</li>
</ul>

#### Behaviors
<ul>
    <li>SaveProgress(): void</li>
    <li>LoadProgress(): Profile</li>
</ul>

### QuestManager

#### Responsibilities
<ul>
    <li>_user: Profile</li>
</ul>

#### Constructor
<ul>
    <li>QuestManager(Profile player)</li>
</ul>

#### Behaviors
<ul>
    <li>DisplayActiveQuests: void</li>
    <li>ActivateTimeQuest: bool -> activate quest that depends on the time</li>
    <li>CalculateChecklistDifficulty: int</li>
    <li>CalculateXpPerQuest: int</li>
    <li>GetNotTimedQuests: List&lt;Quest&gt;</li>
    <li>GetWeeklyQuests: List&lt;Quest&gt;</li>
    <li>GetMonthlyQuests: List&lt;Quest&gt;</li>
    <li>DisplayActiveQuests: void</li>
    <li>DisplayFailedQuests: void</li>
    <li>DisplayCompletedQuests: void</li>
    <li>DisplayDailyQuest: void</li>
    <li>DisplayWeeklyQuest: void</li>
    <li>DisplayMonthlyQuest: void</li>
</ul>

## Parent Class

### Quest

#### Responsibilities
<ul>
    <li>_shortName: string</li>
    <li>_description: string</li>
    <li>_xpPoints: int</li>
    <li>_status: string -> it it was completed or failed to completed</li>
</ul>

#### Constructor
<ul>
    <li>Quest(string shortName, string description, int xpPoints)</li>
</ul> 

#### Behaviors
<ul>
    <li>RecordEvent(): void Abstract</li>
    <li>IsComplete(): bool Abstract</li>
    <li>GetDetailsString(): string</li>
    <li>GetStringRepresentation(): string Abstract</li>
</ul>

## Child Classes

### SimpleQuest

#### Responsibilities
<ul>
    <li>_active: bool</li>
    <li>const XPMAX: double</li>
</ul>

#### Constructor
<ul>
    <li>SimpleQuest(string shortName, string description, int xpPoints)</li>
</ul>

#### Behaviors
<ul>
    <li>RecordEvent(): void</li>
    <li>IsComplete(): bool</li>
    <li>GetDetailsString(): string</li>
</ul>

### ChecklistQuest

#### Responsibilities
<ul>
    <li>_amountCompleted: int</li>
    <li>_target: int</li>
    <li>_bonusXP: int</li>
    <li>const XPMAXSTEP: double</li>
    <li>const XPMAXBONUS: double</li>
</ul>

#### Constructor
<ul>
    <li>ChecklistQuest(string shortName, string description, int xpPoints, int bonus, int target)</li>
</ul>

#### Behaviors
<ul>
    <li>RecordEvent(): void</li>
    <li>IsComplete(): bool</li>
    <li>GetDetailsString(): string</li>
    <li>GetStringRepresentation(): string</li>
</ul>

### TimedQuest

#### Responsibilities
<ul>
    <li>_initialDate: DateTime</li>
    <li>const XPMAX: double</li>
</ul>

#### Constructor
<ul>
    <li>TimedQuest(string shortName, string description, int xpPoints, DateTime initialDate)</li>
</ul>

#### Behaviors
<ul>
    <li>RecordEvent(): void</li>
    <li>IsComplete(): bool</li>
    <li>GetDetailsString(): string</li>
    <li>ActivateQuest(): bool</li>
</ul>

### EternalQuest

#### Responsibilities
<ul>
    <li>_period: string</li>
    <li>const XPMAX: double</li>
</ul>

#### Constructor
<ul>
    <li>EternalQuest(string shortName, string description, int xpPoints, string _period)</li>
</ul>

#### Behaviors
<ul>
    <li>RecordEvent(): void</li>
    <li>IsComplete(): bool</li>
    <li>GetStringRepresentation(): string</li>
</ul>

### CustomQuest

#### Responsibilities
<ul>
    <li>_difficulty: string</li>
    <li>const XPEASEY: double</li>
    <li>const XPMEDIUM: double</li>
    <li>const XPHARD: double</li>
</ul>

#### Constructor
<ul>
    <li>CustomQuest(string shortName, string description, int xpPoints, string _difficulty)</li>
</ul>

#### Behaviors
<ul>
    <li>RecordEvent(): void</li>
    <li>IsComplete(): bool</li>
    <li>GetStringRepresentation(): string</li>
    <li>CreateQuestByDifficulty(): void</li>
</ul>