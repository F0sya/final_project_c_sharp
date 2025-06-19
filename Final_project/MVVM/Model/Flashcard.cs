using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project_wpf.MVVM.Model
{
    public class Flashcard
    {
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public bool IsFlipped { get; set; }
    }
}
