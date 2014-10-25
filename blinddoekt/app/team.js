+function () {
    'use strict';

    var md5  = require('MD5')
      , lang = require('./lang')

    function Team (name) {
	if (!name) {
	    throw new Error('A team name is required')
	}

	var assignedDriver = false
	  , assignedNavigator = false

	this.name = name
        this.registry = {}

	this.driverHash = md5(name + 'driver')
	this.navigatorHash = md5(name + 'navigator')

	this.hasDriver = function () {
	    return assignedDriver
	}

	this.hasNavigator = function () {
	    return assignedNavigator
	}

	this.assignDriver = function () {
	    assignedDriver = true
	}

	this.assignNavigator = function () {
	    assignedNavigator = true
	}
    }

    Team.prototype.tryAllocate = function tryAllocate () {
        if (false === this.hasDriver()) {
	    this.assignDriver()
	    return { identifier: this.driverHash, role: 'driver' }
	} else if (false === this.hasNavigator()) {
	    this.assignNavigator()
	    return { identifier: this.navigatorHash, role: 'navigator' }
	}

	return { error: 'This team already has a driver and navigator!' }
    }

    Team.prototype.allocateMember = function allocateMember (callingIp) {
        var team

        if (this.registry[callingIp]) {
            return this.registry[callingIp]
        }

        team = this.tryAllocate()
        this.registry[callingIp] = team
        return team
    }

    function findByHash(teams, hash, key) {
	return lang.first(teams.filter(function (t) {
	    return t[key] === hash
	}))
    }

    Team.findByNav = function findByNav (teams, hash) {
	return findByHash(teams, hash, 'navigatorHash')
    }

    Team.findByDriver = function findByDriver (teams, hash) {
	return findByHash(teams, hash, 'driverHash')
    }

    Team.findMemberById = function findMemberById (teams, teamName, callingIp) {
        return lang.first(lang.flatMap(teams.filter(function (t) {
            return t.name === teamName
        }))(function (t) {
            return Object.keys(t.registry).map(function (k) {
                return { ip: k, membership: t.registry[k] }
            })
        }).filter(function (m) {
            return m.ip === callingIp
        })).membership
    }

    module.exports = Team;
}()
