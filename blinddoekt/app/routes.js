+function () {
    'use strict';

    function configure (server, app) {
        server.get('/:hash/view', function (req, res) {
            var result = app.getView(req.params.hash)

            return res.status(200).send(result)
        });

        server.put('/:hash/step', function (req, res) {
            res.status(500).send({ error: 'incorrect pathing for steps' })
        });

        server.put('/register/:name', function (req, res) {
            var result = app.registerTeam(req.params.name, req.ip)

            res.status(200).send(result)
        });

        server.get('/whoami/:name', function (req, res) {
            var result = app.whoAmI(req.params.name, req.ip)

            res.status(200).send(result)
        })
    }

    module.exports = { configure: configure };
}();
