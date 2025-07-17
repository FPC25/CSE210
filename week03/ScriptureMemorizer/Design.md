# Classes

## Word Class

### Responsibilities
<ul>
    <li>_text: string</li>
    <li>_isHidden: bool</li>
</ul>

### Constructor
<ul>
    <li>Word(text: string)</li>
</ul>

### Behaviors
<ul>
    <li>HideWord(): string</li>
        <!--
            hiddenword = len(word) * "_"
            return hiddenword
        -->
    <li>IsHidden: bool</li>
        <!--
            return _ishidden;
        -->
    <li>Hide: void</li>
        <!--
            _ishidden = true;
        -->
    <li>Display: void</li>
</ul>

## Scripture

### Responsibilities
<ul>
    <li>_scripture: List&lt;Word&gt; </li>
    <li>_reference: Reference </li>

</ul>

### Constructor
<ul>
    <li>Scripture(Reference: Reference, text: string)</li>
</ul>

### Behaviors
<ul>
    <li>Display(): void</li>
        <!--
            print(scripture)
        -->
    <li>HideRandomWord(): void</li>
        <!--
        scripture = list(text)
        word = random(scripture) # return a random Word obj
        index = scripture.index(word)
        if word.GetIsHidden() != true:
            word.HideWord()
            word.SetIsHidden()
            scripture[index] = hiddenword 
        -->
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
    <li>Display: void</li>
</ul>