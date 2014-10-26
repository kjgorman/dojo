+function () {
    'use strict';

    var spanning = require('./spanning')
      , lang     = require('./lang')

    function Map (cells) {
        this.cells = cells
    }

    Map.prototype.toString = function toString () {
        var row, col, line, str = ''
        for (row = 0; row < this.cells.length; row++) {
            line = 'row: ' + row + '|'
            for (col = 0; col < this.cells[row].length; col++) {
                line += this.cells[row][col]
            }
            str += (line + '\n')
        }
        return str
    }

    function generateCell (row, col, width, height) {
        if (col === 0 || col === width - 1) return 1

        return Math.random() < ((row / height) * 0.5) ? 1 : 0
    }

    function generateMap (width, height) {
        var cells = [], row, col, line
        for (row = 0; row < height; row++) {
            line = []
            for(col = 0; col < width; col++) {
                line.push(generateCell(row, col, width, height))
            }
            cells.push(line)
        }

        return new Map(cells)
    }

    function generate (width, height) {
        var map

        do {
            map = generateMap(width, height)
        } while(!spanning(map))

        return map
    }

    function encode (map) {
        var width = map.cells[0].length
          , chunks = width/32

        if (width % 32 !== 0)
            throw new Error('map must have a width that is a multiple of 32 (got: '+width+')')

        return lang.flatMap(map.cells)(function (row) {
            var i = 0, res = []
            for(;i < chunks; i++)
                res.push(row.slice(i * 32, (i+1)*32))
            return res
        }).map(function (chunk) {
            return parseInt(chunk.join(''), 2)
        })
    }

    module.exports = { generate: generate, encode: encode }
}()
