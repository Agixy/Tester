using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public string SingleAnswer { get; set; }
        public bool isAnswerCorrect { get; set; }
        public virtual Question Quest { get; set; }

        public override string ToString()
        {
            return $"Odp: {SingleAnswer}";
        }
    }
}
