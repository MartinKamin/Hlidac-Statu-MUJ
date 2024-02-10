/*
 Highcharts JS v8.2.2 (2020-10-22)

 Dependency wheel module

 (c) 2010-2018 Torstein Honsi

 License: www.highcharts.com/license
*/
(function(a){"object"===typeof module&&module.exports?(a["default"]=a,module.exports=a):"function"===typeof define&&define.amd?define("highcharts/modules/dependency-wheel",["highcharts","highcharts/modules/sankey"],function(f){a(f);a.Highcharts=f;return a}):a("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(a){function f(a,k,f,n){a.hasOwnProperty(k)||(a[k]=n.apply(null,f))}a=a?a._modules:{};f(a,"Series/DependencyWheelSeries.js",[a["Core/Animation/AnimationUtilities.js"],a["Core/Series/Series.js"],
a["Core/Globals.js"],a["Mixins/Nodes.js"]],function(a,f,r,n){var k=a.animObject,q=f.seriesTypes.sankey.prototype;f.seriesType("dependencywheel","sankey",{center:[null,null],curveFactor:.6,startAngle:0},{orderNodes:!1,getCenter:f.seriesTypes.pie.prototype.getCenter,createNodeColumns:function(){var a=[this.createNodeColumn()];this.nodes.forEach(function(b){b.column=0;a[0].push(b)});return a},getNodePadding:function(){return this.options.nodePadding/Math.PI},createNode:function(a){var b=q.createNode.call(this,
a);b.index=this.nodes.length-1;b.getSum=function(){return b.linksFrom.concat(b.linksTo).reduce(function(a,b){return a+b.weight},0)};b.offset=function(a){function h(a){return a.fromNode===b?a.toNode:a.fromNode}var d=0,c,g=b.linksFrom.concat(b.linksTo);g.sort(function(a,b){return h(a).index-h(b).index});for(c=0;c<g.length;c++)if(h(g[c]).index>b.index){g=g.slice(0,c).reverse().concat(g.slice(c).reverse());var p=!0;break}p||g.reverse();for(c=0;c<g.length;c++){if(g[c]===a)return d;d+=g[c].weight}};return b},
translate:function(){var a=this.options,b=2*Math.PI/(this.chart.plotHeight+this.getNodePadding()),p=this.getCenter(),h=(a.startAngle-90)*r.deg2rad;q.translate.call(this);this.nodeColumns[0].forEach(function(d){if(d.sum){var c=d.shapeArgs,g=p[0],f=p[1],k=p[2]/2,l=k-a.nodeWidth,m=h+b*c.y;c=h+b*(c.y+c.height);d.angle=m+(c-m)/2;d.shapeType="arc";d.shapeArgs={x:g,y:f,r:k,innerR:l,start:m,end:c};d.dlBox={x:g+Math.cos((m+c)/2)*(k+l)/2,y:f+Math.sin((m+c)/2)*(k+l)/2,width:1,height:1};d.linksFrom.forEach(function(c){if(c.linkBase){var d,
e=c.linkBase.map(function(e,p){e*=b;var k=Math.cos(h+e)*(l+1),m=Math.sin(h+e)*(l+1),n=a.curveFactor;d=Math.abs(c.linkBase[3-p]*b-e);d>Math.PI&&(d=2*Math.PI-d);d*=l;d<l&&(n*=d/l);return{x:g+k,y:f+m,cpX:g+(1-n)*k,cpY:f+(1-n)*m}});c.shapeArgs={d:[["M",e[0].x,e[0].y],["A",l,l,0,0,1,e[1].x,e[1].y],["C",e[1].cpX,e[1].cpY,e[2].cpX,e[2].cpY,e[2].x,e[2].y],["A",l,l,0,0,1,e[3].x,e[3].y],["C",e[3].cpX,e[3].cpY,e[0].cpX,e[0].cpY,e[0].x,e[0].y]]}}})}})},animate:function(a){if(!a){var b=k(this.options.animation).duration/
2/this.nodes.length;this.nodes.forEach(function(a,h){var d=a.graphic;d&&(d.attr({opacity:0}),setTimeout(function(){d.animate({opacity:1},{duration:b})},b*h))},this);this.points.forEach(function(a){var b=a.graphic;!a.isNode&&b&&b.attr({opacity:0}).animate({opacity:1},this.options.animation)},this)}}},{setState:n.setNodeState,getDataLabelPath:function(a){var b=this.series.chart.renderer,f=this.shapeArgs,h=0>this.angle||this.angle>Math.PI,d=f.start,c=f.end;this.dataLabelPath||(this.dataLabelPath=b.arc({open:!0}).add(a));
this.dataLabelPath.attr({x:f.x,y:f.y,r:f.r+(this.dataLabel.options.distance||0),start:h?d:c,end:h?c:d,clockwise:+h});return this.dataLabelPath},isValid:function(){return!0}});""});f(a,"masters/modules/dependency-wheel.src.js",[],function(){})});
//# sourceMappingURL=dependency-wheel.js.map