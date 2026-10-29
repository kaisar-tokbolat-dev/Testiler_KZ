using Microsoft.EntityFrameworkCore;

namespace Testiler_KZ {
    
    public enum TestStatus {
        // Незавершен
        Unfinished = 0,
        // Опубликован
        Published,
        // Проверен модератором
        Checked,
        // Отклонен модератором
        Rejected,
    }

    /// <summary>
    /// Тест.
    /// </summary>
    public class Test {
        /// <summary>
        /// Идентификатор теста.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок теста.
        /// </summary>
        private string _title = string.Empty;
        public string Title {
            get => _title;
            set {
                string title = value.Trim();
                if (title.Length < TestConstraints.MinTitleLength || title.Length > TestConstraints.MaxTitleLength) {
                    throw new ArgumentException("invalidTestTitleFormat");
                }
                _title = title;
            }
        }

        // Категория теста
        public Guid? CategoryId { get; set; } = null;

        /// <summary>
        /// Описание теста.
        /// </summary>
        private string _description = string.Empty;
        public string Description {
            get => _description;
            set {
                string description = value.Trim();
                if (description.Length < TestConstraints.MinDescriptionLength || description.Length > TestConstraints.MaxDescriptionLength) {
                    throw new ArgumentException("invalidTestDescriptionFormat");
                }
                _description = description;
            }
        }
        
        /// <summary>
        /// Название файла изображения.
        /// </summary>
        public string ImageFileName { get; set; } = string.Empty;

        /// <summary>
        /// Автор теста.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Дата создания теста.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Дата публикации теста.
        /// </summary>
        public DateTime? PublishedDateTime { get; set; }

        /// <summary>
        /// Статус теста
        /// </summary>
        private int _status;
        public int Status {
            get => _status;
            set {
                if (!Enum.GetValues(typeof(TestStatus)).Cast<int>().Contains(value)) {
                    throw new ArgumentException("invalidQuestionTypeFormat");
                }
                _status = value;
            }
        }

        /// <summary>
        /// Отзывы теста.
        /// </summary>
        public List<Review> Reviews { get; set; } = new();

        /// <summary>
        /// Вопросы теста.
        /// </summary>
        private readonly List<Question> _questions = new();

        public IReadOnlyCollection<Question> Questions => _questions;

        /// <summary>
        /// Добавить вопрос.
        /// </summary>
        public Test AddQuestion(Question question) {
            // Порядок вопроса
            int order = _questions.Count + 1;
            question.Order = order;

            // Если имеются два или более одинаковых вопроса
            if (_questions.Any(q => q.Text.Trim() == question.Text.Trim())) {
                throw new ArgumentException("questionExistsError");
            }

            // Добавить вопрос
            _questions.Add(question);

            return this;
        }

        // Добавить коллекцию вопросов.
        public Test AddRangeQuestion(IEnumerable<Question> questions) {
            // Временные вопросы
            var tmp_questions = _questions.Concat(questions).ToArray();

            // Если имеются два или более одинаковых вопроса
            if (tmp_questions.DistinctBy(q => q.Text.Trim()).Count() < tmp_questions.Length) {
                throw new ArgumentException("questionExistsError");
            }

            foreach (Question question in questions) {
                AddQuestion(question);
            }
            return this;
        }

        // Удалить вопрос
        public Test RemoveQuestion(Guid questionId) {
            // Вопрос
            var question = _questions.Where(q => q.Id == questionId).FirstOrDefault();

            // Если вопроса не существует
            if (question == null) {
                throw new ArgumentException("questionNotFound");
            }

            // Удалить вопрос
            _questions.Remove(question);

            // Изменить порядк остальных вопросов
            _questions.OrderBy(q => q.Order);
            for (int i = 0; i < _questions.Count; i++) {
                _questions[i].Order = i + 1;
            }

            return this;
        }

        // Заменить вопрос
        public Test ReplaceQuestion(Question new_question, DbContext db) {
            // Старый вопрос
            var old_question = _questions.Where(q => q.Id == new_question.Id).First();

            // Удалить старый вопрос
            _questions.Remove(old_question);
            db.Entry(old_question).State = EntityState.Deleted;

            // Назначить порядок новому вопросу
            new_question.Order = old_question.Order;
            new_question.TestId = this.Id;

            // Добавить новый вопрос
            _questions.Add(new_question);
            db.Entry(new_question).State = EntityState.Added;

            return this;
        }

        private Test() { }

        // Конструктор
        public Test(Guid id, string title, Guid categoryId, string description, string imageFileName, Guid authorId, DateTime createdDateTime, int status) {
            this.Id = id;
            this.Title = title;
            this.CategoryId = categoryId;
            this.Description = description;
            this.ImageFileName = imageFileName;
            this.AuthorId = authorId;
            this.CreatedDateTime = createdDateTime;
            this.Status = status;
        }
    }
}
