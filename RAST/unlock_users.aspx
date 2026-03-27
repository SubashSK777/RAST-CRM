<%@ Page Title="Unlock Users" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="unlock_users.aspx.cs" Inherits="unlock_users" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section class="content-header">
            <h1>User Unlock Management</h1>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="page-wrapper-box">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Locked Users</h3>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="gvLockedUsers" runat="server"
                                CssClass="table table-bordered table-striped" AutoGenerateColumns="False"
                                OnRowCommand="gvLockedUsers_RowCommand" EmptyDataText="No locked users found.">
                                <Columns>
                                    <asp:BoundField DataField="s_Name" HeaderText="User Name" />
                                    <asp:BoundField DataField="s_Email" HeaderText="Email Address" />
                                    <asp:BoundField DataField="failed_attempts" HeaderText="Failed Attempts" />
                                    <asp:BoundField DataField="locked_at" HeaderText="Locked At" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnUnlock" runat="server" Text="Unlock"
                                                CommandName="UnlockUser" CommandArgument='<%# Eval("i_UserId") %>'
                                                CssClass="btn btn-success btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Content>