using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBay_Sell_Crawler
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String[] KeyWord = { "Date of Purchase", "ebay.oDocument.oPage" };
        String[] KeySKU = { "/", "?" };
        String SKU;
        String thisProductName;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text=GetHttpTxt(TextBox3.Text);
            //class="contentValueFont"
            TextBox1.Text = MakePlainText(TextBox1.Text.Replace("class=\"contentValueFont\">", ">,").Replace("class=\"onheadNav\">", ">,")).Replace("<br />","");
            //TextBox1.Text = MakePlainText(TextBox1.Text);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            String[] KeyWordFormat = TextBox1.Text.Split(KeyWord, StringSplitOptions.RemoveEmptyEntries);

            TextBox2.Text = KeyWordFormat[1].Replace("\t","");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            using (iniki_RivalEntities ent = new iniki_RivalEntities())
            {
                int DeleteCheck = 0;
                string[] DateTimeList = { "yyyy-MM-ddHH:mm:ss" };

                string[] str1 = Regex.Split(TextBox2.Text, ",");
                for (int i = 1; i <= str1.Length; i = i + 4)
                {
                    if (i >= str1.Length - 1)
                    { break; }
                    try
                    {
                        String Buyer = str1[i];
                        //String Price = str1[i + 1];
                        String Qua = str1[i + 2];
                        string FormatTime = str1[i + 3].Replace("PDT", "").Replace("PST", "");
                        DateTime BuyTime = DateTime.ParseExact(FormatTime, "MMM-dd-yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
                        var productsell = new Rival_ProductSell { Purchaser = Buyer, SellValue = Convert.ToInt32(Qua), SellDate = BuyTime, ProductName = thisProductName, Product_SKU = SKU };
                        ent.Rival_ProductSell.Add(productsell);
                        ent.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }

        }
        //整理原始碼標籤 By Demo
        public string MakePlainText(string x)
        {
            string filterString = x == null ? "" : x.Trim();
            if (!string.IsNullOrWhiteSpace(filterString))
            {
                filterString = filterString.Replace(Environment.NewLine, "₪");
                filterString = new Regex(@"<[^>]*>").Replace(filterString, "");
                filterString = new Regex(@"\s{2,}").Replace(filterString, "");
                return new Regex(@"[₪]+").Replace(filterString, "<br />");
            }
            return filterString;
        }

        //Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36
        //模擬瀏覽器去要原始碼資料
        private string GetHttpTxt(string strUrl)
        {
            string strHtml = string.Empty;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                myHttpWebRequest.Timeout = 40000;
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36";
                using (WebResponse myWebResponse = myHttpWebRequest.GetResponse())
                {
                    using (Stream myStream = myWebResponse.GetResponseStream())
                    {
                        using (StreamReader myStreamReader = new StreamReader(myStream))
                        {
                            strHtml = myStreamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return strHtml;

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            using(iniki_RivalEntities ent=new iniki_RivalEntities())
            {
                var v = from n in ent.Rival_Products where n.Id >= 20857 select n;
                //int i = v.ToList().Count;
                foreach(var h in v)
                {
                    string[] formatSKU = h.ProductsUrl.Split(KeySKU, StringSplitOptions.RemoveEmptyEntries);
                    String ProductUrl = "http://offer.ebay.com/ws/eBayISAPI.dll?ViewBidsLogin&item=" + formatSKU[4] + "&rt=nc&_trksid=p2047675.l2564";
                    TextBox3.Text = ProductUrl;
                    thisProductName = h.ProductsName;
                    SKU = formatSKU[4];

                    Button1_Click(null, null);
                    Thread.Sleep(40000);
                    Button2_Click(null, null);
                    Button3_Click(null, null);

                }
            }
        }

        
    }
}