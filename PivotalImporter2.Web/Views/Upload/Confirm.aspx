<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PivotalImporter2.Web.Controllers.Models.UploadModel>" %>
<%@ Import Namespace="PivotalImporter2.Domain.Extensions" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Upload
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<form action="">

	<div class="confirmHeader">
		<div class="storyCountWrapper">
			<span class="storyCount"><%: Model.PivotalStories.Count %></span> stories to upload to
			<select name="projectId" id="projectId">
				<% foreach (var p in Model.PivotalProjects) { %>
					<option value="<%: p.Id %>" <%: (Model.ProjectId == p.Id) ? " selected=\"selected\"" : String.Empty %>><%: p.Name %></option>
				<%} %>
			</select>
		</div>
		<button class="btnUploadToPivotal"><span>Upload to Pivotal</span></button>
	</div>
	
    <div class="genericLabels">
        <label>Add these labels to all stories:</label>
        <input type="text" name="genericLabels" value="<%: Model.GenericLabels %>" /> 
    </div>

	<h3 class="pnlHdr">Confirm details</h3>
	<div class="storyList">
		<% foreach (var story in Model.PivotalStories) { %>
			<div class="story">
			
				<button class="del delx">Delete</button>
				<button class="unDel">Re-instate</button>

			<div class="summary">
				<span class="expand">&nbsp;</span>
				<span class="estimate"><span class="e<%: story.Estimate %>"></span></span>
				<span class="name"><%: story.Name %></span>
			</div>
			<div class="detail">

				<input type="hidden" name="CreationDate" value="<%: story.CreationDate.ToString("yyyy/MM/dd") %>" />

				<div class="detailName">
					<span class="hide">&nbsp;</span>
					<input class="fullWidth name" type="text" name="name" value="<%: story.Name %>" />
				</div>
				
				<ul class="subset">
					<li class="clearfix type">
					  <div class="left">STORY TYPE</div>
					  <div class="right">

							<select name="storyType">
								<% foreach (PivotalStoryType option in Enum.GetValues(typeof(PivotalStoryType))) { %>
									<option value="<%: option %>" <%= (story.StoryTypeString == option.ToString()) ? "selected=\"selected\"" : String.Empty %>><%: option.ToTitleCase() %></option>
								<% } %>
							</select>
 
						</div>
					</li>
					<li class="clearfix release_date_row" style="display: none;">
					  <div class="left">RELEASE DATE</div>
					  <div class="right">&nbsp;
					   </div>
					</li>
					<li class="clearfix points point_scale_linear">
					  <div class="left">POINTS</div>
					  <div class="right">
						<input type="text" name="estimate" value="<%: story.Estimate %>" />
					  </div>
					</li>
					<li class="clearfix state">
					  <div class="left">STATE</div>
					  <div class="right">
							<span>Not yet scheduled</span>
					  </div>
					</li>
					<li class="clearfix requester select_list">
					  <div class="left">REQUESTER</div>
					  <div class="right">
							<select name="storyType">
								<% foreach (var option in Model.PivotalMemberships) { %>
									<option value="<%: option.Person.Name %>" <%= (story.Requestor == option.Person.Name) ? "selected=\"selected\"" : String.Empty %>><%: option.Person.Name %></option>
								<% } %>
							</select>
					  </div>
					</li>
					<li class="clearfix owner select_list">
					  <div class="left">OWNER</div>
					  <div class="right">
						<select name="owner">
								<% foreach (var option in Model.PivotalMemberships) { %>
									<option value="<%: option.Person.Name %>" <%= (story.Owner == option.Person.Name) ? "selected=\"selected\"" : String.Empty %>><%: option.Person.Name %></option>
								<% } %>
							</select>
					  </div>
					</li>
    
				  </ul>

				
				<label>Description</label>
				<textarea class="fullWidth desc" name="description" rows="3" cols="80"><%: story.Description %></textarea>

				<label>Labels</label>
				<%

					var labelStr = String.Empty;
					if (story.LabelValues != null && story.LabelValues.Any())
					{
						foreach (var str in story.LabelValues)
						{
							labelStr += ", " + str;
						}
						labelStr = labelStr.Remove(0, 2);
					}
%>
				<input type="text" class="fullWidth labels" name="labels" value="<%: labelStr %>" />

				<% if (story.Tasks != null && story.Tasks.Any()) { %>
				<label>Tasks</label>
				<ul class="tasks">
					<% foreach (var task in story.Tasks) { %>
					<li><%: task.Description %></li>
					<% } %>
				</ul>
				<% } %>
			</div>
			</div>
		<% } %>

	</div>
	<div class="confirmHeader">
		<div class="storyCountWrapper"><span class="storyCount"><%: Model.PivotalStories.Count %></span> stories to upload</div>
		<button class="btnUploadToPivotal"><span>Upload to Pivotal</span></button>
	</div>
	</form>

</asp:Content>
