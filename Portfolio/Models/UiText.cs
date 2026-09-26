namespace Portfolio.Models;

public record UiText(
    string Lang,
    string NavAria,
    string ScrollDown,
    string Phone,
    string SectionStack,
    string SectionExpertise,
    string SectionExperience,
    string SectionEducation,
    string SectionPrinciples,
    string SectionLanguages,
    string DigitalCertificate,
    string DegreeTitle,
    string DegreeText,
    string ContactTitle,
    string PreferredContact)
{
    public static readonly UiText Ru = new(
        Lang: "ru",
        NavAria: "Навигация",
        ScrollDown: "Прокрутить вниз",
        Phone: "Телефон",
        SectionStack: "Стек технологий",
        SectionExpertise: "Экспертиза",
        SectionExperience: "Опыт работы",
        SectionEducation: "Образование",
        SectionPrinciples: "Как я работаю",
        SectionLanguages: "Языки",
        DigitalCertificate: "Электронный сертификат",
        DegreeTitle: "Прикладная информатика",
        DegreeText: "Высшее профильное образование + международные сертификации Cisco",
        ContactTitle: "Давайте работать вместе",
        PreferredContact: "— предпочитаемый способ связи");

    public static readonly UiText En = new(
        Lang: "en",
        NavAria: "Navigation",
        ScrollDown: "Scroll down",
        Phone: "Phone",
        SectionStack: "Tech Stack",
        SectionExpertise: "Expertise",
        SectionExperience: "Experience",
        SectionEducation: "Education",
        SectionPrinciples: "How I Work",
        SectionLanguages: "Languages",
        DigitalCertificate: "Digital certificate",
        DegreeTitle: "Applied Informatics",
        DegreeText: "Relevant university degree + international Cisco certifications",
        ContactTitle: "Let's Work Together",
        PreferredContact: "— preferred contact method");

    public static UiText For(string lang) => lang == "en" ? En : Ru;
}
