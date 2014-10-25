+function () {
    'use strict';

    function log (req, res, next) {
        console.log('[%s | %s | %s] %s', new Date(), req.method, req.ip, req.url)
        next()
    }

    module.exports = log
}()
