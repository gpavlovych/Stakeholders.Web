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
                    default:
                        $('#magic-line')
                            .animate({ left: $(".filterTabAll").position().left, width: $(".filterTabAll").width() });
                        break;
                }
            }

            this.$onChanges = function(newValue) {
                refresh(ctrl.timeFilterPeriod);
            };

            this.$onInit = function () {
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
        }],
        templateUrl: 'time-filter/time-filter.html'
    })