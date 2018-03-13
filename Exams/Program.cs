using ConsoleTools.Menu;
using Exams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams
{
    class Program
    {
        static void Main(string[] args)
        {
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
                }

                context.Categories.Add(myCategory);
                context.SaveChanges();
            }
        }

        private static Category CheckCategory(ExamContext context)
        {
            Category category = null;
            bool isCategoryCorrect = false;
            do
            {
                Console.WriteLine("Podaj nazwę kategorii:");      // Dodac wracanie dod odania kateogrii
                string categoryName = Console.ReadLine();

                //using (var context = new ExamContext())
                //{
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
                        
               // }
            } while (!isCategoryCorrect);

            return category ;
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
            using (var context = new ExamContext())
            {
                var category = CheckCategory(context);

                foreach (var question in category.Questions)       
                {
                    Console.WriteLine(question.ToString());
                    foreach (var answerr in question.Answers)
                    {
                        Console.WriteLine(answerr);
                    }
                    Console.WriteLine("Twoja odpowiedź:");
                    string userAnswer = Console.ReadLine();
              
                    
                }
                Console.ReadKey();

            }

        }
    }
}
