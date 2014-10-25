var assert = require('assert')
  , team   = require('../app/team')
  , lang   = require('../app/lang')

var firstIp = '127.0.0.1', secondIp = '127.0.0.2', thirdIp = '127.0.0.3'

describe('teams need names', function () {
    it('should throw when we don\'t provide one', function () {
	assert.throws(function () {
	    new team()
	}, Error)
    })
})

describe('adding new members to a team', function () {
    function hasAllocatedId (response) {
	assert(response.identifier, 'expected to be given member id but got <'+JSON.stringify(response)+'>')
    }

    it('should allow you to allocate a member', function () {
	var t = new team('foo')
	  , res = t.allocateMember(firstIp)

	hasAllocatedId(res)
    })

    it('should add the allocated member to the registry cache', function () {
        var t = new team('foo')
          , res = t.allocateMember(firstIp)

        assert.equal(res, t.registry[firstIp])
        assert.equal(1, Object.keys(t.registry).length)
    })

    it('should allow you to allocate two members to the same team', function () {
	var t = new team('foo')
	  , first = t.allocateMember(firstIp)
	  , second = t.allocateMember(secondIp)

	hasAllocatedId(first)
	hasAllocatedId(second)

	assert.notEqual(first.identifier, second.identifier)
    })

    it('should allocate the first applicant the driver role', function () {
	var t = new team('foo')
	  , first = t.allocateMember(firstIp)

	assert.equal(first.role, 'driver')
    })

    it('should allocate the second applicant the navigator role', function () {
	var t = new team('foo')
	  , first = t.allocateMember(firstIp)
	  , second = t.allocateMember(secondIp)

	assert.equal(second.role, 'navigator')
    })

    it('should be an error to allocate three members to the same team', function () {
	var t = new team('foo')
	  , first  = t.allocateMember(firstIp)
	  , second = t.allocateMember(secondIp)
	  , third  = t.allocateMember(thirdIp)

	assert(third.error, 'expected an error but got <'+JSON.stringify(third)+'>')
    })

    it('should allocate different identifiers to different teams', function () {
	var foo = new team('foo')
	  , bar = new team('bar')
	  , first = foo.allocateMember(firstIp)
	  , second = bar.allocateMember(secondIp)

	hasAllocatedId(first)
	hasAllocatedId(second)
	assert.notEqual(first.identifier, second.identifier)
    })
})

describe('finding a team by its members', function () {
    function allocateEach (ts, ips) {
        return lang.zip(ts, ips, function (t, ip) {
            return { team: t, ip: ip }
        }).map(function (pair) {
	    return pair.team.allocateMember(pair.ip)
	})
    }

    it('should be able to locate by navigator', function () {
	var a = new team('a')
	  , b = new team('b')
	  , c = new team('c')
	  , teams = [a, b, c]
          , ips   = [firstIp, secondIp, thirdIp]
	  , nav

	allocateEach(teams, ips)
	nav = a.allocateMember(secondIp).identifier

	assert.equal(a, team.findByNav(teams, nav))
    })

    it('should be able to locate by driver', function () {
	var a = new team('a')
	  , b = new team('b')
	  , c = new team('c')
	  , teams = [a, b, c]
          , ips = [secondIp, thirdIp, firstIp]
	  , driver

	driver = a.allocateMember(firstIp)
	allocateEach(teams, ips)

        assert.equal(a.driverHash, driver.identifier)
	assert.equal(a, team.findByDriver(teams, driver.identifier))
    })

    it('should return undefined if no such team exists', function () {
	var a = new team('a')

	a.allocateMember(firstIp)

	assert.equal(undefined, team.findByDriver([a], 'clearly not a hash'))
    })

    it('should return undefined if there wasn\'t even an allocation', function () {
	var a = new team('a')

	assert.equal(undefined, team.findByDriver([a], ''))
    })
})

describe('asking for allocation when we\'ve already allocated yr ip', function () {
    it('should be able to tell you what your hash is if you register for the same team from'+
       'the same ip address', function () {
           var a = new team('a')
             , id = a.allocateMember(firstIp).identifier

           assert.equal(id, a.allocateMember(firstIp).identifier)
       })

    it('should still yet you register for a different team though', function () {
        var a = new team('a')
          , b = new team('b')
          , first = a.allocateMember(firstIp)
          , second = b.allocateMember(firstIp)

        assert.notEqual(first.identifier, second.identifier)
    })
})

describe('finding out what your role is in a team by sending yr ip', function () {
    it('should be able to find yr role', function () {
        var a = new team('a')
          , b = new team('b')
          , first = a.allocateMember(firstIp)

        assert.deepEqual([ { ip: firstIp, membership: first }]
                       , team.findMemberById([a, b], 'a', firstIp))
    })
})
