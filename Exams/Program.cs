using ConsoleTools.Menu;
using Exams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.IO;
using ResultsSaving;

namespace Exams
{
    class Program
    {
        static SavingOperations Saving = new SavingOperations();

        static void Main(string[] args)
        {
            Saving.Deserialization();            

            bool shouldExit = false;

            ConsoleMenu menu = new ConsoleMenu(new[] {
                new GeneralOption("Dodaj nową kategorię", AddCategory),
                new GeneralOption("Dodaj pytanie do istniejącej kategorii", AddQuestion),
                new GeneralOption("Przeprowadź test z kategorii", TakeTest ),
                new GeneralOption("Exit", () => shouldExit = true),
                });

            do
            {
                menu.Show();
            } while (!shouldExit);        
        }
       

        private static void AddCategory()
        {
            var category = new Category();
            Console.WriteLine("Podaj nazwę kategorii");
            string categoryName = Console.ReadLine();
            category.CategoryName = categoryName;

            using (var context = new ExamContext())   
            {
                var myCategory = context.Categories.FirstOrDefault(a => a.CategoryName == categoryName);       
                                                                                                                            
                if (myCategory == null) 
                {
                    myCategory = new Category();
                    myCategory.CategoryName = categoryName;
                    context.Categories.Add(myCategory);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Taka kategoria juz istnieje");
                }             
            }
        }

        private static Category CheckCategory(ExamContext context)
        {
            Category category = null;
            bool isCategoryCorrect = false;
            do
            {
                Console.WriteLine("Podaj nazwę kategorii:");
                string categoryName = Console.ReadLine();

                var checkedCategory = context.Categories.FirstOrDefault(a => a.CategoryName == categoryName);

                if (checkedCategory == null)
                {
                    Console.WriteLine("Nie ma takiej kategorii. Podaj prawidłowa kategorie.");
                }
                else
                {
                    category = checkedCategory;
                    isCategoryCorrect = true;
                }

            } while (!isCategoryCorrect);

            return category;
        }

        private static void AddQuestion()
        {          
            using (var context = new ExamContext())
            {
                var category = CheckCategory(context);

                var question = new Question();
                Console.WriteLine("Podaj pytanie");
                question.SingleQuestion = Console.ReadLine();

                question.Cat = category;
                context.Questions.Add(question);
                context.SaveChanges();

                Console.WriteLine("Pytanie zostalo dodane");

                for (int i = 1; i <= 4; i++)
                {
                    var answer = new Answer();
                    if (i == 1)
                    {
                        Console.WriteLine("Podaj prawidłową odpowiedź");
                        string myAnswer = Console.ReadLine();
                        answer.SingleAnswer = myAnswer;
                        answer.isAnswerCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine($"Podaj {i} błędą odpowiedź");
                        string myAnswer = Console.ReadLine();
                        answer.SingleAnswer = myAnswer;
                        answer.isAnswerCorrect = false;
                    }

                    answer.Quest = question;
                    context.Answers.Add(answer);       
                }
                context.SaveChanges();
            }
        }

        private static void TakeTest()
        {
            ResultsSaving.Result result = new ResultsSaving.Result();
            Console.Clear();
            int goodAnsw=0, numberOfQuestions;
            using (var context = new ExamContext())
            {
                 Category category = CheckCategory(context);

                foreach (var question in category.Questions)
                {
                    Console.WriteLine(question);

                    foreach (var answer in RandomPermutation(question.Answers))
                    {
                        Console.WriteLine(answer);
                    }

                    Console.WriteLine("Twoja odpowiedz:");
                    var userAnswer = Console.ReadLine();
                    Answer usAnswer;

                    usAnswer = question.Answers.FirstOrDefault(a => a.SingleAnswer
                    .Equals(userAnswer, StringComparison.CurrentCultureIgnoreCase)
                    && a.isAnswerCorrect);

                    if (usAnswer != null)       
                    {
                        goodAnsw++;
                    }
                    else
                        Console.WriteLine("Błędna odpowiedź");

                }
                numberOfQuestions = category.Questions.Count();
                result.CategoryOfExam = category.CategoryName;
            }
            
            result.GoodAnswers = goodAnsw;         
            result.AllQuestions = numberOfQuestions;
            Saving.ResultsList.Add(result);

            Console.WriteLine(result);         
            Saving.SerializeResult();        
        }   

        public static IEnumerable<T> RandomPermutation<T>(IEnumerable<T> sequence)
        {
            Random random = new Random();
            T[] resultArray = sequence.ToArray();
            
            for (int i = 0; i < resultArray.Length - 1; i++)
            {
                int swapIndex = random.Next(i, resultArray.Length);
                if (swapIndex != i)
                {
                    T temp = resultArray[i];
                    resultArray[i] = resultArray[swapIndex];
                    resultArray[swapIndex] = temp;
                }
            }
            return resultArray;
        }           
    }
}
