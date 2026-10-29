using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Sources;
using Testiler_KZ;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
        options => {
            options.LoginPath = "/authorization.html";
        });
builder.Services.AddAuthorization();

// Подключение к базе данных
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlite(connection));

var app = builder.Build();

app.UseAntiforgery();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.EnsureCreated();

    // Добавить учетных записей в базу данных
    if (!db.Accounts.Any()) {
        foreach (Account account in ApplicationContext.accounts) {
            db.Accounts.Add(account);
        }
    }

    // Добавить категории в базу данных
    if (!db.Categories.Any()) {
        foreach (Category category in ApplicationContext.categories) {
            db.Categories.Add(category);
        }
    }

    //Добавить тесты в базу данных
    if (!db.Tests.Any()) {
        foreach (Test test in ApplicationContext.tests) {
            db.Tests.Add(test);
        }
    }

    // Добавить отзывы в базу данных
    if (!db.Reviews.Any()) {
        foreach (Review review in ApplicationContext.reviews) {
            db.Reviews.Add(review);
        }
    }

    db.SaveChanges();
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Количество тестов
app.MapGet("/api/admin/test-count", [Authorize(Roles = "admin")] async ([FromQuery] string? filter, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество тестов
    int testCount = 0;

    // Тесты с категориями
    if (filter == "with") {
        testCount = await db.Tests.CountAsync(t => t.CategoryId != null);
    }

    // Тесты без категорий
    else if (filter == "without") {
        testCount = await db.Tests.CountAsync(t => t.CategoryId == null);
    }
    
    // Общее количество тестов
    else {
        testCount = await db.Tests.CountAsync();
    }

    // Вернуть количество тестов
    return Results.Json(new { testCount });
});

// Количество категорий
app.MapGet("/api/admin/category-count", [Authorize(Roles = "admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество категорий
    var categoryCount = await db.Categories.CountAsync();

    // Вернуть количество категорий
    return Results.Json(new { categoryCount });
});

// Количество отзывов
app.MapGet("/api/admin/review-count", [Authorize(Roles = "admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество отзывов
    var reviewCount = await db.Reviews.CountAsync();

    // Вернуть количество отзывов
    return Results.Json(new { reviewCount });
});

// Новые отзывы
app.MapGet("/api/new-reviews/preview", [Authorize(Roles = "admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество отзывов
    int count = 3;

    // Новые отзывы
    var new_reviews = await db.Reviews
        .AsNoTracking()
        .Where(r => r.IsChecked == false)
        .Select(r => new {
            r.Id,
            Username = db.Accounts.Where(a => a.Id == r.AuthorId).Select(p => p.Username).FirstOrDefault(),
            r.Comment,
            r.Rating,
            r.CreatedDateTime
        })
        .OrderByDescending(r => r.CreatedDateTime)
        .Take(count)
        .ToListAsync();

    // Вернуть отзывы
    return Results.Json(new { reviews = new_reviews });
});

// Количество пользователей
app.MapGet("/api/admin/user-count", [Authorize(Roles = "admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество пользователей
    var userCount = await db.Accounts.CountAsync(a => a.Role == (int)Roles.user);

    // Вернуть количество пользователей
    return Results.Json(new { userCount });
});

// Загрузка выбранного теста
app.MapGet("/api/test-info-view", [Authorize(Roles = "admin")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidTestId)
        .Include(t => t.Questions)
        .ThenInclude(q => q.Answers)
        .Select(t => new {
            t.Id,
            t.ImageFileName,
            t.AuthorId,
            t.Title,
            Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault(),
            t.CategoryId,
            t.PublishedDateTime,
            Username = db.Accounts
               .Where(p => p.Id == t.AuthorId)
               .Select(p => p.Username)
               .FirstOrDefault(),
            Status = ((TestStatus)t.Status).ToString(),
            t.Description,
            t.Questions,
            Reviews = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(
            r => new {
                r.Id,
                r.AuthorId,
                Username = db.Accounts.Where(a => a.Id == r.AuthorId).Select(p => p.Username).FirstOrDefault(),
                r.Comment,
                r.Rating,
                r.CreatedDateTime,
                r.PublishedDateTime
            }
        )
        .ToList(),
            AverageRating = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(r => (double?)r.Rating).Average() ?? 0
        }).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    return Results.Json(new { test });
});

// Загрузка выбранного отзыва
app.MapGet("/api/review", [Authorize(Roles = "admin")] async ([FromQuery] string reviewId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID отзыва
    if (!Guid.TryParse(reviewId, out var guidReviewId)) {
        return Results.Content("invalidReviewId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Отзыв
    var review = await db.Reviews.AsNoTracking().Where(r => r.Id == guidReviewId)
        .Select(r => new {
            r.Id,
            r.TestId,
            r.IsChecked,
            r.Rating,
            r.CreatedDateTime,
            r.Comment,
            Username = db.Accounts.Where(a => a.Id == r.AuthorId).First().Username ?? null
        })
        .FirstOrDefaultAsync();

    // Если отзыв не найден
    if (review == null) {
        return Results.Content("reviewNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    return Results.Json(new { review });
});

// Удалить отзыв
app.MapGet("/api/delete-review", [Authorize(Roles = "admin")] async ([FromQuery] string reviewId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID отзыва
    if (!Guid.TryParse(reviewId, out var guidReviewId)) {
        return Results.Content("invalidReviewId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Отзыв
    var review = await db.Reviews.Where(r => r.Id == guidReviewId).FirstOrDefaultAsync();

    // Если отзыв не найден
    if (review == null) {
        return Results.Content("reviewNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Удалить отзыв
        db.Reviews.Remove(review);

        // Сохранить изменения
        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
});

// Опубликовать отзыв
app.MapGet("/api/publish-review", [Authorize(Roles = "admin")] async ([FromQuery] string reviewId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID отзыва
    if (!Guid.TryParse(reviewId, out var guidReviewId)) {
        return Results.Content("invalidReviewId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Отзыв
    var review = await db.Reviews.Where(r => r.Id == guidReviewId).FirstOrDefaultAsync();

    // Если отзыв не найден
    if (review == null) {
        return Results.Content("reviewNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Опубликовать отзыв
        review.IsChecked = true;
        review.PublishedDateTime = DateTime.Now;

        // Сохранить изменения
        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
});

// Одобрить тест
app.MapPost("/api/accept-test", [Authorize(Roles = "admin")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId).FirstOrDefaultAsync();

    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        test.Status = (int)TestStatus.Checked;

        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 404);
    }

}).DisableAntiforgery();

// Отклонить тест
app.MapPost("/api/reject-test", [Authorize(Roles = "admin")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected)).FirstOrDefaultAsync();

    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        if (test.Status != (int)TestStatus.Rejected) {
            test.Status = (int)TestStatus.Rejected;
            await db.SaveChangesAsync();
        }

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 404);
    }

}).DisableAntiforgery();

// Загрузка главной страницы
app.MapGet("/home/{*section}", [Authorize(Roles = "user, admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Роль аккаунта
    var accountRole = await db.Accounts
        .AsNoTracking()
        .Where(p => p.Id == accountId)
        .Select(p => (Roles?)p.Role)
        .FirstOrDefaultAsync();

    // Если результат null
    if (accountRole == null) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Файл главной страницы на основе роли
    string fileName = accountRole switch {
        Roles.admin => "main_admin.html",
        _ => "main.html"
    };

    // Отобразить файл
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", fileName), "text/html");
});

// Опубликовать тест
app.MapGet("/api/publish-test", [Authorize(Roles = "user")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected)).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Дата публикации теста
    test.PublishedDateTime = DateTime.Now;

    // Изменить статус теста
    test.Status = (int)TestStatus.Published;
    
    // Сохранить изменения
    await db.SaveChangesAsync();

    return Results.Ok();
});

// Получить тесты по категориям
app.MapGet("/api/category/tests", [Authorize(Roles = "user")] async ([FromQuery] string category, [FromQuery] int? limit, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тесты без категории
    if (category == "uncategorized") {
        // Тесты по выбранной категории
        var uncategorizedTests = await db.Tests
            .AsNoTracking().Where(t =>
                (t.Status == (int)TestStatus.Published || t.Status == (int)TestStatus.Checked)
                &&
                t.CategoryId == null
            )
            .Select(t => new {
                t.Id,
                t.ImageFileName,
                t.Title,
                t.PublishedDateTime,
                Username = db.Accounts
                    .Where(p => p.Id == t.AuthorId)
                    .Select(p => p.Username)
                    .FirstOrDefault(),
                Status = ((TestStatus)t.Status).ToString(),
                ReviewsCount = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Count(),
                AverageRating = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(r => (double?)r.Rating).Average() ?? 0
            })
            .ToListAsync();

        if (limit.HasValue) {
            return Results.Json(uncategorizedTests.Take(limit.Value));
        }

        return Results.Json(uncategorizedTests);
    }

    // ID категории
    var selectedCategory = await db.Categories.Where(c => c.Name == category).FirstOrDefaultAsync();

    // Если отсутствует категория
    if (selectedCategory == null) {
        return Results.Content("categoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тесты по выбранной категории
    var tests = await db.Tests
        .AsNoTracking().Where(t =>
            (t.Status == (int)TestStatus.Published || t.Status == (int)TestStatus.Checked)
            &&
            t.CategoryId == selectedCategory.Id
        )
        .Select(t => new {
            t.Id,
            t.ImageFileName,
            t.Title,
            t.PublishedDateTime,
            CategoryText = db.Categories.Where(c => c.Id == selectedCategory.Id).Select(c => c.Text).FirstOrDefault() ?? null,
            Username = db.Accounts
                .Where(p => p.Id == t.AuthorId)
                .Select(p => p.Username)
                .FirstOrDefault(),
            Status = ((TestStatus)t.Status).ToString(),
            ReviewsCount = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Count(),
            AverageRating = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(r => (double?)r.Rating).Average() ?? 0
        }).OrderBy(t => t.CategoryText)
        .ToListAsync();

    // Если тесты отсутствуют
    if (tests == null) {
        return Results.Content("testNotFoundByCategory", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Отправить тесты
    if (limit.HasValue) {
        return Results.Json(tests.Take(limit.Value));
    }
    return Results.Json(tests);

});

// Получить категорию по названию
app.MapGet("/api/category", [Authorize(Roles = "user")] async ([FromQuery] string categoryName, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Категория
    var category = await db.Categories
        .AsNoTracking()
        .Where(c => c.Name == categoryName)
        .FirstOrDefaultAsync();

    // Если категория не найдена
    if (category == null) {
        return Results.Content("categoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    return Results.Json(category);
});

// Получить категории
app.MapGet("/api/categories", [Authorize(Roles = "user")] async ([FromQuery] int? limit, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    if (limit.HasValue) {
        return Results.Json(db.Categories.Take(limit.Value).OrderBy(c => c.Text));
    }

    return Results.Json(db.Categories.OrderBy(c => c.Text));
});

// Поиск тестов
app.MapPost("/api/test-search", [Authorize(Roles = "user")] async ([FromForm] string text, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Если текст пустой
    if (string.IsNullOrWhiteSpace(text)) {
        return Results.Ok(new List<Test>());
    }

    try {
        // Тесты
        var tests = await db.Tests
            .AsNoTracking()
            .Select(t => new {
                t.Id,
                t.ImageFileName,
                t.Title,
                t.Description,
                t.PublishedDateTime,
                Username = db.Accounts
                    .Where(p => p.Id == t.AuthorId)
                    .Select(p => p.Username)
                    .FirstOrDefault(),
                t.Status
            })
            .Where(t =>
                (
                    EF.Functions.Like(t.Title, $"%{text}%") ||
                    EF.Functions.Like(t.Username, $"%{text}%") ||
                    EF.Functions.Like(t.Description, $"%{text}%")
                ) && (
                    t.Status == (int)TestStatus.Published || t.Status == (int)TestStatus.Checked
                )
            )
            .ToListAsync();

        // Преобразованные тесты
        var parsedTests = tests.Select(t => new {
            t.Id,
            t.ImageFileName,
            t.Title,
            t.Description,
            t.PublishedDateTime,
            t.Username,
            Status = ((TestStatus)t.Status).ToString(),
            ReviewsCount = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Count(),
            AverageRating = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(r => (double?)r.Rating).Average() ?? 0,
            titleData = t.Title,
            usernameData = t.Username,
            descriptionData = t.Description,
        }).ToList();

        return Results.Json(parsedTests);
    }

    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery().RequireAuthorization();

// Страница создания теста
app.MapGet("/create-test/{*section}", [Authorize(Roles = "user")] () => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "create_test.html"), "text/html; charset=utf-8");
}).RequireAuthorization();

// Отобразить созданный тест, если он незавершен / отклонен
app.MapGet("/api/create-test", [Authorize(Roles = "user")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected))
        .FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Отправить тест
    return Results.Json(test);
}).RequireAuthorization();

// Страница добавления вопросов
app.MapGet("/questions/{*section}", [Authorize(Roles = "user")] (ApplicationContext db) => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "questions.html"), "text/html; charset=utf-8");
}).RequireAuthorization(); ;

// Отобразить вопросы теста, если он незавершен / отклонен
app.MapGet("/api/questions/{*section}", [Authorize(Roles = "user")] async ([FromQuery] string testId, ApplicationContext db) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected))
        .Include(t => t.Questions.OrderBy(q => q.Order))
        .ThenInclude(q => q.Answers.OrderBy(a => a.Order))
        .FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест с вопросами
    return Results.Json(new { title = test.Title, questions = test.Questions });

}).RequireAuthorization();

// Страница добавления вопроса
app.MapGet("/add-question", [Authorize(Roles = "user")] async (string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected))
        .FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Отобразить страницу
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "add_question.html"), "text/html; charset=utf-8");
});

// Добавить вопросы
app.MapPost("/api/add-question/{*section}", [Authorize(Roles = "user")] async (
    [FromQuery] string testId,
    ClaimsPrincipal user,
    ApplicationContext db,
    [FromForm] string questionsJson
) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected)).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    if (string.IsNullOrWhiteSpace(questionsJson)) {
        return Results.BadRequest("questionsNotFound");
    }

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    QuestionData[] questionDataCollection = System.Text.Json.JsonSerializer.Deserialize<QuestionData[]>(questionsJson ?? "", options) ?? Array.Empty<QuestionData>();

    if (questionDataCollection == null || questionDataCollection.Length == 0) {
        return Results.BadRequest("questionsNotFound");
    }

    // Добавить вопросы
    try {
        foreach (var questionData in questionDataCollection) {
            // Ответы для чтения
            AnswerData[] answersData = questionData.answerDataCollection;

            // Ответы
            Answer[] answers = new Answer[answersData.Length];
            for (int i = 0; i < answers.Length; i++) {
                answers[i] = new Answer(answersData[i].Id, answersData[i].Text, answersData[i].IsCorrect);
            }

            // Вопрос
            Question question = new Question(questionData.id, questionData.text, questionData.type);

            // Добавить ответы
            question.AddRangeAnswer(answers, db);

            // Добавить вопрос
            test.AddQuestion(question);

            // Сохранить изменения
            await db.SaveChangesAsync();
        }
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }

    return Results.Ok();
}).DisableAntiforgery();

// Изменить вопрос
app.MapPost("/api/change-question/{*section}", [Authorize(Roles = "user")] async (
    [FromQuery] string testId,
    ApplicationContext db,
    [FromForm] string questionsJson,
    ClaimsPrincipal user
) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected)).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    if (string.IsNullOrWhiteSpace(questionsJson)) {
        return Results.BadRequest("questionsNotFound");
    }

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    QuestionData[] questionDataCollection = System.Text.Json.JsonSerializer.Deserialize<QuestionData[]>(questionsJson ?? "", options) ?? Array.Empty<QuestionData>();

    // Если вопросы отсутствуют
    if (questionDataCollection == null || questionDataCollection.Length == 0) {
        return Results.BadRequest("questionsNotFound");
    }

    // Изменить вопросы
    try {
        foreach (var questionData in questionDataCollection) {
            // Ответы для чтения
            AnswerData[] answersData = questionData.answerDataCollection;

            // Ответы
            Answer[] answers = new Answer[answersData.Length];
            for (int i = 0; i < answers.Length; i++) {
                answers[i] = new Answer(answersData[i].Id, answersData[i].Text, answersData[i].IsCorrect);
            }

            // Новый вопрос
            Question new_question = new Question(questionData.id, questionData.text, questionData.type);

            // Заменить вопрос
            test.ReplaceQuestion(new_question, db);

            // Добавить ответы
            new_question.AddRangeAnswer(answers, db);
        }

        // Сохранить изменения
        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

// Удалить вопрос
app.MapPost("/api/delete-question", [Authorize(Roles = "user")] async ([FromQuery] string testId, [FromQuery] string questionId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && (t.Status == (int)TestStatus.Unfinished || t.Status == (int)TestStatus.Rejected)).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID вопроса
    if (!Guid.TryParse(questionId, out var guidQuestionId)) {
        return Results.Content("invalidQuestionIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Удалить вопрос
    try {
        // Удалить вопрос из теста
        test.RemoveQuestion(guidQuestionId);

        // Сохранить изменения
        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

// Получить имя учетной записи
app.MapGet("/api/username", [Authorize(Roles = "user, admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Аккаунт
    var account = await db.Accounts
        .AsNoTracking()
        .Where(p => p.Id == guidAccountId)
        .Select(a => new {
            a.Username
        })
        .FirstOrDefaultAsync();

    // Аккаунт пользователя
    if (account == null) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Имя пользователя
    var username = account.Username;
    
    // Вернуть имя
    return Results.Json(new { username });
});

// Получить язык учетной записи
app.MapGet("/api/language", [Authorize(Roles = "user, admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт
    var account = await db.Accounts
        .AsNoTracking()
        .Where(a => a.Id == guidAccountId)
        .Include(a => a.Parameters)
        .Select(a => new {
            a.Parameters
        })
        .FirstOrDefaultAsync();

    // Аккаунт пользователя
    if (account == null) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Язык
    var selectedLanguage = account.Parameters.SelectedLanguage;

    // Вернуть язык
    return Results.Json(new { selectedLanguage });
});

// Загрузка тестов в HTML-странице "main.html"
app.MapGet("/api/tests", [Authorize(Roles = "user")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    return Results.Json(await db.Tests
        .AsNoTracking().Where(t => t.Status == (int)TestStatus.Published || t.Status == (int)TestStatus.Checked)
        .Select(t => new {
            t.Id,
            t.ImageFileName,
            t.Title,
            t.PublishedDateTime,
            Username = db.Accounts
                .Where(p => p.Id == t.AuthorId)
                .Select(p => p.Username)
                .FirstOrDefault(),
            t.Status
        })
        .ToListAsync());
});

// Загрузка первых четырех новых тестов
app.MapGet("/api/new-tests/preview", [Authorize(Roles = "admin")] async (ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество тестов
    int count = 4;

    // Новые тесты
    var new_tests = await db.Tests
        .AsNoTracking()
        .Where(t => t.Status == (int)TestStatus.Published)
        .Select(t => new {
            t.Id,
            t.Title,
            t.ImageFileName,
            t.AuthorId,
            t.CreatedDateTime,
            t.PublishedDateTime,
            t.Status
        })
        .OrderByDescending(t => t.PublishedDateTime)
        .Take(count)
        .ToListAsync();

    // Преобразованные тесты
    var converted_tests = new_tests.
    Select(t => new {
        t.Id,
        t.Title,
        t.ImageFileName,
        Username = db.Accounts
                .Where(p => p.Id == t.AuthorId)
                .Select(p => p.Username)
                .FirstOrDefault(),
        t.CreatedDateTime,
        t.PublishedDateTime,
        Status = ((TestStatus)t.Status).ToString(),
    })
    .ToList();

    // Вернуть тесты
    return Results.Json(new { tests = converted_tests });
});

// Загрузка новых тестов
app.MapGet("/api/new-tests", [Authorize(Roles = "admin")] async ([FromQuery] int? limit, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество тестов пользователя
    var user_test_count = await db.Tests.Where(t => t.AuthorId == accountId).CountAsync();

    // Количество тестов на одной странице
    int count = 10;

    if (limit.HasValue) {
        // Номер страницы
        int page = limit.Value;

        // Стартовый индекс (для 1-й страницы: 1*10 - 10 = 0)
        int start = page * count - count;

        // Переходы
        var back = start - count >= 0;
        var next = (start + count) < user_test_count;

        // Тесты пользователя
        var pagedTestData = await db.Tests
        .AsNoTracking()
        .Where(t => t.Status == (int)TestStatus.Published)
        .Select(t => new {
            t.Id,
            t.Title,
            t.CategoryId,
            t.ImageFileName,
            t.AuthorId,
            t.CreatedDateTime,
            t.PublishedDateTime,
            t.Status
        })
        .OrderByDescending(t => t.PublishedDateTime)
        .ToListAsync();

        // Пронумерованные тесты
        var pagedTests = pagedTestData
        .Select((t, index) => new {
            Order = start + index + 1,
            t.Id,
            t.Title,
            Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault() ?? null,
            t.ImageFileName,
            Username = db.Accounts
                .Where(p => p.Id == t.AuthorId)
                .Select(p => p.Username)
                .FirstOrDefault(),
            t.CreatedDateTime,
            t.PublishedDateTime,
            Status = ((TestStatus)t.Status).ToString(),
        }).ToList();

        // Вернуть тесты
        return Results.Json(new { back, next, tests = pagedTests });
    }

    // Тесты пользователя
    var firstTestData = await db.Tests
        .AsNoTracking()
        .Where(t => t.Status == (int)TestStatus.Published)
        .Take(count)
        .Select(t => new {
            t.Id,
            t.Title,
            t.CategoryId,
            t.ImageFileName,
            t.AuthorId,
            t.CreatedDateTime,
            t.PublishedDateTime,
            t.Status
        })
        .OrderByDescending(t => t.PublishedDateTime)
        .ToListAsync();

    // Пронумерованные тесты
    var firstTests = firstTestData
    .Select((t, index) => new {
        Order = index + 1,
        t.Id,
        t.Title,
        Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault() ?? null,
        t.ImageFileName,
        Username = db.Accounts
                .Where(p => p.Id == t.AuthorId)
                .Select(p => p.Username)
                .FirstOrDefault(),
        t.CreatedDateTime,
        t.PublishedDateTime,
        Status = ((TestStatus)t.Status).ToString(),
    }).ToList();

    // Вернуть тесты
    return Results.Json(new { back = false, next = count < user_test_count, tests = firstTests });
});

// Загрузка изображения теста
app.MapGet("/api/test-image/{imageFileName}", [Authorize(Roles = "user, admin")] (string imageFileName) => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "test_images", imageFileName));
});

// Открыть страницу теста
app.MapGet("/info", [Authorize(Roles = "user")] ([FromQuery] string testId) => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "test_info.html"), "text/html");
});

// Загрузка информации о теста
app.MapGet("/api/info", [Authorize(Roles = "user")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidTestId)
        .Select(t => new {
            t.Id,
            t.ImageFileName,
            t.AuthorId,
            t.Title,
            Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault(),
            t.CategoryId,
            t.PublishedDateTime,
            Username = db.Accounts
               .Where(p => p.Id == t.AuthorId)
               .Select(p => p.Username)
               .FirstOrDefault(),
            Status = ((TestStatus)t.Status).ToString(),
            t.Description,
            Reviews = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(
            r => new {
                r.Id,
                r.AuthorId,
                Username = db.Accounts.Where(a => a.Id == r.AuthorId).Select(p => p.Username).FirstOrDefault(),
                r.Comment,
                r.Rating,
                r.CreatedDateTime,
                r.PublishedDateTime
            }
        )
        .ToList(),
            AverageRating = db.Reviews.Where(r => r.TestId == t.Id && r.IsChecked).Select(r => (double?)r.Rating).Average() ?? 0
        }).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Принадлежит ли тест автору
    var is_my_test = test.AuthorId == guidAccountId;

    // Проверить, существует ли отзыв пользователя
    var can_add_review = !await db.Reviews.AnyAsync(r => r.AuthorId == guidAccountId && r.TestId == guidTestId);

    // Проходил ли пользователь тест
    var is_taked = await db.TestHistories.AnyAsync(t => t.TestId == guidTestId && t.UserId == guidAccountId);

    // Вернуть тест
    return Results.Json(new { isTaked = is_taked, canAddReview = can_add_review, isMyTest = is_my_test, test });
});

// Добавить отзыв
app.MapPost("/api/add-review", async ([FromForm] string testId, [FromForm] string comment, [FromForm] double rating, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId).Include(t => t.Reviews).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Если тест принадлежит автору
    if (test.AuthorId == guidAccountId) {
        return Results.Content("cannotReviewMyTest", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если отзыв существует
    if (test.Reviews.Any(r => r.AuthorId == guidAccountId)) {
        return Results.Content("reviewExists", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Отзыв
    try {
        var review = new Review(Guid.NewGuid(), guidAccountId, comment, rating, DateTime.Now, guidTestId, false);
        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }

}).DisableAntiforgery();

// Открыть страницу прохождения теста
app.MapGet("/view/test", [Authorize(Roles = "user")] (HttpContext context) => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "test_view.html"), "text/html");
});

// Загрузка теста для прохождения
app.MapGet("/api/view", [Authorize(Roles = "user")] async ([FromQuery] string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Тест
    var test = await db.Tests
        .AsNoTracking()
        .Where(t => t.Id == guidId)
        .Select(t => new {
            t.Title,
            Questions = t.Questions.Select(q => new {
                q.Id,
                q.Text,
                q.Type,
                Answers = q.Answers.Select(a => new {
                    a.Id,
                    a.Text
                })
            })
        })
        .FirstOrDefaultAsync();

    // Отправить тест при его наличии
    return test is not null
            ? Results.Json(test)
            : Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
});

// Завершение теста
app.MapPost("/api/complete", async (
        [FromQuery] string testId,
        [FromForm] string? answeredQuestionsJson,
        ClaimsPrincipal user,
        ApplicationContext db
    ) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testId, out var guidId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    // Отвеченные вопросы
    QuestionAndAnswers[] questions_and_answers = System.Text.Json.JsonSerializer.Deserialize<QuestionAndAnswers[]?>(answeredQuestionsJson ?? "", options) ?? Array.Empty<QuestionAndAnswers>();

    // Отвеченные вопросы отсутствуют
    if (questions_and_answers == null || questions_and_answers.Length == 0) {
        return Results.Content("answeredQuestionsNotFound", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Пройденный тест
    var passed_test = await db.Tests
    .AsNoTracking()
    .Where(t => t.Id == guidId)
    .Select(t => new {
        t.Id,
        t.Title,
        t.ImageFileName,
        Questions = t.Questions.Select(q => new {
            q.Id,
            q.Type,
            q.Text,
            Answers = q.Answers.Select(a => new {
                a.Id,
                a.IsCorrect,
                a.Text
            })
        })
    })
    .FirstOrDefaultAsync();

    // Если тест отстутствует
    if (passed_test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Истории вопросов
    List<QuestionHistory> questionHistories = new List<QuestionHistory>();

    // Перебор вопросов
    foreach (var question in passed_test.Questions) {
        // История вопросов
        QuestionHistory questionHistory = new QuestionHistory(
            Guid.NewGuid(),
            question.Id,
            question.Text,
            (int)question.Type,
            question.Answers.Select(a => new AnswerHistory(Guid.NewGuid(), a.Id, a.Text, a.IsCorrect)).ToList(),
            question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList(),
            questions_and_answers.Where(qA => qA.QuestionId == question.Id).SelectMany(qA => qA.AnswersId).Cast<Guid>().ToList()
        );
        questionHistories.Add(questionHistory);
    }

    // Подсчет результатов
    foreach (var questionHistory in questionHistories) {
        // Правильные ответы
        var correctAnswer = questionHistory.CorrectAnswers.Distinct().ToArray();

        if (questionHistory.UserAnswers.Count <= questionHistory.CorrectAnswers.Count) {
            foreach (var userAnswer in questionHistory.UserAnswers) {
                if (questionHistory.CorrectAnswers.Contains(userAnswer)) {
                    questionHistory.Score += 1;
                }
            }
        }
    }

    // Максимальный балл
    double max_score = questionHistories.Select(q => q.Answers.Where(a => a.IsCorrect).Count()).Sum();

    // Заработанные баллы
    double score = questionHistories.Select(q => q.Score).Sum();

    // История теста
    TestHistory testHistory = new TestHistory(
        Guid.NewGuid(),
        passed_test.Id,
        passed_test.Title,
        accountId,
        DateTime.Now,
        passed_test.ImageFileName,
        questionHistories,
        score,
        max_score
    );

    db.TestHistories.Add(testHistory);
    await db.SaveChangesAsync();

    return Results.Ok(new { testHistoryId = testHistory.Id });

}).DisableAntiforgery();

// Открыть страницу теста
app.MapGet("/test-history/{*section}", [Authorize(Roles = "user")] () => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "protected", "test_history.html"), "text/html");
});

// Отображение результатов теста
app.MapGet("/api/test-history", [Authorize(Roles = "user")] async ([FromQuery] string testHistoryId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var accountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == accountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID теста
    if (!Guid.TryParse(testHistoryId, out var guidTestHistoryId)) {
        return Results.Content("uncorrectTestHistoryIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // История теста
    var testHistory = await db.TestHistories
        .AsNoTracking()
        .Where(t => t.Id == guidTestHistoryId)
        .Include(t => t.Questions)
        .ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync();

    // История теста не найдена
    if (testHistory == null) {
        return Results.Content("testHistoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    return Results.Ok(new { testHistory } );
});

// Веб-страница "Авторизация"
app.MapGet("/", (HttpContext context) => {
    return Results.File(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/html", "authorization.html"), "text/html; charset=utf-8");
});

// Аутентификация
app.MapPost("/api/authentication", async ([FromForm] string login, [FromForm] string password, HttpContext context, ApplicationContext db) => {
    // Аккаунт
    var authorizedAccount = await db.Accounts
        .AsNoTracking()
        .Where(a => a.Login == login && a.Password == password)
        .Select(a => new {
            a.Id,
            a.Login,
            a.Role
        })
        .FirstOrDefaultAsync();

    // Аккаунта не существует
    if (authorizedAccount == null) {
        return Results.Content("unauthorizedError", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Создание утверждений
    var claims = new List<Claim> {
        // ID учетной записи
        new Claim(ClaimTypes.NameIdentifier, authorizedAccount.Id.ToString()),
        // Роль учетной записи
        new Claim(ClaimsIdentity.DefaultRoleClaimType, ((Roles)authorizedAccount.Role).ToString())
    };

    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal);

    return Results.Ok();
}).DisableAntiforgery();

// Регистрация
app.MapPost("/api/registration", async ([FromForm] string username, [FromForm] string login, [FromForm] string password, [FromForm] string selectedLanguage, ApplicationContext db) => {
    // Логин существует
    if (await db.Accounts.AsNoTracking().AnyAsync(p => p.Login == login)) {
        return Results.Content("loginExistsError", "text/plain", System.Text.Encoding.UTF8, 400);
    }
    // Имя пользователя существует
    if (await db.Accounts.AsNoTracking().AnyAsync(p => p.Username == username)) {
        return Results.Content("usernameExistsError", "text/plain", System.Text.Encoding.UTF8, 400);
    }
    // Создать аккаунт
    try {
        Account account = new Account(Guid.NewGuid(), login, password, username, (int)Roles.user, DateTime.Now, new Parameters(selectedLanguage));
        await db.Accounts.AddAsync(account);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

// Веб-страница "Мои личный кабинет"
app.MapGet("/account/{*section}", [Authorize(Roles = "user, admin")] async (HttpContext context) => {
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("protected/account.html");
}).RequireAuthorization();

// Изменить имя
app.MapPost("/api/change-username", [Authorize(Roles = "user, admin")] async ([FromForm] string username, ClaimsPrincipal user, ApplicationContext db) => {

    // ID учетной записи в виде строки
    var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

    // ID учетной записи (GUID)
    if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId)) {
        return Results.Unauthorized();
    }

    // Учетная запись
    var account = await db.Accounts.Where(p => p.Id == userId).FirstOrDefaultAsync();

    // Если учетной записи не существует
    if (account == null) {
        return Results.Content("userDoesNotExist", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Если новое имя учетной записи существует
    if (await db.Accounts.AsNoTracking().AnyAsync(p => p.Username == username)) {
        return Results.Content("usernameExistsError", "text/plain", System.Text.Encoding.UTF8, 500);
    }

    // Изменить имя
    try {
        account.Username = username;
        await db.SaveChangesAsync();
        return Results.Json(new { username = account.Username });
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }

}).DisableAntiforgery().RequireAuthorization();

// Изменить пароль
app.MapPost("/api/change-password", [Authorize(Roles = "user, admin")] async ([FromForm] string password, ClaimsPrincipal user, ApplicationContext db) => {

    // ID учетной записи в виде строки
    var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

    // ID учетной записи (GUID)
    if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId)) {
        return Results.Unauthorized();
    }

    // Учетная запись
    var account = await db.Accounts.Where(p => p.Id == userId).FirstOrDefaultAsync();

    // Если учетной записи не существует
    if (account == null) {
        return Results.Content("userDoesNotExist", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Если новый пароль совпадает со старым
    if (password == account.Password) {
        return Results.Content("samePassword", "text/plain", System.Text.Encoding.UTF8, 500);
    }

    // Изменить пароль
    try {
        account.Password = password;
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery().RequireAuthorization();

// Изменить язык
app.MapPost("/api/change-language", [Authorize(Roles = "user, admin")] async ([FromForm] string language, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи в виде строки
    var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

    // ID учетной записи (GUID)
    if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId)) {
        return Results.Unauthorized();
    }

    // Учетная запись
    var account = await db.Accounts.Where(p => p.Id == userId).Include(p => p.Parameters).FirstOrDefaultAsync();

    // Если учетной записи не существует
    if (account == null) {
        return Results.Content("userDoesNotExist", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Если выбран текущий язык
    if (language == account.Parameters.SelectedLanguage) {
        return Results.Content("chooseAnotherLanguage", "text/plain", System.Text.Encoding.UTF8, 500);
    }

    // Изменить язык
    try {
        account.Parameters.SelectedLanguage = language;
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery().RequireAuthorization();

// Выход из системы
app.MapGet("/logout", [Authorize(Roles = "user, admin")] async (HttpContext context) => {
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
}).RequireAuthorization();

// Сохранить тест
app.MapPost("/api/save-test", [Authorize(Roles = "user")] async (ClaimsPrincipal user,
    [FromForm] string title,
    [FromForm] string categoryId,
    [FromForm] string description,
    [FromForm] IFormFile image,
    ApplicationContext db) => {
        // ID учетной записи
        if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
            return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
        }

        // Аккаунт пользователя
        if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
            return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
        }

        // ID теста
        Guid testId = Guid.NewGuid();

        // Парсинг категории
        if (!Guid.TryParse(categoryId, out var guidCategoryId)) {
            return Results.Content("invalidCategoryIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
        }

        // Если отсутствует категория
        if (!await db.Categories.AnyAsync(c => c.Id == guidCategoryId)) {
            return Results.Content("categoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
        }

        // Если изображение не получено
        if (image == null || image.Length == 0) {
            return Results.BadRequest("imageNotFound");
        }

        try {
            // Имя изображения
            var imageName = testId.ToString() + Path.GetExtension(image.FileName);

            // Созданный тест
            Test createdTest = new Test(testId, title, guidCategoryId, description, imageName, guidAccountId, DateTime.Now, (int)TestStatus.Unfinished);

            // Добавить категорию
            createdTest.CategoryId = guidCategoryId;

            // Добавить тест в базу данных
            await db.Tests.AddAsync(createdTest);

            // Сохранить изменения
            await db.SaveChangesAsync();

            // Путь папки "test_images"
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "test_images");
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }

            // Путь изображения
            var imagePath = Path.Combine(folder, imageName);

            // Загрузить изображение в папку
            using (var stream = new FileStream(imagePath, FileMode.Create)) {
                await image.CopyToAsync(stream);
            }

            // Отправить ID сохранненого теста
            return Results.Json(new { testId });
        }
        catch (Exception ex) {
            return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
        }

}).DisableAntiforgery();

// Изменить тест
app.MapPost("/api/change-test", [Authorize(Roles = "user")] async (ClaimsPrincipal user, ApplicationContext db,
    [FromForm] string testId,
    [FromForm] string? title = null,
    [FromForm] string? description = null,
    [FromForm] string? categoryId = null,
    [FromForm] IFormFile? image = null) => {
        // ID учетной записи
        if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
            return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
        }

        // Аккаунт пользователя
        if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
            return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
        }

        // ID теста
        if (!Guid.TryParse(testId, out var guidTestId)) {
            return Results.Content("invalidIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
        }

        // Тест
        var test = await db.Tests.Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId).FirstOrDefaultAsync();

        // Если тест не найден
        if (test == null) {
            return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
        }

        // =============
        // ИЗМЕНИТЬ ТЕСТ
        // =============
        try {
            // Изменить заголовок
            if (title != null) {
                test.Title = title;
            }

            // ID категории
            Guid? guidCategoryId = null;
            // Парсинг категории
            if (!string.IsNullOrWhiteSpace(categoryId)) {
                if (!Guid.TryParse(categoryId, out var parsedCategoryId)) {
                    return Results.Content("invalidCategoryIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
                }
                guidCategoryId = parsedCategoryId;
            }
            // Если отсутствует категория
            if (guidCategoryId != null && !await db.Categories.AnyAsync(c => c.Id == guidCategoryId)) {
                return Results.Content("categoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
            }
            // Изменить категорию
            test.CategoryId = guidCategoryId;

            // Изменить описание
            if (description != null) {
                test.Description = description;
            }
            // Изменить изображение
            if (image != null) {
                // Имя нового изображения
                var imageName = testId.ToString() + Path.GetExtension(image.FileName);

                // Путь папки "test_images"
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "test_images");
                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }

                // Путь старого изображения
                var oldImagePath = Path.Combine(folder, test.ImageFileName);

                // Удалить прежнее изображение
                if (System.IO.File.Exists(oldImagePath)) {
                    System.IO.File.Delete(oldImagePath);
                }

                // Путь нового изображения
                var newImagePath = Path.Combine(folder, imageName);

                // Загрузить новое изображение в папку
                using (var stream = new FileStream(newImagePath, FileMode.Create)) {
                    await image.CopyToAsync(stream);
                }
            }
            // Сохранить изменения
            await db.SaveChangesAsync();

            return Results.Ok();
        }
        catch (Exception ex) {
            return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
        }
}).RequireAuthorization().DisableAntiforgery();

// Получить тесты пользователя
app.MapGet("/api/user-tests", [Authorize(Roles = "user")] async ([FromQuery] int? limit, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество тестов пользователя
    var user_test_count = await db.Tests.Where(t => t.AuthorId == guidAccountId).CountAsync();

    // Количество тестов на одной странице
    int count = 10;

    if (limit.HasValue) {
        // Номер страницы
        int page = limit.Value;

        // Стартовый индекс (для 1-й страницы: 1*10 - 10 = 0)
        int start = page * count - count;

        // Переходы
        var back = start - count >= 0;
        var next = (start + count) < user_test_count;

        // Тесты пользователя
        var pagedTestData = await db.Tests
        .AsNoTracking()
        .Where(t => t.AuthorId == guidAccountId)
        .Skip(start)
        .Take(count)
        .Select(t => new {
            t.Id,
            t.Title,
            t.CategoryId,
            t.ImageFileName,
            t.CreatedDateTime,
            t.PublishedDateTime,
            t.Status
        }).ToListAsync();

        // Пронумерованные тесты
        var pagedTests = pagedTestData
        .Select((t, index) => new {
            Order = start + index + 1,
            t.Id,
            t.Title,
            Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault() ?? null,
            t.ImageFileName,
            t.CreatedDateTime,
            t.PublishedDateTime,
            Status = ((TestStatus)t.Status).ToString(),
        }).ToList();

        return Results.Json(new { back, next, tests = pagedTests, totalCount = user_test_count });
    }

    // Тесты пользователя
    var firstTestData = await db.Tests
        .AsNoTracking()
        .Where(t => t.AuthorId == guidAccountId)
        .Take(count)
        .Select(t => new {
            t.Id,
            t.Title,
            t.CategoryId,
            t.ImageFileName,
            t.CreatedDateTime,
            t.PublishedDateTime,
            t.Status
        })
        .ToListAsync();

    // Пронумерованные тесты
    var firstTests = firstTestData
    .Select((t, index) => new {
        Order = index + 1,
        t.Id,
        t.Title,
        Category = db.Categories.Where(c => c.Id == t.CategoryId).FirstOrDefault() ?? null,
        t.ImageFileName,
        t.CreatedDateTime,
        t.PublishedDateTime,
        Status = ((TestStatus)t.Status).ToString(),
    }).ToList();

    return Results.Json(new { back = false, next = count < user_test_count, tests = firstTests, totalCount = user_test_count });
});

// Получить историю тестов
app.MapGet("/api/test-histories", [Authorize(Roles = "user")] async ([FromQuery] int? limit, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Количество пройденных тестов
    var taked_test_count = await db.TestHistories.AsNoTracking().Where(t => t.UserId == guidAccountId).CountAsync();

    // Количество тестов на одной странице
    int count = 10;

    if (limit.HasValue) {
        // Номер страницы
        int page = limit.Value;

        // Стартовый индекс (для 1-й страницы: 1*10 - 10 = 0)
        int start = page * count - count;

        // Переходы
        var back = start - count >= 0;
        var next = (start + count) < taked_test_count;

        // Тесты пользователя
        var pagedTestData = await db.TestHistories
        .AsNoTracking()
        .Where(t => t.UserId == guidAccountId)
        .Skip(start)
        .Take(count)
        .Select(t => new {
            t.Id,
            t.Title,
            t.TestId,
            t.TakedDateTime,
            t.ImageFileName,
            t.Score,
            t.MaxScore,
        })
        .ToListAsync();

        // Пронумерованные тесты
        var pagedTests = pagedTestData
        .Select((t, index) => new {
            Order = start + index + 1,
            t.Id,
            t.Title,
            t.TestId,
            t.TakedDateTime,
            t.ImageFileName,
            t.Score,
            t.MaxScore
        }).ToList();

        return Results.Json(new { back, next, tests = pagedTests, totalCount = taked_test_count });
    }

    // Тесты пользователя
    var firstTestData = await db.TestHistories
        .AsNoTracking()
        .Where(t => t.UserId == guidAccountId)
        .Take(count)
        .Select(t => new {
            t.Id,
            t.Title,
            t.TestId,
            t.TakedDateTime,
            t.ImageFileName,
            t.Score,
            t.MaxScore
        })
        .ToListAsync();

    // Пронумерованные тесты
    var firstTests = firstTestData
    .Select((t, index) => new {
        Order = index + 1,
        t.Id,
        t.Title,
        t.TestId,
        t.TakedDateTime,
        t.ImageFileName,
        t.Score,
        t.MaxScore
    }).ToList();

    return Results.Json(new { back = false, next = count < taked_test_count, tests = firstTests, totalCount = taked_test_count });
});

// Удалить историю теста
app.MapPost("/api/delete-test-history", [Authorize(Roles = "user")] async ([FromQuery] string testHistoryId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID учетной записи
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Аккаунт пользователя
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID истории теста
    if (!Guid.TryParse(testHistoryId, out var guidTestHistoryId)) {
        return Results.Content("invalidTestIdFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // История теста
    var testHistory = await db.TestHistories
        .Where(t => t.Id == guidTestHistoryId && t.UserId == guidAccountId)
        .Include(t => t.Questions)
        .ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync();

    // Если история теста не найдена
    if (testHistory == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Удалить историю теста
        db.TestHistories.Remove(testHistory);

        // Удалить изображение, если тест не существует
        if (!await db.Tests.AnyAsync(t => t.Id == testHistory.TestId)) {
            // Пусть изображения
            var ImagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "test_images"), testHistory.ImageFileName);

            // Удалить изображение
            if (System.IO.File.Exists(ImagePath)) {
                System.IO.File.Delete(ImagePath);
            }
        }

        // Сохранить изменения
        await db.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

// Удалить тест
app.MapPost("/api/delete-test", async (string testId, ClaimsPrincipal user, ApplicationContext db) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests
        .Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId)
        .Include(t => t.Questions)
        .ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Удалить тест
        db.Tests.Remove(test);

        // Сохранить изменения
        await db.SaveChangesAsync();

        // Удалить изображение, если тест не пройден
        if (!await db.TestHistories.AnyAsync(t => t.TestId == guidTestId)) {
            // Пусть изображения
            var ImagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "test_images"), test.ImageFileName);

            // Удалить изображение
            if (System.IO.File.Exists(ImagePath)) {
                System.IO.File.Delete(ImagePath);
            }
        }

        return Results.Ok();
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

// Генерация вопросов
app.MapPost("/api/question-generation", [Authorize(Roles = "user")] async ([FromQuery] string testId, ApplicationContext db, ClaimsPrincipal user) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidTestId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidTestId && t.AuthorId == guidAccountId && t.Status == (int)TestStatus.Unfinished).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Вопросы теста
        QuestionView[] questionViews = test.Questions.Select(q => new QuestionView(q.Text, (int)q.Type, q.Answers.Select(a => new AnswerView(a.Text, a.IsCorrect)).ToList())).ToArray();

        // Перемешать вопросы
        Random.Shared.Shuffle(questionViews);

        // Максимально допустимое количество вопросов
        int questionsLimit = 10;

        // Обрезать вопросы
        if (questionViews.Length > questionsLimit) {
            questionViews = questionViews[..questionsLimit];
        }

        // Язык пользователя
        var language = await db.Accounts.Where(a => a.Id == guidAccountId).Select(a => a.Parameters.SelectedLanguage).FirstOrDefaultAsync();

        // Параметры ИИ
        GeminiParams geminiParams = new GeminiRepositorySettings(Path.Combine(Directory.GetCurrentDirectory(), "gemini.json")).Load();

        // Генерация вопросов
        var client = new Client();
        var response = await client.Models.GenerateContentAsync(
            model: geminiParams.Model,
            contents: geminiParams.GetQuestionGenerationQuery(questionViews, language)
        );

        // Ответ чат-бота
        string? rawJson = response?.Candidates?[0]?.Content?.Parts?[0].Text;

        // Сгенерированные вопросы
        QuestionView[] response_questions = JsonConvert.DeserializeObject<QuestionView[]>(rawJson ?? "") ?? Array.Empty<QuestionView>();

        if (response_questions == null) {
            return Results.Content("questionsGenerationError", "text/plain", System.Text.Encoding.UTF8, 500);
        }

        // Преобразовать вопросы в тип Question
        Question[] questions = new Question[response_questions.Length];

        // Перебор вопросов
        for (int i = 0; i < questions.Length; i++) {

            // Вопрос
            questions[i] = new Question(Guid.NewGuid(), response_questions[i].Text, (int)response_questions[i].Type);

            // Ответы
            Answer[] answers = new Answer[response_questions[i].Answers.Count];

            // Перебор ответов
            for (int j = 0; j < answers.Length; j++) {
                // Ответ
                answers[j] = new Answer(Guid.NewGuid(), response_questions[i].Answers[j].Text, response_questions[i].Answers[j].IsCorrect);
            }
            // Добавить ответы на вопрос
            questions[i].AddRangeAnswer(answers);
        }

        // Отправить вопросы
        return Results.Json(questions);
    }

    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
});

// Проверка грамматики
app.MapPost("/api/grammar-check", [Authorize(Roles = "user")] async ([FromQuery] string testId, ApplicationContext db, ClaimsPrincipal user) => {
    // ID теста
    if (!Guid.TryParse(testId, out var guidId)) {
        return Results.Content("invalidTestIDFormat", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Тест
    var test = await db.Tests.Where(t => t.Id == guidId && t.AuthorId == guidAccountId && t.Status == (int)TestStatus.Unfinished).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // Если тест не найден
    if (test == null) {
        return Results.Content("testNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Вопросы теста
        QuestionViewWithId[] questionViews = test.Questions.Select(q => new QuestionViewWithId(q.Id, q.Text, (int)q.Type, q.Answers.Select(a => new AnswerView(a.Text, a.IsCorrect)).ToList())).ToArray();

        // Параметры ИИ
        GeminiParams geminiParams = new GeminiRepositorySettings(Path.Combine(Directory.GetCurrentDirectory(), "gemini.json")).Load();

        // Генерация вопросов
        var client = new Client();
        var response = await client.Models.GenerateContentAsync(
            model: geminiParams.Model,
            contents: geminiParams.GetGrammarCheckQuery(questionViews)
        );

        // Ответ чат-бота
        string? rawJson = response?.Candidates?[0]?.Content?.Parts?[0].Text;

        // Сгенерированные вопросы
        QuestionViewWithId[] response_questions = JsonConvert.DeserializeObject<QuestionViewWithId[]>(rawJson ?? "") ?? Array.Empty<QuestionViewWithId>();

        if (response_questions == null) {
            return Results.Content("questionsGenerationError", "text/plain", System.Text.Encoding.UTF8, 500);
        }

        // Преобразовать вопросы в тип Question
        Question[] questions = new Question[response_questions.Length];

        // Перебор вопросов
        for (int i = 0; i < questions.Length; i++) {
            // Вопрос
            questions[i] = new Question(response_questions[i].Id, response_questions[i].Text, (int)response_questions[i].Type);

            // Ответы
            Answer[] answers = new Answer[response_questions[i].Answers.Count];

            // Перебор ответов
            for (int j = 0; j < answers.Length; j++) {
                // Ответ
                answers[j] = new Answer(Guid.NewGuid(), response_questions[i].Answers[j].Text, response_questions[i].Answers[j].IsCorrect);
            }
            // Добавить ответы на вопрос
            questions[i].AddRangeAnswer(answers);
        }

        // Вопросы пользователя
        var user_questions = test.Questions
        .Select(q => new {
            Id = q.Id,
            Text = q.Text,
            Type = (int)q.Type,
            Order = q.Order,
            Answers = q.Answers.Select(a =>
                new {
                    a.Text,
                    a.IsCorrect,
                })
        }).ToArray();

        // Вопросы ИИ
        var ai_questions = questions
        .Select(q => new {
            Id = q.Id,
            Text = q.Text,
            Type = (int)q.Type,
            Answers = q.Answers.Select(a =>
                new {
                    a.Text,
                    a.IsCorrect,
                })
        }).ToArray();

        // Отправить вопросы
        return Results.Json(new { user_questions, ai_questions });
    }

    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
});

// Спросить у ИИ
app.MapPost("/api/ask-ai", [Authorize(Roles = "user")] async ([FromQuery] string testHistoryId, [FromForm] string questionHistoryId, [FromForm] string questionForAskJson, ApplicationContext db, ClaimsPrincipal user) => {
    // ID аккаунта
    if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var guidAccountId)) {
        return Results.Content("invalidAccountId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // Если аккаунт не найден
    if (!await db.Accounts.AnyAsync(a => a.Id == guidAccountId)) {
        return Results.Content("accountNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID истории теста
    if (!Guid.TryParse(testHistoryId, out var guidTestHistoryId)) {
        return Results.Content("invalidTestHistoryId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // История теста
    var testHistory = await db.TestHistories.Where(t => t.Id == guidTestHistoryId).Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync();

    // История теста остутствует
    if (testHistory == null) {
        return Results.Content("testHistoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // ID истории вопроса
    if (!Guid.TryParse(questionHistoryId, out var guidQuestionHistoryId)) {
        return Results.Content("invalidQuestionHistoryId", "text/plain", System.Text.Encoding.UTF8, 400);
    }

    // История вопроса
    var questionHistory = testHistory.Questions.Where(q => q.Id == guidQuestionHistoryId).FirstOrDefault();

    // История вопроса отсутствует
    if (questionHistory == null) {
        return Results.Content("questionHistoryNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    // Вопрос для отправки ИИ
    QuestionForAsk? questionForAsk = JsonConvert.DeserializeObject<QuestionForAsk>(questionForAskJson);

    // Если вопрос пустой
    if (questionForAsk == null) {
        return Results.Content("questionNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
    }

    try {
        // Язык пользователя
        var language = await db.Accounts.Where(a => a.Id == guidAccountId).Select(a => a.Parameters.SelectedLanguage).FirstOrDefaultAsync();

        // Параметры ИИ
        GeminiParams geminiParams = new GeminiRepositorySettings(Path.Combine(Directory.GetCurrentDirectory(), "gemini.json")).Load();

        // Генерация вопросов
        var client = new Client();
        var response = await client.Models.GenerateContentAsync(
            model: geminiParams.Model,
            contents: geminiParams.GetQuestionForAskQuery(questionForAsk, language)
        );

        // Ответ чат-бота
        string? aiAnswer = response?.Candidates?[0]?.Content?.Parts?[0].Text;

        if (aiAnswer == null) {
            return Results.Content("AiAnswerNotFound", "text/plain", System.Text.Encoding.UTF8, 404);
        }

        // Сохранить ответ
        questionHistory.AiAnswer = aiAnswer;

        await db.SaveChangesAsync();

        return Results.Json(aiAnswer);
    }
    catch (Exception ex) {
        return Results.Content(ex.Message, "text/plain", System.Text.Encoding.UTF8, 500);
    }
}).DisableAntiforgery();

app.Run();

public record QuestionAndAnswers(Guid QuestionId, List<Guid> AnswersId);
public record LoginAndPassword(string Login, string Password);
public record UserRegistrationData(string Username, string Login, string Password, string SelectedLanguage);

public record AnswerView(string Text, bool IsCorrect);
public record QuestionView(string Text, int Type, List<AnswerView> Answers);
public record QuestionViewWithId(Guid Id, string Text, int Type, List<AnswerView> Answers);

public record AnswerData(Guid Id, string Text, bool IsCorrect);
public record QuestionData(Guid id, string text, int type, AnswerData[] answerDataCollection);
public record QuestionForAsk(string testTitle, string questionText, string[] wrongUserAnswers, string[] correctAnswers);