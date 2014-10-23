+function () {
    'use strict';

    function configure (server, app) {
        server.get('/:hash/view', function (req, res) {
            var result = app.getView(req.params.hash)

            return res.status(200).send(result)
        });

        server.put('/:hash/step', function (req, res) {
            
        });

        server.put('/register/:name', function (req, res) {
            var result = app.registerTeam(req.params.name)
            
            res.status(200).send(result)
        });
    }

    module.exports = { configure: configure };
}();
