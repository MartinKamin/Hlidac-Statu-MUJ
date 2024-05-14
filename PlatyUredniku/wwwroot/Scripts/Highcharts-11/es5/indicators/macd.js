!/**
 * Highstock JS v11.4.1 (2024-04-04)
 *
 * Indicator series type for Highcharts Stock
 *
 * (c) 2010-2024 Sebastian Bochan
 *
 * License: www.highcharts.com/license
 */function(o){"object"==typeof module&&module.exports?(o.default=o,module.exports=o):"function"==typeof define&&define.amd?define("highcharts/indicators/macd",["highcharts","highcharts/modules/stock"],function(t){return o(t),o.Highcharts=t,o}):o("undefined"!=typeof Highcharts?Highcharts:void 0)}(function(o){"use strict";var t=o?o._modules:{};function s(o,t,s,e){o.hasOwnProperty(t)||(o[t]=e.apply(null,s),"function"==typeof CustomEvent&&window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded",{detail:{path:t,module:o[t]}})))}s(t,"Stock/Indicators/MACD/MACDIndicator.js",[t["Core/Globals.js"],t["Core/Series/SeriesRegistry.js"],t["Core/Utilities.js"]],function(o,t,s){var e,i=this&&this.__extends||(e=function(o,t){return(e=Object.setPrototypeOf||({__proto__:[]})instanceof Array&&function(o,t){o.__proto__=t}||function(o,t){for(var s in t)Object.prototype.hasOwnProperty.call(t,s)&&(o[s]=t[s])})(o,t)},function(o,t){if("function"!=typeof t&&null!==t)throw TypeError("Class extends value "+String(t)+" is not a constructor or null");function s(){this.constructor=o}e(o,t),o.prototype=null===t?Object.create(t):(s.prototype=t.prototype,new s)}),n=o.noop,r=t.seriesTypes,a=(r.column,r.sma),l=s.extend,p=s.correctFloat,c=s.defined,h=s.merge,d=function(s){function e(){return null!==s&&s.apply(this,arguments)||this}return i(e,s),e.prototype.init=function(){t.seriesTypes.sma.prototype.init.apply(this,arguments);var o=this.color;this.options&&(c(this.colorIndex)&&(this.options.signalLine&&this.options.signalLine.styles&&!this.options.signalLine.styles.lineColor&&(this.options.colorIndex=this.colorIndex+1,this.getCyclic("color",void 0,this.chart.options.colors),this.options.signalLine.styles.lineColor=this.color),this.options.macdLine&&this.options.macdLine.styles&&!this.options.macdLine.styles.lineColor&&(this.options.colorIndex=this.colorIndex+1,this.getCyclic("color",void 0,this.chart.options.colors),this.options.macdLine.styles.lineColor=this.color)),this.macdZones={zones:this.options.macdLine.zones,startIndex:0},this.signalZones={zones:this.macdZones.zones.concat(this.options.signalLine.zones),startIndex:this.macdZones.zones.length}),this.color=o},e.prototype.toYData=function(o){return[o.y,o.signal,o.MACD]},e.prototype.translate=function(){var t=this,s=["plotSignal","plotMACD"];o.seriesTypes.column.prototype.translate.apply(t),t.points.forEach(function(o){[o.signal,o.MACD].forEach(function(e,i){null!==e&&(o[s[i]]=t.yAxis.toPixels(e,!0))})})},e.prototype.destroy=function(){this.graph=null,this.graphmacd=this.graphmacd&&this.graphmacd.destroy(),this.graphsignal=this.graphsignal&&this.graphsignal.destroy(),t.seriesTypes.sma.prototype.destroy.apply(this,arguments)},e.prototype.drawGraph=function(){for(var o,s=this,e=s.points,i=s.options,n=s.zones,r={options:{gapSize:i.gapSize}},a=[[],[]],l=e.length;l--;)c((o=e[l]).plotMACD)&&a[0].push({plotX:o.plotX,plotY:o.plotMACD,isNull:!c(o.plotMACD)}),c(o.plotSignal)&&a[1].push({plotX:o.plotX,plotY:o.plotSignal,isNull:!c(o.plotMACD)});["macd","signal"].forEach(function(o,e){var n;s.points=a[e],s.options=h((null===(n=i["".concat(o,"Line")])||void 0===n?void 0:n.styles)||{},r),s.graph=s["graph".concat(o)],s.zones=(s["".concat(o,"Zones")].zones||[]).slice(s["".concat(o,"Zones")].startIndex||0),t.seriesTypes.sma.prototype.drawGraph.call(s),s["graph".concat(o)]=s.graph}),s.points=e,s.options=i,s.zones=n},e.prototype.applyZones=function(){var o=this.zones;this.zones=this.signalZones.zones,t.seriesTypes.sma.prototype.applyZones.call(this),this.graphmacd&&this.options.macdLine.zones.length&&this.graphmacd.hide(),this.zones=o},e.prototype.getValues=function(o,s){var e,i,n,r=s.longPeriod-s.shortPeriod,a=[],l=[],h=[],d=0,u=[];if(!(o.xData.length<s.longPeriod+s.signalPeriod)){for(n=0,e=t.seriesTypes.ema.prototype.getValues(o,{period:s.shortPeriod,index:s.index}),i=t.seriesTypes.ema.prototype.getValues(o,{period:s.longPeriod,index:s.index}),e=e.values,i=i.values;n<=e.length;n++)c(i[n])&&c(i[n][1])&&c(e[n+r])&&c(e[n+r][0])&&a.push([e[n+r][0],0,null,e[n+r][1]-i[n][1]]);for(n=0;n<a.length;n++)l.push(a[n][0]),h.push([0,null,a[n][3]]);for(n=0,u=(u=t.seriesTypes.ema.prototype.getValues({xData:l,yData:h},{period:s.signalPeriod,index:2})).values;n<a.length;n++)a[n][0]>=u[0][0]&&(a[n][2]=u[d][1],h[n]=[0,u[d][1],a[n][3]],null===a[n][3]?(a[n][1]=0,h[n][0]=0):(a[n][1]=p(a[n][3]-u[d][1]),h[n][0]=p(a[n][3]-u[d][1])),d++);return{values:a,xData:l,yData:h}}},e.defaultOptions=h(a.defaultOptions,{params:{shortPeriod:12,longPeriod:26,signalPeriod:9,period:26},signalLine:{zones:[],styles:{lineWidth:1,lineColor:void 0}},macdLine:{zones:[],styles:{lineWidth:1,lineColor:void 0}},threshold:0,groupPadding:.1,pointPadding:.1,crisp:!1,states:{hover:{halo:{size:0}}},tooltip:{pointFormat:'<span style="color:{point.color}">●</span> <b> {series.name}</b><br/>Value: {point.MACD}<br/>Signal: {point.signal}<br/>Histogram: {point.y}<br/>'},dataGrouping:{approximation:"averages"},minPointLength:0}),e}(a);return l(d.prototype,{nameComponents:["longPeriod","shortPeriod","signalPeriod"],pointArrayMap:["y","signal","MACD"],parallelArrays:["x","y","signal","MACD"],pointValKey:"y",markerAttribs:n,getColumnMetrics:o.seriesTypes.column.prototype.getColumnMetrics,crispCol:o.seriesTypes.column.prototype.crispCol,drawPoints:o.seriesTypes.column.prototype.drawPoints}),t.registerSeriesType("macd",d),d}),s(t,"masters/indicators/macd.src.js",[t["Core/Globals.js"]],function(o){return o})});