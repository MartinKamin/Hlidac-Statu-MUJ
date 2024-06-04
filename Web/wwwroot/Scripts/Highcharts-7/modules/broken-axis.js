/*
 Highcharts JS v7.2.2 (2020-08-24)

 (c) 2009-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function(c){"object"===typeof module&&module.exports?(c["default"]=c,module.exports=c):"function"===typeof define&&define.amd?define("highcharts/modules/broken-axis",["highcharts"],function(k){c(k);c.Highcharts=k;return c}):c("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(c){function k(c,g,k,m){c.hasOwnProperty(g)||(c[g]=m.apply(null,k))}c=c?c._modules:{};k(c,"modules/broken-axis.src.js",[c["parts/Globals.js"],c["parts/Utilities.js"]],function(c,g){var k=g.extend,m=g.isArray,q=g.pick;
g=c.addEvent;var w=c.find,r=c.fireEvent,n=c.Axis,t=c.Series,u=function(a,d){return w(d,function(d){return d.from<a&&a<d.to})};k(n.prototype,{isInBreak:function(a,d){var f=a.repeat||Infinity,e=a.from,b=a.to-a.from;d=d>=e?(d-e)%f:f-(e-d)%f;return a.inclusive?d<=b:d<b&&0!==d},isInAnyBreak:function(a,d){var f=this.options.breaks,e=f&&f.length,b;if(e){for(;e--;)if(this.isInBreak(f[e],a)){var c=!0;b||(b=q(f[e].showPoints,!this.isXAxis))}var p=c&&d?c&&!b:c}return p}});g(n,"afterInit",function(){"function"===
typeof this.setBreaks&&this.setBreaks(this.options.breaks,!1)});g(n,"afterSetTickPositions",function(){if(this.isBroken){var a=this.tickPositions,d=this.tickPositions.info,c=[],e;for(e=0;e<a.length;e++)this.isInAnyBreak(a[e])||c.push(a[e]);this.tickPositions=c;this.tickPositions.info=d}});g(n,"afterSetOptions",function(){this.isBroken&&(this.options.ordinal=!1)});n.prototype.setBreaks=function(a,d){function c(a){var c=a,d;for(d=0;d<b.breakArray.length;d++){var e=b.breakArray[d];if(e.to<=a)c-=e.len;
else if(e.from>=a)break;else if(b.isInBreak(e,a)){c-=a-e.from;break}}return c}function e(a){var c;for(c=0;c<b.breakArray.length;c++){var d=b.breakArray[c];if(d.from>=a)break;else d.to<a?a+=d.len:b.isInBreak(d,a)&&(a+=d.len)}return a}var b=this,h=m(a)&&!!a.length;b.isDirty=b.isBroken!==h;b.isBroken=h;b.options.breaks=b.userOptions.breaks=a;b.forceRedraw=!0;h||b.val2lin!==c||(delete b.val2lin,delete b.lin2val);h&&(b.userOptions.ordinal=!1,b.val2lin=c,b.lin2val=e,b.setExtremes=function(b,a,c,d,e){if(this.isBroken){for(var f,
v=this.options.breaks;f=u(b,v);)b=f.to;for(;f=u(a,v);)a=f.from;a<b&&(a=b)}n.prototype.setExtremes.call(this,b,a,c,d,e)},b.setAxisTranslation=function(a){n.prototype.setAxisTranslation.call(this,a);this.unitLength=null;if(this.isBroken){a=b.options.breaks;var c=[],d=[],e=0,f,h=b.userMin||b.min,g=b.userMax||b.max,k=q(b.pointRangePadding,0),p;a.forEach(function(a){f=a.repeat||Infinity;b.isInBreak(a,h)&&(h+=a.to%f-h%f);b.isInBreak(a,g)&&(g-=g%f-a.from%f)});a.forEach(function(a){l=a.from;for(f=a.repeat||
Infinity;l-f>h;)l-=f;for(;l<h;)l+=f;for(p=l;p<g;p+=f)c.push({value:p,move:"in"}),c.push({value:p+(a.to-a.from),move:"out",size:a.breakSize})});c.sort(function(a,b){return a.value===b.value?("in"===a.move?0:1)-("in"===b.move?0:1):a.value-b.value});var m=0;var l=h;c.forEach(function(a){m+="in"===a.move?1:-1;1===m&&"in"===a.move&&(l=a.value);0===m&&(d.push({from:l,to:a.value,len:a.value-l-(a.size||0)}),e+=a.value-l-(a.size||0))});b.breakArray=d;b.unitLength=g-h-e+k;r(b,"afterBreaks");b.staticScale?b.transA=
b.staticScale:b.unitLength&&(b.transA*=(g-b.min+k)/b.unitLength);k&&(b.minPixelPadding=b.transA*b.minPointOffset);b.min=h;b.max=g}});q(d,!0)&&this.chart.redraw()};g(t,"afterGeneratePoints",function(){var a=this.xAxis,c=this.yAxis,f=this.points,e=f.length,b=this.options.connectNulls;if(a&&c&&(a.options.breaks||c.options.breaks))for(;e--;){var h=f[e];var g=null===h.y&&!1===b;g||!a.isInAnyBreak(h.x,!0)&&!c.isInAnyBreak(h.y,!0)||(f.splice(e,1),this.data[e]&&this.data[e].destroyElements())}});g(t,"afterRender",
function(){this.drawBreaks(this.xAxis,["x"]);this.drawBreaks(this.yAxis,q(this.pointArrayMap,["y"]))});c.Series.prototype.drawBreaks=function(a,c){var d=this,e=d.points,b,h,g,k;a&&c.forEach(function(c){b=a.breakArray||[];h=a.isXAxis?a.min:q(d.options.threshold,a.min);e.forEach(function(d){k=q(d["stack"+c.toUpperCase()],d[c]);b.forEach(function(b){g=!1;if(h<b.from&&k>b.to||h>b.from&&k<b.from)g="pointBreak";else if(h<b.from&&k>b.from&&k<b.to||h>b.from&&k>b.to&&k<b.from)g="pointInBreak";g&&r(a,g,{point:d,
brk:b})})})})};c.Series.prototype.gappedPath=function(){var a=this.currentDataGrouping,d=a&&a.gapSize;a=this.options.gapSize;var f=this.points.slice(),e=f.length-1,b=this.yAxis;if(a&&0<e)for("value"!==this.options.gapUnit&&(a*=this.basePointRange),d&&d>a&&d>=this.basePointRange&&(a=d);e--;)f[e+1].x-f[e].x>a&&(d=(f[e].x+f[e+1].x)/2,f.splice(e+1,0,{isNull:!0,x:d}),this.options.stacking&&(d=b.stacks[this.stackKey][d]=new c.StackItem(b,b.options.stackLabels,!1,d,this.stack),d.total=0));return this.getGraphPath(f)}});
k(c,"masters/modules/broken-axis.src.js",[],function(){})});
//# sourceMappingURL=broken-axis.js.map