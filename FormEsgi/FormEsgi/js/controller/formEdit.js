// define angular module/app
var formApp = angular.module('formApp', []);

// create angular controller and pass in $scope and $http
formApp.controller('formController', ['$scope', '$http', function ($scope, $http) {
    // créer un objet qui contiendra les données 
    // à faire la transition entre le serveur et le client
    $scope.formData = {};
    // Variable indiquant le nombre de question.
    $scope.numberOfQuestion = 1;

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
        "id": "1"
    }];
    $scope.dataQuestions.details = [{
        "question": "",
        "placeholder": "Question 1",
        "id": $scope.numberOfQuestion,
        "type": "1",
        "answers": $scope.dataAnswers.details
    }];

    $scope.idInitQuestion = 0;
    $scope.initQuestion = function (question, idTypeQuestion, questionID) {
        if ($scope.idInitQuestion == 0) {
            $scope.dataAnswers.details = [{
                "answer": "",
                "placeholder": "Réponse 1",
                "id": "0"
            }];
            $scope.dataQuestions.details = [{
                "question": question,
                "placeholder": "Question 1",
                "id": questionID,
                "type": idTypeQuestion,
                "answers": $scope.dataAnswers.details
            }];
            $scope.idInitQuestion++;
        } else {
            $scope.addQuestionEdit(question, idTypeQuestion, questionID);
        }
    };

    $scope.initAnswer = function (fixedAnswer, fixedAnswerID, questionId) {
        $scope.addAnswerEdit(questionId, fixedAnswer, fixedAnswerID);
    };

    // Envoyer les données saisies au serveur
    $scope.processForm = function () {
        console.log("Date cloture " + $scope.formData.dateCloture);

        var jsonQuestions = JSON.stringify($scope.dataQuestions.details);
        var jsonForm = JSON.stringify($scope.formData);
        $http({
            method: 'POST',
            dataType: JSON,
            url: '/Formulaire/EditForm',
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
	                scope.$apply(function () { $location.path("/Formulaire/List"); });
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
        var maxIdQuestion = $scope.maxIdQuestion();
        maxIdQuestion++;
        var maxIdAnswer = $scope.maxIdAnswer();
        maxIdAnswer++;
        var arrQuest = $scope.dataQuestions.details;
        var nbrQuestionto = parseInt(arrQuest.length) + 1;

        var answerForQuestion = [{
            "answer": "",
            "placeholder": "Réponse 1",
            "id": maxIdAnswer
        }];
        $scope.dataQuestions.details.push({
            "question": "",
            "placeholder": "Question " + nbrQuestionto,
            "id": maxIdQuestion,
            "type": "4",
            "answers": answerForQuestion
        });
    };

    // Ajouter une Question déjà créée
    $scope.addQuestionEdit = function (question, idTypeQuestion, idQuestion) {
        $scope.numberOfQuestion++;
        var answerForQuestion = [{
            "answer": "",
            "placeholder": "Réponse 1",
            "id": "0"
        }];
        $scope.dataQuestions.details.push({
            "question": question,
            "placeholder": "Question " + $scope.numberOfQuestion,
            "id": idQuestion,
            "type": idTypeQuestion,
            "answers": answerForQuestion
        });
    };

    // Ajouter une réponse pour une question
    $scope.addAnswerEdit = function (questionID, answer, answerID) {
        var arrQuest = $scope.dataQuestions.details;
        for (var item in arrQuest) {
            if (arrQuest[item]['id'] == questionID) {
                var arrAnsw = arrQuest[item]['answers'];

                if (arrAnsw[0]['id'] == 0) {
                    $scope.dataAnswers.details = [{
                        "answer": answer,
                        "placeholder": "Réponse 1 :",
                        "id": answerID
                    }];
                    arrQuest[item]['answers'] = $scope.dataAnswers.details;
                    $scope.dataQuestions.details = arrQuest;
                }
                else {
                    arrAnsw.push({
                        "answer": answer,
                        "placeholder": "Réponse " + (parseInt(arrAnsw.length) + parseInt(1))+ " : ",
                        "id": answerID
                    });
                    arrQuest[item]['answers'] = arrAnsw;
                    $scope.dataQuestions.details = arrQuest;
                }
            }
        }

    };

    // Ajouter une réponse pour une question
    $scope.addAnswer = function (questionID) {
        var maxIdAnswer = $scope.maxIdAnswer();
        maxIdAnswer++;
        var arrQuest = $scope.dataQuestions.details;

        for (var item in arrQuest) {
            if (arrQuest[item]['id'] == questionID) {
                var arrAnsw = arrQuest[item]['answers'];
                arrAnsw.push({
                    "answer": "",
                    "placeholder": "Réponse " + (parseInt(arrAnsw.length) + parseInt(1)) + " : ",
                    "id": maxIdAnswer
                });
                arrQuest[item]['answers'] = arrAnsw;
                $scope.dataQuestions.details = arrQuest;
            }
        }
    };

    // Supprimer une réponse
    $scope.removeAnswer = function (answerID) {
        var arrQuest = $scope.dataQuestions.details;
        for (var question in arrQuest) {
            var arrAnsw = arrQuest[question]['answers'];
            
            if (arrAnsw.length > 1) {
                for (var answer in arrAnsw) {
                    if (arrAnsw[answer]['id'] == answerID) {
                        arrAnsw.splice(answer,1);
                    }
                }
            }
        }
    };

    // Afficher le bouton de suppression d'une réponse
    // Seulement quand il y a plus d'une réponse
    $scope.showRemoveAnswer = function (questionID) {
        var arrQuest = $scope.dataQuestions.details;
        for (var question in arrQuest) {
            if (arrQuest[question]['id'] == questionID) {
                var arrAnsw = arrQuest[question]['answers'];
                if (arrAnsw.length > 1) { return true; };
            }
        }

        return false;
    };

    $scope.maxIdQuestion = function() {
        var arrQuest = $scope.dataQuestions.details;
        var maxId = 0;
        for (var question in arrQuest) {
            if (arrQuest[question]['id'] >= maxId) {
                maxId = arrQuest[question]['id'];
            }
        }

        return maxId;
    };

    $scope.maxIdAnswer = function() {
        var arrQuest = $scope.dataQuestions.details;
        var maxId = 0;

        for (var question in arrQuest) {
            var arrAnsw = arrQuest[question]['answers'];

            for (var answer in arrAnsw) {
                if (arrAnsw[answer]['id'] >= maxId) {
                    maxId = arrAnsw[answer]['id'];
                }
            }
        }

        return maxId;
    };

    // Supprimer une question
    $scope.removeQuestion = function (questionID) {
        var arrQuest = $scope.dataQuestions.details;

        if (arrQuest.length > 1) {
            for (var question in arrQuest) {
                if (arrQuest[question]['id'] == questionID) {
                    arrQuest.splice(question, 1);
                }
            }
        }
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