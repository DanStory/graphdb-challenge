'use strict';

var port = process.env.PORT || 1337;

var http = require('http');

var finalhandler = require('finalhandler');
var serveStatic = require('serve-static');

var serve = serveStatic("./wwwroot");

var config = {
	apiUrl: process.env.APIURL || ""
}

var server = http.createServer(function (request, response) {
	if(request.url.toLowerCase()=='/config.json'){
		response.writeHead(200, { 'Content-Type': 'application/json'});
		response.write(JSON.stringify(config));
		response.end();
		return;
	}

	var done = finalhandler(request, response);
	serve(request, response, done);
});

server.listen(port);
console.log(`Running on TCP:${port}`);