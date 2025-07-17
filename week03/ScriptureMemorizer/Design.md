# Classes

## Word Class

### Responsibilities
<ul>
    <li>_text: string</li>
    <li>_isHidden: bool -> initialize as false</li>
</ul>

### Constructor
<ul>
    <li>Word(text: string)</li>
</ul>

### Behaviors
<ul>
    <li>HideWord(): string</li>
        <code>
           hiddenword = len(word) * "_"
           return hiddenword
        </code>
    <li>IsHidden: bool</li>
        <code>
            return _ishidden;
        </code>
    <li>Hide: void<li>
        <code>
            _ishidden = true;
        </code>
    <li>Display()</li>
</ul>

## Scripture

### Responsibilities
<ul>
    <li>_scripture: List<Word> </li>
    <li>_reference: Reference </li>

</ul>

### Constructor
<ul>
    <li>Scripture(Reference: Reference, text: string)</li>
</ul>

### Behaviors
<ul>
    <li>Display(): void</li>
        <code>
            print(scripture)
        </code>
    <li>HideRandomWord(): void</li>
        <code>
        scripture = list(text)
        word = random(scripture) # return a random Word obj
        index = scripture.index(word)
        if word.GetIsHidden() != true:
            word.HideWord()
            word.SetIsHidden()
            scripture[index] = hiddenword 
        </code>
    <li>IsCompletelyHidden(): bool</li>
</ul>

## Reference Class

### Responsibilities
<ul>
    <li>_book: string</li>
    <li>_chapter: int</li>
    <li>_verse: int</li>
    <li>_endVerse: int</li>
</ul>

### Constructor
<ul>
    <li>Reference(book: string, chapter: int, verse: int)</li>
    <li>Reference(book: string, chapter: int, verse: int, endVerse: int)</li>
</ul>

### Behaviors
<ul>
    <li>Display</li>
</ul>