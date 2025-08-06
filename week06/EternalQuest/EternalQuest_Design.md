# Classes

## Normal Classes

### Profile

#### Responsibilities

- _name: string</li>
- _level: int</li>
- _currentXP: int</li>
- _age: int</li>
- _married: bool</li>
- _dominicalEducation: string?</li>
- _male: bool</li>
- _priesthood: string?</li>
- _ordinances: `dictionary<string, DateTime>`</li>
- _calling: `List<String>`</li>
- _familysearchLink: string</li>
- _ldsAccount: string</li>
- _patriarcalBlessing: bool</li>
- _sacramentalTime: DateTime</li>
- _working: bool</li>
- _activeRecommendation: bool</li>
- _recommendationDueDate: DateTime</li>
- _quests: `Dict<string, List<Quest>>`</li>

#### Constructor

- Profile(string Name, int age, string sex)</li>

#### Behaviors

- GetDominicalEducation: string</li>
- SetDominicalEducation: void</li>
- GetPriesthood: string</li>
- SetAaronicPriesthood: void</li>
- SetMelchizedekPriesthood: void</li>
- GetOrdinances: Dict<string, DateTime></li>
- AddOrdinance: void</li>
- GetCalling: `List<string>`</li>
- AddCalling: void</li>
- RemoveCalling: void</li>
- GetFamilysearchLink: string</li>
- SetFamilysearchLink: void</li>
- GetAccount: string</li>
- SetAccount: void</li>
- GetPatriarcalBlessing: bool</li>
- SetPatriarcalBlessing: void</li>
- GetSacramentalTime: DateTime</li>
- SetSacramentalTime: void</li>
- GetWorking: bool</li>
- SetWorking: void</li>
- ProfileMenu: void</li>
- GetLevel: int</li>
- GetXP: int</li>
- AddXP: void</li>
- CalculateNextLevelXp: int</li>
- DisplayLevelProgress: void</li>
- DisplayUserInfo: void</li>

### Game

#### Responsibilities

- _user: Profile</li>

#### Constructor

- Run()</li>

#### Behaviors

- InitialMenu(): void</li>
- GameMenu(): void</li>

### SaveLoadProgress

#### Responsibilities

- _file: string</li>

#### Constructor

- SaveLoadProgress(string filepath)</li>

#### Behaviors

- SaveProgress(): void</li>
- LoadProgress(): Profile</li>

### QuestManager

#### Responsibilities

- _user: Profile</li>

#### Constructor

- QuestManager(Profile player)</li>

#### Behaviors

- DisplayActiveQuests: void</li>
- ActivateTimeQuest: bool -> activate quest that depends on the time</li>
- CalculateChecklistDifficulty: int</li>
- CalculateXpPerQuest: int</li>
- GetNotTimedQuests: List&lt;Quest&gt;</li>
- GetWeeklyQuests: List&lt;Quest&gt;</li>
- GetMonthlyQuests: List&lt;Quest&gt;</li>
- DisplayActiveQuests: void</li>
- DisplayFailedQuests: void</li>
- DisplayCompletedQuests: void</li>
- DisplayDailyQuest: void</li>
- DisplayWeeklyQuest: void</li>
- DisplayMonthlyQuest: void</li>

## Parent Class

### Quest

#### Responsibilities

- _shortName: string</li>
- _description: string</li>
- _xpPoints: int</li>
- _status: string -> it it was completed or failed to completed</li>

#### Constructor

- Quest(string shortName, string description, int xpPoints)</li>

#### Behaviors

- RecordEvent(): void Abstract</li>
- IsComplete(): bool Abstract</li>
- GetDetailsString(): string</li>
- GetStringRepresentation(): string Abstract</li>

## Child Classes

### SimpleQuest

#### Responsibilities

- _active: bool</li>
- const XPMAX: double</li>

#### Constructor

- SimpleQuest(string shortName, string description, int xpPoints)</li>

#### Behaviors

- RecordEvent(): void</li>
- IsComplete(): bool</li>
- GetDetailsString(): string</li>

### ChecklistQuest

#### Responsibilities

- _amountCompleted: int</li>
- _target: int</li>
- _bonusXP: int</li>
- const XPMAXSTEP: double</li>
- const XPMAXBONUS: double</li>

#### Constructor

- ChecklistQuest(string shortName, string description, int xpPoints, int bonus, int target)</li>

#### Behaviors

- RecordEvent(): void</li>
- IsComplete(): bool</li>
- GetDetailsString(): string</li>
- GetStringRepresentation(): string</li>

### TimedQuest

#### Responsibilities

- _initialDate: DateTime</li>
- const XPMAX: double</li>

#### Constructor

- TimedQuest(string shortName, string description, int xpPoints, DateTime initialDate)</li>

#### Behaviors

- RecordEvent(): void</li>
- IsComplete(): bool</li>
- GetDetailsString(): string</li>
- ActivateQuest(): bool</li>

### EternalQuest

#### Responsibilities

- _period: string</li>
- const XPMAX: double</li>

#### Constructor

- EternalQuest(string shortName, string description, int xpPoints, string _period)</li>

#### Behaviors

- RecordEvent(): void</li>
- IsComplete(): bool</li>
- GetStringRepresentation(): string</li>

### CustomQuest

#### Responsibilities

- _difficulty: string</li>
- const XPEASEY: double</li>
- const XPMEDIUM: double</li>
- const XPHARD: double</li>

#### Constructor

- CustomQuest(string shortName, string description, int xpPoints, string _difficulty)</li>

#### Behaviors

- RecordEvent(): void</li>
- IsComplete(): bool</li>
- GetStringRepresentation(): string</li>
- CreateQuestByDifficulty(): void</li>
