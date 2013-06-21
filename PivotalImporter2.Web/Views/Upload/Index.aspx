<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PivotalImporter2.Web.Controllers.Models.UploadViewModel>" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Upload
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Upload</h2>
	<form action="/upload/confirm" method="post" enctype="multipart/form-data">
	    <fieldset>
        <div class="field">
		    <label for="ProjectId">Project:</label>
		    <select name="ProjectId">
			    <% foreach (var project in Model.PivotalProjects) { %>
				    <option value="<%: project.Id %>"><%: project.Name %></option>
			    <% } %>
		    </select>
            <%: Html.ValidationMessageFor(x => x.ProjectId) %>
        </div>
		<!-- TODO: Allow upload of file by dragging onto area? -->
        
        <div class="field">
		    <label for="file">Filename:</label>
		    <%--<input type="file" name="file" id="file" />--%>
            <input type="file" data-val="true" data-val-required="Please select a file to upload" name="file" />
            <%: Html.ValidationMessageFor(x => x.File) %>
		</div>
      </fieldset>
        <div class="buttons">
            <button class="btnSubmit">Preview Stories</button>
        </div>
	</form>

</asp:Content>
