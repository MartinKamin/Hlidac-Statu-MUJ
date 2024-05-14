!/**
 * Highstock JS v11.4.1 (2024-04-04)
 *
 * Indicator series type for Highcharts Stock
 *
 * (c) 2010-2024 Sebastian Bochan
 *
 * License: www.highcharts.com/license
 */function(t){"object"==typeof module&&module.exports?(t.default=t,module.exports=t):"function"==typeof define&&define.amd?define("highcharts/indicators/accumulation-distribution",["highcharts","highcharts/modules/stock"],function(e){return t(e),t.Highcharts=e,t}):t("undefined"!=typeof Highcharts?Highcharts:void 0)}(function(t){"use strict";var e=t?t._modules:{};function o(t,e,o,n){t.hasOwnProperty(e)||(t[e]=n.apply(null,o),"function"==typeof CustomEvent&&window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded",{detail:{path:e,module:t[e]}})))}o(e,"Stock/Indicators/AD/ADIndicator.js",[e["Core/Series/SeriesRegistry.js"],e["Core/Utilities.js"]],function(t,e){var o,n=this&&this.__extends||(o=function(t,e){return(o=Object.setPrototypeOf||({__proto__:[]})instanceof Array&&function(t,e){t.__proto__=e}||function(t,e){for(var o in e)Object.prototype.hasOwnProperty.call(e,o)&&(t[o]=e[o])})(t,e)},function(t,e){if("function"!=typeof e&&null!==e)throw TypeError("Class extends value "+String(e)+" is not a constructor or null");function n(){this.constructor=t}o(t,e),t.prototype=null===e?Object.create(e):(n.prototype=e.prototype,new n)}),r=t.seriesTypes.sma,i=e.error,s=e.extend,u=e.merge,a=function(t){function e(){return null!==t&&t.apply(this,arguments)||this}return n(e,t),e.populateAverage=function(t,e,o,n,r){var i=e[n][1],s=e[n][2],u=e[n][3],a=o[n];return[t[n],u===i&&u===s||i===s?0:(2*u-s-i)/(i-s)*a]},e.prototype.getValues=function(t,o){var n,r,s,u=o.period,a=t.xData,c=t.yData,l=o.volumeSeriesID,p=t.chart.get(l),f=p&&p.yData,h=c?c.length:0,d=[],y=[],m=[];if(!(a.length<=u)||!h||4===c[0].length){if(!p){i("Series "+l+" not found! Check `volumeSeriesID`.",!0,t.chart);return}for(r=u;r<h;r++)n=d.length,s=e.populateAverage(a,c,f,r,u),n>0&&(s[1]+=d[n-1][1]),d.push(s),y.push(s[0]),m.push(s[1]);return{values:d,xData:y,yData:m}}},e.defaultOptions=u(r.defaultOptions,{params:{index:void 0,volumeSeriesID:"volume"}}),e}(r);return s(a.prototype,{nameComponents:!1,nameBase:"Accumulation/Distribution"}),t.registerSeriesType("ad",a),a}),o(e,"masters/indicators/accumulation-distribution.src.js",[e["Core/Globals.js"]],function(t){return t})});