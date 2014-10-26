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
            var result = app.applySteps(req.body)

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
    }

    module.exports = { configure: configure };
}();
