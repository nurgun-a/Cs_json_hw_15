using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Cs_json_hw_15
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        int ID;
      
        const string Group = "PV221";
        public Person() { }
        public Person(int _id) => ID = _id;
        public Person(string _name, int _id, int _age)
        {
            Name = _name;
            ID = _id;
            Age = _age;
        }
        public override string ToString() => $"{ID} {Name} {Age} {Group}";
    }
    public class RW_Person
    {
        public List <Person> MyPersons { get; set; }
        public string Fname { get; set; }
        public void SerializeP()
        {
            string p1_json = JsonSerializer.Serialize(MyPersons);
            File.WriteAllText(Fname, p1_json);
            WriteLine("Serialize OK");
        }
        public void DeserializeP()
        {                   
            if (File.Exists(Fname))
            {
                string p1_new_str = File.ReadAllText(Fname);
                List<Person> p1_new = JsonSerializer.Deserialize<List<Person>>(p1_new_str);
                foreach (Person item in p1_new)
                {
                    WriteLine(item);
                }
            }
            else
            {
                WriteLine($"Ошибка чтения");
            }
           
        }
        public void Menu(List<string> st, int _index)
        {
            for (int i = 0; i < st.Count; i++)
            {
                if (i == _index)
                {
                    BackgroundColor = ForegroundColor;
                    ForegroundColor = ConsoleColor.Black;
                }
                WriteLine($"             {st[i]}");
                ResetColor();
            }
            WriteLine();
        }
        public void Thread_func()
        {
            int index = 0;
            List<string> st_menu = new List<string> { "Записать", "Считать" };
            try
            {
                while (true)
                {
                    Clear();
                    WriteLine($"Enter - Выбор действия\nEsc   - Выход\n\n");
                    Menu(st_menu, index);
                    switch (ReadKey(true).Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (index < st_menu.Count() - 1)
                                index++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (index > 0)
                                index--;
                            break;
                        case ConsoleKey.Enter:
                            {
                                if (index == 0)
                                {
                                    SerializeP();
                                }
                                else if (index == 1)
                                {
                                    DeserializeP();
                                }
                                ReadKey();
                            }
                            break;
                        case ConsoleKey.Escape:
                            {
                                Environment.Exit(0);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>
            {
               new Person(10) { Name = "Ivan", Age = 33 },
               new Person(11) { Name = "Petr", Age = 29 },
               new Person(12) { Name = "Sergey", Age = 17 },
               new Person(13) { Name = "Vasiliy", Age = 45 }
            };

            RW_Person rwp = new RW_Person();            
            string f_name = "Person.json";
            rwp.MyPersons = persons;
            rwp.Fname = f_name;
            Thread th1 = new Thread(new ThreadStart(rwp.Thread_func));

            th1.Start();
        }        
    }
}
