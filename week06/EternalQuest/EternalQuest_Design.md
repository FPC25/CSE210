# Classes

## Normal Classes

### Profile

#### Responsibilities

- _name: string
- _level: int
- _currentXP: int
- _birthday: DateTime
- _age: int
- _married: bool
- _dominicalEducation: string?
- _male: bool
- _priesthood: string?
- _ordinances: `dictionary<string, DateTime>`
- _calling: `List<String>`
- _familysearchLink: string
- _ldsAccount: string
- _patriarcalBlessing: bool
- _sacramentalTime: DateTime
- _working: bool
- _activeRecommendation: bool
- _recommendationDueDate: DateTime
- _quests: `Dict<string, List<Quest>>`

#### Constructor

- Profile(string Name, int age, string sex)

#### Behaviors

- GetAge(): int
- GetDominicalEducation: string
- SetDominicalEducation: void
- GetPriesthood: string
- SetAaronicPriesthood: void
- SetMelchizedekPriesthood: void
- GetOrdinances: Dict<string, DateTime>
- AddOrdinance: void
- GetCalling: `List<string>`
- AddCalling: void
- RemoveCalling: void
- GetFamilysearchLink: string
- SetFamilysearchLink: void
- GetAccount: string
- SetAccount: void
- GetPatriarcalBlessing: bool
- SetPatriarcalBlessing: void
- GetSacramentalTime: DateTime
- SetSacramentalTime: void
- GetWorking: bool
- SetWorking: void
- ProfileMenu: void
- GetLevel: int
- GetXP: int
- AddXP: void
- CalculateNextLevelXp: int
- DisplayLevelProgress: void
- DisplayUserInfo: void

### Game

#### Responsibilities

#### Constructor

- Run()

#### Behaviors

- InitialMenu(): void
- GameMenu(): void

### SaveLoadProgress

#### Responsibilities

- _file: string

#### Constructor

- SaveLoadProgress(string filepath)

#### Behaviors

- SaveProgress(): void
- LoadProgress(): Profile

### QuestManager

#### Responsibilities

- _user: Profile

#### Constructor

- QuestManager(Profile player)

#### Behaviors

- DisplayActiveQuests: void
- ActivateTimeQuest: bool -> activate quest that depends on the time
- CalculateChecklistDifficulty: int
- CalculateXpPerQuest: int
- GetNotTimedQuests: `List<Quest>`;
- GetWeeklyQuests: `List<Quest>`;
- GetMonthlyQuests: `List<Quest>`;
- DisplayActiveQuests: void
- DisplayFailedQuests: void
- DisplayCompletedQuests: void
- DisplayDailyQuest: void
- DisplayWeeklyQuest: void
- DisplayMonthlyQuest: void

## Parent Class

### Quest

#### Responsibilities

- _shortName: string
- _description: string
- _xpPoints: int
- _status: string -> it it was completed or failed to completed

#### Constructor

- Quest(string shortName, string description, int xpPoints)

#### Behaviors

- RecordEvent(): void Abstract
- IsComplete(): bool Abstract
- GetDetailsString(): string
- GetStringRepresentation(): string Abstract

## Child Classes

### SimpleQuest

#### Responsibilities

- _active: bool
- const XPMAX: double

#### Constructor

- SimpleQuest(string shortName, string description, int xpPoints)

#### Behaviors

- RecordEvent(): void
- IsComplete(): bool
- GetDetailsString(): string

### ChecklistQuest

#### Responsibilities

- _amountCompleted: int
- _target: int
- _bonusXP: int
- const XPMAXSTEP: double
- const XPMAXBONUS: double

#### Constructor

- ChecklistQuest(string shortName, string description, int xpPoints, int bonus, int target)

#### Behaviors

- RecordEvent(): void
_amountComplete++
if (IsCompleted())
{
    do something
}
else
{
    do another thing;
}

- IsComplete(): bool
return _amountCompleted == _target;

- GetDetailsString(): string
- GetStringRepresentation(): string

### TimedQuest

#### Responsibilities

- _initialDate: DateTime
- const XPMAX: double

#### Constructor

- TimedQuest(string shortName, string description, int xpPoints, DateTime initialDate)

#### Behaviors

- RecordEvent(): void
- IsComplete(): bool
- GetDetailsString(): string
- ActivateQuest(): bool

### EternalQuest

#### Responsibilities

- _period: string
- const XPMAX: double

#### Constructor

- EternalQuest(string shortName, string description, int xpPoints, string _period)

#### Behaviors

- RecordEvent(): void
- IsComplete(): bool
- GetStringRepresentation(): string

### CustomQuest

#### Responsibilities

- _difficulty: string
- const XPEASEY: double
- const XPMEDIUM: double
- const XPHARD: double

#### Constructor

- CustomQuest(string shortName, string description, int xpPoints, string _difficulty)

#### Behaviors

- RecordEvent(): void
- IsComplete(): bool
- GetStringRepresentation(): string
- CreateQuestByDifficulty(): void
