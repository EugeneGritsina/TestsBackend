# TestsBackend version 0.5

***updates***:

*ver 0.5*
- Добавлен Swagger.

*ver 0.4*
 - Метод getSubjects (пункт 7) + устранение неисправности, связанной с добавлением теста
 - Обновление списка моделей внизу документации: теперь это не модели, которые находятся в БД, а модели для ОТПРАВКИ в БД

*ver 0.3*
- Объект вопроса теперь содержит массив ответов, он не возвращается отдельным объектом, как было раньше

*ver 0.2*
- estimatedTime теперь везде string: и в принимаемых, в ввозвращаемых объектах
- post запрос возвращает весь DTO теста, а не только связанную с ним информацию

*ver 0.1*
- Тест для студента теперь возвращается БЕЗ статусов в ответах (правильный/неправильный)
- estimatedTime, возвращаемый в объекте теста для студента, теперь имеет тип string формата "hh:mm"

Доступ ко всем методам апи осуществляется через URL http://testsbsu.azurewebsites.net/api/tests

### В данный момент хост на сервере преостановлен, чтобы не использовать подписку. Для доступа к API нужно использовать домен localhost:44347

JSON является и входным, и выходным типом данных.

## 1) GET ALL TESTS: Список тестов с объектом предмета SubjectObject.

Тесты возвращаются без вопросов, только их описание (is_open, name...).

**GET** запрос на url 
http://testsbsu.azurewebsites.net/api/tests/

JSON Модель
```
[
    {
        "id": 1,
        "name": "Методологии программирования",
        "dueDateTime": null,
        "estimatedTime": null,
        "questionsAmount": 3,
        "maxMark": 10,
        "isOpen": true,
        "subjectObject": {
            "id": 1,
            "name": "Технологии Программирования",
            "subjectTypeId": 1
        },
        "creationDate": "2007-01-01T00:00:00"
    },
    {
        "id": 2,
        "name": "Ссылочные типы и типы значения",
        "dueDateTime": null,
        "estimatedTime": null,
        "questionsAmount": 2,
        "maxMark": 10,
        "isOpen": true,
        "subjectObject": {
            "id": 1,
            "name": "Технологии Программирования",
            "subjectTypeId": 1
        },
        "creationDate": "2007-01-01T00:00:00"
    }
]
```


## 2) GET TEST STUDENT: Конкретный тест для страницы с прохождением теста.

ВАЖНО: НЕ ВСЕ ТЕСТЫ МОГУТ БЫТЬ ВОЗВРАЩЕНЫ, т.к. действует алгоритм по сложению баллов за вопросы, а у некоторых тестов может просто не доставать пунктов у вопросов для набора максимальной оценки за тест. Тестируйте на тестах с id = 1,2,3.

**GET** запрос на URL: 
http://testsbsu.azurewebsites.net/api/tests/STUDENT/1 ,
где 1 - id нужного теста.

JSON Модель:
```
{
    "id": 2,
    "name": "Ссылочные типы и типы значения",
    "dueDateTime": null,
    "estimatedTime": "1970-01-01T00:00:00",
    "questionsAmount": 2,
    "maxMark": 10,
    "isOpen": true,
    "creationDate": "2007-01-01T00:00:00",
    "subjectObject": {
        "id": 1,
        "name": "Технологии Программирования",
        "subjectTypeId": 1
    },
    "questions": [
        {
            "question": {
                "id": 9,
                "testId": 2,
                "description": "Выберите ссылочные типы:",
                "type": true,
                "points": 5,
                "answers": [
                {
                    "id": 11,
                    "questionId": 9,
                    "value": "MyClass"
                },
                {
                    "id": 12,
                    "questionId": 9,
                    "status": true,
                    "value": "object"
                },
                {
                    "id": 13,
                    "questionId": 9,
                    "value": "int"
                }
            ]
            },
            
        },
        {
            "question": {
                "id": 10,
                "testId": 2,
                "description": "Выберите типы значения:",
                "type": true,
                "points": 5,
                "answers": [
                    {
                        "id": 14,
                        "questionId": 10,
                        "value": "int"
                    },
                    {
                        "id": 15,
                        "questionId": 10,
                        "value": "object"
                    },
                    {
                        "id": 16,
                        "questionId": 10,
                        "value": "char"
                    },
                    {
                        "id": 17,
                        "questionId": 10,
                        "value": "double"
                    }
                ]
            }
        }
    ]
}
```

## 3) GET TEST PROFESSOR: Тест целиком (со всеми вопросами) для страницы редактирования уже существующего теста. 

JSON формат тот же, что и у запроса 2, c добавлением поля статуса в объект ответа
```
    "status": true
```

**GET** запрос на url: 
http://testsbsu.azurewebsites.net/api/tests/1 ,
где 1 - id нужного теста.

## 4) CREATE, UPDATE: сохранение/обновление теста:

**POST/PUT** запрос на url: 
http://testsbsu.azurewebsites.net/api/tests/

## 5) DELETE: удаление теста.

**DELETE** запрос на url: 
http://testsbsu.azurewebsites.net/api/tests/1
где 1 - id нужного теста

## 6) CLOSE/OPEN: закрыть/открыть возможность прохождения теста.

**PUT** запрос на url: 
http://testsbsu.azurewebsites.net/api/tests/1
где 1 - id нужного теста

## 7) GET SUBJECTS: возврат списка предметов с списком тестов в каждом объекте предмета.

**GET** запрос на url: 
http://testsbsu.azurewebsites.net/api/SUBJECTS

## МОДЕЛИ ДЛЯ ОТПРАВКИ В БД:

### Отправка преподавателем при создании, обновлении теста

#### Модель Test:
1) int id
2) Subject subjectObject : *объект предмета целиком*
3) string Name
4) string (nullable) dueDateTime : [формат: '"2007-01-01T00:00:00"'] - *дата и время закрытия возможности прохождения теста*
5) string (nullable) estimatedTime : [формат: '"00:00"]' - *время для прохождения теста*
6) int questionsAmount : *количество вопросов, которое должно показываться у студента для одного теста. ПОКА НЕ ИСПОЛЬЗОВАЛ*
7) int maxMark : *максимальный балл для прохождения теста. ВАЖНО: баллы добавленных вопросов можно сложить так, чтобы набрать 10 баллов. Иначе тест просто не вернется.*
8) bool isOpen : *открыта или закрыта возможность прохождения теста*
9) string creationDate : [формат: '"2007-01-01T00:00:00"']
10) List<Question> questions : *список объектов вопросов, содержащих ответы*

#### Модель Subject
1) int id
2) string Name
3) int subjectTypeId : *тип предмета (лекция, семинар...).*

#### Модель Question
1) int id
2) int testId
3) string description : *тут и нужно писать сам вопрос.*
4) bool type : true - *тест с выбором варианта ответа, false - ввод ответа вручную.*
5) double points : *количество баллов за правильный ответ на вопрос.*
6) List<Answer> answers : *список ответов*

#### Модель Answer
1) int id
2) int questionId
3) bool status : *true - правильный ответ, false - неправильный*
4) string value : *содержание ответа, сам ответ*
