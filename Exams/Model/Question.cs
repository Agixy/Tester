using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string SingleQuestion { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Category Cat { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }

        public override string ToString()    
        {
            return $"Pytanie: {SingleQuestion}";
        }

        public void ShowQuestion()
        {

        }
    }
}
