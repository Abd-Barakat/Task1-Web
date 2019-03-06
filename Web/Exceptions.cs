using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class EmptyException:Exception
    {
        public EmptyException():base("Please write a question")
        {

        }
        public EmptyException(string Message, Exception ex) : base("Please write a question",ex)
        {

        }
    }
    public class PunctuationException:Exception
    {
        public PunctuationException() : base("Punctuation marks is not allowed")
        {

        }
       
        public PunctuationException(string Message, Exception ex) : base("Punctuation marks is not allowed", ex)
        {

        }
    }
    public class ContainNumberException:Exception
    {
        public ContainNumberException ():base ("Questions  shouldn't  contain number ")
        {

        }
        public ContainNumberException(string Message,Exception ex):base("Questions  shouldn't  contain number",ex)
        {

        }
    }
}