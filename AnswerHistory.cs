namespace Testiler_KZ {
    public class AnswerHistory {
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }

        public AnswerHistory() { }
        public AnswerHistory(Guid id, Guid answerId, string text, bool isCorrect) {
            this.Id = id;
            this.AnswerId = answerId;
            this.Text = text;
            IsCorrect = isCorrect;
        }
    }
}
