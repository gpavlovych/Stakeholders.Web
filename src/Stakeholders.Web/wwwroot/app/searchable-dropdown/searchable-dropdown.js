﻿'use strict';
var dd = angular.module('ngDropdowns', []);

dd.directive('dropdownSelect', ['DropdownService', '$window',
  function (DropdownService, $window) {
      return {
          restrict: 'A',
          replace: true,
          scope: {
              dropdownUrl: '@',
              dropdownSelect: '=',
              dropdownModel: '=',
              dropdownValue: '=',
              dropdownOnchange: '&'
          },

          controller: ['$scope', '$element', '$attrs', '$resource', function ($scope, $element, $attrs, $resource) {
              $scope.searchText = "";
              $scope.labelField = $attrs.dropdownItemLabel || 'text';
              if ($attrs.dropdownValue && $scope.dropdownUrl)
                  $scope.$watch('dropdownValue',
                      function(newValue, oldValue) {
                          if (newValue) {
                              $resource($scope.dropdownUrl + "/:id")
                                  .get({ id: $scope.dropdownValue },
                                      function(result) {
                                          $scope.dropdownModelValue = result;
                                      });
                          } else {
                              $scope.dropdownModelValue = null;
                          }
                      },
                      true);
              $scope.$watch('dropdownModel',
                  function(newValue, oldValue) {
                      $scope.dropdownModelValue = newValue;
                  },
                  true);
              $scope.search = function(obj) {
                  $resource($scope.dropdownUrl + "/:id")
                      .query({ search: obj.searchText},
                          function(result) {
                              $scope.dropdownSelect = result;
                          });
              };
              if ($scope.dropdownUrl) {
                  $resource($scope.dropdownUrl + "/:id")
                      .query({ search: $scope.searchText },
                          function(result) {
                              $scope.dropdownSelect = result;
                          });
              }
              DropdownService.register($element);

              this.select = function (selected) {
                  if (selected.id !== $scope.dropdownValue) {
                     $scope.dropdownValue = selected.id;
                  }
                  if (selected !== $scope.dropdownModelValue) {
                      angular.copy(selected, $scope.dropdownModelValue);
                  }
                  $scope.dropdownOnchange({
                      selected: selected
                  });
              };

              var $clickEvent = ('click' || 'touchstart' in $window);
              $element.bind($clickEvent, function (event) {
                  $element.find('input').focus();
                  event.stopPropagation();
                  DropdownService.toggleActive($element);
              });

              $scope.$on('$destroy', function () {
                  DropdownService.unregister($element);
              });
          }],

          template: [
            '<div class="wrap-dd-select">',
              '<span class="selected">{{dropdownModelValue[labelField]}}</span>',
              '<ul class="dropdown">',
                '<li>{{searchText}}</li>',
                '<li ng-if="dropdownUrl"><input type="text" ng-model="searchText" ng-change="search(this)"/></li>',
                '<li ng-repeat="item in dropdownSelect"',
                ' class="dropdown-item"',
                ' dropdown-select-item="item"',
                ' dropdown-item-label="labelField">',
                '</li>',
              '</ul>',
            '</div>'
          ].join('')
      };
  }
]);

dd.directive('dropdownSelectItem', [
  function () {
      return {
          require: '^dropdownSelect',
          replace: true,
          scope: {
              dropdownItemLabel: '=',
              dropdownSelectItem: '='
          },

          link: function (scope, element, attrs, dropdownSelectCtrl) {
              scope.selectItem = function () {
                  if (scope.dropdownSelectItem.href) {
                      return;
                  }
                  dropdownSelectCtrl.select(scope.dropdownSelectItem);
              };
          },

          template: [
            '<li ng-class="{divider: dropdownSelectItem.divider}">',
              '<a href="" class="dropdown-item"',
              ' ng-if="!dropdownSelectItem.divider"',
              ' ng-click="selectItem()">',
                '{{dropdownSelectItem[dropdownItemLabel]}}',
              '</a>',
            '</li>'
          ].join('')
      };
  }
]);

dd.directive('dropdownMenu', ['$parse', '$compile', 'DropdownService', '$window',
  function ($parse, $compile, DropdownService, $window) {
      return {
          restrict: 'A',
          replace: false,
          scope: {
              dropdownMenu: '=',
              dropdownModel: '=',
              dropdownOnchange: '&'
          },

          controller: ['$scope', '$element', '$attrs', function ($scope, $element, $attrs) {
              $scope.labelField = $attrs.dropdownItemLabel || 'text';

              var $clickEvent = ('click' || 'touchstart' in $window);
              var $template = angular.element([
                '<ul class="dropdown">',
                  '<li ng-repeat="item in dropdownMenu"',
                  ' class="dropdown-item"',
                  ' dropdown-item-label="labelField"',
                  ' dropdown-menu-item="item">',
                  '</li>',
                '</ul>'
              ].join(''));
              // Attach this controller to the element's data
              $template.data('$dropdownMenuController', this);

              var tpl = $compile($template)($scope);
              var $wrap = angular.element('<div class="wrap-dd-menu"></div>');

              $element.replaceWith($wrap);
              $wrap.append($element);
              $wrap.append(tpl);

              DropdownService.register(tpl);

              this.select = function (selected) {
                  if (selected !== $scope.dropdownModel) {
                      angular.copy(selected, $scope.dropdownModel);
                  }
                  $scope.dropdownOnchange({
                      selected: selected
                  });
              };

              $element.bind($clickEvent, function (event) {
                  event.stopPropagation();
                  DropdownService.toggleActive(tpl);
              });

              $scope.$on('$destroy', function () {
                  DropdownService.unregister(tpl);
              });
          }]
      };
  }
]);

dd.directive('dropdownMenuItem', [
  function () {
      return {
          require: '^dropdownMenu',
          replace: true,
          scope: {
              dropdownMenuItem: '=',
              dropdownItemLabel: '='
          },

          link: function (scope, element, attrs, dropdownMenuCtrl) {
              scope.selectItem = function () {
                  if (scope.dropdownMenuItem.href) {
                      return;
                  }
                  dropdownMenuCtrl.select(scope.dropdownMenuItem);
              };
          },

          template: [
            '<li ng-class="{divider: dropdownMenuItem.divider}">',
              '<a href="" class="dropdown-item"',
              ' ng-if="!dropdownMenuItem.divider"',
              ' ng-click="selectItem()">',
                '{{dropdownMenuItem[dropdownItemLabel]}}',
              '</a>',
            '</li>'
          ].join('')
      };
  }
]);

dd.factory('DropdownService', ['$document',
  function ($document) {
      var body = $document.find('body'),
          service = {},
          _dropdowns = [];

      body.bind('click', function () {
          angular.forEach(_dropdowns, function (el) {
              el.removeClass('active');
          });
      });

      service.register = function (ddEl) {
          _dropdowns.push(ddEl);
      };

      service.unregister = function (ddEl) {
          var index;
          index = _dropdowns.indexOf(ddEl);
          if (index > -1) {
              _dropdowns.splice(index, 1);
          }
      };

      service.toggleActive = function (ddEl) {
          angular.forEach(_dropdowns, function (el) {
              if (el !== ddEl) {
                  el.removeClass('active');
              }
          });
          
          ddEl.toggleClass('active');
          if (ddEl.hasClass('active')) {
              ddEl.find('input').focus();
          }
      };

      return service;
  }
]);//angular
//    .module('porlaDashboard.searchableDropdown', [])
//.component('searchableDropdown',
//    {
//        transclude: true,
//        bindings: {
//            url: '<',
//            display:'<',
//            selectedValue: '@',
//           // dropdownSelect: '=',
//            dropdownModel: '=',
//            dropdownOnchange: '&'
//        },
//        //require: {
//        //    activityFormController: '^activityForm',
//        //    taskFormController: '^taskForm'
//        //},
//        controller: [
//            '$resource', function ($resource) {
//                var ctrl = this;
//                ctrl.$onInit = function () {
//                    updateSelectedItem();
//                    ctrl.dropdownSelect = ctrl.getMatches('');

//                }
//                ctrl.$onChanges = function (changes) {
//                    updateSelectedItem();
//                };
//                ctrl.getMatches = function (searchText) {
//                    return $resource(
//                    ctrl.url + '/:id',
//                    null,
//                    {
//                        'update': { method: 'PUT' }
//                    }).query({ search: searchText });
//                };
//                ctrl.updateSelectedValue = function () {
//                    ctrl.selectedValue = ctrl.selectedItem != null ? ctrl.selectedItem.id : null;
//                };
//                function updateSelectedItem() {
//                    if (ctrl.selectedValue != null) {
//                        $resource(
//                                ctrl.url + '/:id',
//                                null,
//                                {
//                                    'update': { method: 'PUT' }
//                                }).get({ id: ctrl.selectedValue }, function (value) {
//                                    ctrl.selectedItem = value;
//                                });
//                    }
//                    else {
//                        ctrl.selectedItem = null;
//                    }
//                }
//            }
//        ],
//        templateUrl: 'searchable-dropdown/searchable-dropdown.html'
//    })