﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questions
{
    [Serializable]
    public abstract class Question
    {
        private string question_text;
        private string question_type;
        private int question_order;

        public string Question_text
        {
            set
            {
                if (!value.Any(char.IsPunctuation))
                    question_text = value;
            }
            get
            {
                return question_text;
            }
        }

        public int Question_order
        {
            set
            {
                question_order = value;
            }
            get
            {
                return question_order;
            }
        }
        public string Question_type
        {
            set
            {
                question_type = value;
            }
            get
            {
                return question_type;
            }
        }




        public Question(string text, int order, string type)
        {
            Question_text = text;
            Question_order = order;
            Question_type = type;
        }

        public abstract List<int> Default_values();
        public abstract void Reset_values();
        public abstract List<int> Current_values();
        public abstract void Set_values(List<int> Values);


        public abstract bool Validate();

    }
    [Serializable]
    public class Slider : Question
    {

        private int start;
        private int start_caption;
        private int end;
        private int end_caption;
        public readonly List<int> Slider_default = new List<int> { 0, 100, 20, 80 };


        public int Start
        {
            get
            {
                return start;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    start = value;
                else
                    throw new ArgumentOutOfRangeException("Slider", "Start value should be between 0-100");


            }
        }
        public int Start_Caption
        {
            get
            {
                return start_caption;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    start_caption = value;
                else
                    throw new ArgumentOutOfRangeException("Slider", "Start caption should be between 0-100");

            }
        }
        public int End
        {
            get
            {
                return end;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    end = value;
                else
                    throw new ArgumentOutOfRangeException("Slider", "End value should be between 0-100");

            }
        }
        public int End_Caption
        {
            get
            {
                return end_caption;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    end_caption = value;
                else
                    throw new ArgumentOutOfRangeException("Slider", "End caption should be between 0-100");
            }
        }

        public Slider(string text, int order, int start = 0, int end = 100, int start_caption = 20, int end_caption = 80) : base(text, order, "Slider")
        {
            Start = start;
            Start_Caption = start_caption;
            End = end;
            End_Caption = end_caption;

        }
        public Slider() : base("", -1, "Slider")
        {
            Start = Slider_default[0];
            End = Slider_default[1];
            Start_Caption = Slider_default[2];
            End_Caption = Slider_default[3];
        }
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Start);
            temp.Add(End);
            temp.Add(Start_Caption);
            temp.Add(End_Caption);
            return temp;
        }
        public override List<int> Default_values()
        {
            return Slider_default;
        }
        public override void Reset_values()
        {
            Start = Slider_default[0];
            End = Slider_default[1];
            Start_Caption = Slider_default[2];
            End_Caption = Slider_default[3];

        }
        public override void Set_values(List<int> Values)
        {
            Start = Values[0];
            End = Values[1];
            Start_Caption = Values[2];
            End_Caption = Values[3];
        }

        public override bool Validate()
        {

            if (Start >= End)
            {
                string Message = string.Format("Start value should be lower than End value where Start = {0} End ={1}", Start, End);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider", Message);
            }
            if (Start >= Start_Caption)
            {
                string Message = string.Format("Start value should be lower than Start caption where Start = {0} Start caption ={1}", Start, Start_Caption);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider", Message);
            }


            if (Start_Caption >= End)//Start Caption should be lower than End value 
            {
                string Message = string.Format("Start Caption should be lower than End value where Start caption = {0} End ={1}", Start_Caption, End);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider", Message);
            }
                if (Start_Caption >= End_Caption)
            {
                string Message = string.Format("Start Caption should be lower than End caption where Start caption = {0} End caption ={1}", Start_Caption, End_Caption);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider", Message );
            }

            if (End_Caption >= End)//End caption should be lower than End value 
            {
                string Message = string.Format("End Caption should be Lower than End value where End caption = {0} End ={1}", End_Caption, End);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider",Message );
            }
            if (End_Caption <= Start_Caption)//End caption should be higer than Start caption
            {
                string Message = string.Format("End caption should be higer than Start caption where End caption = {0}Start caption ={1}", End_Caption, Start_Caption);
                Reset_values();
                throw new ArgumentOutOfRangeException("Slider", Message);
            }

            return true;
        }
    }
    [Serializable]
    public class Smiley : Question
    {

        private int faces;
        private int Entered_faces;
        public int Faces
        {
            get
            {
                return faces;
            }
            set
            {
                Entered_faces = value;
                if (value >= 2 && value <= 5)
                {
                    faces = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Smiles", "Number of Smiles  should be between 2-5");

            }
        }

        public Smiley(string text, int order, int faces = 3) : base(text, order, "Smiley")
        {
            Faces = faces;
        }
        public Smiley() : base("", -1, "Smiley")
        {
            Faces = 3;
        }
        public override List<int> Default_values()
        {
            List<int> temp = new List<int>();
            temp.Add(3);
            return temp;
        }
        public override void Reset_values()
        {
            Faces = 3;
        }
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Faces);
            return temp;
        }
        public override void Set_values(List<int> Values)
        {
            Faces = Values[0];
        }
        public override bool Validate()
        {
            if (Entered_faces < 2 || Entered_faces > 5)//End caption should be higer than Start caption
            {
                return false;
            }
            return true;
        }
    }
    [Serializable]
    public class Stars : Question
    {
        private int star = 5;
        private int Entered_star;

        public int Star//property to get and set field within range[0-100]
        {
            get
            {
                return star;
            }
            set
            {
                Entered_star = value;//save user input what ever if correct or not (used in Validate Method)
                if (value >= 0 && value <= 10)
                {
                    star = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Stars", "Number of stars  should be between 0-10");
                }

            }
        }
        public Stars() : base("", -1, "Stars")//constructor used to add question with empty fields
        {
            Star = 5;
        }
        public Stars(string text, int order, int star = 5) : base(text, order, "Stars")//constructor used to initialize  fields and send text,order and stars to question constuctor
        {
            Star = star;
        }
        /// <summary>
        /// return default values
        /// </summary>
        /// <returns></returns>
        public override List<int> Default_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Star);
            return temp;
        }
        /// <summary>
        /// Resets the values.
        /// </summary>
        public override void Reset_values()
        {
            Star = 5;
        }
        /// <summary>
        /// return currents the values.
        /// </summary>
        /// <returns></returns>
        public override List<int> Current_values()
        {
            List<int> temp = new List<int>();
            temp.Add(Star);
            return temp;
        }
        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="Values"></param>
        public override void Set_values(List<int> Values)
        {

            Star = Values[0];
        }
        /// <summary>
        /// validate user inputs
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (Entered_star < 0 || Entered_star > 10)//End caption should be higer than Start caption
            {
                return false;
            }
            return true;
        }
    }

}
