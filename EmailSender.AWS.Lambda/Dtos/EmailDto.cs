namespace EmailSender.AWS.Lambda.Dtos;
public record EmailDto(string Email, string? Html = default, string? Title = default);
