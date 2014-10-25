+function () {
    'use strict';

    var register = require('./client/client')
      , chalk    = require('chalk')
      , express  = require('express')
      , App      = require('./app/app')
      , routes   = require('./app/routes')
      , client   = new App(register, routes)
      , server   = express()

    client.configure(server)

    server.listen(3002, function () {
        console.log(chalk.cyan('client server up on 3002'))
    })
}()
