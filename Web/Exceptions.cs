using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class EmptyException:Exception
    {
        public EmptyException():base("please write a question")
        {

        }
        public EmptyException(string Message, Exception ex) : base("please write a question",ex)
        {

        }
    }
    public class PunctuationException:Exception
    {
        public PunctuationException() : base("question must not contain any punctuation marks")
        {

        }
       
        public PunctuationException(string Message, Exception ex) : base("question must not contain any punctuation marks", ex)
        {

        }
    }
    public class ContainNumberException:Exception
    {
        public ContainNumberException ():base ("questions  shouldn't  contain number ")
        {

        }
        public ContainNumberException(string Message,Exception ex):base("questions  shouldn't  contain number",ex)
        {

        }
    }
}