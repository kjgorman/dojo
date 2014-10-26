+function () {
    'use strict';

    // bfs to ensure a path exists in the map
    function hasPath(map) {
        var queue = []
          , set   = lookup()
          , cells = map.cells
          , col   = 1
          , current

        for(; col < cells[0].length - 1; col++) {
            queue.push({ row: 0, col: col})
            set(0, col)
        }

        function markUnvisited (adjacent) {
            return !set(adjacent.row, adjacent.col)
        }

        while (queue.length > 0) {
            current = queue.shift()
            if (current.row === cells.length - 1)
                return true

            queue.push.apply(queue, adjacentTo(current, cells).filter(markUnvisited))
        }
        return false
    }


    function adjacentTo(point, cells) {
        var adj = []
        //above
        if (point.row - 1 > 0)
            adj.push({row: point.row-1, col: point.col})
        //left
        if (point.col - 1 > 1)
            adj.push({row: point.row, col: point.col - 1})
        //right
        if (point.col + 1 < (cells[0].length -1))
            adj.push({row: point.row, col: point.col + 1})
        //below
        if (point.row + 1 < cells.length)
            adj.push({row: point.row + 1, col: point.col })

        return adj.filter(function (p) {
            return cells[p.row][p.col] !== 1
        })
    }

    function lookup() {
        var _lookup = {}
        return function (row, col) {
            // if we haven't visited this row create
            // the row lookup
            if (!_lookup[row]) _lookup[row] = {}
            // if we haven't seen this cell, mark it
            // as seen and return false for membership
            if (!_lookup[row][col]) {
                _lookup[row][col] = true
                return false
            }
            // otherwise we've seen this cell before
            return true
        }
    }

    module.exports = hasPath
}()
