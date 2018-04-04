/* --------------------------------------
		CUSTOM FUNCTION WRITE HERE
-------------------------------------- */
jQuery(document).on('ready', function() {
	"use strict";
	
	/* -------------------------------------
			MAP CLUSTERING
	-------------------------------------- */
	if(jQuery('#tg-map').length > 0 ){
		madlink_init_map_script('tg-map');
	}
	if(jQuery('#tg-serviceslocationmap').length > 0 ){
		madlink_init_map_script('tg-serviceslocationmap');
	}
	/* -------------------------------------
			SVG INJECT HERE
	-------------------------------------- */
	var mySVGsToInject = document.querySelectorAll('.tg-svginject');
	SVGInjector(mySVGsToInject);
	/* -------------------------------------
			COLLAPSE MENU SMALL DEVICES
	-------------------------------------- */
	function collapseMenu(){
		$('.tg-navigation ul li.tg-hasdropdown').prepend('<span class="tg-dropdowarrow"><i class="fa fa-plus"></i></span>');
		$('.tg-navigation ul li.tg-hasdropdown span').on('click', function() {
			$(this).next().next().slideToggle(300);
			$(this).parent('li.tg-hasdropdown').toggleClass('tg-open');
		});
	}
	collapseMenu();
	/* -------------------------------------
			HOME SLIDER VERSION THREE
	-------------------------------------- */
	var _tg_categoryslider = jQuery("#tg-categoryslider");
	_tg_categoryslider.owlCarousel({
		autoPlay : false,
		slideSpeed : 300,
		navigation : true,
		pagination : false,
		navigationText: [
			"<span class='tg-btnroundprev'><i class='fa fa-angle-left'></i></span>",
			"<span class='tg-btnroundnext'><i class='fa fa-angle-right'></i></span>"
		],
		itemsCustom : [
			[0, 1],
			[481, 3],
			[991, 3],
			[1200, 6],
		],
	});
	/* -------------------------------------
		CUSTOM CODE FOR CATEGORY SLIDER
	-------------------------------------- */
	jQuery('#tg-categoryslider .item').on( "click", function() {
		var _this = jQuery(this);
		var dataId = _this.data('id');
		var contentParent = _this.parents('.tg-searchbulder').find('.tg-subcategories');
		contentParent.find('.tg-tabcontent').hide().removeClass('tg-active');
		contentParent.find('#' + dataId).show().addClass('tg-active');
	});
	/* -------------------------------------
			SIGNIN/SIGNUP SLIDER
	-------------------------------------- */
	var _tg_signincategoryslider = jQuery("#tg-signincategoryslider");
	_tg_signincategoryslider.owlCarousel({
		autoPlay : false,
		slideSpeed : 300,
		navigation : true,
		pagination : false,
		navigationText: [
			"<span class='tg-btnroundprev'><i class='fa fa-angle-left'></i></span>",
			"<span class='tg-btnroundnext'><i class='fa fa-angle-right'></i></span>"
		],
		itemsCustom : [
			[0, 1],
			[481, 2],
			[991, 3],
			[1200, 4],
		],
	});
	/* -------------------------------------
		CUSTOM CODE FOR CATEGORY SLIDER
	-------------------------------------- */
	jQuery('#tg-signincategoryslider .item').on( "click", function() {
		var _this = jQuery(this);
		var dataId = _this.data('id');
		var contentParent = _this.parents('.tg-searchbulder').find('.tg-subcategories');
		contentParent.find('.tg-tabcontent').hide().removeClass('tg-active');
		contentParent.find('#' + dataId).show().addClass('tg-active');
	});
	/* -------------------------------------
			SIGNIN/SIGNUP SLIDER
	-------------------------------------- */
	var _tg_signincategoryslider_two = jQuery("#tg-signincategoryslider-two");
	_tg_signincategoryslider_two.owlCarousel({
		autoPlay : false,
		slideSpeed : 300,
		navigation : true,
		pagination : false,
		navigationText: [
			"<span class='tg-btnroundprev'><i class='fa fa-angle-left'></i></span>",
			"<span class='tg-btnroundnext'><i class='fa fa-angle-right'></i></span>"
		],
		itemsCustom : [
			[0, 1],
			[481, 2],
			[991, 3],
			[1200, 4],
		],
	});
	/* -------------------------------------
		CUSTOM CODE FOR CATEGORY SLIDER
	-------------------------------------- */
	jQuery('#tg-signincategoryslider-two .item').on( "click", function() {
		var _this = jQuery(this);
		var dataId = _this.data('id');
		var contentParent = _this.parents('.tg-searchbulder').find('.tg-subcategories');
		contentParent.find('.tg-tabcontent').hide().removeClass('tg-active');
		contentParent.find('#' + dataId).show().addClass('tg-active');
	});
	/* --------------------------------------
			COUNTER
	-------------------------------------- */
	try {
		jQuery('.tg-counter').appear(function () {
			jQuery('.tg-counter > h2 > span').countTo();
		});
	} catch (err) {}
	/* ---------------------------------------
			PORTFOLIO FILTERABLE
	-------------------------------------- */
	var _container = jQuery('.tg-featureddirectposts');
	var _optionSets = jQuery('.option-set');
	var _optionLinks = _optionSets.find('a');
	function doIsotopeFilter() {
		if (jQuery().isotope) {
			var isotopeFilter = '';
			_optionLinks.each(function () {
				var selector = jQuery(this).attr('data-filter');
				var link = window.location.href;
				var firstIndex = link.indexOf('filter=');
				if (firstIndex > 0) {
					var id = link.substring(firstIndex + 7, link.length);
					if ('.' + id == selector) {
						isotopeFilter = '.' + id;
					}
				}
			});
			jQuery(window).load(function () {
				_container.isotope({
					itemSelector: '.tg-directpost',
					filter: isotopeFilter
				});
			});
			_optionLinks.each(function () {
				var $this = jQuery(this);
				var selector = $this.attr('data-filter');
				if (selector == isotopeFilter) {
					if (!$this.hasClass('tg-active')) {
						var $optionSet = $this.parents('.option-set');
						$optionSet.find('.tg-active').removeClass('tg-active');
						$this.addClass('tg-active');
					}
				}
			});
			_optionLinks.on('click', function () {
				var $this = jQuery(this);
				var selector = $this.attr('data-filter');
				_container.isotope({itemSelector: '.tg-directpost', filter: selector});
				if (!$this.hasClass('tg-active')) {
					var $optionSet = $this.parents('.option-set');
					$optionSet.find('.tg-active').removeClass('tg-active');
					$this.addClass('tg-active');
				}
				return false;
			});
		}
	}
	var isotopeTimer = window.setTimeout(function () {
		window.clearTimeout(isotopeTimer);
		doIsotopeFilter();
	}, 1000);
	/* --------------------------------------
			SEARCH FILTER TOGGLE			
	-------------------------------------- */
	jQuery('.tg-btnfilter').on("click", function(){
		jQuery("#tg-filtersection").slideToggle(400);
	});
	/* --------------------------------------
			SHORT DESCRIPTION				
	-------------------------------------- */
	var _readmore = jQuery('.tg-detailpage .tg-description');
	_readmore.readmore({
		speed: 500,
		collapsedHeight: 70,
		moreLink: '<a class="tg-btntext" href="#">more...</a>',
		lessLink: '<a class="tg-btntext" href="#">less...</a>',
	});
	/* --------------------------------------
			BOOTSTRAP TOOLTIP
	-------------------------------------- */
	jQuery('[data-toggle="tooltip"]').tooltip();
	/* -------------------------------------
			HOME SLIDER VERSION THREE
	-------------------------------------- */
	function reviewSlider(){
		var sync1 = jQuery("#tg-feedbackslider");
		var sync2 = jQuery("#tg-reviewerdpslider");
		sync1.owlCarousel({
			singleItem : true,
			slideSpeed : 1000,
			pagination:false,
			afterAction : syncPosition,
			responsiveRefreshRate : 200,
		});
		sync2.owlCarousel({
			items					: 6,
			itemsDesktop			: [1199,6],
			itemsTablet				: [768,4],
			itemsMobile				: [479,2],
			pagination:false,
			responsiveRefreshRate : 100,
			afterInit : function(el){
				el.find(".owl-item").eq(0).addClass("tg-active");
			}
		});
		function syncPosition(el){
			var current = this.currentItem;
			jQuery("#tg-reviewerdpslider")
			.find(".owl-item")
			.removeClass("tg-active")
			.eq(current)
			.addClass("tg-active");
			if(jQuery("#tg-reviewerdpslider").data("owlCarousel") !== undefined){
				center(current);
			}
		}
		jQuery("#tg-reviewerdpslider").on("click", ".owl-item", function(e){
			e.preventDefault();
			var number = jQuery(this).data("owlItem");
			sync1.trigger("owl.goTo",number);
		});
		function center(number){
			var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
			var num = number;
			var found = false;
			for(var i in sync2visible){
				if(num === sync2visible[i]){
					var found = true;
				}
			}
			if(found===false){
				if(num>sync2visible[sync2visible.length-1]){
				sync2.trigger("owl.goTo", num - sync2visible.length+2);
				}else{
					if(num - 1 === -1){
						num = 0;
					}
					sync2.trigger("owl.goTo", num);
				}
			} else if(num === sync2visible[sync2visible.length-1]){
				sync2.trigger("owl.goTo", sync2visible[1]);
			} else if(num === sync2visible[0]){
				sync2.trigger("owl.goTo", num-1);
			}
		}
	}
	reviewSlider();
	/* -------------------------------------
			BRANDS SLIDER
	-------------------------------------- */
	var _tg_brandsslider = jQuery("#tg-brandsslider");
	_tg_brandsslider.owlCarousel({
		slideSpeed : 300,
		singleItem: true,
		pagination: true,
	});
	/* -------------------------------------
			CHECKBOX SCROLL
	-------------------------------------- */
	var _tg_directpostslistingscrollbar = jQuery("#tg-directpostslistingscrollbar");
	_tg_directpostslistingscrollbar.mCustomScrollbar({
		axis:"y",
	});
	/* -------------------------------------
			TIME RANGE SLIDER				
	-------------------------------------- */
	var _tg_timerangeslider = jQuery("#tg-timerangeslider");
	_tg_timerangeslider.slider({
		range: true,
		min: 0,
		max: 1440,
		step: 15,
		values: [540, 1020],
		slide: function (e, ui) {
			var hours1 = Math.floor(ui.values[0] / 60);
			var minutes1 = ui.values[0] - (hours1 * 60);
			if (hours1.length == 1) hours1 = '0' + hours1;
			if (minutes1.length == 1) minutes1 = '0' + minutes1;
			if (minutes1 == 0) minutes1 = '00';
			if (hours1 >= 12) {
				if (hours1 == 12) {
					hours1 = hours1;
					minutes1 = minutes1 + " PM";
				} else {
					hours1 = hours1 - 12;
					minutes1 = minutes1 + " PM";
				}
			} else {
				hours1 = hours1;
				minutes1 = minutes1 + " AM";
			}
			if (hours1 == 0) {
				hours1 = 12;
				minutes1 = minutes1;
			}
			jQuery('.tg-timeslider').html(hours1 + ':' + minutes1);
			var hours2 = Math.floor(ui.values[1] / 60);
			var minutes2 = ui.values[1] - (hours2 * 60);
			if (hours2.length == 1) hours2 = '0' + hours2;
			if (minutes2.length == 1) minutes2 = '0' + minutes2;
			if (minutes2 == 0) minutes2 = '00';
			if (hours2 >= 12) {
				if (hours2 == 12) {
					hours2 = hours2;
					minutes2 = minutes2 + " PM";
				} else if (hours2 == 24) {
					hours2 = 11;
					minutes2 = "59 PM";
				} else {
					hours2 = hours2 - 12;
					minutes2 = minutes2 + " PM";
				}
			} else {
				hours2 = hours2;
				minutes2 = minutes2 + " AM";
			}
			jQuery('.tg-timeslider2').html(hours2 + ':' + minutes2);
		}
	});
	/* -------------------------------------
			CONSULTATION FEE SLIDER			
	-------------------------------------- */
	function ageRangeslider(){
		jQuery("#tg-consultationfeerangeslider").slider({
			range: true,
			min: 0,
			max: 500,
			values: [ 20, 160 ],
			slide: function( event, ui ) {
				jQuery( "#tg-consultationfeeamount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
			}
		});
		jQuery( "#tg-consultationfeeamount" ).val( "$" + jQuery("#tg-consultationfeerangeslider").slider( "values", 0 ) + " - $" + jQuery("#tg-consultationfeerangeslider").slider( "values", 1 ) );
	}
	ageRangeslider();
	/* -------------------------------------
			RADIUS RANGE SLIDER
	-------------------------------------- */
	function radiusRangeslider(){
		jQuery("#tg-radiusrangeslider").slider({
			range: true,
			min: 0,
			max: 500,
			values: [ 20, 160 ],
			slide: function( event, ui ) {
				jQuery( "#tg-radiusamount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
			}
		});
		jQuery( "#tg-radiusamount" ).val( jQuery( "#tg-radiusrangeslider" ).slider( "values", 0 ) + " - " + jQuery( "#tg-radiusrangeslider" ).slider( "values", 1 ) );
	}
	radiusRangeslider();
	/* -------------------------------------
			Google Map
	-------------------------------------- */
	jQuery("#tg-location-map, #tg-addressmap").gmap3({
		marker: {
			address: "1600 Elizabeth St, Melbourne, Victoria, Australia",
			options: {
				title: "Robert Frost Elementary School",
				icon: "images/marker.png",
			}
		},
		map: {
			options: {
				zoom: 16,
				scrollwheel: false,
				disableDoubleClickZoom: true,
			}
		}
	});
	/* -------------------------------------
			PROGRESS BAR
	-------------------------------------- */
	try {
		jQuery('#tg-userskill').appear(function () {
			jQuery('.tg-skillholder').each(function () {
				jQuery(this).find('.tg-skillbar').animate({
					width: jQuery(this).attr('data-percent')
				}, 2500);
			});
		});
	} catch (err) {}
	/* --------------------------------------
			COUNTER
	-------------------------------------- */
	function expireyCounter(){
		var note = jQuery('#note'),
			ts = new Date(2017, 0, 1),
			newYear = true;
		if((new Date()) > ts){
			ts = (new Date()).getTime() + 10*24*60*60*1000;
			newYear = false;
		}
		jQuery('#tg-countdown').countdown({
			timestamp	: ts,
			callback	: function(days, hours, minutes, seconds){
				var message = "";
				message += days + " day" + ( days==1 ? '':'s' ) + ", ";
				message += hours + " hour" + ( hours==1 ? '':'s' ) + ", ";
				message += minutes + " minute" + ( minutes==1 ? '':'s' ) + " and ";
				message += seconds + " second" + ( seconds==1 ? '':'s' ) + " <br />";
				if(newYear){
					message += "left until the new year!";
				}
				else {
					message += "left to 10 days from now!";
				}
				note.html(message);
			}
		});
	}
	expireyCounter();
	/* --------------------------------------
			FORM STEP PROGRESS
	-------------------------------------- */
	function formSteps(){
		jQuery('.tg-btnnext').on('click', function () {
			jQuery('.tg-current').removeClass('tg-current').hide().next().show().addClass('tg-current');
			jQuery('.tg-formprogressbar li.tg-active').next().addClass('tg-active');
			if (jQuery('.tg-formprogressbar')) {};
		});
		jQuery('.tg-btnprevious').on('click', function () {
			jQuery('.tg-current').removeClass('tg-current').hide().prev().show().addClass('tg-current');
			jQuery('.tg-formprogressbar li.tg-active').removeClass('tg-active').prev().addClass('tg-active');
		});
	}
	formSteps();
	/* --------------------------------------
			SERVICES FEE COLLAPSE
	-------------------------------------- */
	var _openAll = new jQueryCollapse(
		jQuery("#tg-servicesfeecollapse"),{
			open: function() {
				this.slideDown(150);
			},
			close: function() {
				this.slideUp(150);
			}
		}
	);
	_openAll.open();
	/* --------------------------------------
			LOAD MORE FEEDBACK
	-------------------------------------- */
	function loadMoreFeedback() {
		jQuery(".tg-patientfeedback").slice(0, 5).show();
		jQuery(".tg-btnloadmore").on('click', function (e) {
			e.preventDefault();
			jQuery(".tg-patientfeedback:hidden").slice(0, 4).slideDown();
			if (jQuery(".tg-patientfeedback:hidden").length == 0) {
				jQuery(".tg-btnloadmore").fadeOut('slow');
			}
			jQuery('html,body').animate({
				scrollTop: jQuery(this).offset().top -30
			}, 1500);
		});
	}
	loadMoreFeedback();
	/* --------------------------------------
			LOAD MORE QUESTION ANSWER
	-------------------------------------- */
	function loadMoreQuestionAnswer() {
		jQuery(".tg-questionanswer").slice(0, 5).show();
		jQuery(".tg-btnloadmoretwo").on('click', function (e) {
			e.preventDefault();
			jQuery(".tg-questionanswer:hidden").slice(0, 4).slideDown();
			if (jQuery(".tg-questionanswer:hidden").length == 0) {
				jQuery(".tg-btnloadmoretwo").fadeOut('slow');
			}
			jQuery('html,body').animate({
				scrollTop: jQuery(this).offset().top -30
			}, 1500);
		});
	}
	loadMoreQuestionAnswer();
	/* -------------------------------------
			APPOINTMENTS CALENDER
	-------------------------------------- */
	jQuery('#tg-calendar').fullCalendar({
		 height: 'auto',
		 dayClick: function(date, jsEvent, view) {
			alert('Clicked on: ' + date.format());
		 }
	});
	/* -------------------------------------
	//		PRELOADER
	//-------------------------------------- */
	//jQuery(window).on('load', function() {
	//	jQuery(".preloader-outer").delay(2000).fadeOut();
	//	jQuery(".pins").delay(2000).fadeOut("slow");
	//});
	/* -------------------------------------
			DATE PICKER
	-------------------------------------- */
	jQuery(".tg-datepicker").datepicker();
	/* -------------------------------------
			TIME PICKER
	-------------------------------------- */
	jQuery('#tg-timepicker1, #tg-timepicker2, #tg-timepicker3, #tg-timepicker4').timepicker({
		minuteStep: 15,
		defaultTime: false,
	});
	/* -------------------------------------
			DASHBOARD NAVIGATION
	-------------------------------------- */
	jQuery(".tg-btndashboard").on('click', function() {
		jQuery("#tg-dashboardnav").slideToggle("slow");
		jQuery(".tg-btndashboard").toggleClass("tg-active");
	});
	/* -------------------------------------
			ACADEMICS SETTING
	-------------------------------------- */
	jQuery('.tg-dashboarddepartment a').on( "click", function(e) {
		e.preventDefault();
		var id = jQuery(this).data('related'); 
		jQuery('.tg-dashboarddepartment').removeClass('tg-active');
		jQuery(this).parent().addClass('tg-active');
		jQuery(".tg-settingcontentarea").each(function(){
			jQuery(this).slideUp(400);
			if(jQuery(this).attr('id') == id) {
				jQuery(this).slideDown(400);
			}
		});
	});
	/* -------------------------------------
			HOME SLIDER VERSION THREE
	-------------------------------------- */
	function gallerySlider(){
		var sync1 = jQuery("#tg-viewslider");
		var sync2 = jQuery("#tg-thumbnailslider");
		sync1.owlCarousel({
			singleItem : true,
			slideSpeed : 1000,
			pagination:false,
			navigation: true,
			navigationText: [
				"<span class='tg-btnroundprev'><i class='fa fa-angle-left'></i></span>",
				"<span class='tg-btnroundnext'><i class='fa fa-angle-right'></i></span>"
			],
			afterAction : syncPosition,
			responsiveRefreshRate : 200,
		});
		sync2.owlCarousel({
			items					: 10,
			itemsDesktop			: [1199,6],
			itemsTablet				: [768,4],
			itemsMobile				: [479,2],
			pagination:false,
			responsiveRefreshRate : 100,
			afterInit : function(el){
				el.find(".owl-item").eq(0).addClass("tg-active");
			}
		});
		function syncPosition(el){
			var current = this.currentItem;
			jQuery("#tg-thumbnailslider")
			.find(".owl-item")
			.removeClass("tg-active")
			.eq(current)
			.addClass("tg-active");
			if(jQuery("#tg-thumbnailslider").data("owlCarousel") !== undefined){
				center(current);
			}
		}
		jQuery("#tg-thumbnailslider").on("click", ".owl-item", function(e){
			e.preventDefault();
			var number = jQuery(this).data("owlItem");
			sync1.trigger("owl.goTo",number);
		});
		function center(number){
			var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
			var num = number;
			var found = false;
			for(var i in sync2visible){
				if(num === sync2visible[i]){
					var found = true;
				}
			}
			if(found===false){
				if(num>sync2visible[sync2visible.length-1]){
				sync2.trigger("owl.goTo", num - sync2visible.length+2);
				}else{
					if(num - 1 === -1){
						num = 0;
					}
					sync2.trigger("owl.goTo", num);
				}
			} else if(num === sync2visible[sync2visible.length-1]){
				sync2.trigger("owl.goTo", sync2visible[1]);
			} else if(num === sync2visible[0]){
				sync2.trigger("owl.goTo", num-1);
			}
		}
	}
	gallerySlider();
	/* -------------------------------------
			CHECKBOX SCROLL
	-------------------------------------- */
	var _tg_themescrollbar = jQuery(".tg-themescrollbar");
	_tg_themescrollbar.mCustomScrollbar({
		axis:"y",
	});
	/* -------------------------------------
				BADGE MODAL BOX				
	-------------------------------------- */
	var _tg_badgemodalbox = jQuery('#tg-badgemodalbox');
	_tg_badgemodalbox.modal('show');
	/* -------------------------------------
			COMPANY LIST DROPDOWN			
	-------------------------------------- */
	jQuery("#tg-btnlisttoggle").on('click', function() {
		jQuery("#tg-companylist").slideToggle();
	});
});
/* -------------------------------------
		GRAPH CHARTS
-------------------------------------- */
function init_monthsviewcharts(_chartIds){
	google.charts.load('current', {'packages':['corechart']});
	google.charts.setOnLoadCallback(drawChart);
	function drawChart() {
		var data = google.visualization.arrayToDataTable([
			['Month', 'Views'],
			["Sep 1", 50],
			["Sep 5", 100],
			["Sep 10", 50],
			["Sep 15", 200],
			["Sep 20", 150],
			["Sep 25", 225],
			["Sep 30", 300],
		]);
		var options = {
			title: '',
			left: 5,
			legend: 'none',
			series: {0: {color: '#22b0e5'},},
			lineWidth: 0,
			chartArea:{left: 40, top: 20, width:"500", height:"260"},
			pointSize: 10,
			series: {0: { pointShape: 'circle' },},
		};
		var chart = new google.visualization.AreaChart(document.getElementById(_chartIds));
		chart.draw(data, options);
	}
}