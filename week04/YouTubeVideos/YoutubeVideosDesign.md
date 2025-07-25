# Classes

## Comment

### Responsibilities
<ul>
    <li>_name: string</li>
    <li>_text: string</li>
</ul>

### Constructor
<ul>
    <li>Comment(string name, string text)</li>
</ul>

### Behaviors
<ul>
    <li>GetCommenterName: string</li>
    <li>GetCommentText: string</li>
</ul>

## Video

### Responsibilities
<ul>
    <li>_author: string</li>
    <li>_title: string</li>
    <li>_lengthSec: int</li>
    <li>_comments: List&lt;Comment&gt;</li>
</ul>

### Constructor
<ul>
    <li>Video(string title, string author, int length, List&lt;Comment&gt; comments)</li>
</ul>

### Behaviors
<ul>
    <li>GetTitle: string</li>
    <li>GetAuthor: string</li>
    <li>GetFormattedLength: string</li>
    <li>CountComment: int</li>
    <li>GetLengthInSec: int</li>
    <li>SecToMin: (int, int)</li>
    <li>MinToHour: (int, int, int)</li>
    <li>GetComments: List&lt;Comment&gt;</li>
</ul>