app.service('APIInterceptor', ['$rootScope', '$location', 'localStorageService', function ($rootScope, $location, localStorageService) {
    var service = this;

    service.request = function (config) {
        var currentUser = localStorageService.get('authorizationData');
        // access_token = currentUser ? currentUser.access_token : null;
        console.log(currentUser);
        if (currentUser == null)
        {
            $location.path("/login");
        }
        else
        {
            $rootScope.isAuthenticated = true;
        }
        //if (access_token) {
        //    config.headers.authorization = access_token;
        //}

        return config;
    };

    service.responseError = function (response) {
        if (response.status === 401) {
            $rootScope.$broadcast('unauthorized');
        }
        return response;
    };
}]);