angular
    .module('porlaDashboard.timeFilter',
    [])
.component('timeFilter',
    {
        transclude: true,
        bindings: {
            timeFilterPeriod: '<',
            timeFilterPeriodChanged: '&'
        },
        controller: ['$rootScope', function ($rootScope) {
            var ctrl = this;

            function refresh(period) {
                if (period) {
                    switch (period) {
                    case 1:
                        $('#magic-line')
                            .animate({ left: $(".filterTabOne").position().left, width: $(".filterTabOne").width() });
                        break;
                    case 2:
                        $('#magic-line')
                            .animate({ left: $(".filterTabTwo").position().left, width: $(".filterTabTwo").width() });
                        break;
                    case 3:
                        $('#magic-line')
                            .animate({
                                left: $(".filterTabThree").position().left,
                                width:
                                    $(".filterTabThree").width()
                            });
                        break;
                    case 4:
                        $('#magic-line')
                            .animate({ left: $(".filterTabFour").position().left, width: $(".filterTabFour").width() });
                        break;
                    }
                } else {
                    $('#magic-line')
                        .animate({ left: $(".filterTabAll").position().left, width: $(".filterTabAll").width() });
                }
            }

            this.$onChanges = function(newValue) {
                refresh(ctrl.timeFilterPeriod);
            };

            this.$onInit = function() {
                refresh(ctrl.timeFilterPeriod);
            };

            $rootScope.$on('setLanguage',
                function() {
                    refresh(ctrl.timeFilterPeriod);
                });
            this.setTimeFilterPeriod = function(value) {
                refresh(value);
                ctrl.timeFilterPeriodChanged({period: value});
            };
            //$('.filterTabOne').on('click', function () {
            //    $('.filterTabTwo, .filterTabThree, .filterTabFour').removeClass('current_page_item');
            //    $(this).toggleClass('current_page_item');
            //});
            //$('.filterTabTwo').on('click', function () {
            //    $('.filterTabOne, .filterTabThree, .filterTabFour').removeClass('current_page_item');
            //    $(this).toggleClass('current_page_item');
            //});
            //$('.filterTabThree').on('click', function () {
            //    $('.filterTabOne, .filterTabTwo, .filterTabFour').removeClass('current_page_item');
            //    $(this).toggleClass('current_page_item');
            //});
            //$('.filterTabFour').on('click', function () {
            //    $('.filterTabOne, .filterTabTwo, .filterTabThree').removeClass('current_page_item');
            //    $(this).toggleClass('current_page_item');
            //});
        }],
        templateUrl: 'time-filter/time-filter.html'
    })