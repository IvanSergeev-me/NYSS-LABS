using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyss_lab2_parser
{
    public class DataObject
    {
        public DataObject(string id, string name, string descripion, string source, string @object, string сonfidentiality, string integrity, string access)
        {
            Id = id;
            Name = name;
            Descripion = descripion;
            Source = source;
            Object = @object;
            Сonfidentiality = сonfidentiality;
            Integrity = integrity;
            Access = access;
        }

        /*private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
   "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
"Нарушение целостности", "Нарушение доступности"};*/
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descripion { get; set; }
        public string Source { get; set; }
        public string Object { get; set; }
        public string Сonfidentiality { get; set; }
        public string Integrity { get; set; }
        public string Access { get; set; }

    }
}
