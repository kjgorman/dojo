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
            if (client.view) return client.view()
            else invalidOperation(true)
        })
    }

    App.prototype.step = function step () {
        return this.client(api, 'foo').then(function (client) {
            if (client.step) return client.step([])
            else invalidOperation(false)
        })
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
