var countOfQuestions = 1;
var curQuestionId = 1; // Уникальное значение для атрибута name

function deleteQuestion(a) {
    // Получаем доступ к ДИВу, содержащему поле
    var contDiv = a.parentNode;
    // Удаляем этот ДИВ из DOM-дерева
    contDiv.parentNode.removeChild(contDiv);
    // Уменьшаем значение текущего числа полей
    countOfQuestions--;
    // Возвращаем false, чтобы не было перехода по сслыке
    return false;
}

function addQuestion(button) {
    countOfQuestions++;
    curQuestionId++;
    var question = document.createElement("div");

    question.innerHTML = '<div class="question" id=\"question_' + curQuestionId + '\">' +
        '<h4>Вопрос номер №' + curQuestionId + '</h4>'+
        '<input type="text" name="question_name" placeholder="Введите вопрос" /> <br />' +
        '<label for="type">Выберите тип вопроса:</label> <br />' +
        '<label for="text_answer">Ввод ответа вручную</label>' +
        '<input type="radio" name="type" id="text_answer" value="0" />'+
        '<label for="choose_answer">Выбор правильного ответа</label>'+
        '<input type="radio" name="type" id="choose_answer" value="1" /> <br />'+
        '<input type="number" name="points" placeholder="Количество баллов за вопрос" /><br />'+
        '<button onclick="return deleteQuestion(this)">Удалить вопрос</button>' +

        '<h4>Ответы к вопросу №' + curQuestionId + '</h4>' +
        '<button onclick="return addAnswer(this);" class="addAnswer">Добавить ответ</button>' +
        '<div class="answer" id="answer_1">'+
            '<input type="text" name="answer_value" placeholder="Ответ" /> <br />'+
            '<label for="true_answer">Правильный ответ</label>'+
            '<input type="radio" name="status" id="true_answer" value="1" />'+
            '<label for="false_answer">Неправильный ответ</label>'+
            '<input type="radio" name="status" id="false_answer" value="0" /><br />'+
            '<button onclick="return deleteAnswer(this)">Удалить ответ</button>'+
        '</div>'+

    '</div>';

    button.parentNode.appendChild(question);
    return false;
}