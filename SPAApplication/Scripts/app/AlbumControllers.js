 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('Album');

    angular.module('Album',['ngRoute'])
    .controller('Album_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/Album/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Album_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/Album/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Album_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
        
        $scope.save = function(){
            $http.post('/Api/Album/', $scope.data)
            .then(function(response){ $location.path("Album"); });
        }

    }])
    .controller('Album_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Album/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

        
        $scope.save = function(){
            $http.put('/Api/Album/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Album"); });
        }

    }])
    .controller('Album_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Album/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/Album/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Album"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/Album', {
                title: 'Album - List',
                templateUrl: '/Static/Album_List',
                controller: 'Album_list'
            })
            .when('/Album/Create', {
                title: 'Album - Create',
                templateUrl: '/Static/Album_Edit',
                controller: 'Album_create'
            })
            .when('/Album/Edit/:id', {
                title: 'Album - Edit',
                templateUrl: '/Static/Album_Edit',
                controller: 'Album_edit'
            })
            .when('/Album/Delete/:id', {
                title: 'Album - Delete',
                templateUrl: '/Static/Album_Delete',
                controller: 'Album_delete'
            })
            .when('/Album/:id', {
                title: 'Album - Details',
                templateUrl: '/Static/Album_Details',
                controller: 'Album_details'
            })
    }])
;

})();
