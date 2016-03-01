using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBay_Sell_Crawler
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] DateTimeList = { "MMM-dd-yy HH:mm:ss" };
            //Oct-26-15 06:26:43
            DateTime dd = DateTime.ParseExact("Oct-26-15 06:26:43", DateTimeList, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);

            DateTime sss = DateTime.ParseExact("Oct-26-15 06:26:43", "MMM-dd-yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
            Response.Write(dd.ToString("yyyy-MM-dd- HH:mm:ss"));
        }
    }
}