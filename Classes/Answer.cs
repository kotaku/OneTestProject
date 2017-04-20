using System;

namespace OneTestProject.Classes
{
    [Serializable]
    public class Answer
    {
        private string _text;
        private bool _correctFlag;

        const string DEFF_ANSWER = "Empty answer. Always false.";

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool isCorrect
        {
            get { return _correctFlag; }
            set { _correctFlag = value; }
        }


        public Answer(string text, bool status)
        {
            Text = text;
            isCorrect = status;
        }

        public Answer(string text) : this(text, false)
        {
        }

        public Answer() : this(DEFF_ANSWER, false)
        {
        }        
    }
}
