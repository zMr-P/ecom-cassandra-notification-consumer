namespace Domain.Interfaces;

public interface IEmailTemplateProvider
{
    string ReadTemplate(string templateName);
}