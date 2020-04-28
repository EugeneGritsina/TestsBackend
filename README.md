# TestsBackend

Api запускается на локальном IIS сервере с портом 44347: localhost:44347
Доступ ко всем методам апи осуществляется через URL https://localhost:44347/api/tests
JSON является и входным, и выходным типом данных.

1) READ: Тесты, сгруппированные по предмету (для начальной страницы):
GET запрос на URL:
https://localhost:44347/api/tests
Тесты возвращаются без вопросов, только их описание (is_open, name...)

JSON Модель
```
[{
  "subject":{
    "id":1,
    "name":"Технологии Программирования",
    "subjectTypeId":1
  },
  "tests":[{
    "id":1,
    "subjectId":1,
    "name":"Методологии программирования",
    "dueDateTime":null,
    "estimatedTime":null,
    "questionsAmount":3,
    "maxMark":10,
    "isOpen":true
  },
  {
    "id":2,
    "subjectId":1,
    "name":"Ссылочные типы и типы значения",
    "dueDateTime":null,
    "estimatedTime":null,
    "questionsAmount":2,
    "maxMark":10,
    "isOpen":true
  }]
 },{
 "subject":{
    "id":2,
    "name":"Физика",
    "subjectTypeId":1
  },
  "tests":[{
     "id":3,
     "subjectId":2,
     "name":"Формулы",
     "dueDateTime":null,
     "estimatedTime":{
          "hasValue":true,
          "value":{
            "ticks":36000000000,
            "days":0,
            "hours":1,
            "milliseconds":0,
            "minutes":0,
            "seconds":0,
            "totalDays":0.041666666666666664,
            "totalHours":1,
            "totalMilliseconds":3600000,
            "totalMinutes":60,
            "totalSeconds":3600}
            },
          "questionsAmount":3,
          "maxMark":10,
          "isOpen":true
          }]
      },
{
  "subject":{
  "id":3,
  "name":"Дискретная Математика и Математическая Логика",
  "subjectTypeId":1
  },
 "tests":[]
}]
```


2) READ: Конкретный тест для страницы с прохождением / редактированием теста:
GET запрос на URL:
https://localhost:44347/api/tests/1
где 1 - id нужного теста

JSON Модель:
```
{
    "test":{
        "id": 1,
        "subject_id": 1
        ..........
    }
    "questions":
    [{ "question":{
          "id": 1,
          "testId": 1,
          .............
        },
        "question":{
        }
    }]
}
```

3) CREATE: сохранение нового теста:
POST запрос на url:
https://localhost:44347/api/tests/

//пока что я не до конца реализовал добавление теста, т.к. большая морока была принять в нужном формате JSON. Если вам будет изи забабахать нужный мне json, я так же быстро реализую добавление теста

4)DELETE:
DELETE запрос на url:
https://localhost:44347/api/tests/1
где 1 - id нужного теста

put (обновление теста) пока не реализовал, тоже трабл с принятием json


Модель Test:
1) int id
2) int subject_id
3) string DateTime (nullable) dueDateTime
4) TimeSpan (nullable) estimatedTime
5) int questionsAmount
6) int maxMark
7) bool isOpen

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
