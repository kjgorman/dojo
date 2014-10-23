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

    Team.prototype.allocateMember = function allocateMember () {
	if (false === this.hasDriver()) {
	    this.assignDriver()
	    return { identifier: this.driverHash, role: 'driver' }
	} else if (false === this.hasNavigator()) {
	    this.assignNavigator()
	    return { identifier: this.navigatorHash, role: 'navigator' }
	}

	return { error: 'This team already has a driver and navigator!' }
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

    module.exports = Team;
}()
