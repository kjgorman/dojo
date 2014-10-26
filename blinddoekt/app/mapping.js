+function () {
    'use strict';

    var spanning = require('./spanning')

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

    module.exports = { generate: generate }
}()
