var chalk     = require('chalk')
  , express   = require('express')
  , logging   = require('./middleware/logging')
  , rateLimit = require('./middleware/throughput')
  , routes    = require('./app/routes')
  , teams     = require('./app/team')
  , blindkoet = require('./app/app')
  , server    = express()
  , app       = new blindkoet(routes, teams)

server.use(logging)
server.use(rateLimit(app))

app.configure(server)

server.listen(3001, function () {
    console.log(chalk.cyan('up on 3001'))
});
