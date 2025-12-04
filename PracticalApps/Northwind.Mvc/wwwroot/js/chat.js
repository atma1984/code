"use strict"; // Включение строгого режима, чтобы выявить ошибки и предотвратить использование невалидных конструкций в JavaScript.
var connection = new signalR.HubConnectionBuilder()  // Создание нового объекта соединения с SignalR, используя класс HubConnectionBuilder.
    .withUrl("/chat")// Указываем URL хаба, с которым будет установлено соединение. Это путь на сервере для чата (например, /chat).
    .build();   // Строим объект соединения после указания всех параметров (в данном случае только URL).
document.getElementById("registerButton").disabled = true; // Отключаем кнопку регистрации (кнопка с id "registerButton"), чтобы она была недоступна для пользователя по умолчанию.
document.getElementById("sendButton").disabled = true; // Отключаем кнопку отправки сообщения (кнопка с id "sendButton"), чтобы она была недоступна для пользователя по умолчанию.
connection.start().then(function () {  // Начинаем соединение с сервером через SignalR.
    document.getElementById("registerButton").disabled = false;  // Когда соединение установлено, включаем кнопку регистрации.
    document.getElementById("sendButton").disabled = false;  // Когда соединение установлено, включаем кнопку отправки сообщений.
}).catch(function (err) {  // Если соединение не удалось установить (ошибка), выводим ошибку в консоль.
    return console.error(err.toString());
});
connection.on("ReceiveMessage", function (received) {  // Обрабатываем событие, когда сервер отправляет сообщение ("ReceiveMessage").
    var li = document.createElement("li"); // Создаем новый элемент списка <li> для отображения сообщения.
    document.getElementById("messages").appendChild(li);  // Добавляем этот элемент списка в контейнер с id "messages", который, вероятно, является <ul> или <ol>.
    // обратите внимание на использование обратной кавычки `,
    // чтобы включить форматированную строку
    li.textContent = // Устанавливаем текст внутри элемента <li> в следующее сообщение.
        `${received.from} says ${received.body} (sent to ${received.to})`; // Формируем строку, которая будет отображаться: кто отправил, что отправил, и кому.
});
document.getElementById("registerButton").addEventListener("click",  // Добавляем обработчик событий на клик по кнопке регистрации.
    function (event) {  // Когда кнопка будет нажата, выполняется эта функция.
        var registermodel = {  // Создаем объект, который содержит данные, которые будут отправлены на сервер для регистрации.
            username: document.getElementById("from").value,  // Берем значение поля ввода с id "from" (имя пользователя).
            groups: document.getElementById("groups").value  // Берем значение поля ввода с id "groups" (группа).
        };
        connection.invoke("Register", registermodel)  // Вызываем метод "Register" на сервере через SignalR, передавая объект с данными.
            .catch(function (err) {  // Если при вызове метода произошла ошибка, выводим её в консоль.
                return console.error(err.toString());
            });
        event.preventDefault();  // Предотвращаем стандартное поведение кнопки (например, перезагрузку страницы).
    });
document.getElementById("sendButton").addEventListener("click",  // Добавляем обработчик событий на клик по кнопке отправки сообщения.
    function (event) {  // Когда кнопка будет нажата, выполняется эта функция.
        var messageToSend = {  // Создаем объект с данными для отправки сообщения.
            to: document.getElementById("to").value,  // Получаем значение поля ввода с id "to" (кому отправить).
            toType: document.getElementById("toType").value,  // Получаем значение поля ввода с id "toType" (тип получателя).
            from: document.getElementById("from").value,  // Получаем значение поля ввода с id "from" (от кого отправляется).
            body: document.getElementById("body").value  // Получаем значение поля ввода с id "body" (текст сообщения).
        };
        connection.invoke("SendMessage", messageToSend)  // Вызываем метод "SendMessage" на сервере через SignalR, передавая объект с сообщением.
            .catch(function (err) {  // Если при вызове метода произошла ошибка, выводим её в консоль.
                return console.error(err.toString());
            });
        event.preventDefault();  // Предотвращаем стандартное поведение кнопки (например, перезагрузку страницы).
    });