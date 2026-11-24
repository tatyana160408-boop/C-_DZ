НОМЕР 1

Пункт а

1. Name попадёт в JSON со значением Alex, потому что это публичное св-во.
2. Score попадёт в JSON со значением 100, потому что это публичное св-во.
3. secretCode не попадёт в JSON, потому что это приватное поле. По умолчанию сериализатор пропускает все поля.
4. IsAlive не попадёт в JSON, потому что это поле. По умолчанию сериализатор пропускает все поля.

Результат: {"Name":"Alex","Score":100}

Пункт б

Чтобы secretCode попало в JSON, перед объявлением поля нужно добавить [JsonInclude]. 
Оно указывает сериализатору включить это поле при вызове Serialize(p).


Пункт в
IsAlive не попадает в JSON, потому что это поле. По умолчанию JsonSerializer пропускает поля, даже если они объявлены как public.

Первый способ. Заменить поле на публичное свойство. Сериализатор прочитает его значение через get.
public bool IsAlive { get; set; }

Второй способ. Добавить [JsonInclude] над полем. Он укажет сериализатору включить это поле в JSON.
[JsonInclude]
public bool IsAlive;





НОМЕР 2

Пункт а

public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}



Пункт б

По умолчанию JsonSerializer учитывает регистр букв.
Поэтому title не совпадает с Title, и Title остаётся равным null.
Чтобы title сопоставился с Title, 
нужно включить настройку PropertyNameCaseInsensitive и передать её в Deserialize:


JsonSerializerOptions options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Game? game = JsonSerializer.Deserialize<Game>(json, options);



Пункт в

Да, является. Квадратные скобки обозначают массив, а его элементы могут быть разных типов.
В C# этот массив можно десериализовать в JsonElement[], потому что каждый JsonElement может представлять значение любого типа JSON.

string json = "[1, \"hello\", true, null]";
JsonElement[]? items = JsonSerializer.Deserialize<JsonElement[]>(json);


НОМЕР 3

1. File.   Метод File.ReadAllText прочитает небольшой файл с настройками целиком, а Console.WriteLine выведет полученный текст на экран.
2. File.   При каждом автосохранении можно преобразовать текущее состояние игры в JSON и записать его через File.WriteAllText. Этот метод заменит предыдущее сохранение новым.
3. FileStream.   Поток можно открыть в режиме FileMode.Append и через StreamWriter дописывать строки в конец журнала. При частых записях поток можно держать открытым, а содержимое журнала при этом не нужно загружать в память.
4. File. Метод   File.Exists проверит, есть ли файл сохранения. Если файл существует, File.ReadAllText прочитает его целиком в строку.
5. FileStream.   Метод Seek позволяет перейти к нужной позиции в бинарном файле, а Write — заменить с этой позиции ровно 10 байт без перезаписи всего файла.