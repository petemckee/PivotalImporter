<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PivotalImporter2.Web.Controllers.Models.UploadModel>" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Upload
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Upload</h2>
	<form action="/upload/confirm" method="post" enctype="multipart/form-data">

		<label for="projectId">Project:</label>
		<select name="projectId">
			<% foreach (var project in Model.PivotalProjects) { %>
				<option value="<%: project.Id %>"><%: project.Name %></option>
			<% } %>
		</select>

		<br />
		<!--
		TODO: Allow upload of file by dragging onto area?
		 -->

		<label for="file">Filename:</label>
		<input type="file" name="file" id="file" />

		<br />

	  <button class="btnSubmit">Upload</button>

	</form>

</asp:Content>
