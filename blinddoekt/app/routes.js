+function () {
    'use strict';

    function configure (server, app) {
        server.get('/:hash/view', function (req, res) {
            var result = app.getView(req.params.hash)

            return res.status(200).send(result)
        });

        server.get('/:hash/whereami', function (req, res) {
            var result = app.getLocation(req.params.hash)

            return res.status(200).send(result)
        })

        server.put('/:hash/step', function (req, res) {
            var result = app.applySteps(req.params.hash, req.body)

            return res.status(200).send(result)
        });

        server.put('/register/:name', function (req, res) {
            var result = app.registerTeam(req.params.name, req.ip)

            res.status(200).send(result)
        });

        server.get('/whoami/:name', function (req, res) {
            var result = app.whoAmI(req.ip)

            res.status(200).send(result)
        })

	server.get('/debug/rate/:amount', function (req, res) {
	    app.rateLimit = parseInt(req.params.amount, 10)

	    res.status(200).send('rate limit updated to '+app.rateLimit)
	})
    }

    module.exports = { configure: configure };
}();
