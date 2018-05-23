using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkovChain.WebApp.Models
{
    public class FileReader
    {
        private string _filePath;
        public FileReader(string filePath)
        {
            _filePath = filePath;
        }

        public virtual IEnumerable<double> Read()
        {
            string[] lines = File.ReadAllLines(_filePath);

            var list = new List<double>();
            foreach (var line in lines)
            {
                list.Add(Convert.ToDouble(line));
            }
            return list;
        }
    }
}
