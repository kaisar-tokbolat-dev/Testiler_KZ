using Microsoft.EntityFrameworkCore;

namespace Testiler_KZ {

    public enum QuestionType {
        RadioButton,
        Checkbox
    }

    /// <summary>
    /// Вопрос теста.
    /// </summary>
    public class Question {

        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        private string _text = String.Empty;
        public string Text {
            get => _text;
            set {
                string text = value.Trim();
                if (text.Length < TestConstraints.MinQuestionLength || text.Length > TestConstraints.MaxQuestionLength) {
                    throw new ArgumentException("invalidQuestionTextFormat");
                }
                _text = text;
            }
        }

        /// <summary>
        /// Тип вопроса.
        /// </summary>
        private int _type;
        public int Type {
            get => _type;
            set {
                // Проверка типа вопроса
                if (!Enum.GetValues(typeof(QuestionType)).Cast<int>().Contains(value)) {
                    throw new ArgumentException("invalidQuestionTypeFormat");
                }
                _type = value;
            }
        }

        /// <summary>
        /// Ответы.
        /// </summary>
        private readonly List<Answer> _answers = new();

        public IReadOnlyCollection<Answer> Answers => _answers;

        // Добавить коллекцию ответов.
        public Question AddRangeAnswer(IEnumerable<Answer> answers) {
            // Если ни один ответ не является правильным
            if (!answers.Any(a => a.IsCorrect)) {
                throw new ArgumentException("selectCorrectAnswer");
            }

            // Если имеются два или более одинаковых ответа
            if (answers.DistinctBy(a => a.Text.Trim()).Count() != answers.Count()) {
                throw new ArgumentException("answerExistsError");
            }

            // Если отмечены две или более радиокнопки
            if (Type == (int)QuestionType.RadioButton && answers.Where(a => a.IsCorrect).Count() > 1) {
                throw new ArgumentException("checkedMoreButtonsError");
            }

            // Перебор ответов
            for (int i = 0; i < answers.Count(); i++) {
                // Ответ
                var answer = answers.ElementAt(i);

                // Порядок ответа
                answer.Order = i + 1;
                
                // Добавить ответ
                _answers.Add(answer);
            }

            return this;
        }

        // Добавить коллекцию ответов с сохранением в БД
        public Question AddRangeAnswer(IEnumerable<Answer> answers, DbContext db) {
            db.Entry(this).State = EntityState.Added;
            return AddRangeAnswer(answers);
        }

        /// <summary>
        /// Порядок вопроса.
        /// </summary>
        private int? _order;
        public int? Order {
            get => _order;
            set {
                if (value != null && (value < 1)) {
                    throw new ArgumentException("invalidQuestionOrderFormat");
                }
                _order = value;
            }
        }

        /// <summary>
        /// Идентификатор теста.
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Конструктор 1.
        /// </summary>
        private Question() { }

        /// <summary>
        /// Конструктор 2.
        /// </summary>
        public Question(Guid id, string text, int type) {
            this.Id = id;
            this.Text = text;
            this.Type = type;
        }
    }
}
