
var countOfAnswers = 1;
var curAnswerId = 1; // Уникальное значение для атрибута name

function deleteAnswer(a) {
    // Получаем доступ к ДИВу, содержащему поле
    var contDiv = a.parentNode;
    // Удаляем этот ДИВ из DOM-дерева
    contDiv.parentNode.removeChild(contDiv);
    // Уменьшаем значение текущего числа полей
    countOfAnswers--;
    // Возвращаем false, чтобы не было перехода по ссылке
    return false;
}

function addAnswer(question) {
    countOfAnswers++;
    curAnswerId++;
    var div = document.createElement("div");
    div.innerHTML = '<div class=\"answer\" id=\"answer_' + curAnswerId + '\">' +
        '<label>Ответ номер ' + curAnswerId + '</label><br>' +
        '<input type=\"text\" name=\"answer_value\" placeholder=\"Ответ\" /><br />' +
        '<label for=\"true_answer\">Правильный ответ</label>' +
        '<input type=\"radio\" name=\"status\" id=\"true_answer\" value=\"1\" />' +
        '<label for=\"false_answer\">Неправильный ответ</label>' +
        '<input type=\"radio\" name=\"status\" id=\"false_answer\" value=\"0\" /> <br />' +
        '<button onclick=\"return deleteAnswer(this)\">Удалить ответ</button></div >';
    question.parentNode.appendChild(div);
    return false;
}