namespace Testiler_KZ {
    /// <summary>
    /// Категория теста.
    /// </summary>
    public class Category {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        private string _name = String.Empty;
        public string Name {
            get => _name;
            set {
                string name = value.Trim();
                if (name.Length == 0) {
                    throw new ArgumentException();
                }
                _name = name;
            }
        }

        /// <summary>
        /// Язык категории.
        /// </summary>
        private string _language = String.Empty;
        public string Language {
            get => _language;
            set {
                string _language = value.Trim().ToLower();
                if (_language.Length == 0) {
                    throw new ArgumentException();
                }
                this._language = _language;
            }
        }

        /// <summary>
        /// Текст категории.
        /// </summary>
        private string _text = String.Empty;
        public string Text {
            get => _text;
            set {
                string text = value.Trim();
                if (text.Length == 0) {
                    throw new ArgumentException();
                }
                _text = text;
            }
        }

        /// <summary>
        /// Конструктор 1.
        /// </summary>
        public Category() { }

        /// <summary>
        ///  Конструктор 2
        /// </summary>
        public Category(Guid id, string name, string language, string text) {
            this.Id = id;
            this.Name = name;
            this.Language = language;
            this.Text = text;
        }
    }
}
