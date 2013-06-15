<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="PivotalTrackerAPI.Domain.Enumerations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h1>Test story import...</h1>

	<a href="#" class="click">Click to test post</a>

<script type="text/javascript">
	$(function () {

		$('.click').click(function (e) {
			e.preventDefault();

			var storiesArray = new Array();
			storiesArray.push({ "Name": "test1", "Description": "testr desc" });
			storiesArray.push({ "Name": "test2", "Description": "testr 222 desc" });

			var json = {
				stories: storiesArray
				, projectId: 123456
			}

			$.ajax("/home/teststory", {
				dataType: 'json'
				, type: 'POST'
				, data: JSON.stringify(json, null, 2)
				, contentType: 'application/json; charset=utf-8',
			})
		.done(function (data, textStatus, jqXHR) {
			//alert("success");
			console.log('Success');
			//jQuery.parseJSON()
		})

		});

	});
</script>

</asp:Content>
