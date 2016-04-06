$(function() {
    
}
)
var topicName = $(".createtopicname");
if (topicName.length > 0) {
    topicName.focusout(function() {
        var tbValue = $.trim(topicName.val());
        var length = tbValue.length;
        if (length >= 4) {
            $.ajax({
                url: '@Url.Content("~/")' + 'Topic/GetSimilarTopics',
                type: 'POST',
                datatype: 'html',
                data: { 'searchTerm': tbValue },
                success: function(data) {
                    if (data != '') {
                        $('.relatedtopicskey').html(data);
                        $('.relatedtopicsholder').show();

                    }
                },
                error: function(xhr, ajacOptions, thrownError) {
                    ShowUserMessage
                }
            })
        }
    })
}