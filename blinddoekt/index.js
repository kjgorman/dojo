var chalk     = require('chalk')
  , express   = require('express')
  , logging   = require('./app/logging')
  , routes    = require('./app/routes')
  , teams     = require('./app/team')
  , blindkoet = require('./app/app')
  , server    = express()
  , app       = new blindkoet(routes, teams)

server.use(logging)
app.configure(server)

server.listen(3001, function () {
    console.log(chalk.cyan('up on 3001'))
});
