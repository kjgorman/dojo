var assert   = require('assert')
  , mapping  = require('../app/mapping')
  , spanning = require('../app/spanning')

describe('simple map generation', function () {
    it('should produce a map of the specified dimensions', function () {
        var m = mapping.generate(5, 5)

        assert.equal(5, m.cells.length)
        assert.equal(5, m.cells[0].length)
    })

    it('should produce walls', function () {
        var m = mapping.generate(5, 5)

        // i.e. 10001
        assert.equal(1, m.cells[0][0])
        assert.equal(1, m.cells[0][4])
    })
})

describe('spanning operations', function () {
    it('should be able to traverse a very simple map', function () {
        var map = { cells: [[1, 0, 0, 1]
                          , [1, 0, 0, 1]
                          , [1, 0, 0, 1]] }

        assert.ok(spanning(map))
    })

    it('should not be able to traverse a map that is completely blocked', function () {
        var map = { cells: [[1, 0, 0, 1]
                          , [1, 1, 1, 1]
                          , [1, 0, 0, 1]] }

        assert.equal(false, spanning(map))
    })

    it('should be able to traverse a map that is partially blocked', function () {
        var map = { cells: [[1, 0, 0, 1]
                          , [1, 1, 0, 1]
                          , [1, 0, 0, 1]] }

        assert.ok(spanning(map))
    })

    it('should not be able to move diagonally', function () {
        var map = { cells: [[1, 0, 0, 1]
                          , [1, 1, 0, 1]
                          , [1, 0, 1, 1]] }

        assert.equal(false, spanning(map))
    })
})
