namespace Testiler_KZ {
    public class QuestionHistory {
        /// <summary>
        /// ID записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID вопроса.
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Тип вопроса.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Ответы.
        /// </summary>
        public List<AnswerHistory> Answers { get; set; } = new List<AnswerHistory>();

        /// <summary>
        /// Правильные ответы.
        /// </summary>
        public List<Guid> CorrectAnswers { get; set; } = new List<Guid>();

        /// <summary>
        /// Ответы пользователя
        /// </summary>
        public List<Guid> UserAnswers { get; set; } = new List<Guid>();

        /// <summary>
        /// Очки за вопрос.
        /// </summary>
        public double Score { get; set; }

        public string AiAnswer { get; set; } = string.Empty;

        public QuestionHistory() { }

        public QuestionHistory(Guid id, Guid questionId, string text, int type, List<AnswerHistory> answers, List<Guid> correctAnswers, List<Guid> userAnswers) {
            this.Id = id;
            this.QuestionId = questionId;
            this.Text = text;
            this.Type = type;
            this.Answers = answers;
            this.CorrectAnswers = correctAnswers;
            this.UserAnswers = userAnswers;
        }
    }
}
