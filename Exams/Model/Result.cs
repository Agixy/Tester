using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Exams.Model
{
    public class Result
    {
        public double GoodAnswers { get; set; }
        public double AllQuestions { get; set; }
        public double ProcentResult { get; set; }

        public override string ToString()
        {
            return $"Wynik: {GoodAnswers}/{AllQuestions} ({CountProcentResult(GoodAnswers, AllQuestions)}%)";
        }

        public double CountProcentResult(double goodAnsw, double allQuest)
        {
            return Math.Round((goodAnsw / allQuest) * 100);
        }

    }
}
