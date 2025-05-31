<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="JAwelsDiamonds.Views.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .grid-style {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

            .grid-style th {
                background-color: #f8f9fa;
                padding: 8px;
                text-align: left;
                cursor: pointer;
            }

                .grid-style th:hover {
                    background-color: #e9ecef;
                }

            .grid-style td {
                padding: 8px;
                border-bottom: 1px solid #ddd;
            }

            content: " ↑";
            color: #007bff;
        }

        .sorted-desc a:after {
            content: " ↓";
            color: #007bff;
        }
    </style>
    <h2>Welcome to JAwels & Diamonds</h2>

    <asp:GridView ID="JewelGridView" runat="server" AutoGenerateColumns="False"
        CssClass="grid-style" EmptyDataText="No jewels found in database"
        DataKeyNames="JewelID" AllowSorting="true" OnSorting="JewelGridView_Sorting">
        <Columns>
            <asp:BoundField DataField="JewelID" HeaderText="ID" SortExpression="JewelID" />
            <asp:BoundField DataField="JewelName" HeaderText="Name" SortExpression="JewelName" />

            <asp:TemplateField HeaderText="Price">
                <ItemTemplate>
                    <%# FormatPrice((decimal)Eval("JewelPrice")) %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="DetailBtn" runat="server" Text="Detail"
                        OnClick="DetailBtn_Click" CommandArgument='<%# Eval("JewelID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
