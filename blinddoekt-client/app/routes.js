+function () {
    'use strict';

    function ok (res, data) {
        res.status(200).send(JSON.stringify(data))
    }

    function error (res, err) {
        res.status(500).send(JSON.stringify(err))
    }

    function echo (res, promise) {
        promise
            .then(function (data) { ok (res, data) })
            .catch(function (err) { error(res, err) })
    }

    function configure (server, application) {
        server.get('/', function (req, res) {
            res.status(200).send('hello, i\'m a server')
        })

        server.get('/register', function (req, res) {
            echo(res, application.register())
        })

        server.get('/whoami', function (req, res) {
            echo(res, application.whoami())
        })

        server.get('/view', function (req, res) {
            echo(res, application.view())
        })

        server.get('/step', function (req, res) {
            echo(res, application.step())
        })
    }

    module.exports = { configure: configure }
}()
