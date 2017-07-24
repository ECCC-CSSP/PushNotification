<%@ Page Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="RyanTest.aspx.cs" Inherits="PushNotification.RyanTest" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


<asp:Button ID="btnMail" runat="server" Text="Send Mail" 
    onclick="btnMail_Click" />

    <asp:Button ID="btnVal" runat="server" Text="Give Result" 
        onclick="btnVal_Click" />
    
    <asp:TextBox ID="txtVal"
        runat="server" Width="570px"></asp:TextBox>

        <asp:Calendar ID="calStart" runat="server"></asp:Calendar>
        <br />
        <asp:TextBox ID="txtStart"
        runat="server" Width="225px"></asp:TextBox>

        <asp:Calendar ID="calEnd" runat="server"></asp:Calendar>

         <br />

         <asp:TextBox ID="txtEnd"
        runat="server" Width="221px"></asp:TextBox>

        
       
</asp:Content>


