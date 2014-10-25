var assert = require('assert')
  , Maybe  = require('../middleware/maybe')

describe('maybe constructors', function () {
    it('should take a value and wrap it in a Just', function () {
        var just = Maybe.pure(5)

        assert.equal(5, just.value)
    })

    it('should take the empty list to a nothing', function () {
        var nothing = Maybe.fromList([])

        assert.equal(undefined, nothing.value)
    })

    it('should take a singleton list to a just', function () {
        var just = Maybe.fromList([5])

        assert.equal(5, just.value)
    })

    it('should take a longer list to a just of the first element, ignoring the rest', function () {
        var just = Maybe.fromList([5,4,3,2,1])

        assert.equal(5, just.value)
    })
})

describe('chaining is a monadic bind', function () {
    it('should satisfy left identity', function () {
        var lhs = Maybe.pure(5).chain(function (value) { return Maybe.pure(value * 2) })

        assert.equal(5 * 2, lhs.value)
    })

    it('should satisfy right identity', function () {
        var lhs = Maybe.pure(5)

        assert.equal(lhs.value, lhs.chain(Maybe.pure).value)
    })

    it('should satisfy associativity', function () {
        var lhs = Maybe
            .pure(5)
            .chain(function (v) { return Maybe.pure(v * 2)})
            .chain(function (v) { return Maybe.pure(v * 2)})
          , rhs = Maybe
            .pure(5)
            .chain(function (v) {
                return Maybe.pure(v * 2).chain(function (w) {
                    return Maybe.pure(w * 2)
                })
            })

        assert.equal(lhs.value, rhs.value)
    })
})
