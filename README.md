# TestsBackend

Доступ ко всем методам апи осуществляется через URL http://testsbsu.azurewebsites.net/api/tests

JSON является и входным, и выходным типом данных.

1) GET ALL: Список тестов с объектом предмета SubjectObject.
Тесты возвращаются без вопросов, только их описание (is_open, name...)

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


2) READ: Конкретный тест для страницы с прохождением теста:
ВАЖНО: НЕ ВСЕ ТЕСТЫ МОГУТ БЫТЬ ВОЗВРАЩЕНЫ, т.к. действует алгоритм по сложению баллов за вопросы, а у некоторых тестов может просто не доставать пунктов у вопросов для набора максимальной оценки за тест. Тестируйте на тестах с id = 1,2,3.

GET запрос на URL:
http://testsbsu.azurewebsites.net/api/tests/1
где 1 - id нужного теста

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
                "points": 5
            },
            "answers": [
                {
                    "id": 11,
                    "questionId": 9,
                    "status": true,
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
                    "status": false,
                    "value": "int"
                }
            ]
        },
        {
            "question": {
                "id": 10,
                "testId": 2,
                "description": "Выберите типы значения:",
                "type": true,
                "points": 5
            },
            "answers": [
                {
                    "id": 14,
                    "questionId": 10,
                    "status": true,
                    "value": "int"
                },
                {
                    "id": 15,
                    "questionId": 10,
                    "status": false,
                    "value": "object"
                },
                {
                    "id": 16,
                    "questionId": 10,
                    "status": true,
                    "value": "char"
                },
                {
                    "id": 17,
                    "questionId": 10,
                    "status": true,
                    "value": "double"
                }
            ]
        }
    ]
}
```

3) CREATE, PUT: сохранение/обновление теста:
POST/PUT запрос на url
http://testsbsu.azurewebsites.net/api/tests/

4) DELETE:
DELETE запрос на url:
http://testsbsu.azurewebsites.net/api/tests/1
где 1 - id нужного теста

5) PUT: закрыть/открыть возможность прохождения теста
PUT запрос на url
http://testsbsu.azurewebsites.net/api/tests/1
где 1 - id нужного теста

Модель Test:
1) int id
2) int subjectId
3) string Name
4) DateTime (nullable) dueDateTime
5) DateTime (nullable) estimatedTime
6) int questionsAmount
7) int maxMark
8) bool isOpen
9) DateTime creationDate

Модель Subject
1) int id
2) string Name
3) int subjectTypeId

Модель Question
1) int id
2) int testId
3) string description
4) bool type
5) double points

Модель Answer
1) int id
2) int questionId
3) bool status
4) string value
