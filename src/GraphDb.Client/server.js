'use strict';

var port = process.env.PORT || 1337;

var http = require('http');

var finalhandler = require('finalhandler');
var serveStatic = require('serve-static');

var serve = serveStatic("./wwwroot");

var server = http.createServer(function (req, res) {
	var done = finalhandler(req, res);
	serve(req, res, done);
});

server.listen(port);