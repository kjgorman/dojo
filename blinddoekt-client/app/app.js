+function () {
    'use strict';

    function App (clientRegistration, routes, config) {
        this.client = clientRegistration
        this.routes = routes

        this.api = config.apiUri
        this.team = config.team
        this.teamMate = config.teamMateUri
    }

    App.prototype.configure = function configure (server) {
        this.routes.configure(server, this)
    }

    App.prototype.register = function register () {
        return this.client(this.api, this.team)
    }

    App.prototype.whereami = function whereami () {
        return this.client(this.api, this.team).then(function (client) {
            return client.whereAmI()
        })
    }

    App.prototype.whoami = function whoami () {
        return this.client(this.api, this.team).then(function (client) {
            return client.whoAmI()
        })
    }

    App.prototype.view = function view () {
        return this.client(this.api, this.team).then(function (client) {
            if (client.view) return client.view()
            else invalidOperation(true)
        })
    }

    App.prototype.step = function step () {
        return this.client(this.api, this.team).then(function (client) {
            if (client.step) return client.step([1,2,3,4,5,6,7,8,9,10].map(function (r) {
                return { row: r, col: 32 }
            }))
            else invalidOperation(false)
        })
    }

    App.prototype.ping = function ping () {
        return this.client(this.teamMate, 'buddy').ping()
    }

    function invalidOperation (view) {
        var viewing = 'you\'re currently the driver, so can\'t view anything!'
          , stepping = 'you\'re currently the navigator, so can\'t move anything!'

        throw new Error((view ? viewing : stepping) +
                        '\n'+
                        'it\'s called blinddoekt for a reason')
    }

    module.exports = App
}()
