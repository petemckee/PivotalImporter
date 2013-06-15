<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<p>Hi there.</p>
	<ul>
		<li><a href="/upload">Upload stories</a></li>
		<li><a href="/Uploads/pivotalimporter2_example.xlsx" target="blank">Download example import spreadsheet</a></li>
		<%--<li><a href="/upload/confirm">Test upload stories</a></li>--%>
	</ul>

</asp:Content>
