var chalk     = require('chalk')
  , express   = require('express')
  , routes    = require('./app/routes')
  , teams     = require('./app/team')
  , blindkoet = require('./app/app')
  , server    = express()
  , app       = new blindkoet(routes, teams)

app.configure(server)

server.listen(3001, function () {
    console.log(chalk.cyan('up on 3001'))
});
