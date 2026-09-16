class Program
{
    static void Main()
    {
        Manager manager = new Manager();
        manager.LoadToFile();
        Console.WriteLine("1. Добавить контакт\n2. Найти контакт по имени\n3. Показать все контакты\n4. Удалить контакт");
        while (true)
        {
            int action = 0;

            Console.Write("Выберите что хотите сделать (введите цифру) ");
            if(!int.TryParse(Console.ReadLine(), out action))
            {
                Console.WriteLine("Вводите только цфиры! Ошибка!");
                continue;
            }
            if(action < 0|| action > 4)
            {
                Console.WriteLine("Вводите цифры только от 1 до 4! Ошибка!");
                continue;
            }
            switch (action)
            {
                case 1:
                    string name = "";
                    string numberPhone = "";
                    while (true)
                    {
                        Console.Write("Введите имя человека ");
                        name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Имя не может быть пустым или состоять из пробелов");
                            continue;
                        }
                        name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                        break;
                    }
                    while (true)
                    {
                        Console.Write("Введите номер телефона ");
                        numberPhone = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(numberPhone))
                        {
                            Console.WriteLine("Номер не может быть пустым или состоять из пробелов");
                            continue;
                        }
                        if (numberPhone[0] != '+')
                        {
                            Console.WriteLine("Номер должен начинаться с +");
                            continue;
                        }
                        if (numberPhone[1] != '7')
                        {
                            Console.WriteLine("После '+' должна идти цифра 7!");
                            continue;
                        }
                        break;
                    }
                    Contact contact = new Contact(name, numberPhone);
                    manager.AddNumber(contact);
                    manager.SaveToFile();
                    break;
                case 2:
                    while (true)
                    {
                        if (!manager.ChekNumberBook())
                        {
                            Console.WriteLine("Контактов ещё нет, сначала добавьте контакт");
                            break;
                        }
                        Console.Write("Введите имя человека ");
                        name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Имя не может быть пустым или состоять из пробелов");
                            continue;
                        }
                        name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                        manager.FindNumber(name);
                        manager.SaveToFile();
                        break;
                    }
                    break;
                case 3:
                    if (!manager.ChekNumberBook())
                    {
                        Console.WriteLine("Контактов ещё нет, сначала добавьте контакт");
                        break;
                    }
                    manager.ShowAllContacts();
                    manager.SaveToFile();
                    break;
                case 4:
                    if (!manager.ChekNumberBook())
                    {
                        Console.WriteLine("Контактов ещё нет, сначала добавьте контакт");
                        break;
                    }
                    Console.Write("Введите имя человека ");
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Имя не может быть пустым или состоять из пробелов");
                        continue;
                    }
                    name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                    manager.DeleteNumber(name);
                    manager.SaveToFile();
                    break;
            }
        }
    }
}
class Contact
{
    public string Name { get; set; }
    public string NumberPhone { get; set; }
    public Contact(string name, string numberPhone)
    {
        Name = name;
        NumberPhone = numberPhone;
    }
}
class Manager
{
    public Dictionary<string, string> contacts = new Dictionary<string, string>();
    public void AddNumber(Contact contact)
    {
        contacts.Add(contact.Name, contact.NumberPhone);
        Console.WriteLine("Номер успешно добавлен!");
    }
    public bool ChekNumberBook()
    {
        return contacts.Count > 0;
    }
    public void DeleteNumber(string name)
    {
        contacts.Remove(name);
        Console.WriteLine($"Контакт {name} удалён");
    }
    public void FindNumber(string name)
    {
            if(!contacts.TryGetValue(name, out var contact))
            {
                Console.WriteLine($"Номер телефона не найден");
            }
            else
            {
                Console.WriteLine($"Номер телефона {contact}");
            }
    }
    public void ShowAllContacts()
    {
            foreach (KeyValuePair<string, string> contact in contacts)
            {
                Console.WriteLine($"Имя: {contact.Key} Номер: {contact.Value}");
            }
    } 
    public void SaveToFile()
    {
        string file = "NumberBook.txt";
        List<string> lines = new List<string>();
        foreach(KeyValuePair<string, string> contact in contacts)
        {
            lines.Add($"{contact.Key}|{contact.Value}");
        }
        File.WriteAllLines(file, lines);
    }
    public void LoadToFile()
    {
        string file = "NumberBook.txt";
        if (!File.Exists(file))
        {
            return;
        }
        string[] lines = File.ReadAllLines(file);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 2)
            {
                contacts.Add(parts[0], parts[1]);
            }
        }
    }
}