+function () {
    'use strict';

    var api = 'http://localhost:3001'

    function App (clientRegistration, routes) {
        this.client = clientRegistration
        this.routes = routes
    }

    App.prototype.configure = function configure (server) {
        this.routes.configure(server, this)
    }

    App.prototype.register = function register () {
        return this.client(api, 'foo')
    }

    App.prototype.whoami = function whoami () {
        return this.client(api, 'foo').then(function (client) {
            return client.whoAmI()
        })
    }

    App.prototype.view = function view () {
        return this.client(api, 'foo').then(function (client) {
            return client.view()
        })
    }

    App.prototype.step = function step () {
        return this.client(api, 'foo').then(function (client) {
            return client.step([])
        })
    }

    module.exports = App
}()
