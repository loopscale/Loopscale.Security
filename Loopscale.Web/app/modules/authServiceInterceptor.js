    'use strict';
   app.factory('authServiceInterceptor', authInterceptorService);

    authInterceptorService.$inject = ['$q', '$injector','$location', 'localStorageService'];

    function authInterceptorService($q, $injector, $location, localStorageService) {
    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

       var authData = localStorageService.get('authorizationData');
        //console.log("authServiceInterceptor -- outside auth interceptor");
        //console.log(authData);
        if (authData) {
            console.log("authServiceInterceptor -- inside auth interceptor");
            //console.log(authData);
            config.headers.Authorization = 'Bearer ' + authData.token;
            //console.log(authData.token);
        }
        else
        {
            console.log("localStorageService.get('authorizationData') is null");
            $location.path('login');
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var $state = $injector.get('$state');
            var authData = localStorageService.get('authorizationData');

            $location.path('/login');
            //authService.logOut();
            //$state.go('login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
    }
