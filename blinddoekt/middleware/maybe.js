+function () {
    'use strict';

    var util = require('util')

    function Maybe () {}

    Maybe.prototype.chain = function chain (fn) {
        if (this.value) return fn(this.value)

        return new Nothing()
    }

    function Just (value) {
        this.value = value
    }

    function Nothing () {}

    util.inherits(Just, Maybe)
    util.inherits(Nothing, Maybe)

    function pure (value) {
        return new Just(value)
    }

    function fromList (lst) {
        return lst.length > 0 ? pure(lst[0]) : new Nothing()
    }

    function just (value) { return new Just(value) }
    function nothing () { return new Nothing() }

    module.exports = {
        just: just,
        nothing: nothing,
        pure: pure,
        fromList: fromList
    }
} ()
