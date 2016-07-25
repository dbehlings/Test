$(function () {

	var hub = $.connection.systemMessagesHub;




	hub.client.newMessage = function (messages) {
		for (var i = 0; i < messages.length; i++) {
			console.log(messages[i]);
		}
	};


	$.connection.hub.start()
		.done(function () {
			console.log('Now connected, connection ID=' + $.connection.id);






		})
		.fail(function (error) {
			console.log("failed to connect." + error);
		});

});