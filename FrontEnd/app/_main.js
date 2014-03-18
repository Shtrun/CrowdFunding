(function () {
    'use strict';

    /* main: startup script creates the 'angelikoo' module and adds custom Ng directives */

    // 'angelikoo' is the one Angular (Ng) module in this app
    // 'angelikoo' module is in global namespace
    window.angelikoo = angular.module('angelikoo', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',          // routing
        'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)

        // Custom modules 
        'common',           // common functions, logger, spinner

        // 3rd Party Modules
        'ui.bootstrap',     // ui-bootstrap (ex: carousel, pagination, dialog)
        'breeze.angular'
    ]);
        
    // Configure routes
    angelikoo.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/', { templateUrl: 'FrontEndViews/Home'/*, controller: 'MainPageCtrl'*/ }).
            when('/companies', { templateUrl: 'FrontEndViews/Companies'/*, controller: 'CompaniesPageCtrl'*/ }).
            when('/company/:companyId', { templateUrl: 'FrontEndViews/Company'/*, controller: 'CompanyPageCtrl'*/ }).
            when('/user', { templateUrl: 'app/user.view.html'/*, controller: 'UserPageCtrl'*/ }).   //???
            otherwise({ redirectTo: '/' });
    }]);

    // Handle routing errors and success events
    angelikoo.run(['$route', '$rootScope', 'breeze', 'dataContext'
        , function ($route, $rootScope, breeze, dataContext) {
            // Include $route to kick start the router.

            //dataContext.initialize();
        }]);
})();