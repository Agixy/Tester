using Exams.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams
{
    class ExamContext : DbContext
    {
        public ExamContext() : base("name=Exams")
        {
        }

        public DbSet<Category> Categories  { get; set; }
        public DbSet<Question> Questions  { get; set; }
        public DbSet<Answer> Answers  { get; set; }


    }
}
