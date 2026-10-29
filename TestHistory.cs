namespace Testiler_KZ {

    public class TestHistory {
        /// <summary>
        /// ID записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID теста.
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Заголовок теста.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Файл изображения.
        /// </summary>
        public string ImageFileName { get; set; } = string.Empty;

        /// <summary>
        /// ID пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата сдачи теста.
        /// </summary>
        public DateTime TakedDateTime { get; set; }

        /// <summary>
        /// Вопросы.
        /// </summary>
        public List<QuestionHistory> Questions { get; set; } = new List<QuestionHistory>();

        /// <summary>
        /// Баллы, заработанные пользователем.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Максимальный балл за текущий тест.
        /// </summary>
        public double MaxScore { get; set; }

        public TestHistory() { }

        public TestHistory(Guid id, Guid testId, string title, Guid userId, DateTime takedDateTime, string imageFileName, List<QuestionHistory> questions, double score, double maxScore) {
            this.Id = id;
            this.Title = title;
            this.TestId = testId;
            this.UserId = userId;
            this.TakedDateTime = takedDateTime;
            this.ImageFileName = imageFileName;
            this.Questions = questions;
            this.Score = score;
            this.MaxScore = maxScore;
        }
    }
}
