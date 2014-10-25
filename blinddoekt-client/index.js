+function () {
    'use strict';

    var register = require('./client/client')
      , chalk    = require('chalk')
      , express  = require('express')
      , server   = express()


    server.listen(3002, function () {
        console.log(chalk.cyan('client server up on 3002'))
    })
}()
