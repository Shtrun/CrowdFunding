(function() {
    'use strict';
    
    var serviceId = 'entityManagerFactory';
    angular.module('angelikoo').factory(serviceId, ['config', emFactory]);

    function emFactory(config) {
        breeze.config.initializeAdapterInstance('modelLibrary', 'backingStore', true);
        breeze.NamingConvention.camelCase.setAsDefault();
        
        var serviceName = config.remoteServiceName;
        var metadataStore = new breeze.MetadataStore();
        
        //metadataStore.registerEntityTypeCtor("Tournament", function() {
        //    this.GamesCount = 0;    // This property is [NotMapped] in the service
        //});
        
        //metadataStore.registerEntityTypeCtor("Player", function () {
        //    this.gamesInCurrentTournament = 0;    // This property is [NotMapped] in the service
        //});

        var provider = {
            metadataStore: metadataStore,
            newManager: newManager
        };
        
        return provider;

        function newManager() {
            var mgr = new breeze.EntityManager({
                serviceName: serviceName,
                metadataStore: metadataStore
            });
            return mgr;
        }
    }
})();