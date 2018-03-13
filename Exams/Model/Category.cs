using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual IList<Question> Questions { get; set; }

        public Category()
        {
            Questions = new List<Question>();
        }
        
    }
}
