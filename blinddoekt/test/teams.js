var assert = require('assert')
  , team   = require('../app/team')

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
	  , res = t.allocateMember()
	
	hasAllocatedId(res)
    })

    it('should allow you to allocate two members to the same team', function () {
	var t = new team('foo')
	  , first = t.allocateMember()
	  , second = t.allocateMember()

	hasAllocatedId(first)
	hasAllocatedId(second)
	assert.notEqual(first.identifier, second.identifier)
    })

    it('should allocate the first applicant the driver role', function () {
	var t = new team('foo')
	  , first = t.allocateMember()

	assert.equal(first.role, 'driver')
    })

    it('should allocate the second applicant the navigator role', function () {
	var t = new team('foo')
	  , first = t.allocateMember()
	  , second = t.allocateMember()

	assert.equal(second.role, 'navigator')
    })

    it('should be an error to allocate three members to the same team', function () {
	var t = new team('foo')
	  , first  = t.allocateMember()
	  , second = t.allocateMember()
	  , third  = t.allocateMember()

	assert(third.error, 'expected an error but got <'+JSON.stringify(third)+'>')
    })

    it('should allocate different identifiers to different teams', function () {
	var foo = new team('foo')
	  , bar = new team('bar')
	  , first = foo.allocateMember()
	  , second = bar.allocateMember()

	hasAllocatedId(first)
	hasAllocatedId(second)
	assert.notEqual(first.identifier, second.identifier)
    })
})

describe('finding a team by its members', function () {
    function allocateEach (ts) {
	return ts.map(function (t) {
	    return t.allocateMember()
	})
    }

    it('should be able to locate by navigator', function () {
	var a = new team('a')
	  , b = new team('b')
	  , c = new team('c')
	  , teams = [a, b, c]
	  , nav

	allocateEach(teams)
	nav = a.allocateMember().identifier

	assert.equal(a, team.findByNav(teams, nav))
    })

    it('should be able to locate by driver', function () {
	var a = new team('a')
	  , b = new team('b')
	  , c = new team('c')
	  , teams = [a, b, c]
	  , driver

	driver = a.allocateMember().identifier
	allocateEach(teams)

	assert.equal(a, team.findByDriver(teams, driver))
    })

    it('should return undefined if no such team exists', function () {
	var a = new team('a')
	
	a.allocateMember()

	assert.equal(undefined, team.findByDriver([a], 'clearly not a hash'))
    })

    it('should return undefined if there wasn\'t even an allocation', function () {
	var a = new team('a')

	assert.equal(undefined, team.findByDriver([a], ''))
    })
})
