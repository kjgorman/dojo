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

describe('encoding', function () {
    it('should reject anything that isn\'t a multiple of 32 bits wide', function () {
        var m = mapping.generate(33, 10)

        assert.throws(function () {
            mapping.encode(m)
        })
    })

    it('should convert a 32 wide row into it\'s integer representation', function () {
        var m = { cells: [[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1]] }

        assert.equal(Math.pow(2, 31) + 1, mapping.encode(m)[0])
    })

    it('should convert the 2d array to a row major 1d array', function () {
        var m = { cells: [[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1]
                        , [1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1]] }

        assert.equal(Math.pow(2, 31) + 1, mapping.encode(m)[0])
        assert.equal(Math.pow(2, 31) + 3, mapping.encode(m)[1])
    })

    it('should convert a 64 bit row into two integers', function () {
        var i = 1, m = { cells: [] }, row = []

        row.push(1)
        for(; i < 63; i++)
            row.push(0)
        row.push(1)

        m.cells.push(row)

        assert.equal(2, mapping.encode(m).length)
        assert.equal(Math.pow(2, 31), mapping.encode(m)[0])
    })
})
