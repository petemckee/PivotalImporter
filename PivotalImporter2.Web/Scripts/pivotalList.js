
var pivotalList = {
	listEl: null
	, stories: null
	, Init: function () {

		var me = this;
		me.listEl = $('.storyList');

		if (me.listEl.length > 0) {

			me.listEl.find('.unDel').hide();

			$('.btnUploadToPivotal').click(function (e) {
				e.preventDefault();
				me.save();
			});

			$('.summary .expand').click(function (e) {
				e.preventDefault();
				if (!$(this).closest('div.story').hasClass('deletedItem')) {
					me.expandItem($(this).closest('.story'));
				}
			});
			$('.detail .hide').click(function (e) {
				e.preventDefault();
				me.hideItem($(this).closest('.story'));
			});

			$('.del').click(function (e) {
				e.preventDefault();
				var story = $(this).closest('.story');
				story.addClass('deletedItem');
				me.hideItem(story);
				me.updateNoOfItems();
				$(this).hide();
				story.find('.unDel').show();
			});
			$('.unDel').click(function (e) {
				e.preventDefault();
				var story = $(this).closest('.story');
				story.removeClass('deletedItem');
				me.updateNoOfItems();
				$(this).hide();
				story.find('.del').show();
			});

			$('.detail input[name="estimate"]').blur(function () {
				me.updateEstimate($(this));
			});
		}

		me.listEl.find('.story .detail').hide();
	}
	, updateNoOfItems: function () {
		$('.storyCount').text(this.listEl.find('.story:not(.deletedItem)').length);
	}
	, updateEstimate: function (estEl) {
		var me = this;
		var estimate = $(estEl).val();
		var estSpan = $(estEl).closest('.story').find('.summary .estimate span');
		$(estSpan).attr('class', '');
		$(estSpan).addClass('e' + estimate);
	}
	, save: function () {
		var me = this;

	    var defaultName = 'None';
		me.stories = new Array();
		me.listEl.find('.story:not(.deletedItem)').each(function (index, value) {

			var detailWrapper = $(this).find('.detail');

			var labelsVal = $(this).find('input[name="labels"]').val();
			var labels = labelsVal.split(', ');

			var tasks = new Array();
			$(this).find('.tasks li').each(function (index, value) {
				tasks.push($(this).text());
			});

		    var requestor = $(this).find('select[name="requestor"]').val();
		    var owner = $(this).find('select[name="owner"]').val();
		    
			var s = {
				Name: detailWrapper.find('input[name="name"]').val()
				, Estimate: $(this).find('input[name="estimate"]').val()
				, Description: detailWrapper.find('textarea[name="description"]').val()
				, Requestor: (requestor == defaultName) ? '' : requestor
				, Owner: (owner == defaultName) ? '' : owner
				, LabelValues: labels
				, Tasks: tasks
				, CurrentState: 'Unscheduled'
				, CurrentStateValue: 'Unscheduled'
			};

			me.stories.push(s);
		});


		me.publish();

	}
	, publish: function () {
		var me = this;
		var projectId = $('#projectId').val();

		var jsonData = {
			upload: true
			, projectId: projectId
			, stories: me.stories
		}

	    // add generic labels
	    var genericLabels = [];
	    var genericLabelTxt = $('input[name="genericLabels"]').val();
	    // TODO ! Use lodash for this (and trim spaces)
	    if (genericLabelTxt.length > 0) {
	        genericLabels = genericLabelTxt.split(',');
	    }
	    
	    for (var s in me.stories)
	    {
	        //. use lodash rather than jquery?
	        me.stories[s].LabelValues = me.stories[s].LabelValues.concat(genericLabels);
	    }
	    
		$.ajax("/upload/publish", {
			dataType: 'json'
			, type: 'POST'
			, data: JSON.stringify(jsonData, null, 2)
			, contentType: 'application/json; charset=utf-8'
		})
		.done(function (data, textStatus, jqXHR) {
			// insert form and submit;
			var pId = data.ProjectId;
			var formId = 'form-' + pId;
			$('body').append('<form action="/upload/success" id="' + formId + '"><input type="hidden" name="projectId" value="' + pId + '" /></form>');
			$('#' + formId).submit();

		})
		.fail(function (jqXHR, textStatus, errorThrown) {
			alert("error");
		});

	}
	, expandItem: function (story) {
		$(story).find('.summary').hide();
		$(story).find('.detail').show();
	}
	, hideItem: function (story) {
		$(story).find('.detail').hide();
		$(story).find('.summary').show();
	}
}

$(function () {
	pivotalList.Init();
});
