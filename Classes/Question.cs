using System;
using System.Collections.Generic;


namespace OneTestProject.Classes
{
    [Serializable]
    public class Question
    {
        private string _text;
        private List<Answer> _answers;
        private string _additionInformation;

        const string DEFF_QUESTION = "Empty question.";


        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public List<Answer> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }

        public string Addition
        {
            get { return _additionInformation; }
            set { _additionInformation = value; }
        }




        public Question(string text, List<Answer> answers, string addition)
        {
            Text = text;
            Answers = answers;
            Addition = addition;
        }

        public Question(string text, List<Answer> answers) : this(text, answers, "")
        {
        }

        public Question(string text) : this(text, new List<Answer>(), "")
        {
        }

        public Question() : this(DEFF_QUESTION, new List<Answer>(), "")
        {
        }
    }
}
