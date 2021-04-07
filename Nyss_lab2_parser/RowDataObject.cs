using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyss_lab2_parser
{
    
    public class RowDataObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Object { get; set; }
        public string Сonfidentiality { get; set; }
        public string Integrity { get; set; }
        public string Access { get; set; }
        public RowDataObject(string id, string name, string description, string source, string @object, string сonfidentiality, string integrity, string access)
        {
            Id = id;
            Name = name;
            Description = description;
            Source = source;
            Object = @object;
            Сonfidentiality = сonfidentiality;
            Integrity = integrity;
            Access = access;
           
        }

       
        /*private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
   "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
"Нарушение целостности", "Нарушение доступности"};*/

        public override string ToString()
        {
            return $"Я ряд номер {Id}, {Name}, {Description}";
        }

    }
}
