using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.RequestCard
{
    public class FullCardInfoViewModel
    {
        public PersonInfo Person { get; set; }
        public ChooseProductModel Product { get; set; }
        public CommentForStepModel CommentForStep { get; set; }
    }
}