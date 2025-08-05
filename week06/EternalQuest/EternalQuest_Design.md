# Classes

## Normal Classes

### Profile

#### Responsibilities
<ul>
    <li>_name: string</li>
    <li>_level: int</li>
    <li>_age: int</li>
    <li>_ensign: ?string</li>
    <li>_sex: string</li>
    <li>_priesthood: ?string</li>
    <li>_ordinances: dictionary&lt;string, DateTime&gt;</li>
    <li>_calling: List<String></li>
    <li>_familysearchLink: string</li>
    <li>_ldsAccount: string</li>
    <li>_patriarcalBlessing: bool</li>
    <li>_sacramentalTime: DateTime</li>
    <li>_working: bool</li>
    <li>_activeRecommendation: bool</li>
    <li>_quests: Dict&lt;string, List&lt;Quest&gt;&gt;</li>
</ul>

#### Constructor
<ul>
    <li>Profile(string Name, int age, string sex)</li>
</ul>

#### Behaviors
<ul>
    <li>GetEnsign: void</li>
    <li>SetSeminar: string</li>
    <li>SetInstitute: Dict</li>
    <li>GetPriesthood: void</li>
    <li>SetPriesthood: string</li>
    <li>GetOrdinances: void</li>
    <li>AddOrdinance: DateTime</li>
    <li>GetCalling: void</li>
    <li>AddCalling: string</li>
    <li>RemoveCalling: string</li>
    <li>GetFamilysearchLink: void</li>
    <li>SetFamilysearchLink: string</li>
    <li>GetAccount: void</li>
    <li>SetAccount: string</li>
    <li>GetPatriarcalBlessing: void</li>
    <li>SetPatriarcalBlessing: bool</li>
    <li>GetSacramentalTime: void</li>
    <li>SetSacramentalTime: DateTime</li>
    <li>GetWorking: void</li>
    <li>SetWorking: string</li>
    <li>ProfileMenu: void</li>
    <li>GetLevel: void</li>
    <li>AddExperience</li>
    <li>CalculateNextLevelXp</li>
    <li>DisplayCompletedQuests: void</li>
    <li>DisplayFailedQuests: void</li>
    <li>DisplayLevelProgress</li>
    <li>DisplayUserInfo</li>
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
    <li>CalculateXpPerLevel: int</li>
    <li>GetNotTimedQuests: List&lt;Quest&gt;</li>
    <li>GetWeeklyQuests: List&lt;Quest&gt;</li>
    <li>GetMonthlyQuests: List&lt;Quest&gt;</li>
    <li>DisplayActiveQuests: void</li>
</ul>

## Parent Class

### Quest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Quest(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>


## Child Classes

### TutorialQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### SimpleQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### ChecklistQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### TimedQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### EternalQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### CustomQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>

### AgeQuest

#### Responsibilities
<ul>
    <li>_varName: type</li>
</ul>

#### Constructor
<ul>
    <li>Class(type var)</li>
</ul>

#### Behaviors
<ul>
    <li>MethodName: type</li>
</ul>