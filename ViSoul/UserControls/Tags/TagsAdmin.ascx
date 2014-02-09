<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagsAdmin.ascx.cs" Inherits="Sniper.Umbraco.Tags.UserControls.TagsAdmin" %>
<%@ Register Assembly="Sniper.Umbraco.UserControlDataTypeCommon" Namespace="Sniper.Umbraco.UserControlDataTypeCommon.WebControls" TagPrefix="sniper" %>
<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<div class="sniper">
    <span>
        <sniper:Setting runat="server" ID="Setting1" Label="Group" Description="The autocomplete group for the tags, only tags within the same group will be autocompleted">
            <ControlTemplate>
                <asp:TextBox runat="server" id="uxTagGroup"></asp:TextBox>
            </ControlTemplate>
        </sniper:Setting>
    </span>
</div>
