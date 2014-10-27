var chalk     = require('chalk')
  , express   = require('express')
  , parser    = require('body-parser')
  , config    = require('./' + (process.argv[2] || 'dev.json'))
  , logging   = require('./middleware/logging')
  , rateLimit = require('./middleware/throughput')
  , mapping   = require('./app/mapping')
  , routes    = require('./app/routes')
  , teams     = require('./app/team')
  , blindkoet = require('./app/app')
  , server    = express()
  , app       = new blindkoet(routes, teams, mapping)

server.use(parser.json())
server.use(logging)
server.use(rateLimit(app, config))

app.configure(server)

server.listen(3001, function () {
    console.log(chalk.cyan('up on 3001'))
});
