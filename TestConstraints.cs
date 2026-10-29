namespace Testiler_KZ {
    public class TestConstraints {
        // Заголовок теста
        public const int MinTitleLength = 3;
        public const int MaxTitleLength = 100;

        // Описание теста
        public const int MinDescriptionLength = 10;
        public const int MaxDescriptionLength = 500;

        // Вопрос
        public const int MinQuestionLength = 5;
        public const int MaxQuestionLength = 1000;

        // Ответ
        public const int MinAnswerLength = 1;
        public const int MaxAnswerLength = 250;
    }
}
