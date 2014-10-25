module.exports = function (grunt) {
    grunt.initConfig({
	pkg: grunt.file.readJSON('package.json'),
	jshint: {
	    file: ['index.js', 'client/**/*.js'],
	    options: {
		laxcomma: true,
		asi: true,
		expr: true
	    }
	},
	express: {
	    dev: {
		options: {
		    script: 'index.js'
		}
	    }
	},
	simplemocha: {
	    all: { src: ['test/**/*.js'] }
	},
	watch: {
	    files:['app/**/*.js', 'client/**/*.js'],
	    tasks:['jshint', 'simplemocha'],
	    options: {
		spawn: false
	    },
            express: {
                files: ['client/**/*.js'],
                tasks: ['express:dev'],
                options: { spawn: false }
            }
	}
    });

    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-simple-mocha');
    grunt.loadNpmTasks('grunt-express-server');

    grunt.registerTask('default', ['jshint']);
    grunt.registerTask('server', ['express:dev', 'watch']);
};
