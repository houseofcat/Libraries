using System.Collections.Generic;

namespace HouseofCat.Models.General
{
    public class Stacky
    {
        public string ExceptionType { get; set; }
        public string Method { get; set; }
        public string FileName { get; set; }
        public int Line { get; set; }
        public List<string> StackLines { get; set; }
    }
}
