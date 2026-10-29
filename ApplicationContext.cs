using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Testiler_KZ {
    public class ApplicationContext : DbContext {

        // Учетные записи
        public DbSet<Account> Accounts { get; set; }

        // Категории тестов
        public DbSet<Category> Categories { get; set; }

        // Тесты
        public DbSet<Test> Tests { get; set; }

        // История тестов
        public DbSet<TestHistory> TestHistories { get; set; }

        // Отзывы
        public DbSet<Review> Reviews { get; set; }

        // Учетные записи
        public static readonly Account[] accounts = {
            new Account(Guid.NewGuid(), "olzhas05", ".vAx/vaEz?p9W", "Path2Success", (int)Roles.user, DateTime.Now, new Parameters("ru")),
            new Account(Guid.NewGuid(), "arman04", "5–tR2h8h#=ZsF", "Mentor", (int)Roles.user, DateTime.Now, new Parameters("ru")),
            new Account(Guid.NewGuid(), "kairat03", ";dn3E*{ApfCRa", "AClass", (int)Roles.user, DateTime.Now, new Parameters("kk")),
            new Account(Guid.NewGuid(), "erkin02", "SdAfdf_231!2fd", "Programmer", (int)Roles.user, DateTime.Now, new Parameters("kk")),
            new Account(Guid.NewGuid(), "samat2005", "Qwe!12rtyP", "QuestMaster72", (int)Roles.user, DateTime.Now, new Parameters("kk")),
            new Account(Guid.NewGuid(), "ivanov1998", "Sun_789Scale", "SolarMind", (int)Roles.user, DateTime.Now, new Parameters("ru")),
            new Account(Guid.NewGuid(), "aidar03", "Dp44!PassfA323", "DataPoint44", (int)Roles.user, DateTime.Now, new Parameters("kk")),
            new Account(Guid.NewGuid(), "nurbol2001", "Star*2026Log", "StarLogic", (int)Roles.user, DateTime.Now, new Parameters("ru")),
            new Account(Guid.NewGuid(), "bolat04", "Alt_99Type!", "AltType", (int)Roles.user, DateTime.Now, new Parameters("kk")),
            new Account(Guid.NewGuid(), "admin", "M0q8t9y>,ym8B", "Manager", (int)Roles.admin, DateTime.Now, new Parameters("ru")),
        };

        // Категории тестов
        public static readonly Category[] categories = {
            new Category(Guid.NewGuid(), "world-history", "kk", "Дүниежүзі тарихы"),
            new Category(Guid.NewGuid(), "biology-grade-6", "ru", "Биология. 6-класс"),
            new Category(Guid.NewGuid(), "geography-grade-9", "kk", "География. 9-сынып"),
            new Category(Guid.NewGuid(), "interesting-facts", "kk", "Қызықты фактілер"),
            new Category(Guid.NewGuid(), "space", "kk", "Ғарыш"),
            new Category(Guid.NewGuid(), "math", "ru", "Математика"),
            new Category(Guid.NewGuid(), "world-history-grade-8", "ru", "Всемирная история. 8-класс"),
            new Category(Guid.NewGuid(), "it-programming", "kk", "IT және бағдарламалау"),
            new Category(Guid.NewGuid(), "uncategorized", "und", "Uncategorized")
        };

        // Тесты
        public static readonly Test[] tests = {
            // 1. ТЕСТ ПО МАТЕМАТИКЕ (Простые числа, НОД и НОК)
            new Test(Guid.NewGuid(), "Простые числа, НОД и НОК", categories.First(c => c.Name == "math").Id, "Тест из 5 вопросов для подготовки к ЕНТ по теме простых чисел и делимости.", "math.jpg", accounts.First(a => a.Login == "samat2005").Id, new DateTime(2025, 02, 20, 16, 44, 56), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2025, 02, 20, 20, 14, 54) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Найдите простые числа.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "4", false),
                            new Answer(Guid.NewGuid(), "5", true),
                            new Answer(Guid.NewGuid(), "2", true),
                            new Answer(Guid.NewGuid(), "3", true)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Найдите НОД (110; 154; 286).", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "11", false),
                            new Answer(Guid.NewGuid(), "22", true),
                            new Answer(Guid.NewGuid(), "32", false),
                            new Answer(Guid.NewGuid(), "44", false),
                            new Answer(Guid.NewGuid(), "64", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Найдите НОК (24; 40).", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "48", false),
                            new Answer(Guid.NewGuid(), "40", false),
                            new Answer(Guid.NewGuid(), "120", true),
                            new Answer(Guid.NewGuid(), "8", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Выберите верные утверждения о числах и их делимости.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Число 1 является простым числом", false),
                            new Answer(Guid.NewGuid(), "Если НОД двух чисел равен 1, то такие числа называются взаимно простыми", true),
                            new Answer(Guid.NewGuid(), "Любое четное число делится на 2", true),
                            new Answer(Guid.NewGuid(), "Произведение НОД(a,b) и НОК(a,b) равно произведению чисел a и b", true)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Найдите НОД (48; 72).", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "12", false),
                            new Answer(Guid.NewGuid(), "24", true),
                            new Answer(Guid.NewGuid(), "18", false),
                            new Answer(Guid.NewGuid(), "48", false)
                        ]
                    )
                ]
            ),

            // 2. ТЕСТ ПО МАТЕМАТИКЕ (Уравнения и неравенства)
            new Test(Guid.NewGuid(), "Линейные и квадратные уравнения", categories.First(c => c.Name == "math").Id, "Тест для проверки навыков решения уравнений и систем неравенств, состоящий из 5-ти вопросов.", "equations.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2025, 05, 12, 10, 15, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2025, 05, 12, 14, 35, 22) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Найдите корни квадратного уравнения: x² - 5x + 6 = 0", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1", false),
                            new Answer(Guid.NewGuid(), "2", true),
                            new Answer(Guid.NewGuid(), "3", true),
                            new Answer(Guid.NewGuid(), "-6", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Решите уравнение: 3x - 7 = 11", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "4", false),
                            new Answer(Guid.NewGuid(), "5", false),
                            new Answer(Guid.NewGuid(), "6", true),
                            new Answer(Guid.NewGuid(), "8", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Какое из следующих чисел является корнем уравнения: √(x + 1) = 4?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "3", false),
                            new Answer(Guid.NewGuid(), "15", true),
                            new Answer(Guid.NewGuid(), "16", false),
                            new Answer(Guid.NewGuid(), "17", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Укажите все целые решения неравенства: 1 < x ≤ 4", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1", false),
                            new Answer(Guid.NewGuid(), "2", true),
                            new Answer(Guid.NewGuid(), "3", true),
                            new Answer(Guid.NewGuid(), "4", true)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "При каком значении параметра 'a' уравнение 0•x = a не имеет решений?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "При a = 0", false),
                            new Answer(Guid.NewGuid(), "При любом a ≠ 0", true),
                            new Answer(Guid.NewGuid(), "При любых значениях a", false),
                            new Answer(Guid.NewGuid(), "Таких значений нет", false)
                        ]
                    )
                ]
            ),
    
            // 3. ГЕОМЕТРИЯ
            new Test(Guid.NewGuid(), "Геометрия: Треугольники и их свойства", categories.First(c => c.Name == "math").Id, "Тест из 5 вопросов на знание основных теорем геометрии и свойств треугольников.", "geometry.jpg", accounts.First(a => a.Login == "samat2005").Id, new DateTime(2026, 01, 15, 09, 00, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 01, 15, 11, 15, 45) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Чему равна сумма углов любого плоского треугольника?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "90°", false),
                            new Answer(Guid.NewGuid(), "180°", true),
                            new Answer(Guid.NewGuid(), "360°", false),
                            new Answer(Guid.NewGuid(), "270°", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Катеты прямоугольного треугольника равны 3 см и 4 см. Найдите длину гипотенузы.", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "5 см", true),
                            new Answer(Guid.NewGuid(), "7 см", false),
                            new Answer(Guid.NewGuid(), "25 см", false),
                            new Answer(Guid.NewGuid(), "6 см", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Укажите свойства, характерные для равнобедренного треугольника.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Углы при основании равны", true),
                            new Answer(Guid.NewGuid(), "Все три стороны равны между собой", false),
                            new Answer(Guid.NewGuid(), "Медиана, проведенная к основанию, является биссектрисой и высотой", true),
                            new Answer(Guid.NewGuid(), "Один из углов обязательно равен 90°", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Существуют ли треугольники с указанными длинами сторон? Выберите возможные варианты.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "2 см, 3 см, 4 см", true),
                            new Answer(Guid.NewGuid(), "1 см, 2 см, 5 см", false),
                            new Answer(Guid.NewGuid(), "5 см, 5 см, 5 см", true),
                            new Answer(Guid.NewGuid(), "3 см, 4 см, 8 см", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Как называется отрезок, соединяющий вершину треугольника с серединой противоположной стороны?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Биссектриса", false),
                            new Answer(Guid.NewGuid(), "Высота", false),
                            new Answer(Guid.NewGuid(), "Медиана", true),
                            new Answer(Guid.NewGuid(), "Средняя линия", false)
                        ]
                    )
                ]
            ),

            // 4. ТЕСТ ПО МАТЕМАТИКЕ (Текстовые задачи и прогрессии)
            new Test(Guid.NewGuid(), "Текстовые задачи и прогрессии", categories.First(c => c.Name == "math").Id, "Тест на проверку умения решать логические текстовые задачи и работать с числовыми последовательностями, состоящий из 5-ти вопросов.", "word_problems.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2025, 08, 14, 11, 20, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2025, 08, 14, 15, 45, 10) }.AddRangeQuestion(
                [   
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Дана арифметическая прогрессия, где первый член (a₁) равен 4, а разность (d) равна 3. Найдите десятый член (a₁₀) этой прогрессии.", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "31", true),
                            new Answer(Guid.NewGuid(), "34", false),
                            new Answer(Guid.NewGuid(), "28", false),
                            new Answer(Guid.NewGuid(), "40", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Свежие грибы содержат 90% воды, а сухие — 12%. Сколько килограммов сухих грибов получится из 22 кг свежих?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "2 кг", false),
                            new Answer(Guid.NewGuid(), "2,5 кг", true),
                            new Answer(Guid.NewGuid(), "3 кг", false),
                            new Answer(Guid.NewGuid(), "4,4 кг", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Укажите формулы, которые могут задавать бесконечно убывающую геометрическую прогрессию.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "bₙ = 5 • (1/2)ⁿ", true),
                            new Answer(Guid.NewGuid(), "bₙ = 2 • (-1/3)ⁿ", true),
                            new Answer(Guid.NewGuid(), "bₙ = 3 • 2ⁿ", false),
                            new Answer(Guid.NewGuid(), "bₙ = 4 • (-1.5)ⁿ", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Расстояние между двумя городами поезд преодолевает за 4 часа со скоростью 60 км/ч. С какой скоростью должен ехать поезд, чтобы проехать это же расстояние за 3 часа?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "70 км/ч", false),
                            new Answer(Guid.NewGuid(), "75 км/ч", false),
                            new Answer(Guid.NewGuid(), "80 км/ч", true),
                            new Answer(Guid.NewGuid(), "90 км/ч", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Велосипедист ехал из пункта А в пункт Б. Выберите все величины, которые находятся в ПРЯМОЙ пропорциональной зависимости друг от друга (при условии, что остальные параметры постоянны).", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Пройденный путь и затраченное время (при постоянной скорости)", true),
                            new Answer(Guid.NewGuid(), "Скорость движения и затраченное время (на фиксированном пути)", false),
                            new Answer(Guid.NewGuid(), "Скорость движения и пройденный путь (за фиксированное время)", true),
                            new Answer(Guid.NewGuid(), "Масса велосипеда и скорость его движения", false)
                        ]
                    )
                ]
            ),
            // 5. КҮН ЖҮЙЕСІНІҢ ПЛАНЕТАЛАРЫ
            new Test(Guid.NewGuid(), "Күн жүйесінің планеталары", categories.First(c => c.Name == "space").Id, "Күн жүйесіндегі планеталар туралы білімді тексеруге арналған 10 сұрақтан тұратын тест.", "planets.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2026, 03, 01, 10, 12, 54), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 03, 01, 16, 14, 19) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Күнге ең жақын орналасқан планета қайсы?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Шолпан (Венера)", false),
                            new Answer(Guid.NewGuid(), "Марс", false),
                            new Answer(Guid.NewGuid(), "Меркурий", true),
                            new Answer(Guid.NewGuid(), "Жер", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Газ алыптарына жататын планеталарды көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Юпитер", true),
                            new Answer(Guid.NewGuid(), "Сатурн", true),
                            new Answer(Guid.NewGuid(), "Уран", false),
                            new Answer(Guid.NewGuid(), "Марс", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Күн жүйесіндегі ең ыстық планета қайсы?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Меркурий", false),
                            new Answer(Guid.NewGuid(), "Шолпан (Венера)", true),
                            new Answer(Guid.NewGuid(), "Юпитер", false),
                            new Answer(Guid.NewGuid(), "Марс", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Бұл планеталардың қайсысында бірде-бір табиғи серік ЖОҚ?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Меркурий", true),
                            new Answer(Guid.NewGuid(), "Шолпан (Венера)", true),
                            new Answer(Guid.NewGuid(), "Марс", false),
                            new Answer(Guid.NewGuid(), "Нептун", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Күн жүйесіндегі ең биік сөнген жанартау — Олимп тауы қай планетада орналасқан?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Жер", false),
                            new Answer(Guid.NewGuid(), "Сатурн", false),
                            new Answer(Guid.NewGuid(), "Марс", true),
                            new Answer(Guid.NewGuid(), "Меркурий", false)
                        ]
                    ),
                    // 6-вопрос
                    new Question(Guid.NewGuid(), "Қай планетаның айқын көрінетін сақиналар жүйесі бар?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Юпитер", false),
                            new Answer(Guid.NewGuid(), "Уран", false),
                            new Answer(Guid.NewGuid(), "Нептун", false),
                            new Answer(Guid.NewGuid(), "Сатурн", true)
                        ]
                    ),
                    // 7-вопрос
                    new Question(Guid.NewGuid(), "Басқа планеталармен салыстырғанда өз осін кері бағытта (шығыстан батысқа қарай) айналатын планеталарды көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Шолпан (Венера)", true),
                            new Answer(Guid.NewGuid(), "Уран", true),
                            new Answer(Guid.NewGuid(), "Жер", false),
                            new Answer(Guid.NewGuid(), "Юпитер", false)
                        ]
                    ),
                    // 8-вопрос
                    new Question(Guid.NewGuid(), "Қай планета телескоппен көрінгенге дейін математикалық есептеулердің көмегімен («қалам ұшында») ашылды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Уран", false),
                            new Answer(Guid.NewGuid(), "Нептун", true),
                            new Answer(Guid.NewGuid(), "Плутон", false),
                            new Answer(Guid.NewGuid(), "Сатурн", false)
                        ]
                    ),
                    // 9-вопрос
                    new Question(Guid.NewGuid(), "Қай планетаның ресми расталған серіктерінің саны ең көп (қазіргі уақытта 140-тан асады)?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Юпитер", false),
                            new Answer(Guid.NewGuid(), "Сатурн", true),
                            new Answer(Guid.NewGuid(), "Нептун", false)
                        ]
                    ),
                    // 10-вопрос
                    new Question(Guid.NewGuid(), "Жер планетасы туралы дұрыс тұжырымдарды таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Жер атмосферасы негізінен оттегінен тұрады", false),
                            new Answer(Guid.NewGuid(), "Жер — Күн жүйесіндегі ең тығыз планета", true),
                            new Answer(Guid.NewGuid(), "Планета бетінің шамамен 71%-ын су басып жатыр", true),
                            new Answer(Guid.NewGuid(), "Жер мінсіз шар пішінді", false)
                        ]
                    )
                ]
            ),
    
            // 6. АСТРОНОМИЯДАН ТЕСТ (ЖАЛПЫ ЭРУДИЦИЯ)
            new Test(Guid.NewGuid(), "Эрудицияны тексеруге арналған астрономиядан жалпы сұрақтар", categories.First(c => c.Name == "space").Id, "Әлемнің құрылымы туралы негізгі білімді және жалпы эрудицияны тексеруге арналған 5 сұрақтан тұратын тест.", "astronomy_trivia.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2026, 03, 05, 11, 00, 19), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 03, 05, 14, 17, 10) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Біздің Күн жүйесіне ең жақын орналасқан жұлдызды көрсетіңіз?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Сириус", false),
                            new Answer(Guid.NewGuid(), "Проксима Центавра", true),
                            new Answer(Guid.NewGuid(), "Темірқазық", false),
                            new Answer(Guid.NewGuid(), "Бетельгейзе", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Төменде көрсетілген нысандардың қайсысы ертегей (ершік) планеталар санатына жатады?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Плутон", true),
                            new Answer(Guid.NewGuid(), "Церера", true),
                            new Answer(Guid.NewGuid(), "Нептун", false),
                            new Answer(Guid.NewGuid(), "Меркурий", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Біздің Күн жүйесі орналасқан галактика қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Андромеда галактикасы", false),
                            new Answer(Guid.NewGuid(), "Құс жолы", true),
                            new Answer(Guid.NewGuid(), "Үлкен Магеллан бұлты", false),
                            new Answer(Guid.NewGuid(), "Үшбұрыш галактикасы", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Өзінен көрінетін жарық шығаруға қабілетті физикалық құбылыстарды немесе нысандарды таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Жұлдыздар", true),
                            new Answer(Guid.NewGuid(), "Метеорлар (атмосферада жанған кезде)", true),
                            new Answer(Guid.NewGuid(), "Планеталар", false),
                            new Answer(Guid.NewGuid(), "Кометалар", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Түнгі аспанда көрінетін жұлдыздың түсін не анықтайды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Оның Жерге дейінгі қашықтығы", false),
                            new Answer(Guid.NewGuid(), "Оның беткі температурасы", true),
                            new Answer(Guid.NewGuid(), "Оның химиялық құрамы", false),
                            new Answer(Guid.NewGuid(), "Оның айналу жылдамдығы", false)
                        ]
                    )
                ]
            ),

            // 7. КОСМОЛОГИЯДАН ТЕСТ
            new Test(Guid.NewGuid(), "Космологияға кіріспе", categories.First(c => c.Name == "space").Id, "Әлемнің құрылымы, пайда болуы және ірі масштабтағы эволюциясы туралы 5 сұрақтан тұратын тест.", "cosmology.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2026, 03, 14, 15, 22, 05), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 03, 14, 17, 18, 54) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Қай ғылыми теория біздің Әлеміміздің өте тығыз әрі ыстық күйден пайда болуын және ерте дамуын сипаттайды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Стационарлық Әлем теориясы", false),
                            new Answer(Guid.NewGuid(), "Үлкен жарылыс теориясы", true),
                            new Answer(Guid.NewGuid(), "Шешектер (струналар) теориясы", false),
                            new Answer(Guid.NewGuid(), "Циклдік Әлем теориясы", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Қазіргі бақылауларға сәйкес, Әлемнің бүкіл массасы мен энергиясының шамамен 95%-ын қандай екі гипотетикалық субстанция құрайды?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Қараңғы материя", true),
                            new Answer(Guid.NewGuid(), "Қараңғы энергия", true),
                            new Answer(Guid.NewGuid(), "Ғарыштық шаң", false),
                            new Answer(Guid.NewGuid(), "Антизат", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Әлемді біркелкі толтыратын және Үлкен жарылыстың «жаңғырығы» болып табылатын реликті сәулелену қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Ғарыштық микротолқынды фондық сәулелену", true),
                            new Answer(Guid.NewGuid(), "Гравитациялық сәулелену", false),
                            new Answer(Guid.NewGuid(), "Пульсарлардың рентгендік сәулеленуі", false),
                            new Answer(Guid.NewGuid(), "Инфрақызыл фон", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Хаббл заңына және қазіргі космологиялық модельдерге сәйкес, қазіргі уақытта біздің Әлемімізде қандай процесс жүріп жатыр?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Ол гравитация әсерінен сығылуда", false),
                            new Answer(Guid.NewGuid(), "Ол статикалық күйде және өлшемдерін өзгертпейді", false),
                            new Answer(Guid.NewGuid(), "Ол үдеумен кеңеюде", true),
                            new Answer(Guid.NewGuid(), "Ол тұрақты баяулаумен кеңеюде", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Қара құрдымдар туралы дұрыс тұжырымдарды таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Қара құрдымның жарық та шыға алмайтын шекарасы оқиғалар көжиегі деп аталады", true),
                            new Answer(Guid.NewGuid(), "Қара құрдымның орталығында шексіз тығыздыққа ие нүкте — сингулярлық орналасқан", true),
                            new Answer(Guid.NewGuid(), "Қара құрдымдарды кәдімгі оптикалық телескоппен мүлдем қара мінсіз сфералар ретінде көруге болады", false),
                            new Answer(Guid.NewGuid(), "Қара құрдымдар Хокинг сәулеленуінің арқасында уақыт өте келе буланып жоқ болуы мүмкін", true)
                        ]
                    )
                ]
            ),

            // 8. КОСМОНАВТИКА ТАРИХЫ
            new Test(Guid.NewGuid(), "Космонавтика тарихы", categories.First(c => c.Name == "space").Id, "Ғарышты игеру дәуірі, ұлы конструкторлар, ғарышкерлер және негізгі миссиялар туралы 5 сұрақтан тұратын тест.", "astronautics.jpg", accounts.First(a => a.Login == "kairat03").Id, new DateTime(2026, 04, 05, 12, 04, 16), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 04, 05, 16, 18, 34) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "КСРО-да әлемдегі тұңғыш Жердің жасанды серігі (Спутник-1) қай жылы ұшырылды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1957", true),
                            new Answer(Guid.NewGuid(), "1961", false),
                            new Answer(Guid.NewGuid(), "1965", false),
                            new Answer(Guid.NewGuid(), "1945", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "1961 жылы 12 сәуірде ғарыш кеңістігіне ұшқан алғашқы адам кім?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Алексей Леонов", false),
                            new Answer(Guid.NewGuid(), "Нил Армстронг", false),
                            new Answer(Guid.NewGuid(), "Юрий Гагарин", true),
                            new Answer(Guid.NewGuid(), "Герман Титов", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Өз жетістіктері бойынша ТҰҢҒЫШ ретінде тарихқа енген ғарышкерлерді/астронавттарды таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Валентина Терешкова — ғарыштағы алғашқы әйел", true),
                            new Answer(Guid.NewGuid(), "Алексей Леонов — ашық ғарышқа шыққан алғашқы адам", true),
                            new Answer(Guid.NewGuid(), "Нил Армстронг — Ай бетіне қадам басқан алғашқы адам", true),
                            new Answer(Guid.NewGuid(), "Илон Маск — жеке ғарыш кемесінің алғашқы ұшқышы", false)
                        ]
                    ),
                    // 4-вопрос
                    // В казахском языке "Аполлон" обычно так и транслитерируется, либо "Аполло".
                    new Question(Guid.NewGuid(), "Адамдардың Айға қону миссиялары жүзеге асырылған американдық ғарыш бағдарламасы қалай аталды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Джемини (Gemini)", false),
                            new Answer(Guid.NewGuid(), "Аполлон (Apollo)", true),
                            new Answer(Guid.NewGuid(), "Скайлэб (Skylab)", false),
                            new Answer(Guid.NewGuid(), "Вояджер (Voyager)", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Төменде көрсетілген орбиталық станциялардың қайсысы ғарышта құрылып, жұмыс істеді?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "«Мир» станциясы", true),
                            new Answer(Guid.NewGuid(), "Халықаралық ғарыш станциясы (ХҒС)", true),
                            new Answer(Guid.NewGuid(), "«Салют» станциясы", true),
                            new Answer(Guid.NewGuid(), "Өлім жұлдызы (Звезда смерти)", false)
                        ]
                    )
                ]
            ),

            // 9. ВСЕМИРНАЯ ИСТОРИЯ
            new Test(Guid.NewGuid(), "Промышленный переворот: техника и общество", categories.First(c => c.Name == "world-history-grade-8").Id, "Тест о начале и последствиях промышленного переворота в XVIII-XIX веках.", "industrial_revolution.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2025, 03, 10, 09, 30, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2025, 03, 10, 12, 45, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "В какой стране впервые начался промышленный переворот?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Франция", false),
                            new Answer(Guid.NewGuid(), "США", false),
                            new Answer(Guid.NewGuid(), "Англия", true),
                            new Answer(Guid.NewGuid(), "Германия", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Кто из изобретателей усовершенствовал паровую машину и внедрил её в производство?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Джеймс Уатт", true),
                            new Answer(Guid.NewGuid(), "Джордж Стефенсон", false),
                            new Answer(Guid.NewGuid(), "Роберт Фултон", false),
                            new Answer(Guid.NewGuid(), "Исаак Ньютон", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Укажите основные социальные классы, возникшие в результате промышленного переворота.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Буржуазия", true),
                            new Answer(Guid.NewGuid(), "Пролетариат (рабочий класс)", true),
                            new Answer(Guid.NewGuid(), "Крестьяне", false),
                            new Answer(Guid.NewGuid(), "Феодалы", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Какой инженер сконструировал первый паровоз?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Абрахам Дерби", false),
                            new Answer(Guid.NewGuid(), "Джордж Стефенсон", true),
                            new Answer(Guid.NewGuid(), "Джеймс Харгривс", false),
                            new Answer(Guid.NewGuid(), "Джон Кей", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Выберите признаки, характерные для промышленного переворота.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Переход от ручного труда к машинному", true),
                            new Answer(Guid.NewGuid(), "Переход от мануфактуры к фабрично-заводскому производству", true),
                            new Answer(Guid.NewGuid(), "Упадок сельского хозяйства", false),
                            new Answer(Guid.NewGuid(), "Развитие исключительно ремесла", false)
                        ]
                    )
                ]
            ),

            // 10. ВСЕМИРНАЯ ИСТОРИЯ
            new Test(Guid.NewGuid(), "Великая французская буржуазная революция", categories.First(c => c.Name == "world-history-grade-8").Id, "Тест о революционных событиях и преобразованиях во Франции в конце XVIII века.", "french_revolution.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2025, 06, 15, 14, 0, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2025, 06, 15, 16, 20, 15) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "В каком году произошло взятие Бастилии, послужившее началом революции во Франции?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1776 год", false),
                            new Answer(Guid.NewGuid(), "1789 год", true),
                            new Answer(Guid.NewGuid(), "1793 год", false),
                            new Answer(Guid.NewGuid(), "1804 год", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "В каком году была принята «Декларация прав человека и гражданина»?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1789 год", true),
                            new Answer(Guid.NewGuid(), "1791 год", false),
                            new Answer(Guid.NewGuid(), "1799 год", false),
                            new Answer(Guid.NewGuid(), "1785 год", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Кто из французских королей был низложен и казнен во время революции?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Людовик XIV", false),
                            new Answer(Guid.NewGuid(), "Людовик XVI", true),
                            new Answer(Guid.NewGuid(), "Карл X", false),
                            new Answer(Guid.NewGuid(), "Наполеон Бонапарт", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Укажите основных лидеров Якобинского клуба.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Максимилиан Робеспьер", true),
                            new Answer(Guid.NewGuid(), "Жан-Поль Марат", true),
                            new Answer(Guid.NewGuid(), "Жорж Дантон", true),
                            new Answer(Guid.NewGuid(), "Шарль де Голль", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Выберите слова, ставшие лозунгом Французской революции.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Свобода", true),
                            new Answer(Guid.NewGuid(), "Равенство", true),
                            new Answer(Guid.NewGuid(), "Братство", true),
                            new Answer(Guid.NewGuid(), "Порядок", false)
                        ]
                    )
                ]
            ),

            // 11. ВСЕМИРНАЯ ИСТОРИЯ
            new Test(Guid.NewGuid(), "Эпоха Просвещения в Европе", categories.First(c => c.Name == "world-history-grade-8").Id, "Тест об изменениях в культурной, философской и общественно-политической жизни Европы в XVIII веке.", "enlightenment.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2025, 09, 05, 11, 0, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2025, 09, 05, 14, 25, 30) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Кто является автором теории разделения властей на законодательную, исполнительную и судебную ветви?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Вольтер", false),
                            new Answer(Guid.NewGuid(), "Шарль Монтескье", true),
                            new Answer(Guid.NewGuid(), "Жан-Жак Руссо", false),
                            new Answer(Guid.NewGuid(), "Дени Дидро", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Какой мыслитель эпохи Просвещения предложил теорию общественного договора?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Жан-Жак Руссо", true),
                            new Answer(Guid.NewGuid(), "Джон Локк", false),
                            new Answer(Guid.NewGuid(), "Томас Гоббс", false),
                            new Answer(Guid.NewGuid(), "Рене Декарт", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Кто был организатором издания и редактором знаменитой французской «Энциклопедии» в эпоху Просвещения?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Томас Джефферсон", false),
                            new Answer(Guid.NewGuid(), "Иммануил Кант", false),
                            new Answer(Guid.NewGuid(), "Дени Дидро", true),
                            new Answer(Guid.NewGuid(), "Фрэнсис Бэкон", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Укажите среди перечисленных выдающихся представителей эпохи Просвещения.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Вольтер", true),
                            new Answer(Guid.NewGuid(), "Джон Локк", true),
                            new Answer(Guid.NewGuid(), "Микеланджело", false),
                            new Answer(Guid.NewGuid(), "Карл Маркс", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Как идеи Просвещения повлияли на страны Европы? Выберите верные утверждения.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Привели к появлению политики «просвещенного абсолютизма»", true),
                            new Answer(Guid.NewGuid(), "Ослабили влияние церкви на общество и науку", true),
                            new Answer(Guid.NewGuid(), "Способствовали укреплению феодального строя", false),
                            new Answer(Guid.NewGuid(), "Стали идейной основой буржуазных революций", true)
                        ]
                    )
                ]
            ),

            // 12. ВСЕМИРНАЯ ИСТОРИЯ (Наполеоновские войны) - На русском языке
            new Test(Guid.NewGuid(), "Наполеоновские войны и Венский конгресс", categories.First(c => c.Name == "world-history-grade-8").Id, "Тест о периоде правления Наполеона Бонапарта и послевоенном устройстве Европы.", "napoleonic_wars.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2025, 11, 20, 15, 0, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2025, 11, 20, 18, 10, 45) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "В каком году Наполеон Бонапарт был провозглашен императором французов?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "1799", false),
                            new Answer(Guid.NewGuid(), "1802", false),
                            new Answer(Guid.NewGuid(), "1804", true),
                            new Answer(Guid.NewGuid(), "1812", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "В какой решающей битве в 1815 году Наполеон потерпел окончательное поражение?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Битва под Аустерлицем", false),
                            new Answer(Guid.NewGuid(), "Бородинское сражение", false),
                            new Answer(Guid.NewGuid(), "Битва при Ватерлоо", true),
                            new Answer(Guid.NewGuid(), "Битва народов под Лейпцигом", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Какова была основная цель Венского конгресса (1814-1815)?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Поддержка идей Французской революции", false),
                            new Answer(Guid.NewGuid(), "Восстановление границ и монархий в Европе", true),
                            new Answer(Guid.NewGuid(), "Создание единого государства в Европе", false),
                            new Answer(Guid.NewGuid(), "Начало новой войны против России", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Какие государства вошли в состав 'Священного союза', созданного после Венского конгресса?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Россия", true),
                            new Answer(Guid.NewGuid(), "Пруссия", true),
                            new Answer(Guid.NewGuid(), "Австрия", true),
                            new Answer(Guid.NewGuid(), "Османская империя", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Кто был главным противником Наполеона на море, одержавшим победу в Трафальгарском сражении?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Адмирал Нельсон", true),
                            new Answer(Guid.NewGuid(), "Герцог Веллингтон", false),
                            new Answer(Guid.NewGuid(), "Кутузов", false),
                            new Answer(Guid.NewGuid(), "Франц II", false)
                        ]
                    )
                ]
            ),

            // 13. БАҒДАРЛАМАЛАУ (Объектіге бағытталған бағдарламалау - ООП) - Қазақ тілінде
            new Test(Guid.NewGuid(), "Объектіге бағытталған бағдарламалау негіздері", categories.First(c => c.Name == "it-programming").Id, "ООП-ның негізгі концепциялары (инкапсуляция, полиморфизм, мұрагерлік) бойынша білімді тексеру тесті.", "oop_basics.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2026, 01, 10, 10, 0, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 01, 10, 12, 30, 15) }.AddRangeQuestion(
                [
                    // 1-сұрақ
                    new Question(Guid.NewGuid(), "ООП-ның негізгі үш тірегін (концепциясын) көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Инкапсуляция", true),
                            new Answer(Guid.NewGuid(), "Мұрагерлік (Inheritance)", true),
                            new Answer(Guid.NewGuid(), "Полиморфизм", true),
                            new Answer(Guid.NewGuid(), "Компиляция", false)
                        ]
                    ),
                    // 2-сұрақ
                    new Question(Guid.NewGuid(), "Деректерді және олармен жұмыс істейтін әдістерді бір объектінің ішіне жасыру және сыртқы әсерден қорғау қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Абстракция", false),
                            new Answer(Guid.NewGuid(), "Инкапсуляция", true),
                            new Answer(Guid.NewGuid(), "Полиморфизм", false),
                            new Answer(Guid.NewGuid(), "Мұрагерлік", false)
                        ]
                    ),
                    // 3-сұрақ
                    new Question(Guid.NewGuid(), "Бірдей атаулы әдістің әртүрлі объектілерде әртүрлі іске асырылу мүмкіндігі қандай ұғымға жатады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Полиморфизм", true),
                            new Answer(Guid.NewGuid(), "Интерфейс", false),
                            new Answer(Guid.NewGuid(), "Модульділік", false),
                            new Answer(Guid.NewGuid(), "Мұрагерлік", false)
                        ]
                    ),
                    // 4-сұрақ
                    new Question(Guid.NewGuid(), "Класс ішіндегі айнымалылар мен әдістердің сыртқа көріну деңгейін реттейтін қолжетімділік модификаторларын таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "public", true),
                            new Answer(Guid.NewGuid(), "private", true),
                            new Answer(Guid.NewGuid(), "protected", true),
                            new Answer(Guid.NewGuid(), "virtual", false)
                        ]
                    ),
                    // 5-сұрақ
                    new Question(Guid.NewGuid(), "Объект құрылған сәтте автоматты түрде шақырылатын және оның бастапқы күйін реттейтін арнайы әдіс қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Деструктор", false),
                            new Answer(Guid.NewGuid(), "Конструктор", true),
                            new Answer(Guid.NewGuid(), "Инициализатор", false),
                            new Answer(Guid.NewGuid(), "Метод", false)
                        ]
                    )
                ]
            ),

            // 14. IT БОЙЫНША ТЕСТ
            new Test(Guid.NewGuid(), "Мәліметтер базасы және SQL негезідері", categories.First(c => c.Name == "it-programming").Id, "Реляциялық мәліметтер базасы, кестелерді жобалау және негізгі SQL сұраныстарын жазу бойынша білімді тексеруге арналған тест.", "databases.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 02, 14, 15, 20, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 02, 14, 19, 45, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Мәліметтер базасынан деректерді алу үшін қай SQL операторы қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "GET", false),
                            new Answer(Guid.NewGuid(), "SELECT", true),
                            new Answer(Guid.NewGuid(), "EXTRACT", false),
                            new Answer(Guid.NewGuid(), "UPDATE", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Төмендегі МББЖ (Мәліметтер базасын басқару жүйелері) қайсысы реляциялық болып табылады?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "PostgreSQL", true),
                            new Answer(Guid.NewGuid(), "Microsoft SQL Server", true),
                            new Answer(Guid.NewGuid(), "MySQL", true),
                            new Answer(Guid.NewGuid(), "MongoDB", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "SQL-де берілген шарт бойынша жолдарды сүзу үшін қай кілттік сөз қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "WHERE", true),
                            new Answer(Guid.NewGuid(), "HAVING", false),
                            new Answer(Guid.NewGuid(), "IF", false),
                            new Answer(Guid.NewGuid(), "FILTER", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Реляциялық мәліметтер базасында кестелер арасында болуы мүмкін байланыс түрлерін таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Біреу-біреуге (1:1)", true),
                            new Answer(Guid.NewGuid(), "Біреу-көпке (1:M)", true),
                            new Answer(Guid.NewGuid(), "Көп-көпке (M:M)", true),
                            new Answer(Guid.NewGuid(), "Барлығы-барлығына", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Мәліметтер базасы кестесіндегі әрбір жазбаны бірегей түрде анықтайтын өріс немесе өрістер жиынтығы қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Сыртқы кілт (Foreign Key)", false),
                            new Answer(Guid.NewGuid(), "Бастапқы кілт (Primary Key)", true),
                            new Answer(Guid.NewGuid(), "Бірегей индекс", false),
                            new Answer(Guid.NewGuid(), "Супер-кілт", false)
                        ]
                    )
                ]
            ),

            // 15. БАҒДАРЛАМАЛАУ (Веб-технологиялар) - Қазақ тілінде
            new Test(Guid.NewGuid(), "Веб-бағдарламалау негіздері (HTML, CSS, JavaScript)", categories.First(c => c.Name == "it-programming").Id, "Веб-сайттардың құрылымы, стильдері және клиенттік сценарийлер туралы базалық тест.", "web_development.jpg", accounts.First(a => a.Login == "arman04").Id, new DateTime(2026, 03, 22, 11, 15, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 03, 22, 13, 50, 45) }.AddRangeQuestion(
                [
                    // 1-сұрақ
                    new Question(Guid.NewGuid(), "Веб-парақшаның мазмұны мен құрылымын анықтау үшін қандай тіл қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "CSS", false),
                            new Answer(Guid.NewGuid(), "HTML", true),
                            new Answer(Guid.NewGuid(), "JavaScript", false),
                            new Answer(Guid.NewGuid(), "PHP", false)
                        ]
                    ),
                    // 2-сұрақ
                    new Question(Guid.NewGuid(), "JavaScript тілінде айнымалыларды жариялау үшін қолданылатын кілттік сөздерді көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "let", true),
                            new Answer(Guid.NewGuid(), "const", true),
                            new Answer(Guid.NewGuid(), "var", true),
                            new Answer(Guid.NewGuid(), "int", false)
                        ]
                    ),
                    // 3-сұрақ
                    new Question(Guid.NewGuid(), "HTML элементтеріне стильдер (түс, қаріп, орналасу) беру үшін не пайдаланылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "JSON", false),
                            new Answer(Guid.NewGuid(), "CSS", true),
                            new Answer(Guid.NewGuid(), "SQL", false),
                            new Answer(Guid.NewGuid(), "XML", false)
                        ]
                    ),
                    // 4-сұрақ
                    new Question(Guid.NewGuid(), "Браузерде тікелей орындалатын (клиенттік) бағдарламалау тілдерін таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "JavaScript", true),
                            new Answer(Guid.NewGuid(), "TypeScript", false),
                            new Answer(Guid.NewGuid(), "C#", false),
                            new Answer(Guid.NewGuid(), "Python", false)
                        ]
                    ),
                    // 5-сұрақ
                    new Question(Guid.NewGuid(), "HTML-де гиперсілтеме (басқа бетке өту) жасау үшін қандай тег қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "<img>", false),
                            new Answer(Guid.NewGuid(), "<link>", false),
                            new Answer(Guid.NewGuid(), "<a>", true),
                            new Answer(Guid.NewGuid(), "<p>", false)
                        ]
                    )
                ]
            ),

            // 16. IT БОЙЫНША ТЕСТ
            new Test(Guid.NewGuid(), "Алгоритмдер және деректер құрылымы", categories.First(c => c.Name == "it-programming").Id, "Негізгі деректер құрылымын (стек, кезек) түсінуді және алгоритмдердің күрделілігін бағалауды тексеруге арналған тест.", "algorithms.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 04, 05, 14, 0, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 04, 05, 16, 15, 30) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Қай деректер құрылымы LIFO (Last In, First Out — соңғы келген, бірінші кетеді) принципі бойынша жұмыс істейді?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Кезек (Queue)", false),
                            new Answer(Guid.NewGuid(), "Стек (Stack)", true),
                            new Answer(Guid.NewGuid(), "Ағаш (Tree)", false),
                            new Answer(Guid.NewGuid(), "Хэш-кесте", false)
                        ]
                    ),
                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Төменде көрсетілген алгоритмдердің қайсысы сұрыптау алгоритмдеріне жатады?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Көпіршікті сұрыптау (Bubble Sort)", true),
                            new Answer(Guid.NewGuid(), "Жылдам сұрыптау (Quick Sort)", true),
                            new Answer(Guid.NewGuid(), "Біріктіру арқылы сұрыптау (Merge Sort)", true),
                            new Answer(Guid.NewGuid(), "Бинарлық іздеу (Binary Search)", false)
                        ]
                    ),
                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Сұрыпталған массивтегі тиімді бинарлық іздеудің ең нашар жағдайдағы уақытша күрделілігі қандай?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "O(1)", false),
                            new Answer(Guid.NewGuid(), "O(n)", false),
                            new Answer(Guid.NewGuid(), "O(log n)", true),
                            new Answer(Guid.NewGuid(), "O(n²)", false)
                        ]
                    ),
                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Элементтері бірінен соң бірі орналасқан сызықтық деректер құрылымдарын таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Массив (Array)", true),
                            new Answer(Guid.NewGuid(), "Байланысқан тізім (Linked List)", true),
                            new Answer(Guid.NewGuid(), "Граф (Graph)", false),
                            new Answer(Guid.NewGuid(), "Бинарлық ағаш", false)
                        ]
                    ),
                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Функцияның өзін-өзі тікелей немесе басқа функциялар арқылы шақыру процесі қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Итерация", false),
                            new Answer(Guid.NewGuid(), "Рекурсия", true),
                            new Answer(Guid.NewGuid(), "Инкапсуляция", false),
                            new Answer(Guid.NewGuid(), "Циклдік", false)
                        ]
                    )
                ]
            ),

            // 17. C# БОЙЫНША ТЕСТ
            new Test(Guid.NewGuid(), "C# бағдарламалау тілінің негіздері", categories.First(c => c.Name == "it-programming").Id, "C# синтаксисін, ООП механизмдерін, деректер типтерімен жұмыс істеуді және жадты басқаруды тексеруге арналған тест.", "csharp_basics.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 05, 19, 10, 15, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 05, 19, 13, 45, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Қай қолжетімділік модификаторы класс мүшесін тек ағымдағы жинақ (Assembly) ішінде ғана көрінетін етеді?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "private", false),
                            new Answer(Guid.NewGuid(), "protected", false),
                            new Answer(Guid.NewGuid(), "internal", true),
                            new Answer(Guid.NewGuid(), "public", false)
                        ]
                    ),

                    // 2-вопрос
                    new Question(Guid.NewGuid(), "C#-тағы сілтемелік типтерге (Reference Types) жататын деректер типтерін таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "class", true),
                            new Answer(Guid.NewGuid(), "struct", false),
                            new Answer(Guid.NewGuid(), "interface", true),
                            new Answer(Guid.NewGuid(), "int", false)
                        ]
                    ),

                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Басқарылмайтын ресурстарды босату және Dispose() әдісінің код блогы арқылы кепілді түрде шақырылуын қамтамасыз ету үшін қай кілттік сөз қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "using", true),
                            new Answer(Guid.NewGuid(), "lock", false),
                            new Answer(Guid.NewGuid(), "try", false),
                            new Answer(Guid.NewGuid(), "finally", false)
                        ]
                    ),

                    // 4-вопрос
                    new Question(Guid.NewGuid(), "C#-тағы абстрактілі кластар үшін төмендегі тұжырымдардың қайсысы дұрыс?", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "new операторы арқылы абстрактілі кластың данасын тікелей жасауға болмайды.", true),
                            new Answer(Guid.NewGuid(), "Абстрактілі класта әдістердің іске асырылуы (реализациясы) болмауы тиіс.", false),
                            new Answer(Guid.NewGuid(), "Класс тек бір ғана абстрактілі кластан мұрагерлік ала алады.", true),
                            new Answer(Guid.NewGuid(), "Абстрактілі класта кем дегенде бір абстрактілі әдіс болуы міндетті.", false)
                        ]
                    ),

                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Төмендегілердің ішінен қайсысы өзгермейтін (immutable) символдар тізбегі болып табылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "StringBuilder", false),
                            new Answer(Guid.NewGuid(), "string", true),
                            new Answer(Guid.NewGuid(), "char[]", false),
                            new Answer(Guid.NewGuid(), "List<char>", false)
                        ]
                    )
                ]
            ),

            // 18. JavaScript негіздері
            new Test(Guid.NewGuid(), "JavaScript негіздері", categories.First(c => c.Name == "it-programming").Id, "JavaScript тілінің синтаксисі, деректер типтері және DOM-мен жұмыс істеу негіздері бойынша тест.", "javascript_basics.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 05, 20, 10, 30, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 05, 20, 14, 45, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "JavaScript-те айнымалыны жариялау үшін қолданылатын кілттік сөздерді көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "let", true),
                            new Answer(Guid.NewGuid(), "const", true),
                            new Answer(Guid.NewGuid(), "var", true),
                            new Answer(Guid.NewGuid(), "define", false)
                        ]
                    ),

                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Массивтің соңына жаңа элемент қосатын әдіс (method) қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "pop()", false),
                            new Answer(Guid.NewGuid(), "shift()", false),
                            new Answer(Guid.NewGuid(), "push()", true),
                            new Answer(Guid.NewGuid(), "join()", false)
                        ]
                    ),

                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Қай деректер типі 'true' немесе 'false' мәндерін қабылдайды?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "String", false),
                            new Answer(Guid.NewGuid(), "Boolean", true),
                            new Answer(Guid.NewGuid(), "Number", false),
                            new Answer(Guid.NewGuid(), "Null", false)
                        ]
                    ),

                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Логикалық 'ЖӘНЕ' (AND) операторын көрсетіңіз.", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "||", false),
                            new Answer(Guid.NewGuid(), "!", false),
                            new Answer(Guid.NewGuid(), "&&", true),
                            new Answer(Guid.NewGuid(), "==", false)
                        ]
                    ),

                    // 5-вопрос
                    new Question(Guid.NewGuid(), "JavaScript-тегі цикл түрлерін таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "for", true),
                            new Answer(Guid.NewGuid(), "while", true),
                            new Answer(Guid.NewGuid(), "foreach", false),
                            new Answer(Guid.NewGuid(), "do...while", true)
                        ]
                    )
                ]
            ),

            // 19. ТЕСТ ПО IT
            new Test(Guid.NewGuid(), "Жасанды интеллект негіздері", categories.First(c => c.Name == "it-programming").Id, "Жасанды интеллект, машиналық оқыту және нейрондық желілердің негізгі концепциялары бойынша тест.", "ai_fundamentals.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 05, 21, 11, 10, 0), (int)TestStatus.Published){ PublishedDateTime = new DateTime(2026, 05, 21, 16, 20, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Машиналық оқытудың (Machine Learning) негізгі түрлерін көрсетіңіз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Мұғаліммен оқыту (Supervised Learning)", true),
                            new Answer(Guid.NewGuid(), "Мұғалімсіз оқыту (Unsupervised Learning)", true),
                            new Answer(Guid.NewGuid(), "Нұсқаулықпен оқыту (Reinforcement Learning)", true),
                            new Answer(Guid.NewGuid(), "Қолмен оқыту (Manual Learning)", false)
                        ]
                    ),

                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Адам миының құрылымына негізделген алгоритм қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Сызықтық регрессия", false),
                            new Answer(Guid.NewGuid(), "Нейрондық желі (Neural Network)", true),
                            new Answer(Guid.NewGuid(), "Шешімдер ағашы", false),
                            new Answer(Guid.NewGuid(), "Генетикалық алгоритм", false)
                        ]
                    ),

                    // 3-вопрос
                    new Question(Guid.NewGuid(), "NLP (Natural Language Processing) технологиясы не үшін қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Роботтардың қозғалысын басқару үшін", false),
                            new Answer(Guid.NewGuid(), "Табиғи тілді өңдеу және түсіну үшін", true),
                            new Answer(Guid.NewGuid(), "Деректер базасын оңтайландыру үшін", false),
                            new Answer(Guid.NewGuid(), "Компьютерлік вирустарды іздеу үшін", false)
                        ]
                    ),

                    // 4-вопрос
                    new Question(Guid.NewGuid(), "Машиналық оқытуда қолданылатын танымал Python кітапханаларын таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "TensorFlow", true),
                            new Answer(Guid.NewGuid(), "PyTorch", true),
                            new Answer(Guid.NewGuid(), "Django", false),
                            new Answer(Guid.NewGuid(), "Scikit-learn", true)
                        ]
                    ),

                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Жүйенің жаңа деректерді алған кезде өздігінен жақсару қабілеті қалай аталады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Мәліметтерді форматтау", false),
                            new Answer(Guid.NewGuid(), "Автоматтандыру", false),
                            new Answer(Guid.NewGuid(), "Өздігінен оқу (Self-learning)", true),
                            new Answer(Guid.NewGuid(), "Индексация", false)
                        ]
                    )
                ]
            ),

            // 20. IT БОЙЫНША ТЕСТ
            new Test(Guid.NewGuid(), "Python бағдарламалау тілі", categories.First(c => c.Name == "it-programming").Id, "Python-ның негізгі деректер құрылымы, тізімдермен жұмыс және синтаксис ерекшеліктері бойынша тест.", "python_intro.jpg", accounts.First(a => a.Login == "erkin02").Id, new DateTime(2026, 05, 22, 12, 00, 0), (int)TestStatus.Checked){ PublishedDateTime = new DateTime(2026, 05, 22, 17, 30, 0) }.AddRangeQuestion(
                [
                    // 1-вопрос
                    new Question(Guid.NewGuid(), "Python-да код блогы (мысалы, функция немесе цикл ішінде) қалай белгіленеді?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Фигуралық жақшалармен {}", false),
                            new Answer(Guid.NewGuid(), "begin/end кілттік сөздерімен", false),
                            new Answer(Guid.NewGuid(), "Шегіністермен (бос орындар немесе табуляция)", true),
                            new Answer(Guid.NewGuid(), "Жай жақшалармен ()", false)
                        ]
                    ),

                    // 2-вопрос
                    new Question(Guid.NewGuid(), "Python-дағы барлық өзгермелі (mutable) деректер типтерін таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "list (тізім)", true),
                            new Answer(Guid.NewGuid(), "tuple (кортеж)", false),
                            new Answer(Guid.NewGuid(), "dict (сөздік)", true),
                            new Answer(Guid.NewGuid(), "set (жиын)", true)
                        ]
                    ),

                    // 3-вопрос
                    new Question(Guid.NewGuid(), "Тізімнің соңына элемент қосу үшін қай әдіс қолданылады?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "add()", false),
                            new Answer(Guid.NewGuid(), "insert()", false),
                            new Answer(Guid.NewGuid(), "append()", true),
                            new Answer(Guid.NewGuid(), "extend()", false)
                        ]
                    ),

                    // 4-вопрос
                    new Question(Guid.NewGuid(), "len([1, 2, 3, 4]) функциясының нәтижесі неге тең?", (int)QuestionType.RadioButton).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "3", false),
                            new Answer(Guid.NewGuid(), "4", true),
                            new Answer(Guid.NewGuid(), "5", false),
                            new Answer(Guid.NewGuid(), "Error", false)
                        ]
                    ),

                    // 5-вопрос
                    new Question(Guid.NewGuid(), "Python-да түсініктеме (комментарий) жазудың дұрыс жолдарын таңдаңыз.", (int)QuestionType.Checkbox).AddRangeAnswer(
                        [
                            new Answer(Guid.NewGuid(), "Жол басындағы # символы", true),
                            new Answer(Guid.NewGuid(), "Жол басындағы // символдары", false),
                            new Answer(Guid.NewGuid(), "Көпжолды түсініктемелер үшін үштік тырнақшалар '''", true),
                            new Answer(Guid.NewGuid(), "/* ... */ символдары", false)
                        ]
                    )
                ]
            ),
        };
        // Отзывы
        public static readonly Review[] reviews = {
            new Review(Guid.NewGuid(), accounts.Where(a => a.Login == "bolat04").First().Id, "Жақсы тест! Маған ұнады. Шолпан мен Уранның өз осінен кері айналуы туралы сұрақтар мені ойландырып тастады - бұл кез келген адамның есінде бола бермейтін дерек. Сұрақтардың күрделілігі жақсы теңгерілген.", 5, new DateTime(2026, 03, 03, 11, 10, 41), tests.Where(t => t.Title == "Күн жүйесінің планеталары").First().Id, true) { PublishedDateTime = new DateTime(2026, 03, 03, 14, 11, 56)},
            new Review(Guid.NewGuid(), accounts.Where(a => a.Login == "aidar03").First().Id, "Жалпы жақсы, бірақ 10 сұрақ аз сияқты, тез бітіп қалды. Астероидтар туралы сұрақтар қосу керек еді. Мен 4 деген бағаны қоямын.", 4, new DateTime(2026, 03, 03, 13, 00, 14), tests.Where(t => t.Title == "Күн жүйесінің планеталары").First().Id, true) { PublishedDateTime = new DateTime(2026, 03, 03, 15, 14, 45) },
            new Review(Guid.NewGuid(), accounts.Where(a => a.Login == "nurbol2001").First().Id, "Маған тест ұнады. Тест сауатты құрастырылған, қателер жоқ. Тест бойынша ең жоғары ұпай жинадым.", 5, new DateTime(2026, 03, 04, 16, 10, 42), tests.Where(t => t.Title == "Күн жүйесінің планеталары").First().Id, true) { PublishedDateTime = new DateTime(2026, 03, 04, 18, 26, 14) },
        };
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options) {

            Database.EnsureCreated();
        }
    }
}
