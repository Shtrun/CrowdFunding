(function () {
    'use strict';

    /* Defines the "companies page view" controller
    * Constructor function relies on Ng injector to provide:
    *     $scope - context variable for the view to which the view binds
    *     breeze - breeze is a "module" known to the injectory thanks to main.js
    *     dataContext - injected data and model access component (dataContext.js)
    *     logger - records notable events during the session (about.logger.js)
    */

    var controllerId = 'CompaniesPageCtrl';
    angular.module('angelikoo').controller('CompaniesPageCtrl',
        ['$scope', 'breeze', 'dataContext', 'common',
        function ($scope, breeze, dataContext, common) {
            var getLogFn = common.logger.getLogFn;
            var log = getLogFn(controllerId);
            var logError = getLogFn(controllerId, 'error');
            var logSuccess = getLogFn(controllerId, 'success');

            log("creating CompaniesPageCtrl");

            var vm = this;

            vm.companiesCategories = [
                { name: 'Funded', active: false },
                { name: 'Hot!', active: true },
                { name: 'New', active: false },
                { name: 'Near closing', active: false }];

            //vm.companies = [
            //    { id: 1, logo: 'http://www.ole.com.ar/bbtfile/5012_201206078BH2NI.png', name: 'All boys', description: 'Equipo chico', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 2, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919txaGHT.jpg', name: 'Argentinos', description: 'Equipo de barrio', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 3, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919AprvTS.jpg', name: 'Belgrano', description: 'Equipo con huevos', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 4, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919x7cYEQ.jpg', name: 'Boca juniors', description: 'Campeon mundial', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 5, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919LjyQq9.jpg', name: 'Estudiantes', description: 'Equipo de Mati', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 6, logo: 'http://www.ole.com.ar/bbtfile/5012_201309199atd2A.jpg', name: 'Lanus', description: 'Sin corazon', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 7, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919S82Aj9.jpg', name: 'Rosario central', description: 'Comegatos', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 8, logo: 'http://www.ole.com.ar/bbtfile/5012_20130919vBcRjT.jpg', name: 'River plate', description: 'Equipo de la B', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //    { id: 9, logo: 'http://www.ole.com.ar/bbtfile/5012_20131030wV2rSa.jpg', name: 'San Lorenzo', description: 'Sin Libertadores', goal: '4000', raised: '1900', status: 'ok', startDate: new Date(2013, 6, 9), endDate: new Date(2014, 5, 12) },
            //];

            activate();

            function activate() {
                common.activateController([_getCompanies()], controllerId)
                    .then(function () {
                        //@@@ This is calling this log before the getSucceeded in _getCompanies!
                        log('Activated companies view');
                    })
                ;
            }

            function _getCompanies(forceRefresh) {
                dataContext.getCompaniesPartials(forceRefresh)
                    .then(function getSucceeded(data) {
                        vm.companies = data;
                    });
            }
        }]);
})();