(function () {
    'use strict';

    /* Defines the "main page view" controller
    * Constructor function relies on Ng injector to provide:
    *     breeze - breeze is a "module" known to the injectory thanks to main.js
    *     dataContext - injected data and model access component (dataContext.js)
    *     logger - records notable events during the session (about.logger.js)
    */
    var controllerId = 'HomePageCtrl';
    angular.module('angelikoo').controller(controllerId,
        ['breeze', /*'dataContext',*/ 'common', '$sce',
        function (breeze, /*dataContext,*/ common, $sce) {

            common.logger.log("creating HomePageCtrl");
            var removeList = breeze.core.arrayRemoveItem;

            var vm = this;
            vm.investorsCount = 19;
            vm.companiesCount = 7;

            vm.mainVideoUrls = ['http://www.thebigdot.com/1234.mp4', 'http://www.youtube.com/watch?v=UGLTIjjHTBQ', 'http://www.youtube.com/watch?v=ztbrmLOUkrY', 'http://www.youtube.com/watch?v=jAT2EWRxVCg'];
            vm.videoUrl = $sce.trustAsResourceUrl(vm.mainVideoUrls[0]);

            vm.companiesCategories = [
                { name: 'Funded', active: false },
                { name: 'Hot!', active: true },
                { name: 'New', active: false },
                { name: 'Near closing', active: false }];

            vm.accelerators = ['Microsoft', 'Amazon', 'Kokorikoo', 'SAP'];

            activate();

            function activate() {
                common.activateController([], controllerId)
                .then(function () {
                    common.logger.log('Activated Admin View', 'data', 'source', true);
                })
                ;
            }
        }]);
})();