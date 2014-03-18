(function () {
    'use strict';

    /* dataContext: data access and model management layer */
    // create and add dataContext to the Ng injector
    // constructor function relies on Ng injector
    // to provide service dependencies
    var serviceId = 'dataContext';
    angular.module('angelikoo').factory(serviceId,
        ['breeze', 'model', 'common', 'entityManagerFactory', '$timeout',
        function (breeze, model, common, entityManagerFactory, $timeout) {

            var getLogFn = common.logger.getLogFn;
            var log = getLogFn(serviceId);
            var logError = getLogFn(serviceId, 'error');
            var logSuccess = getLogFn(serviceId, 'success');

            var initialized;

            configureBreeze();
            var EntityQuery = breeze.EntityQuery;
            var manager = entityManagerFactory.newManager();
            //manager.enableSaveQueuing(true);

            var dataContext = {
                metadataStore: manager.metadataStore,
                getCompaniesPartials: _getCompaniesPartials,
                getCompany: _getCompany
            };
            model.initialize(dataContext);
            return dataContext;

            //#region private members

            function _getCompaniesPartials(forceRefresh) {
                var query = breeze.EntityQuery.from('Companies')
                .select('description, endDate, goal, id, logo, name, raised, startDate, status')
                .orderBy('name')
                .toType('Company');

                var promise = manager
                    .executeQuery(query)
                    .then(querySucceeded)
                    .catch(_queryFailed);
                return promise;

                function querySucceeded(data) {
                    var qType = data.XHR ? 'remote' : 'local';
                    logSuccess(qType + ' query succeeded, got ' + data.results.length + ' companies');
                    return data.results;
                }
            }

            function _getCompany(id, forceRefresh) {
                var query = breeze.EntityQuery.from('Companies')
                .select('description, endDate, goal, id, logo, name, raised, startDate, status')
                .where('id', '==', id)
                .toType('Company');

                var promise = manager
                    .executeQuery(query)
                    .then(querySucceeded)
                    .catch(_queryFailed);
                return promise;


                function querySucceeded(data) {
                    var qType = data.XHR ? "remote" : "local";
                    logSuccess(qType + ' query succeeded, got ' + data.results.length + ' companies');
                    return data.results;
                }
            }

            // ReSharper disable InconsistentNaming
            function _queryFailed(error) {
                // ReSharper restore InconsistentNaming
                var msg = config.appErrorPrefix + 'Error retrieving data.in ' + error.message;
                logError(msg, error);
                throw error;
            }
            
            function configureBreeze() {
                // configure to use camelCase
                breeze.NamingConvention.camelCase.setAsDefault();

                // configure to resist CSRF attack
                var antiForgeryToken = $("#antiForgeryToken").val();
                if (antiForgeryToken) {
                    // get the current default Breeze AJAX adapter & add header
                    var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
                    ajaxAdapter.defaultSettings = {
                        headers: {
                            'RequestVerificationToken': antiForgeryToken
                        },
                    };
                }
            }
            //#endregion
        }]);
})();