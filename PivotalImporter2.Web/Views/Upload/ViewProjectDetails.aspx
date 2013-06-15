<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Controllers.Models.TestPivotalModel>" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Model" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Pivotal service Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Pivotal service Test</h2>

	<% foreach (var p in Model.PivotalProjects) { %>
		
		<div>
			<h3><%: p.Name %> [<%: p.Id %>]</h3>

			<h4>Users</h4>
			<ul>
			<%
			var users = Model.ProjectUsers.Where(x => x.Key == p.Id).FirstOrDefault().Value;
			foreach (var u in users) { %>
				<li><%: u.Person.Name %> (<%: u.Id %>)</li>
			<% } %>
			</ul>

			<h4>Stories</h4>
			<ul>
			<%
			
			if (Model.ProjectStories.Where(x => x.Key == p.Id).Any())
			{
			var stories = Model.ProjectStories.Where(x => x.Key == p.Id).First().Value;
				if (stories != null) {
			foreach (var u in stories) { %>
				<li><%: u.Name %></li>
				<% } %>
			<% } %>
			<% } %>
			</ul>
		</div>
		<p>&nbsp;</p>

	<% } %>
	

</asp:Content>
