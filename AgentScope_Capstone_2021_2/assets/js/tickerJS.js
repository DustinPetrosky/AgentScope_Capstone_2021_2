/// <reference path="../../scripts/jquery-3.4.1.js" />


jQuery(document).ready(function ($) {

    var jssor_1_options = {
        $AutoPlay: 1,
        $Idle: 0,
        $SlideDuration: 5000,
        $SlideEasing: $Jease$.$Linear,
        $PauseOnHover: 4,
        $SlideWidth: 140,
        $Cols: 7,
        $Align: 0
    };

    var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

    /*#region responsive code begin*/

    var MAX_WIDTH = 980;

    function ScaleSlider() {
        var containerElement = jssor_1_slider.$Elmt.parentNode;
        var containerWidth = containerElement.clientWidth;

        if (containerWidth) {

            var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

            jssor_1_slider.$ScaleWidth(expectedWidth);
        }
        else {
            window.setTimeout(ScaleSlider, 30);
        }
    }

    ScaleSlider();

    $(window).bind("load", ScaleSlider);
    $(window).bind("resize", ScaleSlider);
    $(window).bind("orientationchange", ScaleSlider);
    /*#endregion responsive code end*/
});