+function () {
    'use strict';

    module.exports = {
	not: function (fn) {
	    return function () {
		return !fn.apply(this, arguments)
	    }
	},
	first: function (lst) {
	    return lst[0]
	}
    }
}()
