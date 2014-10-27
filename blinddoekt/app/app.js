+function () {
    'use strict';

    var lang           = require('./lang')
      , FormattedError = require('./error')

    var App = function (routes, team, mapping, config) {
        this.teams = []
        this.routes = routes
        this.team = team
        this.mapping = mapping
        this.map = mapping.generate(64, 100)
	this.rateLimit = config.rateLimit
    }

    App.prototype.configure = function configure (server) {
        this.routes.configure(server, this)
    }

    App.prototype.registerTeam = function registerTeam (teamName, callingIp) {
        var current = lang.first(this.teams.filter(lang.not(function (t) {
            return t.name !== teamName
        })))

        if (!current) {
            this.teams.push(current = new this.team(teamName))
        }

        return current.allocateMember(callingIp)
    }

    App.prototype.whoAmI = function whoAmI (callingIp) {
        return this.team.findMemberById(this.teams, callingIp)
    }

    App.prototype.getView = function getView (navHash) {
        var current = this.team.findByNav(this.teams, navHash), slice

        if (!current) return new FormattedError('you are not the navigator')

	slice = this.map.slice(current.location)

        return {
	    lower: slice.lower,
	    upper: slice.upper,
	    encoding: this.mapping.encode(slice),
	    location: current.location
	}
    }

    App.prototype.applySteps = function applySteps (hash, steps) {
        var current = this.team.findByDriver(this.teams, hash), res

        if(!current) return new FormattedError('you are not the driver')

        res = this.map.traverse(current.location, steps)
        current.location = res.position

        if (current.location.row === this.map.cells.length) {
            return {
                success: 'success! you reached the end... sorry there\'s no prize tho'
            }
        }

        return res
    }

    App.prototype.getLocation = function getLocation (navHash) {
        var current = this.team.findByNav(this.teams, navHash)

        if (!current) return new FormattedError('you are not the navigator')

        return current.location
    }

    App.prototype.findTeamByIp = function findTeamByIp (ip) {
        return lang.pluck('team')(this
                                  .team
                                  .findMemberById(this.teams, ip))
    }

    module.exports = App
}();
