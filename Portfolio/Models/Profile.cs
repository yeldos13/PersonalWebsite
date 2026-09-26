namespace Portfolio.Models;

public record Profile(
    string FullName,
    string ShortName,
    string Role,
    string Tagline,
    string Summary,
    string Location,
    string Citizenship,
    Contacts Contacts,
    IReadOnlyList<Stat> Stats,
    IReadOnlyList<SkillGroup> SkillGroups,
    IReadOnlyList<Expertise> Expertise,
    IReadOnlyList<Job> Experience,
    IReadOnlyList<Education> Education,
    IReadOnlyList<Certificate> Certificates,
    IReadOnlyList<Principle> Principles,
    IReadOnlyList<SpokenLanguage> Languages,
    string WorkPreferences);

public record Contacts(string Telegram, string Email, string Phone)
{
    public string TelegramUrl => $"https://t.me/{Telegram.TrimStart('@')}";
    public string PhoneUrl => "tel:" + new string(Phone.Where(c => char.IsDigit(c) || c == '+').ToArray());
}

public record Stat(string Value, string Label);

public record SkillGroup(string Title, string Dot, IReadOnlyList<Skill> Items);

public record Skill(string Name, string Badge, string Color);

public record Expertise(string Icon, string Title, string Tag, string Description, IReadOnlyList<string> Tags);

public record Job(
    string Title,
    string Company,
    string CompanyUrl,
    string Period,
    string Duration,
    string Location,
    string Description,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<string> Tags);

public record Education(string Year, string Degree, string Institution, string Details);

public record Certificate(string Year, string Title, string Issuer);

public record Principle(string Title, string Text, string Initials, string Color);

public record SpokenLanguage(string Name, string Level, string Code, int Percent);
