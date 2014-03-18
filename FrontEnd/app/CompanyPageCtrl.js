(function () {
    'use strict';

    /* Defines the "company page view" controller
    * Constructor function relies on Ng injector to provide:
    *     breeze - breeze is a "module" known to the injectory thanks to main.js
    *     dataContext - injected data and model access component (dataContext.js)
    *     logger - records notable events during the session (about.logger.js)
    */
    var controllerId = 'CompanyPageCtrl';
    angular.module('angelikoo').controller('CompanyPageCtrl',
        ['$routeParams', 'breeze', 'dataContext', 'common',
        function ($routeParams, breeze, dataContext, common) {

            var getLogFn = common.logger.getLogFn;
            var log = getLogFn(controllerId);
            var logError = getLogFn(controllerId, 'error');
            var logSuccess = getLogFn(controllerId, 'success');

            common.logger.log("creating CompanyPageCtrl");
        
            var vm = this;
            
            activate();

            function activate() {
                common.activateController([_getCompany($routeParams.companyId)], controllerId)
                    .then(function () {
                        //@@@ This is calling this log before the getSucceeded in _getCompany!
                        log('Activated company view');
                    })
                ;
            }

            function _getCompany(id, forceRefresh) {
                dataContext.getCompany(id, forceRefresh)
                    .then(function getSucceeded(data) {
                        vm.company = data[0];
                    });
            }
        }]);
})();