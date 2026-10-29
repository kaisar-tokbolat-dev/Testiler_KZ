namespace Testiler_KZ {
    /// <summary>
    /// Параметры.
    /// </summary>
    public class Parameters {

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Выбранный язык.
        /// </summary>
        public string SelectedLanguage { get; set; } = String.Empty;

        public Parameters() { }

        public Parameters(string SelectedLanguage) {
            this.Id = Guid.NewGuid();
            this.SelectedLanguage = SelectedLanguage;
        }
    }
}
