<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unscrambler.aspx.cs" Inherits="UnScrambler.Unscrambler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
          <link rel="stylesheet" href="us.css"/>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <title>Word Unscrambler</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="wrapper">
                <h1>
                    <p class="shadow text1">
                        Unscrambler     
                    </p>
                </h1>
                <h2>
                    <p>
                        <asp:textbox ID="myTextletters" runat="server" placeholder="Your Letters" CssClass="myText"/>
                        <asp:textbox ID ="wordlen" runat="server" placeholder="0" CssClass="myLength" OnTextChanged="wordlen_TextChanged" AutoPostBack="true"/>
                    </p>
                    <div>
                        <asp:Repeater ID="rpt_container" runat="server">
                            <ItemTemplate>
                                <asp:textbox ID="letter" runat="server" MaxLength="1" CssClass="myText2"/>
                            </ItemTemplate>

                        </asp:Repeater>
                    </div>
                    <p>
                        <asp:Button cssclass="button" runat="server" ID="btn_unscramble" OnClick="btn_unscramble_Click" Text="Unscramble"/>
                    </p>
                </h2>
                <div>
                    <div style="background-color: white">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 33%;">
                                    <table>
                                        <asp:Repeater ID="rpt_found_col1" runat="server">
                                            <ItemTemplate>
                                                <tr><td><asp:Label ID="lbl_found" runat="server" /></td></tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                                <td style="width: 33%;">
                                    <table>
                                        <asp:Repeater ID="rpt_found_col2" runat="server">
                                            <ItemTemplate>
                                                <tr><td><asp:Label ID="lbl_found" runat="server" /></td></tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                                <td style="width: 34%;">
                                    <table>
                                        <asp:Repeater ID="rpt_found_col3" runat="server">
                                            <ItemTemplate>
                                                <tr><td><asp:Label ID="lbl_found" runat="server" /></td></tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <p id="invalidlengthvalidation" style="color: red;" runat="server" visible="false">
                    Invalid Length
                </p>
            </div>
        </form>
    </body>
</html>
