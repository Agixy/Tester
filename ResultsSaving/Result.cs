using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultsSaving
{
    public class Result
    {
        public int GoodAnswers { get; set; }
        public int AllQuestions { get; set; }
        public string CategoryOfExam { get; set; }

        public override string ToString()
        {
            return $"Wynik: {GoodAnswers}/{AllQuestions} ({CountProcentResult(GoodAnswers, AllQuestions)}%)";
        }

        public double CountProcentResult(int goodAnsw, int allQuest)
        {
            return Math.Round(((double)goodAnsw / allQuest) * 100);
        }
    }
}
