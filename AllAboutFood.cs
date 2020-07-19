using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Курсова_робота
{
    class Dish
    {
        string name;
        double price;
       
        public string Name
        {
            get => name;
            set
            {
                if (value != null) name = value;
                else throw new Exception("Name = null");
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
            }
        }

        public Dish(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public Dish() : this("Noname", 0)
        { }
        public override string ToString()
        {
            return $"Name - {Name,-7} Price = {Price,-4}";
        }
    }
    static class Chef
    {
        static public int id = 1;
        static public void WriteDishToFullFile(Dish dish) //Full file has all information about all dishes and drinks
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("../../../FullFile.xml");
            XmlElement root = xd.DocumentElement;

            XmlElement el = xd.CreateElement("Dish");

            XmlAttribute i = xd.CreateAttribute("id");
            id++;
            i.Value = id.ToString();
            el.Attributes.Append(i);

            XmlAttribute name = xd.CreateAttribute("name");
            name.Value = dish.Name;
            el.Attributes.Append(name);

            XmlAttribute price = xd.CreateAttribute("price");
            price.Value = dish.Price.ToString();
            el.Attributes.Append(price);

            root.AppendChild(el);

            xd.Save("../../../FullFile.xml");
        }
        static public void AddDishToFullFile()
        {
            Console.Write("Enter name of new dish ---> ");
            string name = Console.ReadLine();

            Console.Write("Enter price of new dish ---> ");
            double price = Convert.ToDouble(Console.ReadLine());

            WriteDishToFullFile(new Dish(name, price));
        }
        static public void AddDishesToCanteen(int id, int count)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("../../../FullFile.xml");

            var nodes = xd.GetElementsByTagName("OneForAll");

            XmlElement DishToCanteen = null;
            foreach (XmlElement it in nodes)
            {
                foreach (XmlElement item in it.ChildNodes)
                {
                    foreach (XmlAttribute el in item.Attributes)
                    {
                        if (el.Name == "id" && el.Value == id.ToString())
                        {
                            DishToCanteen = item;
                            Console.WriteLine("found");

                        }
                    }
                }
            }

            Dish dish = new Dish();
            foreach (XmlAttribute it in DishToCanteen.Attributes)
            {
                if (it.Name == "name") dish.Name = it.Value;
                if (it.Name == "price") dish.Price = Convert.ToInt32(it.Value);
            }
            XmlDocument xd2 = new XmlDocument();
            xd2.Load("../../../Canteen.xml");

            nodes = xd2.GetElementsByTagName("OneForAll");

            bool found = false;
            foreach (XmlElement it in nodes)
            {
                foreach (XmlElement item in it.ChildNodes)
                {
                    foreach (XmlAttribute el in item.Attributes)
                    {
                        if (el.Name == "name" && el.Value == dish.Name)
                        {
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                if (e.Name == "count")
                                {
                                    e.Value = (Convert.ToInt32(e.Value) + count).ToString();
                                    found = true;
                                }
                            }
                        }
                    }
                }
                if (found == false)
                {
                    XmlElement newDish = xd2.CreateElement("Dish");

                    XmlAttribute name = xd2.CreateAttribute("name");
                    name.Value = dish.Name;
                    newDish.Attributes.Append(name);

                    XmlAttribute price = xd2.CreateAttribute("price");
                    price.Value = dish.Price.ToString();
                    newDish.Attributes.Append(price);

                    XmlAttribute count_ = xd2.CreateAttribute("count");
                    count_.Value = count.ToString();
                    newDish.Attributes.Append(count_);
                    it.AppendChild(newDish);
                }
            }
            xd2.Save("../../../Canteen.xml");
        }
        static public bool MakeAnOrder(ref double price, string name, int count)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("../../../Canteen.xml");
            bool rez = false;
            bool found = false;
            var nodes = xd.GetElementsByTagName("OneForAll");
            int p = 0;
            foreach (XmlElement it in nodes)
            {
                foreach (XmlElement item in it.ChildNodes)
                {
                    foreach (XmlAttribute el in item.Attributes)
                    {
                        if (el.Name == "name" && el.Value == name)
                        {
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                if (e.Name == "price") p = Convert.ToInt32(e.Value);
                                else if (e.Name == "count")
                                {
                                    if (Convert.ToInt32(e.Value) >= count)
                                    {
                                        e.Value = (Convert.ToInt32(e.Value) - count).ToString();
                                        price = p * count;
                                        Console.WriteLine($"Successful order of {count} {name}. It cost {price} dollars.");
                                        found = true;
                                        rez = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"There are not so many({count}) {name}, there are only {e.Value}.");
                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (found == false) Console.WriteLine($"There are not these dishes: {name}.");
            xd.Save("../../../Canteen.xml");
            return rez;
        }
        static public void EvaluatingDish(string name)
        {
            Console.WriteLine($"\t\t\t\t  Pleace evaluating {name}");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("  -------1-------   ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("-------2-------   ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("-------3-------   ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("-------4-------   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-------5-------\n");


            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("   It`s terribly       ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Not tasty           ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("tasty           ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("very tasty        ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("delicious    \n");

            Console.ForegroundColor = ConsoleColor.White;
            int mark = 0;
            while (mark <= 0 || mark >= 6)
            {
                Console.Write("\n                                             ");
                mark = Convert.ToInt32(Console.ReadLine());
            }            
            Console.WriteLine("                                          Thanks");

            XmlDocument xd = new XmlDocument();
            xd.Load("../../../Marks.xml");

            var nodes = xd.GetElementsByTagName("OneForAll");

            bool found = false;
            foreach (XmlElement it in nodes)
            {
                foreach (XmlElement item in it.ChildNodes)
                {
                    foreach (XmlAttribute el in item.Attributes)
                    {
                        if (el.Name == "name" && el.Value == name)
                        {
                            int count = 0;
                            found = true;
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                if (e.Name == "count")
                                {
                                    count = Convert.ToInt32(e.Value);
                                    e.Value = (Convert.ToInt32(e.Value) + 1).ToString();
                                }
                                else if (e.Name == "mark")
                                {
                                    e.Value = (Convert.ToInt32(e.Value) + mark).ToString();
                                }
                                else if(e.Name == "totalMark")
                                {
                                    e.Value = ((Convert.ToDouble(e.Value) * count + mark) / (count + 1)).ToString();
                                }
                            }
                        }
                    }
                }
                if (found == false)
                {
                    XmlElement newDish = xd.CreateElement("Saled");

                    XmlAttribute N = xd.CreateAttribute("name");
                    N.Value = name;
                    newDish.Attributes.Append(N);

                    XmlAttribute C = xd.CreateAttribute("count");
                    C.Value = "1";
                    newDish.Attributes.Append(C);

                    XmlAttribute P = xd.CreateAttribute("mark");
                    P.Value = mark.ToString();
                    newDish.Attributes.Append(P);

                    XmlAttribute M = xd.CreateAttribute("totalMark");
                    M.Value = mark.ToString();
                    newDish.Attributes.Append(M);

                    it.AppendChild(newDish);
                }
            }
            xd.Save("../../../Marks.xml");
        }
    }
}
