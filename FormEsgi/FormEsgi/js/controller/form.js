// define angular module/app
var formApp = angular.module('formApp', []);

// create angular controller and pass in $scope and $http
formApp.controller('formController', ['$scope', '$http', function ($scope, $http) {
    // créer un objet qui contiendra les données 
    // à faire la transition entre le serveur et le client
    $scope.formData = {};
    // Variable indiquant le nombre de question.
    $scope.numberOfQuestion = 1;
    // variable indiquant le nombre de réponse.
    $scope.answerId = 1;

    // Variable qui cachera les éléments du formulaires
    $scope.visible = true;
    $scope.see = "Cacher";

    // Les deux objets qui contiendront respectivement 
    // les données des questions et des réponses associées.
    $scope.dataQuestions = {};
    $scope.dataAnswers = {};

    // On initilialise les deux objets 
    // avec des données par défaut qu'on place dans un tableau
    $scope.dataAnswers.details = [{
        "answer": "",
        "placeholder": "Réponse 1",
        "id": $scope.answerId
    }];
    $scope.dataQuestions.details = [{
        "question": "",
        "placeholder": "Question 1",
        "id": $scope.numberOfQuestion,
        "type": "1",
        "answers": $scope.dataAnswers.details
    }];

    // Envoyer les données saisies au serveur
    $scope.processForm = function () {
        console.log("Date cloture " + $scope.formData.dateCloture);
        //$scope.formData.dateCloture = $scope.formData.dateCloture.replace("T", " ");
        //console.log("Date cloture " + $scope.formData.dateCloture);
        var jsonQuestions = JSON.stringify($scope.dataQuestions.details);
        var jsonForm = JSON.stringify($scope.formData);
        $http({
            method: 'POST',
            dataType: JSON,
            url: '/Formulaire/CreateForm',
            data: $.param({ "form": jsonForm, "quest": jsonQuestions }),
          //  data    : $.param( {  "dataMain" : $scope.formData, "dataRest" : $scope.dataQuestions.details  }),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
	        .success(function (data) {
	            console.log(data);

	            if (!data.success) {
	                // if not successful, bind errors to error variables
	                $scope.errorTitle = data.errors.title;
	                $scope.errorDescription = data.errors.description;
	                $scope.message = data.message;
	                $scope.question = data.question;
	                $scope.answer = data.answer;
	            } else {
	                // if successful, bind success message to message
	                $scope.message = data.message;
	                $scope.question = data.question;
	                $scope.answer = data.answer;
	            }
	        });

    };

    // Ajouter une Question
    $scope.addQuestion = function () {
        $scope.numberOfQuestion++;
        var answerForQuestion = [{
            "answer": "",
            "placeholder": "Réponse 1",
            "id": $scope.answerId
        }];
        $scope.dataQuestions.details.push({
            "question": "",
            "placeholder": "Question " + $scope.numberOfQuestion,
            "id": $scope.numberOfQuestion,
            "type": "1",
            "answers": answerForQuestion
        });
    };

    // Ajouter une réponse pour une question
    $scope.addAnswer = function (questionID) {
        $scope.answerId++;
        var arrQuest = $scope.dataQuestions.details;
        var arrAnsw = arrQuest[questionID - 1]['answers'];

        arrAnsw.push({
            "answer": "",
            "placeholder": "Réponse " + (parseInt(arrAnsw.length) + parseInt(1)),
            "id": $scope.answerId
        });
        $scope.dataQuestions.details[questionID - 1]['answers'] = arrAnsw;
    };

    // Supprimer une réponse
    $scope.removeAnswer = function (questionID) {
        var arrQuest = $scope.dataQuestions.details;
        var arrAnsw = arrQuest[questionID - 1]['answers']; // Récupérer le tableau de réponse pour la question
        // Vérifier si ce n'est pas la seule réponse pour la question
        // Supprimer la réponse du tableau
        if (arrAnsw.length > 1) {
            $scope.answerId--;
            var itemRemove = arrAnsw.pop();
        };

    };

    // Afficher le bouton de suppression d'une réponse
    // Seulement quand il y a plus d'une réponse
    $scope.showRemoveAnswer = function (questionID) {
        var arrQuest = $scope.dataQuestions.details;
        var arrAnsw = arrQuest[questionID - 1]['answers']; // Récupérer le tableau de réponse pour la question
        if (arrAnsw.length > 1) { return true; }; // Permettre de supprimer que quand il y a plus de 1 réponse
    };

    // Supprimer une question
    $scope.removeQuestion = function () {
        var arrQuest = $scope.dataQuestions.details;
        if (arrQuest.length > 1) {
            $scope.numberOfQuestion--;
            var itemRemove = arrQuest.pop();
        };
    };

    // Afficher le bouton de suppression d'une question
    // seulement s'il y a plus d'une question
    $scope.showRemoveQuestion = function () {
        var arrQuest = $scope.dataQuestions.details;
        if (arrQuest.length > 1) { return true; };
    };

    $scope.visibleElement = function () {
        if ($scope.see == "Cacher") { $scope.see = "Afficher"; $scope.visible = false; }
        else { $scope.see = "Cacher"; $scope.visible = true; }
    };


}]);

//focus directive
formApp.directive('myFocus', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.$watch(attr.myFocus, function (n, o) {
                if (n != 0 && n) {
                    element[0].focus();
                }
            });
        }
    };
});
//blur directive
formApp.directive('myBlur', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            element.bind('blur', function () {
                //apply scope (attributes)
                scope.$apply(attr.myBlur);
                //return scope value for focusing to false
                scope.$eval(attr.myFocus + '=false');
            });
        }
    };
});