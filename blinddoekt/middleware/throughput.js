+function () {
    'use strict';
    var lang           = require('../app/lang')
      , FormattedError = require('../app/error')
      , Maybe          = require('./maybe')
      , timingCache    = {}
      , rateLimit

    function restrict (app, config) {
	rateLimit = config.rateLimit

        return function (req, res, next) {
            var team = Maybe.fromList(app.findTeamByIp(req.ip))
              , now = new Date()
              , previous
              , diff

            var maybeNext = team.chain(function (t) {
                    var previous = timingCache[t.name]
                    timingCache[t.name] = now

                    return previous !== undefined
                    ? Maybe.just(previous)
                    : Maybe.nothing()
                })
                .chain(function (p) {
                    diff = now - p

                    return diff < rateLimit
                        ? Maybe.just(function () {
                            res.status(420).send(new FormattedError('rate limit exceeded'))
                        })
                        : Maybe.nothing()
                })

            if (maybeNext.value) maybeNext.value()
            else next()
        }
    }

    module.exports = restrict
}()
