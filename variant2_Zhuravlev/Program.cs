using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace variant2_Zhuravlev
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Произвести сортировку по:('1'-убывание '2'-возрастание)");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    StreamReader sr1 = new StreamReader("i1.txt");
                    List<Item1> items1 = new List<Item1>();
                    string str1;
                    while ((str1 = sr1.ReadLine()) != null)
                    {
                        string[] t = str1.Split(' ');
                        items1.Add(new Item1 { id = t[0], name = t[1] });
                    }
                    sr1.Close();

                    StreamReader sr2 = new StreamReader("i2.txt");
                    List<Item2> items2 = new List<Item2>();
                    string str2;
                    while ((str2 = sr2.ReadLine()) != null)
                    {
                        string[] t = str2.Split(' ');
                        items2.Add(new Item2 { invent = t[0], id = t[1], group = t[2], cena = Convert.ToInt32(t[3]) });
                    }
                    sr2.Close();

                    //соединение
                    StreamWriter sw = new StreamWriter("soed.txt");
                    var soed = from gr in items2
                               join t in items1 on gr.id equals t.id
                               select new { Id = gr.id, Cena = gr.cena, Name = t.name, Group = gr.@group };
                    Console.WriteLine("Наим-ие   Ст-ть  Код    Группа");
                    Console.WriteLine("------------------------------");
                    foreach (var item in soed)
                    {
                        Console.WriteLine($"{item.Name} - {item.Cena} - {item.Id} - {item.Group}");
                        sw.WriteLine($"{item.Name} {item.Cena} {item.Id} {item.Group}");
                    }
                    sw.Close();
                    Console.ReadLine();


                    break;
                case 2:
                    //группировка по группе продуктов
                    StreamReader sr3 = new StreamReader("soed.txt");
                    List<SortItem> sortitems = new List<SortItem>();
                    string str3;
                    while ((str3 = sr3.ReadLine()) != null)
                    {
                        string[] t = str3.Split(' ');
                        sortitems.Add(new SortItem { name = t[0], cena = t[1], id = t[2], groupp = t[3] });
                    }
                    var itemsGroups = from item in sortitems
                                      group item by item.groupp into g
                                      select new
                                      {
                                          Name = g.Key,
                                          Count = g.Count(),
                                          Studentes = from p in g select p
                                      };
                    foreach (var item in itemsGroups)
                    {
                        Console.WriteLine($"\nГруппа '{item.Name}' содержит элементов - {item.Count}");
                        foreach (SortItem it in item.Studentes)
                        {
                            Console.WriteLine($"{it.name} - {it.cena}");
                        }
                        Console.WriteLine();
                    }
                    sr3.Close();
                    Console.ReadLine();
                    break;
            }
        }
    }
    internal class Item1
    {
        public string id { get; set; }
        public string name { get; set; }

    }
    internal class Item2
    {
        public string invent { get; set; }
        public string id { get; set; }
        public string group { get; set; }
        public int cena { get; set; }
    }
    internal class SortItem
    {
        public string name { get; set; }
        public string cena { get; set; }
        public string id { get; set; }
        public string groupp { get; set; }
    }
}
