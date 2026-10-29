// Языки
const languages = [
    { code: "ru", language: "Русский" },
    { code: "kk", language: "Қазақша" }
];

// Контент
const data = {
    kk: {
        mainPage: "Басты бет",
        authorization: "Авторизация",
        buttonRegister: "Тіркелу",
        buttonLogIn: "Кіру",
        login: "Логин:",
        password: "Құпиясөз",
        enterLoginError: "Логинді енгізіңіз",
        enterPasswordError: "Құпиясөзді енгізіңіз",
        unauthorizedError: "Қате логин немесе құпиясөз",
        registration: "Тіркелу",
        buttonLogIn: "Кіру",
        login: "Логин",
        username: "Есіміңіз",
        forgotPassword: "Құпиясөзді ұмыттыңыз ба?",
        buttonRegister: "Тіркелу",
        enterUsernameError: "Есіміңізді енгізіңіз",
        enterLoginError: "Логинді енгізіңіз",
        enterPasswordError: "Құпиясөзді енгізіңіз",
        loginExistsError: "Мұндай логині бар қолданушы бұрыннан бар",
        usernameExistsError: "Мұндай есімі бар қолданушы бұрыннан бар",
        accountNotFound: "Аккаунт табылмады",
        
        invalidLoginFormat: "Қате логин форматы. Логин келесі талаптарға сай болуы керек:\r\n- ұзындығы 4-тен 30 символға дейін.\r\n- тек кіші латын әріптері мен цифрларды енгізуге рұқсат етіледі.\r\n- логин міндетті түрде кіші әріптен басталуы керек.",
        invalidPasswordFormat: "Қате құпиясөз форматы. Құпиясөз келесі талаптарға сай болуы керек:\r\n- ұзындығы 8-ден 30 символға дейін, бос орындарсыз;\r\n- кем дегенде бір кіші латын әрпінен тұруы керек;\r\n- кем дегенде бір бас латын әрпінен тұруы керек;\r\n- кем дегенде бір цифрдан тұруы керек;\r\n- кем дегенде бір арнайы символ немесе астын сызу белгісі болуы керек (мысалы: !, @, #, $, _, %).",
        invalidUsernameFormat: "Қате қолданушы есімі форматы. Қолданушы есімі келесі талаптарға сай болуы керек:\r\n- ұзындығы 4-тен 30 символға дейін;\r\n- тек латын әріптеріне (бас және кіші), цифрларға және астын сызу белгісіне рұқсат етіледі;\r\n- бос орындар мен арнайы символдарды енгізуге болмайды.",
        successRegistrationLabel: "Сіз жүйеде сәтті тіркелдіңіз! Енді сіз сайтта авторизацияны жүзеге асыра аласыз",
        testSearch: "Іздеу",
        teastSearchPlaceholder: "Іздеу үшін мәтінді теріңіз...",
        takeTest: "Өту",
        link: "Сілтеме",
        menuTitle: "Меню",
        logOutQuestion: "Аккаунттан шығуды растайсыз ба?",
        takeTestQuestion: "Тестіні өтуді растайсыз ба?",
        deleteTestQuestion: "Тестіні жоюды растайсыз ба?",
        completeTestQuestion: "Тестіні аяқтауды растайсыз ба?",
        hasUnansweredQuestion: "Барлық сұрлақтарға жауап берілген жоқ. Тестіні аяқтауды растайсыз ба?",
        deleteTestHistory: "Сіз тест тарихын жоюды растайсыз ба?",
        totalTestCount: "Жалпы тестілер саны",
        correctAnswer: "",
        correctAnswers: "Дұрыс жауаптар",
        noNewReviews: "Жаңа пікірлер жоқ",
        noNewTests: "Жаңа тестілер жоқ",
        accept: "Мақұлдау",
        reject: "Қабылдамау",

        testDeletedSuccess: "Тест жойылды",
        testHistoryDeletedSuccess: "Тест тарихы жойылды",
        questionDeleted: "Сұрақ жойылды",
        yes: "Иә",
        no: "Жоқ",
        testCreating: "Тестіні құру",
        testTitle: "Тест тақырыбы",
        testDescription: "Сипаттамасы",
        testCategory: "Категориясы",
        testImage: "Суреті",
        upload: "Жүктеу",
        next: "Жалғастыру",
        back: "Артқа",
        cancel: "Бас тарту",
        change: "Өзгерту",
        usernamePlaceholder: "Есімді енгізіңіз",
        passwordPlaceholder: "Құпиясөзді енгізіңіз",
        selectedLanguage: "Таңдалған тіл",
        emptyTests: "Мұнда сіз құрған тестілер көрсетіледі.",
        emptyTestHistory: "Мұнда сіз өткен тестілер көрсетіледі.",
        save: "Сақтау",
        clear: "Тазарту",
        enterTitle: "Тест тақырыбын енгізіңіз",
        selectCategory: "Тест категориясын таңдаңыз",
        enterDescription: "Тест сипаттамасын енгізіңіз",
        uploadImage: "Суретті жүктеңіз",
        testNotFound: "Тест табылмады",
        invalidTestIDFormat: "Тестінің қате ID форматы",
        invalidQuestionIdFormat: "Сұрақтың қате ID форматы",
        userDoesNotExist: "Қолданушы жүйеде жоқ",
        completeTheTest: "Аяқтау",
        unfinishedTests: "Аяқталмаған тестілер",
        delete: "Жою",
        continue: "Жалғастыру",
        testCompleted: "Тест аяқталды",
        showTestResults: "Тест нәтижесін көру",
        mainPage: "Басты бет",
        categories: "Категориялар",
        replace: "Алмастыру",
        createTest: "Тестіні құру",
        createdDateTime: "Құрылған күні",
        publishedDateTime: "Жарияланған күні",
        takedDateTime: "Өтілген күні",
        testResult: "Нәтижесі",
        takedDateTime: "Тестінің өтілген күні",
        userScore: "Тестіні өтушінің жинаған балы",
        questionCount: "Сұрақтар саны",
        testsNotFound: "Тестілер табылмады",

        author: "Автор",
        addQuestion: "Сұрақты қосу",
        noQuestions: "Сұрақтар жоқ",
        question: "Сұрақ",
        questions: "Сұрақтар",
        questionText: "Сұрақ мәтіні",
        questionType: "Сұрақ типі",
        deleteQuestion: "Сұрақты жою",
        addAnswer: "Жауапты қосу",
        aiInstruments: "AI құралдары",
        questionGeneration: "Сұрақтарды генерациялау",
        grammarCheck: "Грамматиканы тексеру",
        questionChecking: "Сұрақтарды тексеру",
        translate: "Басқа тілге аудару",
        questionGenerationLoading: "Сұрақтарды генерациялау мақсатында ЖИ-ге қосылу...",
        aiLoading: "ЖИ-ге қосылу...",
        addSelectedQuestions: "Таңдалған сұрақтарды қосу",
        selectAll: "Барлығын таңдау",
        clearSelection: "Таңдауды тазарту",
        questionsAddedSuccess: "Сұрақтар сәтті қосылды",
        yourQuestions: "Сіздің сұрақтарыңыз",
        aiQuestions: "ЖИ сұрақтары",
        categoryNotSelected: "Таңдалмады",
        check: "Тексеру",
        publishTest: "Тестіні жариялау",
        uncategorized: "Категориясыз",
        aiAnswer: "ЖИ жауабы",
        selectUserQuestions: "Пайдаланушы сұрақтарын таңдау",
        selectAiQuestions: "ЖИ сұрақтарын таңдау",

        usernameChangedSuccess: "Қолданушы есімі сәтті өзгерді",
        passwordChangedSuccess: "Қолданушы құпиясөзі сәтті өзгерді",
        samePassword: "Жаңа құпиясөзді енгізіңіз",
        chooseAnotherLanguage: "Басқа тілді таңдаңыз",
        languageChangedSuccess: "Қолданушы тілі сәтті өзгерді",
        testPublishedSuccess: "Сіздің тестіңіз сәтті жарияланды және оны басқа пайдаланушылар өте алады. Бірақ тест әкімшімен тексерілмеді.",
        reviewCreatedSuccess: "Сіздің пікіріңіз сәтті құрылды және модерацияны күтуде",

        testChecked: "Тексерілді",
        open: "Ашу",
        openTest: "Тестіні ашу",
        takeTestToLeaveReview: "Пікір қалдыру үшін алдымен тестіні өтіңіз",
        alreadyReviewed: "Сіз пікір қалдырғансыз",
        canPublishReview: "Сіз өз пікіріңізді қалдыра аласыз",
        writeReview: "Пікір жазу",
        review: "Пікір",
        publishReview: "Жариялау",
        askAi: "ЖИ-ден сұрау",
        cannotReviewMyTest: "Сіз өзіңіздің тестіңізге пікір қалдыра алмайсыз",

        questionExistsError: "Мұндай сұрақ тестіде бар",
        answerExistsError: "Мұндай жауап сұрақта бар",
        selectCorrectAnswer: "Кемінде бір дұрыс жауапты таңдаңыз",
        checkedMoreButtonsError: "Екі немесе одан көп радиобатырмалар таңдалды",
        reviews: "Пікірлер",
        noReviews: "Пікірлер жоқ",
        linkCopiedSuccess: "Сілтеме сәтті көшірілді",
        linkCopyError: "Сілтемені көшіру барысында қателік орын алды",
        rating: "Рейтингісі",
        status: "Статусы",
        checkedByAdmin: "Әкімшімен тексерілді",
        uncheckedByAdmin: "Әкімшімен тексерілмеді",
        allCategories: "Барлық категориялар",
        deleteCurrentTestQuestion: "Сіз ағымдағы тестіні жоюды растайсыз ба?",
        deleteTestChanges: "Сіз ағымдағы тестідегі өзгерістерді жоюды растайсыз ба?",
        publishTestQuestion: "Сіз ағымдағы тестіні жариялауды растайсыз ба?",

        statistics: "Статистикалық деректер",
        testCount: "Тестілер",
        categoryCount: "Категориялар",
        reviewCount: "Пікірлер",
        userCount: "Пайдаланушылар",
        testsWithCategory: "Категориясы бар тестілер",
        newTests: "Жаңа тестілер",
        newReviews: "Жаңа пікірлер",

        "answer": "Жауап",
        "correctAnswer": "Дұрыс жауап",

        "my-tests": "Менің тестілерім",
        "test-history": "Тестілер тарихы",
        "personal-data": "Жеке деректер",
        "parameters": "Баптаулар",
        "log-out": "Шығу",
        "radio": "Біржауапты сұрақ",
        "checkbox": "Көпжауапты сұрақ",

        "checked": "Тексерілді",
        "published": "Жарияланды",
        "unfinished": "Аяқталмады",
        "rejected": "Қабылданбады"
    },
    ru: {
        mainPage: "Главная",


        authorization: "Авторизация",
        buttonRegister: "Зарегистрироваться",
        buttonLogIn: "Войти",
        login: "Логин",
        password: "Пароль",
        unauthorizedError: "Неверный логин или пароль",
        registration: "Регистрация",
        buttonLogIn: "Войти",
        username: "Имя",
        forgotPassword: "Забыли пароль?",
        buttonRegister: "Зарегистрироваться",
        enterUsernameError: "Введите имя",
        enterLoginError: "Введите логин",
        enterPasswordError: "Введите пароль",
        loginExistsError: "Пользователь с таким логином уже существует",
        usernameExistsError: "Пользователь с таким именем уже существует",
        accountNotFound: "Аккаунт не найден",

        invalidLoginFormat: "Некорректный формат логина. Логин должен соответствовать следующим требованиям:\r\n- длина от 4 до 30 символов.\r\n- разрешены только строчные (маленькие) латинские буквы и цифры.\r\n- логин должен начинаться со строчной буквы.",
        invalidPasswordFormat: "Некорректный формат пароля. Пароль должен соответствовать следующим требованиям:\r\n- длина от 8 до 30 символов без пробелов.\r\n- должен содержать хотя бы одну строчную букву латинскую букву;\r\n- должен содержать хотя бы одну заглавную латинскую букву;\r\n- должен содержать хотя бы одну цифру;\r\n- должен содержать хотя бы один специальный символ или знак подчеркивания (например: !, @, #, $, _, %).",
        invalidUsernameFormat: "Некорректный формат имени пользователя. Имя пользователя должно соответствовать следующим требованиям:\r\n- длина от 4 до 30 символов;\r\n- разрешены только латинские буквы (заглавные и строчные), цифры и знак подчеркивания;\r\n- пробелы и специальные символы не допускаются.",
        successRegistrationLabel: "Вы успешно зарегистрировались! Теперь вы можете осуществить авторизацию на сайте",
        testSearch: "Поиск",
        teastSearchPlaceholder: "Введите текст для поиска...",
        takeTest: "Пройти",
        link: "Ссылка",
        menuTitle: "Меню",
        logOutQuestion: "Вы действительно хотите выйти из аккаунта?",
        takeTestQuestion: "Вы действительно хотите начать прохождение теста?",
        deleteTestQuestion: "Вы действительно хотите удалить тест?", 
        completeTestQuestion: "Вы действительно хотите завершить тест?",
        hasUnansweredQuestion: "Не все вопросы отвечены. Вы действительно хотите завершить тест?",
        deleteTestHistory: "Вы действительно хотите удалить историю теста?",
        totalTestCount: "Общее количество тестов",
        correctAnswers: "Правильные ответы",
        noNewReviews: "Нет новых отзывов",
        noNewTests: "Нет новых тестов",
        accept: "Одобрить",
        reject: "Отклонить",

        testDeletedSuccess: "Тест был удален",
        testHistoryDeletedSuccess: "История теста удалена",
        questionDeleted: "Вопрос был удален",
        yes: "Да",
        no: "Нет",
        testCreating: "Создание теста",
        testTitle: "Заголовок теста",
        testDescription: "Описание",
        testCategory: "Категория",
        testImage: "Изображение:",
        upload: "Загрузить",
        next: "Далее",
        back: "Назад",
        cancel: "Отмена",
        change: "Изменить",
        usernamePlaceholder: "Введите имя",
        passwordPlaceholder: "Введите пароль",
        selectedLanguage: "Выбранный язык",
        emptyTests: "Здесь будут отображаться созданные вами тесты.",
        emptyTestHistory: "Здесь будут отображаться пройденные вами тесты.",
        save: "Сохранить",
        clear: "Очистить",
        enterTitle: "Введите заголовок теста",
        selectCategory: "Выберите категорию теста",
        enterDescription: "Введите описание теста",
        uploadImage: "Загрузите изображение",
        testNotFound: "Тест не найден",
        invalidTestIDFormat: "Некорректный формат ID теста",
        invalidQuestionIdFormat: "Некорректный формат ID вопроса",
        userDoesNotExist: "Пользователь не существует",
        completeTheTest: "Завершить",
        unfinishedTests: "Незавершенные тесты",
        delete: "Удалить",
        continue: "Продолжить",
        testCompleted: "Тест завершен",
        showTestResults: "Посмотреть результаты тестов",
        mainPage: "Главная страница",
        categories: "Категории",
        replace: "Заменить",
        createTest: "Создать тест",
        createdDateTime: "Дата создания",
        publishedDateTime: "Дата публикации",
        takedDateTime: "Дата прохождения",
        testResult: "Результат",
        takedDateTime: "Дата прохождения теста",
        userScore: "Баллы, набранные пользователем",
        questionCount: "Количество вопросов",
        testsNotFound: "Тесты не найдены",

        author: "Автор",
        addQuestion: "Добавить вопрос",
        noQuestions: "Вопросы отсутствуют",
        question: "Вопрос",
        questions: "Вопросы",
        questionText: "Текст вопроса",
        questionType: "Тип вопроса",
        deleteQuestion: "Удалить вопрос",
        addAnswer: "Добавить ответ",
        aiInstruments: "AI-инструменты",
        questionGeneration: "Генерация вопросов",
        grammarCheck: "Проверка грамматики",
        questionChecking: "Проверка вопросов",
        translate: "Перевод на другой язык",
        questionGenerationLoading: "Подключение к ИИ для генерации вопросов...",
        aiLoading: "Подключение к ИИ...",
        addSelectedQuestions: "Добавить выбранные вопросы",
        selectAll: "Выбрать все",
        clearSelection: "Очистить выбор",
        questionsAddedSuccess: "Вопросы были успешно добавлены",
        yourQuestions: "Ваши вопросы",
        aiQuestions: "Вопросы ИИ",
        categoryNotSelected: "Не выбрана",
        check: "Проверить",
        publishTest: "Опубликовать тест",
        uncategorized: "Без категории",
        aiAnswer: "Ответ ИИ",
        selectUserQuestions: "Выбрать вопросы пользователя",
        selectAiQuestions: "Выбрать вопросы ИИ",

        usernameChangedSuccess: "Имя пользователя было успешно изменено",
        passwordChangedSuccess: "Пароль пользователя был успешно изменен",
        samePassword: "Введите новый пароль",
        chooseAnotherLanguage: "Выберите другой язык",
        languageChangedSuccess: "Язык пользователя был успешно изменен",
        testPublishedSuccess: "Ваш тест успешно опубликован и его могут проходить другие пользователи. Но тест не проверен администратором.",
        reviewCreatedSuccess: "Ваш отзыв успешно создан и ожидает модерации.",

        testChecked: "Проверен",
        open: "Открыть",
        openTest: "Открыть тест",
        takeTestToLeaveReview: "Пройдите тест, чтобы написать отзыв",
        alreadyReviewed: "Вы уже оставили отзыв",
        canPublishReview: "Вы можете оставить свой отзыв",
        writeReview: "Написать отзыв",
        review: "Отзыв", 
        publishReview: "Опубликовать",
        askAi: "Спросить у ИИ",
        cannotReviewMyTest: "Вы не можете оценивать свои тест",

        questionExistsError: "Такой вопрос содержится в тесте",
        answerExistsError: "Такой ответ содержится в вопросе",
        selectCorrectAnswer: "Выберите как минимум один правильный ответ",
        checkedMoreButtonsError: "Отмечены две или более радиокнопки",
        reviews: "Отзывы",
        noReviews: "Нет отзывов",
        linkCopiedSuccess: "Ссылка успешно скопирована",
        linkCopyError: "Произошла ошибка при копировании",
        rating: "Рейтинг",
        status: "Статус",
        checkedByAdmin: "Проверен администратором",
        uncheckedByAdmin: "Не проверен",
        allCategories: "Все категории",
        deleteCurrentTestQuestion: "Вы действительно хотите сбросить текущий тест?",
        deleteTestChanges: "Вы действительно хотите сбросить изменения в текущем тесте?",
        publishTestQuestion: "Вы действительно хотите опубликовать текущий тест?",

        statistics: "Статистические данные",
        testCount: "Тестов",
        categoryCount: "Категории",
        reviewCount: "Отзывы",
        userCount: "Пользователи",
        testsWithCategory: "Тесты с категориями",
        newTests: "Новые тесты",
        newTests: "Новые отзывы",

        "answer": "Ответ",
        "correctAnswer": "Верный ответ",
        "my-tests": "Мои тесты",
        "test-history": "История тестов",
        "personal-data": "Личные данные",
        "parameters": "Параметры",
        "log-out": "Выход",
        "radio": "С единичным выбором",
        "checkbox": "Со множественным выбором",

        "checked": "Проверен",
        "published": "Опубликован",
        "unfinished": "Не завершен",
        "rejected": "Отклонен"
    },
};

// Элементы меню аккаунта пользователя
const user_menu_elements = [
    "my-tests", "test-history", "personal-data", "parameters", "log-out"
];

// Элементы меню аккаунта администратора
const admin_menu_elements = [
    "personal-data", "parameters", "log-out"
];

// Ссылка на главную страницу
function getLinkToMainPage(isAuthorized = true) {
    const project_name = document.createElement("a");
    project_name.id = "project-name";
    project_name.text = "Testiler.kz";
    if (isAuthorized) {
        project_name.href = "/home/main";
    }
    else {
        project_name.href = "/";
    }
    return project_name;
}

// Контейнеры сообщений
const message_box_elements = new Array();

// Показать сообщение
function showMessageBox(message, message_type = "error", milliseconds = 3000) {
    // Удалить все сообщения
    message_box_elements.forEach((x) => {
        x.remove();
    });
    message_box_elements.length = 0;

    // Контейнер сообщения
    const message_box = document.createElement("div");
    message_box.className = "message-box";
    message_box_elements.push(message_box);
    document.startViewTransition(() => {
        document.body.appendChild(message_box);
    });

    // Иконка сообщения
    const icon_message = document.createElement("label");
    if (message_type === "success") {
        message_box.className += " success";
        icon_message.className = "animation fa-regular fa-circle-check fa-bounce";
        icon_message.style = "color: white;";
    }
    else if (message_type === "error") {
        message_box.className += " error";
        icon_message.className = "animation fa-solid fa-circle-xmark fa-bounce";
        icon_message.style = "color: white";
    }
    else {
        icon_message.className = "animation fa-solid fa-circle-info fa-bounce";
        icon_message.style = "color: white";
    }
    message_box.appendChild(icon_message);

    // Текст сообщения
    const label_message = document.createElement("label");
    label_message.textContent = message;
    message_box.appendChild(label_message);

    // Таймер для удаления сообщения
    setTimeout(() => {
        document.startViewTransition(() => {
            message_box.remove();
        });
    }, milliseconds);
}

// Показать встроенное сообщение
function getInlineMessage(message, message_type = "info") {
    // Контейнер сообщения
    const div_inline_message = document.createElement("div");
    div_inline_message.className = "inline-message " + message_type;

    // Контейнер иконки и сообщения
    const div_message_icon = document.createElement("div");
    div_message_icon.className = "message-icon";
    div_inline_message.appendChild(div_message_icon);

    // Иконка
    const inline_message_icon = document.createElement("div");

    // Тип сообщения
    if (message_type === "success") {
        inline_message_icon.className = "animation fa-regular fa-circle-check fa-2x fa-bounce";
        inline_message_icon.style = "color: green;";
    }
    else if (message_type === "error") {
        inline_message_icon.className = "animation fa-solid fa-circle-xmark fa-2x fa-bounce";
        inline_message_icon.style = "color: red;";
    }
    else {
        inline_message_icon.className = "animation fa-solid fa-circle-info fa-2x fa-bounce";
        inline_message_icon.style = "color: purple;";
    }
    div_message_icon.appendChild(inline_message_icon);

    // Сообщение
    const success_registration_label = document.createElement("label");
    success_registration_label.textContent = message;
    div_message_icon.appendChild(success_registration_label);

    return div_inline_message;
}

// Кнопка личного аккаунта пользователя
function divMenuAccount(lang, username, isUser = true) {
    // Меню
    const div_menu = document.createElement("div");
    div_menu.id = "menu";

    // Кнопка
    const button_username = document.createElement("button");
    button_username.id = "button-username";
    button_username.textContent = username;
    button_username.addEventListener("click", () => {
        document.getElementById("submenu").classList.toggle('show');
    });
    div_menu.appendChild(button_username);

    // Подменю
    const div_submenu = document.createElement("div");
    div_submenu.id = "submenu";
    div_menu.appendChild(div_submenu);

    // Элементы меню
    var menu_elements = null;

    if (isUser) {
        menu_elements = user_menu_elements;
    }
    else {
        menu_elements = admin_menu_elements;
    }

    menu_elements.forEach(x => {
        const div_submenu_element = document.createElement("div");
        div_submenu_element.id = x;
        div_submenu_element.textContent = data[lang][x];
        div_submenu.appendChild(div_submenu_element);
    });

    // Навигация меню
    div_submenu.addEventListener("click", (event) => {
        // Выход из меню
        if (event.target && event.target.id === "log-out") {
            const modal = dialogLogOut(lang);
            document.body.appendChild(modal);
            document.startViewTransition(() => modal.showModal());
        }
        // Пункты меню
        else if (event.target && menu_elements.find(x => x === event.target.id)) {
            window.location.href = "/account/" + event.target.id;
        }
    });

    return div_menu;
}

// Диалог выхода из аккаунта
function dialogLogOut(lang) {
    // Модальное окно
    const modal = document.createElement("dialog");

    // Содержимое элемента
    modal.innerHTML = `
        <!--Основной контент-->
        <div class="content">
            <!--Текст-->
            <label>${data[lang]?.logOutQuestion ?? ""}</label>
            <!--Кнопки-->
            <div class="buttons">
                <!--Кнопка "Да"-->
                <button id="button-confirm">${data[lang]?.yes ?? ""}</button
                <!--Кнопка "Нет"-->
                <button id="button-decline">${data[lang]?.no ?? ""}</button>
            </div>
        </div>
    `;

    // Удаление окна из DOM
    modal.addEventListener("close", () => {
        document.startViewTransition(() => {
            modal.remove();
        });
    });

    // Назначение кнопок
    modal.addEventListener("click", (event) => {
        // Да
        if (event.target && event.target.id === "button-confirm") {
            window.location.replace("/logout");
            localStorage.setItem("lang", "kk");
        }
        // Нет
        else if (event.target && event.target.id === "button-decline") {
            document.startViewTransition(() => modal.close());
        }
    });

    return modal;
}

// Диалог отображения изображения
function dialogShowImage(img) {
    // Модальное окно
    const modal = document.createElement("dialog");
    modal.style = "width: 80%; height: 80%;";

    // Удаление окна из DOM
    modal.addEventListener("close", () => {
        document.startViewTransition(() => {
            modal.remove();
        });
    });

    // Основной контент
    const div_image_content = document.createElement("div");
    div_image_content.className = "dialog-image-content";
    modal.appendChild(div_image_content);

    // Контейнер изображения
    const div_dialog_image_container = document.createElement("div");
    div_dialog_image_container.className = "dialog-image-container";
    div_image_content.appendChild(div_dialog_image_container);

    // Изображение
    div_dialog_image_container.appendChild(img);

    // Кнопка "Закрыть"
    const div_circle_close = document.createElement("div");
    div_circle_close.className = "circle-close fa-solid fa-circle-xmark fa-2x";
    div_circle_close.addEventListener("click", () => {
        document.startViewTransition(() => modal.close());
    });
    div_image_content.appendChild(div_circle_close);

    return modal;
}

// Получить язык учетной записи
async function getSelectedLanguage() {
    const response = await fetch("/api/language");
    const account = await response.json();
    const lang = account.selectedLanguage;
    if (!lang) {
        lang = 'kk';
    }
    return lang;
}

// Получить имя учетной записи
async function getUsername() {
    const response = await fetch("/api/username");
    const account = await response.json();
    const username = account.username;
    return username.length > 10 ? username.substring(0, 10) + "..." : username;
}

// Загрузка категорий
async function getCategories() {
    const response = await fetch(`/api/categories`);
    const categories = await response.json();
    return categories;
}

// Получить дату в правильном формате
function getDate(date) {
    return new Date(date).toLocaleString("ru-RU", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit"
    });
}

// Получить идентификатор теста
function getTestId() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    return urlParams.get("testId");
}

// Получить идентификатор истории теста
function getTestHistoryId() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    return urlParams.get("testHistoryId");
}

// Показать результаты теста
function showTestResults(lang, max_score, score) {
    if (lang === "kk") {
        return `Сіздің нәтижеңіз: ${max_score} баллдан ${score} балл.`;
    }
    else if (lang === "ru") {
        return `Ваш результат: ${score} баллов из ${max_score}.`;
    }
}

// Баллы за вопрос
function showQuestionScore(lang, score, maxScore) {
    if (lang === "kk") {
        return `Балдар: ${maxScore} баллдан ${score} балл`;
    }
    else if (lang === "ru") {
        return `Баллы: ${score} из ${maxScore}`;
    }
}

// Допустимые расширения изображений
const allowed_image_extensions = ['image/jpeg', 'image/png', 'image/webp'];

// Получить расширение файла
function getExtension(fileName) {
    return fileName.split('.').pop().toLowerCase();
}

// Типы вопросов
const questionTypes = ["radio", "checkbox"];

// Отобразить рейтинг
function showStarRating(rating) {
    const stars = document.createElement("div");
    stars.style = "vertical-align: middle; position: relative; display: inline-flex;";

    // 1. Пустые звезды
    const empty_stars = document.createElement("div");
    empty_stars.style = "display: flex; flex-direction: row;";

    for (let i = 0; i < 5; i++) {
        const star = document.createElement("i");
        star.className = "fa-regular fa-star";
        empty_stars.appendChild(star);
    }

    // 2. Закрашенные звезды
    const painted_stars = document.createElement("div");
    const percentage = (rating / 5) * 100;
    const cutOff = 100 - percentage;
    painted_stars.style = `
        position: absolute;
        width: 100%; 
        clip-path: inset(0 ${cutOff}% 0 0);
        display: flex; 
        flex-direction: row;
        vertical-align: center;
    `;

    for (let i = 0; i < 5; i++) {
        const star = document.createElement("i");
        star.className = "fa-solid fa-star";
        painted_stars.appendChild(star);
    }

    stars.appendChild(empty_stars);
    stars.appendChild(painted_stars);

    return stars;
}

// Скопировать текущую ссылку
function copyCurrentLink(lang) {
    const currentUrl = window.location.href;

    navigator.clipboard.writeText(currentUrl)
        .then(() => {
            showMessageBox(data[lang]?.linkCopiedSuccess ?? "", "success");
        })
        .catch(() => {
            showMessageBox(data[lang]?.linkCopyError, "error");
        });
}

// Отобразить тест
function showTest(test, lang, isUser = true) {
    // Контейнер теста
    const div_test = document.createElement("div");
    div_test.className = "test-menu";

    // Контейнер изображения теста
    const div_image_container = document.createElement("div");
    div_image_container.className = "image-container";
    div_test.appendChild(div_image_container);

    // Изображение теста
    const image = document.createElement("img");
    image.src = `/api/test-image/${test.imageFileName}`;
    div_image_container.appendChild(image);

    // Заголовок теста
    const title = document.createElement("label");
    title.className = "title";
    title.textContent = test.title;
    div_test.appendChild(title);

    // КОНТЕЙНЕР ДАТЫ
    const div_date = document.createElement("div");
    div_date.style = "display: flex; gap: 10px; justify-content: flex-end;";
    div_test.appendChild(div_date);
    // 1. Иконка даты
    const icon_date = document.createElement("div");
    icon_date.className = "fa-solid fa-calendar";
    div_date.appendChild(icon_date);
    if (isUser) {
        // 2. Дата публикации теста
        const date = document.createElement("label");
        date.textContent = getDate(test.publishedDateTime);
        div_date.appendChild(date);
    }
    else {
        // 2. Дата создания теста
        const date = document.createElement("label");
        date.textContent = getDate(test.createdDateTime);
        div_date.appendChild(date);
    }

    // КОНТЕЙНЕР АВТОРА
    const div_author = document.createElement("div");
    div_author.style = "display: flex; gap: 10px; justify-content: flex-end;";
    div_test.appendChild(div_author);
    // 1. Иконка автора
    const icon_author = document.createElement("div");
    icon_author.className = "fa-solid fa-user-pen";
    div_author.appendChild(icon_author);
    // 2. Автор
    const author = document.createElement("a");
    author.textContent = test.username;
    div_author.appendChild(author);

    // КОНТЕЙНЕР РЕЙТИНГА
    const div_rating = document.createElement("div");
    div_rating.style = "display: flex; gap: 10px; align-items: center; justify-content: flex-end;";
    div_test.appendChild(div_rating);

    if (isUser) {
        // Звезды
        const div_star_rating = showStarRating(test.averageRating);
        div_rating.appendChild(div_star_rating);

        // Отзывы отсутствуют
        if (test.reviewsCount == 0) {
            const label_no_reviews = document.createElement("label");
            label_no_reviews.textContent = data[lang]?.noReviews ?? "";
            label_no_reviews.style = "font-style: italic;";
            div_test.appendChild(label_no_reviews);
        }
        else {
            // Рейтинг в цифрах
            const label_rating = document.createElement("label");
            label_rating.textContent = test.averageRating.toFixed(1);
            div_rating.appendChild(label_rating);

            // КОНТЕЙНЕР КОЛИЧЕСТВА ОТЗЫВОВ
            const div_reviews_count = document.createElement("div");
            div_reviews_count.style = "display: flex; gap: 10px; justify-content: flex-end;";
            div_test.appendChild(div_reviews_count);

            // 1. Иконка количества отзывов
            const icon_reviews_count = document.createElement("div");
            icon_reviews_count.className = "fa-solid fa-comments";
            div_reviews_count.appendChild(icon_reviews_count);

            // 2. Количество отзывов
            const label_comments_count = document.createElement("label");
            label_comments_count.textContent = test.reviewsCount;
            div_reviews_count.appendChild(label_comments_count);
        }
    }

    // Отобразить галочку, если тест проверен
    if (test.status.toLowerCase() === "checked") {

        // Контейнер
        const div_checked = document.createElement("div");
        div_checked.className = "checked-container";
        div_image_container.appendChild(div_checked);

        // Галочка
        const icon_checked = document.createElement("div");
        icon_checked.className = "fa-regular fa-circle-check";
        icon_checked.style.color = "white";
        div_checked.appendChild(icon_checked);

        // Надпись
        const label_checked = document.createElement("label");
        label_checked.textContent = data[lang]?.testChecked ?? "";
        label_checked.style = "align-self: left;";
        div_checked.appendChild(label_checked);
    }

    // Кнопка "Открыть" для пользователя
    if (isUser) {
        const button_open_test = document.createElement("button");
        button_open_test.className = "small wide";
        button_open_test.textContent = data[lang]?.open.toUpperCase() ?? "";
        button_open_test.addEventListener("click", () => {
            window.location.href = `/info?testId=${test.id}`;
        });
        div_test.appendChild(button_open_test);
        // Иконка кнопки "Открыть"
        const icon_open_test = document.createElement("div");
        icon_open_test.className = "fa-solid fa-book-open";
        button_open_test.appendChild(icon_open_test);
    }

    return div_test;
}

// Получить результаты теста
function getTestResults(test) {
    const result = test.score / test.maxScore * 100;
    return result.toFixed(2);
}

// Оценить тест
function rateTest(onSelect) {
    const container = document.createElement("div");
    container.style = "display: inline-flex; flex-direction: row; gap: 4px; cursor: pointer;";

    // Текущая оценка
    let currentSelectedRating = 0;
    const starElements = [];

    // Пустые звезды (5)
    for (let i = 1; i <= 5; i++) {
        const star = document.createElement("i");
        star.className = "fa-regular fa-star";
        // Индексы звезд
        star.dataset.value = i;

        // Подсветка звезд при наведении
        star.addEventListener("mouseenter", () => {
            highlightStars(i);
        });

        // 2. Фиксация оценки
        star.addEventListener("click", () => {
            currentSelectedRating = i;
            highlightStars(i);
            if (typeof onSelect === "function") {
                onSelect(currentSelectedRating);
            }
        });

        starElements.push(star);
        container.appendChild(star);
    }

    // Возвращение поставленной оценки 
    container.addEventListener("mouseleave", () => {
        highlightStars(currentSelectedRating);
    });

    // Покраска звезд
    function highlightStars(count) {
        starElements.forEach((star, index) => {
            if (index < count) {
                star.className = "fa-solid fa-star";
            } else {
                star.className = "fa-regular fa-star";
            }
        });
    }

    return container;
}

// Вопросы пользователя
const user_questions = [
    {
        "id": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792",
        "text": "5 + 5 = ?",
        "type": 0,
        "answers": [
            {
                "id": "2b4cf3ea-83fb-452c-8012-52cf6db785de",
                "text": "10",
                "isCorrect": true,
                "order": 1,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "a35276e4-5a6b-4e07-8beb-fde86155f533",
                "text": "15",
                "isCorrect": false,
                "order": 2,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "ed2d3b68-992d-4244-ad51-879090673eb5",
                "text": "5",
                "isCorrect": false,
                "order": 3,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "7ede9919-fd2c-4037-8fec-30b131186132",
                "text": "20",
                "isCorrect": false,
                "order": 4,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            }
        ],
        "order": 2,
        "testId": "3ecfa396-2f4e-40f9-8bbf-9a4d9fcb1d3f"
    },
    {
        "id": "a6c87695-4ba4-4444-b285-1ad3439e05fa",
        "text": "8 - 3 = ?",
        "type": 0,
        "answers": [
            {
                "id": "a02a1ab9-a0f7-46a1-a0b3-b3e5480f1601",
                "text": "4",
                "isCorrect": false,
                "order": 1,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "0867f11b-b0eb-4ed1-8748-4557026ab6b2",
                "text": "5",
                "isCorrect": true,
                "order": 2,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "1d955cef-8c66-4de9-95b8-e1744daad5f3",
                "text": "6",
                "isCorrect": false,
                "order": 3,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "6873c529-ad8d-4e8f-adca-a4b504eef75a",
                "text": "3",
                "isCorrect": false,
                "order": 4,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            }
        ],
        "order": 5,
        "testId": "3ecfa396-2f4e-40f9-8bbf-9a4d9fcb1d3f"
    },
];

// Сгенерированные вопросы
const ai_questions = [
    {
        "id": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792",
        "text": "Сколько будет 5 + 5 = ?",
        "type": 1,
        "answers": [
            {
                "id": "2b4cf3ea-83fb-452c-8012-52cf6db785de",
                "text": "10",
                "isCorrect": true,
                "order": 1,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "a35276e4-5a6b-4e07-8beb-fde86155f533",
                "text": "6 + 4",
                "isCorrect": true,
                "order": 2,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "ed2d3b68-992d-4244-ad51-879090673eb5",
                "text": "5",
                "isCorrect": false,
                "order": 3,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            },
            {
                "id": "7ede9919-fd2c-4037-8fec-30b131186132",
                "text": "20",
                "isCorrect": false,
                "order": 4,
                "questionId": "cd28bb5d-6d53-4b5b-9d2b-f0e9725d7792"
            }
        ],
        "order": 2,
        "testId": "3ecfa396-2f4e-40f9-8bbf-9a4d9fcb1d3f"
    },
    {
        "id": "a6c87695-4ba4-4444-b285-1ad3439e05fa",
        "text": "Сколько будет 8 - 3 = ?",
        "type": 1,
        "answers": [
            {
                "id": "a02a1ab9-a0f7-46a1-a0b3-b3e5480f1601",
                "text": "2 + 3",
                "isCorrect": true,
                "order": 1,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "0867f11b-b0eb-4ed1-8748-4557026ab6b2",
                "text": "5",
                "isCorrect": true,
                "order": 2,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "1d955cef-8c66-4de9-95b8-e1744daad5f3",
                "text": "6",
                "isCorrect": false,
                "order": 3,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            },
            {
                "id": "6873c529-ad8d-4e8f-adca-a4b504eef75a",
                "text": "3",
                "isCorrect": false,
                "order": 4,
                "questionId": "a6c87695-4ba4-4444-b285-1ad3439e05fa"
            }
        ],
        "order": 5,
        "testId": "3ecfa396-2f4e-40f9-8bbf-9a4d9fcb1d3f"
    },
]