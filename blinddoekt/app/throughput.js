+function () {
    'use strict';
    var lang = require('../app/lang')
      , FormattedError = require('.../app/error')
      , timingCache = {}

    function restrict (app) {
        return function (req, res, next) {
            var team = lang.first(app.findTeamByIp(req.ip))
              , now = new Date()
              , previous
              , diff

            if (!team) next()
            else {
                previous = timingCache[team.name]
                timingCache[team.name] = now

                if (!previous) next()
                else {
                    diff = now - previous
                    if (diff > oneMinute) {
                        res.status(420).send(new FormattedError('rate limit exceeded'))
                    } else {
                        next()
                    }
                }
            }
        }
    }

    module.exports = restrict
}()
