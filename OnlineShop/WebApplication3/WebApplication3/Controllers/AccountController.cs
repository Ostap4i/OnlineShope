using Microsoft.AspNetCore.Identity; // Простір імен для роботи з ASP.NET Core Identity
using Microsoft.AspNetCore.Mvc;    // Простір імен для контролерів і переглядів
using Microsoft.Win32;
// Простір імен для моделей (як RegisterViewModel, LoginViewModel і т.д.)
using System.Threading.Tasks;
using WebApplication3.Model;      // Простір імен для роботи з асинхронними операціями

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager; // Для роботи з користувачами
    private readonly SignInManager<ApplicationUser> _signInManager; // Для роботи з аутентифікацією

    // Конструктор, що приймає UserManager і SignInManager
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager; // Ініціалізація UserManager
        _signInManager = signInManager; // Ініціалізація SignInManager
    }

    // Сторінка реєстрації (GET)
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Повертає форму реєстрації
    }

    // Обробка реєстрації користувача (POST)
    [HttpPost]
    [ValidateAntiForgeryToken] // Захист від CSRF атак
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // Перевірка чи є модель валідною (читається з атрибутів в моделі)
        if (ModelState.IsValid)
        {
            // Створюємо новий об'єкт користувача на основі даних з форми
            var user = new User
            {
                UserName = model.Email,  // Встановлюємо ім'я користувача як email
                Email = model.Email,     // Встановлюємо email користувача
                FirstName = model.FirstName,  // Ім'я користувача
                LastName = model.LastName   // Прізвище користувача
            };

            // Спробуємо створити користувача з паролем
            var result = await _userManager.CreateAsync(user, model.Password);

            // Якщо користувача створено успішно
            if (result.Succeeded)
            {
                // Виконуємо вхід користувача після реєстрації (якщо успішно зареєстровано)
                await _signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false означає, що сесія не буде зберігатися після закриття браузера
                return RedirectToAction("Index", "Home"); // Перехід на головну сторінку після успішної реєстрації
            }

            // Якщо виникли помилки під час реєстрації, додаємо їх в модель (для відображення на формі)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); // Додаємо помилки в модель для відображення користувачу
            }
        }

        // Якщо модель не валідна або виникли помилки, повертаємо користувача назад на форму реєстрації
        return View(model);
    }

    // Сторінка логіну (GET)
    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Повертає форму логіну
    }

    // Обробка логіну користувача (POST)
    [HttpPost]
    [ValidateAntiForgeryToken] // Захист від CSRF атак
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/"); // Якщо не передано параметр returnUrl, використовуємо головну сторінку

        // Якщо модель валідна (всі поля введені правильно)
        if (ModelState.IsValid)
        {
            // Пробуємо увійти за допомогою логіну та пароля
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            // Якщо вхід успішний
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl); // Перехід на потрібну сторінку після успішного логіну
            }
            else
            {
                // Якщо вхід не вдався, додаємо помилку в модель для відображення на формі
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль.");
            }
        }

        // Якщо модель не валідна або вхід не вдалося здійснити, повертаємо користувача на сторінку логіну
        return View(model);
    }

    // Вихід з системи (POST)
    [HttpPost]
    [ValidateAntiForgeryToken] // Захист від CSRF атак
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync(); // Виконуємо вихід з системи
        return RedirectToAction("Index", "Home"); // Переходимо на головну сторінку після виходу
    }

    // Допоміжний метод для перенаправлення на правильну сторінку після логіну
    private IActionResult RedirectToLocal(string returnUrl)
    {
        // Якщо returnUrl є локальним, редиректимо на нього
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home"); // Якщо returnUrl не є локальним, перенаправляємо на головну сторінку
        }
    }
}

