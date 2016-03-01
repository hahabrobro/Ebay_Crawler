<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EBay_Sell_Crawler.WebForm1" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Height="282px" Width="524px" TextMode="MultiLine"></asp:TextBox>
    
        <asp:TextBox ID="TextBox3" runat="server" Height="282px" Width="524px" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="開始抓商品資料" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="第一次過濾" />
    
    </div>
        <p>
    
        <asp:TextBox ID="TextBox2" runat="server" Height="282px" Width="524px" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="變成Csv" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="寫入資料庫" />
        </p>
    </form>
</body>
</html>
