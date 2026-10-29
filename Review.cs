namespace Testiler_KZ {
    /// <summary>
    /// Отзыв теста.
    /// </summary>
    public class Review {

        /// <summary>
        /// ID отзыва.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID автора отзыва.
        /// </summary>
        public Guid AuthorId { get; set; }

        private string _comment = string.Empty;

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment {
            get => _comment;
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value), "invalidCommentFormat");
                }
                string comment = value.Trim();
                if (comment.Length < 10) {
                    throw new ArgumentException("invalidCommentFormat");
                }
                this._comment = comment;
            }
        }

        /// <summary>
        /// Рейтинг.
        /// </summary>
        private double _rating;
        public double Rating {
            get => _rating;
            set {
                if (value < 1 || value > 5) {
                    throw new ArgumentException("invalidRatingFormat");
                }
                _rating = value;
            }
        }

        /// <summary>
        /// Дата создания отзыва.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Дата публикации отзыва.
        /// </summary>
        public DateTime? PublishedDateTime { get; set; } = null;

        /// <summary>
        /// ID теста.
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Был ли проверен отзыв.
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Конструктор 1.
        /// </summary>
        private Review() { }

        /// <summary>
        /// Конструктор 2.
        /// </summary>
        public Review(Guid id, Guid authorId, string comment, double rating, DateTime createdDateTime, Guid testId, bool isChecked) {
            this.Id = id;
            this.AuthorId = authorId;
            this.Comment = comment;
            this.Rating = rating;
            this.CreatedDateTime = createdDateTime;
            this.TestId = testId;
            this.IsChecked = isChecked;
        }
    }
}
