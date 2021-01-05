using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.CustomAttributes
{
    public class RangeUntilYearAttribute : RangeAttribute
    {
        public RangeUntilYearAttribute(int minimum) : base(minimum, DateTime.Now.Year)
        {

        }
    }
}
