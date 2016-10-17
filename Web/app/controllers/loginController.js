app.controller('loginController',['$rootScope', '$scope', '$state', '$location', 'loginService', function($rootScope, $scope, $state, $location, loginService){
    
    //    //$scope.loginUser = function (form) {
       
    //    //    var userDetails = {};
    //    //    userDetails.Username = $scope.Username;
    //    //    userDetails.Password = $scope.Password;
       

    //    //    loginService.Login(userDetails).then(
    //    //            function (d) {
    //    //               // $rootScope.alerts = [];
    //    //               //// authProvider.setUser(d);
                   
    //    //               // $rootScope.role = d.Role;
    //    //               // //$cookies.put(d.data.sessionIdCookieName, d.data.sessionAuthorization);
    //    //               // $scope.$errorMessage = "";
    //    //               // //alert($rootScope.role);                                              
    //    //                var url = $scope.uiBaseUrl + "#/admin";
    //    //                //var url = $scope.uiBaseUrl + "admin/#";
    //    //                //console.log("url to log-in");
    //    //                //console.log(url);
    //    //                //$window.location.href = url;
    //    //                $location.path("/dashboard");


    //    //                //$rootScope.spinner = false;
    //    //                //$rootScope.login = false;

    //    //            }, errorResult);       
    //    //}

    $scope.loginUser = function (signin) {

        console.log('entering login');

        var userDetails = {};
        userDetails.userName = $scope.Username;
        userDetails.password = $scope.Password;

        loginService.Login(userDetails)

        $scope.isAuthenticated = true;

        $state.go('dashboard');
    }

    $scope.logOut = function () {
        loginService.Logout();
        console.log('logOut');

        $scope.isAuthenticated = false;

        $state.go('login');
    }

    console.log('loginController');
}]);
