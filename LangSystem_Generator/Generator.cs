﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LangSystem_Generator.Models.Database;


namespace LangSystem_Generator
{
    class Generator
    {
        private static readonly string FirstNames = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\listaimion.txt";
        private static readonly string LastNames = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\listanazwisk.txt";
        private static readonly string BuisnessNames = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\listanazwfirm.txt";
        private static readonly string TradehNames = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\branze.txt";
        private static readonly string Cities = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\listamiast.txt";
        private static readonly string Streets = Path.GetDirectoryName(Application.ResourceAssembly.Location) + @"\ulice.txt";
        private static readonly List<string> Languages = new List<string>
        {
            "Angielski",
            "Niemiecki",
            "Włoski",
            "Norweski",
            "Hiszpański",
            "Francuski",
            "Rosyjski"
        };
        private static readonly List<string> Departments = new List<string>
        {
            "Warszawa 01-464",
            "Gdańsk 80-761",
            "Łódź  90-001",
            "Kraków 31-403",
            "Lublin 20-218",
            "Szczucin 70-004",
            "Poznań  60-967",
            "Wrocław 51-416"
        };

        private static readonly List<string> IDOfDepartment = new List<string>
        {
            "WAW",
            "GDA",
            "LDZ",
            "KRK",
            "LBL",
            "SZC",
            "POZ",
            "WRO"
        };

        private static readonly string DataBaseBulkPath = System.IO.Path.GetDirectoryName(Application.ResourceAssembly.Location);

        private static List<Department> departments = new List<Department>();

        public static void generateDataBase(bool update)
        {
            // Na podstawie tych dwóch list będziemy tworzyć adres XD
            List<string> streets = File.ReadAllLines(Streets).ToList();
            List<string> cities = File.ReadAllLines(Cities).ToList();

            int counter = 1;
            Random _rand = new Random();
            foreach( string department in Departments)
            {
                string ID = IDOfDepartment[counter - 1].ToString() + "_" + counter.ToString();
                string adress = Departments[counter - 1].ToString() + ", " + streets[_rand.Next(0, streets.Count())].ToString() + " " + _rand.Next(1, 513).ToString();

                departments.Add(new Department(ID, adress));

                counter++;
            }



            writeDataBase(false);
        }

        private static void writeDataBase(bool update)
        {
            // Tabela języki - działa :D
            using (var jezyk = new StreamWriter(DataBaseBulkPath + @"\Jezyki" + (update ? "Update" : "") + ".bulk"))
                foreach (string language in Languages)
                    jezyk.WriteLine(language);

            // Tabela filie - działa pięknie :D
            using (var filia = new StreamWriter(DataBaseBulkPath + @"\Filie" + (update ? "Update" : "") + ".bulk"))
                foreach (Department department in departments)
                    filia.WriteLine(department.DepartmentNr.ToString() + " | " + department.Adress.ToString());

        }


        
    }
}
