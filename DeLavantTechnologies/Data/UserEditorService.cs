using Microsoft.AspNetCore.Identity;
using DeLavantTechnologies.Data;

public class UserEditorService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    public UserEditorService(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
    }

    public async Task<(bool Success, string ErrorMessage)> SaveFullName(ApplicationUser user, string fullName)
    {
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) return (false, "Введите минимум Фамилию и Имя");
        if (parts.Length > 3) return (false, "Слишком много слов в ФИО");

        user.LastName = parts[0];
        user.FirstName = parts[1];
        user.MiddleName = parts.Length == 3 ? parts[2] : "";

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return (false, "Ошибка сохранения");

        return (true, "ФИО сохранено");
    }

    public async Task<(bool Success, string ErrorMessage)> SaveEmail(ApplicationUser user, string email)
    {
        user.Email = email;
        await _userStore.SetUserNameAsync(user, email, default);
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return (false, "Ошибка сохранения email");

        return (true, "Email сохранён");
    }

    public async Task<(bool Success, string ErrorMessage)> SaveRole(ApplicationUser user, string role)
    {
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (!currentRoles.Contains(role))
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role);
        }
        return (true, $"Роль изменена на {role}");
    }

    public async Task<(bool Success, string ErrorMessage)> UpdatePassword(ApplicationUser user, string password)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded) return (false, "Ошибка при обновлении пароля");

        return (true, "Пароль успешно обновлён");
    }

    // Метод для сохранения всех изменений
    public async Task<(bool Success, string Message)> SaveAllChanges(
        ApplicationUser user,
        string fullName,
        string email,
        string role,
        IEnumerable<string> selectedPositions)
    {
        if (user == null) return (false, "Пользователь не найден");

        // Разбор ФИО
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
            return (false, "Введите минимум Фамилию и Имя");
        if (parts.Length > 3)
            return (false, "Слишком много слов в ФИО");

        user.LastName = parts[0];
        user.FirstName = parts[1];
        user.MiddleName = parts.Length == 3 ? parts[2] : "";

        // Email
        user.Email = email;
        await _userStore.SetUserNameAsync(user, email, default);

        // Роль
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (!currentRoles.Contains(role))
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role);
        }

        // Должности
        user.Positions = selectedPositions.ToList();

        // Сохраняем все изменения
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return (true, "Все изменения сохранены");

        return (false, "Ошибка при сохранении изменений");
    }
}


