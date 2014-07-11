// define angular module/app
var userApp = angular.module('userApp', []);

// create angular controller and pass in $scope and $http
userApp.controller('inscriptionController', ['$scope', '$http', function ($scope, $http) {
    // create a blank object to hold our form information
    // $scope will allow this to pass between controller and view
    $scope.formData = {};

    // process the form
    $scope.processForm = function () {
        var arrTest = ['salut', 'coucou', 'violent'];
        var jsonString = JSON.stringify($scope.dataQuestions.details);
        $http({
            method: 'POST',
            url: 'back.php',
            data: $.param({ data: jsonString }),
            //   data    : $.param($scope.formData, $scope.dataQuestions),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
	        .success(function (data) {
	            console.log(data);

	            if (!data.success) {
	                // if not successful, bind errors to error variables
	                $scope.errorTitle = data.errors.title;
	                $scope.errorDescription = data.errors.description;
	                $scope.message = data.message;
	            } else {
	                // if successful, bind success message to message
	                $scope.message = data.message;
	            }
	        });

    };
}]);

userApp.controller('connectionController', ['$scope', '$http', function ($scope, $http) {
    // create a blank object to hold our form information
    // $scope will allow this to pass between controller and view
    $scope.formData = {};

    // process the form
    $scope.processForm = function () {
        var jsonString = JSON.stringify($scope.dataQuestions.details);
        $http({
            method: 'POST',
            url: 'back.php',
            data: $.param({ data: jsonString }),
            //   data    : $.param($scope.formData, $scope.dataQuestions),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
	        .success(function (data) {
	            console.log(data);

	            if (!data.success) {
	                // if not successful, bind errors to error variables
	                $scope.errorTitle = data.errors.title;
	                $scope.errorDescription = data.errors.description;
	                $scope.message = data.message;
	            } else {
	                // if successful, bind success message to message
	                $scope.message = data.message;
	            }
	        });

    };
}]);

userApp.controller('editUserController', ['$scope', '$http', function ($scope, $http) {
    // create a blank object to hold our form information
    // $scope will allow this to pass between controller and view
    $scope.formData = {};

    // process the form
    $scope.processForm = function () {
        var arrTest = ['salut', 'coucou', 'violent'];
        var jsonString = JSON.stringify($scope.dataQuestions.details);
        $http({
            method: 'POST',
            url: 'back.php',
            data: $.param({ data: jsonString }),
            //   data    : $.param($scope.formData, $scope.dataQuestions),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
	        .success(function (data) {
	            console.log(data);

	            if (!data.success) {
	                // if not successful, bind errors to error variables
	                $scope.errorTitle = data.errors.title;
	                $scope.errorDescription = data.errors.description;
	                $scope.message = data.message;
	            } else {
	                // if successful, bind success message to message
	                $scope.message = data.message;
	            }
	        });

    };
}]);

// Vérifier que les champs Password sont identiques
userApp.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {

            var me = attrs.ngModel;
            var matchTo = attrs.pwCheck;

            scope.$watch('[me, matchTo]', function (value) {
                ctrl.$setValidity('pwmatch', scope[me] === scope[matchTo]);
            });

        }
    }
}]);