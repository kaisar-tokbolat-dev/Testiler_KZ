using Newtonsoft.Json;

namespace Testiler_KZ {
    
    // Параметры ИИ
    public class GeminiParams {
        /// <summary>
        /// Модель Gemini
        /// </summary>
        [JsonProperty("Model")]
        public string Model = string.Empty;

        /// <summary>
        /// Запрос для генерации вопросов
        /// </summary>
        [JsonProperty("QuestionGenerationQuery")]
        private string QuestionGenerationQuery = string.Empty;

        public string GetQuestionGenerationQuery(QuestionView[] questionViews, string? language) {
            return QuestionGenerationQuery
                .Replace("{questionViews}", JsonConvert.SerializeObject(questionViews))
                .Replace("{language}", language ?? "ru");
        }

        /// <summary>
        /// Запрос для проверки грамматики
        /// </summary>
        [JsonProperty("GrammarCheckQuery")]
        private string GrammarCheckQuery = string.Empty;

        public string GetGrammarCheckQuery(QuestionViewWithId[] questionViews) {
            return GrammarCheckQuery.
                Replace("{questionViews}", JsonConvert.SerializeObject(questionViews));
        }

        /// <summary>
        /// Запрос для обращения к ИИ с вопросом
        /// </summary>
        [JsonProperty("QuestionForAskQuery")]
        private string QuestionForAskQuery = string.Empty;

        public string GetQuestionForAskQuery(QuestionForAsk questionForAsk, string? language) {
            return QuestionForAskQuery
                .Replace("{questionForAsk}", JsonConvert.SerializeObject(questionForAsk))
                .Replace("{language}", language ?? "ru");
        }
    }
}
