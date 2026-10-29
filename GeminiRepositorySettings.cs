using Newtonsoft.Json;

namespace Testiler_KZ {
    // Репозиторий для работы с json-файлом
    public class GeminiRepositorySettings {
        private readonly string _file;

        // Конструктор
        public GeminiRepositorySettings(string file) {
            this._file = file;
        }

        // Загрузка конфигурации
        public GeminiParams Load() {
            // Проверка существования файла
            if (!File.Exists(this._file)) {
                throw new FileNotFoundException();
            }

            // JSON-файл
            string json = File.ReadAllText(this._file);

            GeminiParams? geminiParams = JsonConvert.DeserializeObject<GeminiParams>(json);

            if (geminiParams == null) {
                throw new ArgumentNullException();
            }

            // Параметры ИИ
            return geminiParams;
        }
    }
}
