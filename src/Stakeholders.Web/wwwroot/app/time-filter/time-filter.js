angular
    .module('porlaDashboard.timeFilter',
    [])
.component('timeFilter',
    {
        transclude: true,
        bindings: {
            timeFilterPeriod: '=',
            timeFilterPeriodChanged: '&'
        },
        controller: ['$rootScope', function ($rootScope) {
            var ctrl = this;

            function refresh() {
                switch (ctrl.timeFilterPeriod || 1) {
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
                        .animate({ left: $(".filterTabThree").position().left, width: $(".filterTabThree").width() });
                    break;
                case 4:
                    $('#magic-line')
                        .animate({ left: $(".filterTabFour").position().left, width: $(".filterTabFour").width() });
                    break;
                }
            }

            $rootScope.$on('setLanguage',
                function() {
                    refresh();
                });
            this.setTimeFilterPeriod = function(value) {
                ctrl.timeFilterPeriod = value;
                refresh();
                ctrl.timeFilterPeriodChanged();
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