(function () {
    'use strict';

    var controllerId = 'shell';
    angular.module('angelikoo').controller(controllerId,
        ['$rootScope', '$location', 'common', 'config', shell]);

    function shell($rootScope, $location, common, config) {
        var vm = this;
        var logSuccess = common.logger.getLogFn(controllerId, 'success');
        var events = config.events;
        vm.busyMessage = 'Please wait ...';
        vm.isBusy = true;
        vm.spinnerOptions = {
            radius: 40,
            lines: 7,
            length: 0,
            width: 30,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#F58A00'
        };

        vm.isActive = function (viewLocation) {
            var active = (viewLocation === $location.path());
            return active;
        };

        activate();

        function activate() {
            //logSuccess('Angelikoo Angular loaded!', null, true);
            var promises = [];
            common.activateController(promises, controllerId);
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        $rootScope.$on('$routeChangeStart',
            function (event, next, current) {
                toggleSpinner(true);
            }
        );

        $rootScope.$on(events.controllerActivateSuccess,
            function (data) {
                toggleSpinner(false);
            }
        );

        $rootScope.$on(events.spinnerToggle,
            function (data) {
                toggleSpinner(data.show);
            }
        );
    };
})();