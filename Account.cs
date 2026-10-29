using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Testiler_KZ {

    public enum Roles {
        admin = 0,
        user = 1
    }

    /// <summary>
    /// Учетная запись
    /// </summary>
    public class Account {
        // Регулярное выражение для логина
        private static readonly Regex LoginRegex = new Regex(@"\A(?=[^a-z]*[a-z])[a-z][a-z\d]{3,29}\z", RegexOptions.Compiled);

        // Регулярное выражение для пароля
        private static readonly Regex PasswordRegex = new Regex(@"\A(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])\S{8,30}\z", RegexOptions.Compiled);

        // Регулярное выражение для имени пользователя
        private static readonly Regex UsernameRegex = new Regex(@"\A[a-zA-Z0-9_]{4,30}\z", RegexOptions.Compiled);

        /// <summary>
        /// Идентификатор учетной записи.
        /// </summary>
        private Guid _id { get; set; }
        public Guid Id {
            get => _id;
            set {
                _id = value;
            }
        }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        private string _login { get; set; } = String.Empty;
        public string Login {
            get => _login;
            set {
                string login = value.Trim();
                if (!LoginRegex.IsMatch(login)) {
                    throw new ArgumentException("invalidLoginFormat");
                }
                _login = login;
            }
        }
        /// <summary>
        /// Пароль.
        /// </summary>
        private string _password { get; set; } = String.Empty;
        public string Password {
            get => _password; 
            set {
                string password = value.Trim();
                if (!PasswordRegex.IsMatch(password)) {
                    throw new ArgumentException("invalidPasswordFormat");
                }
                _password = password;
            }
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        private string _username { get; set; } = String.Empty;
        public string Username { get => _username;
            set {
                string username = value.Trim();
                if (!UsernameRegex.IsMatch(username)) {
                    throw new ArgumentException("invalidUsernameFormat");
                }
                _username = username;
            }
        }

        /// <summary>
        /// Роль.
        /// </summary>
        private int _role;
        public int Role {
            get => _role;
            set {
                if (!Enum.GetValues(typeof(Roles)).Cast<int>().Contains(value)) {
                    throw new ArgumentException("invalidRole");
                }
                _role = value;
            }
        }

        /// <summary>
        /// Дата создания аккаунта.
        /// </summary>
        public DateTime CreatedDateTime { get; }

        /// <summary>
        /// Параметры.
        /// </summary>
        public required Parameters Parameters { get; set; }

        public Account() { }

        [SetsRequiredMembers]
        public Account(Guid id, string login, string password, string username, int role, DateTime createdDateTime, Parameters parameters) {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Username = username;
            this.Role = role;
            this.CreatedDateTime = createdDateTime;
            this.Parameters = parameters;
        }
    }
}
