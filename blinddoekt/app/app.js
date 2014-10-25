+function () {
    'use strict';

    var lang = require('./lang')
      , error = require('./error')

    var App = function (routes, team) {
	this.teams = []
	this.routes = routes
	this.team = team
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

    App.prototype.whoAmI = function whoAmI (teamName, callingIp) {
        return this.team.findMemberById(this.teams, teamName, callingIp)
    }

    App.prototype.getView = function getView (navHash) {
	var current = this.team.findByNav(this.teams, navHash)

        if (!current) return new FormattedError('you are not the navigator')
    }

    module.exports = App
}();
