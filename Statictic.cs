using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Курсова_робота
{
    class Statictic
    {
        static public void PrintFile(string fname)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(fname);

            var nodes = xd.GetElementsByTagName("OneForAll");
            double fullPrise = 0;
            List<XmlElement> dels = new List<XmlElement>();
            foreach (XmlElement it in nodes)
            {
                foreach (XmlElement item in it.ChildNodes)
                {
                    foreach (XmlAttribute attr in item.Attributes)
                    {
                        if (attr.Name == "id")
                        {
                            Console.Write($"#{attr.Name} = {attr.Value,-4}");
                            Chef.id = Convert.ToInt32(attr.Value);
                        }
                        else if (attr.Name == "price")
                        {
                            Console.Write($"{attr.Name} = {attr.Value,-7}");
                            if (fname == "../../../SalesStatistic.xml")
                            {
                                fullPrise += Convert.ToDouble(attr.Value);
                            }
                        }
                        else Console.Write($"{attr.Name,-3} - {attr.Value,-13}");
                        if (attr.Name == "count" && attr.Value == "0") dels.Add(item);
                    }
                    Console.WriteLine();
                }
                foreach (XmlElement del in dels)
                {
                    it.RemoveChild(del);
                }
            }
            xd.Save(fname);
            if (fname == "../../../SalesStatistic.xml") Console.WriteLine($"Total prise = {fullPrise}");
            Console.WriteLine();
        }
        static public void AddSalesStatistics(double price, string name, int count)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("../../../SalesStatistic.xml");

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
                            found = true;
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                if (e.Name == "count")
                                {
                                    e.Value = (Convert.ToInt32(e.Value) + count).ToString();
                                }
                                else if (e.Name == "price")
                                {
                                    e.Value = (Convert.ToInt32(e.Value) + price).ToString();
                                    found = true;
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
                    C.Value = count.ToString();
                    newDish.Attributes.Append(C);

                    XmlAttribute P = xd.CreateAttribute("price");
                    P.Value = price.ToString();
                    newDish.Attributes.Append(P);

                    it.AppendChild(newDish);
                }
            }
            xd.Save("../../../SalesStatistic.xml");
        }
    }
}
