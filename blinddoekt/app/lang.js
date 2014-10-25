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
	},
        zip: function (fst, snd, zipper) {
            return (fst.length < snd.length ? snd : fst)
                .map(function (_, idx) {
                    return zipper(fst[idx], snd[idx])
                })
        },
        flatMap: function (lst) {
            return function (fn) {
                return Array.prototype.concat.apply([], lst.map(fn))
            }
        },
        id: function (x) { return x }
    }
}()
