/// <binding AfterBuild='less' Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    less = require("gulp-less");

var paths = {
    webroot: "./wwwroot/"
};

gulp.task("clean",
    function(cb) {
        rimraf(paths.webroot + '/css', cb);
    });

gulp.task("less",
    function() {
        return gulp
            .src('./Styles/**/*.less')
            .pipe(less())
            .pipe(gulp.dest(paths.webroot + '/css'));
    });