using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{

    public class RegisterViewModel
    {
        // Поле для електронної пошти користувача
        [Required]  // Це поле обов'язкове для заповнення
        [EmailAddress]  // Перевірка, щоб введена строка мала правильний формат електронної пошти (наприклад, user@example.com)
        public string Email { get; set; }

        // Поле для пароля користувача
        [Required]  // Це поле обов'язкове для заповнення
        [DataType(DataType.Password)]  // Вказує, що це поле призначене для пароля, тобто символи в ньому повинні маскуватися (зірочки або крапки)
        public string Password { get; set; }

        // Поле для підтвердження пароля
        [DataType(DataType.Password)]  // Маскування введених символів, щоб це виглядало як поле для пароля
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]  // Перевірка, чи співпадає введений пароль з підтвердженням
                                                                        // Якщо паролі не співпадають, буде виведено повідомлення про помилку
        public string ConfirmPassword { get; set; }

        // Поле для введення імені користувача
        [Required]  // Це поле є обов'язковим для заповнення
        public string FirstName { get; set; }

        // Поле для введення прізвища користувача
        [Required]  // Це поле також є обов'язковим
        public string LastName { get; set; }
    }
}

