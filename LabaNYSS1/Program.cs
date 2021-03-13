using System;
using System.Collections.Generic;

namespace LabaNYSS1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Welcome to Phonebook");
            Console.WriteLine("Use next commands to: ");
            Console.WriteLine("'1' - create a new record");
            Console.WriteLine("'2' - delete an existing record");
            Console.WriteLine("'3' - edit an existing record");
            Console.WriteLine("'4' - view all existing records");
            Console.WriteLine("'5' - view an existing record");
            Console.WriteLine("'6' - to exit");
            int command;
            while ((command = Convert.ToInt32(Console.ReadLine()))!=6)
            {
                switch (command)
                {
                    case 1:
                        Console.WriteLine("Вы создаете новую запись!");
                        p.AddRecord();
                        Console.WriteLine("Enter a command");
                        break;
                    case 2:
                        p.DeleteRecord();
                        Console.WriteLine("Enter a command");
                        break;
                    case 3:
                        p.EditRecord();
                        Console.WriteLine("Enter a command");
                        break;
                    case 4:
                        p.ViewAllRecords();
                        Console.WriteLine("Enter a command");
                        break;
                    case 5:
                        p.ViewRecord();
                        Console.WriteLine("Enter a command");
                        break;
                    default: break;

                }
            }
        }
       public void AddRecord()
        {
            Record record = new Record();
            

            Console.WriteLine("Введите фамилию");
         
            string lastname = CreateString(Console.ReadLine());
            record.Lastname = lastname;

            Console.WriteLine("Введите имя");
            string name = CreateString(Console.ReadLine());
            record.Name = name;

            Console.WriteLine("Введите отчество, если это необходимо");
            string middlename = Console.ReadLine();
            if (!string.IsNullOrEmpty(middlename)) record.Middlename =  middlename;

            Console.WriteLine("Введите номер телефона");
            
            int phoneNumber = CreateInt(Convert.ToInt32(CreateString(Console.ReadLine())));
            record.Phonenumber = phoneNumber;

            Console.WriteLine("Введите страну");
            string country = CreateString(Console.ReadLine());
            record.Country = country;


            Console.WriteLine("Введите дату рождения в формате дд.мм.гггг, если это необходимо");
            string rawDate = Console.ReadLine();
            record.Birthdate = CreateDate(rawDate);
            Console.WriteLine("Введите организацию, если это необходимо");
            string company = Console.ReadLine();
            if (!string.IsNullOrEmpty(company)) record.Company = company;

            Console.WriteLine("Введите должность, если это необходимо");
            string job = Console.ReadLine();
            if (!string.IsNullOrEmpty(company)) record.Job = job;

            Console.WriteLine("Введите дополнительную информацию, если это необходимо");
            string others = Console.ReadLine();
            if (!string.IsNullOrEmpty(company)) record.Others = others;

            PhoneNumbersList.records.Add(record);
        }
        public void EditRecord()
        {
            Console.WriteLine("Редактирование записи");
            Console.WriteLine("Введите номер телефона абонента, чью запись вы хотите изменить");
            int number = CreateInt(Convert.ToInt32(CreateString(Console.ReadLine())));
            Record record = PhoneNumbersList.records.Find(x => x.Phonenumber == number);
            Console.WriteLine("Старая информация об абоненте:");
            Console.WriteLine(record.ToString());
            Console.WriteLine("Если Вы нажмете Ввод, не изменив данные, то запись не изменится");
            Console.WriteLine("Измените фамилию");
            string newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Lastname!=newString) record.Lastname = newString;
            Console.WriteLine("Измените имя");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Lastname != newString) record.Name = newString;
            Console.WriteLine("Измените отчество");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Middlename != newString) record.Middlename = newString;
            Console.WriteLine("Измените номер"); 
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString))
            {
                int newNumber = Convert.ToInt32(newString);
                if (isUniqueNumber(newNumber)) record.Phonenumber = newNumber;
                else Console.WriteLine("Номер не уникальный, попробуйте снова!");
            }
            Console.WriteLine("Измените страну");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Country != newString) record.Country = newString;
            Console.WriteLine("Измените дату рождения");
            string rawDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(rawDate)) record.Birthdate = CreateDate(rawDate);
            Console.WriteLine("Измените компанию");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Company != newString) record.Company = newString;
            Console.WriteLine("Измените должность");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Job != newString) record.Job = newString;
            Console.WriteLine("Измените дополнительную информацию");
            newString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newString) && record.Others != newString) record.Others = newString;
            Console.WriteLine("Новая запись выглядит следующим образом:");
            Console.WriteLine(record.ToString());
        }
        public bool isUniqueNumber(int number)
        {
            for (int i =0; i < PhoneNumbersList.records.Count; i++)
            {
                if (PhoneNumbersList.records[i].Phonenumber == number) return false;

            }
            return true;
        }
        public DateTime CreateDate(string rawDate)
        {
            
            if (!string.IsNullOrEmpty(rawDate))
            {

                int day = Convert.ToInt32(rawDate.Split('.')[0]);
                int month = Convert.ToInt32(rawDate.Split('.')[1]);
                int year = Convert.ToInt32(rawDate.Split('.')[2]);
                DateTime birthdate = new DateTime(year, month, day);
                return birthdate;
            }
            else return new DateTime();
        }
        public void ViewAllRecords()
        {
            foreach(var item in PhoneNumbersList.records)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public void ViewRecord()
        {
            Console.WriteLine("Введите номер записи из справочника");
            int item = Convert.ToInt32(Console.ReadLine());
            if (item > 0 && item <= PhoneNumbersList.records.Count) Console.WriteLine(PhoneNumbersList.records[item-1].ToString());
            else Console.WriteLine("Такой записи не существует");
        }
        public void DeleteRecord()
        {
            Console.WriteLine("Введите номер записи из справочника");
            int item = Convert.ToInt32(Console.ReadLine());
            if (item > 0 && item <= PhoneNumbersList.records.Count) PhoneNumbersList.records.RemoveAt(item - 1);
            else Console.WriteLine("Такой записи не существует");
        }
        public string CreateString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Это поле обязательно!");
                CreateString(Console.ReadLine());
            }
            return str;

        }
        public int CreateInt(int i)
        {

            if (i.Equals(null) || i<=0 || !isUniqueNumber(i))
            {
                if (!isUniqueNumber(i)) Console.WriteLine("Телефон не уникален");
                else Console.WriteLine("Это поле обязательно!");
                CreateInt(Convert.ToInt32(CreateString(Console.ReadLine())));
            }
            return i;
        }
    }
    
    public static class PhoneNumbersList
    {
        public static List<Record> records = new List<Record>();
    }
    public class Record
    {
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public int Phonenumber { get; set; }
        public string Country { get; set; }
        public DateTime Birthdate { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Others { get; set; }

        public override string ToString()
        {
            return $"Абонент: ФИО - {Lastname} {Name} {Middlename}, Номер телефона - {Phonenumber}, Страна - {Country}, Дата рождения - {Birthdate}, Организация - {Company}, Должность - {Job}, Прочая информация - {Others}";
        }

    }
}
