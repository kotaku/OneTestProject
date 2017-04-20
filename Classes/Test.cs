using System;
using System.Collections.Generic;


namespace OneTestProject.Classes
{
    [Serializable]
    public class Test
    {
        private string _name;
        private List<Question> _questions;

        const string DEFF_NAME = "Empty name.";


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }



        public Test(string name, List<Question> questions)
        {
            Name = name;
            Questions = questions;
        }

        public Test(string name) : this(name, new List<Question>())
        {
        }

        public Test() : this(DEFF_NAME, new List<Question>())
        {
        }
    }
}
