using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ResultsSaving
{
    public class SavingOperations
    {
        public List<Result> ResultsList { get; set; } = new List<Result>();
        private string PathToFile { get; } 

        public SavingOperations()
        {
            PathToFile = Path.Combine(Environment.CurrentDirectory, "Results of tests", "result.json");
        }

        public void SerializeResult()
        {
            var resultFile = new JavaScriptSerializer().Serialize(ResultsList);
            Directory.CreateDirectory(Path.GetDirectoryName(PathToFile));
            File.WriteAllText(PathToFile, resultFile);
            Console.WriteLine(File.ReadAllText(PathToFile));
        }         

        public void Deserialization()
        {
            try
            {
                string json = File.ReadAllText(PathToFile);
                ResultsList = new JavaScriptSerializer().Deserialize<List<Result>>(json);
            }
            catch
            {
                // Proceed with empty list            
            }         
        }
    }
}
