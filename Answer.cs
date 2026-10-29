namespace Testiler_KZ {

    /// <summary>
    /// Ответ.
    /// </summary>
    public class Answer {

        /// <summary>
        /// Идентификатор ответа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст ответа.
        /// </summary>
        private string _text = String.Empty;
        public string Text {
            get => _text;
            set {
                string text = value.Trim();
                if (text.Length < TestConstraints.MinAnswerLength || text.Length > TestConstraints.MaxAnswerLength) {
                    throw new ArgumentException("invalidAnswerTextFormat");
                }
                _text = text;
            }
        }

        /// <summary>
        /// Правильный ли ответ.
        /// </summary>
        public bool IsCorrect { get; set; }

        // Порядок ответа.
        private int? _order;
        public int? Order {
            get => _order;
            set {
                if (value != null && (value < 1)) {
                    throw new ArgumentException("invalidAnswerOrderFormat");
                }
                _order = value;
            }
        }

        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public Guid QuestionId { get; set; }

        private Answer() { }

        ///Конструктор
        public Answer(Guid Id, string Text, bool IsCorrect) {
            this.Id = Id;
            this.Text = Text;
            this.IsCorrect = IsCorrect;
        }
    }
}
