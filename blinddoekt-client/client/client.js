+function () {
  'use strict';

    var rp = require('request-promise')
      , util = require('util')

    function Client (base, team) {
        this.apiBase = base
        this.team = team
    }

    Client.prototype.whoAmI = function whoAmiI () {
        return rp({
            uri: this.apiBase + '/whoami/' + this.team,
            method: 'GET'
        })
    }

    function Navigator (hash, team, base) {
        Navigator.super_.call(this, base, team)
        this.hash = hash

        this.view = function view () {
            return rp({
                uri: base + '/' + this.hash + '/view',
                method: 'GET'
            })
        }

        this.whereAmI = function whereAmI () {
            return rp({
                uri: base + '/' + this.hash + '/whereami',
                method: 'GET'
            })
        }
    }

    function Driver (hash, team, base) {
        Driver.super_.call(this, base, team)
        this.hash = hash

        this.step = function step (steps) {
            return rp({
                uri: base + '/' + this.hash + '/step',
                method: 'PUT'
            })
        }
    }

    util.inherits(Driver, Client)
    util.inherits(Navigator, Client)

    var _cached = {}

    module.exports = function (base, team) {
        if (_cached[team]) return _cached[team]

        return _cached[team] = rp({
            uri: base + '/register/' + team,
            method: 'PUT'
        }).then(function (response) {
            var membership = JSON.parse(response)
              , Ctor = (membership.role === 'driver' ? Driver : Navigator)

            return new Ctor(membership.identifier, team, base)
        })
    }
}()
