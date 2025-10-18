using System.ComponentModel.DataAnnotations;
using User.Api.Enum;

namespace User.Api.Dto;

public record CreateUserDto : IValidatableObject
{
    [Required(ErrorMessage = "Nome de usuário é obrigatório.")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "E-mail é obrigatório."), EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; init; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O nome deve conter apenas letras.")]
    public string Firstname { get; init; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O sobrenome deve ter pelo menos 2 caracteres.")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O sobrenome deve conter apenas letras.")]
    public string Lastname { get; init; } = string.Empty;

    [Required(ErrorMessage = "Tipo do usuário é obrigatório.")]
    public UserType UserType { get; init; }

    [StringLength(128, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "É necessário confirmar a senha.")]
    // ReSharper disable once MemberCanBePrivate.Global
    public string ConfirmPassword { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Password.Any(char.IsUpper))
            yield return new ValidationResult("A senha deve conter pelo menos uma letra maiúscula.",
                [nameof(Password)]);
        if (!Password.Any(char.IsDigit))
            yield return new ValidationResult("A senha deve conter pelo menos um número.", [nameof(Password)]);
        if (Password != ConfirmPassword)
            yield return new ValidationResult("Senhas não coincidem.", [nameof(Password)]);
    }
}